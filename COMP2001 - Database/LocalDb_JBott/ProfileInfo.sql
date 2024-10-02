CREATE VIEW dbo.ProfileInfo 
AS 
SELECT 
pd.ProfileID, 
pd.Email,
CONCAT(pd.FirstName, ' ', pd.LastName) AS Name,
CONCAT(pd.City, ',', pd.County, ',', pd.Country) AS Location,
pd.ProfileImageUrl,
pd.Height,
pd.Weight,
pd.Birthday,
pd.AboutMe,
pd.Langauge,
pd.Archived,
STRING_AGG(act.ActivityName, ', ') AS FavouriteActivities 
FROM 
dbo.Profile_Details pd 
LEFT JOIN 
dbo.profile_activities pa ON pd.ProfileID = pa.ProfileID 
LEFT JOIN 
dbo.Activities act ON pa.ActivityID = act.ActivityID 
GROUP BY 
pd.ProfileID, pd.Email, pd.FirstName, pd.LastName, pd.City, pd.County, pd.Country, pd.ProfileImageUrl, pd.Height, pd.Weight, pd.Birthday, pd.AboutMe, pd.Langauge, pd.Archived; 