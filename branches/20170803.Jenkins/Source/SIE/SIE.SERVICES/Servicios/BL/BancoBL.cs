using System;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Interfaces;
using System.Collections.Generic;

namespace SIE.Services.Servicios.BL
{
    public class BancoBL 
    {
        /// <summary>
        ///     Obtiene un lista paginada de bancos
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<BancoInfo> ObtenerPorPagina(PaginacionInfo pagina, BancoInfo filtro)
        {
            ResultadoInfo<BancoInfo> result;
            try
            {
                Logger.Info();
                var bancoDAL = new BancoDAL();
                result = bancoDAL.ObtenerPorPagina(pagina, filtro);
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
        ///     Obtiene un banco por Id
        /// </summary>
        /// <param name="bancoID"></param>
        /// <returns></returns>
        public BancoInfo ObtenerPorID(int bancoID)
        {
            BancoInfo bancoInfo;
            try
            {
                Logger.Info();
                var bancoDAL = new BancoDAL();
                bancoInfo = bancoDAL.ObtenerPorID(bancoID);
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
        ///     Obtiene un banco por Id
        /// </summary>
        /// <param name="bancoInfo"></param>
        /// <returns></returns>
        public BancoInfo ObtenerPorID(BancoInfo bancoInfo)
        {
            BancoInfo banInfo;
            try
            {
                Logger.Info();
                var bancoDAL = new BancoDAL();
                banInfo = bancoDAL.ObtenerPorID(bancoInfo);
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
        /// Obtiene una entidad Banco por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad Banco por su Id</param>
        /// <returns></returns>
        public BancoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var bancoDAL = new BancoDAL();
                BancoInfo result = bancoDAL.ObtenerPorDescripcion(descripcion);
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

        ///     Obtiene un telefono de un banco
        /// </summary>
        /// <param name="telefono"></param>
        /// <returns></returns>
        public BancoInfo ObtenerPorTelefono(string telefono)
        {
            try
            {
                Logger.Info();
                var bancoDAL = new BancoDAL();
                BancoInfo result = bancoDAL.ObtenerPorTelefono(telefono);
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
                var bancoDAL = new BancoDAL();
                if (info.BancoID != 0)
                {
                    bancoDAL.Actualizar(info);
                }
                else
                {
                    bancoDAL.Crear(info);
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

        public IList<BancoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var bancoDAL = new BancoDAL();
                IList<BancoInfo> result = bancoDAL.ObtenerTodos();
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
