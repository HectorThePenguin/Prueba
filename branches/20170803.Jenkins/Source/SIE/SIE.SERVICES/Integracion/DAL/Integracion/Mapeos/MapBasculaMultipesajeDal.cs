using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    public class MapBasculaMultipesajeDAL
    {
        /// <summary>
        /// Mapeo de la informacion devuelta de la consulta
        /// generada en el procedimiento almacenado
        /// BasculaMultipesaje_ObtenerPesaje
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>regresa la la informacion que genero la consulta</returns>
        internal static BasculaMultipesajeInfo ConsultarBasculaMultipesaje(DataSet ds)
        {
            BasculaMultipesajeInfo resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                resultado = (from info in dt.AsEnumerable()
                         select new BasculaMultipesajeInfo
                         {
                             Chofer = info.Field<string>("Chofer"),
                             Placas = info.Field<string>("Placas"),
                             PesoBruto = info.Field<int>("PesoBruto"),
                             PesoTara = info.Field<int>("PesoTara"),
                             Producto = info.Field<string>("Producto"),
                             UsuarioCreacion = info.Field<int>("UsuarioCreacionID"),
                             QuienRecibe = new OperadorInfo() { OperadorID = info.Field<int>("OperadorID") },
                             FechaPesoBruto = info.Field<DateTime?>("FechaPesoBruto"),
                             FechaPesoTara = info.Field<DateTime?>("FechaPesoTara"),
                             FechaCreacion = info.Field<DateTime>("Fecha"),
                             FechaModificacion = info.Field<DateTime?>("FechaModificacion"),
                             Observacion =  info.Field<string>("Observacion"),
                             EnvioSAP = info.Field<bool>("EnvioSAP")
                         }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }

        /// <summary>
        /// Mapeo de la informacion devuelta de la consulta
        /// generada en el procedimiento almacenado
        /// BasculaMultipesaje_Crear
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>regresa la la informacion que genero la consulta</returns>
        internal static long ObtenerFolioDespuesDeRegistrar(DataSet ds)
        {
             long result = 0;
            try
            {
                
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                BasculaMultipesajeInfo resultado = (from info in dt.AsEnumerable()
                             select new BasculaMultipesajeInfo
                             {
                                 FolioMultipesaje = new FolioMultipesajeInfo(){ Folio = info.Field<int>("Folio")}
                             }).FirstOrDefault();

                result = resultado.FolioMultipesaje.Folio;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }

        /// <summary>
        /// Mapeo de la informacion devuelta de la consulta
        ///  generada en el procedimiento almacenado
        ///  BasculaMultipesaje_ObtenerPorFolio
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>regresa la la informacion que genero la consulta</returns>
        internal static FolioMultipesajeInfo ObtenerFolioPorId(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                  var  lista = (from info in dt.AsEnumerable()
                             select new FolioMultipesajeInfo()
                             {
                                 Chofer = info.Field<string>("Chofer"),
                                 Producto = info.Field<string>("Producto"),
                                 Folio = info.Field<long>("Folio")
                             }).First();
                  return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

        }

        /// <summary>
        /// Mapeo de la informacion devuelta de la consulta
        /// generada en el procedimiento almacenado
        /// BasculaMultipesaje_ObtenerFoliosPorPagina
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>regresa la la informacion que genero la consulta</returns>
        internal static ResultadoInfo<FolioMultipesajeInfo> ObtenerPorPaginaCompleto(DataSet ds)
        {
            ResultadoInfo<FolioMultipesajeInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<FolioMultipesajeInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new FolioMultipesajeInfo()
                         {
                             Chofer = info.Field<string>("Chofer"),
                             Producto = info.Field<string>("Producto"),
                             Folio = info.Field<long>("Folio")
                         }).ToList();

                resultado = new ResultadoInfo<FolioMultipesajeInfo>
                {
                    Lista = lista,
                    TotalRegistros =
                        Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }
    }
}
