using System;
using System.Collections.Generic;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Modelos;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.BL;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.PL
{
    public class CorralPL
    {
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CorralInfo> ObtenerPorPagina(PaginacionInfo pagina, CorralInfo filtro)
        {
            ResultadoInfo<CorralInfo> result;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                result = corralBL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un Corral por sus Dependencias
        /// </summary>
        /// <param name="pagina">Paginado de Datos</param>
        /// <param name="corralInfos">Info con Filtros</param>
        /// <returns></returns>
        public ResultadoInfo<CorralInfo> ObtenerPorDependencia(PaginacionInfo pagina, CorralInfo corralInfos)
        {
            ResultadoInfo<CorralInfo> resultadoCorral;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                resultadoCorral = corralBL.ObtenerPorDependencia(pagina, corralInfos);
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
            return resultadoCorral;
        }

        /// <summary>
        /// Obtiene un Corral por sus Dependencias
        /// </summary>
        /// <param name="corralInfo">Info con Filtros</param>
        /// <returns></returns>
        public CorralInfo ObtenerPorDependencia(CorralInfo corralInfo)
        {
            CorralInfo resultadoCorral;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                resultadoCorral = corralBL.ObtenerPorDependencia(corralInfo);
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
            return resultadoCorral;
        }

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Corral
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(CorralInfo info)
        {
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                int result = corralBL.Guardar(info);
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
        /// Metodo que Indica si el Corral Seleccionado
        /// Es Usando en Ruteo
        /// </summary>
        /// <param name="embarqueID"></param>
        /// <param name="corralID"></param>
        /// <returns>
        /// true - En caso de que Si ha sido Seleccionado para Ruteo
        /// flase - En caso de que No ha sido Seleccionado para Ruteo
        /// </returns>
        public bool CorralSeleccionadoParaRuteo(int embarqueID, int corralID)
        {
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                bool corralEstaEnRuteo = corralBL.CorralSeleccionadoParaRuteo(embarqueID, corralID);
                return corralEstaEnRuteo;
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
        /// Obtiene el Corral por ID
        /// </summary>
        /// <param name="corralID"></param>
        /// <returns></returns>
        public CorralInfo ObtenerPorId(int corralID)
        {
            CorralInfo resultadoCorral;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                resultadoCorral = corralBL.ObtenerPorId(corralID);
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
            return resultadoCorral;
        }
        
        /// <summary>

        /// Metodo que valida si existe el codigo del corral y no tiene asignado un lote ni servio de alimento
        /// </summary>
        /// <param name="organizacionID"></param>

        /// <param name="codigoCorral"></param>
        public CorralInfo ObtenerValidacionCorral(int organizacionID, string codigoCorral)
        {

            CorralInfo corral;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();

                corral = corralBL.ObtenerValidacionCorral(organizacionID, codigoCorral);
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

            return corral;
        }

        /// <summary>
        /// Obtiene el Corral por ID
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        public CorralInfo ObtenerPorCodigoCorral(CorralInfo corralInfo)
        {
            CorralInfo resultadoCorral;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                resultadoCorral = corralBL.ObtenerPorCodicoCorral(corralInfo);
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
            return resultadoCorral;
        }

        /// <summary>
        /// Obtiene el Corral por ID
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        public CorralInfo ObtenerPorCodigoCorraleta(CorralInfo corralInfo)
        {
            CorralInfo resultadoCorral;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                resultadoCorral = corralBL.ObtenerPorCodicoCorraleta(corralInfo);
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
            return resultadoCorral;
        }

        /// <summary>
        /// Obtiene el corral que ya fue asignado a un embarque
        /// </summary>
        /// <param name="folioEmbarque"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public CorralInfo ObtenerPorEmbarqueRuteo(int folioEmbarque, int organizacionID)
        {
            CorralInfo corralInfo;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                corralInfo = corralBL.ObtenerPorEmbarqueRuteo(folioEmbarque, organizacionID);
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
            return corralInfo;
        }

        /// <summary>
        /// Obtiene una entidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public CorralInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                CorralInfo result = corralBL.ObtenerPorDescripcion(descripcion);
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

        public CorralInfo ObtenerPorCodigoGrupo(CorralInfo corral)
        {
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                CorralInfo result = corralBL.ObtenerPorCodigoGrupo(corral);
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

        public ResultadoInfo<CorralInfo> ObtenerCorralesPorTipo(CorralInfo corral)
        {
            ResultadoInfo<CorralInfo> result;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                result = corralBL.ObtenerCorralesPorTipo(corral);
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

        public ResultadoInfo<CorralInfo> ObtenerCorralesPorTipoCorralDetector(CorralInfo corral)
        {
            ResultadoInfo<CorralInfo> result;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                result = corralBL.ObtenerCorralesPorTipoCorralDetector(corral);
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

        public ResultadoInfo<CorralInfo> ObtenerCorralesPorTipoEnfermeria(CorralInfo corral)
        {
            ResultadoInfo<CorralInfo> result;
            try
            {
                Logger.Info();
                List<int> listaTiposCorral = new List<int>();
                listaTiposCorral.Add((int)TipoCorral.CronicoRecuperacion);
                listaTiposCorral.Add((int)TipoCorral.CronicoVentaMuerte);
                var corralBL = new CorralBL();
                result = corralBL.ObtenerCorralesPorTipo(corral, listaTiposCorral);
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


        public CorralInfo ObtenerCorralPorCodigo(int organizacionId, string corralCodigo)
        {
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                CorralInfo result = corralBL.ObtenerCorralPorCodigo(organizacionId, corralCodigo);
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

        public EntradaGanadoInfo ObtenerPartidaCorral(int organizacionId, int corralID)
        {
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                EntradaGanadoInfo result = corralBL.ObtenerPartidaCorral(organizacionId, corralID);
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
        /// Metodo para validar si el corral pertenece a la enfermeria
        /// </summary>
        public bool ValidarCorralPorEnfermeria(string codigoCorral, int enfermeria, int organizacionId)
        {
            try
            {
                Logger.Info();
                var corralBl = new CorralBL();
                return corralBl.ValidarCorralPorEnfermeria(codigoCorral, enfermeria, organizacionId);
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

        public string ActualizarCorralesCabezas(AnimalMovimientoInfo animalMovimiento, int loteOrigen)
        {
            try
            {
                Logger.Info();
                var corralBl = new CorralBL();
                return corralBl.ActualizarCorralesCabezas(animalMovimiento, loteOrigen);
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
        /// Obtner existencia de corral
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="corralCodigo"></param>
        /// <returns></returns>
        public CorralInfo ObtenerExistenciaCorral(int organizacionId, string corralCodigo)
        {
            CorralInfo resultadoCorral;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                resultadoCorral = corralBL.ObtenerExistenciaCorral(organizacionId, corralCodigo);
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
            return resultadoCorral;
        }

        /// <summary>
        /// Metodo para generar el Reporte Proyector y Comportamiento de Ganado
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public List<ReporteProyectorInfo> ObtenerReporteProyectorComportamiento(int organizacionID)
        {
            try
            {
                Logger.Info();
                var corralBl = new CorralBL();
                return corralBl.ObtenerReporteProyectorComportamiento(organizacionID);
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
        /// Obtiene el numero de cabezas en el corral
        /// </summary>
        /// <param name="corral">Corral al cual se le contara las cabezas</param>
        /// <returns></returns>
        public int ContarCabezas(CorralInfo corral)
        {
            try
            {
                Logger.Info();
                var corralBl = new CorralBL();
                return corralBl.ContarCabezas(corral);
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

        public ResultadoInfo<CorralInfo> ObtenerCorralesPorTipoCorraletaSacrificio(CorralInfo corral)
        {
            ResultadoInfo<CorralInfo> result;
            try
            {
                Logger.Info();
                List<int> listaTiposCorral = new List<int>();
                listaTiposCorral.Add((int)TipoCorral.CorraletaSacrificio);
                var corralBL = new CorralBL();
                result = corralBL.ObtenerCorralesPorTipo(corral, listaTiposCorral);
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

        public List<AnimalInfo> ObtenerAretesCorraleta(CorralInfo corralInfo,LoteInfo loteOrigen)
        {
            try
            {
                Logger.Info();
                var corralBl = new CorralBL();
                return corralBl.ObtenerAretesCorraleta(corralInfo, loteOrigen);
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

        public void TraspasarAnimalSalidaEnfermeria(int corralInfo, int loteOrigen)
        {
            try
            {
                Logger.Info();
                var corralBl = new CorralBL();
                corralBl.TraspasarAnimalSalidaEnfermeria(corralInfo,loteOrigen);
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
        /// Obtiene el Corral por Organizacion y Codigo
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        public CorralInfo ObtenerPorCodicoOrganizacionCorral(CorralInfo corralInfo)
        {
            CorralInfo resultadoCorral;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                resultadoCorral = corralBL.ObtenerPorCodicoOrganizacionCorral(corralInfo);
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
            return resultadoCorral;
        }

        /// <summary>
        /// obtiene la corraleta de sacrificio de la organizacion
        /// </summary>
        /// <param name="corral"></param>
        /// <returns></returns>
        public CorralInfo ObtenerCorraletaSacrificio(CorralInfo corral)
        {
            ResultadoInfo<CorralInfo> result;
            CorralInfo corralResultado = null;
            try
            {
                Logger.Info();
                var listaTiposCorral = new List<int> {(int) TipoCorral.CorraletaSacrificio};
                corral.GrupoCorral = (int) GrupoCorralEnum.Corraleta;
                var corralBL = new CorralBL();
                result = corralBL.ObtenerCorralesPorTipo(corral, listaTiposCorral);

                if (result != null)
                {
                    if (result.Lista.Count > 0)
                    {
                        corralResultado = result.Lista[0];
                    }
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
            return corralResultado;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        public CorralInfo ValidarCorralEnfermeria(CorralInfo corralInfo)
        {
            CorralInfo resultadoCorral;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                resultadoCorral = corralBL.ValidarCorralEnfermeria(corralInfo);
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
            return resultadoCorral;
        }

        public CorralInfo ObtenerPorCodigoGrupoCorral(CorralInfo corral)
        {
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                CorralInfo result = corralBL.ObtenerPorCodigoGrupoCorral(corral);
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

        public int ObtenerExisteInterfaceSalida(int organizacionID, string corralCodigo, string arete)
        {
            try
            {
                Logger.Info();
                var corralBl = new CorralBL();
                return corralBl.ObtenerExisteInterfaceSalida(organizacionID, corralCodigo, arete);
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

        public CorralInfo ObtenerCorralPorLoteID(int loteID, int organizacionID)
        {
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                CorralInfo result = corralBL.ObtenerCorralPorLoteID(loteID, organizacionID);
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
        /// Obtiene el listado de corrales dependiendo del Grupo Corral al que pertenecen
        /// </summary>
        /// <param name="grupo"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public IList<CorralInfo> ObtenerCorralesPorGrupo(GrupoCorralInfo grupo, int organizacionId)
        {
            try
            {
                Logger.Info();
                var corralBl = new CorralBL();
                IList<CorralInfo> result = corralBl.ObtenerCorralesPorGrupo(grupo, organizacionId);
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
        /// Obtiene el listado de corrales dependiendo del Grupo Corral Enfermeria y que tengan programacion en servicio de alimentos.
        /// </summary>
        /// <param name="grupo"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public IList<CorralInfo> ObtenerPorCorralesPorGrupoConProgramacionDeAlimentos(GrupoCorralInfo grupo, int organizacionId)
        {
            try
            {
                Logger.Info();
                var corralBl = new CorralBL();
                IList<CorralInfo> result = corralBl.ObtenerPorCorralesPorGrupoConProgramacionDeAlimentos(grupo, organizacionId);
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
        /// Obtiene un corral por su grupo corral
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        public CorralInfo ObtenerPorGrupoCorral(CorralInfo corralInfo)
        {
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                CorralInfo result = corralBL.ObtenerPorGrupoCorral(corralInfo);
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
        /// Obtiene una lista de corrales paginada
        /// por su grupo corral
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CorralInfo> ObtenerPorPaginaGrupoCorral(PaginacionInfo pagina, CorralInfo filtro)
        {
            ResultadoInfo<CorralInfo> result;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                result = corralBL.ObtenerPorPaginaGrupoCorral(pagina, filtro);
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
        /// Obtiene una lista de corrales paginada con una lista de grupo de corrales especificada y por organizacionid
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CorralInfo> ObtenerPorPaginaGruposCorrales(PaginacionInfo pagina, CorralInfo filtro)
        {
            ResultadoInfo<CorralInfo> result;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                result = corralBL.ObtenerPorPaginaGruposCorrales(pagina, filtro);
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
        /// Obtiene un corral por codigo que este activo y tenga lote activo.
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="corralCodigo"></param>
        /// <returns></returns>
        public CorralInfo ObtenerCorralActivoPorCodigo(int organizacionId, string corralCodigo)
        {
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                CorralInfo result = corralBL.ObtenerCorralActivoPorCodigo(organizacionId, corralCodigo);
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
        /// <param name="organizacionID"> </param>
        /// <returns></returns>
        public CorralInfo ObtenerPorDescripcionOrganizacion(string descripcion, int organizacionID)
        {
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                CorralInfo result = corralBL.ObtenerPorDescripcionOrganizacion(descripcion, organizacionID);
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
        /// Obtiene todos los corrales que pertencen al tipo de corral y organizacion especificados, 
        /// ademas deberan contener un lote activo y no tener enfermerias asignadas.
        /// </summary>
        /// <param name="tipoCorral">Tipo de corral al que pertenece el corral.</param>
        /// <param name="organizacionId">OrganizacionId al que pertenece el corral.</param>
        /// <returns>Una lista de corrales</returns>
        public List<CorralInfo> ObtenerCorralesConLoteActivoPorTipoCorralSinEnfermeriaAsignada(TipoCorralInfo tipoCorral, int organizacionId)
        {
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                List<CorralInfo> result = corralBL.ObtenerCorralesConLoteActivoPorTipoCorralSinEnfermeriaAsignada(tipoCorral, organizacionId);
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
        /// Obtiene todos los corrales que pertencen al tipo de corral y organizacion especificados y no tener enfermerias asignadas.
        /// </summary>
        /// <param name="tipoCorral">Tipo de corral al que pertenece el corral.</param>
        /// <param name="organizacionId">OrganizacionId al que pertenece el corral.</param>
        /// <returns>Una lista de corrales</returns>
        public List<CorralInfo> ObtenerCorralesPorTipoCorralSinEnfermeriaAsignada(TipoCorralInfo tipoCorral, int organizacionId)
        {
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                List<CorralInfo> result = corralBL.ObtenerCorralesPorTipoCorralSinEnfermeriaAsignada(tipoCorral, organizacionId);
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

        public List<CorralInfo> ObtenerCorralesPorId(int[] corralesId)
        {
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                List<CorralInfo> result = corralBL.ObtenerCorralesPorId(corralesId);
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

        public List<CorralInfo> ObtenerCorralesPorEnfermeriaId(int enfermeriaId)
        {
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                List<CorralInfo> result = corralBL.ObtenerCorralesPorEnfermeriaId(enfermeriaId);
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
        /*
        public IList<CorralInfo> ObtenerParaProgramacionReimplanteDestino(CorralInfo corralInfo)
        {
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                IList<CorralInfo> result = corralBL.ObtenerParaProgramacionReimplanteDestino(corralInfo);
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
        */
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CorralInfo> ObtenerPorPaginaCorralDestinoReimplante(PaginacionInfo pagina, CorralInfo filtro)
        {
            ResultadoInfo<CorralInfo> result;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                result = corralBL.ObtenerParaProgramacionReimplanteDestino(pagina, filtro);
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
        /// <param name="filtro"></param>
        /// <returns></returns>
        public CorralInfo ObtenerPorCodigoCorralDestinoReimplante(CorralInfo filtro)
        {
            CorralInfo result;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                result = corralBL.ObtenerPorCodigoParaProgramacionReimplanteDestino( filtro);
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
        /// Valida que el corral tenga existencia
        /// </summary>
        /// <param name="corralID"></param>
        /// <returns></returns>
        public CorralInfo ValidaCorralConLoteConExistenciaActivo(int corralID)
        {
            CorralInfo result;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                result = corralBL.ValidaCorralConLoteConExistenciaActivo(corralID);
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
        /// Obtiene los Corrales por sus codigos
        /// </summary>
        /// <param name="codigosCorral"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public List<CorralInfo> ObtenerCorralesPorCodigosCorral(List<string> codigosCorral, int organizacionID)
        {
            List<CorralInfo> result;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                result = corralBL.ObtenerCorralesPorCodigosCorral(codigosCorral, organizacionID);
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
        /// Obtiene las Secciones de los corrales
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public List<SeccionModel> ObtenerSeccionesCorral(int organizacionID)
        {
            List<SeccionModel> result;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                result = corralBL.ObtenerSeccionesCorral(organizacionID);
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
        /// Obtiene los Corrales por el tipo de corral
        /// </summary>
        /// <param name="tipoCorral"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public List<CorralInfo> ObtenerCorralesPorTipoCorral(TipoCorralInfo tipoCorral, int organizacionID)
        {
            List<CorralInfo> result;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                result = corralBL.ObtenerCorralesPorTipoCorral(tipoCorral, organizacionID);
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
        /// Obtiene un Corral por sus Dependencias
        /// </summary>
        /// <param name="pagina">Paginado de Datos</param>
        /// <param name="corralInfos">Info con Filtros</param>
        /// <returns></returns>
        public ResultadoInfo<CorralInfo> ObtenerFormulaPorDependencia(PaginacionInfo pagina, CorralInfo corralInfos)
        {
            ResultadoInfo<CorralInfo> resultadoCorral;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                resultadoCorral = corralBL.ObtenerFormulaPorDependencia(pagina, corralInfos);
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
            return resultadoCorral;
        }

        /// <summary>
        /// Obtiene los corrales improductivos para la pantalla Corte por Transferencia
        /// </summary>
        /// <param name="tipoCorralID"></param>
        /// <returns></returns>
        public List<CorralInfo> ObtenerCorralesImproductivos(int tipoCorralID)
        {
            List<CorralInfo> resultadoCorral;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                resultadoCorral = corralBL.ObtenerCorralesImproductivos(tipoCorralID);
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
            return resultadoCorral;
        }

        /// <summary>
        /// Obtiene el Corral por ID
        /// </summary>
        /// <param name="corralID"></param>
        /// <returns></returns>
        public CorralInfo ObtenerFormulaCorralPorID(CorralInfo corral)
        {
            CorralInfo resultadoCorral;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                resultadoCorral = corralBL.ObtenerFormulaCorralPorID(corral);
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
            return resultadoCorral;
        }

        /// <summary>
        /// Obtener lista de corralestas configuradas para sacrificio
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="codigoCorraletas"></param>
        /// <returns></returns>
        public ResultadoInfo<CorralInfo> ObtenerInformacionCorraletasDisponiblesSacrificio(int organizacionId, string codigoCorraletas)
        {
            ResultadoInfo<CorralInfo> resultadoCorral;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                resultadoCorral = corralBL.ObtenerInformacionCorraletasDisponiblesSacrificio(organizacionId, codigoCorraletas);
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
            return resultadoCorral;
        }

        /// <summary>
        /// Obtener obtener corral por codigo
        /// </summary>
        /// <param name="corral"></param>
        /// <returns></returns>
        public CorralInfo ObtenerCorralesPorTipos(CorralInfo corral)
        {
            CorralInfo result;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                result = corralBL.ObtenerCorralesPorTipos(corral);
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
        /// Obtener lista de corrales por pagina
        /// </summary>
        /// <param name="paginacion"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CorralInfo> ObtenerCorralesPorPagina(PaginacionInfo paginacion, CorralInfo filtro)
        {
            ResultadoInfo<CorralInfo> result;
            try
            {
                Logger.Info();
                var corralBL = new CorralBL();
                result = corralBL.ObtenerCorralesPorPagina(paginacion, filtro);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        } 
    }
}
 