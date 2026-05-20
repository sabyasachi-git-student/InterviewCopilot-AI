USE InterviewCopilotAI;
GO

CREATE TABLE Users
(
    UserId INT IDENTITY(1,1) NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL,
    PasswordHash NVARCHAR(500) NOT NULL,
    Role NVARCHAR(50) NOT NULL DEFAULT 'User',
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),

    CONSTRAINT PK_Users PRIMARY KEY (UserId),
    CONSTRAINT UQ_Users_Email UNIQUE (Email)
);
GO

INSERT INTO Users (FullName, Email, PasswordHash, Role)
VALUES
('Sabyasachi Gupta', 'sabyasachi@example.com', 'DummyHashedPassword123', 'User');
GO