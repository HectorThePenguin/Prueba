using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using SIE.Services.Facturas;
using SIE.Services.Info.Modelos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class MuertesEnTransitoDAL : DALBase
    {
        internal void Guardar(List<EntradaGanadoMuerteInfo> entradaGanadoMuerteLista, ClienteInfo cliente)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMuertesEnTransitoDAL.ObtenerParametrosGuardar(entradaGanadoMuerteLista, cliente);
                Create("MuertesEnTransito_EntradaGanadoMuerte_Guardar", parameters);
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

        internal ResultadoInfo<MuertesEnTransitoInfo> ObtenerPorPagina(PaginacionInfo pagina, MuertesEnTransitoInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxMuertesEnTransitoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("MuertesEnTransito_EntradaGanado_ObtenerPorPagina", parameters);
                ResultadoInfo<MuertesEnTransitoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapMuertesEnTransitoDAL.ObtenerPorPagina(ds);
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

        internal MuertesEnTransitoInfo ObtenerPorFolioEntrada(MuertesEnTransitoInfo filtro)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMuertesEnTransitoDAL.ObtenerParametrosPorFolioEntrada(filtro);
                DataSet ds = Retrieve("MuertesEnTransito_EntradaGanado_ObtenerPorFolioEntrada", parameters);
                MuertesEnTransitoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapMuertesEnTransitoDAL.ObtenerPorFolioEntrada(ds);
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

        internal List<AnimalInfo> ObtenerAnimalesPorFolioEntrada(MuertesEnTransitoInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxMuertesEnTransitoDAL.ObtenerParametrosPorFolioEntrada(filtro);
                DataSet ds = Retrieve("MuertesEnTransito_EntradaGanado_ObtenerAretesPorFolioEntrada", parameters);
                List<AnimalInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapMuertesEnTransitoDAL.ObtenerAretesPorFolioEntrada(ds);
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

        internal FacturaInfo ObtenerDatosFacturaMuertesEnTransito(long folioMuerte, int organizacionID)
        {
            FacturaInfo facturaInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxMuertesEnTransitoDAL.ObtenerParametrosObtenerDatosFacturaMuertesEnTransito(folioMuerte, organizacionID);
                DataSet ds = Retrieve("MuertesEnTransito_ObtenerDatosFactura", parameters);
                if (ValidateDataSet(ds))
                {
                    facturaInfo = MapMuertesEnTransitoDAL.ObtenerDatosFacturaMuertesEnTransito(ds);
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

            return facturaInfo;
        }



        internal int ObtenerTotalFoliosValidos(int organizacionId)
        {
            int resultado = 0;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxMuertesEnTransitoDAL.ObtenerParametrosObtenerTotalFoliosValidos(organizacionId);
                DataSet ds = Retrieve("MuertesEnTransitoVenta_ValidarExistenciaFolio", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapMuertesEnTransitoDAL.ObtenerTotalFoliosValidos(ds);
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

            return resultado;
        }

        internal ValidacionesFolioVentaMuerte ValidarFolio(int folioEntrada, int organizacionId, List<string> aretes)
        {
            ValidacionesFolioVentaMuerte resultado = new ValidacionesFolioVentaMuerte();
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxMuertesEnTransitoDAL.ObtenerParametrosValidarFolioEntrada(folioEntrada, organizacionId, aretes);
                DataSet ds = Retrieve("MuertesEnTransitoVenta_ValidarFolio", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapMuertesEnTransitoDAL.ObtenerValidacionesFolioEntrada(ds);
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

            return resultado;
        }
        
    }
}
