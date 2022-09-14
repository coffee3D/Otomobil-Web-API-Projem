using NetCoreAPIMSSQL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAPIMSSQL.Data.Repositories
{
    public class CarRepository : ICarRepository
    {
        private MsSQLConfiguration _connectionString;

        public CarRepository(MsSQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> DeleteCar(int id)
        {
            string query = @" 
                              DELETE FROM Cars WHERE Id = '" + id + "' ";

            string sqlDataSource = _connectionString.ConnectionString;

            int result = 0;

            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    result = await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                }
            }

            return result > 0;
        }

        public async Task<IEnumerable<Car>> GetAllCars()
        {

            string query = @"
                             SELECT Id, Make, Model, Color, Year, Doors FROM Cars
                           ";

            List<Car> carList = new List<Car>();

            string constr = _connectionString.ConnectionString;

            using (SqlConnection conn = new SqlConnection(constr))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        Car car = new Car()
                        {
                            Id = reader.GetInt32(0),
                            Make = reader.GetString(1),
                            Model = reader.GetString(2),
                            Color = reader.GetString(3),
                            Year = reader.GetInt32(4),
                            Doors = reader.GetInt16(5)
                        };
                        carList.Add(car);
                    }

                    reader.Close();
                    conn.Close();
                }
            }

            return carList;

        }

        public async Task<Car> GetCarDetails(int id)
        {
            string query = @"
                             SELECT Id, Make, Model, Color, Year, Doors FROM Cars WHERE Id = '" + id + "' ";

            string sqlDataSource = _connectionString.ConnectionString;

            SqlDataReader sqlDataReader = null;

            Car returnCar = new Car();

            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    sqlDataReader = await cmd.ExecuteReaderAsync();
                    while (await sqlDataReader.ReadAsync())
                    {
                        Car car = new Car()
                        {
                            Id = sqlDataReader.GetInt32(0),
                            Make = sqlDataReader.GetString(1),
                            Model = sqlDataReader.GetString(2),
                            Color = sqlDataReader.GetString(3),
                            Year = sqlDataReader.GetInt32(4),
                            Doors = sqlDataReader.GetInt16(5)
                        };
                        returnCar = car;
                    }


                    sqlDataReader.Close();
                    conn.Close();
                }
            }

            return returnCar;
        }

        public async Task<bool> InsertCar(Car car)
        {
            string query = @" 
                             INSERT INTO Cars (Make, Model, Color, Year, Doors)
                             VALUES ('" + car.Make + "', '" + car.Model + "', '" + car.Color + "', '" + car.Year + "', '" + car.Doors + "')  ";

            string sqlDataSource = _connectionString.ConnectionString;

            int result = 0;

            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    result = await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                }
            }

            return result > 0;
        }

        public async Task<bool> UpdateCar(Car car)
        {
            string query = @" 
                              UPDATE Cars SET 
                              Make = '" + car.Make + "'" + ", Model = '" + car.Model + "', Color = '" + car.Color + "', Year = '" + car.Year + "', Doors = '" + car.Doors + "'  WHERE Id = '" + car.Id + "' ";

            string sqlDataSource = _connectionString.ConnectionString;

            int result = 0;

            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    result = await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                }
            }

            return result > 0;
        }
    }
}
