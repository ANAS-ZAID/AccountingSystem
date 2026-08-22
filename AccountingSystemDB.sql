CREATE DATABASE AccountingSystemDB;
USE AccountingSystemDB;
go

-- جدول شجرة الحسابات
create table ChartOfAccounts(
id int  primary key identity(1,1),
name  NVARCHAR(MAX) not null,
type  NVARCHAR(15) not null,
accountNumber int  ,
natureOfAccount text not null,
accountLocation   NVARCHAR(MAX),
rankk int not null,
parentId int,
accountGroupId int ,
constraint FK_ChartOfAccounts_Parent foreign key (parentId)
references ChartOfAccounts (id),
 constraint FK_ChartOfAccount_AccountGroup foreign key (accountGroupId)
references AccountsGroups (id),
)


--alter table ChartOfAccounts
--alter column id   identity(1,1) ;
-- جدول العملات
CREATE TABLE Currencies (
    id INT IDENTITY(1,1) PRIMARY KEY,
	name NVARCHAR(MAX),
    code NVARCHAR(MAX),
    exchangeRate DECIMAL(18,6),
	currencyType NVARCHAR(MAX),
);

create table AccountsGroups(
 id int identity(1,1) primary key,
 name NVARCHAR(MAX) NOT NULL,
)
create table City(
 id int identity(1,1) primary key,
 name NVARCHAR(MAX) NOT NULL,
)
CREATE PROCEDURE addCity
    @name NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO City (name)
    VALUES (@name)
	SELECT SCOPE_IDENTITY() AS NewId;
END
CREATE PROCEDURE updateCity
    @id INT,
    @name NVARCHAR(MAX)
AS
BEGIN
    UPDATE City
    SET name = @name
    WHERE id = @id;
END
create table Area(
 id int identity(1,1) primary key,
 name NVARCHAR(MAX) NOT NULL,
  cityId int,
constraint FK_Area_City foreign key (cityId)
references City (id)
)
CREATE PROCEDURE addArea
    @name NVARCHAR(MAX),
	@cityId int
AS
BEGIN
    INSERT INTO Area(name,cityId)
    VALUES (@name,@cityId)
	SELECT SCOPE_IDENTITY() AS NewId;
END
CREATE PROCEDURE updateArea
    @id INT,
    @name NVARCHAR(MAX),
	@cityId int
AS
BEGIN
    UPDATE Area
    SET name = @name, cityId=@cityId
    WHERE id = @id;
END
--drop PROCEDURE getArea
CREATE PROCEDURE getArea
 
AS
BEGIN
    SELECT 
        a.id,
        a.name,
        c.name as cityName,
		a.cityId 
    FROM
        Area a
    INNER JOIN City c ON a.cityId = c.id
END
create table Companies(
 id int identity(1,1) primary key,
 name NVARCHAR(MAX) NOT NULL,
)
create table TypesClassify(
 id int identity(1,1) primary key,
 name NVARCHAR(MAX) NOT NULL,
  companyId int,
constraint FK_TypeClassify_Companies foreign key (companyId)
references Companies (id)
)
--المخازن

create table Stores(
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(MAX),
   address NVARCHAR(MAX),
   accountId int ,
	 constraint FK_Store_ChartOfAccount foreign key (accountId)
references ChartOfAccounts (id),

);

-- جدول الصناديق
CREATE TABLE Cashiers (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(MAX),
	accountId int ,
	 constraint FK_Cashier_ChartOfAccount foreign key (accountId)
references ChartOfAccounts (id),
   -- openingBalance DECIMAL(18,2),
);
--alter table Cashiers
--add  accountId int references ChartOfAccounts(id);
-- جدول الفروع
create table Branches(
 id int identity(1,1) primary key,
 name NVARCHAR(MAX) NOT NULL,
 administratorName NVARCHAR(MAX) NOT NULL,
 phoneNumber NVARCHAR(MAX),
 address NVARCHAR(MAX),
 storeId int NULL ,
 cityId int,
 areaId int,
  CONSTRAINT FK_Store_Branche FOREIGN KEY (storeId) REFERENCES Stores(id),
constraint FK_City_Branche foreign key (cityId)
references City (id),
constraint FK_Area_Branche foreign key (areaId)
references Area (id),
)

