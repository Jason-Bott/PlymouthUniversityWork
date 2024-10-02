CREATE TABLE [dbo].[User_Details]
(
    Email VARCHAR(255) NOT NULL,
    Username VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Admin BIT NOT NULL,

    CONSTRAINT pk_User_Details PRIMARY KEY (Email)
)