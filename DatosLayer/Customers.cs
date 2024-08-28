using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosLayer
{
    // Clase para representar un cliente
    public class Customers
    {
        // Identificador único del cliente
        public string CustomerID { get; set; }

        // Nombre de la compañía del cliente
        public string CompanyName { get; set; }

        // Nombre de la persona de contacto del cliente
        public string ContactName { get; set; }

        // Título del contacto (cargo) del cliente
        public string ContactTitle { get; set; }

        // Dirección de la compañía del cliente
        public string Address { get; set; }

        // Ciudad donde se encuentra la compañía del cliente
        public string City { get; set; }

        // Región o estado donde se encuentra la compañía del cliente
        public string Region { get; set; }

        // Código postal de la dirección del cliente
        public string PostalCode { get; set; }

        // País donde se encuentra la compañía del cliente
        public string Country { get; set; }

        // Número de teléfono del cliente
        public string Phone { get; set; }

        // Número de fax del cliente
        public string Fax { get; set; }
    }
}