CREATE PROCEDURE getAllBranches
 
AS
BEGIN
    SELECT 
        b.id,
        b.name,
		b.administratorName,
		b.phoneNumber,
        c.name as cityName,
		a.name as areaName,
		b.cityId ,
		b.areaId,
		b.address,
		b.storeId

    FROM
        Branches b
    INNER JOIN City c ON b.cityId = c.id
	 INNER JOIN Area a ON b.areaId = a.id
	  LEFT OUTER JOIN Stores s ON b.storeId = s.id
END

-- إجراء لإدخال بيانات جديدة إلى جدول "Branches"
CREATE PROCEDURE addBranch
    @Name NVARCHAR(MAX),
    @AdministratorName NVARCHAR(MAX),
    @PhoneNumber NVARCHAR(MAX),
    @CityId INT, 	
    @AreaId INT,
	@Address NVARCHAR(MAX),
	@StoreId INT
AS
BEGIN
    INSERT INTO Branches (Name, administratorName, PhoneNumber, CityId,AreaId,Address, StoreId)
    VALUES (@Name, @AdministratorName, @PhoneNumber, @CityId, @AreaId, @Address, @StoreId)
END

-- إجراء لتعديل بيانات موجودة في جدول "Branches"
CREATE PROCEDURE updateBranch
    @Id INT,
    @Name NVARCHAR(MAX),
    @AdministratorName NVARCHAR(MAX),
    @PhoneNumber NVARCHAR(MAX),
    @CityId INT, 	
    @AreaId INT,
	@Address NVARCHAR(MAX),
	@StoreId INT
AS
BEGIN
    UPDATE Branches
    SET Name = @Name,
        AdministratorName = @AdministratorName,
        PhoneNumber = @PhoneNumber,
        CityId = @CityId,
        AreaId = @AreaId,
		Address = @Address, 
		StoreId = @StoreId
    WHERE Id = @Id
END
-- جدول العملاء
create table Customers(
 id int identity(1,1) primary key,
 name NVARCHAR(MAX) NOT NULL,
 phoneNamber NVARCHAR(MAX),
 address text,
 accountId int,
 cityId int,
 areaId int,
 constraint FK_Customer_ChartOfAccount foreign key (accountId)
references ChartOfAccounts (id),
constraint FK_Customer_City foreign key (cityId)
references City (id),
constraint FK_Customer_Area foreign key (areaId)
references Area (id),
)

-- جدول انواع الموظفين
CREATE TABLE EmployeesTypes (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(MAX)
);
-- جدول الموظفين
create table Employees(
 id int identity(1,1) primary key,
 name NVARCHAR(MAX) NOT NULL,
 password NVARCHAR(MAX) not null,
 phoneNamber NVARCHAR(MAX),
 status BIT,
 loginName NVARCHAR(MAX) NOT NULL,

 accountId int not null,
 cashierId int,
  brancheId int,
  employeeTypeId int,
constraint FK_Employee_ChartOfAccount foreign key (accountId)
references ChartOfAccounts (id),
 CONSTRAINT FK_cashier_employee FOREIGN KEY (cashierId) REFERENCES Cashiers(id),
  CONSTRAINT FK_Branche_employee FOREIGN KEY (brancheId) REFERENCES Branches(id),
   CONSTRAINT FK_TypeEmployee_employee FOREIGN KEY (employeeTypeId) REFERENCES EmployeesTypes(id)
)
alter table Employees
add    loginPassword NVARCHAR(MAX) NOT NULL
-- جدول مجموعات الاصناف
CREATE TABLE ClassifyGroups (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(MAX),
	ordering int null,
	image VARBINARY(MAX) null, -- لتخزين صور المجموعه

);

-- جدول الوحدات
CREATE TABLE Units (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(MAX)
);

