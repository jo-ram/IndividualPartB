using IndividualPartB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualPartB
{
    class Program
    {
        static void Main(string[] args)
        {
            StudentService studServ = new StudentService();
            TrainerService trainServ = new TrainerService();
            CourseService coursServ = new CourseService();
            AssignmentService assServ = new AssignmentService();

            int answer = 0;


            while (answer != 8)
            {
                Console.WriteLine("Press:");
                Console.WriteLine("1 to add Course");
                Console.WriteLine("2 to add Student");
                Console.WriteLine("3 to add Trainer");
                Console.WriteLine("4 to add Assignment");
                Console.WriteLine("5 to show list of all students");
                Console.WriteLine("6 to show list of all trainers");
                Console.WriteLine("7 to add Assignments per Course Per Student");
                Console.WriteLine("Press 8 to exit the program");
                answer = Convert.ToInt32(Console.ReadLine());




                if (answer == 1)
                {
                    Cours course = new Cours();
                    Console.WriteLine("Give the Course Title");
                    course.Title = Console.ReadLine();
                    Console.WriteLine("Give the Course Stream");
                    course.Stream = Console.ReadLine();
                    Console.WriteLine("Give the Course Type");
                    course.Type = Console.ReadLine();
                    Console.WriteLine("Give the Start Date (yyyy-mm-dd)");
                    course.StartDate = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Give the End Date (yyyy-mm-dd)");
                    course.EndDate = DateTime.Parse(Console.ReadLine());


                    //add to database

                    coursServ.CreateCourse(course);
                }

                if (answer == 2)
                {
                    Student studentInsert = new Student();
                    Console.WriteLine("Give the Student First Name");
                    studentInsert.FirstName = Console.ReadLine();
                    Console.WriteLine("Give the Student Last Name");
                    studentInsert.LastName = Console.ReadLine();
                    Console.WriteLine("Give the Date Of Birth (yyyy-mm-dd)");
                    studentInsert.DateOfBirth = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Give the Tuition Fees");
                    studentInsert.TuitionFees = decimal.Parse(Console.ReadLine());

                    //StudentService studServ1 = new StudentService();
                    studServ.CreateStudent(studentInsert);
                }


                if (answer == 3)
                {
                    Trainer trainer = new Trainer();
                    Console.WriteLine("Give the Trainer First Name");
                    trainer.FirstName = Console.ReadLine();
                    Console.WriteLine("Give the Trainer Last Name");
                    trainer.LastName = Console.ReadLine();
                    Console.WriteLine("Give the Trainer's subject");
                    trainer.Subject = Console.ReadLine();
                    trainServ.CreateTrainer(trainer);
                }

                if (answer == 4)
                {
                    Assignement assignment = new Assignement();
                    Console.WriteLine("Give the Assignment Title");
                    assignment.Title = Console.ReadLine();
                    Console.WriteLine("Give the Assignment Description");
                    assignment.Description = Console.ReadLine();
                    Console.WriteLine("Give the Submission Date (yyyy-mm-dd)");
                    assignment.SubDateTime = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Give the Oral Mark");
                    assignment.OralMark = int.Parse(Console.ReadLine());
                    Console.WriteLine("Give the Total Mark");
                    assignment.TotalMark = int.Parse(Console.ReadLine());
                    assServ.CreateAssignment(assignment);
                }

                if (answer == 5)
                {

                    foreach (var student in studServ.GetAllStudents())
                    {
                        Console.WriteLine(student.ToString());
                    }
                }

                if (answer == 6)
                {

                    foreach (var trainer in trainServ.GetAllTrainers())
                    {
                        Console.WriteLine(trainer.ToString());
                    }
                }

                if (answer == 7)
                {

                    foreach (var assignment in assServ.GetAllAssignments())
                    {
                        Console.WriteLine(assignment.ToString());
                    }
                }
            }

        }
    }
}