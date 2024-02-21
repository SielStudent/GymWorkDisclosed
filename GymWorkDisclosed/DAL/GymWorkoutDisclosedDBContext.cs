using DAL.DBO;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class GymWorkoutDisclosedDBContext: DbContext
{
    public GymWorkoutDisclosedDBContext(DbContextOptions<GymWorkoutDisclosedDBContext> options) : base(options)
    {
    }
    public DbSet<BodyPartEntity> bodyParts { get; set; }
    public DbSet<ExerciseEntity> exercises { get; set; }
    public DbSet<GymGoerEntity> gymGoer { get; set; }
    public DbSet<MuscleGroupEntity> muscleGroups { get; set; }
    public DbSet<SetEntity> sets { get; set; }
    public DbSet<WorkoutEntity> workouts { get; set; }
    public DbSet<PersonalTrainerEntity> personalTrainers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BodyPartEntity>()
            .HasMany(b => b.MuscleGroups)
            .WithOne(m => m.BodyPartEntity)
            .HasForeignKey(m => m.BodyPartId);

        modelBuilder.Entity<ExerciseEntity>()
            .HasMany(e => e.Workouts)
            .WithOne(w => w.ExerciseEntity)
            .HasForeignKey(w => w.ExerciseId);
        
        modelBuilder.Entity<MuscleGroupExerciseEntity>()
            .HasKey(MGE => new { MGE.MuscleGroupId, MGE.ExerciseId });
        
        modelBuilder.Entity<MuscleGroupExerciseEntity>()
            .HasOne(MGE => MGE.MuscleGroupEntity)
            .WithMany(MGE => MGE.MuscleGroupExerciseEntities)
            .HasForeignKey(MGE => MGE.MuscleGroupId);
        
        modelBuilder.Entity<MuscleGroupExerciseEntity>()
            .HasOne(MGE => MGE.ExerciseEntity)
            .WithMany(MGE => MGE.MuscleGroupExerciseEntities)
            .HasForeignKey(MGE => MGE.ExerciseId);

        modelBuilder.Entity<GymGoerEntity>()
            .HasKey(g => g.Id);

        modelBuilder.Entity<GymGoerEntity>()
            .HasMany(g => g.Workouts)
            .WithOne(w => w.GymGoerEntity)
            .HasForeignKey(w => w.GymGoerId);
        
        modelBuilder.Entity<WorkoutEntity>()
            .HasMany(w => w.Sets)
            .WithOne(s => s.WorkoutEntity)
            .HasForeignKey(s => s.WorkoutId);
        
        modelBuilder.Entity<PersonalTrainerEntity>()
            .HasMany(t => t.GymGoers)
            .WithOne(g => g.PersonalTrainerEntity)
            .HasForeignKey(g => g.PersonalTrainerEntityId);
            
    }
}

