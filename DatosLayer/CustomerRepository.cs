using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DatosLayer
{
    // Método para obtener todos los clientes
    public class CustomerRepository
    {
        
        public List<Customers> ObtenerTodos() {
            // Abre la conexión con la base de datos
            using (var conexion= DataBase.GetSqlConnection()) {
                // Consulta SQL para seleccionar todos los campos de la tabla Customers
                String selectFrom = "";
                selectFrom = selectFrom + "SELECT [CustomerID] " + "\n";
                selectFrom = selectFrom + "      ,[CompanyName] " + "\n";
                selectFrom = selectFrom + "      ,[ContactName] " + "\n";
                selectFrom = selectFrom + "      ,[ContactTitle] " + "\n";
                selectFrom = selectFrom + "      ,[Address] " + "\n";
                selectFrom = selectFrom + "      ,[City] " + "\n";
                selectFrom = selectFrom + "      ,[Region] " + "\n";
                selectFrom = selectFrom + "      ,[PostalCode] " + "\n";
                selectFrom = selectFrom + "      ,[Country] " + "\n";
                selectFrom = selectFrom + "      ,[Phone] " + "\n";
                selectFrom = selectFrom + "      ,[Fax] " + "\n";
                selectFrom = selectFrom + "  FROM [dbo].[Customers]";

                // Ejecuta la consulta SQL
                using (SqlCommand comando = new SqlCommand(selectFrom, conexion)) {
                    // Lee los resultados de la consulta
                    SqlDataReader reader = comando.ExecuteReader();
                    // Lista para almacenar los clientes
                    List<Customers> Customers = new List<Customers>();

                    // Recorre cada fila del resultado
                    while (reader.Read())
                    {
                        // Convierte la fila en un objeto Customers
                        var customers = LeerDelDataReader(reader);
                        // Añade el objeto a la lista
                        Customers.Add(customers);
                    }
                    // Retorna la lista de clientes
                    return Customers;
                }
            }
           
        }

        // Método para obtener un cliente por ID
        public Customers ObtenerPorID(string id) {
            // Abre la conexión con la base de datos
            using (var conexion = DataBase.GetSqlConnection()) {

                // Consulta SQL para seleccionar un cliente específico por ID
                String selectForID = "";
                selectForID = selectForID + "SELECT [CustomerID] " + "\n";
                selectForID = selectForID + "      ,[CompanyName] " + "\n";
                selectForID = selectForID + "      ,[ContactName] " + "\n";
                selectForID = selectForID + "      ,[ContactTitle] " + "\n";
                selectForID = selectForID + "      ,[Address] " + "\n";
                selectForID = selectForID + "      ,[City] " + "\n";
                selectForID = selectForID + "      ,[Region] " + "\n";
                selectForID = selectForID + "      ,[PostalCode] " + "\n";
                selectForID = selectForID + "      ,[Country] " + "\n";
                selectForID = selectForID + "      ,[Phone] " + "\n";
                selectForID = selectForID + "      ,[Fax] " + "\n";
                selectForID = selectForID + "  FROM [dbo].[Customers] " + "\n";
                // Condición para seleccionar por ID
                selectForID = selectForID + $"  Where CustomerID = @customerId";

                using (SqlCommand comando = new SqlCommand(selectForID, conexion))
                {
                    // Añade el parámetro de ID a la consulta
                    comando.Parameters.AddWithValue("customerId", id);

                    // Ejecuta la consulta y obtiene el lector de datos
                    var reader = comando.ExecuteReader();
                    Customers customers = null;
                    //validadmos 
                    if (reader.Read()) {
                        customers = LeerDelDataReader(reader);
                    }
                    // Retorna el cliente
                    return customers; 
                }

            }
        }

        // Método para convertir una fila del SqlDataReader en un objeto Customers
        public Customers LeerDelDataReader( SqlDataReader reader) {

            // Crea un nuevo objeto Customers
            Customers customers = new Customers(); 
            customers.CustomerID = reader["CustomerID"] == DBNull.Value ? " " : (String)reader["CustomerID"];
            customers.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (String)reader["CompanyName"];
            customers.ContactName = reader["ContactName"] == DBNull.Value ? "" : (String)reader["ContactName"];
            customers.ContactTitle = reader["ContactTitle"] == DBNull.Value ? "" : (String)reader["ContactTitle"];
            customers.Address = reader["Address"] == DBNull.Value ? "" : (String)reader["Address"];
            customers.City = reader["City"] == DBNull.Value ? "" : (String)reader["City"];
            customers.Region = reader["Region"] == DBNull.Value ? "" : (String)reader["Region"];
            customers.PostalCode = reader["PostalCode"] == DBNull.Value ? "" : (String)reader["PostalCode"];
            customers.Country = reader["Country"] == DBNull.Value ? "" : (String)reader["Country"];
            customers.Phone = reader["Phone"] == DBNull.Value ? "" : (String)reader["Phone"];
            customers.Fax = reader["Fax"] == DBNull.Value ? "" : (String)reader["Fax"];
            // Retorna el objeto Customers
            return customers;
        }
        //-------------

        // Método para insertar un nuevo cliente en la base de datos
        public int InsertarCliente(Customers customer) {
            // Abre la conexión con la base de datos
            using (var conexion = DataBase.GetSqlConnection()) {
                // Consulta SQL para insertar un nuevo cliente
                String insertInto = "";
                insertInto = insertInto + "INSERT INTO [dbo].[Customers] " + "\n";
                insertInto = insertInto + "           ([CustomerID] " + "\n";
                insertInto = insertInto + "           ,[CompanyName] " + "\n";
                insertInto = insertInto + "           ,[ContactName] " + "\n";
                insertInto = insertInto + "           ,[ContactTitle] " + "\n";
                insertInto = insertInto + "           ,[Address] " + "\n";
                insertInto = insertInto + "           ,[City]) " + "\n";
                insertInto = insertInto + "     VALUES " + "\n";
                insertInto = insertInto + "           (@CustomerID " + "\n";
                insertInto = insertInto + "           ,@CompanyName " + "\n";
                insertInto = insertInto + "           ,@ContactName " + "\n";
                insertInto = insertInto + "           ,@ContactTitle " + "\n";
                insertInto = insertInto + "           ,@Address " + "\n";
                insertInto = insertInto + "           ,@City)";

                // Ejecuta la consulta SQL
                using (var comando = new SqlCommand( insertInto,conexion )) {
                    // Añade parámetros y ejecuta la consulta
                    int insertados = parametrosCliente(customer, comando);
                    // Retorna el número de filas insertadas
                    return insertados;
                }

            }
        }
        //-------------

        // Método para actualizar un cliente existente en la base de datos
        public int ActualizarCliente(Customers customer) {
            // Abre la conexión con la base de datos
            using (var conexion = DataBase.GetSqlConnection()) {
                // Consulta SQL para actualizar un cliente por ID
                String ActualizarCustomerPorID = "";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "UPDATE [dbo].[Customers] " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "   SET [CustomerID] = @CustomerID " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[CompanyName] = @CompanyName " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[ContactName] = @ContactName " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[ContactTitle] = @ContactTitle " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[Address] = @Address " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[City] = @City " + "\n";
                // Condición de actualización por ID
                ActualizarCustomerPorID = ActualizarCustomerPorID + " WHERE CustomerID= @CustomerID";
                using (var comando = new SqlCommand(ActualizarCustomerPorID, conexion)) {

                    // Añade parámetros y ejecuta la consulta
                    int actualizados = parametrosCliente(customer, comando);

                    // Retorna el número de filas actualizadas
                    return actualizados;
                }
            } 
        }

        // Método para añadir parámetros a un comando SQL y ejecutarlo
        public int parametrosCliente(Customers customer, SqlCommand comando) {
            comando.Parameters.AddWithValue("CustomerID", customer.CustomerID);
            comando.Parameters.AddWithValue("CompanyName", customer.CompanyName);
            comando.Parameters.AddWithValue("ContactName", customer.ContactName);
            comando.Parameters.AddWithValue("ContactTitle", customer.ContactName);
            comando.Parameters.AddWithValue("Address", customer.Address);
            comando.Parameters.AddWithValue("City", customer.City);
            var insertados = comando.ExecuteNonQuery();
            return insertados;
        }

        // Método para eliminar un cliente de la base de datos
        public int EliminarCliente(string id) {
            // Abre la conexión con la base de datos
            using (var conexion = DataBase.GetSqlConnection() ){
                // Consulta SQL para eliminar un cliente por ID
                String EliminarCliente = "";
                EliminarCliente = EliminarCliente + "DELETE FROM [dbo].[Customers] " + "\n";
                EliminarCliente = EliminarCliente + "      WHERE CustomerID = @CustomerID";
                using (SqlCommand comando = new SqlCommand(EliminarCliente, conexion)) {
                    // Añade el parámetro de ID a la consulta
                    comando.Parameters.AddWithValue("@CustomerID", id);
                    // Ejecuta la consulta SQL
                    int elimindos = comando.ExecuteNonQuery();
                    // Retorna el número de filas eliminadas
                    return elimindos;
                }
            }
        }
    }
}
