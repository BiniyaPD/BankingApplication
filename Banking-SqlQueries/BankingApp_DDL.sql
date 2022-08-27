--Create database db_banking;
USE  db_bank;

CREATE SEQUENCE ManagerSequence
CREATE TABLE manager(
managerId varchar(20) CONSTRAINT [PK_MyTable] PRIMARY KEY (managerId)
firstName Nvarchar(30) null,
lastName NVARCHAR(30) null,
gender varchar(10) null,
DOB date null,
managerPassword Nvarchar(60) null,
emailId NVARCHAR(50) null,
mobileNumber NVARCHAR(10) null
);

CREATE TABLE customerlogin(
customerId varchar(20) PRIMARY KEY,
customerpassword varchar(25) NOT NULL
);

GO
CREATE SEQUENCE CustomerSequence
CREATE TABLE customer(
customerId varchar(20) CONSTRAINT [PK_CUstomerTable] PRIMARY KEY (customerId)
managerId varchar(20) foreign key references manager(managerId),
firstName Nvarchar(30) not null,
lastName NVARCHAR(30) not null,
gender varchar(10) not null,
DOB date not null,
emailId NVARCHAR(50)not null,
mobileNumber NVARCHAR(15) not null,
city varchar(30) not null,
[state] varchar(30)not null,
pincode varchar(6) not null,
isDeleted bit not null
)


GO
CREATE TABLE account(
accountNumber INT PRIMARY KEY IDENTITY(1,1),
customerId VARCHAR(20) FOREIGN KEY REFERENCES customer(customerId),
balance FLOAT not null,
DOC DATE not null,
TIN VARCHAR(20) not null,
IFSC VARCHAR(20) not null,
isDeleted bit not null
)


GO
CREATE SEQUENCE SavingsSequence
CREATE TABLE savingsAccount(
savingsAccountNo varchar(20) CONSTRAINT [PK_SavingsTable] PRIMARY KEY (savingsAccountNo)
accountNumber INT FOREIGN KEY REFERENCES account(accountNumber),
withdrawlLimit FLOAT not null,
minimumBalance FLOAT not null
)


GO
CREATE SEQUENCE CurrentSequence
CREATE TABLE currentAccount(
currentAccountNo varchar(20) CONSTRAINT [PK_CurrentTable] PRIMARY KEY (currentAccountNo)
accountNumber INT FOREIGN KEY REFERENCES account(accountNumber),
withdrawlLimit FLOAT not null,
minimumBalance FLOAT not null
)


GO
CREATE SEQUENCE CorporateSequence
CREATE TABLE corporateAccount(
corporateAccountNo varchar(20) CONSTRAINT [PK_CorporateTable] PRIMARY KEY (corporateAccountNo)
accountNumber INT FOREIGN KEY REFERENCES account(accountNumber),
withdrawlLimit FLOAT not null,
minimumBalance FLOAT not null
)


GO
CREATE TABLE [transaction](
transactionId INT PRIMARY KEY IDENTITY(1000,1),
sourceAccountNo INT FOREIGN KEY REFERENCES account(accountNumber),
transactionAmount FLOAT not null,
transactionType VARCHAR(10) not null,
transactionDate DATE not null,
destinationAccountNo INT null,
transactionDescription NVARCHAR(30) not null
)
GO