-- جدول الاصناف
create table Classify(
 id int identity(1,1) primary key,
  nameAr  NVARCHAR(MAX) NOT NULL,
  nameEn  NVARCHAR(MAX) NOT NULL,
  image VARBINARY(MAX), -- لتخزين صور المنتجات
  type NVARCHAR(MAX) not null,
  description NVARCHAR(MAX),
  ClassifyNumber int  ,
  parentId int,
  ClassifyGroupId int,
  typeClassifyId int,
   companyId int,
   visible BIT,
   constraint FK_Classify_Parent foreign key (parentId)
references Classify (id),
   constraint FK_Classify_Companies foreign key (companyId)
   references Companies (id),
   	constraint FK_ClassifyGroup_Classify foreign key (ClassifyGroupId) references ClassifyGroups(id),
        constraint FK_TypeClassify_Classify foreign key (typeClassifyId) references TypesClassify(id),
)
alter table Classify
add  visible BIT

alter table Classify
alter column  nameEn  NVARCHAR(MAX)  null
--add      constraint FK_Classify_Companies foreign key (companyId)
--   references Companies (id)
-- جدول القياسات المختلفة للمنتجات
create table MeasurementsItems(
 id int identity(1,1) primary key,
 UnitId int ,
 barcode int,
 itemId INT,
  purchasePrice decimal(10,2),
  sellingPrice decimal (10,2),
   WholesalePrice decimal(10,2),
  WholesalePurchasePrice decimal (10,2),
  descountPrice decimal(10,5),
  minimumPurchaseAmount decimal(10,2),
  	 constraint  FK_MeasurementItem_Classify foreign key (itemId) references Classify(id),
	constraint FK_MeasurementItem_Unit foreign key (UnitId) references Units(id),

)
alter table MeasurementsItems
alter column   minimumPurchaseAmount decimal (10,2)
-- جدول الوصفات
create table CompositeItem(
 id int identity(1,1) primary key,
 measurementItemId int ,
 componentItemId int,
 quantity decimal (10,2),
   purchasePrice decimal(10,2),
  sellingPrice decimal (10,2),
 constraint FK_itemComponent_CompositesItems foreign key (componentItemId) references MeasurementsItems(id),
 constraint FK_measurementItem_CompositesItems foreign key (measurementItemId) references MeasurementsItems(id)
)

-- جدول الموردين
CREATE TABLE Suppliers (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(MAX),
    address NVARCHAR(MAX),
	 accountId int unique NOT NULL,
	 phoneNumber nvarchar(13),
 constraint FK_ESupplier_ChartOfAccount foreign key (accountId)
references ChartOfAccounts (id),
);




-- جدول المخزون
CREATE TABLE Inventory (
    id INT IDENTITY(1,1) PRIMARY KEY,
    measurementItemId INT,
    quantity INT,
	itemId INT,
	storeId int ,
    CONSTRAINT FK_MeasurementItem_Inventory FOREIGN KEY (measurementItemId) REFERENCES MeasurementsItems(id),
	 CONSTRAINT FK_item_Inventory FOREIGN KEY (itemId) REFERENCES Classify(id),
	CONSTRAINT FK_Store_Inventory FOREIGN KEY (storeId) REFERENCES Stores(id)

);

alter table Inventory
add  CONSTRAINT FK_item_Inventory FOREIGN KEY (itemId) REFERENCES Classify(id)
-- جدول المشتريات
CREATE TABLE Purchases (
    id INT IDENTITY(1,1) PRIMARY KEY,
	number int unique,
	cashierId int,
	employeeId int,
	storeId int,
    supplierId INT,
    currencyId int,
    date DATE,
	enteryDate DATE,
	paymentType text,
	type text,
	description text,
	amountPaid DECIMAL(18,2),
	priceType Text,
	CONSTRAINT FK_cashier_Purchase FOREIGN KEY (cashierId) REFERENCES Cashiers(id),
	    CONSTRAINT FK_empoleey_Purchase FOREIGN KEY (employeeId) REFERENCES Employees(id),
	    CONSTRAINT FK_Store_Purchase FOREIGN KEY (storeId) REFERENCES Stores(id),
	    CONSTRAINT FK_supplier_Purchase FOREIGN KEY (supplierId) REFERENCES Suppliers(id),
		 constraint FK_Currencie_Purchase foreign key (currencyId)
       references Currencies (id)

);

