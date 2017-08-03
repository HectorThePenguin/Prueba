using System;
using System.Reflection;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Info.Filtros;
using SIE.Base.Exepciones;
using SIE.Services.Servicios.BL;
using SIE.Services.Info.Enums;
using System.Collections.Generic;
using System.Linq;

namespace SIE.Services.Servicios.PL
{
    public class EnvioAlimentoPL
    {
        ParametroGeneralInfo _parametroEnvioAlimento;

        /// <summary>
        /// información del parametro general del envío
        /// </summary>
        public ParametroGeneralInfo ParametroEnvioAlimento
        {
            get { return _parametroEnvioAlimento; }
            set { _parametroEnvioAlimento = value; }
        }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public EnvioAlimentoPL()
        {
            this._parametroEnvioAlimento = null;
        }

        /// <summary>
        /// Obtiene el listado de las familias configuradas
        /// </summary>
        /// <returns>Regresa una lista de subfamilias configuradas en el parametro "Ayuda envio producto"</returns>
        public List<SubFamiliaInfo> ObtenerSubFamiliasConfiguradas()
        {
            List<SubFamiliaInfo> lstSubFamiliasConf = new List<SubFamiliaInfo>();
            try{
                List<SubFamiliaInfo> lstSubFamilias = (List<SubFamiliaInfo>)new SubFamiliaBL().ObtenerTodos(EstatusEnum.Activo);
                foreach (string subFamilia in this._parametroEnvioAlimento.Valor.Split('|').ToList())
                {
                    lstSubFamiliasConf.AddRange(lstSubFamilias.FindAll(sub => sub.SubFamiliaID.ToString().PadLeft(2, '0') == subFamilia));
                }
            }
            catch (Exception ex){
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lstSubFamiliasConf;
        }

        /// <summary>
        /// Revisa si el parametro para el envio del alimento se encuentran cofigurado
        /// </summary>
        /// <returns>Regresa si el parametro "Ayuda envio producto" esta configurado</returns>
        public bool ConfiguracionParametroEnvioAlimento
        {
            get {
                try{
                    this._parametroEnvioAlimento = new ParametroGeneralBL().ObtenerPorClaveParametro(ParametrosEnum.AYUDAENVIOPRODUCTO.ToString());
                    return (this._parametroEnvioAlimento != null);
                }
                catch (Exception ex){
                    Logger.Error(ex);
                    throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
                }
            }
        }

        /// <summary>
        /// Indica si existen organizaciones registradas y activas
        /// </summary>
        /// <returns>Regresa si ahy organizaciones activas</returns>
        public bool HayOrganizacionesActivas
        {
            get
            {
                try
                {
                    IList<OrganizacionInfo> lstOrganizaciones = new OrganizacionBL().ObtenerTodos(EstatusEnum.Activo);
                    if (lstOrganizaciones != null)
                    {
                        return (lstOrganizaciones.Count > 0);
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
                }
            }
        }

        /// <summary>
        /// Verifica si la subfamilia tiene productos configurados
        /// </summary>
        /// <param name="subFamiliaID">Identificador de la subfamilia</param>
        /// <returns>Regresa si una subfamilia tiene o no productos configurados</returns>
        public bool HayProductosConfigurados(int subFamiliaID)
        {
            List<ProductoInfo> lProductoInfo = new ProductoBL().ObtenerPorSubFamilia
                (new ProductoInfo{ 
                    SubfamiliaId = subFamiliaID, 
                    Descripcion = string.Empty, 
                    ProductoId = 0});
            if (lProductoInfo == null | lProductoInfo.Count == 0){
                return false;
            }

            return true;
        }

        /// <summary>
        /// Obtiene el listado de alamacenes en los que se encuentra el producto 
        /// con inventario y estan asignados a la organizacion del usuario
        /// </summary>
        /// <param name="usuarioId">Usuario del producto</param>
        /// <returns>List<AlmacenInfo>Regresa una lista de almacenes con existencias del producto indicado</returns>
        public List<AlmacenInfo> ObtenerAlmacenesProducto(FiltroAlmacenProductoEnvio filtroEnvio)
        {
            List<AlmacenInfo> lstAlamcenes = new List<AlmacenInfo>();
            try
            {
                return new AlmacenBL().ObtenerAlamcenPorProducto(filtroEnvio);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene la información del precio promedio de un producto
        /// </summary>
        /// <param name="almacenInventarioInfo">Información del almacen inventario</param>
        /// <returns>AlmacenInventarioInfo</returns>
        public AlmacenInventarioInfo ObtenerCantidadPrecioPromedioPorAlmacenID(AlmacenInventarioInfo almacenInventarioInfo)
        {
            AlmacenInventarioInfo result = new AlmacenInventarioInfo();
            try
            {
                var almacenInventarioPL = new AlmacenInventarioPL();
                result = almacenInventarioPL.ObtenerPorAlmacenIdProductoId(almacenInventarioInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene la información del los lotes de cuerdo al almacen y el roducto
        /// </summary>
        /// <param name="almacen">Información del almacen</param>
        /// <param name="producto">Información del producto</param>
        /// <returns>List<AlmacenInventarioLoteInfo>Regresa una lista de lotes activos de un producto indicado en un almacen indicado</returns>
        public List<AlmacenInventarioLoteInfo> ObtenerLotesPorAlmacenProducto(AlmacenInfo almacen, ProductoInfo producto)
        {
            List<AlmacenInventarioLoteInfo> lstLotes = new List<AlmacenInventarioLoteInfo>();
            try
            {
                lstLotes = new AlmacenInventarioLoteBL().ObtenerPorAlmacenProducto(almacen, producto);
                lstLotes.RemoveAll(obj => obj.Cantidad <= 0);
                lstLotes = lstLotes.OrderBy(obj => obj.Lote).ToList();
                return lstLotes;
            }
              catch (Exception ex)
              {
                  Logger.Error(ex);
                  throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
              }
        }

        /// <summary>
        /// Registra el envìo del alimento
        /// </summary>
        /// <param name="envioAlimento">Información del envío del alimento</param>
        /// <returns>Regresa una confirmacion del registro de envio de alimento</returns>
        public EnvioAlimentoInfo RegistrarEnvioAlimento(EnvioAlimentoInfo envioAlimento)
        {
            EnvioAlimentoInfo envio = null;
            try
            {
                Logger.Info();
                var salidaProductoBL = new EnvioAlimentoBL();
                envio = salidaProductoBL.RegistrarEnvioAlimento(envioAlimento);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return envio;
        }

        /// <summary>
        /// Obtiene la información de la CostoProducto de acuerdo al almacen y producto
        /// </summary>
        /// <param name="almacenID">Identificador del almacen donde se encuentra la mercancia del producto</param>
        /// <param name="producto">Identificacador del producto</param>
        /// <returns>ClaseCostoProductoInfo</returns>
        public ClaseCostoProductoInfo ObtenerCostoAlmacenProducto(int almacenID, int producto)
        {
            ClaseCostoProductoInfo costo = null;
            try
            {
                Logger.Info();
                var costoProductoBL = new ClaseCostoProductoBL();
                costo = costoProductoBL.ObtenerPorProductoAlmacen(producto, almacenID);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return costo;
        }

        /// <summary>
        /// Obtiene el parametro de configuración del paramtro de cuenta sap en transito de la orgnaización destino
        /// </summary>
        /// <param name="organizacionDestino">Organizacón a la que se envia el alimento</param>
        /// <returns>Regresa información del parametro de "Cuenta inventario en transito" de una organizacion</returns>
        public ParametroOrganizacionInfo ObtenerParametroCuentaTransito(OrganizacionInfo organizacionOrigen)
        {
            ParametroOrganizacionInfo oParametroOrganizacion = null;
            try{
                Logger.Info();
                List<ParametroInfo> lstParametros = new ParametroBL().ObtenerTodos(EstatusEnum.Activo).ToList();
                if (lstParametros.Exists(obj=> obj.Clave == ParametrosEnum.CuentaInventarioTransito.ToString()))
                {
                    oParametroOrganizacion = new ParametroOrganizacionInfo();
                    oParametroOrganizacion.Parametro = new ParametroInfo();
                    oParametroOrganizacion.Parametro = lstParametros.Find(obj => obj.Clave == ParametrosEnum.CuentaInventarioTransito.ToString());
                    oParametroOrganizacion.Organizacion = organizacionOrigen;
                    oParametroOrganizacion = new ParametroOrganizacionBL().ObtenerPorParametroOrganizacionID(oParametroOrganizacion);
                    if (oParametroOrganizacion == null)
                    {
                        oParametroOrganizacion = new ParametroOrganizacionInfo { ParametroOrganizacionID = 0 };
                    }
                }
            } catch (Exception ex) {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return oParametroOrganizacion;
        }

        /// <summary>
        /// Valida si esta registrado y activo el parametro de "Ayuda Envio Producto"
        /// </summary>
        /// <returns>Regresa true si existe el parametro con la clave "AyudaEnvioProducto" y con estatus activo.</returns>
        public bool ValidarExisteParametroAyudaEnvioProducto() 
        {
            ParametroBL parametroBl = new ParametroBL();
            List<ParametroInfo> parametros = parametroBl.ObtenerTodos().ToList();
            ParametroInfo parametroEncontrado;
            if (parametros == null)
            {
                return false;
            }
            parametroEncontrado = parametros.Where(parametro => parametro.Clave == ParametrosEnum.AYUDAENVIOPRODUCTO.ToString()).FirstOrDefault();
            if(parametroEncontrado==null)
            {
                return false;
            }

            return parametroEncontrado.Activo == EstatusEnum.Activo;
        } 
    }
}
