using StudentsDiaryWpf.Models.Configurations;
using StudentsDiaryWpf.Models.Domains;
using StudentsDiaryWpf.Properties;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace StudentsDiaryWpf
{
    public class ApplicationDbContext : DbContext
    {

        private static string _connectionString = $@"Server={Settings.Default.ServerAdress}\{Settings.Default.ServerName};
                                                    Database={Settings.Default.DbName};
                                                    User Id={Settings.Default.Login};
                                                    Password={Settings.Default.Password}";

        public ApplicationDbContext()
            : base(_connectionString)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Ratings> Ratings { get; set; }
        public DbSet<Group> Group { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new RatingsConfiguration());
            modelBuilder.Configurations.Add(new StudentConfiguration());
        }

        
        


}
}