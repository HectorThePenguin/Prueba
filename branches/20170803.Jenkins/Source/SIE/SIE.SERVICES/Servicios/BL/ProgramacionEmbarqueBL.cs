using System;
using System.Collections.Generic;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class ProgramacionEmbarqueBL
    {
        /// <summary>
        /// Método que obtiene los embarques programados de acuerdo a la organizacion ingresada.
        /// </summary>
        /// <param name="organizacionId"> ID de la organizacion para filtrar </param>
        /// <returns> Listado con los embarques econtrados </returns>
        public List<EmbarqueInfo> ObtenerProgramacionPorOrganizacionId(int organizacionId)
        {

            try
            {
                Logger.Info();
                var programacionEmbarqueDAL = new ProgramacionEmbarqueDAL();
                List<EmbarqueInfo> programacionEmbarquesInfo = programacionEmbarqueDAL.ObtenerProgramacionPorOrganizacionId(organizacionId);
                return programacionEmbarquesInfo;
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
        /// Método que referencia a la capa DAL para obtener los ruteos de acuerdo a un origen y destino
        /// </summary>
        /// <param name="ruteoInfo"> Objeto con la información del ruteo. </param>
        /// <returns> Lista de los ruteos encontrados </returns>
        public List<RuteoInfo> ObtenerRuteosPorOrigenyDestino(RuteoInfo ruteoInfo)
        {
            try
            {
                Logger.Info();
                var programacionEmbarqueDAL = new ProgramacionEmbarqueDAL();
                List<RuteoInfo> listaRuteos = programacionEmbarqueDAL.ObtenerRuteosPorOrganizacionId(ruteoInfo);
                return listaRuteos;
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
        /// Método que obtiene los detales del ruteo seleccionado.
        /// </summary>
        /// <param name="embarqueInfo"> Objeto con la información del ruteo para la búsqueda </param>
        /// <returns> Detalles del ruteo seleccionado </returns>
        public List<RuteoDetalleInfo> ObtenerRuteoDetallePorRuteoID(EmbarqueInfo embarqueInfo)
        {
            try
            {
                Logger.Info();
                var programacionEmbarqueDAL = new ProgramacionEmbarqueDAL();
                List<RuteoDetalleInfo> listaDetalles = programacionEmbarqueDAL.ObtenerRuteoDetallesPorRuteoId(embarqueInfo);
                return listaDetalles;
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
        ///  Metodo que guarda una Programación de Embarque
        /// </summary>
        /// <param name="info"></param>
        internal void Eliminar(EmbarqueInfo info)
        {
            try
            {
                Logger.Info();
                var programacionEmbarqueDAL = new ProgramacionEmbarqueDAL();
                programacionEmbarqueDAL.Eliminar(info);
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
        ///  Metodo que guarda una Programación de Embarque
        /// </summary>
        /// <param name="info"></param>
        internal void Guardar(EmbarqueInfo info, TipoGuardadoProgramacionEmbarqueEnum seccion)
        {
            try
            {
                Logger.Info();
                var programacionEmbarqueDAL = new ProgramacionEmbarqueDAL();

                using(var transaction = new TransactionScope())
                {
                    // Guardado para seecion de programacion
                    if (seccion == TipoGuardadoProgramacionEmbarqueEnum.Programacion)
                    {
                        if (info.EmbarqueID != 0)
                        {
                            // Actualizacion para pestaña de programación

                            if (info.TipoEmbarque.TipoEmbarqueID == TipoEmbarqueEnum.Ruteo.GetHashCode())
                            {
                                // Actualizacion para tipo de embarque 'Ruteo'
                                programacionEmbarqueDAL.ActualizarEmbarqueProgramacion(info);
								programacionEmbarqueDAL.ActualizarProgramacionTipoEmbarqueRuteo(info);
                            }
                            else if (info.TipoEmbarque.TipoEmbarqueID == TipoEmbarqueEnum.Directo.GetHashCode())
                            {
                                // Actualizacion para tipo de embarque 'Directo'
                                programacionEmbarqueDAL.ActualizarProgramacionTipoEmbarqueDirecto(info);
                            }
                            else
                            {
                                // Actualizacion para cualquier otro tipo de embarque
                                programacionEmbarqueDAL.ActualizarProgramacion(info);
                            }
                            if (info.Observaciones != null)
                            {
                                programacionEmbarqueDAL.ActualizarProgramacionEmbarqueObservacion(info);
                            }
                        }
                        else
                        {
                            // Guardado para pestaña de programación

                            if (info.TipoEmbarque.TipoEmbarqueID == TipoEmbarqueEnum.Ruteo.GetHashCode())
                            {
                                // Guardado para tipo de embarque 'Ruteo'
                                programacionEmbarqueDAL.CrearProgramacionTipoEmbarqueRuteo(info);
                            }
                            else
                            {
                                // Guardado para cualquier otro tipo de embarque
                                programacionEmbarqueDAL.CrearProgramacion(info);
                            }
                        }
                    }
                    else if (seccion == TipoGuardadoProgramacionEmbarqueEnum.Transporte)
                    {
                        // Guardado para pestaña de transporte
                        if (info.UsuarioCreacionID == -1 )
                        {
                            programacionEmbarqueDAL.ActualizarTransporteEmbarque(info);
                        }
                        else
                        {
                            programacionEmbarqueDAL.CrearTransporteEmbarque(info);
                        }
             
                   }
                   else if (seccion == TipoGuardadoProgramacionEmbarqueEnum.Datos)
                   {
                       // Guardado para pestaña de Datos
                       programacionEmbarqueDAL.CrearDatosEmbarque(info);
                   }
                    transaction.Complete();
                }
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
        ///  Metodo que guardado para seccion de datos
        /// </summary>
        /// <param name="info"></param>
        internal EmbarqueInfo GuardarDatos(EmbarqueInfo info)
        {
            var result = new EmbarqueInfo();

            try
            {
                Logger.Info();
                var programacionEmbarqueDAL = new ProgramacionEmbarqueDAL();
                

                using (var transaction = new TransactionScope())
                {
                    
                    if (info.UsuarioCreacionID == -1 )
                    {
                        // Actualizar para pestaña de Datos
                        result = programacionEmbarqueDAL.ActualizarDatosEmbarque(info);
                    }
                    else
                    {
                        // Guardar para pestaña de Datos
                        result = programacionEmbarqueDAL.CrearDatosEmbarque(info);
                    }
                    transaction.Complete();
                }
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
        /// Método que obtiene los tipos de embarques registrados.
        /// </summary>
        /// <returns> Lista con los tipos de embarques encontrados. </returns>
        public List<TipoEmbarqueInfo> ObtenerTiposEmbarque(EstatusEnum Activo)
        {
            List<TipoEmbarqueInfo> result;
            try
            {
                Logger.Info();
                var programacionEmbarqueDAL = new ProgramacionEmbarqueDAL();
                result = programacionEmbarqueDAL.ObtenerTiposEmbarque(Activo);
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
        /// Metodo para Consultar las jaulas programadas
        /// </summary>
        /// <param name="embarqueInfo">filtros donde viene la OrganizacionID y la FechaInicio</param>
        /// <returns></returns>
        public List<EmbarqueInfo> ObtenerJaulasProgramadas(EmbarqueInfo embarqueInfo)
        {
            try
            {
                Logger.Info();
                var programacionEmbarqueDAL = new ProgramacionEmbarqueDAL();
                List<EmbarqueInfo> jaulasProgramadas = programacionEmbarqueDAL.ObtenerJaulasProgramadas(embarqueInfo);
                return jaulasProgramadas;

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Método que obtiene los ruteos registrados que se encuentren activos.
        /// </summary>
        /// <returns> Listado de los ruteos obtenidos </returns>
        public List<RuteoInfo> ObtenerRuteosActivos()
        {
            try
            {
                Logger.Info();
                var programacionEmbarqueDAL = new ProgramacionEmbarqueDAL();
                List<RuteoInfo> listaRuteos = programacionEmbarqueDAL.ObtenerRuteosActivos();
                return listaRuteos;
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
        /// Método que obtiene los detalles del embarque ruteo seleccionado.
        /// </summary>
        /// <param name="embarqueInfo"> Información del embarque seleccionado </param>
        /// <returns> Listada de los ruteos del embarque </returns>
        public List<EmbarqueRuteoInfo> ObtenerDetallesEmbarqueRuteo(EmbarqueInfo embarqueInfo)
        {
            try
            {
                Logger.Info();
                var programacionEmbarqueDAL = new ProgramacionEmbarqueDAL();
                List<EmbarqueRuteoInfo> listaDetalles = programacionEmbarqueDAL.ObtenerDetallesEmbarqueRuteo(embarqueInfo);
                return listaDetalles;
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
        /// Metodo para consultar la informacion si cuenta con folio embarque 
        /// </summary>
        /// <returns></returns>
        public EmbarqueInfo CuentaConFolioEmbarque(int embarqueId)
        {
            try
            {
                var programacionEmbarqueDAL = new ProgramacionEmbarqueDAL();
                EmbarqueInfo embarque = programacionEmbarqueDAL.CuentaConFolioEmbarque(embarqueId);

                return embarque;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Procedimiento que valida que exista tarifa para un origen y un destino
        /// </summary>
        /// <param name="ruteoInfo"></param>
        /// <returns></returns>
        public bool ObtenerFleteOrigenDestino(RuteoInfo ruteoInfo)
        {
            try
            {
                Logger.Info();
                var programacionEmbarqueDAL = new ProgramacionEmbarqueDAL();
                int existeTarifa = programacionEmbarqueDAL.ObtenerFleteOrigenDestino(ruteoInfo);
                if (existeTarifa != 0)
                {
                    return true;   
                }
                else
                {
                    return false;
                }
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
        /// Método que obtiene los embarques para la pestaña de transportes.
        /// </summary>
        /// <param name="embarqueInfo"> Objeto que contiene los datos necesarios para la consulta </param>
        /// <returns> Listado de embarques pertenecientes a la organización ingresada. </returns>
        public List<EmbarqueInfo> ObtenerProgramacionTransporte(EmbarqueInfo embarqueInfo)
        {

            try
            {
                Logger.Info();
                var programacionEmbarqueDAL = new ProgramacionEmbarqueDAL();
                List<EmbarqueInfo> programacionEmbarquesInfo = programacionEmbarqueDAL.ObtenerProgramacionTransporte(embarqueInfo);
                return programacionEmbarquesInfo;
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
        /// Metodo que elimina la informacion del embarque registrada en la seccion de datos
        /// </summary>
        /// <returns></returns>
        internal void EliminarInformacionDatos(EmbarqueInfo info)
        {
            try
            {
                Logger.Info();
                var programacionEmbarqueDAL = new ProgramacionEmbarqueDAL();
                programacionEmbarqueDAL.EliminarInformacionDatos(info);
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
        /// Método que obtiene los datos necesarios para la pestaña de datos.
        /// </summary>
        /// <param name="embarqueInfo"> Objeto que contiene los parametros necearios para realizar la consulta </param>
        /// <returns> Listado con los embarques encontrados. </returns>
        public List<EmbarqueInfo> ObtenerProgramacionDatos(EmbarqueInfo embarqueInfo)
        {

            try
            {
                Logger.Info();
                var programacionEmbarqueDAL = new ProgramacionEmbarqueDAL();
                List<EmbarqueInfo> programacionEmbarquesInfo = programacionEmbarqueDAL.ObtenerProgramacionDatos(embarqueInfo);
                return programacionEmbarquesInfo;
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
        /// Metodo para consultar el costo del flete 
        /// </summary>
        /// <param name="embarqueTarifa"> Objeto que contiene los datos necesarios para la consulta </param>
        /// <returns> Listado con los embarques encontrados </returns>
        public EmbarqueTarifaInfo ObtenerCostoFlete(EmbarqueTarifaInfo embarqueTarifa)
        {
            try
            {
                Logger.Info();
                var programacionEmbarqueDAL = new ProgramacionEmbarqueDAL();
                EmbarqueTarifaInfo EmbarqueTarifa = programacionEmbarqueDAL.ObtenerCostoFlete(embarqueTarifa);
                return EmbarqueTarifa;
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
        /// Método que obtiene los gastos fijos de acuerdo a la ruta seleccionada
        /// </summary>
        /// <param name="embarqueInfo"> Objeto contenedor de la ruta seleccionada. </param>
        /// <returns> Objeto contenedor de los gastos fijos asignados a dicha ruta. </returns>
        public AdministracionDeGastosFijosInfo ObtenerGastosFijos(EmbarqueInfo embarqueInfo)
        {
            try
            {
                Logger.Info();
                var programacionEmbarqueDAL = new ProgramacionEmbarqueDAL();
                AdministracionDeGastosFijosInfo gastosFijosRespuesta = programacionEmbarqueDAL.ObtenerGastosFijos(embarqueInfo);
                return gastosFijosRespuesta;
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
        /// Método que obtiene la información que se enviará en el correo.
        /// </summary>
        /// <param name="embarqueInfo"> Objeto contenedor de los filtros de búsqueda. </param>
        /// <returns> Objeto que contiene la información para enviar al correo. </returns>
        public EmbarqueInfo ObtenerInformacionCorreo(EmbarqueInfo embarqueInfo)
        {
            try
            {
                var programacionEmbarqueDAL = new ProgramacionEmbarqueDAL();
                EmbarqueInfo embarque = programacionEmbarqueDAL.ObtenerInformacionCorreo(embarqueInfo);

                return embarque;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        ///  Metodo para validar estatus del embarque
        /// </summary>
        /// <param name="info"></param>
        internal EmbarqueInfo ValidarEstatus(EmbarqueInfo info)
        {
            var result = new EmbarqueInfo();

            try
            {
                Logger.Info();
                var programacionEmbarqueDAL = new ProgramacionEmbarqueDAL();
                // Actualizar para pestaña de Datos
                result = programacionEmbarqueDAL.ValidarEstatus(info);
                
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
    }
}
