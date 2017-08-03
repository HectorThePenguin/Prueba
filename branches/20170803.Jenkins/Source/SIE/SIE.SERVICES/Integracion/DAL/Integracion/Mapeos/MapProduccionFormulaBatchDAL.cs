using System;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapProduccionFormulaBatchDAL
    {
        /// <summary>
        /// Obtiene un objeto al guardar un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ProduccionFormulaBatchInfo GuardarProduccionFormula(DataSet ds)
        {
            ProduccionFormulaBatchInfo resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select
                                 new ProduccionFormulaBatchInfo
                                 {
                                     ProduccionFormulaBatchID = info.Field<int>("ProduccionFormulaID"),
                                     ProduccionFormulaID = info.Field<int>("ProduccionFormulaID"),
                                     OrganizacionID = info.Field<int>("OrganizacionID"),
                                     ProductoID = info.Field<int>("ProductoID"),
                                     FormulaID = info.Field<int>("FormulaID"),
                                     RotomixID = info.Field<int>("RotoMix"),
                                     Batch = info.Field<int>("Batch"),
                                     CantidadProgramada = info.Field<int>("CantidadProgramada"),
                                     CantidadReal = info.Field<int>("CantidadReal")
                                 }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene un objeto al guardar un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ProduccionFormulaBatchInfo ValidarProduccionFormulaBatch(DataSet ds)
        {
            ProduccionFormulaBatchInfo resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select
                                 new ProduccionFormulaBatchInfo
                                 {
                                     ProduccionFormulaBatchID = info.Field<int>("ProduccionFormulaID"),
                                     ProduccionFormulaID = info.Field<int>("ProduccionFormulaID"),
                                     OrganizacionID = info.Field<int>("OrganizacionID"),
                                     ProductoID = info.Field<int>("ProductoID"),
                                     FormulaID = info.Field<int>("FormulaID"),
                                     RotomixID = info.Field<int>("RotoMixID"),
                                     Batch = info.Field<int>("Batch"),
                                     CantidadProgramada = info.Field<int>("CantidadProgramada"),
                                     CantidadReal = info.Field<int>("CantidadReal")
                                 }).First();
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
