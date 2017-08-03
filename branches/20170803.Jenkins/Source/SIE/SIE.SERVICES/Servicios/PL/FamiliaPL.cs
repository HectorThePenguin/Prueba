using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class FamiliaPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Familia
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(FamiliaInfo info)
        {
            try
            {
                Logger.Info();
                var familiaBL = new FamiliaBL();
                int result = familiaBL.Guardar(info);
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
        public ResultadoInfo<FamiliaInfo> ObtenerPorPagina(PaginacionInfo pagina, FamiliaInfo filtro)
        {
            ResultadoInfo<FamiliaInfo> resultado;
            try
            {
                Logger.Info();
                var familiaBL = new FamiliaBL();
                resultado = familiaBL.ObtenerPorPagina(pagina, filtro);
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
            return resultado;
        }

        /// <summary>
        ///     Obtiene un Familia por Id
        /// </summary>
        /// <param name="familiaId"></param>
        /// <returns></returns>
        public FamiliaInfo ObtenerPorID(int familiaId)
        {
            FamiliaInfo familiaInfo;
            try
            {
                Logger.Info();
                var familiaBL = new FamiliaBL();
                familiaInfo = familiaBL.ObtenerPorID(familiaId);
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
            return familiaInfo;
        }

        /// <summary>
        ///     Obtiene un Familia por Id Filtro
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public FamiliaInfo ObtenerPorID(FamiliaInfo filtro)
        {
            FamiliaInfo familiaInfo;
            try
            {
                Logger.Info();
                var familiaBL = new FamiliaBL();
                familiaInfo = familiaBL.ObtenerPorID(filtro.FamiliaID);
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
            return familiaInfo;
        }

        /// <summary>
        ///  Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<FamiliaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var familiaBL = new FamiliaBL();
                IList<FamiliaInfo> result = familiaBL.ObtenerTodos(estatus);
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
        ///  Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<FamiliaInfo> Centros_ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var familiaBL = new FamiliaBL();
                IList<FamiliaInfo> result = familiaBL.Centros_ObtenerTodos(estatus);
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
        /// Obtiene una entidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public FamiliaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var familiaBL = new FamiliaBL();
                FamiliaInfo result = familiaBL.ObtenerPorDescripcion(descripcion);
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
