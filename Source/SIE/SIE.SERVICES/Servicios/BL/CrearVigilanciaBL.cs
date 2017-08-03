using System;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Servicios.BL
{
    public class CrearVigilanciaBL
    {
        /// <summary>
        /// Se utiliza para guardar datos en la tabla RegistroVigilancia
        /// </summary>
        /// <param name="registrovigilanciainfo"></param>
        /// <returns></returns>
        internal int GuardarDatos(RegistroVigilanciaInfo registrovigilanciainfo)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    Logger.Info();
                    var registrovigilanciaBl = new RegistroVigilanciaBL();
                    int resultado = registrovigilanciaBl.GuardarDatos(registrovigilanciainfo);

                    if (resultado > 0 && 
                        registrovigilanciainfo.Producto.SubFamilia.SubFamiliaID == (int)SubFamiliasEnum.MicroIngredientes)
                    {
                        //Obtener Info RegistroVigilancia
                        var registroVigilanciaInfo = new RegistroVigilanciaInfo
                        {
                            FolioTurno = resultado,
                            Organizacion = registrovigilanciainfo.Organizacion
                        };
                        var registroVigilanciaBL = new RegistroVigilanciaBL();
                        registroVigilanciaInfo = registroVigilanciaBL.ObtenerRegistroVigilanciaPorFolioTurno(registroVigilanciaInfo);

                        //Almacenar entrada Producto
                        var entradaProductoBL = new EntradaProductoBL();

                        var estatus = new EstatusInfo() {EstatusId = (int)Estatus.Aprobado};

                        var entradaProducto = new EntradaProductoInfo
                        {
                            Organizacion = registrovigilanciainfo.Organizacion,
                            Producto =  registrovigilanciainfo.Producto,
                            RegistroVigilancia = registroVigilanciaInfo,
                            UsuarioCreacionID = registrovigilanciainfo.UsuarioCreacionID,
                            Estatus = estatus,
                            Observaciones = "Entrada de premezcla"
                        };

                        entradaProducto =
                            entradaProductoBL.GuardarEntradaProductoSinDetalle(entradaProducto,
                                (int)TipoFolio.EntradaProducto);
                        resultado = entradaProducto.Folio;
                    }

                    transaction.Complete();
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

    }
}
