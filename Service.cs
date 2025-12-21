using EduTrackerNew.Data;
using EduTrackerNew.Models;
using Microsoft.EntityFrameworkCore;

namespace EduTrackerNew
{
    internal class Service
    {
        public static void GetStudents(int sortField, int sortOrder)
        {
            using var context = new EduTrackerDbContext();

            IQueryable<Student> query = context.Students;

            if (sortField == 1 && sortOrder == 1)
            {
                query = query.OrderBy(x => x.FirstName);  //förnamn + asc
            }
            else if (sortField == 1 && sortOrder == 2)
            {
                query = query.OrderByDescending(x => x.FirstName); //förnamn + desc
            }
            else if (sortField == 2 && sortOrder == 2)
            {
                query = query.OrderByDescending(x => x.LastName); // efternamn + desc
            }
            else if (sortField == 2 && sortOrder == 1)
            {
                query = query.OrderBy(s => s.LastName); // efternamn + asc
            }


            foreach (var student in query)
            {
                Console.WriteLine($"{student.FirstName} {student.LastName}");
            }

        }

        public static void ShowAllClasses()
        {
            using var context = new EduTrackerDbContext();
            var allClasses = context.Classes.ToList();


            foreach (var c in allClasses)
            {
                Console.WriteLine(c.ClassName);
            }
        }
        public static void GetStudnetsFromClass(string className)
        {
            using var context = new EduTrackerDbContext();

            List<Student> studentsFromClass = [];

            studentsFromClass = context.Students
                .Include(s => s.Class)
                .Where(c => c.Class.ClassName == className)
                .ToList();

            if (!studentsFromClass.Any())
            {
                Console.WriteLine("Could not find class or no students in that class.");
                return;
            }

            foreach (var student in studentsFromClass)
            {
                Console.WriteLine($"{student.FirstName} {student.LastName}");
            }
        }

        public static void AddStudent(string firstName, string lastName, string className)
        {
            using var context = new EduTrackerDbContext();

            var classEntity = context.Classes
                .FirstOrDefault(c => c.ClassName == className);

            if (classEntity == null)
            {
                Console.WriteLine("Class does not exist.");
                return;
            }

            var newStudent = new Student
            {
                FirstName = firstName,
                LastName = lastName,
                ClassId = classEntity.ClassId
            };

            context.Students.Add(newStudent);
            context.SaveChanges();
        }

        public static List<Staff> GetStaffById(int positionId)
        {
            using var context = new EduTrackerDbContext();
            return context.Staff
                .Where(s => s.PositionId == positionId)
                .ToList();
        }
        public static List<Staff> GetAllStaff()
        {
            using var context = new EduTrackerDbContext();
            return context.Staff.ToList();
        }

        public static void AddStaff(string firstName, string lastname, int positionId)
        {
            using var context = new EduTrackerDbContext();

            var newStaff = new Staff
            {
                FirstName = firstName,
                LastName = lastname,
                PositionId = positionId
            };

            context.Staff.Add(newStaff);
            context.SaveChanges();
        }
    }
}
