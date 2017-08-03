using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class MarcasPL
    {
        /// <summary>
        /// Método utilizado para dar de alta una nueva marca
        /// </summary>
        /// <param name="marcasInfo"> Objeto con la información de la marca a guardar. </param>
        /// <returns> Información de la marca guardada </returns>
        public MarcasInfo GuardarMarca(MarcasInfo marcasInfo)
        {
            MarcasInfo result;
            try
            {
                Logger.Info();
                var marcasBL = new MarcasBL();
                result = marcasBL.GuardarMarca(marcasInfo);

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
        /// Método que se utiliza para obtener los registros de marcas filtrados por el paginador
        /// </summary>
        /// <param name="pagina"> Informacion del filtro del paginador </param>
        /// <param name="filtro"> Objeto con la informacion del filtro de la marca. </param>
        /// <returns> Lista de marcas encontradas </returns>
        public ResultadoInfo<MarcasInfo> ObtenerPorPagina(PaginacionInfo pagina, MarcasInfo filtro)
        {
            ResultadoInfo<MarcasInfo> result;
            try
            {
                Logger.Info();
                var marcasBL = new MarcasBL();
                result = marcasBL.ObtenerPorPagina(pagina, filtro);
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
        /// Método que obtiene todas las marcas registradas.
        /// </summary>
        /// <returns> Lista con las marcas encontradas </returns>
        public IList<MarcasInfo> ObtenerMarcas(EstatusEnum Tipo, EstatusEnum Activo)
        {
            IList<MarcasInfo> result;
            try
            {
                Logger.Info();
                var marcasBL = new MarcasBL();
                result = marcasBL.ObtenerMarcas(Tipo, Activo);

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
        /// Método que se utiliza para comprobar que no exista la marca antes de guardarla
        /// </summary>
        /// <param name="marcasInfo"> Objeto con la información de la marca. </param>
        /// <returns> Información de la marca en caso de encontrarla </returns>
        public MarcasInfo VerificaExistenciMarca(MarcasInfo marcasInfo)
        {
            try
            {
                Logger.Info();
                var marcasBL = new MarcasBL();
                marcasInfo = marcasBL.VerificaExistenciaMarca(marcasInfo);

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
            return marcasInfo;
        }
    }
}
