using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualPartB.Services
{
    class TrainerService
    {
        private readonly static string _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        public List<Trainer> GetAllTrainers()
        {
            List<Trainer> trainers = new List<Trainer>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Trainers", conn);
                    SqlDataReader trainerReader = cmd.ExecuteReader();
                    while (trainerReader.Read())
                    {
                        Trainer trainer = new Trainer()
                        {
                            FirstName = (string)trainerReader["FirstName"],
                            LastName = (string)trainerReader["LastName"],
                            Subject = (string)trainerReader["subject"]

                        };
                        trainers.Add(trainer);
                    }
                    trainerReader.Close();
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
            return trainers;
        }

        public void CreateTrainer(Trainer trainer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    string queryString = "insert into Trainers (FirstName,LastName,Subject) " +
                                         "values (@firstName,@lastName,@subject)";

                    SqlCommand cmd = new SqlCommand(queryString, conn);

                    cmd.Parameters.Add(new SqlParameter("@firstName", trainer.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@lastName", trainer.LastName));
                    cmd.Parameters.Add(new SqlParameter("@subject", trainer.Subject));

                    int rowsInserted = cmd.ExecuteNonQuery();
                    if (rowsInserted > 0)
                    {
                        Console.WriteLine($"Trainer {trainer.FirstName} {trainer.LastName} inserted successfully in the database ");
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
