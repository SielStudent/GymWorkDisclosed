using BusinessLogic.Classes;

namespace BusinessLogic.Services.Workout;

public class WorkoutService
{
    private List<Exercise> _personalBestWorkoutsPerExercise;
    private Classes.Workout _currentBestTimeWorkout;
    private Classes.Workout _currentBestWeightWorkout;
    private Classes.Workout _currentBestRepsWorkout;
    public readonly IWorkoutRepository _workoutRepository;
    public WorkoutService(IWorkoutRepository workoutRepository)
    {
        _workoutRepository = workoutRepository;
        _personalBestWorkoutsPerExercise = new List<Exercise>();
        _currentBestTimeWorkout = new Classes.Workout();
        _currentBestWeightWorkout = new Classes.Workout();
        _currentBestRepsWorkout = new Classes.Workout();
    }
    
    public Classes.Workout AddWorkout(Classes.Workout workout, Guid gymGoerId)
    {
        return _workoutRepository.AddWorkout(workout, gymGoerId);
    }
    
    public List<Exercise> GetPersonalBestWorkoutsPerExerciseByGymGoerId(Guid gymGoerId)
    {
        List<Classes.Workout> workouts = _workoutRepository.GetWorkoutsByGymGoerId(gymGoerId);
        
        foreach (Exercise exercise in workouts.Select(w => w.Exercise))
        {
            if(!_personalBestWorkoutsPerExercise.Exists(e => e.Id == exercise.Id))
            {
                _currentBestWeightWorkout = new Classes.Workout();
                _currentBestRepsWorkout = new Classes.Workout();
                _currentBestTimeWorkout = new Classes.Workout();
                CheckEveryWorkoutPerExerciseForPersonalBest(workouts.Where(w => w.Exercise.Id == exercise.Id).ToList());
                SetPersonalBestRepsWorkout(workouts.Where(w => w.Exercise.Id == exercise.Id).ToList());
                exercise.Workouts.Add(_currentBestWeightWorkout);
                exercise.Workouts.Add(_currentBestRepsWorkout);
                exercise.Workouts.Add(_currentBestTimeWorkout);
                _personalBestWorkoutsPerExercise.Add(exercise);
            }
        }
       
        return _personalBestWorkoutsPerExercise;
    }
    private void CheckEveryWorkoutPerExerciseForPersonalBest(List<Classes.Workout> workouts)
    {
        foreach (Classes.Workout workout in workouts)
        {
            if (_currentBestWeightWorkout.Sets.Count == 0)
            {
                _currentBestWeightWorkout = workout;
            }
            else
            {
                SetPersonalBestWeightWorkoutIfBetterThanPreviousBest(workout);
            }
            SetPersonalBestRepsWorkout(workouts);
            SetPersonalBestTimeWorkoutIfBetterThanPreviousBest(workout);
        }
    }
    private void SetPersonalBestTimeWorkoutIfBetterThanPreviousBest(Classes.Workout workout)
    {
        if (workout.Time > _currentBestTimeWorkout.Time)
        {
            _currentBestTimeWorkout = workout;
        }
    }
    private void SetPersonalBestWeightWorkoutIfBetterThanPreviousBest(Classes.Workout workout)
    {
        foreach (Set set in workout.Sets)
        {
            if (set.Weight > _currentBestWeightWorkout.Sets.Select(s => s.Weight).Max())
            {
                _currentBestWeightWorkout = workout;
            }
        }
    }
    private void SetPersonalBestRepsWorkout(List<Classes.Workout> workouts)
    {
        _currentBestRepsWorkout = workouts
            .OrderByDescending(w => w.Sets
                .Sum(s => s.Reps))
            .First();
    }
}