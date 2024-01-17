using System.Net.Http.Headers;
using System.Text;
using DAL;
using GymWorkDisclosed.DTOs;
using IntegrationTests.JsonSerialiseObjects.Exercise;
using IntegrationTests.JsonSerialiseObjects.GymGoer;
using IntegrationTests.JsonSerialiseObjects.PersonalTrainer;
using IntegrationTests.JsonSerialiseObjects.Workouts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace IntegrationTests;

[TestClass]
public class WorkoutIntegrationTest
{
    private HttpClient _client;
    private GymWorkoutDisclosedDBContext _context;

    [TestInitialize]
    public void TestInitialize()
    {
        var factory = new GymWorkDisclosedWebAppFactory();
        _client = factory.CreateClient();
        var options = new DbContextOptionsBuilder<GymWorkoutDisclosedDBContext>();
        options.UseMySql("Server=localhost;Uid=root;Database=GymWorkTestDB;Pwd=rootpassword;",
            ServerVersion.AutoDetect("Server=localhost;Uid=root;Database=GymWorkTestDB;Pwd=rootpassword;"));
        GymWorkoutDisclosedDBContext context = new GymWorkoutDisclosedDBContext(options.Options);
        context.Database.EnsureDeleted();
        context.Database.Migrate();
        context.Database.EnsureCreated();
        _context = context;
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
    }

    [TestMethod]
    public async Task ShouldRetrieveGymGoerFromApi()
    {
        //arrange
        _context.Database.EnsureCreated();
        DatabaseSeeder.Initialise(_context);
        DatabaseSeeder.Seed();

        //act
        var response =
            await _client.GetAsync(
                "/api/GymGoer/b25b8dc7-9bf0-4c10-88f9-a4606314d2e5?filterproperty=all&filtervalue=all");
        var jsonString = await response.Content.ReadAsStringAsync();

        //assert
        response.EnsureSuccessStatusCode();

        //act
        GymGoerSerialiseObject gymGoer = JsonConvert.DeserializeObject<GymGoerSerialiseObject>(jsonString);

        // Assert 
        Assert.AreEqual(3, gymGoer.Workouts.Count);
        Assert.AreEqual("Test", gymGoer.Name);
    }

    [TestMethod]
    public async Task ShouldRetrievePersonalBestWorkoutsFromApi()
    {
        //arrange
        string token = await FireBaseAuthenticationUserBuilder.Auth();
        var header = new AuthenticationHeaderValue("Bearer", token);
        _context.Database.EnsureCreated();
        DatabaseSeeder.Initialise(_context);
        DatabaseSeeder.Seed();
        _client.DefaultRequestHeaders.Authorization = header;

        //act
        var response = await _client.GetAsync("/api/Workout/b25b8dc7-9bf0-4c10-88f9-a4606314d2e5");
        var jsonString = await response.Content.ReadAsStringAsync();

        //assert
        response.EnsureSuccessStatusCode();

        //act
        PersonalBestWorkoutsPerExerciseSerialiseObject personalBestWorkoutsPerWorkoutsPerExercise =
            JsonConvert.DeserializeObject<PersonalBestWorkoutsPerExerciseSerialiseObject>(jsonString);
        PersonalBestExcerciseSerialiseObject bicepCurlPersonalBestExcercise = personalBestWorkoutsPerWorkoutsPerExercise
            .Exercises.Where(e => e.Name == "Bicep Curl").FirstOrDefault();
        int bicepCurlTotalReps = bicepCurlPersonalBestExcercise.BestRepsWorkout.TotalReps;
        int tricepMaxWeight = personalBestWorkoutsPerWorkoutsPerExercise.Exercises
            .Where(e => e.Name == "Tricep Extension").FirstOrDefault().BestWeightWorkout.MaxWeight;
        int bicepCurlLongestTime = personalBestWorkoutsPerWorkoutsPerExercise.Exercises
            .Where(e => e.Name == "Bicep Curl").FirstOrDefault().BestTimeWorkout.TimeInSeconds;
        // Assert 
        Assert.AreEqual(100, bicepCurlTotalReps);
        Assert.AreEqual(30, tricepMaxWeight);
        Assert.AreEqual(300, bicepCurlLongestTime);
    }

    [TestMethod]
    public async Task ShouldRetrieveGymGoerIdFromAuthApi()
    {
        //arrange
        string token = await FireBaseAuthenticationUserBuilder.Auth();
        var header = new AuthenticationHeaderValue("Bearer", token);
        _context.Database.EnsureCreated();
        DatabaseSeeder.Initialise(_context);
        DatabaseSeeder.Seed();
        _client.DefaultRequestHeaders.Authorization = header;

        //act
        var response = await _client.GetAsync("/api/Auth/user@example.com");
        var jsonString = await response.Content.ReadAsStringAsync();

        //assert
        response.EnsureSuccessStatusCode();

        //act

        GymGoerSerialiseObject gymGoer = JsonConvert.DeserializeObject<GymGoerSerialiseObject>(jsonString);
        string guid = gymGoer.Guid.ToString();

        // Assert
        Assert.AreEqual("b25b8dc7-9bf0-4c10-88f9-a4606314d2e5", guid);
    }

    [TestMethod]
    public async Task ShouldPostNewWorkout()
    {
        //arrange
        string token = await FireBaseAuthenticationUserBuilder.Auth();
        var header = new AuthenticationHeaderValue("Bearer", token);
        _context.Database.EnsureCreated();
        DatabaseSeeder.Initialise(_context);
        DatabaseSeeder.Seed();
        _client.DefaultRequestHeaders.Authorization = header;

        List<SetDTO> setDtos = new List<SetDTO>()
        {
            new SetDTO(null, 10, 20, 30),
            new SetDTO(null, 10, 20, 30)
        };
        AddWorkoutDTO addWorkoutDTO = new AddWorkoutDTO(700, new Guid("b25b8dc7-9bf0-4c10-88f9-a4606314d2e5"),
            new Guid("84c29521-b68a-475d-b700-51c9b245ff63"), setDtos);
        string json = JsonConvert.SerializeObject(addWorkoutDTO);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        //act
        var response = await _client.PostAsync("/api/Workout", content);
        var jsonString = await response.Content.ReadAsStringAsync();

        //assert
        response.EnsureSuccessStatusCode();

        //act
        WorkoutSerialiseObject workout = JsonConvert.DeserializeObject<WorkoutSerialiseObject>(jsonString);

        //assert
        Assert.AreEqual(700, workout.TimeInSeconds);
        Assert.AreEqual("AddWorkout Extension", workout.Exercise.Name);
        Assert.AreEqual(2, workout.Sets.Count);
    }

    [TestMethod]
    public async Task ShouldGetPersonalTrainer()
    {
        //arrange
        _context.Database.EnsureCreated();
        DatabaseSeeder.Initialise(_context);
        DatabaseSeeder.Seed();

        //act
        var response =
            await _client.GetAsync(
                "/api/PersonalTrainer/c9bf8bd0-fba3-4ee7-b414-925136964129");
        var jsonString = await response.Content.ReadAsStringAsync();

        //assert
        response.EnsureSuccessStatusCode();

        //act
        PersonalTrainerSerialiseObject personalTrainer =
            JsonConvert.DeserializeObject<PersonalTrainerSerialiseObject>(jsonString);

        // Assert 
        Assert.AreEqual("Test", personalTrainer.GymGoers[0].Name);
        Assert.AreEqual(new Guid("b25b8dc7-9bf0-4c10-88f9-a4606314d2e5"), personalTrainer.GymGoers[0].Guid);
    }
}