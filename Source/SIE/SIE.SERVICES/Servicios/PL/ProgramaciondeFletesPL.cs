using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ProgramaciondeFletesPL
    {
        /// <summary>
        /// Obtener contratos
        /// </summary>
        /// <returns></returns>
        public List<ContratoInfo> ObtenerContratosPorTipo(int activo, int tipoFlete)
        {
            
            try
            {
                Logger.Info();
                var programacionFletesBl = new ProgramaciondeFletesBL();
                List<ContratoInfo> programaciondeFletesInfo = programacionFletesBl.ObtenerContratosPorTipo(activo, tipoFlete);
                return programaciondeFletesInfo;
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
        /// Elimina proveedor seleccionado
        /// </summary>
        /// <param name="proveedorSeleccionado"></param>
        /// <param name="datosProgramaciondeFletes"></param>
        /// <returns></returns>
        public List<ProgramaciondeFletesInfo> ElimnarProveedorSeleccionado(ProgramaciondeFletesInfo proveedorSeleccionado, List<ProgramaciondeFletesInfo> datosProgramaciondeFletes)
        {
            try
            {
                Logger.Info();
                var programacionFletesBl = new ProgramaciondeFletesBL();
                List<ProgramaciondeFletesInfo> programaciondeFletesInfo = programacionFletesBl.ElimnarProveedorSeleccionado(proveedorSeleccionado, datosProgramaciondeFletes);
                return programaciondeFletesInfo;
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
        /// Elimina costo seleccionado
        /// </summary>
        /// <param name="costoSeleccionado"></param>
        /// <param name="listaCostosFletesDetalleInfo"></param>
        /// <returns></returns>
        public List<FleteDetalleInfo> ElimnarCostoSeleccionado(FleteDetalleInfo costoSeleccionado, List<FleteDetalleInfo> listaCostosFletesDetalleInfo)
        {
            try
            {
                Logger.Info();
                var programacionFletesBl = new ProgramaciondeFletesBL();
                List<FleteDetalleInfo> fleteDetalleInfo = programacionFletesBl.ElimnarCostoSeleccionado(costoSeleccionado, listaCostosFletesDetalleInfo);
                return fleteDetalleInfo;
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
        /// Obtener Fletes
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <returns></returns>
        public List<ProgramaciondeFletesInfo> ObtenerFletes(ContratoInfo contratoInfo)
        {
            try
            {
                Logger.Info();
                var programacionFletesBl = new ProgramaciondeFletesBL();
                List<ProgramaciondeFletesInfo> resultado = programacionFletesBl.ObtenerFletes(contratoInfo);
                return resultado;
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
        /// Guarda la programacion de fletes
        /// </summary>
        /// <param name="listaProgramaciondeFletesInfos"></param>
        /// <returns></returns>
        public bool GuardarProgramaciondeFlete(List<ProgramaciondeFletesInfo> listaProgramaciondeFletesInfos)
        {
            bool regreso = false;
            try
            {
                Logger.Info();
                var programacionFletesBl = new ProgramaciondeFletesBL();
                programacionFletesBl.GuardarProgramaciondeFlete(listaProgramaciondeFletesInfos);
                regreso = true;
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
            return regreso;
        }
        /// <summary>
        /// Eliminar los proveedores del detalle
        /// </summary>
        /// <param name="listaProveedorFletes"></param>
        /// <param name="usuarioOrganizacionId"></param>
        /// <returns></returns>
        public bool EliminarProveedorFlete(List<ProgramaciondeFletesInfo> listaProveedorFletes, int usuarioOrganizacionId)
        {
            try
            {
                Logger.Info();
                var programacionFletesBl = new ProgramaciondeFletesBL();
                return programacionFletesBl.EliminarProveedorFlete(listaProveedorFletes, usuarioOrganizacionId);
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
        /// Desactiva los registro de flete detalle
        /// </summary>
        /// <param name="listaFleteDetalle"></param>
        /// <param name="usuarioOrganizacionId"></param>
        /// <returns></returns>
        public bool EliminarFleteDetalle(List<FleteDetalleInfo> listaFleteDetalle, int usuarioOrganizacionId)
        {
            try
            {
                Logger.Info();
                var programacionFletesBl = new ProgramaciondeFletesBL();
                return programacionFletesBl.EliminarFleteDetalle(listaFleteDetalle, usuarioOrganizacionId);
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
        /// Cancela un flete
        /// </summary>
        /// <param name="listaProgramaciondeFletesInfos"></param>
        /// <returns></returns>
        public bool CancelarProgramacionFlete(List<ProgramaciondeFletesInfo> listaProgramaciondeFletesInfos)
        {
            bool regreso = false;
            try
            {
                Logger.Info();
                var programacionFletesBl = new ProgramaciondeFletesBL();
                programacionFletesBl.CancelarProgramacionFlete(listaProgramaciondeFletesInfos);
                regreso = true;
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
            return regreso;
        }
        /// <summary>
        /// obtiene los contratos por pagina
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ContratoInfo> ObtenerPorPagina(PaginacionInfo pagina, ContratoInfo filtro)
        {
            try
            {
                Logger.Info();
                var programacionFletesBl = new ProgramaciondeFletesBL();
                ResultadoInfo<ContratoInfo> programaciondeFletesInfo = programacionFletesBl.ObtenerPorPagina(pagina, filtro);
                return programaciondeFletesInfo;

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
