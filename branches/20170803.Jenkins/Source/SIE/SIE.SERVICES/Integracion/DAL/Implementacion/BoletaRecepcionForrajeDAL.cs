using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using System.Data;
using System.Data.SqlClient;
using SIE.Services.Info.Enums;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class BoletaRecepcionForrajeDAL : DALBase 
    {
        /// <summary>
        /// Metodos que obtiene el rango de humedad permito para el producto determinado
        /// </summary>
        internal RegistroVigilanciaInfo ObtenerRangos(RegistroVigilanciaInfo registroVigilanciaInfo, IndicadoresEnum indicador)
        {
            try
            {
                Logger.Info();
                var parameters = AuxBoletaRecepcionForrajeDAL.ObtenerParametrosRangosProducto(registroVigilanciaInfo, indicador);
                var ds = Retrieve("BoletaRecepcionForraje_ObtenerRangos", parameters);
                RegistroVigilanciaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapBoletaRecepcionForrajeDAL.ObtenerRangos(ds);
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
        /// Método que agrega un registro a la tabla "EntradaProductoMuestra" 
        /// Se agrega un solo registro por tratarse de un folio que cuya CalidadOrigen= 1
        /// </summary>
        internal void AgregarNuevoRegistro(EntradaProductoInfo entradaProducto)
        {
            try
            {
                Logger.Info();
                var parameters = AuxBoletaRecepcionForrajeDAL.AgregarNuevoRegistro(entradaProducto);
                Create("BoletaRecepcionForraje_CrearNuevoRegistro", parameters);
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
