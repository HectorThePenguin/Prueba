using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    public class MarcasBL
    {
        /// <summary>
        /// Método que se utiliza para guardar la información de una nueva marca.
        /// </summary>
        /// <param name="marcasInfo"> Objeto con la información de la marca a guardar. </param>
        /// <returns> Objeto con la información de la marca guardada </returns>
        public MarcasInfo GuardarMarca(MarcasInfo marcasInfo)
        {
            MarcasInfo result;
            try
            {
                Logger.Info();
                var marcasDAL = new MarcasDAL();
                if (marcasInfo.MarcaId == 0)
                {
                    result = marcasDAL.GuardarMarca(marcasInfo);    
                }
                else
                {
                    result = marcasDAL.ActualizarMarca(marcasInfo);
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
            return result;
        }

        /// <summary>
        /// Método utilizado para verificar la existencia de la marca antes de guardarla.
        /// </summary>
        /// <param name="marcasInfo"> Objeto con la información de la marca. </param>
        /// <returns> Objeto con la información de la marca encontrada. </returns>
        public MarcasInfo VerificaExistenciaMarca(MarcasInfo marcasInfo)
        {
            try
            {
                Logger.Info();
                var marcasDAL = new MarcasDAL();
                marcasInfo = marcasDAL.VerificaExistenciaMarca(marcasInfo);
                return marcasInfo;
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
        /// Método utilizado para obtener una lista de las marcas registradas filtradas por el paginador.
        /// </summary>
        /// <param name="pagina"> Información del filtro de paginador </param>
        /// <param name="filtro"> Información del filtro de la marca </param>
        /// <returns> Lista con las marcas encontradas </returns>
        public ResultadoInfo<MarcasInfo> ObtenerPorPagina(PaginacionInfo pagina, MarcasInfo filtro)
        {
            ResultadoInfo<MarcasInfo> result;
            try
            {
                Logger.Info();
                var marcasDAL = new MarcasDAL();
                result = marcasDAL.ObtenerPorPagina(pagina, filtro);
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
            return result;
        } 

        /// <summary>
        /// Método para obtener todas las marcas
        /// </summary>
        /// <returns> Lista con las marcas encontradas. </returns>
        public IList<MarcasInfo> ObtenerMarcas(EstatusEnum Tipo, EstatusEnum Activo)
        {
            IList<MarcasInfo> result;
            try
            {
                Logger.Info();
                var marcasDAL = new MarcasDAL();
                result = marcasDAL.ObtenerMarcas(Tipo, Activo);

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
            return result;
        }
        
    }
}
