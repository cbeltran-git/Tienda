using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Tienda
{
    public partial class producto : System.Web.UI.Page
    {
        string strConexion = "Data Source =192.168.1.23; Initial Catalog =Tienda; User ID =cbeltran; Password=root";
        string idProducto = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            idProducto = Request.QueryString["id"];
            if (string.IsNullOrEmpty(idProducto))
            {
                Response.Redirect("productos.aspx");
            }

            if (!Page.IsPostBack)
            {
                cargarDatos();
            }
        }

        void cargarDatos()
        {
            using (var conexion = new SqlConnection(strConexion))
            {
                conexion.Open();
                string sql = "Select * From Producto Where IdProducto=@id";
                using (var command = new SqlCommand(sql, conexion))
                {
                    command.Parameters.AddWithValue("@id", idProducto);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader != null && reader.HasRows)
                    {
                        reader.Read();
                        txtNombre.Text = reader["Nombre"].ToString();
                        txtMarca.Text = reader["Marca"].ToString();
                        txtPrecio.Text = reader["Precio"].ToString();
                        txtObservacion.Text = reader["Observacion"].ToString();
                    }
                    else
                    {
                        string script = "alert('El producto con el id " + idProducto + " no existe.');";
                        script += "window.location='productos.aspx';";
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "mensaje", script, true);
                    }
                }
            }
        }

        protected void Actualizar(object sender, EventArgs e)
        {

            string nombre = txtNombre.Text;
            string marca = txtMarca.Text;
            string precio = txtPrecio.Text; 
            string observacion = txtObservacion.Text;
            using (var conexion = new SqlConnection(strConexion))
            {
                conexion.Open();
                var sql = "UPDATE Producto set Nombre=@nombre, Marca=@marca, Precio = @precio, Observacion=@observacion Where idproducto = @id";
                using (var command = new SqlCommand(sql, conexion))
                {
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@marca", marca);
                    command.Parameters.AddWithValue("@precio", precio);
                    command.Parameters.AddWithValue("@observacion", observacion);
                    command.Parameters.AddWithValue("@id", idProducto);
                    int filas = command.ExecuteNonQuery();
                    if(filas > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                "mensaje", "alert('Actualizando datos')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                "mensaje", "alert('No se pudo actualizar')", true);
                    }
                }
            }
            

        }
    }
}