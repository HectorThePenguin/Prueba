using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class CheckListCorralPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad CheckListCorral
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        /// <param name="xml">Xml que contiene la estructura para guardar generar el PDF</param>
        public int Guardar(CheckListCorralInfo info, XDocument xml)
        {
            try
            {
                Logger.Info();
                var checkListCorralBL = new CheckListCorralBL();
                int result = checkListCorralBL.Guardar(info, xml);
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
        public ResultadoInfo<CheckListCorralInfo> ObtenerPorPagina(PaginacionInfo pagina, CheckListCorralInfo filtro)
        {
            try
            {
                Logger.Info();
                var checkListCorralBL = new CheckListCorralBL();
                ResultadoInfo<CheckListCorralInfo> result = checkListCorralBL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista
        /// </summary>
        /// <returns></returns>
        public IList<CheckListCorralInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var checkListCorralBL = new CheckListCorralBL();
                IList<CheckListCorralInfo> result = checkListCorralBL.ObtenerTodos();
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
        public IList<CheckListCorralInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var checkListCorralBL = new CheckListCorralBL();
                IList<CheckListCorralInfo> result = checkListCorralBL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad por su Id
        /// </summary>
        /// <param name="checkListCorralID"></param>
        /// <returns></returns>
        public CheckListCorralInfo ObtenerPorID(int checkListCorralID)
        {
            try
            {
                Logger.Info();
                var checkListCorralBL = new CheckListCorralBL();
                CheckListCorralInfo result = checkListCorralBL.ObtenerPorID(checkListCorralID);
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
        public CheckListCorralInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var checkListCorralBL = new CheckListCorralBL();
                CheckListCorralInfo result = checkListCorralBL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene un registro de CheckListCorral
        /// </summary>
        /// <param name="loteID">identificador del Lote</param>
        /// <param name="organizacionID">identificador de la Organización</param>
        /// <returns></returns>
        public CheckListCorralInfo ObtenerPorLote(int organizacionID, int loteID)
        {
            try
            {
                Logger.Info();
                var checkListCorralBL = new CheckListCorralBL();
                CheckListCorralInfo result = checkListCorralBL.ObtenerPorLote(organizacionID, loteID);
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

