using COMP2001_JBott.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;

namespace COMP2001_JBott.Controllers
{
    public class HomeController : Controller
    {
        public static bool loggedIn = false;
        public static string userEmail = "";
        public static bool ownsProfile = false;
        public static bool isAdmin = false;

        private readonly HttpClient _httpClient;

        SqlCommand command = new SqlCommand();
        SqlDataReader reader;
        SqlConnection connection = new SqlConnection();

        List<User_Details> users = new List<User_Details>();
        List<Profile_Details> profiles = new List<Profile_Details>();
        List<Activities> activities = new List<Activities>();
        List<profile_activities> profileActivities = new List<profile_activities>();
        ProfileInfo profileInfo = new ProfileInfo();
        EditInfo editInfo = new EditInfo();
        List<String> editActivities = new List<String>();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            connection.ConnectionString = COMP2001_JBott.Properties.Resources.ConnectionString;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login()
        {
            string email = Request.Form["Email"];
            userEmail = Request.Form["Email"];
            string password = Request.Form["Password"];

            using (HttpClient httpClient = new HttpClient())
            {
                bool userExists = await CheckUser(httpClient, email, password);
                TempData["Response"] = userExists ? "User exists" : "User does not exist";
            }

            if(loggedIn)
            {
                CheckAdmin();
                return View("Home");
            }
            return View("Index");
        }

        private async Task<bool> CheckUser(HttpClient httpClient, string email, string password)
        {
            var apiUrl = "https://web.socem.plymouth.ac.uk/COMP2001/auth/api/users";
            var request = new { Email = email, Password = password };
            var response = await httpClient.PostAsJsonAsync(apiUrl, request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonArray = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(responseContent);

                if(jsonArray != null && jsonArray.Length == 2 && jsonArray[0] == "Verified" && jsonArray[1] == "True")
                {
                    loggedIn = true;
                    return true;
                }
            }
            loggedIn = false;
            return false;
        }

        private void CheckAdmin()
        {
            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT [Admin] FROM [COMP2001_JBott].[CW2].[User_Details] WHERE [Email] = '" + userEmail + "'";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["Admin"].ToString() == "True")
                    {
                        isAdmin = true;
                    }
                    else
                    {
                        isAdmin = false;
                    }
                }
                connection.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult YourProfiles()
        {
            FetchYourProfiles();
            return View(profiles);
        }

        private void FetchYourProfiles()
        {
            if (profiles.Count > 0)
            {
                profiles.Clear();
            }
            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT [ProfileID],[Email],[FirstName],[LastName] FROM [COMP2001_JBott].[CW2].[Profile_Details] WHERE [Archived] = 'False'";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["Email"].ToString() == userEmail)
                    {
                        profiles.Add(new Profile_Details()
                        {ProfileID = reader["ProfileId"].ToString()
                        ,FirstName = reader["FirstName"].ToString()
                        ,LastName = reader["LastName"].ToString()
                        });
                    }
                }
                connection.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public IActionResult AllProfiles()
        {
            FetchAllProfiles();
            return View(profiles);
        }

        private void FetchAllProfiles()
        {
            if (profiles.Count > 0)
            {
                profiles.Clear();
            }
            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT [ProfileID],[FirstName],[LastName] FROM [COMP2001_JBott].[CW2].[Profile_Details] WHERE [Archived] = 'False'";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    profiles.Add(new Profile_Details()
                    {ProfileID = reader["ProfileId"].ToString()
                    ,FirstName = reader["FirstName"].ToString()
                    ,LastName = reader["LastName"].ToString()
                    });
                }
                connection.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public IActionResult LogOut()
        {
            loggedIn = false;
            isAdmin = false;
            return View("Index");
        }

        public IActionResult ViewProfile(int ProfileID)
        {
            FetchProfileInfo(ProfileID);
            return View(profileInfo);
        }

