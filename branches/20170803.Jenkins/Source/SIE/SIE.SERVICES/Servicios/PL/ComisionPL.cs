using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ComisionPL
    {
        public List<TipoComisionInfo> obtenerTiposComision()
        {
            ComisionBL comicionBL = new ComisionBL();

            var listaComisiones = comicionBL.ObtenerTiposComision();

            return listaComisiones;
        }

        // <summary>
        /// Metrodo Para Guardar en en la tabla Animal
        /// </summary>
        public bool GuardarComisiones(List<ComisionInfo> comisionInfo)
        {
            bool result;
            try
            {
                Logger.Info();
                ComisionBL comisionBL = new ComisionBL();
                result = comisionBL.GuardarComisiones(comisionInfo);
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
            return result;
        }

        /// <summary>
        /// Obtiene las comisiones capturadas para el proveedor solicitado
        /// </summary>
        /// <param name="ProveedorID">ID del Proveeod</param>
        /// <returns>Lista de comisiones</returns>
        public List<ComisionInfo> obtenerComisionesProveedor(int ProveedorID)
        {
            ComisionBL comisionBL = new ComisionBL();
            List<ComisionInfo> lsComisiones = new List<ComisionInfo>();

            lsComisiones = comisionBL.obtenerComisionesProveedor(ProveedorID);

            return lsComisiones;
        }
    }
}
