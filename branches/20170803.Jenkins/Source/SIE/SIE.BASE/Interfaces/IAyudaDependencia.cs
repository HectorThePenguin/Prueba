using System;
using System.Collections.Generic;
using SIE.Base.Infos;

namespace SIE.Base.Interfaces
{
    public interface IAyudaDependencia<T>
    {
        /// <summary>
        /// Metodo para obtener Datos de Ayuda por Id
        /// </summary>
        /// <param name="id">Clave por la Cual se Realizara la Busqueda</param>
        /// <param name="dependencias">Lista de Objetos de los Cuales Dependera la Busqueda</param>
        /// <returns>Objeto con Lista de Datos</returns>
        ResultadoInfo<T> ObtenerPorId(int id, IList<IDictionary<IList<String>, Object>> dependencias);

        /// <summary>
        /// Metodo para obtener Datos de Ayuda por Descripcion
        /// </summary>
        /// <param name="pagina">Objeto con el Cual se especificara las Opciones de Paginado</param>
        /// <param name="descripcion">Descripcion por la Cual se Realizara la Busqueda</param>
        /// <param name="dependencias">Lista de Objetos de los Cuales Dependera la Busqueda</param>
        /// <returns>Objeto con Lista de Datos y Total de Paginas</returns>
        ResultadoInfo<T> ObtenerPorDescripcion(PaginacionInfo pagina, String descripcion,
                                               IList<IDictionary<IList<String>, Object>> dependencias);
    }
}