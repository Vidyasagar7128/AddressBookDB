use bridgeLabz
CREATE PROCEDURE createRecord(
	@FirstName varchar(50),
	@LastName varchar(50),
	@Address varchar(50),
	@City varchar(50),
	@State varchar(50),
	@Zip varchar(50),
	@Phone varchar(50),
	@Email varchar(50),
	@BookName varchar(50),
	@Type varchar(20)
)
AS
BEGIN
SET NOCOUNT ON;
INSERT into addressBook values(@FirstName,@LastName,@Address,@City,@State,@Zip,@Phone,@Email,@BookName,@Type);
END
GO
CREATE table addressBook(
	Id int IDENTITY Primary key,
	FirstName varchar(50),
	LastName varchar(50),
	Address varchar(50),
	City varchar(50),
	State varchar(50),
	Zip varchar(50),
	Phone varchar(50),
	Email varchar(50),
);
SELECT * from addressBook