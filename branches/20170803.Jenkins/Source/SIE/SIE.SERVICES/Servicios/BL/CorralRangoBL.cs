using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class CorralRangoBL
    {
        /// <summary>
        ///  Metodo que regresa una lista los Corrales Disponibles por OrganizacionID
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal IList<CorralRangoInfo> ObtenerPorOrganizacionID(int organizacionID)
        {
            IList<CorralRangoInfo> lista;
            try
            {
                Logger.Info();
                var corralRangoDal = new CorralRangoDAL();
                lista = corralRangoDal.ObtenerPorOrganizacionID(organizacionID);
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
            return lista;
        }

        /// <summary>
        /// Obtiene la lista de corrales configurados
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal IList<CorralRangoInfo> ObtenerConfiguradosPorOrganizacionID(int organizacionID)
        {
            IList<CorralRangoInfo> lista;
            try
            {
                Logger.Info();
                var corralRangoDal = new CorralRangoDAL();
                lista = corralRangoDal.ObtenerCorralesConfiguradosPorOrganizacionID(organizacionID);
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
            return lista;
        }

        
        /// <summary>
        /// Crea un registro en la tabla de CorralRango
        /// </summary>
        /// <param name="info"></param>
        internal void Crear(CorralRangoInfo info)
        {
            try
            {
                Logger.Info();
                var corralRangoDal = new CorralRangoDAL();
                corralRangoDal.Crear(info);
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
        /// <summary>
        /// Actualiza un registro en la tabla de CorralRango
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(List<CorralRangoInfo> info)
        {
            try
            {
                Logger.Info();
                var corralRangoDal = new CorralRangoDAL();
                corralRangoDal.Actualizar(info);
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
        /// <summary>
        /// Metodo que regresa un boolean true si el corral tiene o false si no tiene lote asignado
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="corralID"></param>
        /// <returns></returns>
        internal Boolean ObtenerLoteAsignado(int organizacionID, int corralID)
        {
            bool tieneLoteAsignado;
            try
            {
                Logger.Info();
                var corralRangoDal = new CorralRangoDAL();
                tieneLoteAsignado = corralRangoDal.ObtenerLoteAsignado(organizacionID, corralID);
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
            return tieneLoteAsignado;
        }

        /// <summary>
        /// Funcion qque obtiene el corral destino
        /// </summary>
        /// <param name="corralRangoInfo"></param>
        /// <param name="dias"></param>
        internal IList<CorralRangoInfo> ObtenerCorralDestino(CorralRangoInfo corralRangoInfo, int dias)
        {
            IList<CorralRangoInfo> lista;
            try
            {
                Logger.Info();
                var corralRangoDal = new CorralRangoDAL();
                lista = corralRangoDal.ObtenerCorralDestino(corralRangoInfo, dias);
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
            return lista;
        }

        /// <summary>
        /// Funcion qque obtiene el corral destino
        /// </summary>
        /// <param name="corralRangoInfo"></param>
        /// <param name="diasBloqueo"></param>
        internal IList<CorralRangoInfo> ObtenerCorralDestinoSinTipoGanado(CorralRangoInfo corralRangoInfo, int diasBloqueo)
        {
            IList<CorralRangoInfo> lista;
            try
            {
                Logger.Info();
                var corralRangoDal = new CorralRangoDAL();
                lista = corralRangoDal.ObtenerCorralDestinoSinTipoGanado(corralRangoInfo, diasBloqueo);
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
            return lista;
        }

        /// <summary>
        /// Metodo para eliminar la configuracion de corrales
        /// </summary>
        /// <param name="corralGrid"></param>
        internal void Eliminar(CorralRangoInfo corralGrid)
        {
            try
            {
                Logger.Info();
                var corralRangoDal = new CorralRangoDAL();
                corralRangoDal.Eliminar(corralGrid);

                //Se elimina la programacion de servicio de alimento
                var servicioAlimentoBL = new ServicioAlimentoBL();
                servicioAlimentoBL.Eliminar(corralGrid);

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

        /// <summary>
        /// Metodo para eliminar la configuracion de corrales
        /// </summary>
        /// <param name="lista"></param>
        /// <param name="corralesEliminados"></param>
        internal void Guardar(List<CorralRangoInfo> lista, IList<CorralRangoInfo> corralesEliminados)
        {
             try
            {
                using (var transaction = new TransactionScope())
                {
                    Logger.Info();
                    var corralRangoDal = new CorralRangoDAL();
                    var listaActualizar = lista.Where(
                                corralGrid => corralGrid.Accion == AccionConfigurarCorrales.Actualizar
                            ).ToList();

                    //Se Actualizan en caso de existir cambios
                    if (listaActualizar.Count > 0)
                    {
                        corralRangoDal.Actualizar(listaActualizar);
                    }

                    //Se agregan los nuevos
                    foreach (var corralGrid in lista.Where(
                                corralGrid => corralGrid.Accion == AccionConfigurarCorrales.Agregar)
                            )
                    {
                        Eliminar(corralGrid);
                        corralRangoDal.Crear(corralGrid);
                    }

                    //Se Eliminan las configuraciones
                    foreach (var corralGrid in corralesEliminados)
                    {
                        Eliminar(corralGrid);
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


