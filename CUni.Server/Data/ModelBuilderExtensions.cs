using CUni.Shared;
using Microsoft.EntityFrameworkCore;

namespace CUni.Server.Data
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder Seed(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Course>().ToTable(nameof(Course))
                .HasMany(c => c.Instructors)
                .WithMany(i => i.Courses);
            modelBuilder.Entity<Student>().ToTable(nameof(Student));
            modelBuilder.Entity<Instructor>().ToTable(nameof(Instructor));

            var alexander = new Student { Id = 1, FirstMidName = "Carson", LastName = "Alexander", EnrollmentDate = DateTime.Parse("2016-09-01") };
            var alonso = new Student { Id = 2, FirstMidName = "Meredith", LastName = "Alonso", EnrollmentDate = DateTime.Parse("2018-09-01") };
            var anand = new Student { Id = 3, FirstMidName = "Arturo", LastName = "Anand", EnrollmentDate = DateTime.Parse("2019-09-01") };
            var barzdukas = new Student { Id = 4, FirstMidName = "Gytis", LastName = "Barzdukas", EnrollmentDate = DateTime.Parse("2018-09-01") };
            var justice = new Student { Id = 6, FirstMidName = "Peggy", LastName = "Justice", EnrollmentDate = DateTime.Parse("2017-09-01") };
            var li = new Student { Id = 5, FirstMidName = "Yan", LastName = "Li", EnrollmentDate = DateTime.Parse("2018-09-01") };
            var norman = new Student { Id = 7, FirstMidName = "Laura", LastName = "Norman", EnrollmentDate = DateTime.Parse("2019-09-01") };
            var olivetto = new Student { Id = 8, FirstMidName = "Nino", LastName = "Olivetto", EnrollmentDate = DateTime.Parse("2011-09-01") };

            var abercrombie = new Instructor { InstructorId = 4, FirstMidName = "Kim", LastName = "Abercrombie", HireDate = DateTime.Parse("1995-03-11") };
            var fakhouri = new Instructor { InstructorId = 1, FirstMidName = "Fadi", LastName = "Fakhouri", HireDate = DateTime.Parse("2002-07-06") };
            var harui = new Instructor { InstructorId = 2, FirstMidName = "Roger", LastName = "Harui", HireDate = DateTime.Parse("1998-07-01") };
            var kapoor = new Instructor { InstructorId = 3, FirstMidName = "Candace", LastName = "Kapoor", HireDate = DateTime.Parse("2001-01-15") };
            var zheng = new Instructor { InstructorId = 5, FirstMidName = "Roger", LastName = "Zheng", HireDate = DateTime.Parse("2004-02-12") };

            var economics = new Department { Id = 4, Name = "Economics", Budget = 100000, StartDate = DateTime.Parse("2007-09-01"), Administrator = kapoor };
            var engineering = new Department { Id = 3, Name = "Engineering", Budget = 350000, StartDate = DateTime.Parse("2007-09-01"), Administrator = harui };
            var english = new Department { Id = 1, Name = "English", Budget = 350000, StartDate = DateTime.Parse("2007-09-01"), Administrator = abercrombie };
            var mathematics = new Department { Id = 2, Name = "Mathematics", Budget = 100000, StartDate = DateTime.Parse("2007-09-01"), Administrator = fakhouri };


            var calculus = new Course { Id = 1045, Title = "Calculus", Credits = 4, Department = mathematics, Instructors = new List<Instructor> { fakhouri } };
            var chemistry = new Course { Id = 1050, Title = "Chemistry", Credits = 3, Department = engineering, Instructors = new List<Instructor> { kapoor, harui } };
            var composition = new Course { Id = 2021, Title = "Composition", Credits = 3, Department = english, Instructors = new List<Instructor> { abercrombie } };
            var literature = new Course { Id = 2042, Title = "Literature", Credits = 4, Department = english, Instructors = new List<Instructor> { abercrombie } };
            var macroeconomics = new Course { Id = 4041, Title = "Macroeconomics", Credits = 3, Department = economics, Instructors = new List<Instructor> { zheng } };
            var microeconomics = new Course { Id = 4022, Title = "Microeconomics", Credits = 3, Department = economics, Instructors = new List<Instructor> { zheng } };
            var trigonometry = new Course { Id = 3141, Title = "Trigonometry", Credits = 4, Department = mathematics, Instructors = new List<Instructor> { harui } };

            modelBuilder.Entity<Student>().HasData(
                alexander,
                alonso,
                anand,
                barzdukas,
                justice,
                li,
                norman,
                olivetto
                );

            modelBuilder.Entity<Instructor>().HasData(
                  abercrombie,
                  fakhouri,
                  harui,
                  kapoor,
                  zheng
                    );

            modelBuilder.Entity<OfficeAssignment>().HasData(
                new OfficeAssignment { Instructor = fakhouri, Location = "Smith 17" },
                new OfficeAssignment { Instructor = harui, Location = "Gowan 27" },
                new OfficeAssignment { Instructor = kapoor, Location = "Thompson 304" }
                );

            modelBuilder.Entity<Department>().HasData(
                economics,
                engineering,
                english,
                mathematics
                );


            modelBuilder.Entity<Course>().HasData(
                calculus,
                chemistry,
                composition,
                engineering,
                literature,
                macroeconomics,
                mathematics,
                microeconomics,
                trigonometry
                );

            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { Student = alexander, Course = chemistry, Grade = Grade.A },
                new Enrollment { Student = alexander, Course = microeconomics, Grade = Grade.C },
                new Enrollment { Student = alexander, Course = macroeconomics, Grade = Grade.B },
                new Enrollment { Student = alonso, Course = calculus, Grade = Grade.B },
                new Enrollment { Student = alonso, Course = trigonometry, Grade = Grade.B },
                new Enrollment { Student = alonso, Course = composition, Grade = Grade.B },
                new Enrollment { Student = anand, Course = chemistry },
                new Enrollment { Student = anand, Course = microeconomics, Grade = Grade.B },
                new Enrollment { Student = barzdukas, Course = chemistry, Grade = Grade.B },
                new Enrollment { Student = li, Course = composition, Grade = Grade.B },
                new Enrollment { Student = justice, Course = literature, Grade = Grade.B }
                );

            return modelBuilder;
        }
    }
}
