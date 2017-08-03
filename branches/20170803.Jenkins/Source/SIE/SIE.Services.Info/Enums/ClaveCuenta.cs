using System.ComponentModel;

namespace SIE.Services.Info.Enums
{
    public enum ClaveCuenta
    {
       [DescriptionAttribute("CTAINVGAN")]
        CuentaInventarioGanadera,

       [DescriptionAttribute("CTAINVMAQ")]
       CuentaInventarioMaquila,

       [DescriptionAttribute("CTAINVINT")]
       CuentaInventarioIntensivo,

       [DescriptionAttribute("CTAINVCEN")]
       CuentaInventarioCentro,

       [DescriptionAttribute("CTAINVPRA")]
       CuentaInventarioPradera,

       [DescriptionAttribute("CTAINVTRAN")]
       CuentaInventarioTransito,

       [DescriptionAttribute("CTACTOGAN")]
       CuentaCostoInventario,

       [DescriptionAttribute("CTABNFGAN")]
       CuentaBeneficioInventario,

       [DescriptionAttribute("CTACTOMP")]
       CuentaCostoMP,
        
       [DescriptionAttribute("CTABNFMP")]
       CuentaBeneficioMP,

       [DescriptionAttribute("CTOGANINT")]
       SalidaMuerteIntensiva,

       [DescriptionAttribute("CTABENINT")]
       CuentaBeneficioIntensivo,
    }
}
