using System;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Base.Interfaces;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Reflection;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class PrecioGanadoPL : IPaginador<PrecioGanadoInfo>, IAyuda<PrecioGanadoInfo>
    {
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<PrecioGanadoInfo> ObtenerPorPagina(PaginacionInfo pagina, PrecioGanadoInfo filtro)
        {
            ResultadoInfo<PrecioGanadoInfo> precioGanadoLista;
            try
            {
                Logger.Info();
                var precioGanadoBL = new PrecioGanadoBL();
                precioGanadoLista = precioGanadoBL.ObtenerPorPagina(pagina, filtro);
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
        public PrecioGanadoInfo ObtenerPorID(int infoId)
        {
            PrecioGanadoInfo info;
            try
            {
                Logger.Info();
                var precioGanadoBL = new PrecioGanadoBL();
                info = precioGanadoBL.ObtenerPorID(infoId);
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
        ///     Obtiene un PrecioGanadoInfo por Id
        /// </summary>
        /// <param name="info"> </param>
        /// <returns></returns>
        public PrecioGanadoInfo ObtenerPorID(PrecioGanadoInfo info)
        {
            try
            {
                Logger.Info();
                var precioGanadoBL = new PrecioGanadoBL();
                PrecioGanadoInfo result = precioGanadoBL.ObtenerPorID(info.PrecioGanadoID);
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
        ///     Metodo que crear un Tipo PrecioGanado
        /// </summary>
        /// <param name="info"></param>
        public void Guardar(PrecioGanadoInfo info)
        {
            try
            {
                Logger.Info();
                var precioGanadoBL = new PrecioGanadoBL();
                precioGanadoBL.Guardar(info);
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
        ///  Obtiene una lista de PrecioGanado filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns> </returns>
        public IList<PrecioGanadoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var precioGanadoBL = new PrecioGanadoBL();
                IList<PrecioGanadoInfo> lista = precioGanadoBL.ObtenerTodos(estatus);
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

        #region IAyuda<PrecioGanadoInfo> Members

        public ResultadoInfo<PrecioGanadoInfo> ObtenerPorId(int id)
        {
            var resultado = new ResultadoInfo<PrecioGanadoInfo>();
            var precioGanados = new List<PrecioGanadoInfo>();
            var precioGanado = ObtenerPorID(id);
            precioGanados.Add(precioGanado);
            resultado.Lista = precioGanados;

            return resultado;
        }

        public ResultadoInfo<PrecioGanadoInfo> ObtenerPorDescripcion(PaginacionInfo pagina, string descripcion)
        {
            var precioGanado = new PrecioGanadoInfo { Activo = EstatusEnum.Activo };
            ResultadoInfo<PrecioGanadoInfo> resultado = ObtenerPorPagina(pagina, precioGanado);

            return resultado;
        }

        #endregion

        /// <summary>
        ///     Obtiene un PrecioGanadoInfo por organización y tipo de ganado
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public PrecioGanadoInfo ObtenerPorOrganizacionTipoGanado(PrecioGanadoInfo info)
        {
            try
            {
                Logger.Info();
                var precioGanadoBL = new PrecioGanadoBL();
                PrecioGanadoInfo result = precioGanadoBL.ObtenerPorOrganizacionTipoGanado(info);
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