alter table Purchases
drop column totalAmount
-- جدول تفاصيل المشتريات
CREATE TABLE PurchaseDetails (
    id INT IDENTITY(1,1) PRIMARY KEY,
    purchaseID INT,
    measurementItemId INT,
		 itemId INT,
    quantity decimal (10,2),
    unitPrice DECIMAL(10,2),
	description NVARCHAR(MAX),
	type NVARCHAR(MAX),
	endDate date,
	  CONSTRAINT FK_MeasurementItem_PurchaseDetails FOREIGN KEY (measurementItemId) REFERENCES MeasurementsItems(id),
	   CONSTRAINT FK_item_PurchaseDetails FOREIGN KEY (itemId) REFERENCES Classify(id),
	   CONSTRAINT FK_Purchase_PurchaseDetails FOREIGN KEY (purchaseID) REFERENCES Purchases(id),

);


-- جدول المبيعات
CREATE TABLE Sales (
    id INT IDENTITY(1,1) PRIMARY KEY,
	number int unique,
	cashierId int,
	employeeId int,
	storeId int,
	customerId INT,
	currencyId int,
	type text,
    date DATE,
	enteryDate DATE,
	paymentType text,
	priceType Text,
	orderType Text,
	orderTime TEXT,
	description text,
    amountPaid DECIMAL(18,2),
	descountPrice DECIMAL(18,2),
	   CONSTRAINT FK_cashier_Sale FOREIGN KEY (cashierId) REFERENCES Cashiers(id),
	    CONSTRAINT FK_empoleey_Sale FOREIGN KEY (employeeId) REFERENCES Employees(id),
	    CONSTRAINT FK_Store_Sale FOREIGN KEY (storeId) REFERENCES Stores(id),
	   CONSTRAINT FK_customer_Sale FOREIGN KEY (customerId) REFERENCES Customers(id),
	   constraint FK_Currencie_Sale foreign key (currencyId)
       references Currencies (id)

);


-- جدول تفاصيل المبيعات
CREATE TABLE SaleDetails (
    id INT IDENTITY(1,1) PRIMARY KEY,
    saleID INT,
    measurementItemId INT,
	itemId INT,
	descountPrice DECIMAL(10,5),
	type NVARCHAR(MAX),
    quantity  DECIMAL(10,2),
    unitPrice DECIMAL(10,2),
	description NVARCHAR(MAX),
		  CONSTRAINT FK_MeasurementItem_SaleDetails FOREIGN KEY (measurementItemId) REFERENCES MeasurementsItems(id),
		   CONSTRAINT FK_item_SaleDetails FOREIGN KEY (itemId) REFERENCES Classify(id),
		    CONSTRAINT FK_Salse_SaleDetails FOREIGN KEY (saleID) REFERENCES Sales(id),
);
alter table SaleDetails
alter column  unitPrice decimal (10,2)
alter table SaleDetails
drop column purchasePrice 
alter table SaleDetails
add  CONSTRAINT FK_item_SaleDetails FOREIGN KEY (itemId) REFERENCES Classify(id)
-- جدول السندات
CREATE TABLE Vouchers (
    id INT IDENTITY(1,1) PRIMARY KEY,
    date DATE,
	entryDate DATE,
    amount DECIMAL(18,2),
	type NVARCHAR(MAX),
	description NVARCHAR(MAX),
    cashierID INT,
    accountId INT,
     currencyId int,
    employeeId INT,
    -- (إيراد، مصروف)
	constraint FK_ChartOfAccounts_Voucher foreign key (accountId)
    references ChartOfAccounts (id),
    constraint FK_Cashier_Voucher foreign key (cashierID)
    references Cashiers (id), 
       constraint FK_Currencie_Voucher foreign key (currencyId)  references Currencies (id),

	    CONSTRAINT FK_empoleey_Voucher FOREIGN KEY (employeeId) REFERENCES Employees(id)
);

alter table Vouchers
add  cashierID int
alter table Vouchers
add CONSTRAINT  FK_Cashier_Voucher foreign key (cashierID)
    references Cashiers (id);
-- جدول أسعار الصرف التاريخية
--CREATE TABLE ExchangeRatesHistory (
   -- id INT IDENTITY(1,1) PRIMARY KEY,
   -- CurrencyID INT,
    --ExchangeDate DATE,
    --ExchangeRate DECIMAL(18,6),

	--constraint FK_Currency_ExchangeRatesHistory foreign key(CurrencyID) references Currencies(id)

--);
--drop table Operations;


