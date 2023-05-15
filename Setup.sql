CREATE DATABASE UserQuestions


USE UserQuestions

CREATE LOGIN devUser   
    WITH PASSWORD = 'devPassword';  
GO  

-- Creates a database user for the login created above.  
CREATE USER devUser FOR LOGIN devUser;  
GO  

EXEC sp_addrolemember N'db_owner', N'devUser'
GO

CREATE TABLE [User]
(
	Id int identity(1,1) not null,
	Name varchar(100) not null
)

CREATE TABLE [Question]
(
	Id int identity(1,1) not null,
	Text varchar(max) not null
)

CREATE TABLE [UserQuestion]
(
	Id int identity(1,1) not null,
	UserId int not null,
	QuestionId int not null,
	Answer varchar(max) not null
)

INSERT INTO Question values ('In what city were you born?');
INSERT INTO Question values ('What is the name of your favorite pet?');
INSERT INTO Question values ('What is your mother''s maiden name?');
INSERT INTO Question values ('What high school did you attend?');
INSERT INTO Question values ('What was the mascot of your high school?');
INSERT INTO Question values ('What was the make of your first car?');
INSERT INTO Question values ('What was you favorite toy as a child?');
INSERT INTO Question values ('Where did you meet your spouse?');
INSERT INTO Question values ('What is your favorite meal?');
INSERT INTO Question values ('Who is your favorite actor/actress?');
INSERT INTO Question values ('What is your favorite album?');

select * from User
select * from UserQuestion
