using System;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    internal class CuentaAlmacenSubFamiliaDAL : BaseDAL
    {
        CuentaAlmacenSubFamiliaAccessor cuentaAlmacenSubFamiliaAccessor;

        protected override void inicializar()
        {
            cuentaAlmacenSubFamiliaAccessor = da.inicializarAccessor<CuentaAlmacenSubFamiliaAccessor>();
        }

        protected override void destruir()
        {
            cuentaAlmacenSubFamiliaAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de CuentaAlmacenSubFamilia
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CuentaAlmacenSubFamiliaInfo> ObtenerPorPagina(PaginacionInfo pagina, CuentaAlmacenSubFamiliaInfo filtro)
        {
            try
            {
                Logger.Info();
                ResultadoInfo<CuentaAlmacenSubFamiliaInfo> result = new ResultadoInfo<CuentaAlmacenSubFamiliaInfo>();
                var condicion = da.Tabla<CuentaAlmacenSubFamiliaInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.CuentaAlmacenSubFamiliaID > 0)
                {
                    condicion = condicion.Where(e=> e.CuentaAlmacenSubFamiliaID == filtro.CuentaAlmacenSubFamiliaID);
                }
                //if (!string.IsNullOrEmpty(filtro.CuentaAlmacenSubFamiliaID))
                //{
                //    condicion = condicion.Where(e=> e.CuentaAlmacenSubFamiliaID.Contains(filtro.CuentaAlmacenSubFamiliaID));
                //}
                result.TotalRegistros = condicion.Count();
                
                int inicio = pagina.Inicio;
                int limite = pagina.Limite;
                if (inicio > 1)
                {
                    int limiteReal = (limite - inicio) + 1;
                    inicio = (limite / limiteReal);
                    limite = limiteReal;
                }
                var paginado = condicion
                                .OrderBy(e => e.CuentaAlmacenSubFamiliaID)
                                .Skip((inicio - 1) * limite)
                                .Take(limite);

                result.Lista = paginado.ToList();

                return result;
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de CuentaAlmacenSubFamilia
        /// </summary>
        /// <returns></returns>
        public IQueryable<CuentaAlmacenSubFamiliaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<CuentaAlmacenSubFamiliaInfo>();
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de CuentaAlmacenSubFamilia filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<CuentaAlmacenSubFamiliaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.Activo == estatus);
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad de CuentaAlmacenSubFamilia por su Id
        /// </summary>
        /// <param name="cuentaAlmacenSubFamiliaId">Obtiene una entidad CuentaAlmacenSubFamilia por su Id</param>
        /// <returns></returns>
        public CuentaAlmacenSubFamiliaInfo ObtenerPorID(int cuentaAlmacenSubFamiliaId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.CuentaAlmacenSubFamiliaID == cuentaAlmacenSubFamiliaId).FirstOrDefault();
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad CuentaAlmacenSubFamilia
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(CuentaAlmacenSubFamiliaInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.CuentaAlmacenSubFamiliaID > 0)
                {
                    id = da.Actualizar<CuentaAlmacenSubFamiliaInfo>(info);
                    cuentaAlmacenSubFamiliaAccessor.ActualizarFechaModificacion(info.CuentaAlmacenSubFamiliaID);
                }
                else
                {
                    id = da.Insertar<CuentaAlmacenSubFamiliaInfo>(info);
                }
                return id;
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de CuentaAlmacenSubFamilia
        /// </summary>
        /// <returns></returns>
        public IQueryable<CuentaAlmacenSubFamiliaInfo> ObtenerCostosSubFamilia(int almacenID)
        {
            try
            {
                Logger.Info();
                return da.Tabla<CuentaAlmacenSubFamiliaInfo>().Where(tabla => tabla.AlmacenID == almacenID);
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
