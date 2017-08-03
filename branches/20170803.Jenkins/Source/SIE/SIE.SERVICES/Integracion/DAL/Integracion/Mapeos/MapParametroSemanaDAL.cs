using System;
using System.Data;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapParametroSemanaDAL
    {
        /// <summary>
        ///     Método asigna el registro del parametro semana obtenido
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ParametroSemanaInfo ObtenerParametroSemana(DataSet ds)
        {
            ParametroSemanaInfo parametroSemanaInfo;
            try
            {

                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                parametroSemanaInfo = new ParametroSemanaInfo();
                foreach (DataRow dr in dt.Rows)
                {
                    parametroSemanaInfo.ParametroSemanaID = Convert.ToInt32(dr["ParametroSemanaID"]);
                    parametroSemanaInfo.Descripcion = Convert.ToString(dr["Descripcion"]);
                    parametroSemanaInfo.Lunes = Convert.ToBoolean(dr["Lunes"]);
                    parametroSemanaInfo.Martes = Convert.ToBoolean(dr["Martes"]);
                    parametroSemanaInfo.Miercoles = Convert.ToBoolean(dr["Miercoles"]);
                    parametroSemanaInfo.Jueves = Convert.ToBoolean(dr["Jueves"]);
                    parametroSemanaInfo.Viernes = Convert.ToBoolean(dr["Viernes"]);
                    parametroSemanaInfo.Sabado = Convert.ToBoolean(dr["Sabado"]);
                    parametroSemanaInfo.Domingo = Convert.ToBoolean(dr["Domingo"]);
                    parametroSemanaInfo.UsuarioCreacionID = Convert.ToInt32(dr["UsuarioCreacionID"]);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametroSemanaInfo;
        }

        /// <summary>
        ///     Método asigna el registro del parametro semana obtenido
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static int ObtenerNumeroSemana(DataSet ds)
        {
            int numeroSemana;
            try
            {

                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                numeroSemana = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    numeroSemana = Convert.ToInt32(dr["Semana"]);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return numeroSemana;
        }
    }
}
