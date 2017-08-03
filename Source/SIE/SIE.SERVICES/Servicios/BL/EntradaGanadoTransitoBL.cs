using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    public class EntradaGanadoTransitoBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad EntradaGanadoTransito
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(EntradaGanadoTransitoInfo info)
        {
            try
            {
                Logger.Info();
                var entradaGanadoTransitoDAL = new EntradaGanadoTransitoDAL();
                int result = info.EntradaGanadoTransitoID;
                if (info.EntradaGanadoTransitoID == 0)
                {
                    result = entradaGanadoTransitoDAL.Crear(info);
                }
                else
                {
                    entradaGanadoTransitoDAL.Actualizar(info);
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<EntradaGanadoTransitoInfo> ObtenerPorPagina(PaginacionInfo pagina, EntradaGanadoTransitoInfo filtro)
        {
            try
            {
                Logger.Info();
                var entradaGanadoTransitoDAL = new EntradaGanadoTransitoDAL();
                ResultadoInfo<EntradaGanadoTransitoInfo> result = entradaGanadoTransitoDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de EntradaGanadoTransito
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EntradaGanadoTransitoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var entradaGanadoTransitoDAL = new EntradaGanadoTransitoDAL();
                IEnumerable<EntradaGanadoTransitoInfo> result = entradaGanadoTransitoDAL.ObtenerTodos();
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
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EntradaGanadoTransitoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var entradaGanadoTransitoDAL = new EntradaGanadoTransitoDAL();
                IEnumerable<EntradaGanadoTransitoInfo> result = entradaGanadoTransitoDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad EntradaGanadoTransito por su Id
        /// </summary>
        /// <param name="entradaGanadoTransitoID">Obtiene una entidad EntradaGanadoTransito por su Id</param>
        /// <returns></returns>
        public EntradaGanadoTransitoInfo ObtenerPorID(int entradaGanadoTransitoID)
        {
            try
            {
                Logger.Info();
                var entradaGanadoTransitoDAL = new EntradaGanadoTransitoDAL();
                IEnumerable<EntradaGanadoTransitoInfo> result = entradaGanadoTransitoDAL.ObtenerPorID(entradaGanadoTransitoID);
                EntradaGanadoTransitoInfo entradaGanadoTransito = ObtieneEntradaGanadoTransito(result);
                return entradaGanadoTransito;
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
        /// Obtiene una entidad EntradaGanadoTransito por su Id
        /// </summary>
        /// <returns></returns>
        public EntradaGanadoTransitoInfo ObtenerPorCorralOrganizacion(string corral, int organizacionID)
        {
            try
            {
                Logger.Info();
                var entradaGanadoTransitoDAL = new EntradaGanadoTransitoDAL();
                IEnumerable<EntradaGanadoTransitoInfo> result =
                    entradaGanadoTransitoDAL.ObtenerPorCorralOrganizacion(corral, organizacionID);
                EntradaGanadoTransitoInfo entradaGanadoTransito = ObtieneEntradaGanadoTransito(result);
                return entradaGanadoTransito;
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
        /// Obtiene el primer elemento de la coleccion
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private EntradaGanadoTransitoInfo ObtieneEntradaGanadoTransito(IEnumerable<EntradaGanadoTransitoInfo> result)
        {
            EntradaGanadoTransitoInfo entradaGanadoTransito = null;
            if (result != null)
            {
                entradaGanadoTransito = result.FirstOrDefault();
            }
            return entradaGanadoTransito;
        }
    }
}
 