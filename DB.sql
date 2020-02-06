CREATE DATABASE UserDB
GO
USE UserDB
GO

Create table Users(
	Id int not null identity(1,1) primary key,
	FirstName varchar(50) not null,
	LastName varchar(50) not null
)