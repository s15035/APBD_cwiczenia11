using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CF_App.DTO;
using CF_App.Service;
using Microsoft.AspNetCore.Mvc;

namespace CF_App.Controllers
{
    [Route("api/doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService service;

        public DoctorController(IDoctorService s)
        {
            service = s;
        }
        [HttpGet("{id}")]
        public IActionResult GetDoctorInfo(int id)
        {
            var doctor = service.FindDoctor(id);
            return Ok(doctor);
        }

        [HttpPost]
        public IActionResult CreateDoctor(DoctorDto dto)
        {
            var id = service.CreateDoctor(dto);
            return Created("", id);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveDoctor(int id)
        {
            service.RemoveDoctor(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDoctor(int id, DoctorDto dto)
        {
            service.UpdateDoctor(id, dto);
            return Ok();
        }
    }
}