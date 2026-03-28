using InventarioAPI.Models;
using System.Data;
using Microsoft.Data.SqlClient;

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
    }
}
