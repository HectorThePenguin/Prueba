using SIE.Services.Info.Enums;

namespace SIE.WinForm.Auxiliar
{
    public static class AuxDivision
    {
        public static string ObtenerDivision(int organizacionID)
        {
            string division;
            if (organizacionID == Organizacion.GanaderaIntegralCentinela.GetHashCode())
            {
                division = Properties.Resources.DivisionAbreviadaCentinela;
            }
            else if (organizacionID == Organizacion.GanaderaIntegralLucero.GetHashCode())
            {
                division = Properties.Resources.DivisionAbreviadaLucero;
            }
            else if (organizacionID == Organizacion.GanaderaIntegralMonarca.GetHashCode())
            {
                division = Properties.Resources.DivisionAbreviadaMonarca;
            }
            else if (organizacionID == Organizacion.GanaderaIntegralVizur.GetHashCode())
            {
                division = Properties.Resources.DivisionAbreviadaVizur;
            }
            else if (organizacionID == Organizacion.GanaderaIntegralSK.GetHashCode())
            {
                division = Properties.Resources.DivisionAbreviadaSK;
            }
            else
            {
                division = string.Empty;
            }
            return division;
        }
    }
}