using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace Tienda
{
    public partial class producto_crear : System.Web.UI.Page
    {
        string strConexion = "Data Source =192.168.1.23; Initial Catalog =Tienda; User ID =cbeltran; Password=root";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                cargarDatos();
            }
        }

        void cargarDatos()
        {
            using (var conexion = new SqlConnection(strConexion))
            {
                using (var command = new SqlCommand("select * from Categoria",conexion))
                {
                    conexion.Open();
                    var reader = command.ExecuteReader();
                    if (reader != null && reader.HasRows)
                    {
                        ListItem item;
                       while(reader.Read())
                        {
                            item = new ListItem();
                            item.Text = reader["Nombre"].ToString();
                            item.Value = reader["idCategoria"].ToString();
                            ddlCategoria.Items.Add(item);
                        }                      
                    }
                }
                
            }
        }
        protected void registrar(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string marca = txtMarca.Text;
            string precio = txtPrecio.Text;
            string stock = txtStock.Text;
            string observacion = txtObservacion.Text;
            string foto = "/assets/img/productos/generico.png";
            string categoria = ddlCategoria.SelectedValue;
            string archivoImagen = Path.GetFileName(fuFoto.PostedFile.FileName);
            if (!string.IsNullOrEmpty(archivoImagen))
            {
                foto = "/assets/img/productos/" + archivoImagen;
                fuFoto.SaveAs(Server.MapPath("~/assets/img/productos/") + archivoImagen);
            }
            if (string.IsNullOrEmpty(categoria))
            {
                //Mostrar Mensaje
            }

            using (var conexion = new SqlConnection(strConexion))
            {
                var sql = "Insert Into Producto(Nombre, Marca, Precio, Stock, " +
                        "Observacion, IdCategoria,Foto) " +
                        "Values(@nombre, @marca, @precio, @stock, @observacion, " +
                        "@categoria,@foto)";
                using (var command = new SqlCommand(sql,conexion))
                {
                    

                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@marca", marca);
                    command.Parameters.AddWithValue("@precio", precio);
                    command.Parameters.AddWithValue("@stock", stock);                   
                    command.Parameters.AddWithValue("@observacion", observacion);
                    command.Parameters.AddWithValue("@categoria", categoria);
                    command.Parameters.AddWithValue("@foto", foto);
                    conexion.Open();
                    int filas = command.ExecuteNonQuery();
                    if (filas > 0)
                    {
                        var script = "alert('Producto registrado');" +
                            "window.location='productos.aspx'";
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "mensaje", script, true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "mensaje", "alert(No se ha podido registrar el producto)", true);
                    }
                }
            }
        }
    }
}