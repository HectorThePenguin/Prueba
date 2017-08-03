using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Base.Interfaces;
using System.Collections.Generic;

namespace SIE.Services.Servicios.PL
{
    public class BancoPL
    {
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<BancoInfo> ObtenerPorPagina(PaginacionInfo pagina, BancoInfo filtro)
        {
            ResultadoInfo<BancoInfo> resultado;
            try
            {
                Logger.Info();
                var bancoBL = new BancoBL();
                resultado = bancoBL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un banco por Id
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <returns></returns>
        public BancoInfo ObtenerPorID(int bancoID)
        {
            BancoInfo bancoInfo;
            try
            {
                Logger.Info();
                var bancoBL = new BancoBL();
                bancoInfo = bancoBL.ObtenerPorID(bancoID);
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
            return bancoInfo;
        }

        /// <summary>
        /// Obtiene un banco por Id
        /// </summary>
        /// <param name="bancoInfo"></param>
        /// <returns></returns>
        public BancoInfo ObtenerPorID(BancoInfo bancoInfo)
        {
            BancoInfo banInfo;
            try
            {
                Logger.Info();
                var bancoBL = new BancoBL();
                banInfo = bancoBL.ObtenerPorID(bancoInfo);
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
            return banInfo;
        }

        /// <summary>
        /// Obtiene una entidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public BancoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var bancoBL = new BancoBL();
                BancoInfo result = bancoBL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene un banco por el telefono
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public BancoInfo ObtenerPorTelefono(string filtro)
        {
            try
            {
                Logger.Info();
                var bancoBL = new BancoBL();
                BancoInfo result = bancoBL.ObtenerPorTelefono(filtro);
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
        ///     Metodo que guarda un banco
        /// </summary>
        /// <param name="info"></param>
        public void Guardar(BancoInfo info)
        {
            try
            {
                Logger.Info();
                var bancoBL = new BancoBL();
                bancoBL.Guardar(info);
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

        public IList<BancoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var bancoBL = new BancoBL();
                IList<BancoInfo> result = bancoBL.ObtenerTodos();
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

    }
}
