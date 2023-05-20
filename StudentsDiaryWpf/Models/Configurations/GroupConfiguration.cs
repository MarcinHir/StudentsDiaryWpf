using StudentsDiaryWpf.Models.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentsDiaryWpf.Models.Configurations
{
    public class GroupConfiguration : EntityTypeConfiguration<Domains.Group>
    {
        public GroupConfiguration()
        {
            ToTable("dbo.Group");
            Property(x => x.Id)
             .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired();
            

        }
    }
}
