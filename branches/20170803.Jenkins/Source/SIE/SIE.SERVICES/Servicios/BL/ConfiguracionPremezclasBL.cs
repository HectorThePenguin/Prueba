using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Servicios.BL
{
    internal class ConfiguracionPremezclasBL
    {
        /// <summary>
        /// Hace el guardado de la premezcla
        /// </summary>
        internal void Guardar(PremezclaInfo premezclaInfo, List<PremezclaDetalleInfo> listaPremezclaDetalle, List<PremezclaDetalleInfo> listaPremezclaEliminados, int usuario)
        {
            try
            {

                var premezclaBl = new PremezclaBL();
                var premezclaDetalleBl = new PremezclaDetalleBL();
                var listaPremezclasModificados = new List<PremezclaDetalleInfo>();
                var listaPremezclasAgregadas = new List<PremezclaDetalleInfo>();
                using (var transaction = new TransactionScope())
                {
                    //Si no esta guardado se crea
                    int premezclaId;
                    if (!premezclaInfo.Guardado)
                    {
                        premezclaId = premezclaBl.Crear(premezclaInfo);
                    }
                    else
                    {
                        premezclaId = premezclaInfo.PremezclaId;
                    }

                    premezclaInfo.PremezclaId = premezclaId;

                    //Guardar detalles y actualizar modificados
                    listaPremezclasModificados.AddRange(listaPremezclaDetalle.Where(detalleInfo => detalleInfo.Guardado));
                    listaPremezclasAgregadas.AddRange(listaPremezclaDetalle.Where(detalleInfo => !detalleInfo.Guardado));

                    if (listaPremezclasAgregadas.Count > 0)
                    {
                        premezclaDetalleBl.Crear(listaPremezclasAgregadas, premezclaInfo);
                    }

                    if (listaPremezclasModificados.Count > 0)
                    {
                        foreach (var listaPremezclasModificadoP in listaPremezclasModificados)
                        {
                            listaPremezclasModificadoP.UsuarioModificacionId = usuario;
                        }
                        premezclaDetalleBl.Actualizar(listaPremezclasModificados);
                    }

                    //Desactivar eliminados
                    if (listaPremezclaEliminados.Count > 0)
                    {
                        foreach (var listaPremezclaEliminado in listaPremezclaEliminados)
                        {
                            listaPremezclaEliminado.Activo = EstatusEnum.Inactivo;
                        }
                        premezclaDetalleBl.Actualizar(listaPremezclaEliminados);
                    }

                    transaction.Complete();
                }
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
