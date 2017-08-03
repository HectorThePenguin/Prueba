
using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class RecepcionProductoDetalleDAL:DALBase
    {
        /// <summary>
        /// Guarda el detalle de una solicitud
        /// </summary>
        /// <param name="listaRecepcionProductoDetalle"></param>
        internal void Guardar(List<RecepcionProductoDetalleInfo> listaRecepcionProductoDetalle)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRecepcionProductoDetalleDAL.ObtenerParametroGuardar(listaRecepcionProductoDetalle);
                Create("RecepcionProductoDetalle_Crear", parameters);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
