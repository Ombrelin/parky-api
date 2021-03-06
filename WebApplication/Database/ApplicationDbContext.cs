﻿using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        
        public DbSet<NationalPark> NationalParks { get; set; }
    }
}