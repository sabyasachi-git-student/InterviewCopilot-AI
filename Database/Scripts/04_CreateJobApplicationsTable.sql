CREATE TABLE JobApplications
(
    JobApplicationId INT IDENTITY(1,1) NOT NULL,
    CompanyId INT NOT NULL,
    JobTitle NVARCHAR(150) NOT NULL,
    JobDescriptionUrl NVARCHAR(500) NULL,
    ApplicationDate DATE NOT NULL,
    ApplicationStatus NVARCHAR(50) NOT NULL DEFAULT 'Applied',
    ExpectedSalary DECIMAL(10,2) NULL,
    Notes NVARCHAR(1000) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),

    CONSTRAINT PK_JobApplications PRIMARY KEY (JobApplicationId),

    CONSTRAINT FK_JobApplications_Companies
        FOREIGN KEY (CompanyId)
        REFERENCES Companies(CompanyId)
);
GO

