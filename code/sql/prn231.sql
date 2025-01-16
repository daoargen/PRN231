-- Tạo database
CREATE DATABASE NET1719_PRN231_PRJ_G3_SchoolPsychologicalHealthSupportSystem;
GO

-- Sử dụng database vừa tạo
USE NET1719_PRN231_PRJ_G3_SchoolPsychologicalHealthSupportSystem;
GO

-- Tạo bảng Report
CREATE TABLE Report (
    id UNIQUEIDENTIFIER PRIMARY KEY,
    type NVARCHAR(50) NOT NULL, -- Thay ENUM bằng NVARCHAR(50)
    tittle NVARCHAR(255) NOT NULL,
    description NVARCHAR(MAX),
    totalScore FLOAT,
    status NVARCHAR(50), -- Thay ENUM bằng NVARCHAR(50)
    startDate DATE,
    endDate DATE,
    note NVARCHAR(MAX),
    isDeleted BIT DEFAULT 0,
    createdAt DATETIME DEFAULT GETDATE(),
    updateAt DATETIME DEFAULT GETDATE()
);

-- Tạo bảng Dashboard
CREATE TABLE Dashboard (
    id UNIQUEIDENTIFIER PRIMARY KEY,
    reportId UNIQUEIDENTIFIER NOT NULL,
    metricName NVARCHAR(255) NOT NULL,
    metricValue NVARCHAR(255),
    metricCategory FLOAT,
    isDeleted BIT DEFAULT 0,
    createdAt DATETIME DEFAULT GETDATE(),
    updateAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (reportId) REFERENCES Report(id)
);
GO

CREATE TABLE DocumentGroup (
    ID UNIQUEIDENTIFIER PRIMARY KEY,
    CategoryName NVARCHAR(50),
    Description NVARCHAR(100),
    CreatedAt DATETIME2(7), 
    UpdatedAt DATETIME2(7) 
);

CREATE TABLE Document (
    ID NVARCHAR(50) PRIMARY KEY,
    CategoryID UNIQUEIDENTIFIER NOT NULL,
    FileName NVARCHAR(100),
    FileType NVARCHAR(10),
    FileSize INT,
    FilePath NVARCHAR(100),
    IsDeleted BIT,
    CreatedAt DATETIME2(7),
    UpdatedAt DATETIME2(7),
    CONSTRAINT FK_Document_DocumentGroup FOREIGN KEY (CategoryID) REFERENCES DocumentGroup(ID) 
);


-- Tạo bảng Câu hỏi khảo sát
CREATE TABLE SurveyQuestions (
    QuestionID INT PRIMARY KEY IDENTITY,
    QuestionText NVARCHAR(500) NOT NULL,
    QuestionType NVARCHAR(50) NOT NULL,
    Scale NVARCHAR(50) NOT NULL,
    IsOptional BIT NOT NULL DEFAULT 0,
    Tag NVARCHAR(100),
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME,
    CreatedBy NVARCHAR(100)
);

-- Tạo bảng Bài khảo sát
CREATE TABLE Surveys (
    SurveyID INT PRIMARY KEY IDENTITY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    TargetAudience NVARCHAR(50),
    CreatedDate DATETIME DEFAULT GETDATE(),
    StartDate DATETIME,
    EndDate DATETIME,
    CreatedBy NVARCHAR(100),
    IsPublished BIT NOT NULL DEFAULT 0,
    Category NVARCHAR(100),
    IsActive BIT NOT NULL DEFAULT 1
);

-- Tạo bảng trung gian
CREATE TABLE SurveyQuestionMappings (
    MappingID INT PRIMARY KEY IDENTITY,
    SurveyID INT NOT NULL,
    QuestionID INT NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    OrderInSurvey INT,
    CONSTRAINT FK_Survey FOREIGN KEY (SurveyID) REFERENCES Surveys(SurveyID),
    CONSTRAINT FK_Question FOREIGN KEY (QuestionID) REFERENCES SurveyQuestions(QuestionID)
);


CREATE TABLE ProgramType (
    TypeID INT IDENTITY(1,1) PRIMARY KEY,         
    TypeName NVARCHAR(255) NOT NULL,             
    Description NVARCHAR(MAX)                   
);



