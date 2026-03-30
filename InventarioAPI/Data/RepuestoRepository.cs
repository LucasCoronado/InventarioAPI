using InventarioAPI.Models;
using Microsoft.Data.SqlClient;
using System.Collections;
using System.Data;

namespace InventarioAPI.Data
{
    public class RepuestoRepository
    {

        private readonly string _cadenaConexion;

        public RepuestoRepository(IConfiguration configuration)
        {
            _cadenaConexion = configuration.GetConnectionString("CadenaSQL") ?? "";
        }

        public async Task<List<Repuesto>> ObtenerTodos()
        {
            var lista = new List<Repuesto>();

            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "SELECT Id, Nombre, Marca, Precio, Stock FROM Repuestos";
                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    await conexion.OpenAsync(); 

                    using (SqlDataReader reader = await comando.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync()) {
                            lista.Add(new Repuesto
                            {
                                Id = reader.GetInt32("Id"),
                                Nombre = reader.GetString("Nombre"),
                                Marca = reader.GetString("Marca"),
                                Precio = reader.GetDecimal("Precio"),
                                Stock = reader.GetInt32("Stock")
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public async Task<Repuesto?> ObtenerPorId(int id)
        {
            Repuesto? repuesto = null;
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "SELECT Id, Nombre, Marca, Precio, Stock FROM Repuestos WHERE Id = @Id";
                
                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@Id", id);
                    await conexion.OpenAsync();

                    using (SqlDataReader reader = await comando.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            repuesto = new Repuesto
                            {
                                Id = reader.GetInt32("Id"),
                                Nombre = reader.GetString("Nombre"),
                                Marca = reader.GetString("Marca"),
                                Precio = reader.GetDecimal("Precio"),
                                Stock = reader.GetInt32("Stock")
                            };
                        }
                    }
                }
            }
            return repuesto;            
        }

        public async Task<int> Insertar(Repuesto repuestoNuevo)
        {
            using(SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = @"INSERT INTO Repuestos (Nombre, Marca, Precio, Stock)
                                 VALUES (@Nombre, @Marca, @Precio, @Stock);
                                 SELECT SCOPE_IDENTITY();";
                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre", repuestoNuevo.Nombre);
                    comando.Parameters.AddWithValue("@Marca", repuestoNuevo.Marca);
                    comando.Parameters.AddWithValue("@Precio", repuestoNuevo.Precio);
                    comando.Parameters.AddWithValue("@Stock", repuestoNuevo.Stock);

                    await conexion.OpenAsync();

                    var resultado = await comando.ExecuteScalarAsync();

                    return Convert.ToInt32(resultado);
                }
            }
        }

        public async Task<bool> Actualizar(Repuesto repuesto)
        {
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = @"UPDATE Repuestos 
                                 SET Nombre = @Nombre, Marca = @Marca, Precio = @Precio, Stock = @Stock
                                 WHERE Id = @Id";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@Id", repuesto.Id);
                    comando.Parameters.AddWithValue("@Nombre", repuesto.Nombre);
                    comando.Parameters.AddWithValue("@Marca", repuesto.Marca);
                    comando.Parameters.AddWithValue("@Precio", repuesto.Precio);
                    comando.Parameters.AddWithValue("@Stock", repuesto.Stock);

                    await conexion.OpenAsync();

                    var filasAfectadas = await comando.ExecuteNonQueryAsync();

                    return filasAfectadas > 0;
                }
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                // El WHERE Id = @Id es VITAL. Sin eso, vaciás la tabla.
                string query = "DELETE FROM Repuestos WHERE Id = @Id";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@Id", id);

                    await conexion.OpenAsync();
                    int filasAfectadas = await comando.ExecuteNonQueryAsync();

                    // Si filasAfectadas es 1, significa que el registro existía y se borró.
                    return filasAfectadas > 0;
                }
            }
        }


    }
}
