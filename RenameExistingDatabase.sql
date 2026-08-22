USE [master];
GO

IF DB_ID(N'AccountingSystemDB') IS NOT NULL
BEGIN
    PRINT N'Database AccountingSystemDB already exists. No rename was performed.';
END
ELSE IF DB_ID(N'RestaurantManagement1') IS NOT NULL
BEGIN
    ALTER DATABASE [RestaurantManagement1] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    ALTER DATABASE [RestaurantManagement1] MODIFY NAME = [AccountingSystemDB];
    ALTER DATABASE [AccountingSystemDB] SET MULTI_USER;
    PRINT N'Database renamed from RestaurantManagement1 to AccountingSystemDB successfully.';
END
ELSE
BEGIN
    PRINT N'Database RestaurantManagement1 was not found on this SQL Server instance.';
END
GO
