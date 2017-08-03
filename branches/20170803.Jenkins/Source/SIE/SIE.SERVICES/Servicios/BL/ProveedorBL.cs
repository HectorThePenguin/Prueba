using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using System.Data;

namespace SIE.Services.Servicios.BL
{
    internal class ProveedorBL
    {
        /// <summary>
        ///     Metodo que Guarda un proveedor
        /// </summary>
        /// <param name="info"></param>
        internal void Guardar(ProveedorInfo info)
        {
            try
            {
                Logger.Info();
                using (var tscope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required))
                {
                    var proveedorDAL = new ProveedorDAL();
                    if (info.ProveedorID != 0)
                    {
                        proveedorDAL.Actualizar(info);
                        var provComisionDal = new ComisionDAL();
                        var provChofDAL = new ProveedorChoferDAL();

                        var lista = (from p in info.Choferes
                                     select new ProveedorChoferInfo()
                                     {
                                         Proveedor = info,
                                         Chofer = p,
                                         Activo = EstatusEnum.Activo,
                                         UsuarioCreacionID = info.UsuarioModificacionID ?? 0
                                     }).ToList();

                        provChofDAL.Guardar(lista);
                        provComisionDal.GuardarComisiones(info.Comisiones);
                        if (info.TipoProveedor.TipoProveedorID == TipoProveedorEnum.Comisionistas.GetHashCode())
                        {
                            var listaRetenciones = new List<ProveedorRetencionInfo>();
                            ObtenerListaProveedorRetencion(listaRetenciones, info);
                            if (listaRetenciones.Any())
                            {
                                var proveedorRetencionBL = new ProveedorRetencionBL();
                                IList<ProveedorRetencionInfo> listaRetencionesGuardadas =
                                    proveedorRetencionBL.ObtenerPorProveedorID(info.ProveedorID);

                                if (listaRetencionesGuardadas != null && listaRetencionesGuardadas.Any())
                                {
                                    ProveedorRetencionInfo proveedorRetencionInfo;
                                    foreach (var retencion in listaRetenciones)
                                    {
                                        if (retencion.Iva != null)
                                        {
                                            proveedorRetencionInfo =
                                                listaRetencionesGuardadas.FirstOrDefault(ret => ret.Iva.IvaID > 0);
                                            if (proveedorRetencionInfo != null)
                                            {
                                                retencion.ProveedorRetencionID =
                                                    proveedorRetencionInfo.ProveedorRetencionID;
                                                retencion.UsuarioModificacionID = info.UsuarioModificacionID ?? 0;
                                            }
                                        }
                                        else
                                        {
                                            proveedorRetencionInfo =
                                              listaRetencionesGuardadas.FirstOrDefault(ret => ret.Retencion.TipoRetencion.Equals(retencion.Retencion.TipoRetencion));
                                            if (proveedorRetencionInfo != null)
                                            {
                                                retencion.ProveedorRetencionID =
                                                    proveedorRetencionInfo.ProveedorRetencionID;
                                                retencion.UsuarioModificacionID = info.UsuarioModificacionID ?? 0;
                                            }
                                        }
                                    }
                                }
                                
                                proveedorRetencionBL.GuardarLista(listaRetenciones);
                            }
                        }

                    }
                    else
                    {
                        proveedorDAL.Crear(info);
                    }
                    tscope.Complete();
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ProveedorInfo> ObtenerPorPagina(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ProveedorInfo> result;
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                result = proveedorDAL.ObtenerPorPagina(pagina, filtro);
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
        /// }obtiene una lista de proveedores de  tipo ganadera
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ProveedorInfo> ObtenerPorPaginaTipoProveedorGanadera(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ProveedorInfo> result;
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                result = proveedorDAL.ObtenerPorPaginaTipoProveedorGanadera(pagina, filtro);
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
        ///     Obtiene una lista de proveedores
        /// </summary>
        /// <param> <name></name> </param>        
        /// <returns></returns>
        internal IList<ProveedorInfo> ObtenerTodos()
        {
            IList<ProveedorInfo> lista;
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                lista = proveedorDAL.ObtenerTodos();
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
            return lista;
        }

        /// <summary>
        ///     Obtiene una lista de Proveedor filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"> </param>
        /// <returns></returns>
        internal IList<ProveedorInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                IList<ProveedorInfo> lista = proveedorDAL.ObtenerTodos(estatus);

                return lista;
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

        internal ProveedorInfo proveedorIdGanadera(int proveedorId)
        {
            ProveedorInfo info;
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                info = proveedorDAL.proveedorIdGanadera(proveedorId);
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
            return info;
        }

        /// <summary>
        ///     Obtiene un proveedor por su Id
        /// </summary>
        /// <param> <name></name> </param>
        /// <param name="proveedorId"> </param>
        /// <returns></returns>
        internal ProveedorInfo ObtenerPorID(int proveedorId)
        {
            ProveedorInfo info;
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                info = proveedorDAL.ObtenerPorID(proveedorId);
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
            return info;
        }

        /// <summary>
        ///     Obtiene un proveedor por su Id
        /// </summary>
        /// <param> <name></name> </param>
        /// <param name="proveedorId"> </param>
        /// <returns></returns>
        internal ProveedorInfo ObtenerPorIDConCorreo(int proveedorId)
        {
            ProveedorInfo info;
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                info = proveedorDAL.ObtenerPorIDConCorreo(proveedorId);
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
            return info;
        }

        internal ResultadoInfo<ProveedorInfo> ObtenerPorId(int id)
        {
            ResultadoInfo<ProveedorInfo> result = null;
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                var proveedor = proveedorDAL.ObtenerPorID(id);
                if (proveedor != null)
                {
                    result = new ResultadoInfo<ProveedorInfo>();
                    result.Lista = new List<ProveedorInfo>() { proveedor };
                    result.TotalRegistros = 1;
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

        internal ResultadoInfo<ProveedorInfo> ObtenerPorDescripcion(PaginacionInfo pagina, string descripcion)
        {

            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                var filtro = new ProveedorInfo { Descripcion = descripcion, CodigoSAP = string.Empty, Activo = EstatusEnum.Activo };
                ResultadoInfo<ProveedorInfo> result = proveedorDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una entidad Proveedor por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad Proveedor por su Id</param>
        /// <returns></returns>
        internal ProveedorInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                ProveedorInfo result = proveedorDAL.ObtenerPorDescripcion(descripcion);
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
        ///     Obtiene un proveedor por su Codigo SAP
        /// </summary>
        /// <param> <name></name> </param>
        /// <param name="proveedorInfo"> </param>
        /// <returns></returns>
        internal ProveedorInfo ObtenerPorCodigoSAP(ProveedorInfo proveedorInfo)
        {
            ProveedorInfo info;
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                info = proveedorDAL.ObtenerPorCodigoSAP(proveedorInfo);
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
            return info;
        }
        /// <summary>
        ///     Obtiene un proveedor del lote
        /// </summary>
        /// <param> <name></name> </param>
        /// <param name="lote"> </param>
        /// <returns></returns>
        internal ProveedorInfo ObtenerPorLote(LoteInfo lote)
        {
            ProveedorInfo info;
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                info = proveedorDAL.ObtenerPorLote(lote);
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
            return info;
        }

        /// <summary>
        /// Obtiene un tipo de proveedor por id
        /// </summary>
        /// <param name="estatus"></param>
        /// <param name="tipoProveedorID"></param>
        /// <returns></returns>
        internal List<ProveedorInfo> ObtenerPorTipoProveedorID(int estatus, int tipoProveedorID)
        {
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                List<ProveedorInfo> lista = proveedorDAL.ObtenerPorTipoProveedorID(estatus, tipoProveedorID);
                return lista;
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

        public ResultadoInfo<ProveedorInfo> ObtenerPorPaginaFiltros(PaginacionInfo pagina, ProveedorInfo filtro)
        {

            ResultadoInfo<ProveedorInfo> result;
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                if (filtro.CodigoSAP == null)
                {
                    filtro.CodigoSAP = "";
                }
                if (filtro.Descripcion == null)
                {
                    filtro.Descripcion = "";
                }
                result = proveedorDAL.ObtenerPorPaginaFiltros(pagina, filtro);
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


        public ResultadoInfo<ProveedorInfo> ObtenerPorPaginaTipoProveedor(PaginacionInfo pagina, ProveedorInfo filtro)
        {

            ResultadoInfo<ProveedorInfo> result;
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                if (filtro.CodigoSAP == null)
                {
                    filtro.CodigoSAP = "";
                }
                if (filtro.Descripcion == null)
                {
                    filtro.Descripcion = "";
                }
                result = proveedorDAL.ObtenerPorPaginaTipoProveedor(pagina, filtro);
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

        public ProveedorInfo ObtenerPorIDFiltroTipoProveedor(int proveedorId)
        {
            ProveedorInfo info;
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                info = proveedorDAL.ObtenerPorID(proveedorId);
                if (info.TipoProveedor.TipoProveedorID != (int)TipoProveedorEnum.ProveedoresFletes)
                {
                    info = null;
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
            return info;
        }
        /// <summary>
        /// Obtiene una lista de proveedores que correspondan a la lista de tipos de proveedores
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ProveedorInfo> ObtenerPorPaginaTiposProveedores(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ProveedorInfo> resultado;
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                resultado = proveedorDAL.ObtenerPorPaginaTiposProveedores(pagina, filtro);
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
        /// Obtiene una lista de proveedores que correspondan a la lista de tipos de proveedores que tienen almacen configurado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ProveedorInfo> ObtenerPorPaginaTiposProveedoresAlmacen(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ProveedorInfo> resultado;
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                resultado = proveedorDAL.ObtenerPorPaginaTiposProveedoresAlmacen(pagina, filtro);
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
        /// Consulta los proveedores que tiene asignado un producto en la tabla fletes internos
        /// </summary>
        /// <param name="productoId"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal List<ProveedorInfo> ObtenerProveedoresEnFletesInternos(int productoId, int organizacionId)
        {
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                return proveedorDAL.ObtenerProveedoresEnFletesInternos(productoId, organizacionId);
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


        internal ResultadoInfo<ProveedorInfo> ObtenerFoliosPorPaginaFletesInterno(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ProveedorInfo> provedoresInfo;
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                provedoresInfo = proveedorDAL.ObtenerFoliosPorPaginaFletesInterno(pagina, filtro);
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
            return provedoresInfo;
        }

        /// <summary>
        /// Obtiene los proveedores que tienen contrato
        /// con el producto seleccionado
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ProveedorInfo ObtenerPorProductoContratoCodigoSAP(ProveedorInfo filtro)
        {
            ProveedorInfo info;
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                info = proveedorDAL.ObtenerPorProductoContratoCodigoSAP(filtro);
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
            return info;
        }

        /// <summary>
        /// Obtiene los proveedores que tienen contrato
        /// con el producto seleccionado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ProveedorInfo> ObtenerPorProductoContrato(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ProveedorInfo> provedoresResult;
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                provedoresResult = proveedorDAL.ObtenerPorProductoContrato(pagina, filtro);
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
            return provedoresResult;
        }

        private ProveedorSapBL _InfoSap;
        internal DataTable VerificaProveedorSAP(string Sociedad, string IdProveedor)
        {
            DataTable resultado = new DataTable();
            _InfoSap = new ProveedorSapBL();

            if (_InfoSap.Ping())
            {
                resultado = _InfoSap.ConsultarProveedorSAP(IdProveedor, Sociedad);

            }
            return resultado;
        }



        internal ProveedorInfo proveedorIdGanadera(ProveedorInfo proveedorInfo)
        {
            ProveedorInfo info;
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                info = proveedorDAL.proveedorIdGanadera(proveedorInfo);
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
            return info;
        }

        private void ObtenerListaProveedorRetencion(List<ProveedorRetencionInfo> listaRetenciones, ProveedorInfo proveedor)
        {
            ProveedorRetencionInfo proveedorRetencionInfo;
            if (proveedor.RetencionISR != null && proveedor.RetencionISR.RetencionID > 0)
            {
                proveedorRetencionInfo = new ProveedorRetencionInfo
                                         {
                                             Proveedor = proveedor,
                                             Retencion = proveedor.RetencionISR,
                                             Activo = proveedor.RetencionISR.Activo,
                                             UsuarioCreacionID = proveedor.UsuarioModificacionID ?? 0
                                         };
                listaRetenciones.Add(proveedorRetencionInfo);
            }
            if (proveedor.RetencionIVA != null && proveedor.RetencionIVA.RetencionID > 0)
            {
                proveedorRetencionInfo = new ProveedorRetencionInfo
                                         {
                                             Proveedor = proveedor,
                                             Retencion = proveedor.RetencionIVA,
                                             Activo = proveedor.RetencionIVA.Activo,
                                             UsuarioCreacionID = proveedor.UsuarioModificacionID ?? 0
                                         };
                listaRetenciones.Add(proveedorRetencionInfo);
            }
            if (proveedor.IVA != null && proveedor.IVA.IvaID > 0)
            {
                proveedorRetencionInfo = new ProveedorRetencionInfo
                                         {
                                             Proveedor = proveedor,
                                             Iva = proveedor.IVA,
                                             Activo= proveedor.IVA.Activo,
                                             UsuarioCreacionID = proveedor.UsuarioModificacionID ?? 0
                                         };
                listaRetenciones.Add(proveedorRetencionInfo);
            }
        }

        /// <summary>
        /// Obtiene el proveedor del Embarque
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ProveedorInfo ObtenerPorCodigoSAPEmbarque(ProveedorInfo filtro)
        {
            ProveedorInfo info;
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                info = proveedorDAL.ObtenerPorCodigoSAPEmbarque(filtro);
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
            return info;
        }

        /// <summary>
        /// Obtiene los proveedores del Embarque
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ProveedorInfo> ObtenerPorPaginaEmbarque(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ProveedorInfo> info;
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                info = proveedorDAL.ObtenerPorPaginaEmbarque(pagina, filtro);
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
            return info;
        }

        //internal ResultadoInfo<ProveedorInfo> ObtenerFleteroPorPagina(PaginacionInfo pagina, ProveedorInfo filtro)
        //{
        //    ResultadoInfo<ProveedorInfo> result;
        //    try
        //    {
        //        Logger.Info();
        //        var proveedorDAL = new ProveedorDAL();
        //        result = proveedorDAL.ObtenerFleteroPorPagina(pagina, filtro);
        //    }
        //    catch (ExcepcionGenerica)
        //    {
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
        //    }
        //    return result;
        //}

        /// <summary>
        ///     Obtiene un Proveedor filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"> </param>
        /// <returns></returns>
        internal IList<ProveedorInfo> ObtenerProveedorActivo(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                IList<ProveedorInfo> lista = proveedorDAL.ObtenerProveedorActivo(estatus);

                return lista;
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
        ///     Obtiene un Proveedor que cuenta con Origen-Destino Configurado
        /// </summary>
        /// <returns></returns>
        internal ProveedorInfo ObtenerProveedorConfiguradoOrigenDestino(EmbarqueDetalleInfo embarque)
        {
            ProveedorInfo info;
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                info = proveedorDAL.ObtenerProveedorConfiguradoOrigenDestino(embarque);
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
            return info;
        }

        /// <summary>
        /// Obtiene los proveedores por una ruta
        /// </summary>
        /// <returns></returns>
        internal List<ProveedorInfo> ObtenerProveedoresPorRuta(int estatus, int tipoProveedorID, int ConfiguracionEmbarqueDetalleID)
        {
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorDAL();
                List<ProveedorInfo> lista = proveedorDAL.ObtenerProveedoresPorRuta(estatus, tipoProveedorID, ConfiguracionEmbarqueDetalleID);
                return lista;
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
