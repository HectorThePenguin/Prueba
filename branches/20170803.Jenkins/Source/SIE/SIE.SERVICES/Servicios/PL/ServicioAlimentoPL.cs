using System;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System.Collections.Generic;

namespace SIE.Services.Servicios.PL
{
    public class ServicioAlimentoPL
    {

        /// <summary>
        /// Obtiene  corrales por corralID
        /// </summary>
        /// <param name="corralID"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public ServicioAlimentoInfo ObtenerPorCorralID(int organizacionID, int corralID)
        {
            ServicioAlimentoInfo servicioAlimentoInfo;
            try
            {
                Logger.Info();
                var servicioAlimentoBL = new ServicioAlimentoBL();
                servicioAlimentoInfo = servicioAlimentoBL.ObtenerPorCorralID(organizacionID, corralID);
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
            return servicioAlimentoInfo;
        }

        /// <summary>
        ///     Metodo que guarda ServicioAlimento
        /// </summary>
        /// <param name="info"></param>
        public void Guardar(IList<ServicioAlimentoInfo> info)
        {
            try
            {
                Logger.Info();
                var servicioAlimentoBL = new ServicioAlimentoBL();
                foreach (ServicioAlimentoInfo temp in info)
                {
                    /*temp.UsuarioCreacionID = 1;
                    temp.UsuarioModificacionID = 1;
                    temp.OrganizacionID = 1;*/
                    temp.Fecha = DateTime.Now;
                    temp.FechaCreacion = DateTime.Now;
                    temp.FechaModificacion = DateTime.Now;
                    temp.Estatus = Services.Info.Enums.EstatusEnum.Activo.GetHashCode();
                }
                servicioAlimentoBL.Guardar(info);
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
        /// Obtener informacion diaria de servicio alimento.
        /// </summary>
        /// <returns></returns>
        public IList<ServicioAlimentoInfo> ObtenerInformacionDiariaAlimento(int organizacionId)
        {
            IList<ServicioAlimentoInfo> servicioAlimentoInfo;
            try
            {
                Logger.Info();
                var servicioAlimentoBL = new ServicioAlimentoBL();
                servicioAlimentoInfo = servicioAlimentoBL.ObtenerInformacionDiariaAlimento(organizacionId);
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
            return servicioAlimentoInfo;
        }
    }
}
