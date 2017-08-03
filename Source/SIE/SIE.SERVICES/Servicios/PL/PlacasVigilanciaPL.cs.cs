using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System.Collections.Generic;

namespace SIE.Services.Servicios.PL
{
    public class PlacasVigilanciaPL
    {

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Organizacion
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(VigilanciaInfo info)
        {
            try
            {
                Logger.Info();
                var placasvigilanciaBL = new PlacasVigilanciaBL();
                int result = placasvigilanciaBL.Guardar(info);
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
        ///     Obtiene un lista paginada de organizaciones 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<VigilanciaInfo> ObtenerCamionPorPagina(PaginacionInfo pagina, VigilanciaInfo filtro)
        {
            ResultadoInfo<VigilanciaInfo> resultadoOrganizacion;
            try
            {
                Logger.Info();
                var placasvigilanciaBL = new PlacasVigilanciaBL();
                resultadoOrganizacion = placasvigilanciaBL.ObtenerCamionPorPagina(pagina, filtro);

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
            return resultadoOrganizacion;
        }

        /// <summary>
        /// Obtiene una lista de Lote filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<VigilanciaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var placasvigilanciaBL = new PlacasVigilanciaBL();
                IList<VigilanciaInfo> lista = placasvigilanciaBL.ObtenerTodos(estatus);

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
        /// Obtiene la placa por id
        /// </summary>
        /// <param name="filtroVigilancia"></param>
        /// <returns></returns>
        public VigilanciaInfo ObtenerPlacaPorID(VigilanciaInfo filtroVigilancia)
        {
            VigilanciaInfo resultadoOrganizacion;
            try
            {
                Logger.Info();
                var placasvigilanciaBL = new PlacasVigilanciaBL();
                resultadoOrganizacion = placasvigilanciaBL.ObtenerPorID(filtroVigilancia);
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
            return resultadoOrganizacion;
        }

    }
}
