namespace SIE.Base.Constantes
{
    public static class Constante
    {
        /// <summary>
        /// Plantilla para la cadena de conexion 
        /// </summary>
        public static string SqlConexion
        {
            get { return @"Data Source={0};Initial Catalog={1};User ID={2};Password={3}"; }
        }
    }
}
