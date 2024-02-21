using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Classes;
using GymWorkDisclosed.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymWorkDisclosed.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly BusinessLogic.Services.AuthService.AuthService _authService;
        
        public AuthController(BusinessLogic.Services.AuthService.AuthService authService)
        {
            _authService = authService;
        }
        
        // GET: api/Auth/5
        [HttpGet("{email}",Name = "Login")]
        [Authorize]
        public GymGoerDTO Get(string email)
        {
            GymGoer gymGoer = _authService.GetGymGoerByEmail(email);
            if (gymGoer != null)
            {
                GymGoerDTO gymGoerDto = new GymGoerDTO(gymGoer.Id, gymGoer.Name, gymGoer.Email);
                return gymGoerDto;
            }
            return null;
        }
        
    }
}
