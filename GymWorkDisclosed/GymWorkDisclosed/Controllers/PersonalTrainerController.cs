using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Classes;
using GymWorkDisclosed.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalTrainerService;

namespace GymWorkDisclosed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalTrainerController : ControllerBase
    {
        private TrainerService _trainerService;
        public PersonalTrainerController(TrainerService trainerService)
        {
            _trainerService = trainerService;
        }
        // GET: api/PersonalTrainer/5
        [HttpGet("{id}", Name = "Get")]
        public PersonaltrainerDTO Get(Guid id)
        {
            PersonalTrainer personalTrainer = _trainerService.GetTrainer(id);
            PersonaltrainerDTO personalTrainerDTO = new PersonaltrainerDTO(personalTrainer.Id, personalTrainer.Name, personalTrainer.Email);
            foreach (GymGoer gymGoer in personalTrainer.GymGoers)
            {
                GymGoerWorkoutsDTO gymGoerDTO = new GymGoerWorkoutsDTO(gymGoer.Id, gymGoer.Name, gymGoer.Email);
                foreach (Workout workout in gymGoer.Workouts)
                {
                    WorkoutDTO workoutDTO = new WorkoutDTO(workout.Id, workout.Time, workout.Date);
                    ExerciseDTO exerciseDTO = new ExerciseDTO(workout.Exercise.Id, workout.Exercise.Name);
                    workoutDTO.Exercise = exerciseDTO;
                    foreach (Set set in workout.Sets)
                    {
                        SetDTO setDTO = new SetDTO(set.Id, set.Reps, set.Weight, set.Time);
                        workoutDTO.Sets.Add(setDTO);
                    }
                    gymGoerDTO.Workouts.Add(workoutDTO);
                }
                personalTrainerDTO.GymGoers.Add(gymGoerDTO);
            }
            return personalTrainerDTO;
        }

    }
}
