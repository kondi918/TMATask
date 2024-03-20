CREATE TABLE Items (
    ItemID INT PRIMARY KEY IDENTITY(1,1),
    ItemGroup VARCHAR(255) NOT NULL,
    UnitOfMeasurement VARCHAR(50) NOT NULL,
    Quantity INT NOT NULL,
    PriceWithoutVAT DECIMAL(10, 2) NOT NULL,
    Status VARCHAR(255) NOT NULL,
    StorageLocation VARCHAR(255),
    ContactPerson VARCHAR(255),
    Photo VARBINARY(MAX)
);

CREATE TABLE TMARequests (
    RequestID INT PRIMARY KEY IDENTITY,
    EmployeeName VARCHAR(255) NOT NULL,
    Comment TEXT,
    Status VARCHAR(255) DEFAULT 'New',
);

CREATE TABLE TMARequestRows (
    RequestRowID INT PRIMARY KEY IDENTITY,
    RequestID INT NOT NULL,
    ItemID INT NOT NULL,
	Quantity INT NOT NULL,
    UnitOfMeasurement VARCHAR(50) NOT NULL,
	PriceWithoutVAT DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (RequestID) REFERENCES TMARequests(RequestID),
    FOREIGN KEY (ItemID) REFERENCES Items(ItemID)
);

CREATE TABLE Users (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) UNIQUE,
    Password NVARCHAR(255),
    Role NVARCHAR(20)
);