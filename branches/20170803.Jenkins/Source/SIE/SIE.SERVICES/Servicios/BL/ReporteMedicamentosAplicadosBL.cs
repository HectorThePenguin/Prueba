using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class ReporteMedicamentosAplicadosBL
    {
        /// <summary>
        /// Obtiene los datos al reporte de medicamentos aplicados
        /// </summary>
        /// <param name="encabezado"></param>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <param name="tipoTratamiento"></param>
        /// <returns></returns>
        internal List<ReporteMedicamentosAplicadosModel> ObtenerReporteMedicamentosAplicados(ReporteEncabezadoInfo encabezado, int organizacionID, DateTime fechaInicial, DateTime fechaFinal, int tipoTratamiento)
        {
            List<ReporteMedicamentosAplicadosModel> lista;
             try
             {
                 Logger.Info();
                 var reporteMedicamentosAplicadosDAL = new ReporteMedicamentosAplicadosDAL();
                 List<ReporteMedicamentosAplicadosDatos> datosReporte =
                     reporteMedicamentosAplicadosDAL.ObtenerParametrosDatosMedicamentosAplicados(organizacionID,
                                                                                                 fechaInicial,
                                                                                                 fechaFinal);
                 if(datosReporte == null)
                 {
                     return null;
                 }
                 lista = tipoTratamiento == 0 ? GenerarDatosReporteGeneral(datosReporte, encabezado) : GenerarDatosReporte(datosReporte, tipoTratamiento, encabezado);

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
             return lista;
        }

        /// <summary>
        /// Obtiene los datos del reporte de manera general
        /// </summary>
        /// <param name="datosReporte"></param>
        /// <param name="encabezado"></param>
        /// <returns></returns>
        private List<ReporteMedicamentosAplicadosModel> GenerarDatosReporteGeneral(IEnumerable<ReporteMedicamentosAplicadosDatos> datosReporte, ReporteEncabezadoInfo encabezado)
        {
            var lista = new List<ReporteMedicamentosAplicadosModel>();

            var productos = (from prod in datosReporte
                             group prod by prod.ProductoID into prodAgru
                             let reporteMedicamentosAplicadosDatos = prodAgru.FirstOrDefault()
                             where reporteMedicamentosAplicadosDatos != null
                             let conteoCabezas = prodAgru.Count()
                             let cantidadProducto = prodAgru.Sum(pro => pro.Cantidad)
                             let importe = ObtenerImporte(prodAgru, cantidadProducto)
                             select new ReporteMedicamentosAplicadosModel
                             {
                                 TipoTratamiento = reporteMedicamentosAplicadosDatos.TipoTratamiento,
                                 Cabezas = conteoCabezas,
                                 Cantidad = cantidadProducto,
                                 Codigo = reporteMedicamentosAplicadosDatos.ProductoID,
                                 Descripcion = reporteMedicamentosAplicadosDatos.Producto,
                                 Precio = reporteMedicamentosAplicadosDatos.Precio,
                                 Importe = importe,
                                 Unidad = reporteMedicamentosAplicadosDatos.Unidad,
                                 UnidadCabeza = Math.Round(cantidadProducto / conteoCabezas, 2),
                                 ImporteCabeza = Math.Round(importe / conteoCabezas, 2),
                                 Contar = prodAgru.Sum(registro => registro.Contar)
                             }).ToList();
            lista.AddRange(productos);

            

            //lista.Add(totalTratamiento);

            return lista;
        }
        /// <summary>
        /// Genera los datos del reporte por tipo de tratamiento
        /// </summary>
        /// <param name="datosReporte"></param>
        /// <param name="tipoTratamiento"></param>
        /// <param name="encabezado"></param>
        /// <returns></returns>
        private List<ReporteMedicamentosAplicadosModel> GenerarDatosReporte(IEnumerable<ReporteMedicamentosAplicadosDatos> datosReporte, int tipoTratamiento, ReporteEncabezadoInfo encabezado)
        {
            var lista = new List<ReporteMedicamentosAplicadosModel>();

            var agrupadoTratamientos = (from medi in datosReporte
                                        where medi.TipoTratamientoID == tipoTratamiento
                                        group medi by medi.TipoTratamiento
                                        into mediAgru
                                        select mediAgru);
            foreach (var agrupadoTratamiento in agrupadoTratamientos)
            {
                IGrouping<string, ReporteMedicamentosAplicadosDatos> tratamiento = agrupadoTratamiento;
                var productos = (from prod in agrupadoTratamiento
                                 group prod by prod.ProductoID into prodAgru 
                                 let reporteMedicamentosAplicadosDatos = prodAgru.FirstOrDefault() 
                                 where reporteMedicamentosAplicadosDatos != null
                                 let conteoCabezas = prodAgru.Count()
                                 let cantidadProducto = prodAgru.Sum(pro => pro.Cantidad)
                                 let importe = ObtenerImporte(prodAgru,cantidadProducto)
                                 select new ReporteMedicamentosAplicadosModel
                                     {
                                         TipoTratamiento = tratamiento.Key,
                                         Cabezas = conteoCabezas,
                                         Cantidad = cantidadProducto,
                                         Codigo = reporteMedicamentosAplicadosDatos.ProductoID,
                                         Descripcion = reporteMedicamentosAplicadosDatos.Producto,
                                         Precio = reporteMedicamentosAplicadosDatos.Precio,
                                         Importe = importe,
                                         Unidad = reporteMedicamentosAplicadosDatos.Unidad,
                                         UnidadCabeza = Math.Round(cantidadProducto / conteoCabezas,2),
                                         ImporteCabeza = Math.Round(importe / conteoCabezas,2),
                                         Contar = prodAgru.Sum(registro => registro.Contar )
                                     }).ToList();
                lista.AddRange(productos);

                var totalTratamiento = new ReporteMedicamentosAplicadosModel
                    {
                        TipoTratamiento = ConstantesBL.Total,
                        Cabezas = productos.Sum(totales => totales.Cabezas),
                        Cantidad = productos.Sum(total=> total.Cantidad),
                        Importe = productos.Sum(totales => totales.Importe)
                    };

                //lista.Add(totalTratamiento);
            }
            
            return lista;
        }

        /// <summary>
        /// Obtiene el importe de los productos
        /// </summary>
        /// <param name="productos"></param>
        /// <param name="cantidad"></param>
        /// <returns></returns>
        private decimal ObtenerImporte(IEnumerable<ReporteMedicamentosAplicadosDatos> productos, decimal cantidad)
        {
            var producto = productos.FirstOrDefault();
            if(producto == null)
            {
                return 0;
            }
            decimal importe = Math.Round(producto.Precio * cantidad,2);
            return importe;
        }

        /// <summary>
        /// Obtiene la informacion del reporte de sanidad
        /// </summary>
        /// <param name="encabezado"></param>
        /// <param name="organizacionId"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <param name="tipoTratamiento"></param>
        /// <returns></returns>
        public List<ReporteMedicamentosAplicadosModel> ObtenerReporteMedicamentosAplicadosSanidad(ReporteEncabezadoInfo encabezado, int organizacionId, int almacenId, DateTime fechaInicial, DateTime fechaFinal, int tipoTratamiento)
        {
            List<ReporteMedicamentosAplicadosModel> lista;
            try
            {
                Logger.Info();
                var reporteMedicamentosAplicadosDAL = new ReporteMedicamentosAplicadosDAL();
                List<ReporteMedicamentosAplicadosDatos> datosReporte =
                    reporteMedicamentosAplicadosDAL.ObtenerParametrosDatosMedicamentosAplicadosSanidad(organizacionId,
                                                                                                almacenId,
                                                                                                fechaInicial,
                                                                                                fechaFinal);
                if (datosReporte == null)
                {
                    return null;
                }
                lista = tipoTratamiento == 0 ? GenerarDatosReporteGeneral(datosReporte, encabezado) : GenerarDatosReporte(datosReporte, tipoTratamiento, encabezado);

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
            return lista;
        }
        /// <summary>
        /// Obtiene el dataset que sera enviado al reporte (crystal report) y generar el reporte de Medicamentis Aplicados
        /// </summary>
        /// <param name="encabezado"></param>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <param name="tipoTratamiento"></param>
        /// <returns></returns>
        internal List<ReporteMedicamentosAplicadosModel> ObtenerReporteMedicamentoCabezasAplicados(ReporteEncabezadoInfo encabezado, int organizacionID, DateTime fechaInicial, DateTime fechaFinal, int tipoTratamiento)
        {
            List<ReporteMedicamentosAplicadosModel> lista;
            try
            {
                Logger.Info();
                var reporteMedicamentosAplicadosDAL = new ReporteMedicamentosAplicadosDAL();
                List<ReporteMedicamentosAplicadosDatos> datosReporte =
                    reporteMedicamentosAplicadosDAL.ObtenerParametrosDatosMedicamentosAplicados(organizacionID,
                                                                                                fechaInicial,
                                                                                                fechaFinal);
                if (datosReporte == null)
                {
                    return null;
                }
                lista = tipoTratamiento == 0 ? GenerarDatosReporteMedicamentosGeneral(datosReporte, encabezado) : GenerarDatosReporteMedicamentos(datosReporte, tipoTratamiento, encabezado);

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
            return lista;
        }
        /// <summary>
        /// Obtiene los datos del reporte Medicamentos Aplicados de manera general
        /// </summary>
        /// <param name="datosReporte"></param>
        /// <param name="encabezado"></param>
        /// <returns></returns>
        private List<ReporteMedicamentosAplicadosModel> GenerarDatosReporteMedicamentosGeneral(IEnumerable<ReporteMedicamentosAplicadosDatos> datosReporte, ReporteEncabezadoInfo encabezado)
        {
            var lista = new List<ReporteMedicamentosAplicadosModel>();


            var agrupadoTratamientos = (from medi in datosReporte
                                        group medi by medi.TipoTratamiento
                                            into mediAgru
                                            select mediAgru);
            foreach (var agrupadoTratamiento in agrupadoTratamientos)
            {
                IGrouping<string, ReporteMedicamentosAplicadosDatos> tratamiento = agrupadoTratamiento;
                var productos = (from prod in agrupadoTratamiento
                                 group prod by prod.ProductoID into prodAgru
                                 let reporteMedicamentosAplicadosDatos = prodAgru.FirstOrDefault()
                                 where reporteMedicamentosAplicadosDatos != null
                                 let conteoCabezas = prodAgru.Count()
                                 let cantidadProducto = prodAgru.Sum(pro => pro.Cantidad)
                                 let importe = ObtenerImporte(prodAgru, cantidadProducto)
                                 select new ReporteMedicamentosAplicadosModel
                                 {
                                     TipoTratamiento = tratamiento.Key,
                                     Cabezas = conteoCabezas,
                                     Cantidad = cantidadProducto,
                                     Codigo = reporteMedicamentosAplicadosDatos.ProductoID,
                                     Descripcion = reporteMedicamentosAplicadosDatos.Producto,
                                     Precio = reporteMedicamentosAplicadosDatos.Precio,
                                     Importe = importe,
                                     Unidad = reporteMedicamentosAplicadosDatos.Unidad,
                                     UnidadCabeza = Math.Round(cantidadProducto / conteoCabezas, 2),
                                     ImporteCabeza = Math.Round(importe / conteoCabezas, 2),
                                     AnimalID = reporteMedicamentosAplicadosDatos.AnimalID
                                 }).ToList();
                lista.AddRange(productos);
            }
            return lista;
        }
        /// <summary>
        /// Genera los datos del reporte Medicamentos Aplicados por tipo de tratamiento
        /// </summary>
        /// <param name="datosReporte"></param>
        /// <param name="tipoTratamiento"></param>
        /// <param name="encabezado"></param>
        /// <returns></returns>
        private List<ReporteMedicamentosAplicadosModel> GenerarDatosReporteMedicamentos(IEnumerable<ReporteMedicamentosAplicadosDatos> datosReporte, int tipoTratamiento, ReporteEncabezadoInfo encabezado)
        {
            var lista = new List<ReporteMedicamentosAplicadosModel>();

            var agrupadoTratamientos = (from medi in datosReporte
                                        where medi.TipoTratamientoID == tipoTratamiento
                                        group medi by medi.TipoTratamiento
                                            into mediAgru
                                            select mediAgru);
            foreach (var agrupadoTratamiento in agrupadoTratamientos)
            {
                IGrouping<string, ReporteMedicamentosAplicadosDatos> tratamiento = agrupadoTratamiento;
                var productos = (from prod in agrupadoTratamiento
                                 group prod by prod.ProductoID into prodAgru
                                 let reporteMedicamentosAplicadosDatos = prodAgru.FirstOrDefault()
                                 where reporteMedicamentosAplicadosDatos != null
                                 let conteoCabezas = prodAgru.Count()
                                 let cantidadProducto = prodAgru.Sum(pro => pro.Cantidad)
                                 let importe = ObtenerImporte(prodAgru, cantidadProducto)
                                 select new ReporteMedicamentosAplicadosModel
                                 {
                                     TipoTratamiento = tratamiento.Key,
                                     Cabezas = conteoCabezas,
                                     Cantidad = cantidadProducto,
                                     Codigo = reporteMedicamentosAplicadosDatos.ProductoID,
                                     Descripcion = reporteMedicamentosAplicadosDatos.Producto,
                                     Precio = reporteMedicamentosAplicadosDatos.Precio,
                                     Importe = importe,
                                     Unidad = reporteMedicamentosAplicadosDatos.Unidad,
                                     UnidadCabeza = Math.Round(cantidadProducto / conteoCabezas, 2),
                                     ImporteCabeza = Math.Round(importe / conteoCabezas, 2),
                                     AnimalID = reporteMedicamentosAplicadosDatos.AnimalID
                                 }).ToList();
                lista.AddRange(productos);

                var totalTratamiento = new ReporteMedicamentosAplicadosModel
                {
                    TipoTratamiento = ConstantesBL.Total,
                    Cabezas = productos.Sum(totales => totales.Cabezas),
                    Cantidad = productos.Sum(total => total.Cantidad),
                    Importe = productos.Sum(totales => totales.Importe)
                };
            }
            return lista;
        }
    }
}
