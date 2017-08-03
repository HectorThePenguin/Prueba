using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class PlacasVigilanciaBL
    {
        /// <summary>
        ///     Metodo que guarda una organización
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(VigilanciaInfo info)
        {
            try
            {
                Logger.Info();
                var placasvigilanciaDAL = new PlacasVigilanciaDAL();
                int result = info.ID;
                if (info.ID == 0)
                {
                    result = placasvigilanciaDAL.Crear(info);
                }
                else
                {
                    placasvigilanciaDAL.Actualizar(info);
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
        internal ResultadoInfo<VigilanciaInfo> ObtenerCamionPorPagina(PaginacionInfo pagina, VigilanciaInfo filtro)
        {
            ResultadoInfo<VigilanciaInfo> result;
            try
            {
                Logger.Info();
                var placasvigilanciaDAL = new PlacasVigilanciaDAL();
                result = placasvigilanciaDAL.ObtenerCamionPorPagina(pagina, filtro);
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
            return result;
        }

        /// <summary>
        ///     Obtiene una lista de las Organizaciones
        /// </summary>
        /// <param> <name></name> </param>        
        /// <returns></returns>
        internal IList<VigilanciaInfo> ObtenerTodos()
        {
            IList<VigilanciaInfo> lista;
            try
            {
                Logger.Info();
                var placasvigilanciaDAL = new PlacasVigilanciaDAL();
                lista = placasvigilanciaDAL.ObtenerTodos();
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
            return lista;
        }


        /// <summary>
        /// Obtiene una lista de Organizacion filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"> </param>
        /// <returns></returns>
        internal IList<VigilanciaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var placasvigilanciaDAL = new PlacasVigilanciaDAL();
                IList<VigilanciaInfo> lista = placasvigilanciaDAL.ObtenerTodos(estatus);

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
        /// Obtiene las placas del camion por id
        /// </summary>
        /// <param name="vigilancia"></param>
        /// <returns></returns>
        internal VigilanciaInfo ObtenerPorID(VigilanciaInfo vigilancia)
        {
            VigilanciaInfo info;
            try
            {
                Logger.Info();
                var placasvigilanciaDAL = new PlacasVigilanciaDAL();
                info = placasvigilanciaDAL.ObtenerPorID(vigilancia);
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
    }
}