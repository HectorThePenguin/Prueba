using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System.Data;

namespace SIE.Services.Servicios.PL
{
    public class ProveedorPL 
    {
        /// <summary>
        ///     Metodo que guarda un Proveedor
        /// </summary>
        /// <param name="info"></param>
        public void Guardar(ProveedorInfo info)
        {
            try
            {
                Logger.Info();
                var proveedorBL = new ProveedorBL();
                proveedorBL.Guardar(info);
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
        public ResultadoInfo<ProveedorInfo> ObtenerPorPagina(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ProveedorInfo> result;
            try
            {
                Logger.Info();
                var proveedorBL = new ProveedorBL();
                result = proveedorBL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene  una lista paginada de proveedores de tipo ganadera
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProveedorInfo> ObtenerPorPaginaTipoProveedorGanadera(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ProveedorInfo> result;
            try
            {
                Logger.Info();
                var proveedorBL = new ProveedorBL();
                result = proveedorBL.ObtenerPorPaginaTipoProveedorGanadera(pagina, filtro);
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
        ///     Obtiene un lista de los Proveedores
        /// </summary>
        /// <returns> </returns>
        public IList<ProveedorInfo> ObtenerTodos()
        {
            IList<ProveedorInfo> lista;
            try
            {
                Logger.Info();
                var proveedorBL = new ProveedorBL();
                lista = proveedorBL.ObtenerTodos();
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
        ///     Obtiene un lista de los Proveedores
        /// </summary>
        /// <returns> </returns>
        /// 
        public IList<ProveedorInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var proveedorBL = new ProveedorBL();
                IList<ProveedorInfo> lista = proveedorBL.ObtenerTodos(estatus);

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
        ///      Obtiene un Proveedor por su Id
        /// </summary>
        /// <returns> </returns>
        public ProveedorInfo ObtenerPorID(int proveedorId)
        {
            ProveedorInfo info;
            try
            {
                Logger.Info();
                var proveedorBL = new ProveedorBL();
                info = proveedorBL.ObtenerPorID(proveedorId);
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
        

        public ProveedorInfo ObtenerPorIDTipoGanadera(ProveedorInfo proveedorInfo)
        {
            ProveedorInfo info;
            try
            {
                Logger.Info();
                var proveedorIdGanadera = new ProveedorBL();
                info = proveedorIdGanadera.proveedorIdGanadera(proveedorInfo);
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
        ///      Obtiene un Proveedor por su Id
        /// </summary>
        /// <returns> </returns>
        public ProveedorInfo ObtenerPorIDConCorreo(int proveedorId)
        {
            ProveedorInfo info;
            try
            {
                Logger.Info();
                var proveedorBL = new ProveedorBL();
                info = proveedorBL.ObtenerPorIDConCorreo(proveedorId);
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



        public ProveedorInfo ObtenerPorID(ProveedorInfo proveedorInfo)
        {
            ProveedorInfo info;
            try
            {
                Logger.Info();
                var proveedorBL = new ProveedorBL();
                info = proveedorBL.ObtenerPorID(proveedorInfo.ProveedorID);
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
        ///      Obtiene un Proveedor por su Codigo SAP
        /// </summary>
        /// <returns> </returns>
        public ProveedorInfo ObtenerPorCodigoSAP(ProveedorInfo proveedorInfo)
        {
            ProveedorInfo info;
            try
            {
                Logger.Info();
                var proveedorBL = new ProveedorBL();
                
                info = proveedorBL.ObtenerPorCodigoSAP(proveedorInfo);
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

        //ResultadoInfo<ProveedorInfo> ObtenerPorDescripcion(PaginacionInfo pagina, string descripcion)
        //{
        //    var proveedor = 
        //        new ProveedorInfo
        //            {
        //                Descripcion = descripcion, Activo = EstatusEnum.Activo
        //            };
        //    ResultadoInfo<ProveedorInfo> resultado = ObtenerPorPagina(pagina, proveedor);
        //    //resultado = new ResultadoInfo<ProveedorInfo>();

        //    return resultado;
        //}

        

        /// <summary>
        /// Obtiene una entidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public ProveedorInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var proveedorBL = new ProveedorBL();
                ProveedorInfo result = proveedorBL.ObtenerPorDescripcion(descripcion);
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
        /// <param name="lote"></param>
        /// <returns></returns>
        public ProveedorInfo ObtenerPorLote(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var proveedorBL = new ProveedorBL();
                ProveedorInfo result = proveedorBL.ObtenerPorLote(lote);
                return result;
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }





        /// <summary>
        /// Obtiene un tipo de proveedor por id
        /// </summary>
        /// <param name="estatus"></param>
        /// <param name="tipoProveedorID"></param>
        /// <returns></returns>
        public List<ProveedorInfo> ObtenerPorTipoProveedorID(int estatus, int tipoProveedorID)
        {
            try
            {
                Logger.Info();
                var proveedorBL = new ProveedorBL();
                List<ProveedorInfo> lista = proveedorBL.ObtenerPorTipoProveedorID(estatus, tipoProveedorID);
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

        public ProveedorInfo ObtenerPorIDFiltroTipoProveedor(ProveedorInfo proveedorInfo)
        {
            ProveedorInfo info;
            try
            {
                Logger.Info();
                var proveedorBL = new ProveedorBL();
                info = proveedorBL.ObtenerPorIDFiltroTipoProveedor(proveedorInfo.ProveedorID);
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


        public ProveedorInfo ObtenerPorIDFiltroTipoProveedor(int proveedorId)
        {
            ProveedorInfo info;
            try
            {
                Logger.Info();
                var proveedorBL = new ProveedorBL();
                info = proveedorBL.ObtenerPorIDFiltroTipoProveedor(proveedorId);
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



        public ResultadoInfo<ProveedorInfo> ObtenerPorPaginaTipoProveedor(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ProveedorInfo> result;
            try
            {
                Logger.Info();
                var proveedorBL = new ProveedorBL();
                result = proveedorBL.ObtenerPorPaginaTipoProveedor(pagina, filtro);

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

        public ResultadoInfo<ProveedorInfo> ObtenerPorPaginaFiltros(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ProveedorInfo> result;
            try
            {
                Logger.Info();
                var proveedorBL = new ProveedorBL();
                result = proveedorBL.ObtenerPorPaginaFiltros(pagina, filtro);

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
        /// Obtiene una lista de proveedores por una lista de proveedores por pagina
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProveedorInfo> ObtenerPorPaginaTiposProveedores(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ProveedorInfo> result;
            try
            {
                Logger.Info();
                var proveedorBL = new ProveedorBL();
                result = proveedorBL.ObtenerPorPaginaTiposProveedores(pagina, filtro);
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
        /// Obtiene una lista de proveedores por una lista de proveedores por pagina que tienen almacen configurado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProveedorInfo> ObtenerPorPaginaTiposProveedoresAlmacen(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ProveedorInfo> result;
            try
            {
                Logger.Info();
                var proveedorBL = new ProveedorBL();
                result = proveedorBL.ObtenerPorPaginaTiposProveedoresAlmacen(pagina, filtro);
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
        /// Consulta los proveedores que tiene asignado un producto en la tabla fletes internos
        /// </summary>
        /// <param name="productoId"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public List<ProveedorInfo> ObtenerProveedoresEnFletesInternos(int productoId, int organizacionId)
        {
            try
            {
                Logger.Info();
                var proveedorDAL = new ProveedorBL();
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

        /// <summary>
        /// Consulta los proveedores que tiene asignado un producto en la tabla fletes internos
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProveedorInfo> ObtenerFoliosPorPaginaFletesInternos(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ProveedorInfo> proveedores;
            try
            {
                Logger.Info();
                var ProveedorBl = new ProveedorBL();
                proveedores = ProveedorBl.ObtenerFoliosPorPaginaFletesInterno(pagina, filtro);
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
            return proveedores;
        }

        // Consulta los proveedores que tiene asignado un producto en la tabla fletes internos por folio
        public ProveedorInfo ObtenerPorFolioFletesInternos(ProveedorInfo filtro)
        {
            ResultadoInfo<ProveedorInfo> listaProveedoresInfo;
            ProveedorInfo proveedorInfo = null;
            try
            {
                Logger.Info();
                PaginacionInfo pagina = new PaginacionInfo();
                pagina.Inicio = 1;
                pagina.Limite = 1;
                var proveedorBl = new ProveedorBL();
                listaProveedoresInfo = proveedorBl.ObtenerFoliosPorPaginaFletesInterno(pagina, filtro);
                if (listaProveedoresInfo != null)
                {
                    proveedorInfo = listaProveedoresInfo.Lista[0];
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
            return proveedorInfo;
        }

        /// <summary>
        /// Obtiene los proveedores que tienen contrato
        /// con el producto seleccionado
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ProveedorInfo ObtenerPorProductoContratoCodigoSAP(ProveedorInfo filtro)
        {
            ProveedorInfo proveedorInfo = null;
            try
            {
                Logger.Info();
                var proveedorBl = new ProveedorBL();
                proveedorInfo = proveedorBl.ObtenerPorProductoContratoCodigoSAP(filtro);
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
            return proveedorInfo;
        }

        /// <summary>
        /// Obtiene los proveedores que tienen contrato
        /// con el producto seleccionado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProveedorInfo> ObtenerPorProductoContrato(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ProveedorInfo> resultado;
            try
            {
                Logger.Info();
                var proveedorBl = new ProveedorBL();
                resultado = proveedorBl.ObtenerPorProductoContrato(pagina, filtro);
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

        public DataTable VerificaProveedorSAP(string Sociedad, string IdProveedor)
        {
            DataTable resultado = new DataTable();
            try
            {

                Logger.Info();
                var proveedorBl = new ProveedorBL();
                resultado = proveedorBl.VerificaProveedorSAP(Sociedad,IdProveedor);
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
        /// Obtiene el proveedor del Embarque
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ProveedorInfo ObtenerPorCodigoSAPEmbarque(ProveedorInfo filtro)
        {
            try
            {
                Logger.Info();
                var proveedorBl = new ProveedorBL();
                return proveedorBl.ObtenerPorCodigoSAPEmbarque(filtro);
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
        /// Obtiene los proveedores del Embarque
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProveedorInfo> ObtenerPorPaginaEmbarque(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            try
            {
                Logger.Info();
                var proveedorBl = new ProveedorBL();
                return proveedorBl.ObtenerPorPaginaEmbarque(pagina, filtro);
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

        //public ResultadoInfo<ProveedorInfo> ObtenerFleteroPorPagina(PaginacionInfo pagina, ProveedorInfo filtro)
        //{
        //    ResultadoInfo<ProveedorInfo> result;
        //    try
        //    {
        //        Logger.Info();
        //        var proveedorBL = new ProveedorBL();
        //        result = proveedorBL.ObtenerFleteroPorPagina(pagina, filtro);
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
        /// Indica si existen proveedores activos
        /// </summary>
        /// <returns>Regresa el listado proveedores activos</returns>
        public IList<ProveedorInfo> ObtenerProveedorActivo (EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var lstProveedores = new ProveedorBL();
                return lstProveedores.ObtenerProveedorActivo(estatus);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            } 
        }

        /// <summary>
        ///      Obtiene un Proveedor que cuenta con Origen-Destino Configurado
        /// </summary>
        /// <returns> </returns>
        public ProveedorInfo ObtenerProveedorConfiguradoOrigenDestino(EmbarqueDetalleInfo embarque)
        {
            ProveedorInfo info;
            try
            {
                Logger.Info();
                var proveedorBL = new ProveedorBL();
                info = proveedorBL.ObtenerProveedorConfiguradoOrigenDestino(embarque);
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
        public List<ProveedorInfo> ObtenerProveedoresPorRuta(int estatus, int tipoProveedorID, int ConfiguracionEmbarqueDetalleID)
        {
            try
            {
                Logger.Info();
                var proveedorBL = new ProveedorBL();
                List<ProveedorInfo> lista = proveedorBL.ObtenerProveedoresPorRuta(estatus, tipoProveedorID, ConfiguracionEmbarqueDetalleID);
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
