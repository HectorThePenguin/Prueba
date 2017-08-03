using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Polizas;
using SIE.Services.Polizas.Fabrica;
using SIE.Services.Servicios.PL;
using SIE.Services.Servicios.BL;

namespace SIE.PolizaServicio
{
    public partial class PolizaServicio : ServiceBase
    {
        #region CONSTRUCTORES

        public PolizaServicio()
        {
            InitializeComponent();

            if (!EventLog.SourceExists("ServiciosPolizaSK"))
            {
                EventLog.CreateEventSource("ServiciosPolizaSK", "Application");
            }
            logger = new EventLog("Application", Environment.MachineName, "ServiciosPolizaSK");
        }

        #endregion CONSTRUCTORES

        #region VARIABLES LOCALES

        private static EventLog logger;
        private static bool enEjecucion;

        #endregion VARIABLES LOCALES


        #region METODOS PROTEGIDOS

        protected override void OnStart(string[] args)
        {
            logger.WriteEntry("INICIANDO SERVICIO DE POLIZAS", EventLogEntryType.Information);
            while (true)
            {
                var hilo = new Thread(HiloSecundario);
                var hiloMuertes = new Thread(GenerarPolizasMuerte);

                hilo.Start();
                hiloMuertes.Start();

                Thread.Sleep(300000);
            }
        }

        protected override void OnStop()
        {
            logger.WriteEntry("FINALIZANDO SERVICIO DE POLIZAS", EventLogEntryType.Information);
        }

        #endregion METODOS PROTEGIDOS

        #region METODOS PRIVADOS

