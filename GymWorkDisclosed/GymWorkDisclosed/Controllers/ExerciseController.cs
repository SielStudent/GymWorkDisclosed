using BusinessLogic.Classes;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Services.ExerciseService;
using GymWorkDisclosed.DTOs;

namespace GymWorkDisclosed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        
        private readonly ExerciseService _exerciseService;
        public ExerciseController(ExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }
        // GET: api/Exercise
        [HttpGet]
        public IActionResult Get()
        {
            List<Exercise> exercises = _exerciseService.GetAllExercises();
            List<ExerciseDTO> exerciseDTOs = new List<ExerciseDTO>();
            foreach (Exercise exercise in exercises)
            {
                ExerciseDTO exerciseDto = new ExerciseDTO(exercise.Id, exercise.Name);
                foreach (MuscleGroup muscleGroup in exercise.MuscleGroups)
                {
                    MuscleGroupDTO muscleGroupDto = new MuscleGroupDTO(muscleGroup.Id, muscleGroup.Name);
                    muscleGroupDto.Bodypart = new BodypartDTO(muscleGroup.BodyPart.Id, muscleGroup.BodyPart.Name);
                    exerciseDto.MuscleGroups.Add(muscleGroupDto);
                }
                exerciseDTOs.Add(exerciseDto);
            }
            return Ok(exerciseDTOs);
        }
        [HttpGet]
        [Route("GetExercisesByGymGoer/{GymgoerId:guid}")]
        public IActionResult GetExercisesByGymGoer(Guid GymgoerId)
        {
            List<Exercise> exercises = _exerciseService.GetExercisesByGymGoer(GymgoerId);
            List<ExerciseDTO> exerciseDTOs = new List<ExerciseDTO>();
            foreach (Exercise exercise in exercises)
            {
                ExerciseDTO exerciseDto = new ExerciseDTO(exercise.Id, exercise.Name);
                foreach (MuscleGroup muscleGroup in exercise.MuscleGroups)
                {
                    MuscleGroupDTO muscleGroupDto = new MuscleGroupDTO(muscleGroup.Id, muscleGroup.Name);
                    muscleGroupDto.Bodypart = new BodypartDTO(muscleGroup.BodyPart.Id, muscleGroup.BodyPart.Name);
                    exerciseDto.MuscleGroups.Add(muscleGroupDto);
                }
                exerciseDTOs.Add(exerciseDto);
            }
            return Ok(exerciseDTOs);
        }
    }
}
