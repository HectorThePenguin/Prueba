using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Integracion.DAL.Excepciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SIE.Services.Integracion.DAL.ORM
{
    internal class CorralDAL : BaseDAL
    {
        CorralAccessor corralAccessor;

        protected override void inicializar()
        {
            corralAccessor = da.inicializarAccessor<CorralAccessor>();
        }

        protected override void destruir()
        {
            corralAccessor = null;
        }

        internal List<Info.Info.CorralInfo> ObtenerCorralesPorTipoCorral(Info.Info.TipoCorralInfo tc, int organizacionID)
        {
            try
            {
                Logger.Info();

                var query = da.Tabla<Info.Info.CorralInfo>();
                query = tc != null ? query.Where(e => e.TipoCorralId == tc.TipoCorralID) : query;
                query = query.Where(e => e.OrganizacionId == organizacionID);

                return query.ToList();
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

        internal List<Info.Info.CorralInfo> ObtenerCorralesConLoteActivoPorTipoCorralSinEnfermeriaAsignada(Info.Info.TipoCorralInfo tipoCorral, int organizacionId)
        {
            try
            {
                Logger.Info();
                var lotes = da.Tabla<Info.Info.LoteInfo>().Where(e => e.Activo == Info.Enums.EstatusEnum.Activo);
                var query = da.Tabla<Info.Info.CorralInfo>();
                query = tipoCorral != null ? query.Where(e => e.TipoCorralId == tipoCorral.TipoCorralID) : query;
                query = query.Where(e => e.OrganizacionId == organizacionId);
                query = query.Join(lotes, e => e.CorralID, e => e.CorralID, (a, b) => a);

                var tblEnfermeriaCorral = da.Tabla<Info.Info.EnfermeriaCorralInfo>();

                query = (from corral in query
                        join enfermeriaCorral in tblEnfermeriaCorral on corral.CorralID equals enfermeriaCorral.CorralID into defaults
                        from enfermeriaDefault in defaults.DefaultIfEmpty()
                        where enfermeriaDefault == null || enfermeriaDefault.Activo == EstatusEnum.Inactivo
                        select corral).Distinct();

                var corralesActivos = from corral in query
                                      join enfermeriaCorral in tblEnfermeriaCorral on corral.CorralID equals
                                          enfermeriaCorral.CorralID
                                      where enfermeriaCorral.Activo == EstatusEnum.Activo
                                      select corral;

                List<Info.Info.CorralInfo> listaCorrales = query.ToList();
                List<Info.Info.CorralInfo> listaCorralesActivos = corralesActivos.ToList();

                return ObtenerListaFinal(listaCorrales, listaCorralesActivos);
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

        internal List<Info.Info.CorralInfo> ObtenerCorralesPorTipoCorralSinEnfermeriaAsignada(Info.Info.TipoCorralInfo tipoCorral, int organizacionId)
        {
            try
            {
                Logger.Info();
                //var lotes = da.Tabla<Info.Info.LoteInfo>();
                var query = da.Tabla<Info.Info.CorralInfo>();
                query = tipoCorral != null ? query.Where(e => e.TipoCorralId == tipoCorral.TipoCorralID) : query;
                query = query.Where(e => e.OrganizacionId == organizacionId);
                //query = query.Join(lotes, e => e.CorralID, e => e.CorralID, (a, b) => a);

                var tblEnfermeriaCorral = da.Tabla<Info.Info.EnfermeriaCorralInfo>();

                query = (from corral in query
                         join enfermeriaCorral in tblEnfermeriaCorral on corral.CorralID equals enfermeriaCorral.CorralID into defaults
                         from enfermeriaDefault in defaults.DefaultIfEmpty()
                         where enfermeriaDefault == null || enfermeriaDefault.Activo == EstatusEnum.Inactivo
                         select corral).Distinct();

                var corralesActivos = from corral in query
                                      join enfermeriaCorral in tblEnfermeriaCorral on corral.CorralID equals
                                          enfermeriaCorral.CorralID
                                      where enfermeriaCorral.Activo == EstatusEnum.Activo
                                      select corral;

                List<Info.Info.CorralInfo> listaCorrales = query.ToList();
                List<Info.Info.CorralInfo> listaCorralesActivos = corralesActivos.ToList();

                return ObtenerListaFinal(listaCorrales, listaCorralesActivos);
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

        internal List<Info.Info.CorralInfo> ObtenerListaFinal(List<Info.Info.CorralInfo> listaCorrales, List<Info.Info.CorralInfo> corralesActivo )
        {
            var listaFinal = new List<Info.Info.CorralInfo>();
            foreach (var corral in listaCorrales)
            {
                var corralExiste = corralesActivo.FirstOrDefault(cor => cor.CorralID == corral.CorralID);
                if(corralExiste == null)
                {
                    listaFinal.Add(corral);
                }
            }
            return listaFinal;
        }

        internal List<Info.Info.CorralInfo> ObtenerCorralesPorId(int[] corralesId)
        {
            try
            {
                Logger.Info();
                var query = from x in da.Tabla<Info.Info.CorralInfo>()
                            where corralesId.Contains(x.CorralID)
                            select x;
                return query.ToList();
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

        internal List<Info.Info.CorralInfo> ObtenerCorralesPorEnfermeriaId(int enfermeriaId)
        {
            try
            {
                Logger.Info();

                var query = from x in da.Tabla<Info.Info.CorralInfo>()
                            join y in da.Tabla<Info.Info.EnfermeriaCorralInfo>() on x.CorralID equals y.CorralID
                            where y.EnfermeriaID == enfermeriaId && y.Activo == Info.Enums.EstatusEnum.Activo
                            select x;
                return query.ToList();
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
    }
}
