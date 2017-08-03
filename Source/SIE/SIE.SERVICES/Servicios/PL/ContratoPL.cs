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
    public class ContratoPL
    {
        /// <summary>
        /// Metodo para obtener un listado de tipos contrato por estatus
        /// </summary>
        public List<ContratoInfo> ObtenerPorEstado(EstatusEnum estatus)
        {
            List<ContratoInfo> result;
            try
            {
                Logger.Info();
                var contratoBL = new ContratoBL();
                result = contratoBL.ObtenerPorEstado(estatus);
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
        /// Metodo para obtener un listado de tipos contrato (activos e inactivos)
        /// </summary>
        public List<ContratoInfo> ObtenerTodos()
        {
            List<ContratoInfo> result;
            try
            {
                Logger.Info();
                var contratoBL = new ContratoBL();
                result = contratoBL.ObtenerTodos();
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
        /// Obtiene un contrato por id
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <returns>ContratoInfo</returns>
        public ContratoInfo ObtenerPorId(ContratoInfo contratoInfo)
        {
            ContratoInfo contrato;
            try
            {
                Logger.Info();
                var contratoBL = new ContratoBL();
                contrato = contratoBL.ObtenerPorId(contratoInfo);
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
            return contrato;
        }

        /// <summary>
        /// Obtiene un contrato por folio
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <returns>ContratoInfo</returns>
        public ContratoInfo ObtenerPorFolio(ContratoInfo contratoInfo)
        {
            ContratoInfo contrato;
            try
            {
                Logger.Info();
                var contratoBL = new ContratoBL();
                contrato = contratoBL.ObtenerPorFolio(contratoInfo);
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
            return contrato;
        }

        /// <summary>
        /// Obtiene la lista de contratos por proveedor
        /// </summary>
        /// <param name="proveedorId"></param>
        /// <param name="organizacionId"> </param>
        /// <returns></returns>
        public List<ContratoInfo> ObtenerContratosPorProveedorId(int proveedorId,int organizacionId)
        {
            List<ContratoInfo> listaContratos;
            try
            {
                Logger.Info();
                var contratoBL = new ContratoBL();
                listaContratos = contratoBL.ObtenerContratosPorProveedorId(proveedorId, organizacionId);
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
            return listaContratos;
        }

        /// <summary>
        /// Obtiene una lista paginada de contrato
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ContratoInfo> ObtenerPorPagina(PaginacionInfo pagina, ContratoInfo filtro)
        {
            ResultadoInfo<ContratoInfo> contratoLista;
            try
            {
                Logger.Info();
                var contratoBL = new ContratoBL();
                contratoLista = contratoBL.ObtenerPorPagina(pagina, filtro);
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
            return contratoLista;
        }

        /// <summary>
        /// Obtiene una lista paginada de contrato
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ContratoInfo> ObtenerPorPaginaSinProgramacion(PaginacionInfo pagina, ContratoInfo filtro)
        {
            ResultadoInfo<ContratoInfo> contratoLista;
            try
            {
                Logger.Info();
                var contratoBL = new ContratoBL();
                contratoLista = contratoBL.ObtenerPorPaginaSinProgramacion(pagina, filtro);
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
            return contratoLista;
        }

        public List<ContratoInfo> ObtenerContratoTipoFlete()
        {
            List<ContratoInfo> result;
            try
            {
                Logger.Info();
                var contratoBL = new ContratoBL();
                result = contratoBL.ObtenerContratoTipoFlete();
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
        /// Obtiene un contrato por folio
        /// </summary>
        /// <param name="contenedor"></param>
        /// <returns>ContratoInfo</returns>
        public ContratoInfo ObtenerPorContenedor(ContratoInfo contenedor)
        {
            ContratoInfo contrato;
            try
            {
                Logger.Info();
                var contratoBL = new ContratoBL();
                contrato = contratoBL.ObtenerPorContenedor(contenedor);
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
            return contrato;
        }

        /// <summary>
        /// Obtiene un contrato por folio
        /// </summary>
        /// <param name="pagina"> </param>
        /// <param name="contenedor"></param>
        /// <returns>ContratoInfo</returns>
        public ResultadoInfo<ContratoInfo> ObtenerPorContenedorPaginado(PaginacionInfo pagina, ContratoInfo contenedor)
        {
            ResultadoInfo<ContratoInfo> contrato;
            try
            {
                Logger.Info();
                var contratoBL = new ContratoBL();
                contrato = contratoBL.ObtenerPorContenedorPaginado(pagina, contenedor);
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
            return contrato;
        }

        /// <summary>
        /// Metodo que actualiza el estado de un contrato
        /// </summary>
        /// <param name="info"></param>
        /// <param name="estatus"></param>
        public void ActualizarEstado(ContratoInfo info, EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var contratoBl = new ContratoBL();
                contratoBl.ActualizarEstado(info, estatus);
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
        /// Obtiene una lista de contratos por XML
        /// </summary>
        /// <param name="contratosId"></param>
        /// <returns></returns>
        public IEnumerable<ContratoInfo> ObtenerPorXML(List<int> contratosId)
        {
            try
            {
                Logger.Info();
                var contratoBl = new ContratoBL();
                IEnumerable<ContratoInfo> contratos = contratoBl.ObtenerPorXML(contratosId);
                return contratos;
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
        /// Obtiene una lista de contratos por fechas
        /// </summary>
        /// <returns></returns>
        public List<ContratoInfo> ObtenerPorFechasConciliacion(int organizacionID, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                Logger.Info();
                var contratoBl = new ContratoBL();
                List<ContratoInfo> contratos = contratoBl.ObtenerPorFechasConciliacion(organizacionID, fechaInicio, fechaFin);
                return contratos;
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
        /// Metodo que actualiza un contrato
        /// </summary>
        /// <param name="info"></param>
        /// <param name="estatus"></param>
        public void ActualizarContrato(ContratoInfo info, EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var contratoBl = new ContratoBL();
                contratoBl.ActualizarContrato(info, estatus);
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
