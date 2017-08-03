using System.Reflection;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Implementacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Servicios.BL
{
    internal class TipoCancelacionBL
    {
        /// <summary>
        /// Metodo para obtener la lista de los tipos de cancelacion activos
        /// </summary>
        /// <returns></returns>
        internal List<TipoCancelacionInfo> ObtenerTodos()
        {
            try
            {
                var tipoCancelacionDal = new TipoCancelacionDAL();
                List<TipoCancelacionInfo> listaTipoCancelacion = tipoCancelacionDal.ObtenerTodos();
                return listaTipoCancelacion;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
