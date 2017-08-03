using System;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    internal class TipoCostoBL
    {
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<TipoCostoInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoCostoInfo filtro)
        {
            ResultadoInfo<TipoCostoInfo> costoLista;
            try
            {
                Logger.Info();
                var tipoCostosDAL = new TipoCostoDAL();
                costoLista = tipoCostosDAL.ObtenerPorPagina(pagina, filtro);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return costoLista;
        }

         /// <summary>
        ///     Metodo que actualiza un Tipo Costo
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(TipoCostoInfo info)
         {
             try
             {
                 Logger.Info();
                 var tipoCostosDAL = new TipoCostoDAL();
                 tipoCostosDAL.Actualizar(info);
             }
             catch (ExcepcionGenerica)
             {
                 throw;
             }
             catch (Exception ex)
             {
                 Logger.Error(ex);
                 throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
             }
         }

        /// <summary>
        ///     Obtiene un TipoCostoInfo por Id
        /// </summary>
        /// <param name="infoId"></param>
        /// <returns></returns>
        internal TipoCostoInfo ObtenerPorID(int infoId)
        {
            TipoCostoInfo info;
            try
            {
                Logger.Info();
                var tipoCostosDAL = new TipoCostoDAL();
                info = tipoCostosDAL.ObtenerPorID(infoId);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return info;
        }

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TipoCosto
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(TipoCostoInfo info)
        {
            try
            {
                Logger.Info();
                var tipoCostoDAL = new TipoCostoDAL();
                int result = info.TipoCostoID;
                if (info.TipoCostoID == 0)
                {
                    result = tipoCostoDAL.Crear(info);
                }
                else
                {
                    tipoCostoDAL.Actualizar(info);
                }
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///   Obtiene una lista de TipoCosto filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<TipoCostoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoCostoDAL = new TipoCostoDAL();
                IList<TipoCostoInfo> result = tipoCostoDAL.ObtenerTodos(estatus);

                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad TipoCosto por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal TipoCostoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoCostoDAL = new TipoCostoDAL();
                TipoCostoInfo result = tipoCostoDAL.ObtenerPorDescripcion(descripcion);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
