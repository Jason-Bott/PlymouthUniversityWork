CREATE TABLE dbo.profile_activities
(
    ActivityID INT NOT NULL,
    ProfileID INT NOT NULL,

    CONSTRAINT fk_profile_activities1 FOREIGN KEY (ActivityID) REFERENCES dbo.Activities(ActivityID),
    CONSTRAINT fk_profile_activities2 FOREIGN KEY (ProfileID) REFERENCES dbo.Profile_Details(ProfileID)
);