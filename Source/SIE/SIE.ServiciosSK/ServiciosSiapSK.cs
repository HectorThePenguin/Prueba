using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using System.Timers;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace ServiciosSK
{
    /// <summary>
    /// Clase del servicio de consumo de alimentos
    /// </summary>
    public partial class ServiciosSiapSK : ServiceBase
    {
        //Initialize the timer
        System.Timers.Timer timer = new System.Timers.Timer();
        System.Timers.Timer timerUpdate = new System.Timers.Timer();

        IList<ConfiguracionAccionesInfo> tareas;
        private static EventLog eventLog1;

        /// <summary>
        /// Constructor
        /// </summary>
        public ServiciosSiapSK()
        {
            InitializeComponent();

            CanPauseAndContinue = true;
            ServiceName = "ServiciosSiapSK";

            if (!EventLog.SourceExists("ServiciosSiapSK"))
                EventLog.CreateEventSource("ServiciosSiapSK", "Application");

            eventLog1 = new EventLog("Application", Environment.MachineName, "ServiciosSiapSK");  
        }

        /// <summary>
        /// Clase que ejecuta la aplicacion demanda.
        /// </summary>
        class AppLauncher
        {
            ConfiguracionAccionesInfo app;
            public AppLauncher(ConfiguracionAccionesInfo tarea)
            {
                app = tarea;
            }

            public void RunApp()
            {
                if (app.Codigo == AccionesSIAPEnum.SerConsAli.ToString())
                {
                    eventLog1.WriteEntry("ServiciosSiapSK RunApp Consumo de Alimento Inicio.");
                    try
                    {
                        var consumoAlimentoPL = new ConsumoAlimentoPL();

                        consumoAlimentoPL.GenerarConsumoAlimento(0,DateTime.Now.AddDays(-1));
                    }
                    catch (Exception ex)
                    {
                        eventLog1.WriteEntry("ServiciosSiapSK RunApp Consumo Alimento Exception: " + ex);
                    }
                    finally
                    {
                        eventLog1.WriteEntry("ServiciosSiapSK RunApp Consumo de Alimento Fin.");
                    }
                }
                else if (app.Codigo == AccionesSIAPEnum.SerCostGan.ToString())
                {
                    eventLog1.WriteEntry("ServiciosSiapSK RunApp Costeo de Ganado Inicio.");
                    try
                    {
                        var costeoGanadoPL = new CosteoGanadoPL();
                        costeoGanadoPL.GenerarCosteoGanado();
                    }
                    catch (Exception ex)
                    {
                        eventLog1.WriteEntry("ServiciosSiapSK RunApp Costeo de Ganado Exception: " + ex);
                    }
                    finally
                    {
                        eventLog1.WriteEntry("ServiciosSiapSK RunApp Costeo de Ganado Fin.");
                    }
                }
                else if (app.Codigo == AccionesSIAPEnum.SerAlerta.ToString())
                {
                    eventLog1.WriteEntry("ServiciosSiapSK RunApp Alertas SIAP.");
                    try
                    {
                        var incidenciaPL = new IncidenciasPL();
                        incidenciaPL.GenerarIncidenciasSIAP();
                    }
                    catch (Exception ex)
                    {
                        eventLog1.WriteEntry("ServiciosSiapSK RunApp Alertas SIAP Exception: " + ex);
                    }
                    finally
                    {
                        eventLog1.WriteEntry("ServiciosSiapSK RunApp Alertas SIAP Fin.");
                    }
                }
                
            }
        }
        
        /// <summary>
        /// Actualiza la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void TimeUpdateElapsed(object sender, ElapsedEventArgs args)
        {
            try
            {
                var configuracionAccionesPL = new ConfiguracionAccionesPL();
                tareas = configuracionAccionesPL.ObtenerTodos();
            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry("ServiciosSiapSK TimeUpdateElapsed Exception: " + ex);
            }
        }
        
         /// <summary>
        /// Timer handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void TimeElapsed(object sender, ElapsedEventArgs args)
        {
            var configuracionPL = new ConfiguracionAccionesPL();
            tareas = configuracionPL.ObtenerTodos();

            foreach (var tarea in tareas)
            {
                DateTime currTime = DateTime.Now;
                DayOfWeek day = currTime.DayOfWeek;
                DateTime runTime = tarea.FechaEjecucion;

                //Para saber si ya se ejecuto en el mismo dia
                DateTime ultimafecha = new DateTime(tarea.FechaUltimaEjecucion.Year,tarea.FechaUltimaEjecucion.Month,tarea.FechaUltimaEjecucion.Day);

                if ((tarea.Repetir && ultimafecha.Date < currTime.Date && ((currTime.Hour > runTime.Hour) || (currTime.Hour == runTime.Hour && currTime.Minute >= runTime.Minute))) ||
                    (!tarea.Repetir && runTime.Date > ultimafecha.Date) && tarea.Codigo != AccionesSIAPEnum.SerAlerta.ToString())
                {
                    bool ejecutar = false;

                    if (tarea.Lunes && day == DayOfWeek.Monday)
                        ejecutar = true;
                    else if (tarea.Martes && day == DayOfWeek.Tuesday)
                        ejecutar = true;
                    else if (tarea.Miercoles && day == DayOfWeek.Wednesday)
                        ejecutar = true;
                    else if (tarea.Jueves && day == DayOfWeek.Thursday)
                        ejecutar = true;
                    else if (tarea.Viernes && day == DayOfWeek.Friday)
                        ejecutar = true;
                    else if (tarea.Sabado && day == DayOfWeek.Saturday)
                        ejecutar = true;
                    else if (tarea.Domingo && day == DayOfWeek.Sunday)
                        ejecutar = true;

                    if (ejecutar)
                    {
                        eventLog1.WriteEntry("ServiciosSiapSK AppLauncher Ejecutar tarea.");
                        var launcher = new AppLauncher(tarea);
                        new Thread(new ThreadStart(launcher.RunApp)).Start();

                        tarea.FechaUltimaEjecucion = DateTime.Now;
                        UpdateTarea(tarea);
                    }

                }
                if (tarea.Repetir && tarea.Codigo == AccionesSIAPEnum.SerAlerta.ToString() &&
                     (((currTime.Hour > runTime.Hour) || (currTime.Hour == runTime.Hour && currTime.Minute >= runTime.Minute)) || currTime.Date > runTime.Date || currTime >= runTime.AddHours(1)))
                {
                    bool ejecutar = false;

                    if (tarea.Lunes && day == DayOfWeek.Monday)
                        ejecutar = true;
                    else if (tarea.Martes && day == DayOfWeek.Tuesday)
                        ejecutar = true;
                    else if (tarea.Miercoles && day == DayOfWeek.Wednesday)
                        ejecutar = true;
                    else if (tarea.Jueves && day == DayOfWeek.Thursday)
                        ejecutar = true;
                    else if (tarea.Viernes && day == DayOfWeek.Friday)
                        ejecutar = true;
                    else if (tarea.Sabado && day == DayOfWeek.Saturday)
                        ejecutar = true;
                    else if (tarea.Domingo && day == DayOfWeek.Sunday)
                        ejecutar = true;

                    if (ejecutar)
                    {
                        ActualizarFechaEjecucion(tarea);
                        eventLog1.WriteEntry("ServiciosSiapSK AppLauncher Ejecutar tarea.");
                        var launcher = new AppLauncher(tarea);
                        new Thread(new ThreadStart(launcher.RunApp)).Start();
                        tarea.FechaUltimaEjecucion = DateTime.Now;
                        UpdateTarea(tarea);
                    }
                }



            }
        }


        /// <summary>
        /// Entrada principal del servicio
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] { new ServiciosSiapSK() };

            Run(ServicesToRun);
        }

        /// <summary>
        /// Establece las cosas a trabajar
        /// </summary>
        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("ServiciosSiapSK Service OnStart"); 
            try
            {
                var configuracionAccionesPL = new ConfiguracionAccionesPL();
                tareas = configuracionAccionesPL.ObtenerTodos();

            }
            catch (Exception ex)
            {
                var srvcController = new ServiceController(ServiceName);
                eventLog1.WriteEntry("ServiciosSiapSK OnStart Exception: " + ex); 
                srvcController.Stop();
            }

            //Tiempo de ejecucion intervalo para la ejecucion de tareas
            timer.Interval = 60000; //TODO: Obtener de la base de datos de parametros
            timer.Elapsed += new ElapsedEventHandler(TimeElapsed);
            timer.Start();
            
            //Tiempo de ejecucion intervalo para la ejecucion de tareas
            timerUpdate.Interval = 100000; //TODO: Obtener de la base de datos de parametros
            timerUpdate.Elapsed += new ElapsedEventHandler(TimeUpdateElapsed);
            timerUpdate.Start();
        }
        
        /// <summary>
        /// Actualizar ejecucion de tarea
        /// </summary>
        /// <param name="tarea"></param>
        private void UpdateTarea(ConfiguracionAccionesInfo tarea)
        {
            try
            {
                var pl = new ConfiguracionAccionesPL();
                pl.ActualizarEjecucionTarea(tarea);
            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry("ServiciosSiapSK UpdateTarea Exception: " + ex); 
            }
        }
        /// <summary>
        /// Actualiza fecha de ejecucion
        /// </summary>
        /// <param name="tarea"></param>
        /// <returns></returns>
        private void ActualizarFechaEjecucion(ConfiguracionAccionesInfo tarea)
        {
            try
            {
                var fechaEjecucion = DateTime.Now;
                tarea.FechaEjecucion = fechaEjecucion.AddHours(1);
                var pl = new ConfiguracionAccionesPL();
                pl.ActualizarFechaEjecucion(tarea);
            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry("ServiciosSiapSK UpdateTareaFechaEjecucion Exception: " + ex);
            }
        }

        /*
        /// <summary>
        /// Detener el servicio
        /// </summary>
        protected override void OnStop()
        {
            eventLog1.WriteEntry("ServiciosSiapSK service stopped");
        }

        /// <summary>
        /// Trazar servicio
        /// </summary>
        /// <param name="content"></param>
        private void TraceService(string content)
        {
            var fs = new FileStream(@"c:\ServicioAlimentacionSK.txt", FileMode.OpenOrCreate, FileAccess.Write);
            var sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(content);
            sw.Flush();
            sw.Close();
        }*/
    }
}
