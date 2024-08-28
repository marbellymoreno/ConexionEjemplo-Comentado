using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Para acceder a la configuración del archivo app.config o web.config
using System.Configuration; 
using System.Xml.Linq;
// Para trabajar con conexiones SQL
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace DatosLayer
{
    public class DataBase
    {
        // Propiedad estática que devuelve la cadena de conexión a la base de datos
        public static string ConnectionString {
            get
            {
                // Obtiene la cadena de conexión "NWConnection" del archivo de configuración
                string CadenaConexion = ConfigurationManager
                    .ConnectionStrings["NWConnection"]
                    .ConnectionString;

                // Crea un objeto SqlConnectionStringBuilder para construir la cadena de conexión
                SqlConnectionStringBuilder conexionBuilder = 
                    new SqlConnectionStringBuilder(CadenaConexion);

                // Establece el nombre de la aplicación si está definido
                conexionBuilder.ApplicationName = 
                    ApplicationName ?? conexionBuilder.ApplicationName;

                // Establece el tiempo de espera de la conexión si es mayor que 0
                conexionBuilder.ConnectTimeout = ( ConnectionTimeout > 0 ) 
                    ? ConnectionTimeout : conexionBuilder.ConnectTimeout;

                // Devuelve la cadena de conexión completa
                return conexionBuilder.ToString();
            }

        }
        // Propiedad estática para establecer el tiempo de espera de la conexión
        public static int ConnectionTimeout { get; set; }

        // Propiedad estática para establecer el nombre de la aplicación
        public static string ApplicationName { get; set; }

        // Método estático para obtener una conexión SQL abierta
        public static SqlConnection GetSqlConnection()
        {
            // Crea una nueva conexión SQL usando la cadena de conexión configurada
            SqlConnection conexion = new SqlConnection(ConnectionString);

            // Abre la conexión a la base de datos
            conexion.Open();

            // Devuelve la conexión abierta
            return conexion;
            
        } 
    }
}
