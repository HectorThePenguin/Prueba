namespace SIE.Base.Validadores
{
    public static class AyudaValidador
    {
        /// <summary>
        /// Valida si el valor es Numerico, para asignarlo al control de ayuda
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public static bool EsValorNumerico(string numero)
        {
            var esNumerico = false;            

            if (!numero.StartsWith("0"))
            {
                int resultado;
                if (int.TryParse(numero, out resultado))
                {
                    esNumerico = true;
                }
            }
            return esNumerico;
        }
    }
}