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

        public async Task<List<Repuesto>> ObtenerTodos(string? busqueda = null)
        {
            var lista = new List<Repuesto>();

            try
            {
                using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
                {
                    string query = "SELECT Id, Nombre, Marca, Precio, Stock FROM Repuestos WHERE Activo = 1";
                    if (!string.IsNullOrEmpty(busqueda))
                    {
                        query += " AND (Nombre LIKE @busqueda OR Marca LIKE @busqueda)";
                    }
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        if (!string.IsNullOrEmpty(busqueda))
                        {
                            comando.Parameters.AddWithValue("@busqueda", $"%{busqueda}%");
                        }
                        await conexion.OpenAsync();

                        using (SqlDataReader reader = await comando.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
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
            }
            catch(SqlException ex)
            {
                Console.WriteLine($"Error de base de datos: {ex.Message}");
                throw new Exception("No pudimos conectarnos a la base de datos de repuestos.");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
                throw;
            }
            return lista;
        }

        public async Task<Repuesto?> ObtenerPorId(int id)
        {
            Repuesto? repuesto = null;
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "SELECT Id, Nombre, Marca, Precio, Stock FROM Repuestos WHERE Id = @Id AND Activo =1";
                
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
                                 WHERE Id = @Id AND Activo = 1";

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
                string query = "UPDATE Repuestos SET Activo = 0 WHERE Id = @id";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@Id", id);
                    await conexion.OpenAsync();
                    int filasAfectadas = await comando.ExecuteNonQueryAsync();

                    return filasAfectadas > 0;
                }
            }
        }


    }
}