--جدول بيغ وشراءالعمل
create table  Trades (
id int IDENTITY(1,1) primary key  ,
type NVARCHAR(MAX),
description NVARCHAR(MAX),
date text ,
currencyFromId int,
currencyToId int,
conversionPrice  DECIMAL(10,5),
accountId int,
cashierId int ,
employeeId int,
constraint FK_CurrencyFrom_Trade foreign key(currencyFromId) references Currencies(id),
constraint FK_CurrencyTo_Trade foreign key(currencyToId) references Currencies(id),
constraint FK_Account_Trade foreign key(accountId) references ChartOfAccounts(id),
constraint FK_Cashier_Trade foreign key(cashierId) references Cashiers(id),
constraint FK_Employee_Trade foreign key(employeeId) references Employees(id)

);
--جدول  القيود البسيطه  
CREATE TABLE SimpleEntries (
    id INT PRIMARY KEY IDENTITY(1,1),
    entryDate DATE,
	updateDate date,
    description NVARCHAR(MAX),
    debitAccount INT,
    creditAccount INT,
    amount DECIMAL(18,2),
	currencyId int,
	employeeId int,
	constraint FK_Currency_Trade foreign key(currencyId) references Currencies(id),
	constraint FK_DebitAccount_Trade foreign key(debitAccount) references ChartOfAccounts(id),
	constraint FK_CreditAccount_Trade foreign key(creditAccount) references ChartOfAccounts(id),
	   CONSTRAINT FK_empoleey_SimpleEntrie FOREIGN KEY (employeeId) REFERENCES Employees(id),
);
--جدول  القيود المركبة 
CREATE TABLE CompoundEntries(
    id INT PRIMARY KEY IDENTITY(1,1),
	 date DATE,
    entryDate DATE,
	updateDate date,
    description NVARCHAR(255),
	type  NVARCHAR(MAX),
	 debitTotal DECIMAL(18,2),
    creditTotal DECIMAL(18,2),
	currencyId int,
	employeeId int,
	constraint FK_Currency_CompoundEntries foreign key(currencyId) references Currencies(id),
	   CONSTRAINT FK_empoleey_CompoundEntrie FOREIGN KEY (employeeId) REFERENCES Employees(id),
);
alter table CompoundEntries
add type  NVARCHAR(MAX)
--جدول  المستهلكات 
create table Consumption(
id int primary key ,
date date,
classifyId INT,
quantity  DECIMAL(10,4),
storeId int,
saleID INT,
 CONSTRAINT FK_Classify_Consumption FOREIGN KEY (ClassifyId) REFERENCES Classify(id),
 CONSTRAINT FK_Store_Consumption FOREIGN KEY (storeId) REFERENCES Stores(id),
 CONSTRAINT FK_Salse_Consumption FOREIGN KEY (saleID) REFERENCES Sales(id),

)


-- جدول الدليل المحاسبي
create table JournalEntries(
 id int primary key identity (1,1) ,
 transactionId int  ,
 transactionType NVARCHAR(MAX),
 transactionDate date,
 accountId int ,
currencyId int,
ExchangeRate DECIMAL(18,2),
   debit DECIMAL(18,2),
    credit DECIMAL(18,2),
	description NVARCHAR(MAX),	
	constraint FK_Account_JournalEntries foreign key (accountId) references ChartOfAccounts(id),
	--constraint FK_Sale_JournalEntries foreign key (transactionId) references Sales(id),
	--constraint FK_Purchase_JournalEntries foreign key (transactionId) references Purchases(id),
	--constraint FK_Voucher_JournalEntries foreign key (transactionId) references Vouchers(id),
	--constraint FK_Trade_JournalEntries foreign key (transactionId) references Trades(id),
	--constraint FK_SimpleEntries_JournalEntries foreign key (transactionId) references SimpleEntries(id),
	--constraint FK_CompoundEntries_JournalEntries foreign key (transactionId) references CompoundEntries(id),
    constraint FK_Currencie_JournalEntrie foreign key (currencyId)  references Currencies (id)
)


