using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapMezcladoraDAL
    {
        /// <summary>
        /// Obtener mezcladora info
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static MezcladoraInfo ObtenerMezcladora(DataSet ds)
        {
            MezcladoraInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new MezcladoraInfo
                         {
                             mezcladoraID = info.Field<int>("mezcladoraID"),
                             NumeroEconomico = info.Field<string>("NumeroEconomico"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum()
                         }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }
        /// <summary>
        /// Listado de mezclado por pagina
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<MezcladoraInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<MezcladoraInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<MezcladoraInfo> lista = (from info in dt.AsEnumerable()
                                          select new MezcladoraInfo
                                          {
                                              mezcladoraID = info.Field<int>("mezcladoraID"),
                                              Descripcion = info.Field<string>("Descripcion"),
                                              NumeroEconomico = info.Field<string>("NumeroEconomico"),
                                              Activo = info.Field<bool>("Activo").BoolAEnum()
                                          }).ToList();

                resultado = new ResultadoInfo<MezcladoraInfo>
                {
                    Lista = lista ?? new List<MezcladoraInfo>(),
                    TotalRegistros =
                        Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        public static List<CalidadMezcladoFactorInfo> ObtenerCalidadMezcladoFactor(DataSet ds)
        {

            List<CalidadMezcladoFactorInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                lista = (from info in dt.AsEnumerable()
                    select new CalidadMezcladoFactorInfo
                    {
                        TipoMuestraID = info.Field<int>("TipoMuestraID"),
                        Muestra = info.Field<string>("Descripcion"),
                        Factor = info.Field<decimal>("Factor"),
                        PesoBH = info.Field<int>("PesoBaseHumeda"),
                        PesoBS = info.Field<int>("PesoBaseSeca"),
                    }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

        public static CalidadMezcladoFormulasAlimentoInfo ObtenerCalidadMezcladoFormulaAlimento(DataSet ds)
        {
            CalidadMezcladoFormulasAlimentoInfo resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select new CalidadMezcladoFormulasAlimentoInfo
                         {
                             CalidadMezcladoID = info.Field<int>("CalidadMezcladoID"),
                             Organizacion = new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionId"),
                                 Descripcion = info.Field<string>("Descripcion")
                             },
                             TipoTecnicaID = info.Field<int>("TipoTecnicaID"),
                             Fecha = info.Field<DateTime>("Fecha"),
                             UsuarioLaboratotorista = new UsuarioInfo
                             {
                                 UsuarioID = info.Field<int>("UsuarioIDLaboratorio")
                             },
                             Formula = new FormulaInfo
                             {
                                 FormulaId = info.Field<int>("FormulaID"),
                             },
                             FechaPremezcla = info.Field<DateTime>("FechaPremezcla"),
                             FechaBatch = info.Field<DateTime>("FechaBatch"),
                             LugarToma = info.Field<int>("TipoLugarMuestraID"),
                             CamionReparto = new CamionRepartoInfo
                             {
                                 CamionRepartoID = info.Field<int>("CamionRepartoID"),
                             },
                             Chofer = new ChoferInfo
                             {
                                 ChoferID = info.Field<int>("ChoferID"),
                             },
                             Mezcladora = new MezcladoraInfo
                             {
                                 mezcladoraID = info.Field<int>("MezcladoraID"),
                             },
                             Operador = new OperadorInfo
                             {
                                 OperadorID = info.Field<int>("OperadorIDMezclador"),
                             },
                             LoteID = info.Field<int>("LoteID"),
                             Batch = info.Field<int>("Batch"),
                             TiempoMezclado = info.Field<int>("TiempoMezclado"),
                             PersonaMuestreo = new OperadorInfo
                             {
                                 OperadorID = info.Field<int>("OperadorIDAnalista"),
                             },
                             GramosMicrot = info.Field<int>("GramosMicrotoxina"),
                             
                         }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }

        public static CalidadMezcladoFormulasAlimentoInfo ObtenerCalidadMezcladoFormula(DataSet ds)
        {
            CalidadMezcladoFormulasAlimentoInfo resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select new CalidadMezcladoFormulasAlimentoInfo
                             {
                                 CalidadMezcladoID = info.Field<int>("CalidadMezcladoID"),
                                 Organizacion = new OrganizacionInfo
                                 {
                                     OrganizacionID = info.Field<int>("OrganizacionId"),
                                 },
                                 /*TipoTecnicaID = info.Field<int>("TipoTecnicaID"),
                                 Fecha = info.Field<DateTime>("Fecha"),*/
                                 UsuarioLaboratotorista = new UsuarioInfo
                                 {
                                     UsuarioID = info.Field<int>("UsuarioIDLaboratorio")
                                 },
                                /* Formula = new FormulaInfo
                                 {
                                     FormulaId = info.Field<int>("FormulaID"),
                                 },
                                 FechaPremezcla = info.Field<DateTime>("FechaPremezcla"),
                                 FechaBatch = info.Field<DateTime>("FechaBatch"),
                                 LugarToma = info.Field<int>("TipoLugarMuestraID"),
                                 CamionReparto = new CamionRepartoInfo
                                 {
                                     CamionRepartoID = info.Field<int>("CamionRepartoID"),
                                 },
                                 Chofer = new ChoferInfo
                                 {
                                     ChoferID = info.Field<int>("ChoferID"),
                                 },
                                 Mezcladora = new MezcladoraInfo
                                 {
                                     mezcladoraID = info.Field<int>("MezcladoraID"),
                                 },
                                 Operador = new OperadorInfo
                                 {
                                     OperadorID = info.Field<int>("OperadorIDMezclador"),
                                 },
                                 LoteID = info.Field<int>("LoteID"),
                                 Batch = info.Field<int>("Batch"),
                                 TiempoMezclado = info.Field<int>("TiempoMezclado"),
                                 PersonaMuestreo = new OperadorInfo
                                 {
                                     OperadorID = info.Field<int>("OperadorIDAnalista"),
                                 },
                                 GramosMicrot = info.Field<int>("GramosMicrotoxina"),*/

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
