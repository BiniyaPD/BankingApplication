using BankingApp.BusinessLayer.Contracts;
using BankingApp.CommonLayer.Models;
using BankingApp.EFLayer.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.BusinessLayer.Implementation
{
    public class UserAccountAsyncManager:IUserAccountAsyncManager
    {
        private readonly IUserAccountAsyncRepository userAccountAsyncRepository;
        private readonly IConfiguration configuration;
        private readonly ICustomerManager customerManager;

        public UserAccountAsyncManager(IUserAccountAsyncRepository userAccountAsyncRepository,IConfiguration configuration,ICustomerManager customerManager)
        {
            this.userAccountAsyncRepository = userAccountAsyncRepository;
            this.configuration = configuration;
            this.customerManager = customerManager;
        }

        public async Task<TokenResponse> GetUserToken(string customerId,string password)
        {
            TokenResponse response = new TokenResponse();
            if(customerId!=null && password!=null)
            {
                Customer customer = new Customer();
                customer = customerManager.GetCustomerById(customerId);
                if(customer!=null)
                {
                    bool isValidated = await userAccountAsyncRepository.ValidateUser(customerId, password);
                    if (isValidated == true)
                    {
                        TokenRequest.CustomerId = customerId;
                        //create claims details based on user information
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub,configuration["Jwt:Subject"]),
                            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                            new Claim("Id",customer.CustomerId),
                            new Claim("FirstName",customer.FirstName),
                            new Claim("LastName",customer.LastName),
                            new Claim("EmailId",customer.EmailId)
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                        response.Token = new JwtSecurityTokenHandler().WriteToken(token);
                        response.Message = "User validated successfully";
                        return response;

                    }
                    else
                    {
                        response.Message = "The username and password does not match";
                        return response;
                    }
                }
                else
                {
                    response.Message = $"Customer with CustomerId:{customerId} Not exist..";
                    return response;
                }
               
            }
            else
            {
                response.Message = "Required user details are not provided";
                return response;
            }
        }
    }
}
