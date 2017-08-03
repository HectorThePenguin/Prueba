using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.Integracion.DAL.Excepciones;
using System.Linq;

namespace SIE.Services.Servicios.BL
{
    public class ComisionBL
    {
        /// <summary>
        /// Obtiene el catalogo de tipos de comision
        /// </summary>
        /// <returns>Lista de tipos de comision</returns>
        public List<TipoComisionInfo> ObtenerTiposComision()
        {
            List<TipoComisionInfo> tiposComision = new List<TipoComisionInfo>();

            try
            {
                Logger.Info();
                var comisionDAL = new ComisionDAL();
                tiposComision = comisionDAL.ObtenerTiposComisiones();

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

            return tiposComision;
        }

        /// <summary>
        /// Guarda las comisiones del proveedor
        /// </summary>
        /// <param name="comsiones">Lista de comisiones a guardar</param>
        /// <returns>Retorna verdadero cuando los datos se guardaron correctamente</returns>
        public bool GuardarComisiones(List<ComisionInfo> comsiones)
        {
            bool resultado = false;
            try
            {
                Logger.Info();
                var comisionDAL = new ComisionDAL();
                resultado = comisionDAL.GuardarComisiones(comsiones);

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

            return resultado;
        }


        /// <summary>
        /// Obtiene la lista de comisiones asignadas al proveedor identificado
        /// </summary>
        /// <param name="ProveedorID">ID del Proveedor</param>
        /// <returns></returns>
        public List<ComisionInfo> obtenerComisionesProveedor(int ProveedorID)
        {
            List<ComisionInfo> resultado = new List<ComisionInfo>();
            try
            {
                Logger.Info();
                var comisionDAL = new ComisionDAL();
                resultado = comisionDAL.obtenerComisionesProveedor(ProveedorID);

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

            return resultado;
        }
    }
}
