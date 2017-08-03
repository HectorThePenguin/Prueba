
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class EntradaProductoParcialDAL:DALBase
    {
        internal bool Crear(ContenedorEntradaMateriaPrimaInfo contenedorEntradaMateriaPrima)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEntradaProductoParcialDAL.ObtenerParametrosCrear(contenedorEntradaMateriaPrima);
                Create("EntradaProductoParcial_Crear", parameters);
                return true;
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
