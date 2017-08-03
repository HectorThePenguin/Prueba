using System;
using System.Collections.Generic;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    public class LoteDistribucionAlimentoBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad LoteDistribucionAlimento
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(LoteDistribucionAlimentoInfo info)
        {
            try
            {
                Logger.Info();
                var loteDistribucionAlimentoDAL = new LoteDistribucionAlimentoDAL();
                int result = info.LoteDistribucionAlimentoID;
                if (info.LoteDistribucionAlimentoID == 0)
                {
                    result = loteDistribucionAlimentoDAL.Crear(info);
                }
                else
                {
                    loteDistribucionAlimentoDAL.Actualizar(info);
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
        /// Obtiene un lista de LoteDistribucionAlimento
        /// </summary>
        /// <returns></returns>
        public IList<LoteDistribucionAlimentoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var loteDistribucionAlimentoDAL = new LoteDistribucionAlimentoDAL();
                IList<LoteDistribucionAlimentoInfo> result = loteDistribucionAlimentoDAL.ObtenerTodos();
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
        public IList<LoteDistribucionAlimentoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var loteDistribucionAlimentoDAL = new LoteDistribucionAlimentoDAL();
                IList<LoteDistribucionAlimentoInfo> result = loteDistribucionAlimentoDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad LoteDistribucionAlimento por su Id
        /// </summary>
        /// <param name="loteDistribucionAlimentoID">Obtiene una entidad LoteDistribucionAlimento por su Id</param>
        /// <returns></returns>
        public LoteDistribucionAlimentoInfo ObtenerPorID(int loteDistribucionAlimentoID)
        {
            try
            {
                Logger.Info();
                var loteDistribucionAlimentoDAL = new LoteDistribucionAlimentoDAL();
                LoteDistribucionAlimentoInfo result = loteDistribucionAlimentoDAL.ObtenerPorID(loteDistribucionAlimentoID);
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
        /// Obtiene una lista para la impresion de la distribucion de alimento
        /// </summary>
        /// <returns></returns>
        public IList<ImpresionDistribucionAlimentoModel> ObtenerImpresionDistribucionAlimento(FiltroImpresionDistribucionAlimento filtro)
        {
            try
            {
                Logger.Info();
                var loteDistribucionAlimentoDAL = new LoteDistribucionAlimentoDAL();
                IList<ImpresionDistribucionAlimentoModel> result = loteDistribucionAlimentoDAL.ObtenerImpresionDistribucionAlimento(filtro);
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

