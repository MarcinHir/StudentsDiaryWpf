using StudentsDiaryWpf.Models.Domains;
using StudentsDiaryWpf.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StudentsDiaryWpf.Models.Converters
{
    public static class StudentConverter
    {
        public static StudentWrapper ToWrapper(this Student model)
        {
            return new StudentWrapper
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Comments = model.Comments,
                Activities = model.Activities,

                Group = new GroupWrapper
                {
                    Id = model.Group.Id,
                    Name = model.Group.Name
                },
                Math = string.Join(", ",model.Ratings
                        .Where(y => y.SubjectId == (int)Subject.Math)
                        .Select(y => y.Rate)),
                Physics = string.Join(", ", model.Ratings
                        .Where(y => y.SubjectId == (int)Subject.Physics)
                        .Select(y => y.Rate)),
                Technology = string.Join(", ", model.Ratings
                        .Where(y => y.SubjectId == (int)Subject.Technology)
                        .Select(y => y.Rate)),
                History = string.Join(", ", model.Ratings
                        .Where(y => y.SubjectId == (int)Subject.History)
                        .Select(y => y.Rate)),
                Geography = string.Join(", ", model.Ratings
                        .Where(y => y.SubjectId == (int)Subject.Geography)
                        .Select(y => y.Rate)),
                Biology = string.Join(", ", model.Ratings
                        .Where(y => y.SubjectId == (int)Subject.Biology)
                        .Select(y => y.Rate)),
                PolishLanguage = string.Join(", ", model.Ratings
                        .Where(y => y.SubjectId == (int)Subject.PolishLanguage)
                        .Select(y => y.Rate)),
                EnglishLanguage = string.Join(", ", model.Ratings
                        .Where(y => y.SubjectId == (int)Subject.EnglishLanguage)
                        .Select(y => y.Rate))
            };
        }

        public static Student ToDao(this StudentWrapper model)
        {
            return new Student
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Comments = model.Comments,
                Activities = model.Activities,
                GroupId = model.Group.Id
            };
        }

        public static List<Ratings> ToRatingDao(this StudentWrapper model)
        {
            var ratings = new List<Ratings>();

            if (!string.IsNullOrWhiteSpace(model.Math))
                model.Math.Split(',').ToList().ForEach(x => ratings.Add(
                    new Ratings
                    {
                        Rate = int.Parse(x),
                        StudentId = model.Id,
                        SubjectId = (int)Subject.Math
                    }));
            if (!string.IsNullOrWhiteSpace(model.Physics))
                model.Physics.Split(',').ToList().ForEach(x => ratings.Add(
                    new Ratings
                    {
                        Rate = int.Parse(x),
                        StudentId = model.Id,
                        SubjectId = (int)Subject.Physics
                    }));
            if (!string.IsNullOrWhiteSpace(model.Technology))
                model.Technology.Split(',').ToList().ForEach(x => ratings.Add(
                    new Ratings
                    {
                        Rate = int.Parse(x),
                        StudentId = model.Id,
                        SubjectId = (int)Subject.Technology
                    }));
            if (!string.IsNullOrWhiteSpace(model.History))
                model.History.Split(',').ToList().ForEach(x => ratings.Add(
                    new Ratings
                    {
                        Rate = int.Parse(x),
                        StudentId = model.Id,
                        SubjectId = (int)Subject.History
                    }));
            if (!string.IsNullOrWhiteSpace(model.Geography))
                model.Geography.Split(',').ToList().ForEach(x => ratings.Add(
                    new Ratings
                    {
                        Rate = int.Parse(x),
                        StudentId = model.Id,
                        SubjectId = (int)Subject.Geography
                    }));
            if (!string.IsNullOrWhiteSpace(model.Biology))
                model.Biology.Split(',').ToList().ForEach(x => ratings.Add(
                    new Ratings
                    {
                        Rate = int.Parse(x),
                        StudentId = model.Id,
                        SubjectId = (int)Subject.Biology
                    }));
            if (!string.IsNullOrWhiteSpace(model.PolishLanguage))
                model.PolishLanguage.Split(',').ToList().ForEach(x => ratings.Add(
                    new Ratings
                    {
                        Rate = int.Parse(x),
                        StudentId = model.Id,
                        SubjectId = (int)Subject.PolishLanguage
                    }));
            if (!string.IsNullOrWhiteSpace(model.EnglishLanguage))
                model.EnglishLanguage.Split(',').ToList().ForEach(x => ratings.Add(
                    new Ratings
                    {
                        Rate = int.Parse(x),
                        StudentId = model.Id,
                        SubjectId = (int)Subject.EnglishLanguage
                    }));

            return ratings;
        }
    }
}
