using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentsDiaryWpf.Models.Domains;
using System.Threading.Tasks;
using StudentsDiaryWpf.Models.Wrappers;
using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;
using StudentsDiaryWpf.Models.Converters;
using System.Runtime.Remoting.Contexts;
using StudentsDiaryWpf.Models;
using System.Windows.Media;

namespace StudentsDiaryWpf
{
    public class Repository
    {
        public List<Group> GetGroups()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Group.ToList();
            }
        }

        public List<StudentWrapper> GetStudents(int groupId)
        {
            using(var context = new ApplicationDbContext())
            {
                var students = context
                    .Students
                    .Include(x => x.Group)
                    .Include(x => x.Ratings)
                    .AsQueryable();

                if (groupId != 0) 
                students = students.Where(x => x.GroupId == groupId);
                return students
                    .ToList()
                    .Select(x => x.ToWrapper())
                    .ToList();
                
            }
        }

        public void DeleteStudent(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var studentToDelete = context.Students.Find(id);
                context.Students.Remove(studentToDelete);
                context.SaveChanges();
            }
        }

        public void UpdateStudent(StudentWrapper studentWrapper)
        {
            var student = studentWrapper.ToDao();
            var ratings = studentWrapper.ToRatingDao();

            using (var context = new ApplicationDbContext())
            {
                UpdateStudentProperties(context, student);

                var studentRatings = GetStudentRatings(context, student);

                UpdateRate(student, ratings, context, studentRatings, Subject.Math);
                UpdateRate(student, ratings, context, studentRatings, Subject.Physics);
                UpdateRate(student, ratings, context, studentRatings, Subject.Technology);
                UpdateRate(student, ratings, context, studentRatings, Subject.Biology);
                UpdateRate(student, ratings, context, studentRatings, Subject.Geography);
                UpdateRate(student, ratings, context, studentRatings, Subject.PolishLanguage);
                UpdateRate(student, ratings, context, studentRatings, Subject.EnglishLanguage);
                UpdateRate(student, ratings, context, studentRatings, Subject.History);

                context.SaveChanges();


            }
        }

        private void UpdateStudentProperties(ApplicationDbContext context, Student student)
        {
            var studentToUpdate = context.Students.Find(student.Id);
            studentToUpdate.Activities = student.Activities;
            studentToUpdate.Comments = student.Comments;
            studentToUpdate.FirstName = student.FirstName;
            studentToUpdate.LastName = student.LastName;
            studentToUpdate.GroupId = student.GroupId;
        }

        private static List<Ratings> GetStudentRatings(ApplicationDbContext context, Student student)
        {
            return context
                    .Ratings
                    .Where(x => x.StudentId == student.Id)
                    .ToList();
        }
        private static void UpdateRate(Student student, List<Ratings> newRatings, ApplicationDbContext context, List<Ratings> studentRatings, Subject subject)
        {
            var subRatings = studentRatings
                    .Where(x => x.SubjectId == (int)subject)
                    .Select(x => x.Rate);

            var newsubRatings = newRatings
                .Where(x => x.SubjectId == (int)subject)
                .Select(x => x.Rate);

            var subRatingsToDelete = subRatings.Except(newsubRatings).ToList();
            var subRatingsToAdd = newsubRatings.Except(subRatings).ToList();


            subRatingsToDelete.ForEach(x =>
            {
                var ratingToDelete = context.Ratings.First(y =>
                y.Rate == x &&
                y.StudentId == student.Id &&
                y.SubjectId == (int)subject);

                context.Ratings.Remove(ratingToDelete);
            });

            subRatingsToAdd.ForEach(x =>
            {
                var ratingToAdd = new Ratings
                {
                    Rate = x,
                    StudentId = student.Id,
                    SubjectId = (int)subject
                };
                context.Ratings.Add(ratingToAdd);
            });
        }

        public void AddStudent(StudentWrapper studentWrapper)
        {
            var student = studentWrapper.ToDao();
            var ratings = studentWrapper.ToRatingDao();

            using (var context = new ApplicationDbContext()) 
            {
                var dbStudent = context.Students.Add(student);

                ratings.ForEach(x =>
                {
                    x.StudentId = dbStudent.Id;
                    context.Ratings.Add(x);
                });

            context.SaveChanges();
            }
            
        }

    }
}
