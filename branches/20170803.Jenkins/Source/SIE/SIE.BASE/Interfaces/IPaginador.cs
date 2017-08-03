using SIE.Base.Infos;

namespace SIE.Base.Interfaces
{
    public interface IPaginador<T>
    {
        /// <summary>
        /// Metodo para Obtener una Lista Paginada
        /// </summary>
        /// <param name="pagina">Objeto con el Cual se especificara las Opciones de Paginado</param>
        /// <param name="filtro">Objeto que servira como Filtro</param>
        /// <returns>Objeto con Lista de Datos y Total de Paginas</returns>
        ResultadoInfo<T> ObtenerPorPagina(PaginacionInfo pagina, T filtro);
    }
}