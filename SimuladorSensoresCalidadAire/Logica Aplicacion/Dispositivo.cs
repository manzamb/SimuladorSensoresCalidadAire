using System;
using System.Collections.Generic;
using System.Text;
using SimuladorSensoresCalidadAire.Logica_Aplicacion;

namespace SimuladorSensoresCalidadAire
{
    class Dispositivo
    {
        //Campos
        string nombredispositivo;
        string feedID;
        string url;
        string servidormqtt;
        List<Sensor> sensores = new List<Sensor>();

        //Propiedades
        public string Nombredispositivo { get => nombredispositivo; set => nombredispositivo = value; }
        public string FeedID { get => feedID; set => feedID = value; }
        public string Url { get => url; set => url = value; }
        public string Servidormqtt { get => servidormqtt; set => servidormqtt = value; }
        internal List<Sensor> Sensores { get => sensores; set => sensores = value; }

        //Metodos
        public Dispositivo()
        {
        }

        public Dispositivo(string nombredispositivo, string feedID, string url, string servidormqtt)
        {
            Nombredispositivo = nombredispositivo;
            FeedID = feedID;
            Url = url;
            Servidormqtt = servidormqtt;
        }

        /// <summary>
        /// Este procedimiento inicializa los senssores con una fecha de inicio de medición ahora y vuelve a cero la lista de medidas
        /// </summary>
        public void InicializarMedicion()
        {
            //fecha de la medida
            DateTime fechaInicioMedicion = DateTime.Now;

            foreach (Sensor s in sensores)
            {
                s.InicioMedicion = fechaInicioMedicion;
                s.Medidas.Clear();
            }
        }

        public void CrearSensor(string nombre, double tiempoExposicion, DateTime inicioMedicion, string topicomqtt, string unidades = "Ug/m3")
        {
            Sensor s = new Sensor
            {
                Nombre = nombre,
                Unidades = unidades,
                TiempoExposicion = tiempoExposicion,
                InicioMedicion = inicioMedicion,
                Topicomqtt = "/" + topicomqtt
            };
            sensores.Add(s);
        }

    }
}
