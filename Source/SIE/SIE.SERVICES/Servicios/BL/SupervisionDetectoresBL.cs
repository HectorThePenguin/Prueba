using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Services.Info.Atributos;
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
    public class SupervisionDetectoresBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad SupervisionDetectores
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(SupervisionDetectoresInfo info)
        {
            try
            {
                Logger.Info();
                var supervisionDetectoresDAL = new SupervisionDetectoresDAL();
                int result = info.SupervisionDetectoresId;
                if (info.SupervisionDetectoresId == 0)
                {
                    result = supervisionDetectoresDAL.Crear(info);
                }
                else
                {
                    supervisionDetectoresDAL.Actualizar(info);
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
        /// Obtiene un lista de SupervisionDetectores
        /// </summary>
        /// <returns></returns>
        public IList<SupervisionDetectoresInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var supervisionDetectoresDAL = new SupervisionDetectoresDAL();
                IList<SupervisionDetectoresInfo> result = supervisionDetectoresDAL.ObtenerTodos();
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
        public IList<SupervisionDetectoresInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var supervisionDetectoresDAL = new SupervisionDetectoresDAL();
                IList<SupervisionDetectoresInfo> result = supervisionDetectoresDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad SupervisionDetectores por su Id
        /// </summary>
        /// <param name="supervisionDetectoresID">Obtiene una entidad SupervisionDetectores por su Id</param>
        /// <returns></returns>
        public SupervisionDetectoresInfo ObtenerPorID(int supervisionDetectoresID)
        {
            try
            {
                Logger.Info();
                var supervisionDetectoresDAL = new SupervisionDetectoresDAL();
                SupervisionDetectoresInfo result = supervisionDetectoresDAL.ObtenerPorID(supervisionDetectoresID);
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
        /// Obtiene la impresion de la Supervision de Detectores
        /// </summary>
        /// <param name="filtro">filtros de la pantalla</param>
        /// <returns></returns>
        public List<ImpresionSupervisionDetectoresModel> ObtenerSupervisionDetectoresImpresion(FiltroImpresionSupervisionDetectores filtro)
        {
            try
            {
                Logger.Info();
                var supervisionDetectoresDAL = new SupervisionDetectoresDAL();
                List<ImpresionSupervisionDetectoresModel> supervisiones = supervisionDetectoresDAL.ObtenerSupervisionDetectoresImpresion(filtro);

                if(supervisiones == null)
                {
                    return null;
                }

                foreach (var impresionSupervision in supervisiones)
                {
                    var propiedades = impresionSupervision.GetType().GetProperties();
                    foreach (var propInfo in propiedades)
                    {
                        dynamic customAttributes = impresionSupervision.GetType().GetProperty(propInfo.Name).GetCustomAttributes(typeof(AtributoImpresionSupervisionDetectores), true);
                        if (customAttributes.Length > 0)
                        {
                            for (var indexAtributos = 0; indexAtributos < customAttributes.Length; indexAtributos++)
                            {
                                var atributos = (AtributoImpresionSupervisionDetectores)customAttributes[indexAtributos];
                                var preguntaID = atributos.PreguntaID;

                                ImpresionSupervisionDetectoresDetalleModel supervision = impresionSupervision.Detalles.FirstOrDefault(
                                    cali => cali.PreguntaID == preguntaID);

                                if (supervision != null)
                                {
                                    propInfo.SetValue(impresionSupervision, supervision.Respuesta, null);
                                    impresionSupervision.TotalSupervision = impresionSupervision.TotalSupervision +
                                                                            supervision.Respuesta;
                                }
                            }
                        }
                    }
                }

                return supervisiones;
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

