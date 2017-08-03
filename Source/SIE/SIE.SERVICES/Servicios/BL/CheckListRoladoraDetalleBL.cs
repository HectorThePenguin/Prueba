using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.ORM;

namespace SIE.Services.Servicios.BL
{
    public class CheckListRoladoraDetalleBL : IDisposable
    {
        #region METODOS ORM

        CheckListRoladoraDetalleDAL checkListRoladoraDetalleDAL;

        public CheckListRoladoraDetalleBL()
        {
            checkListRoladoraDetalleDAL = new CheckListRoladoraDetalleDAL();
        }

        public void Dispose()
        {
            checkListRoladoraDetalleDAL.Disposed += (s, e) =>
            {
                checkListRoladoraDetalleDAL = null;
            };
            checkListRoladoraDetalleDAL.Dispose();
        }

        /// <summary>
        /// Obtiene una lista paginada de CheckListRoladoraDetalle
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CheckListRoladoraDetalleInfo> ObtenerPorPagina(PaginacionInfo pagina, CheckListRoladoraDetalleInfo filtro)
        {
            try
            {
                Logger.Info();
                return checkListRoladoraDetalleDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista de CheckListRoladoraDetalle
        /// </summary>
        /// <returns></returns>
        public IList<CheckListRoladoraDetalleInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return checkListRoladoraDetalleDAL.ObtenerTodos().ToList();
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
        /// Obtiene una lista de CheckListRoladoraDetalle filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IList<CheckListRoladoraDetalleInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return checkListRoladoraDetalleDAL.ObtenerTodos().Where(e => e.Activo == estatus).ToList();
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
        /// Obtiene una entidad de CheckListRoladoraDetalle por su Id
        /// </summary>
        /// <param name="checkListRoladoraDetalleId">Obtiene una entidad CheckListRoladoraDetalle por su Id</param>
        /// <returns></returns>
        public CheckListRoladoraDetalleInfo ObtenerPorID(int checkListRoladoraDetalleId)
        {
            try
            {
                Logger.Info();
                return checkListRoladoraDetalleDAL.ObtenerTodos().Where(e => e.CheckListRoladoraDetalleID == checkListRoladoraDetalleId).FirstOrDefault();
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

        ///// <summary>
        ///// Obtiene una entidad de CheckListRoladoraDetalle por su descripcion
        ///// </summary>
        ///// <param name="checkListRoladoraDetalleId">Obtiene una entidad CheckListRoladoraDetalle por su descripcion</param>
        ///// <returns></returns>
        //public CheckListRoladoraDetalleInfo ObtenerPorDescripcion(string descripcion)
        //{
        //    try
        //    {
        //        Logger.Info();
        //        return checkListRoladoraDetalleDAL.ObtenerTodos().Where(e=> e.CheckListRoladoraDetalleID.ToLower() == descripcion.ToLower()).FirstOrDefault();
        //    }
        //    catch(ExcepcionGenerica)
        //    {
        //        throw;
        //    }
        //    catch(Exception ex)
        //    {
        //        Logger.Error(ex);
        //        throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
        //    }
        //}

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad CheckListRoladoraDetalle
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(CheckListRoladoraDetalleInfo info)
        {
            try
            {
                Logger.Info();
                return checkListRoladoraDetalleDAL.Guardar(info);
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

        #endregion METODOS ORM

        #region METODOS DAL

        public dynamic ObtenerCheckListCompleto(int organizacionID, int turno, int roladoraId)
        {
            try
            {
                Logger.Info();
                var checkListRoladoraDetalleDAL = new Integracion.DAL.Implementacion.CheckListRoladoraDetalleDAL();
                List<CheckListRoladoraDetalleInfo> checkListRoladoraDetalle =
                    checkListRoladoraDetalleDAL.ObtenerCheckListCompleto(organizacionID, turno, roladoraId);
                dynamic checkListCompletado = ValidarCheckListCompletado(checkListRoladoraDetalle);
                return checkListCompletado;
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

        private dynamic ValidarCheckListCompletado(List<CheckListRoladoraDetalleInfo> checkListRoladoraDetalle)
        {
            dynamic checkListCompleto;
            if (checkListRoladoraDetalle != null && checkListRoladoraDetalle.Any())
            {

                CheckListRoladoraInfo checkListRoladora =
                    checkListRoladoraDetalle.Select(roladora => new CheckListRoladoraInfo
                                                                    {
                                                                        CheckListRoladoraHorometro = new CheckListRoladoraHorometroInfo
                                                                            {
                                                                                HorometroInicial = roladora.CheckListRoladora.CheckListRoladoraHorometro.HorometroInicial,
                                                                                HorometroFinal = roladora.CheckListRoladora.CheckListRoladoraHorometro.HorometroFinal
                                                                            }
                                                                        //HorometroInicial = roladora.CheckListRoladora.HorometroInicial,
                                                                        //HorometroFinal = roladora.CheckListRoladora.HorometroFinal
                                                                    }).FirstOrDefault();
                CheckListRoladoraGeneralInfo checkListRoladoraGeneral =
                    checkListRoladoraDetalle.Select(general => new CheckListRoladoraGeneralInfo
                                                                   {
                                                                       UsuarioIDSupervisor =
                                                                           general.CheckListRoladora.
                                                                           CheckListRoladoraGeneral.UsuarioIDSupervisor,
                                                                       Observaciones =
                                                                           general.CheckListRoladora.
                                                                           CheckListRoladoraGeneral.Observaciones,
                                                                       SurfactanteInicio =
                                                                           general.CheckListRoladora.
                                                                           CheckListRoladoraGeneral.SurfactanteInicio,
                                                                       SurfactanteFin =
                                                                           general.CheckListRoladora.
                                                                           CheckListRoladoraGeneral.SurfactanteFin,
                                                                       ContadorAguaInicio =
                                                                           general.CheckListRoladora.
                                                                           CheckListRoladoraGeneral.ContadorAguaInicio,
                                                                       ContadorAguaFin =
                                                                           general.CheckListRoladora.
                                                                           CheckListRoladoraGeneral.ContadorAguaFin,
                                                                       GranoEnteroFinal =
                                                                           general.CheckListRoladora.
                                                                           CheckListRoladoraGeneral.GranoEnteroFinal
                                                                   }).FirstOrDefault();
                List<CheckListRoladoraDetalleInfo> checkListDetalle =
                    checkListRoladoraDetalle.Where(clave => clave.CheckListRoladoraDetalleID > 0).OrderBy(
                        clave => clave.CheckListRoladoraDetalleID).Select(det => det).ToList();
                bool completado = !string.IsNullOrEmpty(checkListRoladora.CheckListRoladoraHorometro.HorometroFinal) &&
                                  checkListRoladoraGeneral.SurfactanteInicio.HasValue
                                  && checkListRoladoraGeneral.SurfactanteFin.HasValue &&
                                  checkListRoladoraGeneral.ContadorAguaInicio.HasValue
                                  && checkListRoladoraGeneral.ContadorAguaFin.HasValue &&
                                  checkListRoladoraGeneral.GranoEnteroFinal.HasValue
                                  &&
                                  (checkListDetalle != null && checkListDetalle.Any() &&
                                   checkListDetalle.Count(accion => accion.CheckListRoladoraAccionID > 0) > 0);

                var checkListRoladoraHorometroDAL = new Integracion.DAL.Implementacion.CheckListRoladoraHorometroDAL();

                var checkList = checkListRoladoraDetalle.FirstOrDefault();


                List<CheckListRoladoraHorometroInfo> listaHorometros =
                    checkListRoladoraHorometroDAL.ObtenerPorCheckListRoladoraGeneralID(checkList.CheckListRoladora.CheckListRoladoraGeneralID);


                checkListCompleto =
                    new
                        {
                            Completo = completado ? 1 : 0,
                            CheckListRoladora = checkListRoladora,
                            CheckListRoladoraGeneral = checkListRoladoraGeneral,
                            CheckListRoladoraDetalle = checkListDetalle,
                            CheckListRoladoraHorometro = listaHorometros
                        };
            }
            else
            {
                checkListCompleto = new { Completo = 0 };
            }
            return checkListCompleto;
        }

        #endregion METODOS DAL
    }
}
