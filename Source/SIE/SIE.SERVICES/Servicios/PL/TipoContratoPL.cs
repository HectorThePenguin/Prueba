using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class TipoContratoPL
    {
        /// <summary>
        /// Obtiene todos los tipos de contrato
        /// </summary>
        /// <returns>Lista de TipoContratoInfo</returns>
        public List<TipoContratoInfo> ObtenerTodos()
        {
            List<TipoContratoInfo> listaTipoContrato = new List<TipoContratoInfo>();
            TipoContratoBL tipoContratoBl = new TipoContratoBL();

            try
            {
                listaTipoContrato = tipoContratoBl.ObtenerTodos();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaTipoContrato;
        }
    }
}
