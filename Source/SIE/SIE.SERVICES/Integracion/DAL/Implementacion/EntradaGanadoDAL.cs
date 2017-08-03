using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class EntradaGanadoDAL : DALBase
    {
        /// <summary>
        ///     Metodo que crear una entrada de ganado
        /// </summary>
        /// <param name="entradaGanado"></param>
        internal int GuardarEntradaGanado(EntradaGanadoInfo entradaGanado)
        {
            int entradaGanadoID;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosGuardado(entradaGanado);
                entradaGanadoID = Create("EntradaGanado_Crear", parametros);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoID;
        }

        /// <summary>
        /// Metodo que guarda una lista de condicines de ganado
        /// </summary>
        /// <param name="listaEntradaCondicion"></param>
        /// <param name="entradaGanadoID"></param>
        internal void GuardarEntradaCondicion(IEnumerable<EntradaCondicionInfo> listaEntradaCondicion, int entradaGanadoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxEntradaGanadoDAL.ObtenerParametrosGuardadoCondicion(listaEntradaCondicion, entradaGanadoID);
                Create("EntradaGanado_CrearCondicionesGanado", parametros);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo que actualiza una entrada de ganado 
        /// </summary>
        /// <param name="entradaGanado"></param>
        internal void ActualizaEntradaGanado(EntradaGanadoInfo entradaGanado)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosActualizar(entradaGanado);
                Update("EntradaGanado_Actualizar", parametros);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo que obtiene una entrada de ganado por ID
        /// </summary>
        /// <param name="entradaGanadoID"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerPorID(int entradaGanadoID)
        {
            EntradaGanadoInfo entradaGanadoInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosPorId(entradaGanadoID);
                DataSet ds = Retrieve("EntradaGanado_ObtenerPorID", parametros);
                if (ValidateDataSet(ds))
                {
                    entradaGanadoInfo = MapEntradaGanadoDAL.ObtenerPorID(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Metodo que obtiene una entrada de ganado Folio de entrada y Organizacion
        /// </summary>
        /// <param name="folioEntrada"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerPorFolioEntrada(int folioEntrada, int organizacionID)
        {
            EntradaGanadoInfo entradaGanadoInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosPorFolioEntrada(folioEntrada, organizacionID);
                DataSet ds = Retrieve("EntradaGanado_ObtenerPorFolioEntrada", parametros);
                if (ValidateDataSet(ds))
                {
                    entradaGanadoInfo = MapEntradaGanadoDAL.ObtenerPorFolioEntrada(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Metodo que obtiene una entrada de ganado Folio de entrada y Organizacion
        /// </summary>
        /// <param name="folioEntrada"></param>
        /// <param name="Dependencias"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerPorFolioEntrada(int folioEntrada, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            EntradaGanadoInfo entradaGanadoInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosPorFolioEntrada(folioEntrada, Dependencias);
                DataSet ds = Retrieve("EntradaGanado_ObtenerPorFolioEntrada", parametros);
                if (ValidateDataSet(ds))
                {
                    entradaGanadoInfo = MapEntradaGanadoDAL.ObtenerPorID(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }
        /// <summary>
        /// Metodo que obtiene las entradas de ganado Organizacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="tipoCorralID"></param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaGanadoInfo> ObtenerPorIDOrganizacion(int organizacionID, int tipoCorralID)
        {
            ResultadoInfo<EntradaGanadoInfo> entradaGanadoInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosPorIDOrganizacion(organizacionID, tipoCorralID);
                DataSet ds = Retrieve("EntradaGanado_ObtenerPorIDOrganizacion", parametros);
                if (ValidateDataSet(ds))
                {
                    entradaGanadoInfo = MapEntradaGanadoDAL.ObtenerPorIDOrganizacionEVP(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }


        /// <summary>
        /// Obtiene un listado de entradas Activas Paginadas
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaGanadoInfo> ObtenerEntradasActivasPorPagina(PaginacionInfo pagina, int organizacionID)
        {
            ResultadoInfo<EntradaGanadoInfo> resultadoGanadoInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosEntradasActivasPorPagina(pagina, organizacionID);
                DataSet ds = Retrieve("EntradaGanado_ObtenerEntradasActivasPorPagina", parametros);
                if (ValidateDataSet(ds))
                {
                    resultadoGanadoInfo = MapEntradaGanadoDAL.ObtenerEntradasActivasPorPagina(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultadoGanadoInfo;
        }


        /// <summary>
        /// Metodo que obtiene las entradas de ganado Organizacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// /// <param name="tipoCorralID"></param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaGanadoInfo> ObtenerPorTipoCorral(int organizacionID, int tipoCorralID)
        {
            ResultadoInfo<EntradaGanadoInfo> entradaGanadoInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxEntradaGanadoDAL.ObtenerParametrosPorTipoCorral(organizacionID, tipoCorralID);
                DataSet ds = Retrieve("EntradaGanado_ObtenerPorTipoCorralID", parametros);
                if (ValidateDataSet(ds))
                {
                    entradaGanadoInfo = MapEntradaGanadoDAL.ObtenerPorTipoCorralID(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Metodo que obtiene una entrada de ganado Folio de entrada y Organizacion
        /// </summary>
        /// <param name="entradaGanadoInfo"></param>
        /// <param name="Dependencias"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerPorDependencias(EntradaGanadoInfo entradaGanadoInfo, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            EntradaGanadoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosPorFolioEntrada(entradaGanadoInfo, Dependencias);
                DataSet ds = Retrieve("EntradaGanado_ObtenerPorFolioEntrada", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoDAL.ObtenerPorDependencias(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene las Entradas de Ganado por Pagina
        /// </summary>
        /// <param name="Pagina">Indica la manera en que se realizara el Paginado</param>
        /// <param name="entradaGanadoInfo">Folio Por el Cual se Filtrara</param>
        /// <param name="Dependencias">Dependencias con Catalogos</param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaGanadoInfo> ObtenerPorDependencias(PaginacionInfo Pagina, EntradaGanadoInfo entradaGanadoInfo, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            ResultadoInfo<EntradaGanadoInfo> resultadoGanadoInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosEntradasPorPagina(Pagina, entradaGanadoInfo, Dependencias);
                DataSet ds = Retrieve("EntradaGanado_ObtenerPorPagina", parametros);
                if (ValidateDataSet(ds))
                {
                    resultadoGanadoInfo = MapEntradaGanadoDAL.ObtenerEntradasPorPagina(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultadoGanadoInfo;
        }

        /// <summary>
        /// Obtiene las Entradas de Ganado por Pagina
        /// </summary>
        /// <param name="Pagina">Indica la manera en que se realizara el Paginado</param>
        /// <param name="FolioEntrada">Folio Por el Cual se Filtrara</param>
        /// <param name="Dependencias">Dependencias con Catalogos</param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaGanadoInfo> ObtenerPorDescripcion(PaginacionInfo Pagina, int FolioEntrada, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            ResultadoInfo<EntradaGanadoInfo> resultadoGanadoInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosEntradasPorPagina(Pagina, FolioEntrada, Dependencias);
                DataSet ds = Retrieve("EntradaGanado_ObtenerPorPagina", parametros);
                if (ValidateDataSet(ds))
                {
                    resultadoGanadoInfo = MapEntradaGanadoDAL.ObtenerEntradasPorPagina(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultadoGanadoInfo;
        }
        /// <summary>
        /// Metodo que obtiene una entrada de ganado por Programacion Embarque ID
        /// </summary>
        /// <param name="embarqueID"></param>
        /// <param name="organizacionOrigenID"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerPorEmbarqueID(int embarqueID, int organizacionOrigenID)
        {
            EntradaGanadoInfo entradaGanadoInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosPorEmbarqueID(embarqueID, organizacionOrigenID);
                DataSet ds = Retrieve("EntradaGanado_ObtenerPorEmbarqueID", parametros);
                if (ValidateDataSet(ds))
                {
                    entradaGanadoInfo = MapEntradaGanadoDAL.ObtenerPorEmbarqueID(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Metodo que obtiene una entrada de ganado por Programacion Embarque ID
        /// </summary>
        /// <param name="embarqueID"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerPorEmbarqueID(int embarqueID)
        {
            EntradaGanadoInfo entradaGanadoInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosPorEmbarqueID(embarqueID);
                DataSet ds = Retrieve("EntradaGanado_ObtenerPorEmbarqueID", parametros);
                if (ValidateDataSet(ds))
                {
                    entradaGanadoInfo = MapEntradaGanadoDAL.ObtenerPorEmbarqueID(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Metodo que obtiene una entrada de ganado Folio de entrada y Organizacion
        /// </summary>
        /// <param name="entradaGanadoInfo"></param>
        /// <param name="Dependencias"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerEntradasGanadoRecibidasPorDependencias(EntradaGanadoInfo entradaGanadoInfo, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            EntradaGanadoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosPorFolioEntrada(entradaGanadoInfo, Dependencias);
                DataSet ds = Retrieve("EntradaGanado_ObtenerPorFolioEntradaRecibido", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoDAL.ObtenerPorDependencias(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene las Entradas de Ganado por Pagina
        /// </summary>
        /// <param name="Pagina">Indica la manera en que se realizara el Paginado</param>
        /// <param name="entradaGanadoInfo">Folio Por el Cual se Filtrara</param>
        /// <param name="Dependencias">Dependencias con Catalogos</param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaGanadoInfo> ObtenerEntradaGanadoRecibidasPaginaPorDependencias(PaginacionInfo Pagina, EntradaGanadoInfo entradaGanadoInfo, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            ResultadoInfo<EntradaGanadoInfo> resultadoGanadoInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosEntradasPorPaginaRecibidoDependencia(Pagina, entradaGanadoInfo, Dependencias);
                DataSet ds = Retrieve("EntradaGanado_ObtenerPorPaginaRecibido", parametros);
                if (ValidateDataSet(ds))
                {
                    resultadoGanadoInfo = MapEntradaGanadoDAL.ObtenerEntradasPorPagina(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultadoGanadoInfo;
        }

        /// <summary>
        /// Metodo que obtiene una entrada de ganado Folio de entrada y Organizacion
        /// </summary>
        /// <param name="folioEntrada"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerEntradasGanadoRecibidas(int folioEntrada, int organizacionID)
        {
            EntradaGanadoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosPorFolioEntrada(folioEntrada, organizacionID);
                DataSet ds = Retrieve("EntradaGanado_ObtenerPorFolioEntradaRecibido", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoDAL.ObtenerEntradasGanadoRecibidas(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
        internal ResultadoInfo<EntradaGanadoInfo> ObtenerPorPagina(PaginacionInfo pagina, EntradaGanadoInfo filtro)
        {
            ResultadoInfo<EntradaGanadoInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxEntradaGanadoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("[dbo].[EntradasGanado_ObtenerPorPagina]", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapEntradaGanadoDAL.ObtenerPorPagina(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }
        internal ResultadoInfo<EntradaGanadoInfo> ObtenerPartidasProgramadas(int folioEntrada, int organizacionID)
        {
            ResultadoInfo<EntradaGanadoInfo> entradaGanadoInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosEntradaProgramadas(folioEntrada, organizacionID);
                DataSet ds = Retrieve("EntradaGanado_Programadas", parametros);
                if (ValidateDataSet(ds))
                {
                    entradaGanadoInfo = MapEntradaGanadoDAL.ObtenerPartidasProgramadas(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }

        internal ResultadoInfo<EntradaGanadoInfo> ObtenerPartidasProgramadasPorPaginacion(PaginacionInfo pagina, int folioEntrada, int organizacionID)
        {
            ResultadoInfo<EntradaGanadoInfo> entradaGanadoInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosEntradaProgramadasPorPaginas(pagina, folioEntrada, organizacionID);
                DataSet ds = Retrieve("EntradaGanado_ProgramadasPorPagina", parametros);
                if (ValidateDataSet(ds))
                {
                    entradaGanadoInfo = MapEntradaGanadoDAL.ObtenerPartidasProgramadasPorPagina(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }
        /// <summary>
        ///     Obtiene CalidadGanado PorSexo
        /// </summary>
        /// <returns></returns>
        internal IList<CalidadGanadoInfo> ObtenerCalidadPorSexo(string sexo)
        {
            IList<CalidadGanadoInfo> lista = null;
            try
            {
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerCalidadPorSexo(sexo);
                DataSet ds = Retrieve("[dbo].[CalidadGanado_ObtenerPorSexo]", parametros);
                if (ValidateDataSet(ds))
                {
                    lista = MapEntradaGanadoDAL.ObtenerCalidadPorSexo(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }
        /// <summary>
        ///     Obtiene CalidadGanado Por CausaRechazo
        /// </summary>
        /// <returns></returns>
        internal IList<CausaRechazoInfo> ObtenerCalidadPorCausaRechazo()
        {
            IList<CausaRechazoInfo> lista = null;
            try
            {
                DataSet ds = Retrieve("[dbo].[CausaRechazo_Obtener]");
                if (ValidateDataSet(ds))
                {
                    lista = MapEntradaGanadoDAL.ObtenerCalidadPorCausaRechazo(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

        /// <summary>
        /// Metodo que obtiene el catalogo de clasificacion
        /// </summary>
        /// <returns></returns>
        internal IList<ClasificacionGanadoInfo> ObtenerCatClasificacion()
        {
            IList<ClasificacionGanadoInfo> lista = null;
            try
            {
                var ds = Retrieve("[dbo].[CorteGanado_ObtenerClasificacionGanado]");
                if (ValidateDataSet(ds))
                {
                    lista = MapEntradaGanadoDAL.ObtenerCatClasificacion(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

        /// <summary>
        /// Metodo que obtiene una entrada de ganado Folio de entrada y Organizacion
        /// </summary>
        /// <param name="entradaGanadoInfo"></param>
        /// <param name="Dependencias"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerEntradasGanadoCosteadoPorDependencias(EntradaGanadoInfo entradaGanadoInfo, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            EntradaGanadoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosPorFolioEntrada(entradaGanadoInfo, Dependencias);
                DataSet ds = Retrieve("EntradaGanado_ObtenerPorFolioEntradaCosteado", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoDAL.ObtenerPorDependencias(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene las Entradas de Ganado por Pagina
        /// </summary>
        /// <param name="Pagina">Indica la manera en que se realizara el Paginado</param>
        /// <param name="entradaGanadoInfo">Folio Por el Cual se Filtrara</param>
        /// <param name="Dependencias">Dependencias con Catalogos</param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaGanadoInfo> ObtenerEntradaGanadoCosteadoPaginaPorDependencias(PaginacionInfo Pagina, EntradaGanadoInfo entradaGanadoInfo, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            ResultadoInfo<EntradaGanadoInfo> resultadoGanadoInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosEntradasPorPaginaCosteadoDependencia(Pagina, entradaGanadoInfo, Dependencias);
                DataSet ds = Retrieve("EntradaGanado_ObtenerPorPaginaCosteado", parametros);
                if (ValidateDataSet(ds))
                {
                    resultadoGanadoInfo = MapEntradaGanadoDAL.ObtenerEntradasPorPagina(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultadoGanadoInfo;
        }

        /// <summary>
        /// Metodo que obtiene una entrada de ganado Folio de entrada y Organizacion
        /// </summary>
        /// <param name="folioEntrada"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerEntradasGanadoCosteado(int folioEntrada, int organizacionID)
        {
            EntradaGanadoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosPorFolioEntrada(folioEntrada, organizacionID);
                DataSet ds = Retrieve("EntradaGanado_ObtenerPorFolioEntradaCosteado", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoDAL.ObtenerPorFolioEntrada(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Metodo que actualiza una entrada de ganado 
        /// </summary>
        /// <param name="entradaGanado"></param>
        internal void ActualizaEntradaGanadoCosteo(EntradaGanadoInfo entradaGanado)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosActualizarCosteado(entradaGanado);
                Update("EntradaGanado_ActualizarCosteo", parametros);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para consultar los datos de la entrada para la captura de Calidad de Ganado
        /// </summary>
        /// <param name="filtroCalificacionGanado"></param>
        internal EntradaGanadoInfo ObtenerEntradaGanadoCapturaCalidad(FiltroCalificacionGanadoInfo filtroCalificacionGanado)
        {
            try
            {
                EntradaGanadoInfo result = null;
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosObtenerEntradaGanadoCapturaCalidad(filtroCalificacionGanado);
                DataSet ds = Retrieve("EntradaGanado_ObtenerParaCapturaCalidad", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoDAL.ObtenerEntradaGanadoCapturaCalidad(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo que actualiza las cabezas muertas
        /// </summary>
        /// <param name="filtroCalificacionGanadoInfo"></param>
        internal void ActualizarCabezasMuertas(FiltroCalificacionGanadoInfo filtroCalificacionGanadoInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosActualizarCabezasMuertas(filtroCalificacionGanadoInfo);
                Update("EntradaGanado_ActualizarCabezasMuertas", parametros);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }


        /// <summary>
        /// Metodo que obtiene una entrada de ganado Folio de entrada y Organizacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="corralID"> </param>
        /// <param name="embarqueID"> </param>
        /// <returns></returns>
        internal int ObtenerPorCorralDisponible(int organizacionID, int corralID, int embarqueID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosPorCorralDisponible(organizacionID, corralID, embarqueID);
                DataSet ds = Retrieve("EntradaGanado_ObtenerPorCorralDisponible", parametros);
                int result = 0;
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoDAL.ObtenerPorCorralDisponible(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        ///  Obtiene la entrada en base a los datos del lote
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerEntradaPorLote(LoteInfo lote)
        {
            EntradaGanadoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerEntradaPorLote(lote);
                var ds = Retrieve("EntradaGanado_ObtenerEntradaPorLote", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoDAL.ObtenerEntradaPorLote(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtener las entradas de ganado en base al corral lote
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        internal List<EntradaGanadoInfo> ObtenerEntradasPorCorralLote(LoteInfo lote)
        {
            List<EntradaGanadoInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerEntradaCorralLote(lote);
                var ds = Retrieve("EntradaGanado_ObtenerEntradasPorCorralLote", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoDAL.ObtenerEntradasPorCorralLote(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtener las entradas de ganado costeadas sin prorratear
        /// </summary>
        /// <param name="tipoOrganizacion"></param>
        /// <returns></returns>
        internal List<EntradaGanadoInfo> ObtenerEntradasCosteadasSinProrratear(int tipoOrganizacion)
        {
            List<EntradaGanadoInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerEntradaCosteadasSinProrratear(tipoOrganizacion);
                var ds = Retrieve("EntradaGanado_ObtenerEntradasCosteadasSinProrratear", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoDAL.ObtenerEntradasCosteadasSinProrratear(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo que obtiene una entrada de ganado Folio de entrada y Organizacion
        /// </summary>
        /// <param name="folioEntrada"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerPorFolioEntradaPorOrganizacion(int folioEntrada, int organizacionID)
        {
            EntradaGanadoInfo entradaGanadoInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosPorFolioEntradaPorOrganizacion(folioEntrada, organizacionID);
                DataSet ds = Retrieve("EntradaGanado_ObtenerPorFolioEntradaOrganizacion", parametros);
                if (ValidateDataSet(ds))
                {
                    entradaGanadoInfo = MapEntradaGanadoDAL.ObtenerPorFolioEntradaPorOrganizacion(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }

        internal ResultadoInfo<EntradaGanadoInfo> ObtenerEntradaPaginado(PaginacionInfo pagina, EntradaGanadoInfo entradaInfo)
        {
            ResultadoInfo<EntradaGanadoInfo> resultadoGanadoInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosEntradaPaginado(pagina, entradaInfo);
                DataSet ds = Retrieve("EntradaGanado_ObtenerPorPaginaCosteado", parametros);
                if (ValidateDataSet(ds))
                {
                    resultadoGanadoInfo = MapEntradaGanadoDAL.ObtenerEntradasPorPaginaPoliza(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultadoGanadoInfo;
        }

        internal EntradaGanadoInfo ObtenerEntradasGanadoRecibidas(EntradaGanadoInfo entradaInfo)
        {
            EntradaGanadoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosPorGanadoRecibidas(entradaInfo);
                DataSet ds = Retrieve("EntradaGanado_ObtenerPorFolioEntradaCosteado", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoDAL.ObtenerEntradasGanadoRecibidas(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtener las entradas de ganado costeadas sin prorratear
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="lotes"></param>
        /// <returns></returns>
        internal List<EntradaGanadoInfo> ObtenerEntradasPorLoteXML(int organizacionId, IList<LoteInfo> lotes)
        {
            List<EntradaGanadoInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerEntradaPorLoteXML(organizacionId,
                                                                                                     lotes);
                var ds = Retrieve("EntradaGanado_ObtenerEntradaPorLoteXML", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoDAL.ObtenerEntradasPorLoteXML(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Actualiza el corral a una entrada de ganado
        /// </summary>
        /// <param name="lote"></param>
        /// <param name="corralInfoDestino"></param>
        /// <param name="usuarioInfo"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ActualizarCorral(LoteInfo lote, CorralInfo corralInfoDestino, UsuarioInfo usuarioInfo)
        {
            EntradaGanadoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosActualizarCorral(lote, corralInfoDestino, usuarioInfo);
                DataSet ds = Retrieve("EntradaGanado_ActualizarCorral", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoDAL.ObtenerEntradaPorLote(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Metodo para obtener una partida de compra directa de una lista de partidas para un lote
        /// </summary>
        /// <param name="entradaSeleccionada"></param>
        /// <returns></returns>
        internal int ObtenerPartidaCompraDirecta(EntradaGanadoInfo entradaSeleccionada)
        {
            int result;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosObtenerPartidaCompraDirecta(entradaSeleccionada);
                result = RetrieveValue<int>("EntradaGanado_ObtenerPartidaCompraDirecta", parametros);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene entradas de ganado por EntradaGanadoID
        /// </summary>
        /// <param name="entradas"></param>
        /// <returns></returns>
        internal List<EntradaGanadoInfo> ObtenerEntradasPorIDs(List<int> entradas)
        {
            List<EntradaGanadoInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerEntradasPorIDs(entradas);
                var ds = Retrieve("EntradaGanado_ObtenerEntradaPorIDXML", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoDAL.ObtenerEntradasPorIDs(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo que obtiene una entrada de ganado por Programacion Embarque ID
        /// </summary>
        /// <param name="embarqueID"></param>
        /// <returns></returns>
        internal List<EntradaGanadoInfo> ObtenerEntradasPorEmbarqueID(int embarqueID)
        {
            List<EntradaGanadoInfo> entradasGanado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosPorEmbarqueID(embarqueID);
                DataSet ds = Retrieve("EntradaGanado_ObtenerPorEmbarqueID", parametros);
                if (ValidateDataSet(ds))
                {
                    entradasGanado = MapEntradaGanadoDAL.ObtenerEntradasPorEmbarqueID(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradasGanado;
        }

        /// <summary>
        /// Actualiza las cabezas origen
        /// </summary>
        /// <param name="entradaGanadoID"> </param>
        /// <param name="cabezasOrigen"></param>
        internal void ActualizaCabezasOrigen(int entradaGanadoID, int cabezasOrigen)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ActualizaCabezasOrigen(entradaGanadoID,
                                                                                                   cabezasOrigen);
                Update("EntradaGanado_ActualizarCabezasOrigen", parametros);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene entradas de ganado por folio de origen
        /// </summary>
        /// <returns></returns>
        internal List<EntradaGanadoInfo> ObtenerEntradasPorFolioOrigenXML(List<EntradaGanadoInfo> foliosOrigen)
        {
            List<EntradaGanadoInfo> entradasGanado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxEntradaGanadoDAL.ObtenerParametrosPorFolioOrigenXML(foliosOrigen);
                DataSet ds = Retrieve("EntradaGanado_ObtenerFolioEntradaXML", parametros);
                if (ValidateDataSet(ds))
                {
                    entradasGanado = MapEntradaGanadoDAL.ObtenerEntradasPorFolioOrigenXML(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradasGanado;
        }

        /// <summary>
        /// Obtiene las entradas para su impresion de tarjeta de recepcion
        /// </summary>
        /// <param name="filtro">filtros para obtener las entradas a imprimir</param>
        /// <returns></returns>
        internal List<ImpresionTarjetaRecepcionModel> ObtenerEntradasImpresionTarjetaRecepcion(FiltroImpresionTarjetaRecepcion filtro)
        {
            List<ImpresionTarjetaRecepcionModel> entradasGanado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxEntradaGanadoDAL.ObtenerParametrosEntradasImpresionTarjetaRecepcion(filtro);
                DataSet ds = Retrieve("EntradaGanado_ObtenerImpresionTarjetaRecepcion", parametros);
                if (ValidateDataSet(ds))
                {
                    entradasGanado = MapEntradaGanadoDAL.ObtenerEntradasImpresionTarjetaRecepcion(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradasGanado;
        }

        /// <summary>
        /// Obtiene las entradas para su impresion de tarjeta de recepcion
        /// </summary>
        /// <param name="filtro">filtros para obtener las entradas a imprimir</param>
        /// <returns></returns>
        internal List<ImpresionCalidadGanadoModel> ObtenerEntradasImpresionCalidadGanado(FiltroImpresionCalidadGanado filtro)
        {
            List<ImpresionCalidadGanadoModel> entradasGanado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxEntradaGanadoDAL.ObtenerParametrosEntradasImpresionCalidadGanado(filtro);
                DataSet ds = Retrieve("EntradaGanado_ObtenerImpresionCalidadGanado", parametros);
                if (ValidateDataSet(ds))
                {
                    entradasGanado = MapEntradaGanadoDAL.ObtenerEntradasImpresionCalidadGanado(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradasGanado;
        }

        /// <summary>
        /// Metodo que obtiene una entrada de ganado Folio de entrada y Organizacion
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerPorFolioEntradaCortadaIncompleta(EntradaGanadoInfo filtro)
        {
            EntradaGanadoInfo entradaGanadoInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosPorFolioEntradaCortadaIncompleta(filtro);
                DataSet ds = Retrieve("EntradaGanado_ObtenerPorFolioEntradaCortadaIncompleta", parametros);
                if (ValidateDataSet(ds))
                {
                    entradaGanadoInfo = MapEntradaGanadoDAL.ObtenerPorFolioEntradaCortadaIncompleta(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Obtiene las Entradas de Ganado por Pagina
        /// </summary>
        /// <param name="pagina">Indica la manera en que se realizara el Paginado</param>
        /// <param name="entradaGanadoInfo">Folio Por el Cual se Filtrara</param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaGanadoInfo> ObtenerEntradaGanadoPaginaCortadasIncompletas(PaginacionInfo pagina, EntradaGanadoInfo entradaGanadoInfo)
        {
            ResultadoInfo<EntradaGanadoInfo> resultadoGanadoInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosEntradaGanadoPaginaCortadasIncompletas(pagina, entradaGanadoInfo);
                DataSet ds = Retrieve("EntradaGanado_ObtenerPorPaginaFolioEntradaCortadaIncompleta", parametros);
                if (ValidateDataSet(ds))
                {
                    resultadoGanadoInfo = MapEntradaGanadoDAL.ObtenerEntradaGanadoPaginaCortadasIncompletas(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultadoGanadoInfo;
        }

        /// <summary>
        /// Metodo que obtiene una entrada de ganado Folio de entrada y Organizacion
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal List<FiltroAnimalesReemplazoArete> ObtenerReemplazoAretes(EntradaGanadoInfo filtro)
        {
            List<FiltroAnimalesReemplazoArete> aretes = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosReemplazoArete(filtro);
                DataSet ds = Retrieve("EntradaGanado_ObtenerReemplazoArete", parametros);
                if (ValidateDataSet(ds))
                {
                    aretes = MapEntradaGanadoDAL.ObtenerReemplazoAretes(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return aretes;
        }

        /// <summary>
        ///     Metodo que crear una entrada de ganado
        /// </summary>
        /// <param name="listaAretes"></param>
        /// <param name="entradaGanado"></param>
        internal int GuardarReemplazoAretes(List<FiltroAnimalesReemplazoArete> listaAretes, EntradaGanadoInfo entradaGanado)
        {
            int entradaGanadoID;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosReemplazoAretes(listaAretes, entradaGanado);
                entradaGanadoID = Create("EntradaGanado_ReemplazoArete", parametros);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoID;
        }

        /// <summary>
        ///  Obtiene la entrada en base al Embarque
        /// </summary>
        /// <param name="embarqueID"></param>
        /// <returns></returns>
        internal List<CabezasPartidasModel> ObtenerCabezasEntradasRuteo(int embarqueID)
        {
            List<CabezasPartidasModel> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosCabezasEntradasRuteo(embarqueID);
                var ds = Retrieve("EntradaGanado_ObtenerRuteosPorEmbarque", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoDAL.ObtenerCabezasEntradasRuteo(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtener entrada ganado por loteID y corralID
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerEntradaGanadoLoteCorral(LoteInfo lote)
        {
            EntradaGanadoInfo entradaGanadoInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEntradaGanadoDAL.ObtenerParametrosEntradaGanadoLoteCorral(lote);
                DataSet ds = Retrieve("EntradaGanado_ObtenerEntradaPorLoteCorralOrganiziacion", parametros);
                if (ValidateDataSet(ds))
                {
                    entradaGanadoInfo = MapEntradaGanadoDAL.ObtenerEntradaGanadoLoteCorral(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }
    }
}
 