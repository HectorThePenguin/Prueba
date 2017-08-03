using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    public class MapProduccionDiariaDetalleDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<ProduccionDiariaDetalleInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ProduccionDiariaDetalleInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ProduccionDiariaDetalleInfo
                             {
								ProduccionDiariaDetalleID = info.Field<int>("ProduccionDiariaDetalleID"),
								ProduccionDiaria = new ProduccionDiariaInfo { ProduccionDiariaID = info.Field<int>("ProduccionDiariaID")},
								Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), Descripcion = info.Field<string>("Producto") },
								PesajeMateriaPrima = new PesajeMateriaPrimaInfo { PesajeMateriaPrimaID = info.Field<int>("PesajeMateriaPrimaID")},
								EspecificacionForraje = info.Field<int>("EspecificacionForraje"),
								HoraInicial = info.Field<string>("HoraInicial"),
								HoraFinal = info.Field<string>("HoraFinal"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<ProduccionDiariaDetalleInfo>
                        {
                            Lista = lista,
                            TotalRegistros = totalRegistros
                        };
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<ProduccionDiariaDetalleInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ProduccionDiariaDetalleInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ProduccionDiariaDetalleInfo
                             {
								ProduccionDiariaDetalleID = info.Field<int>("ProduccionDiariaDetalleID"),
								ProduccionDiaria = new ProduccionDiariaInfo { ProduccionDiariaID = info.Field<int>("ProduccionDiariaID")},
								Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), Descripcion = info.Field<string>("Producto") },
								PesajeMateriaPrima = new PesajeMateriaPrimaInfo { PesajeMateriaPrimaID = info.Field<int>("PesajeMateriaPrimaID")},
								EspecificacionForraje = info.Field<int>("EspecificacionForraje"),
								HoraInicial = info.Field<string>("HoraInicial"),
								HoraFinal = info.Field<string>("HoraFinal"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ProduccionDiariaDetalleInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ProduccionDiariaDetalleInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProduccionDiariaDetalleInfo
                             {
								ProduccionDiariaDetalleID = info.Field<int>("ProduccionDiariaDetalleID"),
								ProduccionDiaria = new ProduccionDiariaInfo { ProduccionDiariaID = info.Field<int>("ProduccionDiariaID")},
								Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), Descripcion = info.Field<string>("Producto") },
								PesajeMateriaPrima = new PesajeMateriaPrimaInfo { PesajeMateriaPrimaID = info.Field<int>("PesajeMateriaPrimaID")},
								EspecificacionForraje = info.Field<int>("EspecificacionForraje"),
								HoraInicial = info.Field<string>("HoraInicial"),
								HoraFinal = info.Field<string>("HoraFinal"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ProduccionDiariaDetalleInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ProduccionDiariaDetalleInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProduccionDiariaDetalleInfo
                             {
								ProduccionDiariaDetalleID = info.Field<int>("ProduccionDiariaDetalleID"),
								ProduccionDiaria = new ProduccionDiariaInfo { ProduccionDiariaID = info.Field<int>("ProduccionDiariaID")},
								Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), Descripcion = info.Field<string>("Producto") },
								PesajeMateriaPrima = new PesajeMateriaPrimaInfo { PesajeMateriaPrimaID = info.Field<int>("PesajeMateriaPrimaID")},
								EspecificacionForraje = info.Field<int>("EspecificacionForraje"),
								HoraInicial = info.Field<string>("HoraInicial"),
								HoraFinal = info.Field<string>("HoraFinal"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        
    }
}

