using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Servicios.BL;
using SIE.Base.Exepciones;

namespace SIE.Services.Servicios.PL
{
    public class TipoGanadoPL
    {
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<TipoGanadoInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoGanadoInfo filtro)
        {
            ResultadoInfo<TipoGanadoInfo> result;
            try
            {
                Logger.Info();
                var tipoGanadoBL = new TipoGanadoBL();
                result = tipoGanadoBL.ObtenerPorPagina(pagina, filtro);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<TipoGanadoInfo> Centros_ObtenerPorPagina(PaginacionInfo pagina, TipoGanadoInfo filtro)
        {
            ResultadoInfo<TipoGanadoInfo> result;
            try
            {
                Logger.Info();
                var tipoGanadoBL = new TipoGanadoBL();
                result = tipoGanadoBL.Centros_ObtenerPorPagina(pagina, filtro);
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
        ///     Obtiene un TipoGanadoInfo por Id
        /// </summary>
        /// <param name="infoId"></param>
        /// <returns></returns>
        public TipoGanadoInfo ObtenerPorID(int infoId)
        {
            TipoGanadoInfo info;
            try
            {
                Logger.Info();
                var tipoGanadoBL = new TipoGanadoBL();
                info = tipoGanadoBL.ObtenerPorID(infoId);
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
        ///     Metodo que crear un Tipo Ganado
        /// </summary>
        /// <param name="info"></param>
        public void Crear(TipoGanadoInfo info)
        {
            try
            {
                Logger.Info();
                var tipoGanadoBL = new TipoGanadoBL();
                tipoGanadoBL.Crear(info);
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
        ///     Obtiene un lista de Tipos de Ganado
        /// <returns></returns>
        /// </summary>
        public List<TipoGanadoInfo> ObtenerTodos()
        {
            List<TipoGanadoInfo> tipoGanadoLista;
            try
            {
                Logger.Info();
                var tipoGanadoBL = new TipoGanadoBL();
                tipoGanadoLista = tipoGanadoBL.ObtenerTodos();
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
            return tipoGanadoLista;
        }

        /// <summary>
        ///     Obtiene un lista de Tipos de Ganado
        /// <returns></returns>
        /// </summary>
        public List<TipoGanadoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoGanadoBL = new TipoGanadoBL();
                List<TipoGanadoInfo> tipoGanadoLista = tipoGanadoBL.ObtenerTodos(estatus);

                return tipoGanadoLista;
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
        /// Metodo que obtiene los rangos iniciales por el sexo del ganado
        /// </summary>
        /// <param name="sexo"></param>
        /// <returns></returns>
        public IList<TipoGanadoInfo> ObtenerPorSexo(String sexo)
        {
            IList<TipoGanadoInfo> lista;
            try
            {
                Logger.Info();
                var tipoGanadoBL = new TipoGanadoBL();
                lista = tipoGanadoBL.ObtenerPorSexo(sexo);
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
        /// Metodo que obtiene los rangos finales y tipo de ganado por el sexo del ganado y rango inicial
        /// </summary>
        /// <param name="sexo"></param>
        /// <param name="rangoInicial"></param>
        /// <returns></returns>
        public TipoGanadoInfo ObtenerPorSexoRangoInicial(String sexo, Decimal rangoInicial)
        {
            TipoGanadoInfo tipoGanadoInfo;
            try
            {
                Logger.Info();
                var tipoGanadoBL = new TipoGanadoBL();
                tipoGanadoInfo = tipoGanadoBL.ObtenerPorSexoRangoInicial(sexo, rangoInicial);
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
            return tipoGanadoInfo;
        }

        /// <summary>
        /// Metodo que obtiene El tipo de ganado en base al sexo y el pedo obtenido en la bascula
        /// </summary>
        /// <param name="sexo"></param>
        /// <param name="peso"></param>
        /// <returns></returns>
        public TipoGanadoInfo ObtenerTipoGanadoSexoPeso(String sexo, int peso)
        {
            TipoGanadoInfo tipoGanadoInfo;
            try
            {
                Logger.Info();
                var tipoGanadoBL = new TipoGanadoBL();
                tipoGanadoInfo = tipoGanadoBL.ObtenerTipoGanadoSexoPeso(sexo, peso);
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
            return tipoGanadoInfo;
        }

        /// <summary>
        /// Metodo que obtiene El tipo de ganado en base al TipoGanadoID que se encuentra en al tabla de InterfaceSalidaAnimal
        /// se filtrara por OrganizacionID, SalidaID, Arete y se llenaran en un objeto del tipo InterfaceSalidaInfo
        /// </summary>
        /// <param name="interfaceSalidaInfo"></param>
        /// <returns></returns>
        public TipoGanadoInfo ObtenerTipoGanadoDeInterfaceSalida(InterfaceSalidaInfo interfaceSalidaInfo)
        {
            TipoGanadoInfo tipoGanadoInfo;
            try
            {
                Logger.Info();
                var tipoGanadoBL = new TipoGanadoBL();
                tipoGanadoInfo = tipoGanadoBL.ObtenerTipoGanadoDeInterfaceSalida(interfaceSalidaInfo);
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
            return tipoGanadoInfo;
        }

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TipoGanado
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(TipoGanadoInfo info)
        {
            try
            {
                Logger.Info();
                var tipoGanadoBL = new TipoGanadoBL();
                int result = tipoGanadoBL.Guardar(info);
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
        /// Metodo para Guardar/Modificar una entidad TipoGanado
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Centros_Guardar(TipoGanadoInfo info)
        {
            try
            {
                Logger.Info();
                var tipoGanadoBL = new TipoGanadoBL();
                int result = tipoGanadoBL.Centros_Guardar(info);
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
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public TipoGanadoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoGanadoBL = new TipoGanadoBL();
                TipoGanadoInfo result = tipoGanadoBL.ObtenerPorDescripcion(descripcion);
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
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public TipoGanadoInfo Centros_ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoGanadoBL = new TipoGanadoBL();
                TipoGanadoInfo result = tipoGanadoBL.Centros_ObtenerPorDescripcion(descripcion);
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
        /// /// Obtiene una entidad 
        /// </summary>
        /// <param name="entradaGanadoID"></param>
        /// <returns></returns>
        public TipoGanadoInfo ObtenerTipoGanadoIDPorEntradaGanado(int entradaGanadoID)
        {
            try
            {
                Logger.Info();
                var tipoGanadoBL = new TipoGanadoBL();
                TipoGanadoInfo result = tipoGanadoBL.ObtenerTipoGanadoIDPorEntradaGanado(entradaGanadoID);
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
