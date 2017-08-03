using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIE.Base.Infos;

namespace SIE.Base.Interfaces
{
    public interface IAyuda<T>
    {
        /// <summary>
        /// Metodo para obtener Datos de Ayuda por Id
        /// </summary>
        /// <param name="Id">Clave por la Cual se Realizara la Busqueda</param>
        /// <returns>Objeto con Lista de Datos</returns>
        ResultadoInfo<T> ObtenerPorId(int Id);

        /// <summary>
        /// Metodo para obtener Datos de Ayuda por Descripcion
        /// </summary>
        /// <param name="pagina">Objeto con el Cual se especificara las Opciones de Paginado</param>
        /// <param name="Descripcion">Descripcion por la Cual se Realizara la Busqueda</param>
        /// <returns>Objeto con Lista de Datos y Total de Paginas</returns>
        ResultadoInfo<T> ObtenerPorDescripcion(PaginacionInfo Pagina, String Descripcion);
    }
}