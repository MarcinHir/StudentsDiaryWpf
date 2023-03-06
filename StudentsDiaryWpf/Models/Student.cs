using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentsDiaryWpf.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Comments { get; set; }
        public string Math { get; set; }
        public string Physics { get; set; }
        public string Biology { get; set; }
        public string History { get; set; }
        public string Geography { get; set; }
        public string Technology { get; set; }
        public string PolishLanguage { get; set; }
        public string EnglishLanguage { get; set; }
        public bool Activities { get; set; }
        public Group Group { get; set; }
    }
}
