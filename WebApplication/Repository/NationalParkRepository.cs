using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Database;
using WebApplication.Models;
using WebApplication.Repository.IRepository;

namespace WebApplication.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext _db;

        public NationalParkRepository(ApplicationDbContext db)
        {
            this._db = db;
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return this._db.NationalParks.OrderBy(park => park.Name).ToList();
        }

        public NationalPark GetNationalPark(Guid id)
        {
            return this._db.NationalParks.FirstOrDefault(park => park.Id.Equals(id));
        }

        public bool NationalParkExists(string name)
        {
            return this._db.NationalParks.Any(park => park.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        public bool NationalParkExists(Guid id)
        {
            return this._db.NationalParks.Any(park => park.Id.Equals(id));
        }

        public bool CreateNationalPark(NationalPark nationalPark)
        {
            this._db.Add(nationalPark);
            return this.Save();
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            this._db.NationalParks.Update(nationalPark);
            return this.Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            this._db.Remove(nationalPark);
            return this.Save();
        }

        public bool Save()
        {
            return this._db.SaveChanges() >= 0 ? true : false;
        }
    }
}