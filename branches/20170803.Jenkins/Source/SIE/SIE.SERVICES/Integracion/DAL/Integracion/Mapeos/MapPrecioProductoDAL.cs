using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Services.Info.Info;
using SIE.Base.Infos;
using System.Data;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapPrecioProductoDAL
    {
        internal static ResultadoInfo<PrecioProductoInfo> ObtenerPorPagina(System.Data.DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<PrecioProductoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select new PrecioProductoInfo
                     {
                         Organizacion = new OrganizacionInfo
                         {
                             OrganizacionID = info.Field<int>("OrganizacionID"),
                             Descripcion = info.Field<string>("DescripcionOrganizacion"),
                         },
                         Producto = new ProductoInfo
                         {
                             ProductoId = info.Field<int>("ProductoID"),
                             Descripcion = info.Field<string>("DescripcionProducto"),
                         },
                         PrecioMaximo = info.Field<decimal>("PrecioMaximo"),
                         Activo = info.Field<bool>("Activo").BoolAEnum(),
                         UsuarioCreacionID = info.Field<int>("UsuarioCreacionID"),
                         FechaCreacion = info.Field<DateTime>("FechaCreacion")
                     }).ToList();

                var resultado =
                    new ResultadoInfo<PrecioProductoInfo>
                    {
                        Lista = lista,
                        TotalRegistros =
                            Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
                    };
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
