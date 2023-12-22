using High_School_Labb_3.Data;
using High_School_Labb_3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.Identity.Client;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace High_School_Labb_3
{

    public class App
    {
        private DatabaseLogic DatabaseLogic { get; set; }

        public App()
        {
            DatabaseLogic = new();
        }
        public void Start()
        {
            Console.WriteLine();
            Console.WriteLine("*** WELCOME TO THE HIGH SCHOOL ***");
            bool continueProgram = true;
            while (continueProgram)
            {
                Console.WriteLine();
               
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Faculty members:");
                Console.WriteLine("2. Students:");
                Console.WriteLine("3. View Grades from this past month:");
                Console.WriteLine("4. View Courses and Grades (Lowest grade, Top grade, and Average grade):");
                Console.WriteLine("5. Add a new student:");
                Console.WriteLine("6. Add a new faculty member:");
                Console.WriteLine("Enter your choice (1, 2, 3, 4, 5, or 6): ");
                Console.Write("Type 'exit' and press 'ENTER' to leave the program.");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        Console.WriteLine();
                        Console.WriteLine("Select a faculty member option:");
                        Console.WriteLine("a. List of all faculty members:");
                        Console.WriteLine("b. List of faculty members by department:");
                        Console.Write("Enter your sub-choice (a or b): ");
                        Console.WriteLine();
                        string facultyOption = Console.ReadLine();

                        switch (facultyOption)
                        {
                            case "a":
                                DatabaseLogic.ListFaculties();
                                break;
                            case "b":
                                List<string> FacultyMembers = DatabaseLogic.GetAllRoleNames();
                                int counter = 1;
                                Dictionary<string, string> userFacultybyDepartmentName = new();
                                Console.WriteLine("Which department would you like to retrieve more data from? Enter your choice (1, 2, 3, 4, or 51):");
                                foreach (var f in FacultyMembers)
                                {
                                    userFacultybyDepartmentName.Add(counter.ToString(), f);
                                    Console.WriteLine($"{counter}) {f}");
                                    counter++;
                                }

                                var userMenuChoice = Console.ReadLine();
                                DatabaseLogic.ShowFacultyWithSepcificRole(userFacultybyDepartmentName[userMenuChoice]);
                                break;
                            default:
                                Console.WriteLine("Invalid sub-choice for faculty");
                                break;
                        }
                        break;
                    case "2":
                        Console.WriteLine();
                        Console.WriteLine("Select a student option:");
                        Console.WriteLine("a. List of all students:");
                        Console.WriteLine("b. Sort students by First Names:");
                        Console.WriteLine("c. Sort students by Last Names:");
                        Console.WriteLine("d. View students first names in ascending order:");
                        Console.WriteLine("e. View students first names in descending order:");
                        Console.WriteLine("f. View students last names in ascending order:");
                        Console.WriteLine("g. View students last names in descending order:");
                        Console.WriteLine("h. List of courses and students:");
                        Console.Write("Enter your sub-choice (a, b, c, d, e, f, g, or h ): ");
                        Console.WriteLine();
                        string studentOption = Console.ReadLine();

                        switch (studentOption)
                        {
                            case "a":
                                DatabaseLogic.ListStudents();
                                break;
                            case "b":
                                Console.WriteLine("Sorting students by First Names:");
                                DatabaseLogic.ListStudentsFirstName();
                                break;
                            case "c":
                                Console.WriteLine("Sorting students by Last Names:");
                                DatabaseLogic.ListStudentsLastName();
                                break;
                            case "d":
                                Console.WriteLine("First names in Ascending order");
                                DatabaseLogic.ListStudentsFNAscending();
                                break;
                            case "e":
                                Console.WriteLine("First names in Descending order");
                                DatabaseLogic.ListStudentsFNDescending();
                                break;
                            case "f":
                                Console.WriteLine("Last names in Ascending order");
                                DatabaseLogic.ListStudentsLNAscending();
                                break;
                            case "g":
                                Console.WriteLine("Last names in Descending order");
                                DatabaseLogic.ListStudentsLNDescending();
                                break;
                            case "h":
                                Console.WriteLine();
                                Console.WriteLine("Select an option:");
                                Console.WriteLine("a. List of all courses:");
                                Console.WriteLine("b. Select a course to see students in the specific course:");
                                Console.Write("Enter your sub-choice (a or b): ");
                                Console.WriteLine();
                                string courseOption = Console.ReadLine();
                                switch (courseOption)
                                {
                                    case "a":
                                        DatabaseLogic.ListCourses();
                                        break;
                                    case "b":
                                        Console.WriteLine("Select a course:");
                                        Console.WriteLine("1 - Photography");
                                        Console.WriteLine("2 - IT");
                                        Console.WriteLine("3 - Math");
                                        Console.WriteLine("4 - Science");
                                        string userCourseChoice = Console.ReadLine();
                                        string coursechoice = "";
                                        switch (userCourseChoice)
                                        {
                                            case "1":
                                                coursechoice = "Photography101";
                                                break;
                                            case "2":
                                                coursechoice = "IT101";
                                                break;
                                            case "3":
                                                coursechoice = "Math201";
                                                break;
                                            case "4":
                                                coursechoice = "Science102";
                                                break;
                                            default:
                                                Console.WriteLine("Invalid sub-choice for faculty");
                                                break;
                                        }

                                        List<Student> studentsInCourse = DatabaseLogic.GetStudentsByCourse(coursechoice);

                                        if (studentsInCourse.Count > 0)
                                        {
                                            Console.WriteLine($"Students in {coursechoice}:");
                                            foreach (var student in studentsInCourse)
                                            {
                                                Console.WriteLine($"{student.FirstName} {student.LastName}");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine($"No students found for {coursechoice}.");
                                        }
                                        break;
                                     
                                        Console.Write("Enter your sub-choice (1 or 2): ");
                                    }
                                    break;
                            }
                                break;
                            case "3":
                                DatabaseLogic.ListRecentGrades();
                                break;
                    case "4":
                        Console.WriteLine("1. Average grade: \n2. Top grade: \n3. Lowest Grade: \nEnter your sub-choice (1, 2, or 3): ");
                        string input = Console.ReadLine();
                      
                        switch (input) 
                        {
                            case "1":
                                DatabaseLogic AverageGrade = new DatabaseLogic();
                                AverageGrade.ListGradeInfo();
                                break;
                            case "2":
                                DatabaseLogic TopGrade = new DatabaseLogic();
                                TopGrade.ListTopGradeInfo();
                                break;
                            case "3":
                                DatabaseLogic LowGrade = new DatabaseLogic();
                                LowGrade.ListLowestGradeInfo();
                                break;
                              

                        }
                        break;
                            case "5":
                                DatabaseLogic.AddNewStudents();
                        Console.WriteLine("New student was added successfully.");
                        break;
                            case "6":
                                 DatabaseLogic.AddNewFaculty();
                        Console.WriteLine("New faculty member was added successfully.");
                        break;

                            case "exit":
                                continueProgram = false;
                                Console.WriteLine("Exiting the program. Goodbye!");
                                break;

                            default:
                                Console.WriteLine("Invalid choice");
                                break; 
                        
                }
                
            }
            
        }
        
    }
    
}

    




