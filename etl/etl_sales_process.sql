USE SalesAnalyticsDW;

----------------------------------------------------
-- 1 Insertar clientes en DimCustomer
----------------------------------------------------
INSERT INTO DimCustomer (CustomerName, City, Country)
SELECT DISTINCT
    CustomerName,
    City,
    Country
FROM RawSales rs
WHERE NOT EXISTS (
    SELECT 1
    FROM DimCustomer dc
    WHERE dc.CustomerName = rs.CustomerName
      AND dc.City = rs.City
      AND dc.Country = rs.Country
);

----------------------------------------------------
-- 2 Insertar productos en DimProduct
----------------------------------------------------
INSERT INTO DimProduct (ProductName, Category, Price)
SELECT DISTINCT
    ProductName,
    Category,
    Price
FROM RawSales rs
WHERE NOT EXISTS (
    SELECT 1
    FROM DimProduct dp
    WHERE dp.ProductName = rs.ProductName
);

----------------------------------------------------
-- 3 Insertar fechas en DimDate
----------------------------------------------------
INSERT INTO DimDate (Date, Year, Month, Day)
SELECT DISTINCT
    SaleDate,
    YEAR(SaleDate),
    MONTH(SaleDate),
    DAY(SaleDate)
FROM RawSales rs
WHERE NOT EXISTS (
    SELECT 1
    FROM DimDate dd
    WHERE dd.Date = rs.SaleDate
);

----------------------------------------------------
-- 4 Insertar ventas en FactSales
----------------------------------------------------
INSERT INTO FactSales (ProductId, CustomerId, DateId, Quantity, TotalAmount)
SELECT
    dp.ProductId,
    dc.CustomerId,
    dd.DateId,
    rs.Quantity,
    rs.TotalAmount
FROM RawSales rs
JOIN DimProduct dp
    ON dp.ProductName = rs.ProductName
JOIN DimCustomer dc
    ON dc.CustomerName = rs.CustomerName
    AND dc.City = rs.City
    AND dc.Country = rs.Country
JOIN DimDate dd
    ON dd.Date = rs.SaleDate;