--جدول العمليات الجداول  
--  لتتبع العمليات التي تتم على الجداول
create table Operations(
id int primary key Identity(1,1),
operationName NVARCHAR(MAX),
operationType NVARCHAR(MAX),
employeeId int ,
description NVARCHAR(MAX),
operationNumber int,
date date,
	
);

drop table Operations
-- جداول الصلاحيات

--
CREATE TABLE Permissions(
id int primary key,
employeeId int,
tableName NVARCHAR(MAX),
addPermission BIT,
deletePermission BIT,
updatePermission BIT,
viewPermission BIT,
importFromExcelPermission BIT,
CONSTRAINT FK_empoleey_CurrencyPermissions FOREIGN KEY (employeeId) REFERENCES Employees(id),

);
--alter table Permissions
--alter column viewPermission BIT not null ;
CREATE PROCEDURE addPermission
    @employeeId int,
	@tableName NVARCHAR(MAX),
	@addPermission BIT,
	@deletePermission BIT,
	@updatePermission BIT,
	@viewPermission BIT,
	@importFromExcelPermission BIT
AS
BEGIN
    INSERT INTO Permissions (tableName ,employeeId ,addPermission ,deletePermission ,updatePermission ,viewPermission ,importFromExcelPermission )
    VALUES (@tableName ,@employeeId ,@addPermission ,@deletePermission ,@updatePermission ,@viewPermission ,@importFromExcelPermission);
END


CREATE PROCEDURE getPermissions
AS
BEGIN
    SELECT * FROM Permissions;
END
CREATE PROCEDURE getPermissionRecorde
   @employeeId int,
   @tableName NVARCHAR(MAX)
	
AS
BEGIN
    SELECT * FROM Permissions  WHERE  employeeId=@employeeId AND tableName=@tableName ;
END
CREATE PROCEDURE updatePermission
	@id int,
	@employeeId int,
	@tableName NVARCHAR(MAX),
	@addPermission BIT,
	@deletePermission BIT,
	@updatePermission BIT,
	@viewPermission BIT,
	@importFromExcelPermission BIT
AS
BEGIN
    UPDATE Permissions 
	SET employeeId=@employeeId,tableName=@tableName,addPermission=@addPermission,
	deletePermission=@deletePermission,updatePermission=@updatePermission,viewPermission=@viewPermission,
	importFromExcelPermission=@importFromExcelPermission
	WHERE id=@id;
END

CREATE PROCEDURE deletePermission
    @id INT
AS
BEGIN
    DELETE FROM Permissions WHERE id = @id;
END
CREATE PROCEDURE addOperation
	@operationName NVARCHAR(MAX),
	@operationType NVARCHAR(MAX),
	@employeeId int ,
	@description NVARCHAR(MAX),
	@operationNumber int,
	@date date
AS
BEGIN
    INSERT INTO Operations (operationName,operationType ,employeeId  ,description,operationNumber,date)
    VALUES (@operationName, @operationType,@employeeId, @description,@operationNumber,@date);
END


CREATE PROCEDURE getOperations
AS
BEGIN
    SELECT * FROM Operations;
END
CREATE PROCEDURE getOperationById
    @id INT
AS
BEGIN
    SELECT * FROM Operations  WHERE id = @id;
END
CREATE PROCEDURE updateOperation
	@id int,
	@operationName NVARCHAR(MAX),
	@operationType NVARCHAR(MAX),
	@employeeId int ,
	@description NVARCHAR(MAX),
	@operationNumber int,
	@date date
AS
BEGIN
    UPDATE Operations 
	SET operationName=@operationName,operationType=@operationType,employeeId=@employeeId,
	description=@description,operationNumber=@operationNumber,date=@date
	WHERE id=@id;
END

CREATE PROCEDURE deleteOperation
    @id INT
AS
BEGIN
    DELETE FROM Operations WHERE id = @id;
END


CREATE PROCEDURE addCurrency
  
	@name NVARCHAR(MAX),
    @code NVARCHAR(MAX),
    @exchangeRate DECIMAL(18,6),
	@currencyType NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Currencies (code, name, currencyType,exchangeRate)
    VALUES (@code, @name,@currencyType, @exchangeRate)
	SELECT SCOPE_IDENTITY() AS NewId;
END


CREATE PROCEDURE getCurrencies
AS
BEGIN
 EXEC GetTableData 'Currencies'; 
 end
