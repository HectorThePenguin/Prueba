using System.Reflection;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Servicios.PL
{
    public class TipoCancelacionPL
    {
        /// <summary>
        /// Metodo para obtener todos los tipos de cancelacion activos
        /// </summary>
        /// <returns></returns>
        public List<TipoCancelacionInfo> ObtenerTodos()
        {
            try
            {
                var tipoCancelacionBl = new TipoCancelacionBL();
                List<TipoCancelacionInfo> listaTipoCancelacion = tipoCancelacionBl.ObtenerTodos();
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
