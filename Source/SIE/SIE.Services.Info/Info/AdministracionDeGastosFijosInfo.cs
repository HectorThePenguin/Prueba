
namespace SIE.Services.Info.Info
{
   public class AdministracionDeGastosFijosInfo : BitacoraInfo
   {
        /// <summary>
        /// Identificador de los gastos fijos
        /// </summary>
       public int GastoFijoID { get; set; }
       
       /// <summary>
        /// Identificador de los gastos fijos
        /// </summary>
       private string descripcion { get; set; }

       /// <summary>
       /// Identificador de los gastos fijos
       /// </summary>
       public decimal Importe { get; set; }

       /// <summary>
       /// Descripcion de la condicion de ganado
       /// </summary>
       public string Descripcion
       {
           get { return descripcion != null ? descripcion.Trim() : descripcion; }
           set
           {
               if (value != descripcion)
               {
                   descripcion = value != null ? value.Trim() : null;
               }
           }
       }
   }
}