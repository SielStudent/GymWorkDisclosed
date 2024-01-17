using BusinessLogic.Classes;
using BusinessLogic.Services.Workout;
using Microsoft.AspNetCore.Mvc;
using GymWorkDisclosed.DTOs;
using Microsoft.AspNetCore.Authorization;


namespace GymWorkDisclosed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkoutController : ControllerBase
    {
        private readonly WorkoutService _workoutService;
        
        public WorkoutController(WorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        // GET: api/Workout/5
        [HttpGet("{id:guid}", Name = "GetPersonalBestWorkoutsByGymGoerId")]
        // [Authorize]
        public IActionResult PersonalBestWorkoutsByGymGoerIdPerExercise(Guid id)
        {
            List<Exercise> exercises = _workoutService.GetPersonalBestWorkoutsPerExerciseByGymGoerId(id);
            PersonalBestWorkoutsDTO personalBestWorkoutsDTO = new PersonalBestWorkoutsDTO();
            foreach (Exercise exercise in exercises)
            {
                Workout maxweight = new Workout();
                Workout maxtime = new Workout();
                Workout maxreps = new Workout();
                PersonalBestExerciseDTO personalBestExerciseDTO = new PersonalBestExerciseDTO(exercise.Id, exercise.Name);
                foreach (Workout workout in exercise.Workouts)
                {
                    if (workout.Time > maxtime.Time)
                    {
                        maxtime = workout;
                    }
                    if (maxweight.Sets.Count != 0 )
                    {
                        if (workout.Sets.Select(s => s.Weight).Max() > maxweight.Sets.Select(s => s.Weight).Max())
                        {
                            maxweight = workout;
                        }
                    }
                    else
                    {
                        maxweight = workout;
                    }

                    maxreps = exercise.Workouts.OrderByDescending(w => w.Sets.Sum(s => s.Reps)).First();
                }

                PbWorkoutDTO mostTime = new PbWorkoutDTO(maxweight.Id, maxweight.Time, maxweight.Date);

                PbWorkoutDTO mostWeight = new PbWorkoutDTO(maxtime.Id, maxtime.Time, maxtime.Date);
                mostWeight.MaxWeight = maxweight.Sets.Select(s => s.Weight).Max();
                PbWorkoutDTO mostReps = new PbWorkoutDTO(maxreps.Id, maxreps.Time, maxreps.Date);
                mostReps.TotalReps = maxreps.Sets.Select(s => s.Reps).Sum();
                personalBestExerciseDTO.BestRepsWorkout = mostReps;
                personalBestExerciseDTO.BestTimeWorkout = mostTime;
                personalBestExerciseDTO.BestWeightWorkout = mostWeight;
                personalBestWorkoutsDTO.Exercises.Add(personalBestExerciseDTO);
            }

            return Ok(personalBestWorkoutsDTO);
        }
        
        // POST: api/Workout
        [HttpPost]
        public IActionResult Post([FromBody] AddWorkoutDTO workoutDTO)
        {
            try
            {
                Workout workout = new Workout(DateOnly.FromDateTime(DateTime.Now), workoutDTO.TimeInSeconds, new GymGoer(workoutDTO.GymGoerId), new Exercise(workoutDTO.ExerciseId));
                foreach (SetDTO setDto in workoutDTO.Sets)
                {
                    workout.Sets.Add(new Set(setDto.Guid, setDto.Reps, setDto.Weight, setDto.TimeInSeconds));
                }
                Workout returnWorkout = _workoutService.AddWorkout(workout, workoutDTO.GymGoerId);
                WorkoutDTO returnWorkoutDTO = new WorkoutDTO(returnWorkout.Id, returnWorkout.Time, returnWorkout.Date);
                foreach (Set set in returnWorkout.Sets)
                {
                    SetDTO setDTO = new SetDTO(set.Id, set.Reps, set.Weight, set.Time);
                    returnWorkoutDTO.Sets.Add(setDTO);
                }
                returnWorkoutDTO.Exercise = new ExerciseDTO(returnWorkout.Exercise.Id, returnWorkout.Exercise.Name);
                return Ok(returnWorkoutDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
