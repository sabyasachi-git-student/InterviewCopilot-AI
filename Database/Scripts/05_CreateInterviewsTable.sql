USE InterviewCopilotAI;
GO

CREATE TABLE Interviews
(
    InterviewId INT IDENTITY(1,1) NOT NULL,
    JobApplicationId INT NOT NULL,
    InterviewRound NVARCHAR(100) NOT NULL,
    InterviewType NVARCHAR(100) NOT NULL,
    InterviewDateTime DATETIME2 NOT NULL,
    InterviewerName NVARCHAR(150) NULL,
    MeetingLink NVARCHAR(500) NULL,
    InterviewStatus NVARCHAR(50) NOT NULL DEFAULT 'Scheduled',
    Feedback NVARCHAR(1000) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),

    CONSTRAINT PK_Interviews PRIMARY KEY (InterviewId),

    CONSTRAINT FK_Interviews_JobApplications
        FOREIGN KEY (JobApplicationId)
        REFERENCES JobApplications(JobApplicationId)
);
GO

INSERT INTO Interviews
(
    JobApplicationId,
    InterviewRound,
    InterviewType,
    InterviewDateTime,
    InterviewerName,
    MeetingLink,
    InterviewStatus,
    Feedback
)
VALUES
(
    1,
    'Round 1',
    'Technical',
    '2026-05-25 10:00:00',
    'Technical Panel',
    'https://teams.microsoft.com/sample-meeting',
    'Scheduled',
    'Prepare ASP.NET Core, Web API, JWT, SQL Server'
),
(
    1,
    'Round 2',
    'System Design',
    '2026-05-28 15:00:00',
    'Architecture Panel',
    'https://teams.microsoft.com/system-design-round',
    'Scheduled',
    'Prepare HLD, LLD, caching, database design'
),
(
    2,
    'Round 1',
    'DSA',
    '2026-05-27 11:30:00',
    'Amazon Interviewer',
    'https://chime.aws/sample-meeting',
    'Scheduled',
    'Focus on arrays, hashing, trees, graphs'
);
GO