        private void FetchProfileInfo(int ProfileID)
        {
            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT [Email] FROM [COMP2001_JBott].[CW2].[Profile_Details] WHERE [ProfileID] = " + ProfileID;
                reader = command.ExecuteReader();
                while(reader.Read())
                {
                    if (reader["Email"].ToString() == userEmail)
                    {
                        ownsProfile = true;
                    }
                    else
                    {
                        ownsProfile = false;
                    }
                }
                connection.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }

            if (ownsProfile)
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT [ProfileID],[Name],[ProfileImageUrl],[AboutMe],[Email],[Location],[Height],[Weight],CONVERT(varchar, [Birthday], 23) as Birthday,[Langauge],[FavouriteActivities] FROM [COMP2001_JBott].[CW2].[ProfileInfo] WHERE [ProfileID] = " + ProfileID;
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        profileInfo = new ProfileInfo()
                        {ProfileID = reader["ProfileID"].ToString()
                        ,Name = reader["Name"].ToString()
                        ,ProfileImageUrl = reader["ProfileImageUrl"].ToString()
                        ,AboutMe = reader["AboutMe"].ToString()
                        ,Email = reader["Email"].ToString()
                        ,Location = reader["Location"].ToString()
                        ,Height = reader["Height"].ToString()
                        ,Weight = reader["Weight"].ToString()
                        ,Birthday = reader["Birthday"].ToString()
                        ,Langauge = reader["Langauge"].ToString()
                        ,FavouriteActivities = reader["FavouriteActivities"].ToString()
                        };
                    }
                    connection.Close();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            else
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT [ProfileID],[Name],[ProfileImageUrl],[AboutMe],[FavouriteActivities] FROM [COMP2001_JBott].[CW2].[ProfileInfo] WHERE [ProfileID] = " + ProfileID;
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        profileInfo = new ProfileInfo()
                        {ProfileID = reader["ProfileID"].ToString()
                        ,Name = reader["Name"].ToString()
                        ,ProfileImageUrl = reader["ProfileImageUrl"].ToString()
                        ,AboutMe = reader["AboutMe"].ToString()
                        ,FavouriteActivities = reader["FavouriteActivities"].ToString()
                        };
                    }
                    connection.Close();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        public IActionResult EditProfile(int ProfileID)
        {
            FetchProfileInfo(ProfileID);
            return View(profileInfo);
        }

        public IActionResult UpdateProfile(int ProfileID)
        {
            editInfo = new EditInfo()
                    {ProfileID = Request.Form["ProfileID"]
                    ,FirstName = Request.Form["FirstName"]
                    ,LastName = Request.Form["LastName"]
                    ,AboutMe = Request.Form["AboutMe"]
                    ,Email = Request.Form["Email"]
                    ,City = Request.Form["City"]
                    ,County = Request.Form["County"]
                    ,Country = Request.Form["Country"]
                    ,Height = Request.Form["Height"]
                    ,Weight = Request.Form["Weight"]
                    ,Birthday = Request.Form["Birthday"]
                    ,Langauge = Request.Form["Langauge"]
                    };

            if (editActivities.Count > 0)
            {
                editActivities.Clear();
            }

            if (Request.Form["FavouriteActivities"].Contains("Running"))
            {
                editActivities.Add("1");
            }
            if (Request.Form["FavouriteActivities"].Contains("Cycling"))
            {
                editActivities.Add("2");
            }
            if (Request.Form["FavouriteActivities"].Contains("Swimming"))
            {
                editActivities.Add("3");
            }
            if (Request.Form["FavouriteActivities"].Contains("Hiking"))
            {
                editActivities.Add("4");
            }
            if (Request.Form["FavouriteActivities"].Contains("Walking"))
            {
                editActivities.Add("5");
            }

            UpdateProfileInfo();
            UpdateActivities();
            FetchProfileInfo(ProfileID);
            return View("ViewProfile", profileInfo);
        }
        private void UpdateProfileInfo()
        {
            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE [COMP2001_JBott].[CW2].[Profile_Details] " +
                    "SET " +
                    "[FirstName] = '" + editInfo.FirstName + "'," +
                    "[LastName] = '" + editInfo.LastName + "'," +
                    "[AboutMe] = '" + editInfo.AboutMe + "'," +
                    "[Email] = '" + editInfo.Email + "'," +
                    "[City] = '" + editInfo.City + "'," +
                    "[County] = '" + editInfo.County + "'," +
                    "[Country] = '" + editInfo.Country + "'," +
                    "[Height] = '" + editInfo.Height + "'," +
                    "[Weight] = '" + editInfo.Weight + "'," +
                    "[Birthday] = '" + editInfo.Birthday + "'," +
                    "[Langauge] = '" + editInfo.Langauge + "' " +
                    "WHERE [ProfileID] = " + editInfo.ProfileID;
                reader = command.ExecuteReader();
                connection.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void UpdateActivities()
        {
            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM [COMP2001_JBott].[CW2].[profile_activities] WHERE [ProfileID] = " + editInfo.ProfileID;
                reader = command.ExecuteReader();
                connection.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }

            foreach (var activity in editActivities)
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO [COMP2001_JBott].[CW2].[profile_activities] (ActivityID, ProfileID) VALUES (" + activity + "," + editInfo.ProfileID +")";
                    reader = command.ExecuteReader();
                    connection.Close();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        public IActionResult DeleteProfile(int ProfileID)
        {
            DeleteProfileInfo(ProfileID);
            FetchYourProfiles();
            return View("YourProfiles", profiles);
        }

        private void DeleteProfileInfo(int ProfileID)
        {
            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE [COMP2001_JBott].[CW2].[Profile_Details] SET [Archived] = 'True' WHERE [ProfileID] = " + ProfileID;
                int rowsEffected = command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public IActionResult CreateProfile()
        {
            return View();
        }

        public IActionResult CreateNewProfile()
        {
            string NextProfileID = GetNextProfileID();

            editInfo = new EditInfo()
            {ProfileID = NextProfileID
            ,FirstName = Request.Form["FirstName"]
            ,LastName = Request.Form["LastName"]
            ,AboutMe = Request.Form["AboutMe"]
            ,Email = Request.Form["Email"]
            ,City = Request.Form["City"]
            ,County = Request.Form["County"]
            ,Country = Request.Form["Country"]
            ,Height = Request.Form["Height"]
            ,Weight = Request.Form["Weight"]
            ,Birthday = Request.Form["Birthday"]
            ,Langauge = Request.Form["Langauge"]
            };

            if (editActivities.Count > 0)
            {
                editActivities.Clear();
            }

            if (Request.Form["FavouriteActivities"].Contains("Running"))
            {
                editActivities.Add("1");
            }
            if (Request.Form["FavouriteActivities"].Contains("Cycling"))
            {
                editActivities.Add("2");
            }
            if (Request.Form["FavouriteActivities"].Contains("Swimming"))
            {
                editActivities.Add("3");
            }
            if (Request.Form["FavouriteActivities"].Contains("Hiking"))
            {
                editActivities.Add("4");
            }
            if (Request.Form["FavouriteActivities"].Contains("Walking"))
            {
                editActivities.Add("5");
            }

            CreateProfileInfo();
            CreateActivities();
            int ProfileID = int.Parse(NextProfileID);
            FetchProfileInfo(ProfileID);
            return View("ViewProfile", profileInfo);
        }
        private void CreateProfileInfo()
        {
            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO [COMP2001_JBott].[CW2].[Profile_Details] (ProfileID, Email, FirstName, LastName, City, County, Country, ProfileImageUrl, Height, Weight, Birthday, AboutMe, Langauge, Archived)" +
                    "VALUES (" + editInfo.ProfileID + ", '" + userEmail + "', '" + editInfo.FirstName + "', '" + editInfo.LastName + "', '" + editInfo.City + "', '" + editInfo.County + "', '" + editInfo.Country + "', 'ProfileImageUrl', " + editInfo.Height + ", " + editInfo.Weight + ", '" + editInfo.Birthday + "', '" + editInfo.AboutMe + "', '" + editInfo.Langauge + "', 0)";
                reader = command.ExecuteReader();
                connection.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void CreateActivities()
        {
            foreach (var activity in editActivities)
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO [COMP2001_JBott].[CW2].[profile_activities] (ActivityID, ProfileID) VALUES (" + activity + "," + editInfo.ProfileID + ")";
                    reader = command.ExecuteReader();
                    connection.Close();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        private string GetNextProfileID()
        {
            List<String> existingID = new List<String>();
            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT [ProfileID] FROM [COMP2001_JBott].[CW2].[ProfileInfo]";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    existingID.Add(reader["ProfileId"].ToString());
                }
                connection.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }

            int nextID = 1;
            foreach (var id in existingID)
            {
                if(nextID.ToString() == id)
                {
                    nextID++;
                }
            }
            return nextID.ToString();
        }


        //
        //
        // Debugging Methods
        // Accesses views that display tables in their entirity
        //
        //

        public IActionResult User_Details()
        {
            FetchUsers();
            return View(users);
        }
        private void FetchUsers()
        {
            if(users.Count > 0)
            {
                users.Clear();
            }
            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT TOP (1000) [Email],[Username],[Password],[Admin] FROM [COMP2001_JBott].[CW2].[User_Details]";
                reader = command.ExecuteReader();
                while(reader.Read())
                {
                    users.Add(new User_Details() 
                    {Email = reader["Email"].ToString() 
                    ,Username = reader["Username"].ToString()
                    ,Password = reader["Password"].ToString()
                    ,Admin = reader["Admin"].ToString()
                    });
                }
                connection.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public IActionResult Profile_Details()
        {
            FetchProfiles();
            return View(profiles);
        }

        private void FetchProfiles()
        {
            if (profiles.Count > 0)
            {
                profiles.Clear();
            }
            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT TOP (1000) [ProfileID],[Email],[FirstName],[LastName],[City],[County],[Country],[ProfileImageUrl],[Height],[Weight],[Birthday],[AboutMe],[Langauge],[Archived] FROM [COMP2001_JBott].[CW2].[Profile_Details]";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    profiles.Add(new Profile_Details()
                    {ProfileID = reader["ProfileId"].ToString()
                    ,Email = reader["Email"].ToString()
                    ,FirstName = reader["FirstName"].ToString()
                    ,LastName = reader["LastName"].ToString()
                    ,City = reader["City"].ToString()
                    ,County = reader["County"].ToString()
                    ,Country = reader["Country"].ToString()
                    ,ProfileImageUrl = reader["ProfileImageUrl"].ToString()
                    ,Height = reader["Height"].ToString()
                    ,Weight = reader["Weight"].ToString()
                    ,Birthday = reader["Birthday"].ToString()
                    ,AboutMe = reader["AboutMe"].ToString()
                    ,Langauge = reader["Langauge"].ToString()
                    ,Archived = reader["Archived"].ToString()
                    });
                }
                connection.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public IActionResult Activities()
        {
            FetchActivities();
            return View(activities);
        }

        private void FetchActivities()
        {
            if (activities.Count > 0)
            {
                activities.Clear();
            }
            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT TOP (1000) [ActivityID],[ActivityName] FROM [COMP2001_JBott].[CW2].[Activities]";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    activities.Add(new Activities()
                    {ActivityID = reader["ActivityID"].ToString()
                    ,ActivityName = reader["ActivityName"].ToString()
                    });
                }
                connection.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public IActionResult profile_activities()
        {
            Fetchprofile_activities();
            return View(profileActivities);
        }

        private void Fetchprofile_activities()
        {
            if (profileActivities.Count > 0)
            {
                profileActivities.Clear();
            }
            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT TOP (1000) [ActivityID],[ProfileID] FROM [COMP2001_JBott].[CW2].[profile_activities]";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    profileActivities.Add(new profile_activities()
                    {ActivityID = reader["ActivityID"].ToString()
                    ,ProfileID = reader["ProfileID"].ToString()
                    });
                }
                connection.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}