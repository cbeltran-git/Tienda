using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Tienda
{
    public partial class productos : System.Web.UI.Page
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
                
                using (var command = new SqlCommand("Select IdProducto AS Id, Nombre, Marca,Foto, Precio AS PrecioProducto from Producto",conexion))
                {
                    var ds = new DataSet();
                    var da = new SqlDataAdapter(command);
                    da.Fill(ds);

                    gvDatos.DataSource = ds;
                    gvDatos.DataBind();

                    rptDatos.DataSource = ds;
                    rptDatos.DataBind();

                }
            }

            using (var conexion = new SqlConnection(strConexion))
            {
                using (var command = new SqlCommand("select * from Producto", conexion))
                {
                    conexion.Open();
                    var reader = command.ExecuteReader();
                    if (reader != null && reader.HasRows)
                    {
                        ListItem item;
                        while (reader.Read())
                        {
                            item = new ListItem();
                            item.Text = reader["Marca"].ToString();
                            item.Value = reader["idProducto"].ToString();
                            ddlFiltrarMarca.Items.Add(item);
                        }
                    }
                }

            }

            using (var conexion = new SqlConnection(strConexion))
            {
                using (var command = new SqlCommand("select * from Categoria", conexion))
                {
                    conexion.Open();
                    var reader = command.ExecuteReader();
                    if (reader != null && reader.HasRows)
                    {
                        ListItem item;
                        while (reader.Read())
                        {
                            item = new ListItem();
                            item.Text = reader["Nombre"].ToString();
                            item.Value = reader["idCategoria"].ToString();
                            ddlFiltrarCategoria.Items.Add(item);
                        }
                    }
                }

            }
        }

        protected void filtro(object sender, EventArgs e)
        {

            string idCategoria = ddlFiltrarCategoria.SelectedIndex.ToString();
            //string marca = ddlFiltrarMarca.Text();

                using (var conexion = new SqlConnection(strConexion))
                {

                    using (var command = new SqlCommand("Select IdProducto AS Id, Nombre, Marca,Foto, Precio AS PrecioProducto from Producto where idCategoria=@id", conexion))
                    {
                        command.Parameters.AddWithValue("@id", idCategoria);
                        var ds = new DataSet();
                        var da = new SqlDataAdapter(command);
                        da.Fill(ds);

                        gvDatos.DataSource = ds;
                        gvDatos.DataBind();

                        rptDatos.DataSource = ds;
                        rptDatos.DataBind();

                    }
                }

            //using (var conexion = new SqlConnection(strConexion))
            //{

            //    using (var command = new SqlCommand("Select IdProducto AS Id, Nombre, Marca,Foto, Precio AS PrecioProducto from Producto where Marca=@marca", conexion))
            //    {
            //        command.Parameters.AddWithValue("@marca", marca);
            //        var ds = new DataSet();
            //        var da = new SqlDataAdapter(command);
            //        da.Fill(ds);

            //        gvDatos.DataSource = ds;
            //        gvDatos.DataBind();

            //        rptDatos.DataSource = ds;
            //        rptDatos.DataBind();

            //    }
            //}
        }

        


    }
}