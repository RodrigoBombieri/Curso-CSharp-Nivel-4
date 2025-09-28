using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace negocio
{
    public class TipoEdicionNegocio
    {
        public List<TipoEdicion> listar()
        {
            List<TipoEdicion> lista = new List<TipoEdicion>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select Id, Descripcion From TIPOSEDICION");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    TipoEdicion aux = new TipoEdicion();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregar(TipoEdicion nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Insert into TIPOSEDICION (Descripcion) values (@Descripcion)");
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificar(TipoEdicion tipoEdicion)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Update TIPOSEDICION set Descripcion = @Descripcion where Id = @Id");
                datos.setearParametro("@Descripcion", tipoEdicion.Descripcion);
                datos.setearParametro("@Id", tipoEdicion.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Delete from TIPOSEDICION where Id = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
