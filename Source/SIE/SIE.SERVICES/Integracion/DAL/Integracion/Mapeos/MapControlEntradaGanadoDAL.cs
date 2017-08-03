using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapControlEntradaGanadoDAL
    {
        /// <summary>
        ///   
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>

        internal static List<ControlEntradaGanadoInfo> ObtenerControlEntradaGanado(DataSet ds)
        {
            var controlEntradaGanadoInfo = new List<ControlEntradaGanadoInfo>();
            var listaControlEntradaGanadoDetalle = new List<ControlEntradaGanadoDetalleInfo>();
            
            try
            {
                Logger.Info();
                DataTable dtControlEntradaGanado = ds.Tables[ConstantesDAL.DtControlEntradaGanado];
                DataTable dtControlEntradaGanadoDetalle = ds.Tables[ConstantesDAL.DtControlEntradaGanadoDetalle];

                listaControlEntradaGanadoDetalle =
                    (from info in dtControlEntradaGanadoDetalle.AsEnumerable()
                     select new ControlEntradaGanadoDetalleInfo
                     {
                         ControlEntradaGanadoDetalleID = info.Field<long>("ControlEntradaGanadoDetalleID"),
                         ControlEntradaGanadoID = info.Field<long>("ControlEntradaGanadoID"),
                         Costo = new CostoInfo
                         {
                             CostoID = info.Field<int>("CostoID"),
                         },
                         Importe = info.Field<decimal>("Importe"),
                         Activo = info.Field<bool>("Activo").BoolAEnum(),
                         UsuarioCreacionID = info.Field<int>("UsuarioCreacionID"),
                         FechaCreacion = info.Field<DateTime>("FechaCreacion")
                     }).ToList();

                controlEntradaGanadoInfo =
                    (from info in dtControlEntradaGanado.AsEnumerable()
                     select new ControlEntradaGanadoInfo
                     {
                         ControlEntradaGandoID = info.Field<long>("ControlEntradaGanadoID"),
                         EntradaGanado = new EntradaGanadoInfo { EntradaGanadoID = info.Field<int>("EntradaGanadoID") },
                         Animal = new AnimalInfo { AnimalID = info.Field<long>("AnimalID"),PesoCompra = info.Field<int>("PesoCompra") },
                         
                         Activo = info.Field<bool>("Activo").BoolAEnum(),
                         UsuarioCreacionID = info.Field<int>("UsuarioCreacionID"),
                         FechaCreacion = info.Field<DateTime>("FechaCreacion")
                     }).ToList();

                if (controlEntradaGanadoInfo == null) 
                {
                    controlEntradaGanadoInfo = new List<ControlEntradaGanadoInfo>();
                }

                if (controlEntradaGanadoInfo.Count > 0)
                {
                    foreach(ControlEntradaGanadoInfo controlEntrada in controlEntradaGanadoInfo)
                    {
                        var detalle = listaControlEntradaGanadoDetalle.Where(
                        ControlEntradaGanadoDetalle => ControlEntradaGanadoDetalle.ControlEntradaGanadoID == controlEntrada.ControlEntradaGandoID).ToList();
                        //controlEntrada.ListaControlEntradaGanadoDetalle = new List<ControlEntradaGanadoDetalleInfo>();
                        controlEntrada.ListaControlEntradaGanadoDetalle = (detalle);
                    }
                }



                return controlEntradaGanadoInfo;

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
