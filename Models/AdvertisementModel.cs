using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HorseVito.Models
{
    public class AdvertisementModel
    {
        public int Id { get; set; }
        public HumanModel Owner { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Breed { get; set; }

        public string Gender { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public int Price { get; set; }

        public string Photo { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

    }
}
