CREATE TABLE [dbo].[Users] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Username] NVARCHAR(255) NOT NULL UNIQUE,
    [Password] TEXT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Courses] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Name] TEXT NOT NULL,
    [Pages] INT NOT NULL,
    [Description] TEXT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Questions] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Title] NVARCHAR(255) NOT NULL,
    [Option1] NVARCHAR(255) NOT NULL,
    [Option2] NVARCHAR(255) NOT NULL,
    [Option3] NVARCHAR(255) NOT NULL,
    [CourseId] INT NOT NULL,
    [CorrectOption] TINYINT NOT NULL CHECK ([CorrectOption] IN (1, 2, 3)),
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id])
);

CREATE TABLE [dbo].[Answers] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [QuestionId] INT NOT NULL,
    [SelectedOption] TINYINT NOT NULL CHECK ([SelectedOption] IN (1, 2, 3)),
    [SubmittedAt] DATETIME NOT NULL,
    [UserId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[Questions] ([Id]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

CREATE TABLE [dbo].[CoursePages] (
    [CourseId] INT NOT NULL,
    [Id] INT NOT NULL,
    [Content] TEXT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC, [CourseId] ASC),
    FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id])
);

CREATE TABLE [dbo].[PageActivities] (
    [UserId] INT NOT NULL,
    [CourseId] INT NOT NULL,
    [PageId] INT NOT NULL,
    [Timestamp] DATETIME NOT NULL,
    [SecondsSpent] INT NOT NULL,
    [Id] BIGINT IDENTITY(1,1) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]),
    FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id]),
    FOREIGN KEY ([PageId], [CourseId]) REFERENCES [dbo].[CoursePages] ([Id], [CourseId])
);

CREATE TABLE [dbo].[UserProgresses] (
    [UserId] INT NOT NULL,
    [CourseId] INT NOT NULL,
    [LastPage] INT NOT NULL,
    [LastUpdated] DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC, [CourseId] ASC),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]),
    FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id])
);