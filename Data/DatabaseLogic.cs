using High_School_Labb_3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace High_School_Labb_3.Data
{
    public class DatabaseLogic
    {
        private HighSchoolContext Context { get; set; }
        public DatabaseLogic()
        {
            Context = new();
        }
        public void ListFaculties()
        {
            var faculties = Context.Faculties.ToList();
            foreach (var faculty in faculties)
            {
                Console.WriteLine($"{faculty.FirstName} {faculty.LastName}");
            }
        }

        public void ListFacultiesByDepartment()
        {
            Console.WriteLine("List faculty members by department:");
            var roles = Context.Roles.ToList();
            foreach (var role in roles)
            {
                Console.WriteLine($"{role.Role1}");
            }
        }
        public void ShowFacultyWithSepcificRole(string rolename)
        {
            var FacultyMembersbyRole = Context.Faculties
                .Include(fd => fd.FkRole)
                .Where(fd => fd.FkRole.Role1 == rolename)
                .ToList();

            foreach (var fbd in FacultyMembersbyRole)
            {
                Console.WriteLine($"{fbd.FirstName} {fbd.LastName} {fbd.FkRole.Role1}");
            }
            Console.ReadKey();
        }
        public void ShowStudentWithSpecificRole(string className)
        {
            var studentsInClass = Context.Enrollments
                .Where(enrollment => enrollment.FkStudent.FirstName == className)
                .ToList();

            foreach (var enrollment in studentsInClass)
            {
                Console.WriteLine($"{enrollment.FkStudent.FirstName} {enrollment.FkStudent.LastName}");
            }

            Console.ReadKey();
        }

        public void ListStudentbyCourses()
        {
            Console.WriteLine("List students by specific courses:");
            var students = Context.Students.ToList();
            foreach (var student in students)
            {
                Console.WriteLine($"{student.FirstName}");
            }
        }

        public void ListStudents()
        {
            var students = Context.Students.ToList();
            foreach (var student in students)
            {
                Console.WriteLine($"{student.FirstName} {student.LastName}");
            }
        }
        public void ListStudentsFirstName()
        {
            var students = Context.Students.ToList();
            foreach (var student in students)
            {
                Console.WriteLine($"{student.FirstName}");
            }
        }
        public void ListStudentsLastName()
        {
            var students = Context.Students.ToList();
            foreach (var student in students)
            {
                Console.WriteLine($"{student.LastName}");
            }
        }
        public void ListStudentsFNAscending()
        {
            var students = Context.Students.OrderBy(student => student.FirstName).ToList();
            PrintStudentNames(students);
        }

        public void ListStudentsFNDescending()
        {
            var students = Context.Students.OrderByDescending(student => student.FirstName).ToList();
            PrintStudentNames(students);
        }
        public void ListStudentsLNAscending()
        {
            var students = Context.Students.OrderBy(student => student.LastName).ToList();
            PrintStudentNames(students);
        }

        public void ListStudentsLNDescending()
        {
            var students = Context.Students.OrderByDescending(student => student.LastName).ToList();
            PrintStudentNames(students);
        }

        public void PrintStudentNames(List<Student> students)
        {
            foreach (var student in students)
            {
                Console.WriteLine($"{student.FirstName} {student.LastName}");
            }
        }

        public void ListCourses()
        {
            var courses = Context.Courses.ToList();
            foreach (var course in courses)
            {
                Console.WriteLine($"{course.CourseName}");
            }
        }

        public void ListRecentGrades()
        {
            DateTime oneMonthAgo = DateTime.Now.AddMonths(-1);
            var grades = Context.Enrollments
                .Include(e => e.FkStudent)
                .Include(e => e.FkCourse)
                .Where(grade => grade.DateOfGrade >= oneMonthAgo)
                .ToList();

            foreach (var grade in grades)
            {
                Console.WriteLine($"Student: {GetStudentName(grade.FkStudent)}, Course: {GetCourseName(grade.FkCourse)}, Grade: {grade.GradeInfo}, Date: {grade.DateOfGrade}");
            }
        }

        public string GetStudentName(Student student)
        {
            return $"{student.FirstName} {student.LastName}";
        }

        public string GetCourseName(Course course)
        {
            return course.CourseName;
        }

        public List<string> GetAllRoleNames()
        {
            return Context.Roles
                .Select(r => r.Role1)
                .ToList();
        }
        public void AddNewStudents()
        {
            HighSchoolContext context = new HighSchoolContext();

            Console.WriteLine("Type in the first name of the new student:");
            string studentFirstName = Console.ReadLine();

            Console.WriteLine("Type in the last name of the new student:");
            string studentLastName = Console.ReadLine();

            Console.WriteLine("Type the date of birth (YYYY-MM-DD) of the new student. Ex. 1984-11-20:");
            string studentDoB = Console.ReadLine();
            DateTime sdate = DateTime.Parse(studentDoB);
            Console.WriteLine("Type the major of the new student: \nAvailable options: \nPhotography \nIT \nMath \nScience");
            string studentMajor = Console.ReadLine();
            Console.WriteLine("Type the class number of the new student: \n4 = Photography \n5 = IT \n6 = Math \n7 = Science");
            int studentClassId = Int32.Parse(Console.ReadLine());
            var newStudent = new Student
            {
                FirstName = studentFirstName,
                LastName = studentLastName,
                DateOfBirth = sdate,
                Major = studentMajor,
                FkClassId = studentClassId
            };
            context.Students.Add(newStudent);
            context.SaveChanges();

            Console.WriteLine($"New student added: {newStudent.FirstName} {newStudent.LastName}, Date Of Birth: {newStudent.DateOfBirth}, Major: {newStudent.Major}, Class ID: {newStudent.FkClassId}");
            Console.ReadKey();
        }

        public void AddNewFaculty()
        {
            HighSchoolContext context = new HighSchoolContext();

            Console.WriteLine("Type in the first name of the new faculty member:");
            string facultyFirstName = Console.ReadLine();

            Console.WriteLine("Type in the last name of the new faculty member:");
            string facultyLastName = Console.ReadLine();

            Console.WriteLine("Type the date of birth (YYYY-MM-DD) of the new faculty member. Ex. 1984-11-20:");
            string facultyDoB = Console.ReadLine();
            DateTime fdate = DateTime.Parse(facultyDoB);
            Console.WriteLine("Type the role number of the new faculty member. \n1 (Principal): \n2 (Admin): \n3 (Teacher): \n4 (Cafeteria): \n5 (Janitor):");
            int facultyRoleId = Int32.Parse(Console.ReadLine());
            var newFaculty = new Faculty
            {
                FirstName = facultyFirstName,
                LastName = facultyLastName,
                DateOfBirth = fdate,
                FkRoleId = facultyRoleId
            };
            context.Faculties.Add(newFaculty);
            context.SaveChanges();

            Console.WriteLine($"New faculty added: {newFaculty.FirstName} {newFaculty.LastName}, Date Of Birth: {newFaculty.DateOfBirth}, Role ID: {newFaculty.FkRoleId}");
            Console.ReadKey();
        }



        internal List<string> GetAllStudentNames()
        {
            return Context.Students.Select(s => s.FirstName + " " + s.LastName).ToList();
        }

        internal List<string> ShowStudentWithSepcificRole()
        {
            return Context.Classes.Select(c => c.Class1).ToList();
        }



        public List<Student> GetStudentsByCourse(string courseName)
        {
            return Context.Enrollments
                .Include(e => e.FkStudent)
                .Include(e => e.FkCourse)
                .Where(e => e.FkCourse.CourseName == courseName)
                .Select(e => e.FkStudent)
                .ToList();
        }

        public void ListGradeInfo()
        {
            HighSchoolContext context = new HighSchoolContext();
            var average = context.Enrollments.Average(g => g.GradeInfo);
            Console.WriteLine("Average grade: " + Math.Round(average,2));
            Console.ReadKey();
        }
        public void ListTopGradeInfo()
        {
            HighSchoolContext context = new HighSchoolContext();
            var top = context.Enrollments.Max(g => g.GradeInfo);
            Console.WriteLine("Top grade: " + top);
            Console.ReadKey();
        }

        public void ListLowestGradeInfo()
        {
            HighSchoolContext context = new HighSchoolContext();
            var low = context.Enrollments.Min(g => g.GradeInfo);
            Console.WriteLine("Lowest grade: " + low);
            Console.ReadKey();
        }



    }
}