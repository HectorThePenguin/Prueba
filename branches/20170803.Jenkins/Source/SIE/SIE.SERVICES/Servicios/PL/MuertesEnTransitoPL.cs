using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System.IO;
using SIE.Services.Info.Modelos;

namespace SIE.Services.Servicios.PL
{
    public class MuertesEnTransitoPL
    {

        public MemoryStream Guardar(MuertesEnTransitoInfo muerteEnTransito, List<AnimalInfo> animales)
        {
            try
            {
                Logger.Info();
                var muerteEnTransitoBl = new MuertesEnTransitoBL();
                MemoryStream result = muerteEnTransitoBl.Guardar(muerteEnTransito, animales);
                return result;
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
        /// Obtiene las entradas de ganado con muertes pendientes a registrar en la ayuda Folio de Entrada.
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<MuertesEnTransitoInfo> ObtenerPorPagina(PaginacionInfo pagina, MuertesEnTransitoInfo filtro)
        {
            try
            {
                Logger.Info();
                var muelrtesEnTransitoBl = new MuertesEnTransitoBL();
                ResultadoInfo<MuertesEnTransitoInfo> result = muelrtesEnTransitoBl.ObtenerPorPagina(pagina, filtro);
                return result;
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
        /// Obtiene la informacion de la entrada de gandado por Folio de entrada y Organizacion
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public MuertesEnTransitoInfo ObtenerPorFolioEntrada(MuertesEnTransitoInfo filtro)
        {
            MuertesEnTransitoInfo muerteEnTransitoInfo;
            try
            {
                Logger.Info();
                var muelrtesEnTransitoBl = new MuertesEnTransitoBL();
                muerteEnTransitoInfo = muelrtesEnTransitoBl.ObtenerPorFolioEntrada(filtro);
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
            return muerteEnTransitoInfo;
        }

        /// <summary>
        /// Obtiene los aretes de los animales que correspondan al Folio de Entrada y la Organizacion
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<AnimalInfo> ObtenerAretesPorFolioEntrada(MuertesEnTransitoInfo filtro)
        {
            try
            {
                Logger.Info();
                var muelrtesEnTransitoBl = new MuertesEnTransitoBL();
                List<AnimalInfo> result = muelrtesEnTransitoBl.ObtenerAnimalesPorFolioEntrada(filtro);
                return result;
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

        public int ObtenerTotalFoliosValidos(int organizacionId)
        {
            int result = 0;
            try
            {
                Logger.Info();
                var muelrtesEnTransitoBl = new MuertesEnTransitoBL();
                result = muelrtesEnTransitoBl.ObtenerTotalFoliosValidos(organizacionId);
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

        public ValidacionesFolioVentaMuerte ValidarFolio(int folioEntrada, int organizacionId, List<string> aretes)
        {
            ValidacionesFolioVentaMuerte result = new ValidacionesFolioVentaMuerte();

            try
            {
                Logger.Info();
                var muelrtesEnTransitoBl = new MuertesEnTransitoBL();
                result = muelrtesEnTransitoBl.ValidarFolio(folioEntrada, organizacionId, aretes);
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
