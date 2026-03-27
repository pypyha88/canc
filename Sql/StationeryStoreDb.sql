CREATE DATABASE StationeryStoreDb;
GO

USE StationeryStoreDb;
GO

CREATE TABLE Users(
    UserId INT IDENTITY PRIMARY KEY,
    FullName NVARCHAR(120) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Phone NVARCHAR(20) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(128) NOT NULL,
    RoleName NVARCHAR(20) NOT NULL CHECK (RoleName IN ('User','Manager','Admin'))
);

CREATE TABLE Products(
    ProductId INT IDENTITY PRIMARY KEY,
    ProductName NVARCHAR(120) NOT NULL,
    Price DECIMAL(18,2) NOT NULL CHECK (Price > 0),
    OldPrice DECIMAL(18,2) NULL,
    DiscountPercent INT NULL CHECK (DiscountPercent BETWEEN 1 AND 99),
    ImagePath NVARCHAR(260) NULL,
    StockQty INT NOT NULL DEFAULT 0 CHECK (StockQty >= 0)
);

CREATE TABLE Orders(
    OrderId INT IDENTITY PRIMARY KEY,
    UserId INT NOT NULL,
    OrderDate DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    TotalAmount DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_Orders_Users FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE OrderItems(
    OrderItemId INT IDENTITY PRIMARY KEY,
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    UnitPrice DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    CONSTRAINT FK_OrderItems_Products FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

-- admin@example.com / Admin123 (SHA256)
INSERT INTO Users(FullName, Email, Phone, PasswordHash, RoleName)
VALUES (N'Администратор', 'admin@example.com', '+79990000000',
        '240BE518FABD2724DDB6F04EEB4E9698A62B267F7E89F02DF822B20D698CC352',
        'Admin');
GO
