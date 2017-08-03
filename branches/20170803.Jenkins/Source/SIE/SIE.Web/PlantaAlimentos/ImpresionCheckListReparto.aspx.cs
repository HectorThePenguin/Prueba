using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Web.PlantaAlimentos
{
    public partial class ImpresionCheckListReparto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var operadorID = Request.QueryString["OperadorID"];
                var fecha = Request.QueryString["Fecha"];
                var camionRepartoID = Request.QueryString["CamionRepartoID"];

                var filtro = new FiltroCheckListReparto
                    {
                        OperadorID = Convert.ToInt32(operadorID),
                        Fecha = DateTime.Parse(fecha),
                        CamionRepartoID = Convert.ToInt32(camionRepartoID)
                    };
                var repartoAlimentoBL = new RepartoAlimentoBL();
                List<RepartoAlimentoInfo> reporte = repartoAlimentoBL.ImprimirRepartos(filtro);

                if (reporte == null)
                {
                    Session["ErrorCheckListReparto"] = "No hay información para los datos ingresados.";
                    return;
                }

                RepartoAlimentoInfo primerReparto =
               reporte.FirstOrDefault(rep => rep.TipoServicioID == TipoServicioEnum.Matutino.GetHashCode());
                RepartoAlimentoInfo segundoReparto =
                    reporte.FirstOrDefault(rep => rep.TipoServicioID == TipoServicioEnum.Vespertino.GetHashCode());

                if (primerReparto == null || segundoReparto == null)
                {
                    Session["ErrorCheckListReparto"] = "No se han finalizado los registros de los dos servicios de reparto.";
                    return;
                }

                GenerarReporte(reporte);
                
            }
            catch (Exception ex)
            {
                Session["ErrorCheckListReparto"] = "Ocurrio un error al imprimir el CheckList de Reparto.";
            }
        }

        private void GenerarReporte(List<RepartoAlimentoInfo> reporte)
        {
            var ds = new DataSet();

            RepartoAlimentoInfo primerReparto =
               reporte.FirstOrDefault(rep => rep.TipoServicioID == TipoServicioEnum.Matutino.GetHashCode());
            RepartoAlimentoInfo segundoReparto =
                reporte.FirstOrDefault(rep => rep.TipoServicioID == TipoServicioEnum.Vespertino.GetHashCode());

            if (primerReparto == null || segundoReparto == null)
            {
                Session["ErrorCheckListReparto"] = "No se han finalizado los registros de los dos servicios de reparto.";
                return;
            }

            ArmarTablaCabecero(ds, primerReparto, segundoReparto);
            ArmarTablaPrimerReparto(ds, primerReparto);
            ArmarTablaPrimerTiempoMuerto(ds, primerReparto);
            ArmarTablaSegundoReparto(ds, segundoReparto);
            ArmarTablaSegundoTiempoMuerto(ds, segundoReparto);


            //ds.WriteXml(@"c:\Reporte\CheckListReparto.xml", XmlWriteMode.WriteSchema);
            
            //var crystalReport = new ReportDocument(); // creating object of crystal report
            //crystalReport.Load(Server.MapPath("~/Reportes/CheckListReparto.rpt")); // path of report 
            //crystalReport.SetDataSource(ds); // binding datatable
            //crv.ReportSource = crystalReport;

            //crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, string.Format("{0}{1}", "CheckListReparto", primerReparto.FechaReparto));
            //crystalReport.Dispose();

            //crystalReport.ExportToHttpResponse
            //(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "PersonDetails");

            var rptdoc = new ReportDocument(); // creating object of crystal report
            rptdoc.Load(Server.MapPath("~/Reportes/CheckListReparto.rpt")); // path of report 
            rptdoc.SetDataSource(ds);
            //var archivo = crystalReport.ExportToStream(ExportFormatType.PortableDocFormat);

            crv.ReportSource = rptdoc;
            crv.DataBind();


            //rptdoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            //rptdoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            //rptdoc.ExportOptions.DestinationOptions = new DiskFileDestinationOptions();
            //((DiskFileDestinationOptions)rptdoc.ExportOptions.DestinationOptions).DiskFileName = Server.MapPath("~/Reportes/CheckListReparto.pdf");
            //rptdoc.Export();
            //rptdoc.Close();
            //rptdoc.Dispose();
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.ContentType = "application/pdf";
            //Response.AppendHeader("Content-Disposition", "attachment; filename=CheckListReparto.pdf");
            //Response.WriteFile(Server.MapPath("~/Reportes/CheckListReparto.pdf"));
            //Response.Flush();
            //Response.Close();
            //System.IO.File.Delete(Server.MapPath("~/Reportes/CheckListReparto.pdf"));
            var arreglo = rptdoc.ExportToStream(ExportFormatType.PortableDocFormat);

            byte[] arregloBytes = new byte[arreglo.Length];

            arreglo.Read(arregloBytes, 0, Convert.ToInt32(arreglo.Length));
            arreglo.Close();

            //arreglo.Length
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=CheckListReparto.pdf");
            Response.BinaryWrite(arregloBytes);

            Session["ErrorCheckListReparto"] = "0";
        }

        private void ArmarTablaCabecero(DataSet ds, RepartoAlimentoInfo primerReparto, RepartoAlimentoInfo segundoReparto)
        {
            var tablaCabecero = new DataTable { TableName = "Cabecero" };

            tablaCabecero.Columns.Add("CamionReparto", typeof(string));
            tablaCabecero.Columns.Add("Fecha", typeof(DateTime));
            tablaCabecero.Columns.Add("Operador", typeof(string));
            tablaCabecero.Columns.Add("HorometroInicial", typeof(int));
            tablaCabecero.Columns.Add("HorometroFinal", typeof(int));
            tablaCabecero.Columns.Add("OdometroInicial", typeof(int));
            tablaCabecero.Columns.Add("OdometroFinal", typeof(int));
            tablaCabecero.Columns.Add("LitrosDiesel", typeof(int));
            tablaCabecero.Columns.Add("Observaciones", typeof(string));
            tablaCabecero.Columns.Add("TotalKilosEmbarcadosPrimero", typeof(int));
            tablaCabecero.Columns.Add("TotalKilosRepartidosPrimero", typeof(int));
            tablaCabecero.Columns.Add("TotalSobrantePrimero", typeof(int));
            tablaCabecero.Columns.Add("TotalTiempoViajePrimero", typeof(string));
            tablaCabecero.Columns.Add("MermaRepartoPrimero", typeof(int));
            tablaCabecero.Columns.Add("TotalKilosEmbarcadosSegundo", typeof(int));
            tablaCabecero.Columns.Add("TotalKilosRepartidosSegundo", typeof(int));
            tablaCabecero.Columns.Add("TotalSobranteSegundo", typeof(int));
            tablaCabecero.Columns.Add("TotalTiempoViajeSegundo", typeof(string));
            tablaCabecero.Columns.Add("MermaRepartoSegundo", typeof(int));


            if (primerReparto == null || segundoReparto == null)
            {
                return;
            }

            DataRow renglonCabecero = tablaCabecero.NewRow();

            renglonCabecero["CamionReparto"] = primerReparto.CamionReparto.NumeroEconomico;
            renglonCabecero["Fecha"] = primerReparto.FechaReparto;
            renglonCabecero["Operador"] = primerReparto.Usuario.Nombre;
            renglonCabecero["HorometroInicial"] = primerReparto.HorometroInicial;
            renglonCabecero["HorometroFinal"] = segundoReparto.HorometroFinal;
            renglonCabecero["OdometroInicial"] = primerReparto.OdometroInicial;
            renglonCabecero["OdometroFinal"] = segundoReparto.OdometroFinal;
            renglonCabecero["LitrosDiesel"] = primerReparto.LitrosDiesel + segundoReparto.LitrosDiesel;
            renglonCabecero["TotalKilosEmbarcadosPrimero"] = primerReparto.TotalKilosEmbarcados;
            renglonCabecero["TotalKilosRepartidosPrimero"] = primerReparto.TotalKilosRepartidos;
            renglonCabecero["TotalSobrantePrimero"] = primerReparto.TotalSobrante;
            renglonCabecero["TotalTiempoViajePrimero"] = primerReparto.TotalTiempoViaje;
            renglonCabecero["MermaRepartoPrimero"] = primerReparto.MermaReparto;
            renglonCabecero["TotalKilosEmbarcadosSegundo"] = segundoReparto.TotalKilosEmbarcados;
            renglonCabecero["TotalKilosRepartidosSegundo"] = segundoReparto.TotalKilosRepartidos;
            renglonCabecero["TotalSobranteSegundo"] = segundoReparto.TotalSobrante;
            renglonCabecero["TotalTiempoViajeSegundo"] = segundoReparto.TotalTiempoViaje;
            renglonCabecero["MermaRepartoSegundo"] = segundoReparto.MermaReparto;
            renglonCabecero["Observaciones"] = string.Format("{0}; {1}", primerReparto.Observaciones,
                                                             segundoReparto.Observaciones);

            tablaCabecero.Rows.Add(renglonCabecero);

            ds.Tables.Add(tablaCabecero);
        }

        private void ArmarTablaPrimerReparto(DataSet ds, RepartoAlimentoInfo primerReparto)
        {
            var tablaPrimerReparto = new DataTable { TableName = "PrimerReparto" };
            tablaPrimerReparto.Columns.Add("Racion", typeof(int));
            tablaPrimerReparto.Columns.Add("Tolva", typeof(string));
            tablaPrimerReparto.Columns.Add("KilosEmbarcados", typeof(int));
            tablaPrimerReparto.Columns.Add("KilosRepartidos", typeof(int));
            tablaPrimerReparto.Columns.Add("Sobrante", typeof(int));
            tablaPrimerReparto.Columns.Add("CorralInicio", typeof(string));
            tablaPrimerReparto.Columns.Add("CorralFinal", typeof(string));
            tablaPrimerReparto.Columns.Add("HoraInicioReparto", typeof(string));
            tablaPrimerReparto.Columns.Add("HoraFinalReparto", typeof(string));
            tablaPrimerReparto.Columns.Add("TiempoTotalViaje", typeof(string));

            primerReparto.ListaGridRepartos.ToList().ForEach(detalle =>
                {
                    DataRow renglonPrimerReparto = tablaPrimerReparto.NewRow();
                    renglonPrimerReparto["Racion"] = detalle.RacionFormula;
                    renglonPrimerReparto["Tolva"] = detalle.NumeroTolva;
                    renglonPrimerReparto["KilosEmbarcados"] = detalle.KilosEmbarcados;
                    renglonPrimerReparto["KilosRepartidos"] = detalle.KilosRepartidos;
                    renglonPrimerReparto["Sobrante"] = detalle.Sobrante;
                    renglonPrimerReparto["CorralInicio"] = detalle.CorralInicio;
                    renglonPrimerReparto["CorralFinal"] = detalle.CorralFinal;
                    renglonPrimerReparto["HoraInicioReparto"] = detalle.HoraInicioReparto;
                    renglonPrimerReparto["HoraFinalReparto"] = detalle.HoraFinalReparto;
                    renglonPrimerReparto["TiempoTotalViaje"] = detalle.TiempoTotalViaje;

                    tablaPrimerReparto.Rows.Add(renglonPrimerReparto);
                });

            ds.Tables.Add(tablaPrimerReparto);
        }

        private void ArmarTablaPrimerTiempoMuerto(DataSet ds, RepartoAlimentoInfo primerReparto)
        {
            var tablaTiemposMuertosPrimerReparto = new DataTable { TableName = "TiemposMuertosPrimerReparto" };
            tablaTiemposMuertosPrimerReparto.Columns.Add("HoraInicial", typeof(string));
            tablaTiemposMuertosPrimerReparto.Columns.Add("HoraFinal", typeof(string));
            tablaTiemposMuertosPrimerReparto.Columns.Add("Causa", typeof(string));

            if (primerReparto.ListaTiempoMuerto != null && primerReparto.ListaTiempoMuerto.Any())
            {
                primerReparto.ListaTiempoMuerto.ForEach(tiempos =>
                    {
                        DataRow renglonTiempoMuerto = tablaTiemposMuertosPrimerReparto.NewRow();
                        renglonTiempoMuerto["HoraInicial"] = tiempos.HoraInicio;
                        renglonTiempoMuerto["HoraFinal"] = tiempos.HoraFin;
                        renglonTiempoMuerto["Causa"] = tiempos.CausaTiempoMuerto.Descripcion;

                        tablaTiemposMuertosPrimerReparto.Rows.Add(renglonTiempoMuerto);
                    });
            }

            ds.Tables.Add(tablaTiemposMuertosPrimerReparto);
        }

        private void ArmarTablaSegundoReparto(DataSet ds, RepartoAlimentoInfo segundoReparto)
        {
            var tablaSegundoReparto = new DataTable { TableName = "SegundoReparto" };
            tablaSegundoReparto.Columns.Add("Racion", typeof(int));
            tablaSegundoReparto.Columns.Add("Tolva", typeof(string));
            tablaSegundoReparto.Columns.Add("KilosEmbarcados", typeof(int));
            tablaSegundoReparto.Columns.Add("KilosRepartidos", typeof(int));
            tablaSegundoReparto.Columns.Add("Sobrante", typeof(int));
            tablaSegundoReparto.Columns.Add("CorralInicio", typeof(string));
            tablaSegundoReparto.Columns.Add("CorralFinal", typeof(string));
            tablaSegundoReparto.Columns.Add("HoraInicioReparto", typeof(string));
            tablaSegundoReparto.Columns.Add("HoraFinalReparto", typeof(string));
            tablaSegundoReparto.Columns.Add("TiempoTotalViaje", typeof(string));

            segundoReparto.ListaGridRepartos.ToList().ForEach(detalle =>
            {
                DataRow renglonPrimerReparto = tablaSegundoReparto.NewRow();
                renglonPrimerReparto["Racion"] = detalle.RacionFormula;
                renglonPrimerReparto["Tolva"] = detalle.NumeroTolva;
                renglonPrimerReparto["KilosEmbarcados"] = detalle.KilosEmbarcados;
                renglonPrimerReparto["KilosRepartidos"] = detalle.KilosRepartidos;
                renglonPrimerReparto["Sobrante"] = detalle.Sobrante;
                renglonPrimerReparto["CorralInicio"] = detalle.CorralInicio;
                renglonPrimerReparto["CorralFinal"] = detalle.CorralFinal;
                renglonPrimerReparto["HoraInicioReparto"] = detalle.HoraInicioReparto;
                renglonPrimerReparto["HoraFinalReparto"] = detalle.HoraFinalReparto;
                renglonPrimerReparto["TiempoTotalViaje"] = detalle.TiempoTotalViaje;

                tablaSegundoReparto.Rows.Add(renglonPrimerReparto);
            });

            ds.Tables.Add(tablaSegundoReparto);
        }

        private void ArmarTablaSegundoTiempoMuerto(DataSet ds, RepartoAlimentoInfo segundoReparto)
        {
            var tablaTiemposMuertosSegundoReparto = new DataTable { TableName = "TiemposMuertosSegundoReparto" };
            tablaTiemposMuertosSegundoReparto.Columns.Add("HoraInicial", typeof(string));
            tablaTiemposMuertosSegundoReparto.Columns.Add("HoraFinal", typeof(string));
            tablaTiemposMuertosSegundoReparto.Columns.Add("Causa", typeof(string));

            if (segundoReparto.ListaTiempoMuerto != null && segundoReparto.ListaTiempoMuerto.Any())
            {
                segundoReparto.ListaTiempoMuerto.ForEach(tiempos =>
                    {
                        DataRow renglonTiempoMuerto = tablaTiemposMuertosSegundoReparto.NewRow();
                        renglonTiempoMuerto["HoraInicial"] = tiempos.HoraInicio;
                        renglonTiempoMuerto["HoraFinal"] = tiempos.HoraFin;
                        renglonTiempoMuerto["Causa"] = tiempos.CausaTiempoMuerto.Descripcion;
                        tablaTiemposMuertosSegundoReparto.Rows.Add(renglonTiempoMuerto);
                    });
            }

            ds.Tables.Add(tablaTiemposMuertosSegundoReparto);
        }

    }
}