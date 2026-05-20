USE InterviewCopilotAI;
GO

CREATE TABLE InterviewQuestions
(
    InterviewQuestionId INT IDENTITY(1,1) NOT NULL,
    InterviewId INT NOT NULL,
    QuestionText NVARCHAR(2000) NOT NULL,
    Technology NVARCHAR(100) NULL,
    DifficultyLevel NVARCHAR(50) NULL,
    QuestionType NVARCHAR(100) NULL,
    IsAnswered BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),

    CONSTRAINT PK_InterviewQuestions PRIMARY KEY (InterviewQuestionId),

    CONSTRAINT FK_InterviewQuestions_Interviews
        FOREIGN KEY (InterviewId)
        REFERENCES Interviews(InterviewId)
);
GO

INSERT INTO InterviewQuestions
(
    InterviewId,
    QuestionText,
    Technology,
    DifficultyLevel,
    QuestionType,
    IsAnswered
)
VALUES
(
    1,
    'What is JWT authentication in ASP.NET Core?',
    'ASP.NET Core',
    'Medium',
    'Backend/API Security',
    0
),
(
    1,
    'Explain dependency injection in ASP.NET Core.',
    'ASP.NET Core',
    'Easy',
    'Backend Fundamentals',
    0
),
(
    2,
    'How would you design a scalable interview preparation platform?',
    'System Design',
    'Hard',
    'High Level Design',
    0
),
(
    3,
    'Solve the Two Sum problem and explain its time complexity.',
    'DSA',
    'Easy',
    'Coding Problem',
    0
);
GO