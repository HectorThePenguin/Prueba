using System;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Reflection;
using System.Collections.Generic;

namespace SIE.Services.Servicios.BL
{
    internal class PrecioGanadoBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad PrecioGanado
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(PrecioGanadoInfo info)
        {
            try
            {
                Logger.Info();
                var precioGanadoDAL = new PrecioGanadoDAL();
                int result = info.PrecioGanadoID;
                if (info.PrecioGanadoID == 0)
                {
                    result = precioGanadoDAL.Crear(info);
                }
                else
                {
                    precioGanadoDAL.Actualizar(info);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<PrecioGanadoInfo> ObtenerPorPagina(PaginacionInfo pagina, PrecioGanadoInfo filtro)
        {
            ResultadoInfo<PrecioGanadoInfo> precioGanadoLista;
            try
            {
                Logger.Info();
                var precioGanadoDAL = new PrecioGanadoDAL();
                precioGanadoLista = precioGanadoDAL.ObtenerPorPagina(pagina, filtro);
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
            return precioGanadoLista;
        }

        /// <summary>
        ///     Obtiene un TipoPrecioGanadoInfo por Id
        /// </summary>
        /// <param name="infoId"></param>
        /// <returns></returns>
        internal PrecioGanadoInfo ObtenerPorID(int infoId)
        {
            PrecioGanadoInfo info;
            try
            {
                Logger.Info();
                var precioGanadoDAL = new PrecioGanadoDAL();
                info = precioGanadoDAL.ObtenerPorID(infoId);
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
        ///     Obtiene una lista de PrecioGanado filtrando por su estatus
        /// </summary>
        /// <param name="estatus"> </param>
        /// <returns></returns>
        internal IList<PrecioGanadoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var precioGanadoDAL = new PrecioGanadoDAL();
                IList<PrecioGanadoInfo> lista = precioGanadoDAL.ObtenerTodos(estatus);

                return lista;
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
        ///     Obtiene un TipoPrecioGanadoInfo por Id
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal PrecioGanadoInfo ObtenerPorOrganizacionTipoGanado(PrecioGanadoInfo info)
        {
            try
            {
                Logger.Info();
                var precioGanadoDAL = new PrecioGanadoDAL();
                PrecioGanadoInfo result = precioGanadoDAL.ObtenerPorOrganizacionTipoGanado(info);
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