        private static void GenerarPolizasMuerte()
        {
            if (!enEjecucion)
            {
                var thread = new Thread(delegate()
                {
                    try
                    {
                        enEjecucion = true;
                        var fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                        var animalHistoricoBL = new AnimalMovimientoHistoricoBL();
                        IEnumerable<AnimalMovimientoInfo> animalesMovimiento =
                            animalHistoricoBL.ObtenerMovimientosMuertes(fecha);
                        if (animalesMovimiento != null && animalesMovimiento.Any())
                        {
                            logger.WriteEntry("INICIA A LA EJECUCION DE LA MUERTE", EventLogEntryType.Information);

                            List<AnimalInfo> animales =
                                animalesMovimiento.Select(x => new AnimalInfo
                                                                   {
                                                                       AnimalID = x.AnimalID,
                                                                       OrganizacionIDEntrada
                                                                           =
                                                                           x.OrganizacionID
                                                                   }).ToList();
                            var animalCostoBL = new AnimalCostoBL();
                            List<AnimalCostoInfo> animalCosto =
                                animalCostoBL.ObtenerCostosAnimal(animales);
                            if (animalCosto != null && animalCosto.Any())
                            {
                                logger.WriteEntry(string.Format("PROCESARA {0} MUERTES", animalCosto.Count),
                                                  EventLogEntryType.Information);

                                List<long> animalesId =
                                    animalCosto.Where(costo => costo.CostoID == 1).Select(
                                        x => x.AnimalID).Distinct().ToList
                                        ();
                                int organizacionID =
                                    animalesMovimiento.ElementAt(0).OrganizacionID;
                                animalCosto.ForEach(
                                    org => org.OrganizacionID = organizacionID);
                                var poliza =
                                    FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(
                                        TipoPoliza.SalidaMuerte);
                                var polizaBL = new PolizaPL();
                                IList<PolizaInfo> polizaGuardada;
                                IList<PolizaInfo> polizaSalidaMuerte;
                                List<AnimalCostoInfo> costosPorAnimal;
                                for (var indexAnimales = 0;
                                     indexAnimales < animalesId.Count;
                                     indexAnimales++)
                                {
                                    polizaGuardada =
                                        polizaBL.ObtenerPoliza(TipoPoliza.SalidaMuerte,
                                                               organizacionID, fecha,
                                                               animalesId[indexAnimales].
                                                                   ToString(), "SM", 1);
                                    if (polizaGuardada == null || !polizaGuardada.Any())
                                    {
                                        costosPorAnimal =
                                            animalCosto.Where(
                                                id =>
                                                id.AnimalID == animalesId[indexAnimales]).
                                                ToList();
                                        polizaSalidaMuerte =
                                            poliza.GeneraPoliza(costosPorAnimal);
                                        if (polizaSalidaMuerte != null &&
                                            polizaSalidaMuerte.Any())
                                        {
                                            polizaSalidaMuerte.ToList().ForEach(datos =>
                                                                                    {
                                                                                        datos
                                                                                            .
                                                                                            UsuarioCreacionID
                                                                                            =
                                                                                            0;
                                                                                        datos
                                                                                            .
                                                                                            OrganizacionID
                                                                                            =
                                                                                            organizacionID;
                                                                                    });
                                            polizaBL.GuardarPolizaEntradaGanado(
                                                polizaSalidaMuerte,
                                                TipoPoliza.SalidaMuerte);
                                            logger.WriteEntry(
                                                string.Format(
                                                    "ESCRIBIO POLIZA DE MUERTE {0}",
                                                    animalesId[indexAnimales]),
                                                EventLogEntryType.Information);
                                        }
                                    }
                                }
                                logger.WriteEntry("FINALIZA A LA EJECUCION DE LA MUERTE", EventLogEntryType.Information);
                                enEjecucion = false;
                            }
                            else
                            {
                                enEjecucion = false;
                            }
                        }
                        else
                        {
                            enEjecucion = false;
                        }
                    }
                    catch (Exception)
                    {
                        logger.WriteEntry("ERROR AL QUERER ESCRIBIR POLIZA DE MUERTE",
                                          EventLogEntryType.Error);
                    }
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
            }
        }

        private static void HiloSecundario()
        {
            var polizaPL = new PolizaPL();
            PolizaAbstract polizaSAP = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.SalidaVenta);
            int organizacionID;
            int polizaID = 0;
            int tipoPolizaID = 0;
            var intentos = new List<int>();
            var archivosEnviados = new HashSet<string>();
            var thread = new Thread(delegate()
            {
                Queue<IList<PolizaInfo>> polizas = null;
                try
                {
                    polizas = polizaPL.ObtenerPolizasPendientes();
                }
                catch (Exception ex)
                {
                    logger.WriteEntry(
                        string.Format(
                            "ERROR AL QUERER RECUPERAR POLIZAS PENDIENTES {0}",
                            ex.StackTrace),
                        EventLogEntryType.Error);
                }
                if (polizas != null)
                {
                    while (polizas.Count > 0)
                    {
                        IList<PolizaInfo> poliza = polizas.Dequeue();
                        organizacionID =
                            poliza.Select(org => org.OrganizacionID).FirstOrDefault();
                        try
                        {
                            lock (archivosEnviados)
                            {
                                polizaID = poliza.Select(id => id.PolizaID).FirstOrDefault();
                                tipoPolizaID = poliza.Select(id => id.TipoPolizaID).FirstOrDefault();
                                switch ((TipoPoliza)tipoPolizaID)
                                {
                                    case TipoPoliza.EntradaGanado:
                                    case TipoPoliza.EntradaCompra:
                                    case TipoPoliza.PolizaSacrificio:
                                        IList<string> referencia3 =
                                            poliza.Select(ref3 => ref3.Referencia3).Distinct().ToList();
                                        for (int indexRef3 = 0; indexRef3 < referencia3.Count; indexRef3++)
                                        {
                                            IList<PolizaInfo> polizasEntrada =
                                                poliza.Where(ref3 => ref3.Referencia3.Equals(referencia3[indexRef3])).
                                                    ToList();
                                            if (polizasEntrada != null && polizasEntrada.Any())
                                            {
                                                if (!archivosEnviados.Contains(
                                                    polizasEntrada.Select(nombre => nombre.ArchivoFolio).First()))
                                                {
                                                    polizaSAP.GuardarArchivoXML(polizasEntrada, organizacionID);
                                                }
                                                archivosEnviados.Add(
                                                    polizasEntrada.Select(nombre => nombre.ArchivoFolio).First());
                                            }
                                        }
                                        break;
                                    default:
                                        if (
                                            !archivosEnviados.Contains(
                                                poliza.Select(nombre => nombre.ArchivoFolio).First()))
                                        {
                                            polizaSAP.GuardarArchivoXML(poliza, organizacionID);
                                        }
                                        archivosEnviados.Add(
                                            poliza.Select(nombre => nombre.ArchivoFolio).First());
                                        break;
                                }
                                logger.WriteEntry(
                                    string.Format(
                                        "ESCRIBIO EL ARCHIVO PARA LA CLAVE DE POLIZA {0}",
                                        polizaID), EventLogEntryType.Information);
                                try
                                {
                                    polizaPL.ActualizaArchivoEnviadoSAP(polizaID);
                                }
                                catch (Exception)
                                {
                                    logger.WriteEntry(
                                        string.Format(
                                            "OCURRIO UN ERROR AL QUERER ACTUALIZAR BANDERA DE ENVIADO A SERVIDOR {0}",
                                            polizaID), EventLogEntryType.Error);
                                }
                            }
                        }
                        catch (Exception)
                        {
                            lock (archivosEnviados)
                            {
                                if (!archivosEnviados.Contains(poliza.Select(nombre => nombre.ArchivoFolio).First()))
                                {
                                    lock (intentos)
                                    {
                                        if (intentos.Count(clave => clave == polizaID) < 3)
                                        {
                                            polizas.Enqueue(poliza);
                                            intentos.Add(polizaID);
                                        }
                                        else
                                        {
                                            intentos.RemoveAll(clave => clave == polizaID);
                                        }
                                    }
                                }
                                if (intentos.Count(clave => clave == polizaID) > 0)
                                {
                                    logger.WriteEntry(
                                        string.Format(
                                            "ERROR AL QUERER ESCRIBIR EL ARCHIVO PARA LA CLAVE DE POLIZA {0}, EN EL INTENTO {1}",
                                            polizaID, intentos.Count(clave => clave == polizaID)),
                                        EventLogEntryType.Error);
                                }
                            }
                        }
                    }
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }

        #endregion METODOS PRIVADOS
    }
}
