SET IDENTITY_INSERT [dbo].[Courses] ON;
INSERT [dbo].[Courses] ([Id], [Name], [Pages], [Description]) VALUES (1, N'Introduction to JavaScript', 10, N'Introduction to JavaScript and its importance in web development.');
INSERT [dbo].[Courses] ([Id], [Name], [Pages], [Description]) VALUES (2, N'Basic Concepts of JavaScript', 10, N'Teaching the basic concepts of JavaScript such as variables, data types, functions');
SET IDENTITY_INSERT [dbo].[Courses] OFF;

SET IDENTITY_INSERT [dbo].[Questions] ON;
INSERT [dbo].[Questions] ([Id], [Title], [Option1], [Option2], [Option3], [CourseId], [CorrectOption]) VALUES (1, N'What is JavaScript primarily used for?', N'Creating server-side applications', N'Creating dynamic and interactive content on websites', N'Designing web pages', 1, 2);
INSERT [dbo].[Questions] ([Id], [Title], [Option1], [Option2], [Option3], [CourseId], [CorrectOption]) VALUES (2, N'Who created JavaScript?', N'Brendan Eich', N'Tim Berners-Lee', N'Linus Torvalds', 1, 1);
INSERT [dbo].[Questions] ([Id], [Title], [Option1], [Option2], [Option3], [CourseId], [CorrectOption]) VALUES (3, N'Which of the following is NOT a feature of JavaScript?', N'Client-side execution', N'Manipulating the DOM', N'Compiling to machine code', 1, 3);
INSERT [dbo].[Questions] ([Id], [Title], [Option1], [Option2], [Option3], [CourseId], [CorrectOption]) VALUES (4, N'Which platform allows JavaScript to be used for server-side programming?', N'Node.js', N'Apache', N'IIS', 1, 1);
INSERT [dbo].[Questions] ([Id], [Title], [Option1], [Option2], [Option3], [CourseId], [CorrectOption]) VALUES (5, N'What is the main advantage of JavaScript''s non-blocking event-driven nature?', N'Improved security', N'Efficient handling of asynchronous tasks', N'Better compatibility with older browsers', 1, 2);
INSERT [dbo].[Questions] ([Id], [Title], [Option1], [Option2], [Option3], [CourseId], [CorrectOption]) VALUES (6, N'Which company maintains the Angular framework?', N'Facebook', N'Google', N'Microsoft', 1, 2);
INSERT [dbo].[Questions] ([Id], [Title], [Option1], [Option2], [Option3], [CourseId], [CorrectOption]) VALUES (7, N'Which JavaScript tool is known for its component-based architecture and virtual DOM?', N'Angular', N'Vue.js', N'React', 1, 3);
INSERT [dbo].[Questions] ([Id], [Title], [Option1], [Option2], [Option3], [CourseId], [CorrectOption]) VALUES (8, N'Which method is NOT used to manipulate the DOM?', N'getElementById', N'querySelector', N'fetch', 1, 3);
INSERT [dbo].[Questions] ([Id], [Title], [Option1], [Option2], [Option3], [CourseId], [CorrectOption]) VALUES (9, N'What is a key benefit of using libraries and frameworks like React, Angular, and Vue.js?', N'Simplified access to databases', N'Efficient development of complex applications', N'Automatic generation of CSS', 1, 2);
INSERT [dbo].[Questions] ([Id], [Title], [Option1], [Option2], [Option3], [CourseId], [CorrectOption]) VALUES (10, N'What is the purpose of code linting tools like Prettier?', N'Compiling JavaScript to machine code', N'Enforcing consistent code style', N'Testing JavaScript code', 1, 2);
INSERT [dbo].[Questions] ([Id], [Title], [Option1], [Option2], [Option3], [CourseId], [CorrectOption]) VALUES (11, N'Which keyword is used to declare a block-scoped variable?', N'var', N'let', N'const', 2, 2);
INSERT [dbo].[Questions] ([Id], [Title], [Option1], [Option2], [Option3], [CourseId], [CorrectOption]) VALUES (12, N'What data type is used to represent text in JavaScript?', N'String', N'Number', N'Boolean', 2, 1);
INSERT [dbo].[Questions] ([Id], [Title], [Option1], [Option2], [Option3], [CourseId], [CorrectOption]) VALUES (13, N'How do you declare a function in JavaScript?', N'def functionName()', N'function functionName()', N'create function functionName()', 2, 2);
INSERT [dbo].[Questions] ([Id], [Title], [Option1], [Option2], [Option3], [CourseId], [CorrectOption]) VALUES (14, N'What is the output of the following code: console.log(2 + ''2'');', N'22', N'4', N'NaN', 2, 1);
INSERT [dbo].[Questions] ([Id], [Title], [Option1], [Option2], [Option3], [CourseId], [CorrectOption]) VALUES (15, N'Which statement is used to handle exceptions in JavaScript?', N'catch', N'throw', N'try...catch', 2, 3);
INSERT [dbo].[Questions] ([Id], [Title], [Option1], [Option2], [Option3], [CourseId], [CorrectOption]) VALUES (16, N'How do you create an array in JavaScript?', N'let arr = {}', N'let arr = []', N'let arr = ()', 2, 2);
INSERT [dbo].[Questions] ([Id], [Title], [Option1], [Option2], [Option3], [CourseId], [CorrectOption]) VALUES (17, N'What is hoisting in JavaScript?', N'The process of compiling code', N'The mechanism of moving declarations to the top', N'The method of importing modules', 2, 2);
INSERT [dbo].[Questions] ([Id], [Title], [Option1], [Option2], [Option3], [CourseId], [CorrectOption]) VALUES (18, N'Which method is used to add an element to the end of an array?', N'push()', N'pop()', N'shift()', 2, 1);
INSERT [dbo].[Questions] ([Id], [Title], [Option1], [Option2], [Option3], [CourseId], [CorrectOption]) VALUES (19, N'What does the ''this'' keyword refer to in an arrow function?', N'The global object', N'The object that owns the method', N'The lexical scope', 2, 3);
INSERT [dbo].[Questions] ([Id], [Title], [Option1], [Option2], [Option3], [CourseId], [CorrectOption]) VALUES (20, N'Which statement is used to exit a loop prematurely?', N'break', N'exit', N'stop', 2, 1);
SET IDENTITY_INSERT [dbo].[Questions] OFF;

SET IDENTITY_INSERT [dbo].[Users] ON;
INSERT [dbo].[Users] ([Id], [Username], [Password]) VALUES (1, N'admin', N'AJgIakOvZFeOpaEH+vv+eN9T7KJhSMJ8TAkezdUBVYmZl60ETmUUU42pPJyrtYwpxw==');
INSERT [dbo].[Users] ([Id], [Username], [Password]) VALUES (2, N'user1', N'AJS5ifMnHNa/HJlFfwAYyIh6t5Ut1ASjbrlJPtbeZG1h1cEt+IpTXT5gr02jFoZl9Q==');
INSERT [dbo].[Users] ([Id], [Username], [Password]) VALUES (3, N'user2', N'AANIEyVhdErW23cN/kuH8bkjmPR3RD1ij/htcIBw6Yb0OF2aqfXmbKcRBOFzBigLOA==');
SET IDENTITY_INSERT [dbo].[Users] OFF;