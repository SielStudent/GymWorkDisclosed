using BusinessLogic.Classes;
using DAL;
using DAL.DBO;

namespace IntegrationTests;

internal static class DatabaseSeeder
{
    private static GymWorkoutDisclosedDBContext _context;
    public static void Initialise(GymWorkoutDisclosedDBContext context)
    {
        _context = context;
    }
    internal static void Seed()
    {
        PersonalTrainerEntity personalTrainerEntity = AddPersonalTrainerEntity();
        BodyPartEntity bodyPartEntity = AddBodyPartEntity();
        List<MuscleGroupEntity> muscleGroupEntities = AddMuscleGroupEntities(bodyPartEntity);
        GymGoerEntity gymGoerEntity = AddGymGoerEntity(personalTrainerEntity);
        List<ExerciseEntity> exerciseEntities = AddExerciseEntities(muscleGroupEntities);
        List<WorkoutEntity> workoutEntities = AddWorkoutEntities(gymGoerEntity, exerciseEntities);
    }

    internal static BodyPartEntity AddBodyPartEntity()
    {
        var bodyPartEntity = new BodyPartEntity
        {
            Id = Guid.NewGuid(),
            Name = "Arm"
        };
        _context.bodyParts.Add(bodyPartEntity);
        _context.SaveChanges();
        return bodyPartEntity;
    }
    internal static GymGoerEntity AddGymGoerEntity(PersonalTrainerEntity personalTrainerEntity)
    {
        var gymGoerEntity = new GymGoerEntity
        {
            Id = new Guid("b25b8dc7-9bf0-4c10-88f9-a4606314d2e5"),
            Name = "Test",
            Email = "user@example.com",
            PersonalTrainerEntityId = personalTrainerEntity.Id
        };
        _context.gymGoer.Add(gymGoerEntity);
        _context.SaveChanges();
        return gymGoerEntity;
    }

    internal static List<MuscleGroupEntity> AddMuscleGroupEntities(BodyPartEntity bodyPartEntity)
    {
        List<MuscleGroupEntity> muscleGroupEntities = new List<MuscleGroupEntity>()
        {
            new MuscleGroupEntity
            {
                Id = Guid.NewGuid(),
                Name = "Biceps",
                BodyPartId = bodyPartEntity.Id,
                BodyPartEntity = bodyPartEntity
            },
            new MuscleGroupEntity
            {
                Id = Guid.NewGuid(),
                Name = "Triceps",
                BodyPartId = bodyPartEntity.Id,
                BodyPartEntity = bodyPartEntity
            }
        };
        _context.muscleGroups.AddRange(muscleGroupEntities);
        _context.SaveChanges();
        return muscleGroupEntities;
    }

    internal static List<ExerciseEntity> AddExerciseEntities(List<MuscleGroupEntity> muscleGroupEntities)
    {
        List<ExerciseEntity> exerciseEntities = new List<ExerciseEntity>()
        {
            new ExerciseEntity
            {
                Id = Guid.NewGuid(),
                Name = "Bicep Curl",
                MuscleGroupExerciseEntities = new List<MuscleGroupExerciseEntity>()
                {
                    new MuscleGroupExerciseEntity
                    {
                        MuscleGroupId = muscleGroupEntities[0].Id,
                        MuscleGroupEntity = muscleGroupEntities[0]
                    }
                }
            },
            new ExerciseEntity
            {
                Id = Guid.NewGuid(),
                Name = "Tricep Extension",
                MuscleGroupExerciseEntities = new List<MuscleGroupExerciseEntity>()
                {
                    new MuscleGroupExerciseEntity
                    {
                        MuscleGroupId = muscleGroupEntities[1].Id,
                        MuscleGroupEntity = muscleGroupEntities[1]
                    }
                }
            },
            new ExerciseEntity{
            Id = new Guid("84c29521-b68a-475d-b700-51c9b245ff63"),
            Name = "AddWorkout Extension",
            MuscleGroupExerciseEntities = new List<MuscleGroupExerciseEntity>()
            {
                new MuscleGroupExerciseEntity
                {
                    MuscleGroupId = muscleGroupEntities[1].Id,
                    MuscleGroupEntity = muscleGroupEntities[1]
                }
            }
        }
        };
        _context.exercises.AddRange(exerciseEntities);
        _context.SaveChanges();
        return exerciseEntities;
    }

    internal static List<WorkoutEntity> AddWorkoutEntities(GymGoerEntity gymGoerEntity, List<ExerciseEntity> exerciseEntities)
    {
        List<WorkoutEntity> workoutEntities = new List<WorkoutEntity>()
        {
            new WorkoutEntity
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Time = 300,
                GymGoerId = gymGoerEntity.Id,
                GymGoerEntity = gymGoerEntity,
                ExerciseId = exerciseEntities[0].Id,
                ExerciseEntity = exerciseEntities[0],
                Sets = new List<SetEntity>()
                {
                    new SetEntity
                    {
                        Id = Guid.NewGuid(),
                        Reps = 10,
                        Weight = 10,
                        Time = 150
                    },
                    new SetEntity
                    {
                        Id = Guid.NewGuid(),
                        Reps = 10,
                        Weight = 10,
                        Time = 150
                    }
                }
            },
            new WorkoutEntity
            {
              Id  = Guid.NewGuid(),
              Date = DateTime.Now,
              Time = 300,
              GymGoerId = gymGoerEntity.Id,
              GymGoerEntity = gymGoerEntity,
              ExerciseId = exerciseEntities[0].Id,
              ExerciseEntity = exerciseEntities[0],
              Sets = new List<SetEntity>()
              {
                  new SetEntity
                  {
                      Id = Guid.NewGuid(),
                      Reps = 50,
                      Weight = 10,
                      Time = 150
                  },
                  new SetEntity
                  {
                      Id = Guid.NewGuid(),
                      Reps = 50,
                      Weight = 10,
                      Time = 150
                  }
              }
            },
            new WorkoutEntity
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Time = 250,
                GymGoerId = gymGoerEntity.Id,
                GymGoerEntity = gymGoerEntity,
                ExerciseId = exerciseEntities[1].Id,
                ExerciseEntity = exerciseEntities[1],
                Sets = new List<SetEntity>()
                {
                    new SetEntity
                    {
                        Id = Guid.NewGuid(),
                        Reps = 50,
                        Weight = 10,
                        Time = 100
                    },
                    new SetEntity
                    {
                        Id = Guid.NewGuid(),
                        Reps = 50,
                        Weight = 30,
                        Time = 100
                    }
                }
            }
        };
        _context.workouts.AddRange(workoutEntities);
        _context.SaveChanges();
        return workoutEntities;
    }

    internal static PersonalTrainerEntity AddPersonalTrainerEntity()
    {
        PersonalTrainerEntity personalTrainerEntity = new PersonalTrainerEntity()
        {
            Id = new Guid("c9bf8bd0-fba3-4ee7-b414-925136964129"),
            Name = "TrainerTest",
            Email = "Trainer@test.com",
        };
        _context.personalTrainers.Add(personalTrainerEntity);
        _context.SaveChanges();
        return personalTrainerEntity;
    }
}