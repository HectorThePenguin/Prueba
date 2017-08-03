using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class CorralRangoPL
    {
        /// <summary>
        /// Metodo que regresa una lista los Corrales Disponibles por OrganizacionID
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public IList<CorralRangoInfo> ObtenerPorOrganizacionID(int organizacionID)
        {
            IList<CorralRangoInfo> list;
            try
            {
                Logger.Info();
                var corralRangoBl = new CorralRangoBL();
                list = corralRangoBl.ObtenerPorOrganizacionID(organizacionID);
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
            return list;
        }
        /// <summary>
        /// Crea un registro en la tabla de CorralRango
        /// </summary>
        /// <param name="info"></param>
        public void Crear(CorralRangoInfo info)
        {
            try
            {
                Logger.Info();
                var corralRangoBl = new CorralRangoBL();
                corralRangoBl.Crear(info);
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
        /// Actualiza un registro en la tabla de CorralRango
        /// </summary>
        /// <param name="info"></param>
        public void Actualizar(List<CorralRangoInfo> info)
        {
            try
            {
                Logger.Info();
                var corralRangoBl = new CorralRangoBL();
                corralRangoBl.Actualizar(info);
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
        /// Obtiene verdadero cuando un corral de la organizacion tiene lote asignado
        /// </summary>
        /// <param name="organizacionID">Organizacion Id</param>
        /// <param name="corralID">Corral ID</param>
        /// <returns>Boolean especificando si tiene lote</returns>
        public Boolean ObtenerLoteAsignado(int organizacionID, int corralID)
        {
            bool tieneLoteAsignado;
            try
            {
                Logger.Info();
                var corralRangoBl = new CorralRangoBL();
                tieneLoteAsignado = corralRangoBl.ObtenerLoteAsignado(organizacionID, corralID);
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
            return tieneLoteAsignado;
        }

        /// <summary>
        /// Funcion que permite obtener el listado de corrales configurados de la organizacion
        /// </summary>
        /// <param name="organizacionID">Id de la Organizacion</param>
        /// <returns>Lista de corrales previamente configurados</returns>
        public IList<CorralRangoInfo> ObtenerCorralesConfiguradosPorOrganizacionID(int organizacionID)
        {
            IList<CorralRangoInfo> list;
            try
            {
                Logger.Info();
                var corralRangoBl = new CorralRangoBL();
                list = corralRangoBl.ObtenerConfiguradosPorOrganizacionID(organizacionID);
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
            return list;
        }

        /// <summary>
        /// Funcion qque obtiene el corral destino
        /// </summary>
        /// <param name="corralRangoInfo"></param>
        /// <param name="dias"></param>
        public IList<CorralRangoInfo> ObtenerCorralDestino(CorralRangoInfo corralRangoInfo, int dias)
        {
            IList<CorralRangoInfo> list;
            try
            {
                Logger.Info();
                var corralRangoBl = new CorralRangoBL();
                list = corralRangoBl.ObtenerCorralDestino(corralRangoInfo, dias);
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
            return list;
        }
        /// <summary>
        /// Funcion que obtiene el corral destino sin proporcionar el tipo de ganado
        /// </summary>
        /// <param name="corralRangoInfo"></param>
        /// <param name="diasBloqueo"></param>
        public IList<CorralRangoInfo> ObtenerCorralDestinoSinTipoGanado(CorralRangoInfo corralRangoInfo,int diasBloqueo)
        {
            IList<CorralRangoInfo> list;
            try
            {
                Logger.Info();
                var corralRangoBl = new CorralRangoBL();
                list = corralRangoBl.ObtenerCorralDestinoSinTipoGanado(corralRangoInfo, diasBloqueo);
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
            return list;
        }

        /// <summary>
        /// Metodo para eliminar la configuracion de corrales
        /// </summary>
        /// <param name="corralGrid"></param>
        public void Eliminar(CorralRangoInfo corralGrid)
        {
            try
            {
                Logger.Info();
                var corralRangoBl = new CorralRangoBL();
                corralRangoBl.Eliminar(corralGrid);
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
        /// Metodo para guardar la configuracion de corrales
        /// </summary>
        /// <param name="lista"></param>
        /// <param name="corralesEliminados"></param>
        public void Guardar(List<CorralRangoInfo> lista, IList<CorralRangoInfo> corralesEliminados)
         {
             try
             {
                 Logger.Info();
                 var corralRangoBl = new CorralRangoBL();
                 corralRangoBl.Guardar(lista, corralesEliminados);
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
