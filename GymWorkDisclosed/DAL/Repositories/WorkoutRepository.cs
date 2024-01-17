using BusinessLogic.Classes;
using BusinessLogic.Services.Workout;
using DAL.DBO;
using Microsoft.EntityFrameworkCore;
namespace DAL.Repositories;

public class WorkoutRepository: IWorkoutRepository
{
    public GymWorkoutDisclosedDBContext _context;
    
    public WorkoutRepository(GymWorkoutDisclosedDBContext context)
    {
        _context = context;
    }
    public List<Workout> GetWorkoutsByGymGoerId(Guid id)
    {
        List<WorkoutEntity> workoutEntities = _context.workouts
            .Include(w => w.Sets)
            .Include(w => w.ExerciseEntity)
            .ThenInclude(e => e.MuscleGroupExerciseEntities)
            .ThenInclude(mge => mge.MuscleGroupEntity)
            .ThenInclude(mg => mg.BodyPartEntity)
            .Where(w => w.GymGoerId == id)
            .ToList();
        List<Workout> workouts = new List<Workout>();
        foreach (var workoutEntity in workoutEntities)
        {
            workouts.Add(workoutEntity.ToWorkout(workoutEntity.ExerciseEntity.MuscleGroupExerciseEntities.Select(mge => mge.MuscleGroupEntity).ToList()));
        }
        return workouts;
    }

    public Workout AddWorkout(Workout workout, Guid gymGoerId)
    {
        WorkoutEntity workoutEntity = new WorkoutEntity();
        workoutEntity.ExerciseId = workout.Exercise.Id;
        workoutEntity.Date = DateTime.Now;
        workoutEntity.Time = workout.Time;
        foreach (Set set in workout.Sets)
        {
            SetEntity setEntity = new SetEntity();
            setEntity.Id = Guid.NewGuid();
            setEntity.Reps = set.Reps;
            setEntity.Time = set.Time;
            setEntity.Weight = set.Weight;
            workoutEntity.Sets.Add(setEntity);
        }
        workoutEntity.GymGoerId = gymGoerId;
        _context.workouts.Add(workoutEntity);
        _context.SaveChanges();
        WorkoutEntity returnWorkoutEntity = _context.workouts
            .Include(w => w.Sets)
            .Include(w => w.ExerciseEntity)
            .ThenInclude(e => e.MuscleGroupExerciseEntities)
            .ThenInclude(mge => mge.MuscleGroupEntity)
            .ThenInclude(mg => mg.BodyPartEntity)
            .First(w => w.Id == workoutEntity.Id);
        return returnWorkoutEntity.ToWorkout(workoutEntity.ExerciseEntity.MuscleGroupExerciseEntities.Select(mge => mge.MuscleGroupEntity).ToList());
    }
    
}