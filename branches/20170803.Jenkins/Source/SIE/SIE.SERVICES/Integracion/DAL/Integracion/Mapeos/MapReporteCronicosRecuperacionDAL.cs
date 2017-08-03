﻿using System;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Reportes;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapReporteCronicosRecuperacionDAL
    {
        /// <summary>
        /// Obtiene los datos del reporte
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ReporteCronicosRecuperacionDatos ObtenerDatosReporte(DataSet ds)
        {
            ReporteCronicosRecuperacionDatos resultado;
            try
            {
                Logger.Info();
                DataTable dtMovimientosEnfermeria = ds.Tables[ConstantesDAL.DtMovimientosEnfermeria];
                DataTable dtMovimientosProduccion = ds.Tables[ConstantesDAL.DtMovimientosProduccion];
                DataTable dtMovimientosTratamientos = ds.Tables[ConstantesDAL.DtTratamientos];
                DataTable dtFolios = ds.Tables[ConstantesDAL.DtFolios];

                resultado = new ReporteCronicosRecuperacionDatos
                {
                    MovimientosEnfermeria = (from movs in dtMovimientosEnfermeria.AsEnumerable()
                                             select new ReporteCronicosRecuperacionEnfermeria
                                             {
                                                 AnimalID = movs.Field<int>("AnimalID"),
                                                 AnimalMovimientoID = movs.Field<long>("AnimalMovimientoID"),
                                                 Arete = movs.Field<string>("Arete"),
                                                 Causa = movs.Field<string>("Causa"),
                                                 CorralEnfermeria = movs.Field<string>("CorralEnfermeria"),
                                                 Detector = movs.Field<string>("Detector"),
                                                 Enfermeria = movs.Field<string>("Enfermeria"),
                                                 Fecha = movs.Field<DateTime>("Fecha"),
                                                 FechaAlta = movs.Field<DateTime?>("FechaAlta"),
                                                 Sexo = Convert.ToChar(movs.Field<string>("Sexo"))
                                             }).ToList(),
                    MovimientosProduccion = (from movs in dtMovimientosProduccion.AsEnumerable()
                                             select new ReporteCronicosRecuperacionProduccion
                                             {
                                                 AnimalID = movs.Field<int>("AnimalID"),
                                                 AnimalMovimientoID = movs.Field<long>("AnimalMovimientoID"),
                                                 CorralProduccion = movs.Field<string>("CorralProduccion"),
                                                 Fecha = movs.Field<DateTime>("Fecha")
                                             }).ToList(),
                    Folios = (from movs in dtFolios.AsEnumerable()
                              select new ReporteCronicosRecuperacionFolio
                              {
                                  AnimalID = movs.Field<long>("AnimalID"),
                                  DiasEngorda = movs.Field<int>("DiasEngorda"),
                                  FolioEntrada = movs.Field<long>("FolioEntrada"),
                                  Organizacion = movs.Field<string>("Organizacion"),
                                  OrganizacionID = movs.Field<int>("OrganizacionID"),
                                  FechaLlegada = movs.Field<DateTime>("FechaLlegada")
                              }).ToList(),
                    Tratamientos = (from movs in dtMovimientosTratamientos.AsEnumerable()
                                    select new ReporteCronicosRecuperacionTratamiento
                                    {
                                        AnimalID = movs.Field<long>("AnimalID"),
                                        AnimalMovimientoID = movs.Field<long>("AnimalMovimientoID"),
                                        CodigoTratamiento = movs.Field<int>("CodigoTratamiento"),
                                        FechaMovimiento = movs.Field<DateTime>("FechaMovimiento"),
                                        Producto = movs.Field<string>("Producto"),
                                    }).ToList()
                };
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
