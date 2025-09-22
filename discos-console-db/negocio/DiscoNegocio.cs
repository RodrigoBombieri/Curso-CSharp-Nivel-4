using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using dominio;

namespace negocio
{
    public class DiscoNegocio
    {
        public List<Disco> listar()
        {
            List<Disco> lista = new List<Disco>();
            AccesoDatos datos = new AccesoDatos();

            try
            {               
                datos.setearConsulta("select D.Id, Titulo, FechaLanzamiento, CantidadCanciones, " +
                    "UrlImagenTapa,IdEstilo, E.Descripcion Estilo, IdTipoEdicion, " +
                    "TE.Descripcion TipoEdicion " +
                    "From DISCOS D " +
                    "inner join ESTILOS E On D.IdEstilo = E.Id " +
                    "inner join TIPOSEDICION TE on D.IdTipoEdicion = TE.Id");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Disco aux = new Disco();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Titulo = (string)datos.Lector["Titulo"];
                    aux.FechaLanzamiento = (DateTime)datos.Lector["FechaLanzamiento"];
                    aux.CantidadCanciones = (int)datos.Lector["CantidadCanciones"];
                    if (!(datos.Lector["UrlImagenTapa"] is DBNull))
                        aux.UrlTapa = (string)datos.Lector["UrlImagenTapa"];

                    aux.TipoEdicion = new TipoEdicion();
                    aux.TipoEdicion.Id = (int)datos.Lector["IdTipoEdicion"];
                    aux.TipoEdicion.Descripcion = (string)datos.Lector["TipoEdicion"];
                    aux.Estilo = new Estilo();
                    aux.Estilo.Id = (int)datos.Lector["IdEstilo"];
                    aux.Estilo.Descripcion = (string)datos.Lector["Estilo"];


                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        

        public void agregar(Disco nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Insert into DISCOS " +
                    "(Titulo, FechaLanzamiento, CantidadCanciones, UrlImagenTapa, IdEstilo, IdTipoEdicion)" +
                    "values(@titulo, @fechaLanzamiento, @cantidadCanciones, @urlImagenTapa, @idEstilo, @idTipoEdicion)");
                datos.setearParametro("@titulo", nuevo.Titulo);
                datos.setearParametro("@fechaLanzamiento", nuevo.FechaLanzamiento);
                datos.setearParametro("@cantidadCanciones", nuevo.CantidadCanciones);
                datos.setearParametro("@urlImagenTapa", nuevo.UrlTapa);
                datos.setearParametro("@idEstilo", nuevo.Estilo.Id);
                datos.setearParametro("@idTipoEdicion", nuevo.TipoEdicion.Id);
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

       

        public void modificar(Disco disco)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update DISCOS set Titulo = @titulo, FechaLanzamiento = @fechaLanzamiento, " +
                    "CantidadCanciones = @cantidadCanciones, UrlImagenTapa = @urlImagen, " +
                    "IdEstilo = @idEstilo, IdTipoEdicion = @idTipoEdicion Where Id = @id");
                datos.setearParametro("@titulo", disco.Titulo);
                datos.setearParametro("@fechaLanzamiento", disco.FechaLanzamiento);
                datos.setearParametro("@cantidadCanciones", disco.CantidadCanciones);
                datos.setearParametro("@urlImagen", disco.UrlTapa);
                datos.setearParametro("@idEstilo", disco.Estilo.Id);
                datos.setearParametro("@idTipoEdicion", disco.TipoEdicion.Id);
                datos.setearParametro("@id", disco.Id);

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
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from DISCOS where id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
