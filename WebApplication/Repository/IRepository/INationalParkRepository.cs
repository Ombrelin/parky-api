using System;
using System.Collections;
using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.Repository.IRepository
{
    public interface INationalParkRepository
    {
        ICollection<NationalPark> GetNationalParks();
        NationalPark GetNationalPark(Guid id);
        bool NationalParkExists(string name);
        bool NationalParkExists(Guid id);
        bool CreateNationalPark(NationalPark nationalPark);
        bool UpdateNationalPark(NationalPark nationalPark);
        bool DeleteNationalPark(NationalPark nationalPark);
        bool Save();
    }
}