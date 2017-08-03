// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapRevisionImplanteDAL.cs" company="Apinterfaces">
//   
// </copyright>
// <summary>
//   Defines the MapRevisionImplanteDAL type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{

    /// <summary>
    /// The map revision implante dal.
    /// </summary>
    internal class MapRevisionImplanteDAL
    {

        /// <summary>
        /// The obtener lugares validacion.
        /// </summary>
        /// <param name="ds">
        /// The ds.
        /// </param>
        /// <returns>
        /// The <see cref="ResultadoInfo"/>.
        /// </returns>
        /// <exception cref="ExcepcionDesconocida">
        /// </exception>
        internal static ResultadoInfo<AreaRevisionInfo> ObtenerLugaresValidacion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<AreaRevisionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new AreaRevisionInfo
                         {
                             AreaRevisionId = info.Field<int>("AreaRevisionID"),
                             Descripcion = info.Field<string>("Descripcion")
                         }).ToList();
                var totalRegistros = 0;
                if (lista != null)
                {
                    totalRegistros = lista.Count;
                }

                var resultado =
                    new ResultadoInfo<AreaRevisionInfo>
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
        /// The obtener causas.
        /// </summary>
        /// <param name="ds">
        /// The ds.
        /// </param>
        /// <returns>
        /// The <see cref="ResultadoInfo"/>.
        /// </returns>
        /// <exception cref="ExcepcionDesconocida">
        /// </exception>
        internal static ResultadoInfo<CausaRevisionImplanteInfo> ObtenerCausas(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CausaRevisionImplanteInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CausaRevisionImplanteInfo
                         {
                             CausaId = info.Field<int>("EstadoImplanteID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Correcto = info.Field<bool>("Correcto")
                         }).ToList();
                var totalRegistros = 0;
                if (lista != null)
                {
                    totalRegistros = lista.Count;
                }

                var resultado =
                    new ResultadoInfo<CausaRevisionImplanteInfo>
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
        /// Obtiene la revision de un animal en el dia actual
        /// </summary>
        /// <param name="ds">
        /// The ds.
        /// </param>
        /// <returns>
        /// The <see cref="RevisionImplanteInfo"/>.
        /// </returns>
        /// <exception cref="ExcepcionDesconocida">
        /// </exception>
        internal static RevisionImplanteInfo ObtenerRevisionAnimalActual(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                RevisionImplanteInfo revision =
                    (from info in dt.AsEnumerable()
                     select
                         new RevisionImplanteInfo
                             {
                                 RevisionImplanteId = info.Field<int>("RevisionImplanteID"),
                                 Lote = new LoteInfo { LoteID = info.Field<int>("LoteID") },
                                 Animal = new AnimalInfo { AnimalID = info.Field<long>("AnimalID") },
                                 Fecha = info.Field<DateTime>("Fecha"),
                                 Causa = new CausaRevisionImplanteInfo { CausaId = info.Field<int>("EstadoImplanteID") },
                                 LugarValidacion = new AreaRevisionInfo { AreaRevisionId = info.Field<int>("AreaRevisionID") }
                             }).First();

                return revision;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
