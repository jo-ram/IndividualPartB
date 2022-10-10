using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualPartB.Services
{
    class StudentService
    {
        private readonly static string _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Students", conn);
                    SqlDataReader studentReader = cmd.ExecuteReader();
                    while (studentReader.Read())
                    {
                        Student student = new Student()
                        {
                            FirstName = (string)studentReader["FirstName"],
                            LastName = (string)studentReader["LastName"],
                            DateOfBirth = (DateTime)studentReader["DateOfBirth"],
                            TuitionFees = (decimal)studentReader["TuitionFees"]
                        };
                        students.Add(student);
                    }
                    studentReader.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Sql exception: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
                finally
                {
                    conn.Close();
                }
            }
            return students;
        }

       


        public void CreateStudent(Student student)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    string queryString = "insert into Students (FirstName,LastName,DateOfBirth,TuitionFees) " +
                                         "values (@FirstName,@LastName,@DateOfBirth,@TuitionFees)";

                    SqlCommand cmd = new SqlCommand(queryString, conn);

                    cmd.Parameters.Add(new SqlParameter("@FirstName", student.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@LastName", student.LastName));
                    cmd.Parameters.Add(new SqlParameter("@DateOfBirth", student.DateOfBirth));
                    cmd.Parameters.Add(new SqlParameter("@TuitionFees", student.TuitionFees));

                    int rowsInserted = cmd.ExecuteNonQuery();
                    if (rowsInserted > 0)
                    {
                        Console.WriteLine($"Student {student.FirstName} {student.LastName} inserted successfully in the database ");
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Sql exception: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
