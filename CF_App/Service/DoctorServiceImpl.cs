using CF_App.DTO;
using CF_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CF_App.Service
{
    public class DoctorServiceImpl : IDoctorService
    {
        private readonly CodeFirstContext context;

        public DoctorServiceImpl()
        {
        }

        public DoctorServiceImpl(CodeFirstContext c)
        {
            context = c;
        }

        public int CreateDoctor(DoctorDto dto)
        {
            var lastId = context.Doctor.Max(d => d.IdDoctor);
            var id = lastId + 1;
            var doctor = new Doctor
            {
                IdDoctor = id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };
            context.Doctor.Add(doctor);
            context.SaveChanges();
            return id;
        }

        public DoctorDto FindDoctor(int id)
        {
            var doctor = context.Doctor.SingleOrDefault(n => n.IdDoctor == id);
            return new DoctorDto
            {
                Email = doctor.Email,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName
            };
        }

        public void RemoveDoctor(int id)
        {
            var doctor = context.Doctor.SingleOrDefault(d => d.IdDoctor == id);
            if (doctor != null)
            {
                context.Doctor.Remove(doctor);
                context.SaveChanges();
            }
        }

        public void UpdateDoctor(int id, DoctorDto dto)
        {
            var doctor = context.Doctor.SingleOrDefault(d => d.IdDoctor == id);
            if (doctor != null)
            {
                if (dto.Email != null) doctor.Email = dto.Email;
                if (dto.FirstName != null) doctor.FirstName = dto.FirstName;
                if (dto.LastName != null) doctor.LastName = dto.LastName;
                context.SaveChanges();
            }
        }
    }
}
