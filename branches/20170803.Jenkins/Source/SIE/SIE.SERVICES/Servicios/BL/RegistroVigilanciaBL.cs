using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class RegistroVigilanciaBL
    {
        /// <summary>
        /// Obtiene un registro de vigilancia
        /// </summary>
        /// <param name="registroVigilanciaInfo"></param>
        /// <returns>RegistroVigilanciaInfo</returns>
        internal RegistroVigilanciaInfo ObtenerRegistroVigilanciaPorId(RegistroVigilanciaInfo registroVigilanciaInfo)
        {
            RegistroVigilanciaInfo registroVigilancia;
            try
            {
                Logger.Info();
                var registroVigilanciaDAL = new RegistroVigilanciaDAL();
                registroVigilancia = registroVigilanciaDAL.ObtenerPorId(registroVigilanciaInfo);

                if (registroVigilancia != null)
                {
                    var camionBl = new CamionBL();
                    registroVigilancia.Camion =
                        camionBl.ObtenerPorID(registroVigilancia.Camion.CamionID);

                    var productoBL = new ProductoBL();
                    registroVigilancia.Producto = productoBL.ObtenerPorID(registroVigilancia.Producto);

                    var proveedorBL = new ProveedorBL();
                    registroVigilancia.ProveedorMateriasPrimas =
                        proveedorBL.ObtenerPorID(registroVigilancia.ProveedorMateriasPrimas.ProveedorID);

                    var proveedorChoferBl = new ProveedorChoferBL();
                    registroVigilancia.ProveedorChofer =
                        proveedorChoferBl.ObtenerProveedorChoferPorId(registroVigilancia.ProveedorChofer.ProveedorChoferID);

                    var contratoBl = new ContratoBL();
                    registroVigilancia.Contrato.Organizacion = new OrganizacionInfo() { OrganizacionID = registroVigilancia.Organizacion.OrganizacionID };
                    registroVigilancia.Contrato = contratoBl.ObtenerPorId(registroVigilancia.Contrato);
                }
            }catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return registroVigilancia;
        }

        /// <summary>
        /// Obtiene un registro de vigilancia
        /// </summary>
        /// <param name="registroVigilanciaInfo"></param>
        /// <returns>RegistroVigilanciaInfo</returns>
        internal RegistroVigilanciaInfo ObtenerRegistroVigilanciaPorFolioTurno(RegistroVigilanciaInfo registroVigilanciaInfo)
        {
            RegistroVigilanciaInfo registroVigilancia;
            try
            {
                Logger.Info();
                var registroVigilanciaDAL = new RegistroVigilanciaDAL();
                registroVigilancia = registroVigilanciaDAL.ObtenerPorFolioTurno(registroVigilanciaInfo);
                
                if (registroVigilancia != null)
                {
                    registroVigilancia = ObtenerRegistroVigilanciaPorId(registroVigilancia);   
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return registroVigilancia;
        }

        /// <summary>
        /// Obtiene un registro de vigilancia independientemente de si se encuentra activo o no.
        /// </summary>
        /// <param name="registroVigilanciaInfo"></param>
        /// <returns>RegistroVigilanciaInfo</returns>
        internal RegistroVigilanciaInfo ObtenerRegistroVigilanciaPorFolioTurnoActivoInactivo(RegistroVigilanciaInfo registroVigilanciaInfo)
        {
            RegistroVigilanciaInfo registroVigilancia;
            try
            {
                Logger.Info();
                var registroVigilanciaDAL = new RegistroVigilanciaDAL();
                registroVigilancia = registroVigilanciaDAL.ObtenerPorFolioTurnoActivoInactivo(registroVigilanciaInfo);

                if (registroVigilancia != null)
                {
                    registroVigilancia = ObtenerRegistroVigilanciaPorId(registroVigilancia);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return registroVigilancia;
        }

        /// <summary>
        /// Se utiliza para guardar datos en la tabla RegistroVigilancia
        /// </summary>
        /// <param name="registrovigilanciainfo"></param>
        /// <returns></returns>
        internal int GuardarDatos(RegistroVigilanciaInfo registrovigilanciainfo)
        {
            try
            {
                    Logger.Info();
                    var registrovigilanciaDal = new RegistroVigilanciaDAL();
                    int resultado = registrovigilanciaDal.GuardarDatos(registrovigilanciainfo);
                    return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Modifica los campos de fecha salida y activo = 0 en la tabla "RegistroVigilancia". de esta forma se registra a que hora salio el camion
        /// </summary>
        /// <param name="registrovigilanciainfo"></param>
        internal void RegistroSalida(RegistroVigilanciaInfo registrovigilanciainfo)
        {
            try
            {
                    Logger.Info();
                    var registrovigilanciaDal = new RegistroVigilanciaDAL();
                    registrovigilanciaDal.RegistroSalida(registrovigilanciainfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal RegistroVigilanciaInfo ObtenerRegistroVigilanciaPorIdAyudaForraje(RegistroVigilanciaInfo registroVigilanciaInfo)
        {
            RegistroVigilanciaInfo registroVigilancia;
            try
            {
                Logger.Info();
                var registroVigilanciaDAL = new RegistroVigilanciaDAL();
                registroVigilancia = registroVigilanciaDAL.ObtenerPorId(registroVigilanciaInfo);

                if (registroVigilancia != null)
                {
                    var proveedorBL = new ProveedorBL();
                    registroVigilancia.ProveedorMateriasPrimas =
                        proveedorBL.ObtenerPorID(registroVigilancia.ProveedorMateriasPrimas.ProveedorID);
                }
                return registroVigilancia;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Se utiliza para guardar datos en la tabla RegistroVigilancia
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<RegistroVigilanciaInfo> ObtenerPorPagina(PaginacionInfo pagina, RegistroVigilanciaInfo filtro)
        {
            try
            {
                Logger.Info();
                var registrovigilanciaDal = new RegistroVigilanciaDAL();
                ResultadoInfo<RegistroVigilanciaInfo> resultado = registrovigilanciaDal.ObtenerPorPagina(pagina, filtro);
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Valida que el las placas del camion ingresado no esté activo y tenga fecha de salida
        /// </summary>
        /// <param name="camion"></param>
        /// <returns></returns>
        public bool ObtenerDisponibilidadCamion(CamionInfo camion)
        {
            bool resultado;
            try
            {
                Logger.Info();
                var registroVigilanciaDal = new RegistroVigilanciaDAL();
                resultado = registroVigilanciaDal.ObtenerDisponibilidadCamion(camion);
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
    }
}
