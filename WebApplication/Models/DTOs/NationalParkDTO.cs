using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models.DTOs
{
    public class NationalParkDTO
    {
        public Guid Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string State { get; set; }
        public DateTime Created { get; set; }
        public DateTime Established { get; set; }
    }
}