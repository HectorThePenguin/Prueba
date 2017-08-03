using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.ORM;
using System.Transactions;

namespace SIE.Services.Servicios.BL
{
    public class CheckListRoladoraBL : IDisposable
    {
        #region IMPLEMENTACION ORM
        CheckListRoladoraDAL checkListRoladoraDAL;

        public CheckListRoladoraBL()
        {
            checkListRoladoraDAL = new CheckListRoladoraDAL();
        }

        public void Dispose()
        {
            checkListRoladoraDAL.Disposed += (s, e) =>
            {
                checkListRoladoraDAL = null;
            };
            checkListRoladoraDAL.Dispose();
        }

        /// <summary>
        /// Obtiene una lista paginada de CheckListRoladora
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CheckListRoladoraInfo> ObtenerPorPagina(PaginacionInfo pagina, CheckListRoladoraInfo filtro)
        {
            try
            {
                Logger.Info();
                return checkListRoladoraDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista de CheckListRoladora
        /// </summary>
        /// <returns></returns>
        public IList<CheckListRoladoraInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return checkListRoladoraDAL.ObtenerTodos().ToList();
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
        /// Obtiene una lista de CheckListRoladora filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IList<CheckListRoladoraInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return checkListRoladoraDAL.ObtenerTodos().Where(e => e.Activo == estatus).ToList();
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
        /// Obtiene una entidad de CheckListRoladora por su Id
        /// </summary>
        /// <param name="checkListRoladoraId">Obtiene una entidad CheckListRoladora por su Id</param>
        /// <returns></returns>
        public CheckListRoladoraInfo ObtenerPorID(int checkListRoladoraId)
        {
            try
            {
                Logger.Info();
                return checkListRoladoraDAL.ObtenerTodos().FirstOrDefault(e => e.CheckListRoladoraID == checkListRoladoraId);
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
        /// Metodo para Guardar/Modificar una entidad CheckListRoladora
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(CheckListRoladoraInfo info)
        {
            try
            {
                Logger.Info();
                return checkListRoladoraDAL.Guardar(info);
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

        #endregion IMPLEMENTACION ORM

        #region IMPLEMENTACION DAL

        /// <summary>
        /// Obtiene una lista de CheckListRoladora
        /// </summary>
        /// <returns></returns>
        public IList<CheckListRoladoraInfo> ObtenerNotificaciones(int organizacionID)
        {
            try
            {
                Logger.Info();
                var checkListRoladoraDALAnterior = new Integracion.DAL.Implementacion.CheckListRoladoraDAL();
                return checkListRoladoraDALAnterior.ObtenerNotificaciones(organizacionID);
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

        public CheckListRoladoraInfo ObtenerPorTurno(int organizacionID, int turno)
        {
            try
            {
                Logger.Info();
                var checkListRoladoraDALAnterior = new Integracion.DAL.Implementacion.CheckListRoladoraDAL();
                return checkListRoladoraDALAnterior.ObtenerPorTurno(organizacionID, turno);
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

        public dynamic ObtenerCheckList(int organizacionID, int turno, int roladoraId)
        {
            try
            {
                Logger.Info();
                var checkListRoladoraDALAnterior = new Integracion.DAL.Implementacion.CheckListRoladoraDAL();
                return checkListRoladoraDALAnterior.ObtenerCheckList(organizacionID, turno, roladoraId);
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

        public void GuardarCheckList(CheckListRoladoraInfo checkListRoladora
                          , CheckListRoladoraGeneralInfo checkListRoladoraGeneral
                          , List<CheckListRoladoraDetalleInfo> checkListRoladoraDetalle
                          , int organizacionID)
        {
            try
            {
                Logger.Info();
                using (var scope = new TransactionScope())
                {
                    GuardarCheckListRolado(checkListRoladora, checkListRoladoraGeneral, checkListRoladoraDetalle,
                                           organizacionID);
                    scope.Complete();
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

        public void GuardarParametrosCheckList(List<CheckListRoladoraInfo> listaCheckListRoladora
                                             , CheckListRoladoraGeneralInfo checkListRoladoraGeneral
                                             , List<CheckListRoladoraHorometroInfo> listaCheckListRoladoraHorometro
                                             , int organizacionID)
        {
            GuardarCheckListRoladoParametro(listaCheckListRoladora, checkListRoladoraGeneral, new List<CheckListRoladoraDetalleInfo>(),
                                            listaCheckListRoladoraHorometro, organizacionID);
        }

        private void GuardarCheckListRoladoParametro(List<CheckListRoladoraInfo> listaCheckListRoladora
                                          , CheckListRoladoraGeneralInfo checkListRoladoraGeneral
                                          , List<CheckListRoladoraDetalleInfo> checkListRoladoraDetalle
                                          , List<CheckListRoladoraHorometroInfo> listaCheckListRoladoraHorometro
                                          , int organizacionID)
        {
            using (var transaction = new TransactionScope())
            {

                CheckListRoladoraInfo checkList = listaCheckListRoladora.FirstOrDefault();
                if (checkList == null)
                {
                    return;
                }
                CheckListRoladoraInfo ultimoCheckListRoladora = ValidarCheckList(organizacionID);
                if (ultimoCheckListRoladora == null)
                {
                    ultimoCheckListRoladora = new CheckListRoladoraInfo
                    {
                        CheckListRoladoraGeneral = new CheckListRoladoraGeneralInfo()
                    };
                }
                var checkListRoladoraGeneralDAL = new Integracion.DAL.Implementacion.CheckListRoladoraGeneralDAL();
                int checkListRoladoraGeneralID;
                if (ultimoCheckListRoladora.CheckListRoladoraGeneral.Turno == checkListRoladoraGeneral.Turno)
                {
                    checkList.CheckListRoladoraID = ultimoCheckListRoladora.CheckListRoladoraID;
                    checkList.CheckListRoladoraGeneral = ultimoCheckListRoladora.CheckListRoladoraGeneral;
                    checkListRoladoraGeneral.CheckListRoladoraGeneralID =
                        ultimoCheckListRoladora.CheckListRoladoraGeneral.CheckListRoladoraGeneralID;
                    checkListRoladoraGeneral.UsuarioIDSupervisor =
                        ultimoCheckListRoladora.CheckListRoladoraGeneral.UsuarioIDSupervisor;
                    checkListRoladoraGeneral.Observaciones = ultimoCheckListRoladora.CheckListRoladoraGeneral.Observaciones;

                    checkListRoladoraGeneralDAL.Actualizar(checkListRoladoraGeneral);
                    checkListRoladoraGeneralID = checkListRoladoraGeneral.CheckListRoladoraGeneralID;
                }
                else
                {
                    checkListRoladoraGeneralID = checkListRoladoraGeneralDAL.Crear(checkListRoladoraGeneral);
                }

                listaCheckListRoladora.ForEach(roladora => roladora.CheckListRoladoraGeneralID = checkListRoladoraGeneralID);

                var checkListRoladoraHorometroDAL = new Integracion.DAL.Implementacion.CheckListRoladoraHorometroDAL();

                List<CheckListRoladoraHorometroInfo> listaHorometros =
                    checkListRoladoraHorometroDAL.ObtenerPorCheckListRoladoraGeneralID(checkListRoladoraGeneralID);

                if (listaCheckListRoladoraHorometro.Any())
                {
                    foreach (var horometro in listaCheckListRoladoraHorometro)
                    {
                        horometro.CheckListRoladoraGeneralID = checkListRoladoraGeneralID;
                        if(listaHorometros == null || !listaHorometros.Any())
                        {
                            continue;
                        }
                        CheckListRoladoraHorometroInfo horometroExiste =
                            listaHorometros.FirstOrDefault(
                                hor => hor.Roladora.RoladoraID == horometro.Roladora.RoladoraID && hor.CheckListRoladoraGeneral.CheckListRoladoraGeneralID ==  horometro.CheckListRoladoraGeneralID);
                        if(horometroExiste != null)
                        {
                            horometro.CheckListRoladoraHorometroID = horometroExiste.CheckListRoladoraHorometroID;
                        }
                    }
                    
                    checkListRoladoraHorometroDAL.Crear(listaCheckListRoladoraHorometro);
                }
                if (checkListRoladoraDetalle != null && checkListRoladoraDetalle.Any())
                {
                    var checkListRoladoraDALAnterior = new Integracion.DAL.Implementacion.CheckListRoladoraDAL();
                    foreach (var checkListRoladora in listaCheckListRoladora)
                    {
                        ultimoCheckListRoladora = ValidarCheckList(organizacionID);
                        if (ultimoCheckListRoladora == null)
                        {
                            ultimoCheckListRoladora = new CheckListRoladoraInfo
                            {
                                CheckListRoladoraGeneral = new CheckListRoladoraGeneralInfo()
                            };
                        }
                        if (ultimoCheckListRoladora.CheckListRoladoraGeneral.Turno == checkListRoladoraGeneral.Turno)
                        {
                            checkListRoladora.CheckListRoladoraID = ultimoCheckListRoladora.CheckListRoladoraID;
                            checkListRoladora.CheckListRoladoraGeneral = ultimoCheckListRoladora.CheckListRoladoraGeneral;
                            checkListRoladoraGeneral.CheckListRoladoraGeneralID =
                                ultimoCheckListRoladora.CheckListRoladoraGeneral.CheckListRoladoraGeneralID;

                            checkListRoladoraDALAnterior.Actualizar(checkListRoladora);
                        }
                        else
                        {
                            checkListRoladora.CheckListRoladoraGeneralID = checkListRoladoraGeneralID;
                            checkListRoladora.CheckListRoladoraGeneral.CheckListRoladoraGeneralID = checkListRoladoraGeneralID;
                            checkListRoladoraDALAnterior.Crear(checkListRoladora);
                        }
                    }
                }
                transaction.Complete();
            }
        }

        public ParametrosCheckListRoladoModel ObtenerGranoEnteroDieselCaldera(int organizacionID, DateTime fechaInicio)
        {
            try
            {
                Logger.Info();
                var checkListRoladoraDALAnterior = new Integracion.DAL.Implementacion.CheckListRoladoraDAL();
                return checkListRoladoraDALAnterior.ObtenerGranoEnteroDieselCaldera(organizacionID, fechaInicio);
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

        private CheckListRoladoraInfo ValidarCheckList(int organizacionID)
        {
            try
            {
                Logger.Info();
                var checkListRoladoraDALAnterior = new Integracion.DAL.Implementacion.CheckListRoladoraDAL();
                return checkListRoladoraDALAnterior.ValidarCheckList(organizacionID);
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

        private void GuardarCheckListRolado(CheckListRoladoraInfo checkListRoladora
                                          , CheckListRoladoraGeneralInfo checkListRoladoraGeneral
                                          , List<CheckListRoladoraDetalleInfo> checkListRoladoraDetalle
                                          , int organizacionID)
        {
            CheckListRoladoraInfo ultimoCheckListRoladora = ValidarCheckList(organizacionID);
            if (ultimoCheckListRoladora == null)
            {
                ultimoCheckListRoladora = new CheckListRoladoraInfo
                                              {
                                                  CheckListRoladoraGeneral = new CheckListRoladoraGeneralInfo()
                                              };
            }
            if (ultimoCheckListRoladora.CheckListRoladoraGeneral.Turno == checkListRoladoraGeneral.Turno)
            {
                checkListRoladora.CheckListRoladoraID = ultimoCheckListRoladora.CheckListRoladoraID;
                checkListRoladora.CheckListRoladoraGeneral = ultimoCheckListRoladora.CheckListRoladoraGeneral;
                checkListRoladoraGeneral.CheckListRoladoraGeneralID =
                    ultimoCheckListRoladora.CheckListRoladoraGeneral.CheckListRoladoraGeneralID;

                GuardarCheckListTurnoExistente(checkListRoladora, checkListRoladoraGeneral, checkListRoladoraDetalle, new List<CheckListRoladoraHorometroInfo>());
            }
            else
            {
                GuardarCheckListTurnoNuevo(checkListRoladora, checkListRoladoraGeneral, checkListRoladoraDetalle, new List<CheckListRoladoraHorometroInfo>());
            }
        }

        private void GuardarCheckListTurnoNuevo(CheckListRoladoraInfo checkListRoladora
                                              , CheckListRoladoraGeneralInfo checkListRoladoraGeneral
                                              , List<CheckListRoladoraDetalleInfo> checkListRoladoraDetalle
                                              , List<CheckListRoladoraHorometroInfo> listaCheckListRoladoraHorometro)
        {
            using (var transaction = new TransactionScope())
            {
                var checkListRoladoraGeneralDAL = new Integracion.DAL.Implementacion.CheckListRoladoraGeneralDAL();
                int checkListRoladoraGeneralID = checkListRoladoraGeneralDAL.Crear(checkListRoladoraGeneral);

                checkListRoladora.CheckListRoladoraGeneralID = checkListRoladoraGeneralID;
                checkListRoladora.CheckListRoladoraGeneral.CheckListRoladoraGeneralID = checkListRoladoraGeneralID;
                var checkListRoladoraDALAnterior = new Integracion.DAL.Implementacion.CheckListRoladoraDAL();
                int checkListRoladoraID = checkListRoladoraDALAnterior.Crear(checkListRoladora);

                if (checkListRoladoraDetalle != null && checkListRoladoraDetalle.Any())
                {
                    checkListRoladoraDetalle.ForEach(ids => ids.CheckListRoladoraID = checkListRoladoraID);
                    var checkListoRoladoraDetalleDAL = new Integracion.DAL.Implementacion.CheckListRoladoraDetalleDAL();
                    checkListoRoladoraDetalleDAL.Crear(checkListRoladoraDetalle);
                }
                transaction.Complete();
            }
        }

        private void GuardarCheckListTurnoExistente(CheckListRoladoraInfo checkListRoladora
                                        , CheckListRoladoraGeneralInfo checkListRoladoraGeneral
                                        , List<CheckListRoladoraDetalleInfo> checkListRoladoraDetalle
                                        , List<CheckListRoladoraHorometroInfo> listaCheckListRoladoraHorometro)
        {
            using (var transaction = new TransactionScope())
            {
                var checkListRoladoraGeneralDAL = new Integracion.DAL.Implementacion.CheckListRoladoraGeneralDAL();
                checkListRoladoraGeneralDAL.Actualizar(checkListRoladoraGeneral);

                var checkListRoladoraDALAnterior = new Integracion.DAL.Implementacion.CheckListRoladoraDAL();
                int checkListoRoladoraID = checkListRoladoraDALAnterior.Crear(checkListRoladora);

                if (checkListRoladoraDetalle != null && checkListRoladoraDetalle.Any())
                {
                    checkListRoladoraDetalle.ForEach(ids => ids.CheckListRoladoraID = checkListoRoladoraID);
                    var checkListoRoladoraDetalleDAL = new Integracion.DAL.Implementacion.CheckListRoladoraDetalleDAL();
                    checkListoRoladoraDetalleDAL.Crear(checkListRoladoraDetalle);
                }
                transaction.Complete();
            }
        }
        public List<CheckListRoladoraHorometroInfo> ObtenerHorometros(int turno, int organizacionID)
        {
            CheckListRoladoraInfo ultimoCheckListRoladora = ValidarCheckList(organizacionID);
            if (ultimoCheckListRoladora == null)
            {
                ultimoCheckListRoladora = new CheckListRoladoraInfo
                                              {
                                                  CheckListRoladoraGeneral = new CheckListRoladoraGeneralInfo()
                                              };
            }
            var checkListRoladoraHorometroDAL = new Integracion.DAL.Implementacion.CheckListRoladoraHorometroDAL();
            List<CheckListRoladoraHorometroInfo> listaHorometros = null;
            if (ultimoCheckListRoladora.CheckListRoladoraGeneral.Turno == turno)
            {
                listaHorometros =
                    checkListRoladoraHorometroDAL.ObtenerPorCheckListRoladoraGeneralID(
                        ultimoCheckListRoladora.CheckListRoladoraGeneral.CheckListRoladoraGeneralID);
            }
            else
            {
                ultimoCheckListRoladora = new CheckListRoladoraInfo
                                              {
                                                  CheckListRoladoraGeneral = new CheckListRoladoraGeneralInfo()
                                              };
            }
            if (listaHorometros == null)
            {
                listaHorometros = new List<CheckListRoladoraHorometroInfo>();
                var horometroDefault = new CheckListRoladoraHorometroInfo
                                           {
                                               CheckListRoladoraGeneral =
                                                   ultimoCheckListRoladora.CheckListRoladoraGeneral
                                           };
                listaHorometros.Add(horometroDefault);
            }
            return listaHorometros;
        }

        /// <summary>
        /// Obtiene los datos para la impresion del
        /// Check List de Rolado
        /// </summary>
        /// <param name="fechaInicial"></param>
        /// <param name="turno"> </param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public List<ImpresionCheckListRoladoModel> ObtenerDatosImpresionCheckListRoladora(DateTime fechaInicial, int turno, int organizacionID)
        {
            List<ImpresionCheckListRoladoModel> datosCheckListRoladora = null;
            try
            {
                Logger.Info();
                var checkListRoladoraDAL = new Integracion.DAL.Implementacion.CheckListRoladoraDAL();
                ImpresionCheckListRoladoModel impresionCheckListRoladoModel = checkListRoladoraDAL.ObtenerDatosImpresionCheckListRoladora(
                    fechaInicial, turno, organizacionID);
                if (impresionCheckListRoladoModel != null)
                {
                    var almacenMovimientoDetalleBL = new AlmacenMovimientoDetalleBL();
                    int año = impresionCheckListRoladoModel.CheckListRoladoraGeneral.FechaInicio.Year;
                    int mes = impresionCheckListRoladoModel.CheckListRoladoraGeneral.FechaInicio.Month;
                    int dia = impresionCheckListRoladoModel.CheckListRoladoraGeneral.FechaInicio.Day;
                    CheckListRoladoraHorometroInfo horaInicial =
                        impresionCheckListRoladoModel.Horometros.OrderBy(id => id.HorometroInicial)
                            .First();
                    int hora = Convert.ToInt32(horaInicial.HorometroInicial.Split(':')[0]);
                    int minuto = Convert.ToInt32(horaInicial.HorometroInicial.Split(':')[1]);
                    var fechaMovimientoInicial = new DateTime(año, mes-1, dia-6, hora, minuto, 0);
                    CheckListRoladoraHorometroInfo horaFinal =
                        impresionCheckListRoladoModel.Horometros.OrderBy(id => id.HorometroInicial)
                            .Last();
                    hora = Convert.ToInt32(horaFinal.HorometroInicial.Split(':')[0]);
                    minuto = Convert.ToInt32(horaFinal.HorometroInicial.Split(':')[1]);
                    var fechaMovimientoFinal = new DateTime(año, mes, dia, hora, minuto, 0);
                    List<AlmacenMovimientoDetalle> movimientosAlmacen =
                        almacenMovimientoDetalleBL.ObtenerAlmacenMovimientoDetalleEntregadosPlanta(
                            fechaMovimientoInicial, fechaMovimientoFinal, organizacionID);
                    var parametrosCheckListRoladoModel = new ParametrosCheckListRoladoModel();
                    if (movimientosAlmacen != null)
                    {
                        decimal totalGrano = movimientosAlmacen.Sum(cant => cant.Cantidad);
                        parametrosCheckListRoladoModel.TotalGranoEntreroPP = totalGrano;
                    }
                    parametrosCheckListRoladoModel.TotalGranoEnteroBodega =
                        impresionCheckListRoladoModel.CheckListRoladoraGeneral.GranoEnteroFinal.Value;
                    parametrosCheckListRoladoModel.SurfactanteInicio =
                        impresionCheckListRoladoModel.CheckListRoladoraGeneral.SurfactanteInicio.Value;
                    parametrosCheckListRoladoModel.SurfactanteFinal =
                        impresionCheckListRoladoModel.CheckListRoladoraGeneral.SurfactanteFin.Value;
                    parametrosCheckListRoladoModel.HumedadGranoEnteroBodega = 0;
                    parametrosCheckListRoladoModel.HumedadGranoRoladoBodega = 0;
                    parametrosCheckListRoladoModel.SuperavitAdicionAguaSurfactante =
                        parametrosCheckListRoladoModel.HumedadGranoRoladoBodega -
                        parametrosCheckListRoladoModel.HumedadGranoEnteroBodega;
                    parametrosCheckListRoladoModel.ContadorAguaInicial =
                        impresionCheckListRoladoModel.CheckListRoladoraGeneral.ContadorAguaInicio.Value;
                    parametrosCheckListRoladoModel.ContadorAguaFinal =
                        impresionCheckListRoladoModel.CheckListRoladoraGeneral.ContadorAguaFin.Value;
                    parametrosCheckListRoladoModel.ConsumoAguaLitro = parametrosCheckListRoladoModel.ContadorAguaFinal -
                                                                      parametrosCheckListRoladoModel.ContadorAguaInicial;
                    parametrosCheckListRoladoModel.TotalGranoProcesado =
                        parametrosCheckListRoladoModel.TotalGranoEnteroBodega -
                        parametrosCheckListRoladoModel.TotalGranoEntreroPP;
                    parametrosCheckListRoladoModel.SuperavitGranoRolado =
                        parametrosCheckListRoladoModel.SuperavitAdicionAguaSurfactante*
                        parametrosCheckListRoladoModel.TotalGranoProcesado;
                    parametrosCheckListRoladoModel.TotalGranoRolado =
                        parametrosCheckListRoladoModel.TotalGranoProcesado +
                        parametrosCheckListRoladoModel.SuperavitGranoRolado;
                    parametrosCheckListRoladoModel.DieseToneladaGranoRolado =
                        parametrosCheckListRoladoModel.ConsumoDieselCalderas/
                        parametrosCheckListRoladoModel.TotalGranoRolado;

                    impresionCheckListRoladoModel.CheckListRoladoraGeneral.ParametrosCheckListRolado =
                        parametrosCheckListRoladoModel;
                    CalcularTiempoOperacionRoladora(impresionCheckListRoladoModel);
                    datosCheckListRoladora = ObtenerDatosAgrupados(impresionCheckListRoladoModel);
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
            return datosCheckListRoladora;
        }

        private List<ImpresionCheckListRoladoModel> ObtenerDatosAgrupados(ImpresionCheckListRoladoModel impresionCheckListRoladoModel)
        {
            IList<int> horasCheckList =
                impresionCheckListRoladoModel.Detalles.Select(hora => hora.CheckListRoladora.FechaCheckList.Hour).
                    Distinct().OrderBy(hora => hora).ToList();
            var resultado = new List<ImpresionCheckListRoladoModel>();
            for (var indexHoras = 0; indexHoras < horasCheckList.Count; indexHoras++)
            {
                var detalles =
                    impresionCheckListRoladoModel.Detalles.Where(
                        hora => hora.CheckListRoladora.FechaCheckList.Hour == horasCheckList[indexHoras]).ToList();
                var res = new ImpresionCheckListRoladoModel
                                {
                                    Hora = horasCheckList[indexHoras],
                                    CheckListRoladoraGeneral = impresionCheckListRoladoModel.CheckListRoladoraGeneral,
                                    Horometros = impresionCheckListRoladoModel.Horometros,
                                    Detalles = detalles
                                };
                resultado.Add(res);
            }
            return resultado;
        }

        private void CalcularTiempoOperacionRoladora(ImpresionCheckListRoladoModel impresionCheckListRoladoModel)
        {
            List<CheckListRoladoraHorometroInfo> horometros = impresionCheckListRoladoModel.Horometros;
            if (horometros != null && horometros.Any())
            {
                TimeSpan tiempoInicial;
                TimeSpan tiempoFinal;
                horometros.ForEach(datos =>
                                       {
                                           tiempoInicial = new TimeSpan();
                                           tiempoFinal = new TimeSpan();
                                           if (!string.IsNullOrWhiteSpace(datos.HorometroInicial))
                                           {
                                               int horaInicial = Convert.ToInt32(datos.HorometroInicial.Split(':')[0]);
                                               int minutoInicial = Convert.ToInt32(datos.HorometroInicial.Split(':')[1]);

                                               tiempoInicial = new TimeSpan(horaInicial, minutoInicial, 0);
                                           }
                                           if (!string.IsNullOrWhiteSpace(datos.HorometroFinal))
                                           {
                                               int horaFinal = Convert.ToInt32(datos.HorometroFinal.Split(':')[0]);
                                               int minutoFinal = Convert.ToInt32(datos.HorometroFinal.Split(':')[1]);

                                               tiempoFinal = new TimeSpan(horaFinal, minutoFinal, 0);
                                           }
                                           datos.HorasOperacion = string.Format("{0}:{1}",
                                                                                (tiempoFinal - tiempoInicial).Hours,
                                                                                (tiempoFinal - tiempoInicial).Minutes);
                                           if (datos.HorasOperacion.Contains("-"))
                                           {
                                               datos.HorasOperacion = string.Empty;
                                           }
                                       });
            }
        }

        #endregion IMPLEMENTACION DAL
    }
}
