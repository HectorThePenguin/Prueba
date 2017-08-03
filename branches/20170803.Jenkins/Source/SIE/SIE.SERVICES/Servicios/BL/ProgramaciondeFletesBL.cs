using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class ProgramaciondeFletesBL
    {
        /// <summary>
        /// Obtener fletes
        /// </summary>
        /// <returns></returns>
        public List<ContratoInfo> ObtenerContratosPorTipo(int activo, int tipoFlete)
        {
            
            try
            {
                Logger.Info();
                var programacionFletesDal = new ProgramaciondeFletesDAL();
                List<ContratoInfo> programaciondeFletesInfo = programacionFletesDal.ObtenerContratosPorTipo(activo, tipoFlete);
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
                var programacionFletInfo = new List<ProgramaciondeFletesInfo>();
                if (proveedorSeleccionado.Flete.FleteID <= 0)
                {
                     programacionFletInfo =
                        datosProgramaciondeFletes.Where(dato => dato.Flete.Proveedor.ProveedorID != proveedorSeleccionado.Flete.Proveedor.ProveedorID)
                            .ToList();
                }
                else
                {
                     programacionFletInfo = datosProgramaciondeFletes.Where(dato => dato.Flete.FleteID != proveedorSeleccionado.Flete.FleteID)
                         .ToList();    
                }
                
                return programacionFletInfo;
                // return programaciondeFletesInfo;
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
        /// Elinima costo seleccionado
        /// </summary>
        /// <param name="costoSeleccionado"></param>
        /// <param name="listaCostosFletesDetalleInfo"></param>
        /// <returns></returns>
        public List<FleteDetalleInfo> ElimnarCostoSeleccionado(FleteDetalleInfo costoSeleccionado, List<FleteDetalleInfo> listaCostosFletesDetalleInfo)
        {
            try
            {
                Logger.Info();
                var programacionFletInfo = listaCostosFletesDetalleInfo.Where(dato => dato.CostoID != costoSeleccionado.CostoID).ToList();
                return programacionFletInfo;
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
        /// Obtiene los fletes 
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <returns></returns>
        public List<ProgramaciondeFletesInfo> ObtenerFletes(ContratoInfo contratoInfo)
        {
            try
            {
                Logger.Info();
                var programacionFletesDal = new ProgramaciondeFletesDAL();
                var costosFletes = new CostoBL();
                List<ProgramaciondeFletesInfo> resultado = programacionFletesDal.ObtenerFletes(contratoInfo);
                if (resultado != null)
                {
                    foreach (var programaciondeFletesInfo in resultado)
                    {
                        var fleteDetalle =
                            costosFletes.ObtenerPorFleteID(programaciondeFletesInfo.Flete.FleteID);
                        programaciondeFletesInfo.Flete.LisaFleteDetalleInfo=fleteDetalle;
                    }
                }
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
        /// Guardar programacion de fletes
        /// </summary>
        /// <param name="listaProgramaciondeFletesInfos"></param>
        public void GuardarProgramaciondeFlete(List<ProgramaciondeFletesInfo> listaProgramaciondeFletesInfos)
        {
            try
            {
                Logger.Info();
                var programacionFletesDal = new ProgramaciondeFletesDAL();
                var programacionFletes = new ProgramaciondeFletesInfo();
                var contrato = new ContratoInfo();
                var contratoBl = new ContratoBL();
                if (listaProgramaciondeFletesInfos.Count > 0)
                {
                    contrato.ContratoId = listaProgramaciondeFletesInfos[0].Flete.ContratoID;
                    contrato.Organizacion = new OrganizacionInfo() { OrganizacionID = listaProgramaciondeFletesInfos[0].Organizacion.OrganizacionID };
                }
                
                contrato = contratoBl.ObtenerPorId(contrato);
                using (var transaction = new TransactionScope())
                {
                    programacionFletesDal.GuardarProgramaciondeFlete(listaProgramaciondeFletesInfos);
                    if (programacionFletes != null)
                    {
                        foreach (var listaProgramaciondeFletes in listaProgramaciondeFletesInfos)
                        {

                            List<ProgramaciondeFletesInfo> listaFletes = programacionFletesDal.ObtenerFletes(contrato);
                            if (listaFletes != null)
                            {
                                programacionFletes = listaFletes.FirstOrDefault(registro => registro.Flete.Proveedor.ProveedorID == listaProgramaciondeFletes.Flete.Proveedor.ProveedorID);
                            }
                            
                            if (listaProgramaciondeFletes.Flete.Opcion > 0)
                            {
                                for (int i = 0; i < listaProgramaciondeFletes.Flete.LisaFleteDetalleInfo.Count; i++)
                                {
                                    listaProgramaciondeFletes.Flete.LisaFleteDetalleInfo[i].FleteID =
                                        programacionFletes.Flete.FleteID;
                                    listaProgramaciondeFletes.Flete.LisaFleteDetalleInfo[i].UsuarioCreacion =
                                        listaProgramaciondeFletes.Flete.UsuarioCreacionID;
                                    listaProgramaciondeFletes.Flete.LisaFleteDetalleInfo[i].UsuarioModificacion =
                                        listaProgramaciondeFletes.Flete.UsuarioCreacionID;
                                }
                                programacionFletesDal.GuardarProgramaciondeFleteDetalle(
                                    listaProgramaciondeFletes.Flete.LisaFleteDetalleInfo);
                            }
                        }
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
        /// Eliminar los proveedores en flete detalle
        /// </summary>
        /// <param name="listaProgramaciondeFletesInfos"></param>
        /// <param name="usuarioModificacionesId"></param>
        public bool EliminarProveedorFlete(List<ProgramaciondeFletesInfo> listaProgramaciondeFletesInfos, int usuarioModificacionesId)
        {
            try
            {
                Logger.Info();
                var programacionFletesDal = new ProgramaciondeFletesDAL();
               

                using (var transaction = new TransactionScope())
                {
                    bool resultado = programacionFletesDal.EliminarProveedorFlete(listaProgramaciondeFletesInfos, usuarioModificacionesId);
                    transaction.Complete();
                    return resultado;
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
        /// Desactiva los registro de flete detalle
        /// </summary>
        /// <param name="listaFletesDetalle"></param>
        /// <param name="usuarioModificacionesId"></param>
        public bool EliminarFleteDetalle(List<FleteDetalleInfo> listaFletesDetalle, int usuarioModificacionesId)
        {
            try
            {
                Logger.Info();
                var programacionFletesDal = new ProgramaciondeFletesDAL();
              
                using (var transaction = new TransactionScope())
                {
                    bool resultado = programacionFletesDal.EliminarFleteDetalle(listaFletesDetalle, usuarioModificacionesId);
                   
                    transaction.Complete();
                    return resultado;
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
        /// Cancelar programacion flete
        /// </summary>
        /// <param name="listaProgramaciondeFletesInfos"></param>
        internal void CancelarProgramacionFlete(List<ProgramaciondeFletesInfo> listaProgramaciondeFletesInfos)
        {
            try
            {
                Logger.Info();
                var programacionFletesDal = new ProgramaciondeFletesDAL();
                using (var transaction = new TransactionScope())
                {
                    foreach (var programacionFlete in listaProgramaciondeFletesInfos)
                    {
                        programacionFletesDal.CancelarProgramacionFlete(programacionFlete);
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
        /// Obtiene los contratos por tipo y por pagina
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ContratoInfo> ObtenerPorPagina(PaginacionInfo pagina, ContratoInfo filtro)
        {

            try
            {
                Logger.Info();
                var programacionFletesDal = new ProgramaciondeFletesDAL();
                ResultadoInfo<ContratoInfo> programaciondeFletesInfo = programacionFletesDal.ObtenerPorPagina(pagina, filtro);
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
