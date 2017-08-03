using System;
using System.Linq;
using System.Data;
using System.Reflection;
using System.Collections.Generic;
using SIE.Base.Log;
using SIE.Base.Exepciones;
using SIE.Services.Info.Info;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Modelos;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    class MapCalidadMezcladoFormulasAlimentoDAL
    {
        /// <summary>
        /// Mapea el resultado de la consulta para cargar el combobox "Analisis de muestras"
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<CalidadMezcladoFormulasAlimentoInfo> CargarComboboxAnalisis(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CalidadMezcladoFormulasAlimentoInfo> cMFA =
                       (from campo in dt.AsEnumerable()
                        select 
                            new CalidadMezcladoFormulasAlimentoInfo
                               {
                                   AnalisisMuestraCombobox = campo.Field<string>("Descripcion")
                               }).ToList();
                return cMFA;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Mapea el resultado de la consulta para cargar el combobox "Tecnica"
        /// </summary>
        internal static IList<CalidadMezcladoFormulasAlimentoInfo> CargarComboboxTecnica(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CalidadMezcladoFormulasAlimentoInfo> cMFA =
                       (from campo in dt.AsEnumerable()
                        select
                            new CalidadMezcladoFormulasAlimentoInfo
                            {
                                
                                Tecnica = campo.Field<string>("Descripcion"),
                                TipoTecnicaID = campo.Field<int>("TipoTecnicaID")
                                
                            }).ToList();
                return cMFA;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Mapea el resultado de la consulta para obtnere los datos de la tabla CalidadMezcladoFactor
        /// </summary>
        internal static IList<CalidadMezcladoFactorInfo> ObtenerTablaFactor(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                IList<CalidadMezcladoFactorInfo> datosTabla = (from campos in dt.AsEnumerable()
                                                        select new CalidadMezcladoFactorInfo
                                                        {
                                                            Muestra = campos.Field<string>("Descripcion"),
                                                            Factor = campos.Field<decimal>("Factor"),
                                                            PesoBH = campos.Field<int>("PesoBaseHumeda"),
                                                            PesoBS = campos.Field<int>("PesoBaseSeca"),
                                                        }).ToList();
                return datosTabla;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Mapea el resultado obtener los datos que se ocupan para llenar la tabla resumen del formulario 
        /// "Calidad Mezclado Alimentos"
        /// </summary>
        internal static IList<CalidadMezcladoFormula_ResumenInfo> TraerDatosTablaResumen(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                IList<CalidadMezcladoFormula_ResumenInfo> datosTabla = (from campos in dt.AsEnumerable()
                                                                       select new CalidadMezcladoFormula_ResumenInfo
                                                               {
                                                                   TipoAnalisis = campos.Field<string>("TipoAnalisis"),
                                                                   Peso = campos.Field<int>("Peso"),
                                                                   PesoBH = campos.Field<int>("PesoBH"),
                                                                   PesoBS = campos.Field<int>("PesoBS"),
                                                                   Factor = campos.Field<Decimal>("Factor"),
                                                                   PromParticulasEsperadas = campos.Field<int>("PromParticulasEsperadas"),
                                                                   TipoMuestraID = campos.Field<int>("TipoMuestraID")
                                                               }).ToList();
                return datosTabla;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Metodo que se utiliza para traer de la tabla CalidadMezcladoDetalle, los registros que se ocupan para cargar en el grid "Analisis de muestras
        /// Inicial-Media-Final" cuando hay datos cargados en el mismo dia
        /// </summary>
        /// <returns></returns>
        internal static IList<CalidadMezcladoFormulasAlimentoInfo> CargarTablaMezcladoDetalle(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                IList<CalidadMezcladoFormulasAlimentoInfo> datosTabla = (from campos in dt.AsEnumerable()
                                                                         select new CalidadMezcladoFormulasAlimentoInfo
                                                                        {
                                                                            AnalisisMuestras = campos.Field<string>("TipoAnalisis"),
                                                                            NumeroMuestras = campos.Field<string>("NumeroMuestra"),
                                                                            PesoGramos = campos.Field<int>("Peso"),
                                                                            ParticulasEncontradas = campos.Field<decimal>("Particulas")
                                                                        }).ToList();
                return datosTabla;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Cargar los datos consultados en BD
        /// </summary>
        /// <returns></returns>
        internal static IList<ImpresionCalidadMezcladoModel> ObtenerImpresionCalidadMezclado(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];
                IList<ImpresionCalidadMezcladoModel> datosTabla = (from info in dt.AsEnumerable()
                                                                   select new ImpresionCalidadMezcladoModel
                                                                         {
                                                                             CalidadMezcladoID = info.Field<int>("CalidadMezcladoID"),
                                                                             Tecnica = info.Field<string>("Tecnica"),
                                                                             Organizacion = info.Field<string>("Organizacion"),
                                                                             Formula = info.Field<string>("Formula"),
                                                                             FechaPremezcla = info.Field<DateTime>("FechaPremezcla"),
                                                                             FechaBatch = info.Field<DateTime>("FechaBatch"),
                                                                             Corral = info.Field<string>("Corral"),
                                                                             TipoLugarMuestra = info.Field<string>("TipoLugarMuestra"),
                                                                             Chofer = info.Field<string>("Chofer"),
                                                                             EncargadoMezcladora = info.Field<string>("EncargadoMezcladora"),
                                                                             Batch = info.Field<int>("Batch"),
                                                                             TiempoMezclado = info.Field<int>("TiempoMezclado"),
                                                                             EncargadoMuestreo = info.Field<string>("EncargadoMuestreo"),
                                                                             GramosMicrotoxina = info.Field<int>("GramosMicrotoxina"),
                                                                             Detalle = (from det in dtDetalle.AsEnumerable()
                                                                                        where info.Field<int>("CalidadMezcladoID") == det.Field<int>("CalidadMezcladoID")
                                                                                        select new ImpresionCalidadMezcladoDetalleModel
                                                                                            {
                                                                                                CalidadMezcladoID = info.Field<int>("CalidadMezcladoID"),
                                                                                                TipoMuestra = det.Field<string>("TipoMuestra"),
                                                                                                ParticulasEsperadas = Convert.ToDouble(det.Field<decimal>("ParticulasEsperadas")),
                                                                                                PesoBaseHumeda = det.Field<int>("PesoBaseHumeda"),
                                                                                                PesoBaseSeca = det.Field<int>("PesoBaseSeca"),
                                                                                                NumeroMuestra = det.Field<string>("NumeroMuestra"),
                                                                                                Peso = det.Field<int>("Peso"),
                                                                                                Particulas = Convert.ToDouble(det.Field<decimal>("Particulas"))
                                                                                            }).ToList()
                                                                         }).ToList();
                return datosTabla;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

    }
}
