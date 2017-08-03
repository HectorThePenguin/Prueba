
using System;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Servicios.PL;

namespace SIE.Services.Servicios.BL
{
    internal class SolicitudPremezclaBL
    {
        /// <summary>
        /// Guarda los datos para una solicitud de premezclas
        /// </summary>
        /// <param name="solicitud"></param>
        /// <returns></returns>
        internal bool Guardar(SolicitudPremezclaInfo solicitud)
        {
            try
            {
                Logger.Info();
                using (var transaction = new TransactionScope())
                {
                    var solicitudPremezclaDal = new SolicitudPremezclaDAL();
                    SolicitudPremezclaInfo solicitudPremezcla = solicitudPremezclaDal.Guardar(solicitud);
                    if (solicitudPremezcla != null)
                    {
                        foreach (var solicitudPremezclaDetalle in solicitud.ListaSolicitudPremezcla)
                        {
                            solicitudPremezclaDetalle.SolicitudPremezclaId = solicitudPremezcla.SolicitudPremezclaId;
                            solicitudPremezclaDetalle.UsuarioCreacion = new UsuarioInfo()
                            {
                                UsuarioID = solicitudPremezcla.UsuarioCreacion.UsuarioID
                            };
                            solicitudPremezclaDetalle.Activo = solicitud.Activo;
                        }
                        var solicitudPremezclaDetalleBl = new SolicitudPremezclaDetalleBL();
                        solicitudPremezclaDetalleBl.Guardar(solicitud.ListaSolicitudPremezcla);
                    }
                    transaction.Complete();
                }
                return true;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