CREATE PROCEDURE getCurrencyById
    @id INT
AS
BEGIN

    SELECT * FROM Currencies  WHERE id = @id;
END

CREATE PROCEDURE updateCurrency
    @id INT,
    @name NVARCHAR(MAX),
    @code NVARCHAR(MAX),
    @exchangeRate DECIMAL(18,6),
	@currencyType NVARCHAR(MAX)
AS
BEGIN
    UPDATE Currencies
    SET code = @code, name = @name,currencyType=@currencyType, exchangeRate = @exchangeRate
    WHERE id = @id;
END
--drop PROCEDURE deleteCurrency
CREATE PROCEDURE deleteCurrency
    @id INT
AS
BEGIN
    DELETE FROM Currencies WHERE id = @id;
	SELECT @id AS DeletedId;
END

CREATE PROCEDURE GetTableData
    @TableName nvarchar(128)
AS
BEGIN
    DECLARE @SQL nvarchar(max);

    -- بناء الاستعلام الديناميكي
    SET @SQL = 'SELECT * FROM ' + QUOTENAME(@TableName);

    -- تنفيذ الاستعلام الديناميكي
    EXEC sp_executesql @SQL;
END
CREATE PROCEDURE GetRowDataFromTable
    @TableName nvarchar(128),
	@id int
AS
BEGIN
    DECLARE @SQL nvarchar(max);

    -- بناء الاستعلام الديناميكي
    SET @SQL = 'SELECT * FROM ' + QUOTENAME(@TableName)+'WHERE id=@id ';

    -- تنفيذ الاستعلام الديناميكي
  EXEC sp_executesql @SQL, N'@id int', @id;
END

CREATE PROCEDURE searchCurrency
    @value NVARCHAR(MAX),
	@columnName NVARCHAR(MAX)
AS
BEGIN
    SELECT * FROM Currencies WHERE @columnName = @value;
END

--drop PROCEDURE DeleteRowFromTable
CREATE PROCEDURE DeleteRowFromTable
    @TableName nvarchar(128),
    @id int
AS
BEGIN
    DECLARE @SQL nvarchar(max);
    DECLARE @RowsAffected INT;

    -- بناء جملة DELETE الديناميكية
    SET @SQL = 'DELETE FROM ' + QUOTENAME(@TableName) + ' WHERE id = @id';

    BEGIN TRY
        -- تنفيذ الجملة الديناميكية
        EXEC sp_executesql @SQL, N'@id int', @id;

        SET @RowsAffected = @@ROWCOUNT;
        SELECT @RowsAffected AS DeletedRows;
    END TRY
    BEGIN CATCH
        -- التعامل مع الأخطاء
        THROW;
    END CATCH
END

CREATE PROCEDURE sp_GetPagedData
    @TableName sysname,
    @ColumnName nvarchar(128),
    @PageSize int,
    @PageNumber int
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SQL nvarchar(max);

    SET @SQL = '
        WITH Row_Num AS
        (
            SELECT *, ROW_NUMBER() OVER (ORDER BY ' + @ColumnName + ') as RowNum
            FROM ' + QUOTENAME(@TableName) + '
        )
        SELECT * FROM Row_Num
        WHERE RowNum BETWEEN ' + CAST((@PageNumber - 1) * @PageSize + 1 AS nvarchar) + ' AND ' + CAST(@PageNumber * @PageSize AS nvarchar) + ';';

    EXEC sp_executesql @SQL;
END















CREATE PROCEDURE SpGetPagedData
    @TableName sysname,
    @ColumnName nvarchar(128),
    @PageSize int,
    @PageNumber int
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SQL nvarchar(max);

    SET @SQL = '
        WITH Row_Num AS
        (
            SELECT *, ROW_NUMBER() OVER (ORDER BY ' + @ColumnName + ') as RowNum
            FROM ' + QUOTENAME(@TableName) + '
        )
        SELECT * FROM Row_Num
        WHERE RowNum BETWEEN ' + CAST((@PageNumber - 1) * @PageSize + 1 AS nvarchar) + ' AND ' + CAST(@PageNumber * @PageSize AS nvarchar) + ';';

    EXEC sp_executesql @SQL;
END