using System;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using System.Data;
using System.Collections.Generic;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class MuestreoTamanoFibraDAL : DALBase
    {
        internal void Guardar(List<MuestreoFibraFormulaInfo> listaMuestreoFormulasInfo)
        {
            try
            {
                Logger.Info();
                var parametros = AuxMuestreoTamanoFibraDAL.ObtenerParametrosGuardar(listaMuestreoFormulasInfo);
                Create("MuestreoFibraFormula_Crear", parametros);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
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

        internal void Guardar(List<MuestreoFibraProductoInfo> listaMuestreoIngredientesInfo)
        {
            try
            {
                Logger.Info();
                var parametros = AuxMuestreoTamanoFibraDAL.ObtenerParametrosGuardar(listaMuestreoIngredientesInfo);
                Create("MuestreoFibraIngrediente_Crear", parametros);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
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
    }
}
