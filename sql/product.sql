CREATE TABLE Products (
      Id INT IDENTITY(1,1) PRIMARY KEY,
      Title NVARCHAR(200) NOT NULL,
      Description NVARCHAR(MAX) NULL,
      CategoryId INT NOT NULL,
      StockQuantity INT NOT NULL,
      IsLive BIT NOT NULL,
    
      CONSTRAINT FK_Products_Categories
          FOREIGN KEY (CategoryId)
          REFERENCES Categories(Id)
          ON DELETE NO ACTION
);