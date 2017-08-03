using System;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    internal class TipoGanadoBL
    {
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<TipoGanadoInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoGanadoInfo filtro)
        {
            ResultadoInfo<TipoGanadoInfo> tipoGanadoLista;
            try
            {
                Logger.Info();
                var tipoGanadoDAL = new TipoGanadoDAL();
                tipoGanadoLista = tipoGanadoDAL.ObtenerPorPagina(pagina, filtro);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<TipoGanadoInfo> Centros_ObtenerPorPagina(PaginacionInfo pagina, TipoGanadoInfo filtro)
        {
            ResultadoInfo<TipoGanadoInfo> tipoGanadoLista;
            try
            {
                Logger.Info();
                var tipoGanadoDAL = new TipoGanadoDAL();
                tipoGanadoLista = tipoGanadoDAL.Centros_ObtenerPorPagina(pagina, filtro);
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
        ///     Metodo que actualiza un Tipo Ganado
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(TipoGanadoInfo info)
        {
            try
            {
                Logger.Info();
                var tipoGanadoDAL = new TipoGanadoDAL();
                tipoGanadoDAL.Actualizar(info);
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
        ///     Obtiene un TipoGanadoInfo por Id
        /// </summary>
        /// <param name="infoId"></param>
        /// <returns></returns>
        internal TipoGanadoInfo ObtenerPorID(int infoId)
        {
            TipoGanadoInfo info;
            try
            {
                Logger.Info();
                var tipoGanadoDAL = new TipoGanadoDAL();
                info = tipoGanadoDAL.ObtenerPorID(infoId);
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
        internal void Crear(TipoGanadoInfo info)
        {
            try
            {
                Logger.Info();
                var tipoGanadoDAL = new TipoGanadoDAL();
                tipoGanadoDAL.Crear(info);
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
        internal List<TipoGanadoInfo> ObtenerTodos()
        {
            List<TipoGanadoInfo> tipoGanadoLista;
            try
            {
                Logger.Info();
                var tipoGanadoDAL = new TipoGanadoDAL();
                tipoGanadoLista = tipoGanadoDAL.ObtenerTodos();
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
        internal List<TipoGanadoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoGanadoDAL = new TipoGanadoDAL();
                List<TipoGanadoInfo> tipoGanadoLista = tipoGanadoDAL.ObtenerTodos(estatus);

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
        internal IList<TipoGanadoInfo> ObtenerPorSexo(String sexo)
        {
            IList<TipoGanadoInfo> lista;
            try
            {
                Logger.Info();
                var tipoGanadoDAL = new TipoGanadoDAL();
                lista = tipoGanadoDAL.ObtenerPorSexo(sexo);
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
        /// Metodo que regresa un TipoGanadoInfo con el rango final y tipo de ganado
        /// por el sexo del ganado y el rango inicial
        /// </summary>
        /// <param name="sexo"></param>
        /// <param name="rangoInicial"></param>
        /// <returns></returns>
        internal TipoGanadoInfo ObtenerPorSexoRangoInicial(String sexo, decimal rangoInicial)
        {
            TipoGanadoInfo tipoGanadoInfo;
            try
            {
                Logger.Info();
                var tipoGanadoDAL = new TipoGanadoDAL();
                tipoGanadoInfo = tipoGanadoDAL.ObtenerPorSexoRangoInicial(sexo, rangoInicial);
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
        internal TipoGanadoInfo ObtenerTipoGanadoSexoPeso(String sexo, int peso)
        {
            TipoGanadoInfo tipoGanadoInfo;
            try
            {
                Logger.Info();
                var tipoGanadoDAL = new TipoGanadoDAL();
                tipoGanadoInfo = tipoGanadoDAL.ObtenerTipoGanadoSexoPeso(sexo, peso);
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
        internal TipoGanadoInfo ObtenerTipoGanadoDeInterfaceSalida(InterfaceSalidaInfo interfaceSalidaInfo)
        {
            TipoGanadoInfo tipoGanadoInfo;
            try
            {
                Logger.Info();
                var tipoGanadoDAL = new TipoGanadoDAL();
                tipoGanadoInfo = tipoGanadoDAL.ObtenerTipoGanadoDeInterfaceSalida(interfaceSalidaInfo);
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
        /// <param name="info"></param>
        internal int Guardar(TipoGanadoInfo info)
        {
            try
            {
                Logger.Info();
                var tipoGanadoDAL = new TipoGanadoDAL();
                int result = info.TipoGanadoID;
                if (info.TipoGanadoID == 0)
                {
                    result = tipoGanadoDAL.Crear(info);
                }
                else
                {
                    tipoGanadoDAL.Actualizar(info);
                }
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
        /// <param name="info"></param>
        internal int Centros_Guardar(TipoGanadoInfo info)
        {
            try
            {
                Logger.Info();
                var tipoGanadoDAL = new TipoGanadoDAL();
                int result = info.TipoGanadoID;
                if (info.TipoGanadoID == 0)
                {
                    result = tipoGanadoDAL.Centros_Crear(info);
                }
                else
                {
                    tipoGanadoDAL.Centros_Actualizar(info);
                }
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
        /// Obtiene una entidad TipoGanado por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal TipoGanadoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoGanadoDAL = new TipoGanadoDAL();
                TipoGanadoInfo result = tipoGanadoDAL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene una entidad TipoGanado por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal TipoGanadoInfo Centros_ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoGanadoDAL = new TipoGanadoDAL();
                TipoGanadoInfo result = tipoGanadoDAL.Centros_ObtenerPorDescripcion(descripcion);
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
        /// Obtiene el tipo de ganado dependiendo de la entrada de ganado
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal TipoGanadoInfo ObtenerTipoGanadoIDPorEntradaGanado(int entradaGanadoID)
        {
            try
            {
                Logger.Info();
                var tipoGanadoDAL = new TipoGanadoDAL();
                TipoGanadoInfo result = tipoGanadoDAL.ObtenerTipoGanadoIDPorEntradaGanado(entradaGanadoID);
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

        internal List<TipoGanadoInfo> ObtenerDescripcionesPorIDs(List<int> tiposGanadoIDs)
        {
            try
            {
                Logger.Info();
                var tipoGanadoDAL = new TipoGanadoDAL();
                return tipoGanadoDAL.ObtenerDescripcionesPorIDs(tiposGanadoIDs);
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

        internal List<ContenedorTipoGanadoPoliza> ObtenerTipoPorAnimal(List<AnimalInfo> animales, TipoMovimiento tipoMovimiento)
        {
            try
            {
                Logger.Info();
                var tipoGanadoDAL = new TipoGanadoDAL();
                return tipoGanadoDAL.ObtenerTipoPorAnimal(animales, tipoMovimiento);
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
