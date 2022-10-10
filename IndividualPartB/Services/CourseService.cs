using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualPartB.Services
{
    class CourseService
    {
        private readonly static string _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;


        public List<Cours> GetAllCourses()
        {
            List<Cours> courses = new List<Cours>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Courses", conn);
                    SqlDataReader coursesReader = cmd.ExecuteReader();
                    while (coursesReader.Read())
                    {
                        Cours course = new Cours();

                        course.Title = (string)coursesReader["title"];
                        course.Stream = (string)coursesReader["stream"];
                        course.Type = (string)coursesReader["type"];
                        course.StartDate = (DateTime)coursesReader["startDate"];
                        course.EndDate = (DateTime)coursesReader["endDate"];


                        courses.Add(course);
                    }
                    coursesReader.Close();
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
            return courses;
        }

        public void CreateCourse(Cours course)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    string queryString = "insert into Courses (Title,Stream,Type,StartDate,EndDate) " +
                                        "values (@title,@stream,@type,@startDate,@endDate)";

                    SqlCommand cmd = new SqlCommand(queryString, conn);

                    cmd.Parameters.Add(new SqlParameter("@title", course.Title));
                    cmd.Parameters.Add(new SqlParameter("@stream", course.Stream));
                    cmd.Parameters.Add(new SqlParameter("@type", course.Type));
                    cmd.Parameters.Add(new SqlParameter("@startDate", course.StartDate));
                    cmd.Parameters.Add(new SqlParameter("@endDate", course.EndDate));

                    int rowsInserted = cmd.ExecuteNonQuery();
                    if (rowsInserted > 0)
                    {
                        Console.WriteLine($"Course: {course.Title} inserted successfully in the database ");
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
