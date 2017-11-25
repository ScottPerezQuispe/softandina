using Sistareo.datos.Proceso;
using Sistareo.entidades.Proceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Sistareo.logica.Proceso
{
    public class RetoqueProductoDetalleLG
    {
        public bool InsertarRetoqueProductoDetalle(RetoqueProductoDetalle oRetoque)
        {
            bool resul=false;
            TimeSpan TotalHoras = new TimeSpan();
            RetoqueProducto oRetoqueProducto = new RetoqueProducto();

            using (TransactionScope trans = new TransactionScope())
            {
                resul = new  RetoqueProductoDetalleDA().InsertarRetoqueProductoDetalle(oRetoque);
                if (resul)
                {

                     var lista = new  RetoqueProductoDetalleLG().ListarPorIdRetoqueDetalle(oRetoque.IdRetoqueProducto).ToList();

                    foreach (var item in lista)
                    {
                        TotalHoras = TotalHoras + item.TotalHoras;
                    }
                    oRetoqueProducto.IdRetoqueProducto = oRetoque.IdRetoqueProducto;
                    oRetoqueProducto.TotalDetalleRetoqueProducto = TotalHoras.ToString() ;
                    oRetoqueProducto.UsuarioModificacion = oRetoque.UsuarioCreacion;

                    resul = new RetoqueProductoLG().ActualizarRetoqueProductoTotal(oRetoqueProducto);

                    if (!resul)
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
                trans.Complete();
                return (resul);
            }
            //new RetoqueProductoDetalleLG().ListarPorIdRetoqueDetalle(IdRetoqueProducto).ToList();
            //return new RetoqueProductoDetalleDA().InsertarRetoqueProductoDetalle(oRetoque);
        }
        public bool ActualizarRetoqueProductoDetalle(RetoqueProductoDetalle oRetoque)
        {
            bool resul = false;
            TimeSpan TotalHoras = new TimeSpan();
            RetoqueProducto oRetoqueProducto = new RetoqueProducto();

            using (TransactionScope trans = new TransactionScope())
            {
                resul = new RetoqueProductoDetalleDA().ActualizarRetoqueProductoDetalle(oRetoque);
                if (resul)
                {

                    var lista = new RetoqueProductoDetalleLG().ListarPorIdRetoqueDetalle(oRetoque.IdRetoqueProducto).ToList();

                    foreach (var item in lista)
                    {
                        TotalHoras = TotalHoras + item.TotalHoras;
                    }
                    oRetoqueProducto.IdRetoqueProducto = oRetoque.IdRetoqueProducto;
                    oRetoqueProducto.TotalDetalleRetoqueProducto = TotalHoras.ToString();
                    oRetoqueProducto.UsuarioModificacion = oRetoque.UsuarioModificacion;

                    resul = new RetoqueProductoLG().ActualizarRetoqueProductoTotal(oRetoqueProducto);

                    if (!resul)
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
                trans.Complete();
                return (resul);
            }
            // return new RetoqueProductoDetalleDA().ActualizarRetoqueProductoDetalle(oRetoque);
        }
        public bool EliminarRetoqueProductoDetalle(int IdRetoqueProducto, string UsuarioModificacion)
        {
            return new RetoqueProductoDetalleDA().EliminarRetoqueProductoDetalle(IdRetoqueProducto, UsuarioModificacion);
        }
        public RetoqueProductoDetalle ObtenerPorIdRetoqueProductoDetalle(int IdRetoqueProductoDetalle)
        {
            return new RetoqueProductoDetalleDA().ObtenerPorIdRetoqueProductoDetalle(IdRetoqueProductoDetalle);
        }
        public List<RetoqueProductoDetalle> ListarPorIdRetoqueDetalle(int IdRetoqueProducto)
        {
            return new RetoqueProductoDetalleDA().ListarPorIdRetoqueDetalle(IdRetoqueProducto);
        }
    }
}
