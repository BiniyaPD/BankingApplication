use db_bank;
SELECT * FROM dbo.account;
SELECT * FROM dbo.savingsAccount;
SELECT * FROM dbo.currentAccount;
SELECT * FROM dbo.corporateAccount;
SELECT * FROM dbo.[transaction];
SELECT * FROM dbo.customer;
SELECT * FROM dbo.manager;
SELECT * FROM dbo.customerlogin;



INSERT INTO dbo.manager(firstName,lastName,gender,DOB,managerPassword,emailId,mobileNumber)
VALUES('Malavika','Sunil','Female','1999-07-26','Malavika@123','malavika@gmail.com','9876543234');

INSERT INTO dbo.manager(firstName,lastName,gender,DOB,managerPassword,emailId,mobileNumber)
VALUES('Mariya','Jose','Female','1999-05-16','Mariya@123','mariya@gmail.com','8876543284');

INSERT INTO dbo.manager(firstName,lastName,gender,DOB,managerPassword,emailId,mobileNumber)
VALUES('Anoop','Charly','Male','1993-07-13','Anoop@123','anoop@gmail.com','9872223234');

INSERT INTO dbo.manager(firstName,lastName,gender,DOB,managerPassword,emailId,mobileNumber)
VALUES('Samual','Jose','Male','1991-09-26','Samual@123','samual@gmail.com','9870099006');

INSERT INTO dbo.manager(firstName,lastName,gender,DOB,managerPassword,emailId,mobileNumber)
VALUES('Rahul','Prakash','Male','1991-01-26','Rahul@123','rahul@gmail.com','7667564456');

delete from dbo.customer where customerId in('C-0000022');

alter table dbo.account add accountType NVARCHAR(20) NOT NUll;

alter table dbo.currentAccount add TinNumber nvarchar(20) null;


INSERT INTO dbo.customerlogin(customerId,customerpassword) VALUES('C-0000014','RamMohan@123');
INSERT INTO dbo.customerlogin(customerId,customerpassword) VALUES('C-0000009','Nisha@123');
INSERT INTO dbo.customerlogin(customerId,customerpassword) VALUES('C-0000016','SamThomas@123');
INSERT INTO dbo.customerlogin(customerId,customerpassword) VALUES('C-0000017','Mahi@123');
INSERT INTO dbo.customerlogin(customerId,customerpassword) VALUES('C-0000021','Rohit@123');
INSERT INTO dbo.customerlogin(customerId,customerpassword) VALUES('C-0000025','TinuJoseph@123');