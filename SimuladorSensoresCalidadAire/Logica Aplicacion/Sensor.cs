using System;
using System.Collections.Generic;
using System.Text;

namespace SimuladorSensoresCalidadAire.Logica_Aplicacion
{
    class Sensor
    {
        //Campos
        string nombre;            //Nombre del sensor particular
        string unidades;          //Valor de las unidades de medición. Se coloca valor por defecto
        double tiempoexposicion;  //Debe colocarse en milisegundos
        DateTime iniciomedicion;  //Fecha en la que inicia la operación de las mediciones
        double nivelmedicion;     //Medición que e calcula como el promedio de la mediciones en el tiempo de exposición
        List<Medida> medidas = new List<Medida>();     //Vectos de datapoint o mediones realizadas en el intervalo
        string topicomqtt;        //Almacena el topico por defecto al cual publica el dato
        Boolean cambioNivelM = false;     //En true establece que el nivel de medición cambió y debe publicarse

        //Propiedades
        public string Nombre { get => nombre; set => nombre = value; }
        public string Unidades { get => unidades; set => unidades = value; }
        public double TiempoExposicion { get => tiempoexposicion; set => tiempoexposicion = value; }
        public DateTime InicioMedicion { get => iniciomedicion; set => iniciomedicion = value; }
        public double NivelMedicion
        {
            get
            {
                return nivelmedicion;
            }
            set
            {
                nivelmedicion = value;
                CambioNivelM = true;  //Un cambio en el Nivel implica que la bandera se coloca en true
            }
        }
        internal List<Medida> Medidas { get => medidas; set => medidas = value; }
        public string Topicomqtt { get => topicomqtt; set => topicomqtt = value; }
        public bool CambioNivelM { get => cambioNivelM; set => cambioNivelM = value; }

        //Metodos
        public Sensor()
        {
        }

        /// <summary>
        /// Crea un sensor particular. 
        /// </summary>
        /// <param name="nombre">Nombre del Sensor</param>
        /// <param name="tiempoExposicion">Tiempo segun la norma para la medición de los niveles</param>
        /// <param name="inicioMedicion">Fecha de inicio de mediciones</param>
        /// <param name="unidades">Unidades que maneja. Por defecto en Calidad del Aire es Ug/m3</param>
        //public Sensor(string nombre, double tiempoExposicion, DateTime inicioMedicion, string topicomqtt, string unidades = "Ug/m3")
        //{
        //    Nombre = nombre;
        //    Unidades = unidades;
        //    TiempoExposicion = tiempoExposicion;
        //    InicioMedicion = inicioMedicion;
        //    Topicomqtt = "/" + topicomqtt;
        //}

        /// <summary>
        /// Permite crear una nueva medida realizada por el sensor
        /// </summary>
        /// <param name="fechaMedida">fecha actual en la que se realizo la medida</param>
        /// <param name="valor">valor medido por el sensor</param>
        public void CrearMedida(DateTime fechaMedida, double valor)
        {
            Medida m = new Medida
            {
                FechaMedida = fechaMedida,
                Valor = valor
            };
            medidas.Add(m);
            CalcularMedicionNivel();
        }

        /// <summary>
        /// Funcion que permite convertir un intervalo de horas, minutos, segundos, milisegundos a milisegundos
        /// </summary>
        /// <param name="horas">horas a convertir</param>
        /// <param name="minutos">minutos a convertir</param>
        /// <param name="segundos">segundos a convertir</param>
        /// <param name="milisegundos">no se convierten pero se suman al valor total</param>
        /// <returns></returns>
        public static int AsignarTiempoExposicion(int horas, int minutos=0, int segundos=0, int milisegundos=0) => horas * 3600000 + minutos * 60000 + segundos * 1000 + milisegundos;

        /// <summary>
        /// Calcula el la medida promedio en el rango o tiempo de exposición definido. Una vez calculado, inicializa el tiempo de inicio y el vector de medidas
        /// </summary>
        /// <returns></returns>
        public void CalcularMedicionNivel()
        {
            double promedio = 0;
            DateTime FechaActual = DateTime.Now;

            //Verificar si cumple ya con el intervalo de medición
            TimeSpan tiempoactualexposicion = FechaActual - iniciomedicion;
            if (tiempoactualexposicion.TotalMilliseconds >= tiempoexposicion)
            {
                if (Medidas.Count > 0)
                {
                    foreach (Medida datapoint in Medidas)
                    {
                        promedio += datapoint.Valor;
                    }
                    promedio = promedio / Medidas.Count;
                }
                //Cambia el nivel de medición para verificar prevención, Alerta, Emeergencia 
                NivelMedicion = promedio;

                //Inicializar tiempo de inicio y vector de medidas
                InicioMedicion = DateTime.Now;
                medidas.Clear();
            }         
        }
    }
}