CREATE TABLE Program (
    ID INT IDENTITY(1,1) PRIMARY KEY,           
    ProgramName VARCHAR(255),                    
    ProgramCode VARCHAR(50) UNIQUE,              
    StartDate DATE,                               
    EndDate DATE,                                
    IsCompleted BIT,                              
    Objectives TEXT,                             
    Budget DECIMAL(15, 2),                       
    MaxParticipation INT,                         
	TargetGroup nvarchar(20),
	CompletionCriteria nvarchar(255),
    TypeID INT,                                   
    CONSTRAINT FK_ProgramType FOREIGN KEY (TypeID) REFERENCES ProgramType(TypeID)
);


CREATE TABLE AppointmentTypes (
    AppointmentTypeId INT PRIMARY KEY IDENTITY(1,1),   -- ID loại cuộc hẹn
    TypeName NVARCHAR(100) NOT NULL,                  -- Tên loại cuộc hẹn (VD: Tư vấn nhận thức, cảm xúc)
    Description NVARCHAR(MAX)                         -- Mô tả loại cuộc hẹn
);




CREATE TABLE Appointments (
    AppointmentId INT PRIMARY KEY IDENTITY(1,1),       -- ID cuộc hẹn, tự động tăng
    UserId INT NOT NULL,                               -- ID người dùng (học sinh hoặc phụ huynh)
    CounselorId INT NOT NULL,                          -- ID chuyên viên tư vấn
    AppointmentDate DATE NOT NULL,                     -- Ngày cuộc hẹn
    AppointmentTime TIME NOT NULL,                     -- Giờ cuộc hẹn
    Status VARCHAR(50) DEFAULT 'Pending',              -- Trạng thái cuộc hẹn
    Reason NVARCHAR(MAX),                              -- Lý do đặt cuộc hẹn
    Notes NVARCHAR(MAX),                               -- Ghi chú của chuyên viên tư vấn
    AppointmentTypeId INT NOT NULL,                    -- ID loại cuộc hẹn (khóa ngoại)
    Location NVARCHAR(255),                            -- Địa điểm diễn ra cuộc hẹn
    PriorityLevel TINYINT DEFAULT 0,                   -- Mức độ ưu tiên của cuộc hẹn (0: Thấp, 1: Trung bình, 2: Cao)
    FOREIGN KEY (AppointmentTypeId) REFERENCES AppointmentTypes(AppointmentTypeId) -- Tham chiếu đến bảng phụ
);


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccount](
	[UserAccountID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[EmployeeCode] [nvarchar](50) NOT NULL,
	[RoleId] [int] NOT NULL,
	[RequestCode] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ApplicationCode] [nvarchar](50) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_UserAccount] PRIMARY KEY CLUSTERED 
(
	[UserAccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[UserAccount] ON 
GO
INSERT [dbo].[UserAccount] ([UserAccountID], [UserName], [Password], [FullName], [Email], [Phone], [EmployeeCode], [RoleId], [RequestCode], [CreatedDate], [ApplicationCode], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'manager', N'@a', N'Accountant', N'Accountant@', N'0913652742', N'000001', 2, NULL, NULL, NULL, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[UserAccount] ([UserAccountID], [UserName], [Password], [FullName], [Email], [Phone], [EmployeeCode], [RoleId], [RequestCode], [CreatedDate], [ApplicationCode], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'staff', N'@a', N'Internal Auditor', N'InternalAuditor@', N'0972224568', N'000002', 3, NULL, NULL, NULL, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[UserAccount] ([UserAccountID], [UserName], [Password], [FullName], [Email], [Phone], [EmployeeCode], [RoleId], [RequestCode], [CreatedDate], [ApplicationCode], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'admin', N'@a', N'Chief Accountant', N'ChiefAccountant@', N'0902927373', N'000003', 1, NULL, NULL, NULL, NULL, NULL, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[UserAccount] OFF
GO

CREATE TABLE NotificationCategory (
    id INT PRIMARY KEY IDENTITY(1,1),
    categoryName NVARCHAR(255) NOT NULL,
    description NVARCHAR(MAX)
);

CREATE TABLE Notification (
    id INT PRIMARY KEY IDENTITY(1,1),
    categoryID INT NOT NULL,
    notificationName NVARCHAR(255) NOT NULL,
    notificationType NVARCHAR(255),
    notificationDetail NVARCHAR(MAX),
    recipientID INT NOT NULL,
    isRead BIT DEFAULT 0,
    isDeleted BIT DEFAULT 0,
    createdAt DATE NOT NULL,
    updateAt DATE,
    CONSTRAINT FK_Notification_Category FOREIGN KEY (categoryID) REFERENCES NotificationCategory (id)
);