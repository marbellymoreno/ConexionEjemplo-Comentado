using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DatosLayer;
using System.Net;
using System.Reflection;


namespace ConexionEjemplo
{
    public partial class Form1 : Form
    {
        // Repositorio de clientes
        CustomerRepository customerRepository = new CustomerRepository();
       

        public Form1()
        {
            // Inicializa el formulario
            InitializeComponent();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            // Se obtienen todos los clientes
            var Customers = customerRepository.ObtenerTodos();
            // Muestra los clientes en el DataGrid
            dataGrid.DataSource = Customers;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Buscar cliente por ID
            var cliente = customerRepository.ObtenerPorID(txtBuscar.Text);
            // Muestra el ID del cliente
            tboxCustomerID.Text = cliente.CustomerID;
            // Muestra el nombre de la compañía
            tboxCompanyName.Text = cliente.CompanyName;
            // Muestra el nombre del contacto
            tboxContacName.Text = cliente.ContactName;
            // Muestra el título del contacto
            tboxContactTitle.Text= cliente.ContactTitle;
            // Muestra la dirección
            tboxAddress.Text = cliente.Address;
            // Muestra la ciudad
            tboxCity.Text = cliente.City;


        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            var resultado = 0;
      ;

            // Obtiene un nuevo cliente
            var nuevoCliente = ObtenerNuevoCliente();

            // Verifica campos nulos
            if (validarCampoNull(nuevoCliente) == false)
            {
                // Inserta cliente
                resultado = customerRepository.InsertarCliente(nuevoCliente);
                // Muestra mensaje de guardado
                MessageBox.Show("Guardado" + "Filas modificadas = " + resultado);
            }
            else {
                // Muestra mensaje de error
                MessageBox.Show("Debe completar los campos por favor");
            }
        }

        // si encuentra un null enviara true de lo caontrario false
        public Boolean validarCampoNull(Object objeto) {

            foreach (PropertyInfo property in objeto.GetType().GetProperties()) {
                object value = property.GetValue(objeto, null);
                if ((string)value == "") {
                    // Retorna true si encuentra campos nulos
                    return true;
                }
            }
            // Retorna false si no hay campos nulos
            return false;
        }

        private void btModificar_Click(object sender, EventArgs e)
        {
            var actualizarCliente = ObtenerNuevoCliente();
            int actualizadas = customerRepository.ActualizarCliente(actualizarCliente);
            // Muestra mensaje de actualización
            MessageBox.Show($"Filas actualizadas = {actualizadas}");
        }

        private Customers ObtenerNuevoCliente() {

            var nuevoCliente = new Customers
            {
                CustomerID = tboxCustomerID.Text,
                CompanyName = tboxCompanyName.Text,
                ContactName = tboxContacName.Text,
                ContactTitle = tboxContactTitle.Text,
                Address = tboxAddress.Text,
                City = tboxCity.Text
            };

            // Retorna un nuevo cliente
            return nuevoCliente; 
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Elimina cliente por ID
            int elimindas = customerRepository.EliminarCliente(tboxCustomerID.Text);
            // Muestra mensaje de eliminación
            MessageBox.Show("Filas eliminadas = " + elimindas);
        }
    }
}
