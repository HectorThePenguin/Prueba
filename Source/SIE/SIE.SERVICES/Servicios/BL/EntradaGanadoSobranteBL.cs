using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL;

namespace SIE.Services.Servicios.BL
{
    internal class EntradaGanadoSobranteBL
    {
        /// <summary>
        ///     Metodo que crear una entrada de ganado
        /// </summary>
        /// <param name="entradaGanadoSobranteInfo"></param>
        internal int GuardarEntradaGanadoSobrante(EntradaGanadoSobranteInfo entradaGanadoSobranteInfo)
        {
            try
            {
                Logger.Info();
                var entradaGanadoSobranteDAL = new EntradaGanadoSobranteDAL();
                var entradaGanadoTransitoBL = new EntradaGanadoTransitoBL();
                var parametroBL = new ParametroOrganizacionBL();
                var animalBl = new AnimalBL();
                int entradaGanadoID = 0;

                /* Obtener Tipo de organizacion Origen*/
                ParametrosEnum corralFaltante =
                    entradaGanadoSobranteInfo.EntradaGanado.TipoOrganizacionOrigenId ==
                    (int)TipoOrganizacion.CompraDirecta ?
                        ParametrosEnum.CORRALFALTDIRECTA :
                        ParametrosEnum.CORRALFALTPROPIO;

                /* Obtener Codigo Corral de parametroOrganizacion*/
                ParametroOrganizacionInfo parametroOrganizacionInfo =
                parametroBL.ObtenerPorOrganizacionIDClaveParametro(
                    entradaGanadoSobranteInfo.EntradaGanado.OrganizacionID,
                    corralFaltante.ToString());

                if (parametroOrganizacionInfo != null)
                {
                    /* Obtener entradaGanadoTransitoInfo del corral faltante parametrizado*/
                    EntradaGanadoTransitoInfo entradaGanadoTransitoInfo =
                        entradaGanadoTransitoBL.ObtenerPorCorralOrganizacion(
                            parametroOrganizacionInfo.Valor,
                            entradaGanadoSobranteInfo.EntradaGanado.OrganizacionID);

                    if (entradaGanadoTransitoInfo != null && entradaGanadoTransitoInfo.Cabezas > 0)
                    {
                        /* Obtener el importe prorrateado mediante el siguiente calculo: Importe = Importe / Cabezas */
                        List<EntradaGanadoTransitoDetalleInfo> detalles = entradaGanadoTransitoInfo.EntradasGanadoTransitoDetalles;
                        for (int indexDetalle = 0; indexDetalle < detalles.Count; indexDetalle++)
                        {
                            entradaGanadoSobranteInfo.Importe = detalles[indexDetalle].Importe / entradaGanadoTransitoInfo.Cabezas;

                            /* Se almacena la entrada Ganado Sobrante*/
                            entradaGanadoID =
                                entradaGanadoSobranteDAL.GuardarEntradaGanadoSobrante(entradaGanadoSobranteInfo);

                            /* Se actuacon los datos a actualizar en la tabla entradaGanadoTransito */
                            detalles[indexDetalle].Importe = entradaGanadoSobranteInfo.Importe * -1;
                            entradaGanadoTransitoInfo.Peso = (entradaGanadoTransitoInfo.Peso /
                                                              entradaGanadoTransitoInfo.Cabezas) * -1;
                            entradaGanadoTransitoInfo.Cabezas = -1;
                            entradaGanadoTransitoInfo.UsuarioModificacionID = entradaGanadoSobranteInfo.UsuarioCreacionID;

                            /*  Al ingresar los registros sobrantes se deben descontar de los faltantes de la tabla 'EntradaGanadoTransito' */
                            entradaGanadoTransitoBL.Guardar(entradaGanadoTransitoInfo);
                        }
                        /* Actualziar el Peso Compra del Animal */
                        entradaGanadoSobranteInfo.Animal.UsuarioModificacionID = entradaGanadoSobranteInfo.UsuarioCreacionID;
                        entradaGanadoSobranteInfo.Animal.PesoCompra = (int)entradaGanadoTransitoInfo.Peso;
                        animalBl.ActualizaPesoCompra(entradaGanadoSobranteInfo.Animal);
                    }
                    else
                    {
                        throw new ExcepcionDesconocida("No hay suficientes cabezas faltantes para cubrir las sobrantes de la partida. Favor de validar.");
                    }
                }
                else
                {
                    throw new ExcepcionDesconocida("No existe un corral de cabezas faltantes configurado. Favor de validar.");
                }
                return entradaGanadoID;
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
        /// Metodo para obtener las cabezas sobrantes de una partida
        /// </summary>
        /// <param name="entradaGanadoId"></param>
        /// <returns></returns>
        internal List<EntradaGanadoSobranteInfo> ObtenerSobrantePorEntradaGanado(int entradaGanadoId)
        {
            List<EntradaGanadoSobranteInfo> entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoSobranteDAL = new EntradaGanadoSobranteDAL();
                entradaGanadoInfo = entradaGanadoSobranteDAL.ObtenerSobrantePorEntradaGanado(entradaGanadoId);
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
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Metodo para Cambiar el estado de cposteado de un animal en la tabla EntradaGanadoSobrante
        /// </summary>
        /// <param name="cabezasSobrante"></param>
        /// <returns></returns>
        internal int ActualizarCosteadoEntradaGanadoSobrante(List<EntradaGanadoSobranteInfo> cabezasSobrante)
        {
            int resp;
            try
            {
                Logger.Info();
                var entradaGanadoSobranteDAL = new EntradaGanadoSobranteDAL();
                resp = entradaGanadoSobranteDAL.ActualizarCosteadoEntradaGanadoSobrante(cabezasSobrante);
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
            return resp;
        }
    }
}
