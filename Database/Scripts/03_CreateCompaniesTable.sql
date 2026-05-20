USE InterviewCopilotAI;
GO

CREATE TABLE Companies
(
    CompanyId INT IDENTITY(1,1) NOT NULL,
    UserId INT NOT NULL,
    CompanyName NVARCHAR(150) NOT NULL,
    Website NVARCHAR(250) NULL,
    Industry NVARCHAR(100) NULL,
    Location NVARCHAR(150) NULL,
    Notes NVARCHAR(1000) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),

    CONSTRAINT PK_Companies PRIMARY KEY (CompanyId),

    CONSTRAINT FK_Companies_Users
        FOREIGN KEY (UserId)
        REFERENCES Users(UserId)
);
GO

INSERT INTO Companies
(
    UserId,
    CompanyName,
    Website,
    Industry,
    Location,
    Notes
)
VALUES
(1, 'Microsoft', 'https://www.microsoft.com', 'Technology', 'Hyderabad / Bengaluru / Remote', 'Target company for .NET and Azure roles'),
(1, 'Amazon', 'https://www.amazon.com', 'Technology / E-commerce / Cloud', 'Bengaluru / Hyderabad / Chennai', 'Possible SDE role preparation'),
(1, 'Infosys', 'https://www.infosys.com', 'IT Services', 'Kolkata / Bengaluru / Pune', 'Relevant for Senior .NET Developer roles');
GO