using StudentsDiaryWpf.Models.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDiaryWpf.Models.Configurations
{
    public class RatingsConfiguration : EntityTypeConfiguration<Ratings>
    {
        public RatingsConfiguration()
        {
            ToTable("dbo.Ratings");
            HasKey(x => x.Id);

        }
    }
}
