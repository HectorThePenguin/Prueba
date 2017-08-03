using System;
using System.Web;
using System.Web.Services;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace SIE.Web.Sanidad
{
    public partial class GanadoVago : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static CorralInfo ObtenerCorralArete(string arete)
        {
            CorralInfo corral = null;

            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                int organizacionId = 0;

                if (seguridad != null)
                {
                    organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                }

                var animalPl = new AnimalPL();
                var corralPl = new CorralPL();
                var animalInfo = animalPl.ObtenerAnimalPorArete(arete, organizacionId);

                if (animalInfo != null)
                {
                    var ultimoMovimiento = animalPl.ObtenerUltimoMovimientoAnimal(animalInfo);
                    if (ultimoMovimiento != null)
                    {
                        corral = corralPl.ObtenerPorId(ultimoMovimiento.CorralID);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            return corral;
        }

        [WebMethod]
        public static CorralInfo ObtenerCorralAreteTestigo(string areteTestigo)
        {
            CorralInfo corral = null;

            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                int organizacionId = 0;

                if (seguridad != null)
                {
                    organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                }

                var animalPl = new AnimalPL();
                var corralPl = new CorralPL();
                var animalInfo = animalPl.ObtenerAnimalPorAreteTestigo(areteTestigo, organizacionId);

                if (animalInfo != null)
                {
                    var ultimoMovimiento = animalPl.ObtenerUltimoMovimientoAnimal(animalInfo);
                    if (ultimoMovimiento != null)
                    {
                        corral = corralPl.ObtenerPorId(ultimoMovimiento.CorralID);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            return corral;
        }

        [WebMethod]
        public static CorralInfo ObtenerCorralAmbosArete(string arete, string areteTestigo)
        {
            CorralInfo corral = null;

            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                int organizacionId = 0;

                if (seguridad != null)
                {
                    organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                }

                var animalPl = new AnimalPL();
                var corralPl = new CorralPL();
                var animalInfo = animalPl.ObtenerAnimalPorArete(arete, organizacionId);

                if (animalInfo != null)
                {
                    if (animalInfo.AreteMetalico == areteTestigo)
                    {
                        var ultimoMovimiento = animalPl.ObtenerUltimoMovimientoAnimal(animalInfo);
                        if (ultimoMovimiento != null)
                        {
                            corral = corralPl.ObtenerPorId(ultimoMovimiento.CorralID);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            return corral;
        }
    }
}