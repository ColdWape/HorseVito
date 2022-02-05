using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorseVito.Models
{
    public class DataBaseContext : DbContext
    {
        public DbSet<HumanModel> People { get; set; }
        public DbSet<Role> Roles { get; set; }
        
        public DbSet<AdvertisementModel> Advertisements { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> dbContextOptions)
            : base(dbContextOptions)
        {
            Database.EnsureCreated();
            SaveChanges();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminLogin = "kchr";
            string adminPassword = "sila";

            // добавляем роли
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };

            HumanModel adminUser = new HumanModel { Id = 1, Username = adminLogin, Password = adminPassword, RoleId = adminRole.Id, Avatar = "../themes/images/ladies/4.jpg" };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<HumanModel>().HasData(new HumanModel[] { adminUser });
            base.OnModelCreating(modelBuilder);
        }
    }
}
