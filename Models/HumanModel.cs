using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HorseVito.Models
{
    public class HumanModel
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Avatar { get; set; }

        public string PhoneNumber { get; set; }

        public int? RoleId { get; set; }
        public Role Role { get; set; }

        public List<AdvertisementModel> Advertisements = new List<AdvertisementModel>();


    }
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<HumanModel> Users { get; set; }
        public Role()
        {
            Users = new List<HumanModel>();
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class RegModel
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public IFormFile Avatar { get; set; }

        public string PhoneNumber { get; set; }



    }
}