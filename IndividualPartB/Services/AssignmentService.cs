using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualPartB.Services
{
    class AssignmentService
    {
        private readonly static string _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        public List<Assignement> GetAllAssignments()
        {
            List<Assignement> assignments = new List<Assignement>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Assignements", conn);
                    SqlDataReader assignmentReader = cmd.ExecuteReader();
                    while (assignmentReader.Read())
                    {
                        Assignement assignment = new Assignement();

                        assignment.Title = (string)assignmentReader["title"];
                        assignment.Description = (string)assignmentReader["description"];
                        assignment.SubDateTime = (DateTime)assignmentReader["subDateTime"];
                        assignment.OralMark = (float)assignmentReader["oralMark"];
                        assignment.TotalMark = (float)assignmentReader["totalMark"];


                        assignments.Add(assignment);
                    }
                    assignmentReader.Close();
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
            return assignments;

        }

        public void CreateAssignment(Assignement assignment)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    string queryString = "insert into Assignements (Title,Description,SubDateTime,OralMark,TotalMark) " +
                                        "values (@title,@description,@subDateTime,@oralMark,@totalMark)";

                    SqlCommand cmd = new SqlCommand(queryString, conn);

                    cmd.Parameters.Add(new SqlParameter("@title", assignment.Title));
                    cmd.Parameters.Add(new SqlParameter("@description", assignment.Description));
                    cmd.Parameters.Add(new SqlParameter("@subDateTime", assignment.SubDateTime));
                    cmd.Parameters.Add(new SqlParameter("@oralMark", assignment.OralMark));
                    cmd.Parameters.Add(new SqlParameter("@totalMark", assignment.TotalMark));

                    int rowsInserted = cmd.ExecuteNonQuery();
                    if (rowsInserted > 0)
                    {
                        Console.WriteLine($"Assignment: {assignment.Description} inserted successfully in the database ");
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
