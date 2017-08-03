using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    public class MapLectorRegistro
    {

        /// <summary>
        /// Obtiene los datos del lector registro
        /// </summary>
        /// <param name="ds">Data set con los registros</param>
        /// <returns></returns>
        public static LectorRegistroInfo ObtenerLectorRegistro(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var resultado =
                    (from info in dt.AsEnumerable()
                     select
                         new LectorRegistroInfo
                         {
                             LectorRegistroID = info.Field<int>("LectorRegistroID"),
                             OrganizacionID = info.Field<int>("OrganizacionID"),
                             Seccion = info.Field<int>("Seccion"),
                             LoteID = info.Field<int>("LoteID"),
                             Fecha = info.Field<DateTime>("Fecha"),
                             CambioFormula = info.Field<bool>("CambioFormula"),
                             Cabezas = info.Field<int>("Cabezas"),
                             EstadoComederoID = info.Field<int>("EstadoComederoID"),
                             CantidadOriginal = info.Field<decimal>("CantidadOriginal"),
                             CantidadPedido = info.Field<decimal>("CantidadPedido")
                         }).First();

                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los datos del detalle de un lector registro
        /// </summary>
        /// <param name="ds">Data set con los datos</param>
        /// <returns>Lista con los detalles de lector registro</returns>
        public static List<LectorRegistroDetalleInfo> ObtenerDetalleLectorRegistro(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var resultado =
                    (from info in dt.AsEnumerable()
                     select
                         new LectorRegistroDetalleInfo()
                         {
                             LectorRegistroID = info.Field<int>("LectorRegistroID"),
                             LectorRegistroDetalleID = info.Field<int>("LectorRegistroDetalleID"),
                             TipoServicioID = info.Field<int>("TipoServicioID"),
                             FormulaIDAnterior = info.Field<int>("FormulaIDAnterior"),
                             FormulaIDProgramada = info.Field<int>("FormulaIDProgramada")
                             
                         }).ToList();

                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los datos del lector registro
        /// </summary>
        /// <param name="ds">Data set con los registros</param>
        /// <returns></returns>
        public static List<LectorRegistroInfo> ObtenerLectorRegistroXML(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];

                var resultado =
                    (from info in dt.AsEnumerable()
                     select
                         new LectorRegistroInfo
                         {
                             LectorRegistroID = info.Field<int>("LectorRegistroID"),
                             OrganizacionID = info.Field<int>("OrganizacionID"),
                             Seccion = info.Field<int>("Seccion"),
                             LoteID = info.Field<int>("LoteID"),
                             Fecha = info.Field<DateTime>("Fecha"),
                             CambioFormula = info.Field<bool>("CambioFormula"),
                             Cabezas = info.Field<int>("Cabezas"),
                             EstadoComederoID = info.Field<int>("EstadoComederoID"),
                             CantidadOriginal = info.Field<decimal>("CantidadOriginal"),
                             CantidadPedido = info.Field<decimal>("CantidadPedido")
                         }).ToList();

                var detalle =
                    (from info in dtDetalle.AsEnumerable()
                     select
                         new LectorRegistroDetalleInfo()
                         {
                             LectorRegistroID = info.Field<int>("LectorRegistroID"),
                             LectorRegistroDetalleID = info.Field<int>("LectorRegistroDetalleID"),
                             TipoServicioID = info.Field<int>("TipoServicioID"),
                             FormulaIDAnterior = info.Field<int>("FormulaIDAnterior"),
                             FormulaIDProgramada = info.Field<int>("FormulaIDProgramada")

                         }).ToList();

                foreach (var lector in resultado)
                {
                    lector.DetalleLector = detalle.Where(det => det.LectorRegistroID == lector.LectorRegistroID).ToList();
                }

                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
