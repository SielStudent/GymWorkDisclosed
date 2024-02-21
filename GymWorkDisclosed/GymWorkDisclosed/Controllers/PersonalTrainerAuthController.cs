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
    public class PersonalTrainerAuthController : Controller
    {
        private TrainerService _trainerService;
        public PersonalTrainerAuthController(TrainerService trainerService)
        {
            _trainerService = trainerService;
        }
        
        [HttpGet("{email}",Name = "TrainerLogin")]
        public PersonaltrainerAuthDTO Get(string email)
        {
            PersonalTrainer trainer = _trainerService.GetTrainerByEmail(email);
            if (trainer != null)
            {
                PersonaltrainerAuthDTO gymGoerDto = new PersonaltrainerAuthDTO(trainer.Id, trainer.Name, trainer.Email);
                return gymGoerDto;
            }
            return null;
        }
    }
}