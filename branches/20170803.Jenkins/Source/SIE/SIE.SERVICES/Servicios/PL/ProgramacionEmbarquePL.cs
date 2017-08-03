using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;


namespace SIE.Services.Servicios.PL
{
    public class ProgramacionEmbarquePL
    {
        /// <summary>
        /// Método que obtiene los embarques de acuerdo a la organizacion ingresada.
        /// </summary>
        /// <param name="organizacionId"> ID de la organizacion para filtrar </param>
        /// <returns> Listado con todos los embarques encontrados </returns>
        public List<EmbarqueInfo> ObtenerProgramacionPorOrganizacionId(int organizacionId)
        {

            try
            {
                Logger.Info();
                var programacionEmbarquesBL = new ProgramacionEmbarqueBL();
                List<EmbarqueInfo> programacionEmbarqueInfo = programacionEmbarquesBL.ObtenerProgramacionPorOrganizacionId(organizacionId);
                return programacionEmbarqueInfo;
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
        /// Método que obtiene los ruteos desingados para un origen y destino.
        /// </summary>
        /// <param name="ruteoInfo"> Objeto con la información de origen y destino </param>
        /// <returns> Lista de los ruteos encontrados </returns>
        public List<RuteoInfo> ObtenerRuteosPorOrigenyDestino(RuteoInfo ruteoInfo)
        {
            try
            {
                Logger.Info();
                var programacionEmbarqueBL = new ProgramacionEmbarqueBL();
                List<RuteoInfo> listaRuteos = programacionEmbarqueBL.ObtenerRuteosPorOrigenyDestino(ruteoInfo);
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
        /// Método que obtiene los detalles del ruteo seleccionado.
        /// </summary>
        /// <param name="embarqueInfo"> Objeto con la información del ruteo para obtener los detalles </param>
        /// <returns> Lista con los detalles del ruteo seleccionado </returns>
        public List<RuteoDetalleInfo> ObtenerRuteoDetallePorRuteoID(EmbarqueInfo embarqueInfo)
        {
            try
            {
                Logger.Info();
                var programacionEmbarqueBL = new ProgramacionEmbarqueBL();
                List<RuteoDetalleInfo> listaDetalles = programacionEmbarqueBL.ObtenerRuteoDetallePorRuteoID(embarqueInfo);
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
        /// Metodo que elimina un Embarque
        /// </summary>
        /// <param name="info"></param>
        public void Eliminar(EmbarqueInfo info)
        {
            try
            {
                Logger.Info();
                var programacionEmbarqueBL = new ProgramacionEmbarqueBL();
                programacionEmbarqueBL.Eliminar(info);
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
        /// Metodo que guarda un Embarque
        /// </summary>
        /// <param name="info"></param>
        public void Guardar(EmbarqueInfo info, TipoGuardadoProgramacionEmbarqueEnum seccion)
        {
            try
            {
                Logger.Info();
                var programacionEmbarqueBL = new ProgramacionEmbarqueBL();
                programacionEmbarqueBL.Guardar(info, seccion);
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
        /// Metodo que guardado para seccion de datos
        /// </summary>
        /// <param name="info"></param>
        public EmbarqueInfo GuardarDatos(EmbarqueInfo info)
        {
            var result = new EmbarqueInfo();
            try
            {
                Logger.Info();
                var programacionEmbarqueBL = new ProgramacionEmbarqueBL();
                result = programacionEmbarqueBL.GuardarDatos(info);
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
        /// Método que obtiene los tipos de embarques.
        /// </summary>
        /// <returns> Lista con las tipos de embarque encontrados </returns>
        public List<TipoEmbarqueInfo> ObtenerTiposEmbarque(EstatusEnum Activo)
        {
            
            try
            {
                Logger.Info();
                var programacionEmbarqueBL = new ProgramacionEmbarqueBL();
                List<TipoEmbarqueInfo> programacionEmbarqueInfo = programacionEmbarqueBL.ObtenerTiposEmbarque(Activo);
                return programacionEmbarqueInfo;
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
        /// Metodo para Consultar las jaulas programadas
        /// </summary>
        /// <param name="embarqueInfo">filtros donde viene la OrganizacionID y la FechaInicio</param>
        /// <returns></returns>
        public List<EmbarqueInfo> ObtenerJaulasProgramadas(EmbarqueInfo embarqueInfo)
        {
            try
            {
                var programacionEmbarqueBL = new ProgramacionEmbarqueBL();
                var jaulasProgramadas = programacionEmbarqueBL.ObtenerJaulasProgramadas(embarqueInfo);
                if (jaulasProgramadas == null)
                {
                    return null;
                }
                return jaulasProgramadas;

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Método para obtener los ruteos activos registrados.
        /// </summary>
        /// <returns> Listado de los ruteos obtenidos </returns>
        public List<RuteoInfo> ObtenerRuteosActivos()
        {
            try
            {
                var programacionEmbarqueBL = new ProgramacionEmbarqueBL();
                var ruteosActivos = programacionEmbarqueBL.ObtenerRuteosActivos();
                if (ruteosActivos == null)
                {
                    return null;
                }
                return ruteosActivos;

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Método que obtiene los detalles del embarque ruteo a actualizar.
        /// </summary>
        /// <param name="embarqueInfo"> Información del embarque seleccionado </param>
        /// <returns> Listado de los detalles del embarque </returns>
        public List<EmbarqueRuteoInfo> ObtenerDetallesEmbarqueRuteo(EmbarqueInfo embarqueInfo)
        {
            try
            {
                Logger.Info();
                var programacionEmbarqueBL = new ProgramacionEmbarqueBL();
                List<EmbarqueRuteoInfo> listaDetalles = programacionEmbarqueBL.ObtenerDetallesEmbarqueRuteo(embarqueInfo);
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
                var programacionEmbarqueBL = new ProgramacionEmbarqueBL();
                EmbarqueInfo embarque = programacionEmbarqueBL.CuentaConFolioEmbarque(embarqueId);

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
                var programacionEmbarqueBL = new ProgramacionEmbarqueBL();
                bool existeTarifa = programacionEmbarqueBL.ObtenerFleteOrigenDestino(ruteoInfo);
                return existeTarifa;
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
        /// Método que obtiene los datos para la pestaña de transporte.
        /// </summary>
        /// <param name="embarqueInfo"> Id de la organización para buscar </param>
        /// <returns> Listado con los embarques pertenecientes a la organización. </returns>
        public List<EmbarqueInfo> ObtenerProgramacionTransporte(EmbarqueInfo embarqueInfo)
        {

            try
            {
                Logger.Info();
                var programacionEmbarquesBL = new ProgramacionEmbarqueBL();
                List<EmbarqueInfo> programacionEmbarqueInfo = programacionEmbarquesBL.ObtenerProgramacionTransporte(embarqueInfo);
                return programacionEmbarqueInfo;
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
        public void EliminarInformacionDatos(EmbarqueInfo info)
        {
            try
            {
                Logger.Info();
                var programacionEmbarqueBL = new ProgramacionEmbarqueBL();
                programacionEmbarqueBL.EliminarInformacionDatos(info);
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
        /// Método que obtiene los datos para mostrar en la pestaña de datos 
        /// </summary>
        /// <param name="embarqueInfo"> Objeto que contiene los datos necesarios para la consulta </param>
        /// <returns> Listado con los embarques encontrados </returns>
        public List<EmbarqueInfo> ObtenerProgramacionDatos(EmbarqueInfo embarqueInfo)
        {

            try
            {
                Logger.Info();
                var programacionEmbarquesBL = new ProgramacionEmbarqueBL();
                List<EmbarqueInfo> programacionEmbarqueInfo = programacionEmbarquesBL.ObtenerProgramacionDatos(embarqueInfo);
                return programacionEmbarqueInfo;
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
        /// Método que obtiene los tractos de acuerdo al proveedor seleccionado.
        /// </summary>
        /// <param name="camionInfo"> Objeto que contiene el proveedor seleccionado </param>
        /// <returns>Listado de los camiones encontrados </returns>
        public List<CamionInfo> ObtenerTractosPorProveedorID(CamionInfo camionInfo)
        {

            try
            {
                Logger.Info();
                var camionPL = new CamionPL();
                List<CamionInfo> listadoCamiones = camionPL.ObtenerPorProveedorID(camionInfo.Proveedor.ProveedorID);    
                return listadoCamiones;
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
        /// Método que obtiene los tractos de acuerdo por placa.
        /// </summary>
        /// <param name="camionInfo"> Objeto de tipo camión contenedor de la placa a buscar </param>
        /// <returns></returns>
        public CamionInfo ObtenerTractosPorDescripcion(CamionInfo camionInfo)
        {

            try
            {
                Logger.Info();
                var camionPL = new CamionPL();
                CamionInfo camionResultado = camionPL.ObtenerPorDescripcion(camionInfo.PlacaCamion);
                return camionResultado;
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
        /// Método que obtiene las jaulas de acuerdo al proveedor seleccionado.
        /// </summary>
        /// <param name="jaulaInfo"> Objeto que contiene el proveedor seleccionado </param>
        /// <returns> Listado de jaulas encontradas </returns>
        public List<JaulaInfo> ObtenerJaulasPorProveedorID(JaulaInfo jaulaInfo)
        {

            try
            {
                Logger.Info();
                var jaulaPL = new JaulaPL();
                List<JaulaInfo> listadoJaulas = jaulaPL.ObtenerPorProveedorID(jaulaInfo.Proveedor.ProveedorID);
                return listadoJaulas;
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
        /// Método que obtiene las jaulas de acuerdo por placa.
        /// </summary>
        /// <param name="jaulaInfo"> Objeto de tipo jaula contenedor de la placa a buscar </param>
        /// <returns></returns>
        public JaulaInfo ObtenerJaulasPorDescripcion(JaulaInfo jaulaInfo)
        {

            try
            {
                Logger.Info();
                var jaulaPL = new JaulaPL();
                JaulaInfo jaulaResultado = jaulaPL.ObtenerPorDescripcion(jaulaInfo.PlacaJaula);
                return jaulaResultado;
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
                var programacionEmbarqueBL = new ProgramacionEmbarqueBL();
                EmbarqueTarifaInfo EmbarqueTarifa = programacionEmbarqueBL.ObtenerCostoFlete(embarqueTarifa);
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
        /// Método que obtiene los gastos fijos de acuerdo a la ruta seleccionada.
        /// </summary>
        /// <param name="embarqueInfo"> Objeto de tipo embarque contenedor de la ruta seleccionada </param>
        /// <returns> Objeto que contiene la suma de los gastos fijos de acuerdo a la ruta seleccionada. </returns>
        public AdministracionDeGastosFijosInfo ObtenerGastosFijos(EmbarqueInfo embarqueInfo)
        {

            try
            {
                var programacionEmbarqueBL = new ProgramacionEmbarqueBL();
                AdministracionDeGastosFijosInfo gastosFijosRespuesta = programacionEmbarqueBL.ObtenerGastosFijos(embarqueInfo);
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
        /// Método que envía correos al transportista correspondiente al embarque
        /// </summary>
        /// <param name="embarqueInfo"> Información que contendrá el correo </param>
        /// <param name="formatoCorreo"> Formato con el que se enviará el correo </param>
        /// <returns> Numero entero de acuerdo al estado de la solictuds. </returns>
        public int EnviarCorreo(EmbarqueInfo embarqueInfo, string formatoCorreo)
        {

            try
            {
                var programacionEmbarqueBL = new ProgramacionEmbarqueBL();
                var embarqueCorreo =  programacionEmbarqueBL.ObtenerInformacionCorreo(embarqueInfo);
                var correo = new CorreoBL();
                var correoEnviar = new CorreoInfo();
                correoEnviar.Mensaje = String.Format(formatoCorreo, embarqueCorreo.Ruteo.OrganizacionOrigen.Descripcion, 
                                                                    embarqueCorreo.Ruteo.OrganizacionDestino.Descripcion,
                                                                    embarqueCorreo.ResponsableEmbarque,
                                                                    embarqueCorreo.TipoEmbarque.Descripcion,
                                                                    embarqueCorreo.CitaCarga);
                correoEnviar.Correos = new List<string>();
                correoEnviar.Asunto = "Solicitud de Unidad";
                correoEnviar.Correos.Add(embarqueInfo.Proveedor.Correo);
                var respuesta = correo.EnviarCorreo(correoEnviar, embarqueInfo.Organizacion);

                if (respuesta.Resultado)
                {
                    return 1;
                }
                else
                {
                    return 0;
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
        /// Metodo para validar el estatus del embarque
        /// </summary>
        /// <param name="info"></param>
        public EmbarqueInfo ValidarEstatus(EmbarqueInfo info)
        {
            var result = new EmbarqueInfo();
            try
            {
                Logger.Info();
                var programacionEmbarqueBL = new ProgramacionEmbarqueBL();
                result = programacionEmbarqueBL.ValidarEstatus(info);
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
