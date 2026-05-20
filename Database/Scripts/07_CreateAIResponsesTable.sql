USE InterviewCopilotAI;
GO

CREATE TABLE AIResponses
(
    AIResponseId INT IDENTITY(1,1) NOT NULL,
    InterviewQuestionId INT NOT NULL,
    ResponseType NVARCHAR(100) NOT NULL,
    GeneratedAnswer NVARCHAR(MAX) NOT NULL,
    ClassificationTopic NVARCHAR(100) NULL,
    ClassificationSubTopic NVARCHAR(150) NULL,
    DifficultyLevel NVARCHAR(50) NULL,
    PriorityLevel NVARCHAR(50) NULL,
    AIModelUsed NVARCHAR(100) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),

    CONSTRAINT PK_AIResponses PRIMARY KEY (AIResponseId),

    CONSTRAINT FK_AIResponses_InterviewQuestions
        FOREIGN KEY (InterviewQuestionId)
        REFERENCES InterviewQuestions(InterviewQuestionId)
);
GO

INSERT INTO AIResponses
(
    InterviewQuestionId,
    ResponseType,
    GeneratedAnswer,
    ClassificationTopic,
    ClassificationSubTopic,
    DifficultyLevel,
    PriorityLevel,
    AIModelUsed
)
VALUES
(
    1,
    'AnswerGeneration',
    'JWT authentication in ASP.NET Core is a token-based authentication mechanism where the server generates a signed token after successful login. The client sends this token in the Authorization header for every protected API request. The authentication middleware validates the token and creates a ClaimsPrincipal object that is attached to HttpContext.User.',
    'ASP.NET Core',
    'JWT Authentication',
    'Medium',
    'High',
    'Azure OpenAI GPT'
),
(
    2,
    'AnswerGeneration',
    'Dependency Injection in ASP.NET Core is a built-in design pattern that allows objects to receive their dependencies from the framework instead of creating them directly. It improves testability, loose coupling, maintainability, and clean architecture.',
    'ASP.NET Core',
    'Dependency Injection',
    'Easy',
    'High',
    'Azure OpenAI GPT'
),
(
    3,
    'Classification',
    'This is a high-level system design question. The answer should cover requirements, APIs, database design, caching, scaling, authentication, AI integration, and deployment strategy.',
    'System Design',
    'Scalable Architecture',
    'Hard',
    'High',
    'Azure OpenAI GPT'
),
(
    4,
    'AnswerGeneration',
    'The Two Sum problem can be solved efficiently using a hash map. We iterate through the array, calculate the required complement for each number, and check whether the complement already exists in the hash map. This gives O(n) time complexity and O(n) space complexity.',
    'DSA',
    'Hashing',
    'Easy',
    'High',
    'Azure OpenAI GPT'
);
GO