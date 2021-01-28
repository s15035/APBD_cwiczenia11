using CF_App.DTO;
using CF_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CF_App.Service
{
    public interface IDoctorService
    {
        DoctorDto FindDoctor(int id);
        void RemoveDoctor(int id);
        void UpdateDoctor(int id, DoctorDto dto);
        int CreateDoctor(DoctorDto dto);
    }
}
