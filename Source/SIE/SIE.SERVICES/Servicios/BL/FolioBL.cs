using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class FolioBL
    {
        /// <summary>
        /// Se remplaza el arete por un arete del mismo corral
        /// </summary>
        public long ObtenerFolio(int organizacionID, TipoFolio tipoFolio)
        {
            try
            {
                Logger.Info();
                var folioDAL = new FolioDAL();
                long folio = folioDAL.ObtenerFolio(organizacionID, tipoFolio);
                return folio;
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
