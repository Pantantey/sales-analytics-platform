USE SalesAnalyticsDW;

CREATE TABLE DimCustomer (
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerName NVARCHAR(100),
    City NVARCHAR(100),
    Country NVARCHAR(100)
);

CREATE TABLE DimProduct (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(100),
    Category NVARCHAR(100),
    Price DECIMAL(10,2)
);

CREATE TABLE DimDate (
    DateId INT IDENTITY(1,1) PRIMARY KEY,
    Date DATE,
    Year INT,
    Month INT,
    Day INT
);

CREATE TABLE FactSales (
    SaleId INT IDENTITY(1,1) PRIMARY KEY,
    ProductId INT,
    CustomerId INT,
    DateId INT,
    Quantity INT,
    TotalAmount DECIMAL(10,2),

    FOREIGN KEY (ProductId) REFERENCES DimProduct(ProductId),
    FOREIGN KEY (CustomerId) REFERENCES DimCustomer(CustomerId),
    FOREIGN KEY (DateId) REFERENCES DimDate(DateId)
);
