CREATE TABLE dbo.Profile_Details
(
    ProfileID INT NOT NULL,
    Email VARCHAR(255) NOT NULL,
    FirstName VARCHAR(255) NOT NULL,
    LastName VARCHAR(255) NOT NULL,
    City VARCHAR(255) NOT NULL,
    County VARCHAR(255) NOT NULL,
    Country VARCHAR(255) NOT NULL,
    ProfileImageUrl VARCHAR(255) NOT NULL,
    Height INT NOT NULL,
    Weight INT NOT NULL,
    Birthday DATE NOT NULL,
    AboutMe VARCHAR(255) NOT NULL,
    Langauge VARCHAR(255) NOT NULL,
    Archived BIT NOT NULL,

    CONSTRAINT pk_Profile_Details PRIMARY KEY (ProfileID),
    CONSTRAINT fk_Profile_Details FOREIGN KEY (Email) 
    REFERENCES dbo.User_Details(Email)
);