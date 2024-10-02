MERGE INTO User_Details AS Target 
USING (VALUES 
        ('grace@plymouth.ac.uk', 'Grace Hopper', 'ISAD123!', 1), 
        ('tim@plymouth.ac.uk', 'Tim Berners-Lee', 'COMP2001!', 0), 
        ('ada@plymouth.ac.uk', 'Ada Lovelace', 'insecurePassword', 0)
) 
AS Source (Email, Username, Password, Admin) 
ON Target.Email = Source.Email 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Email, Username, Password, Admin) 
VALUES (Email, Username, Password, Admin);

MERGE INTO Profile_Details AS Target 
USING (VALUES 
        ('1', 'grace@plymouth.ac.uk', 'Grace', 'Hopper', 'Plymouth', 'Devon', 'England', 'https://gravatar.com/avatar/95149cf2136ea08a552eedce22faa8e0?s=400&d=robohash&r=x', '154', '67', '1992-04-12', 'I am Grace Hopper', 'English', '0'), 
        ('2', 'tim@plymouth.ac.uk', 'Tim', 'Berners-Lee', 'Plymouth', 'Devon', 'England', 'https://robohash.org/33b4c74d8b1f0ebd680c71b69eda733d?set=set4&bgset=&size=400x400', '178', '72', '1995-08-22', 'I am Tim Berners-Lee', 'English', '0'), 
        ('3', 'ada@plymouth.ac.uk', 'Ada', 'Lovelace', 'Plymouth', 'Devon', 'England', 'https://robohash.org/aecf9d98342d51e63400847917714eb7?set=set4&bgset=&size=400x400', '168', '71', '1999-01-04', 'I am Ada Lovelace', 'English', '0')
) 
AS Source (ProfileID, Email, FirstName, LastName, City, County, Country, ProfileImageUrl, Height, Weight, Birthday, AboutMe, Langauge, Archived) 
ON Target.ProfileID = Source.ProfileID 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (ProfileID, Email, FirstName, LastName, City, County, Country, ProfileImageUrl, Height, Weight, Birthday, AboutMe, Langauge, Archived) 
VALUES (ProfileID, Email, FirstName, LastName, City, County, Country, ProfileImageUrl, Height, Weight, Birthday, AboutMe, Langauge, Archived);

MERGE INTO Activities AS Target
USING (VALUES 
        ('1', 'Running'), 
        ('2', 'Cycling'), 
        ('3', 'Swimming'),
        ('4', 'Hiking'),
        ('5', 'Walking')
) 
AS Source (ActivityID, ActivityName) 
ON Target.ActivityID = Source.ActivityID 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (ActivityID, ActivityName) 
VALUES (ActivityID, ActivityName);

MERGE INTO profile_activities AS Target
USING (VALUES 
        ('1', '1'), 
        ('2', '1'), 
        ('2', '2'),
        ('3', '2'),
        ('4', '3'),
        ('5', '3')
) 
AS Source (ActivityID, ProfileID)
ON Target.ActivityID = Source.ActivityID AND Target.ProfileID = Source.ProfileID
WHEN NOT MATCHED BY TARGET THEN
INSERT (ActivityID, ProfileID) 
VALUES (ActivityID, ProfileID);