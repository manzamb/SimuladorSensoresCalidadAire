using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using SimuladorSensoresCalidadAire.Logica_Aplicacion;

namespace SimuladorSensoresCalidadAire
{
    class SimularSensoresCA
    {
        //Parametros del Brpker MQTT
        //const string MQTT_BROKER_ADDRESS = "iot.eclipse.org";
        const string MQTT_BROKER_ADDRESS = "Localhost";
        //Tiempo en milisegundos para esperar cuando ocurre un error en el servidor. Permite dar tiempo para recuperarse
        const int SLEEP_TIME = 100000;
        //Lleva el conteo de cantidad de mensajes enviados al servidor
        static int nummensaje = 0;

        public static void Simular()
        {
            //El simulador crea 5 dispositivos que miden la calidad del aire. Cada dispositivo simula los sensores:
            //Antes de 2017: PST-24 horas,PM10-24 horas, SO2-24 horas, NO2- 1 horas, O3- 1 horas, CO- 8 horas.
            //Cambio 2017: PST-24 horas,PM10-24 horas, SO2-1 horas, NO2-1 horas, O3-8 horas, CO-8 horas.
            //Según la norma tecnica Colombiana Resolución 2554 del 2017 - Ministerio de Ambiente y Desarrollo Sostenible,
            //esos son los periodos de observación de cada una de las estaciones. Para los cuales en ese periodo de tiempo
            //se calcula el promedio de las mediciones en cada estación de los valores de los sensores

            //Identificadores feedXively de los sensores a simular que estan en los JSON del indice semántico
            //Crear los dispositivos
            Dispositivo calidadAireCajibio = new Dispositivo("CA Cajibio", "541602029","192.168.0.1", MQTT_BROKER_ADDRESS);
            Dispositivo calidadAirePopayan = new Dispositivo("CA Popayán", "2003665973", "192.168.0.2", MQTT_BROKER_ADDRESS);
            Dispositivo calidadAireTimbio = new Dispositivo("CA Timbio", "1898258902", "192.168.0.3", MQTT_BROKER_ADDRESS);
            Dispositivo calidadAireToribio = new Dispositivo("CA Toribio", "1509142040", "192.168.0.4", MQTT_BROKER_ADDRESS);
            Dispositivo calidadAireTambo = new Dispositivo("CA Tambo", "1422637955", "192.168.0.5", MQTT_BROKER_ADDRESS);

            //Crear los sensores de los dispositivos
            //fecha de la medida
            DateTime fechaInicioExposicion = DateTime.Now;

            //Sensores del dispositivo ubicado en Cajibio: Nombresensor, tiempoexposicion(hora,minutos,segundos,mili), topico
            //Los tiempos de Expocisión: Para N02 y S02 es 1h, O3 y CO es 8h, PM10 y PST es 24h. Se pueden cambiar aun perido mas corto
            //calidadAireCajibio.CrearSensor("NO2",Sensor.AsignarTiempoExposicion(1), fechaInicioExposicion, "Dioxido_de_nitrogeno_NO2");
            //calidadAireCajibio.CrearSensor("SO2", Sensor.AsignarTiempoExposicion(1), fechaInicioExposicion, "Dioxido_de_Azufre_SO2");
            //calidadAireCajibio.CrearSensor("O3", Sensor.AsignarTiempoExposicion(8), fechaInicioExposicion, "Ozono_03");
            //calidadAireCajibio.CrearSensor("CO", Sensor.AsignarTiempoExposicion(8), fechaInicioExposicion, "Monoxido_de_Carbono_CO"); 
            //calidadAireCajibio.CrearSensor("PM10", Sensor.AsignarTiempoExposicion(24), fechaInicioExposicion, "PM10_Material_Particulado");
            //calidadAireCajibio.CrearSensor("PM25", Sensor.AsignarTiempoExposicion(24), fechaInicioExposicion, "PST_Material_Particulado_Total");

            //para que se calcule el promedio de exposición: Ejemplo con conversión de 1 hora a 10 segundos tnenemos:
            //Para N02 y S02 es 10 seg, O3 y CO es 80 seg, PM10 y PST es 240 seg
            //Este es para que genere promedios más rápido, en minutos y menos. El de arriba sería como lo dicta la norma Colombiana en horas.
            calidadAireCajibio.CrearSensor("NO2", Sensor.AsignarTiempoExposicion(0,0,10), fechaInicioExposicion, "Dioxido_de_nitrogeno_NO2");
            calidadAireCajibio.CrearSensor("SO2", Sensor.AsignarTiempoExposicion(0,0,10), fechaInicioExposicion, "Dioxido_de_Azufre_SO2");
            calidadAireCajibio.CrearSensor("O3", Sensor.AsignarTiempoExposicion(0,0,80), fechaInicioExposicion, "Ozono_03");
            calidadAireCajibio.CrearSensor("CO", Sensor.AsignarTiempoExposicion(0,0,80), fechaInicioExposicion, "Monoxido_de_Carbono_CO");
            calidadAireCajibio.CrearSensor("PM10", Sensor.AsignarTiempoExposicion(0,0,240), fechaInicioExposicion, "PM10_Material_Particulado");
            calidadAireCajibio.CrearSensor("PM25", Sensor.AsignarTiempoExposicion(0,0,240), fechaInicioExposicion, "PST_Material_Particulado_Total");

            //Dado que los sensores de todos los dispositivos son los mismos se copian directamente del primero que se configuro
            calidadAirePopayan.Sensores = calidadAireCajibio.Sensores;
            calidadAireTimbio.Sensores = calidadAireCajibio.Sensores;
            calidadAireToribio.Sensores = calidadAireCajibio.Sensores;
            calidadAireTambo.Sensores = calidadAireCajibio.Sensores;

            //Lista de Dispositivos a procesar
            List<Dispositivo> DispositivosSimular = new List<Dispositivo>
            {
                calidadAireCajibio,
                calidadAirePopayan,
                calidadAireTimbio,
                calidadAireToribio,
                calidadAireTambo
            };

            //samplingPeriod es el periodo de publicación al broker MQTT de cada conjunto de datos de los N sensores de los M dispositivos
            //Esta en Milisegundos: Tratar que este tiempo sea menor de el tiempo de exposición.
            //Se puede hacer una regla de tres para generar promedio más rápido. Ej: 1 hora = 10 seg, entonces
            //no colocarlo mas de 5 segundos de espera.
            //Si se coloca en 0 la precisión del periodo de exposición es más exacta. Pero se llena de muchas mediciones
            //Cuando se utilicen horas, ya se puede colocar en 5 o 10 segundos pero la precisión del tiempo de exposición puede aumentar un poco
            const int samplingPeriod = 0;   
            string sample = "";
            double value = 0;

            //Iniciar las mediciones
            foreach (Dispositivo d in DispositivosSimular)
            {
                d.InicializarMedicion();
            }

            //En este bucle se crean todas las mediciones simuladas
            while (true)
            {

                //Obtener medición y enviar mensaje al broker MQTT
                foreach (Dispositivo d in DispositivosSimular)
                {
                    //fecha de la medida
                    DateTime fechamedida = DateTime.Now;

                    //Asignar medición aleatoria a cada sensor, se busca con una expresion lamda
                    d.Sensores.Find(x => x.Nombre == "NO2").CrearMedida(fechamedida, MuestraNO2());
                    d.Sensores.Find(x => x.Nombre == "SO2").CrearMedida(fechamedida, MuestraSO2());
                    d.Sensores.Find(x => x.Nombre == "O3").CrearMedida(fechamedida, MuestraO3());
                    d.Sensores.Find(x => x.Nombre == "CO").CrearMedida(fechamedida, MuestraCO());
                    d.Sensores.Find(x => x.Nombre == "PM10").CrearMedida(fechamedida, MuestraPM10());
                    d.Sensores.Find(x => x.Nombre == "PM25").CrearMedida(fechamedida, MuestraPST());
                }

                //Enviar mensajes al Broker MQTT
                //Obtener medición y enviar mensaje al broker MQTT
                foreach (Dispositivo d in DispositivosSimular)
                {
                    foreach(Sensor s in d.Sensores)
                    {
                        //Coloca el FeedID/Topico almacenado en cada sensor previamente
                        sample = d.FeedID + s.Topicomqtt;
                        //Obtiene el valor generado para el sensor en el bucle anterior. Es el último gnerado
                        value = s.Medidas[s.Medidas.Count-1].Valor;
                        //Envía el mensaje a Broker
                        EnviarMensajeMQTT(sample, value.ToString("n"));
                        //Verificar si se calculo un Nivel de medición para enviarlo por MQTT
                        if (s.CambioNivelM)
                        {
                            //Coloca la medició promedio en un topico interno a cada sesnsor. Ej.541602029/Dioxido_de_nitrogeno_NO2/PromedioMedicion
                            sample = d.FeedID + s.Topicomqtt + "/PromedioMedicion";
                            value = s.NivelMedicion;
                            EnviarMensajeMQTT(sample, value.ToString("n"));
                            s.CambioNivelM = false;
                        }
                    }
                }


                //Esperar el tiempo de mediciones definido para volver a enviar nuevas mediciones
                Console.WriteLine("Esperando {0:D} segundos para continuar con la siguiente medición", samplingPeriod / 1000);
                Thread.Sleep(samplingPeriod);
            }

        }

        #region   "Procedimientos de cada una de las mediciones de cada sensor"


        //////////////////////////////////////////////////////
        //PST-24 horas
        //Prevención	Alerta	    Emergencia
        //375 µg/m3	    625 µg/m3	875 µg/m3
        static double MuestraPST()
        {
            Random rnd = new Random();
            double medida = rnd.Next(900); //techo de 900 para que halla un intervalo de emergencia
            double mda = 150; //minimo de 150 para que no se entregen valores muy bajos

            if (medida >= 625 && medida <= 875)
            {
                if (1 == rnd.Next(3)) //si toma el valor 1 entre 3 valores envia los datos en el rango 625 - 875
                {
                    return mda = medida;
                }
            }

            if (medida > 875)
            {
                if (1 == rnd.Next(20)) //si toma el valor 1 entre 10 valores envia los datos en el rango mayor 875
                {
                    return mda = medida;
                }
            }

            if (mda == 150)
            {
                return mda = 150 + rnd.Next(225); // Para prevención es 375 = 150 + 225
            }

            return mda;
        }

        ////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////
        //PM10-24 horas
        //Prevención	Alerta	    Emergencia
        //300 µg/m3	    400 µg/m3	500 µg/m3
        private static double MuestraPM10()
        {
            Random rnd = new Random();
            double medida = rnd.Next(600); //techo de 900 para que halla un intervalo de emergencia
            double mda = 150; //minimo de 150 para que no se entregen valores muy bajos

            if (medida >= 400 && medida <= 500)
            {
                if (1 == rnd.Next(3)) //si toma el valor 1 entre 3 valores envia los datos en el rango 625 - 875
                {
                    return mda = medida;
                }
            }

            if (medida > 500)
            {
                if (1 == rnd.Next(20)) //si toma el valor 1 entre 10 valores envia los datos en el rango mayor 875
                {
                    return mda = medida;
                }
            }

            if (mda == 150)
            {
                return mda = 150 + rnd.Next(150); // Para prevención es 300 = 150 + 150
            }

            return mda;
        }

        ////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////
        //SO2-24 horas
        //Prevención	Alerta	    Emergencia
        //500 µg/m3	    1000 µg/m3	1600 µg/m3
        private static double MuestraSO2()
        {
            Random rnd = new Random();
            double medida = rnd.Next(1700); //techo de 900 para que halla un intervalo de emergencia
            double mda = 300; //minimo de 150 para que no se entregen valoras muy bajos

            if (medida >= 1000 && medida <= 1600)
            {
                if (1 == rnd.Next(3)) //si toma el valor 1 entre 3 valores envia los datos en el rango 625 - 875
                {
                    return mda = medida;
                }
            }

            if (medida > 1600)
            {
                if (1 == rnd.Next(20)) //si toma el valor 1 entre 10 valores envia los datos en el rango mayor 875
                {
                    return mda = medida;
                }
            }

            if (mda == 300)
            {
                return mda = 300 + rnd.Next(200); // Para prevención es 500 = 300 + 200
            }

            return mda;
        }
        ////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////
        //NO2- 1 horas
        //Prevención	Alerta	    Emergencia
        //400 µg/m3	    800 µg/m3	2000 µg/m3
        static double MuestraNO2()
        {
            Random rnd = new Random();
            double medida = rnd.Next(2100); //techo de 900 para que halla un intervalo de emergencia
            double mda = 300; //minimo de 150 para que no se entregen valoras muy bajos

            if (medida >= 800 && medida <= 2000)
            {
                if (1 == rnd.Next(3)) //si toma el valor 1 entre 3 valores envia los datos en el rango 625 - 875
                {
                    return mda = medida;
                }
            }

            if (medida > 2000)
            {
                if (1 == rnd.Next(20)) //si toma el valor 1 entre 10 valores envia los datos en el rango mayor 875
                {
                    return mda = medida;
                }
            }

            if (mda == 300)
            {
                return mda = 300 + rnd.Next(100); // Para prevención es 400 = 300 + 100
            }

            return mda;
        }
        ////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////
        //O3- 1 horas
        //Prevención	Alerta	    Emergencia
        //350 µg/m3	    700 µg/m3	1000 µg/m3
        static double MuestraO3()
        {
            Random rnd = new Random();
            double medida = rnd.Next(1100); //techo de 900 para que halla un intervalo de emergencia
            double mda = 250; //minimo de 150 para que no se entregen valoras muy bajos

            if (medida >= 700 && medida <= 1000)
            {
                if (1 == rnd.Next(3)) //si toma el valor 1 entre 3 valores envia los datos en el rango 625 - 875
                {
                    return mda = medida;
                }
            }

            if (medida > 1000)
            {
                if (1 == rnd.Next(20)) //si toma el valor 1 entre 10 valores envia los datos en el rango mayor 875
                {
                    return mda = medida;
                }
            }

            if (mda == 250)
            {
                return mda = 250 + rnd.Next(100); // Para prevención es 350 = 250 + 100
            }

            return mda;
        }
        ////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////
        //CO- 8 horas
        //Prevención	Alerta	    Emergencia
        //17 mg/m3	    34 mg/m3	46 mg/m3
        static double MuestraCO()
        {
            Random rnd = new Random();
            double medida = rnd.Next(50); //techo de 900 para que halla un intervalo de emergencia
            double mda = 10; //minimo de 150 para que no se entregen valoras muy bajos

            if (medida >= 34 && medida <= 46)
            {
                if (1 == rnd.Next(3)) //si toma el valor 1 entre 3 valores envia los datos en el rango 625 - 875
                {
                    return mda = medida;
                }
            }

            if (medida > 46)
            {
                if (1 == rnd.Next(20)) //si toma el valor 1 entre 10 valores envia los datos en el rango mayor 875
                {
                    return mda = medida;
                }
            }

            if (mda == 10)
            {
                return mda = 10 + rnd.Next(7); // Para prevención es 10 = 10 + 7
            }

            return mda;
        }
        ////////////////////////////////////////////////////////

        #endregion

        #region "Procedimientos para enviar el mensaje MQTT"

        private static void SuscribirTopicMQTT(string topic)
        {


            // create client instance 
            MqttClient client = new MqttClient(MQTT_BROKER_ADDRESS);

            // register to message received 
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);

            // subscribe to the topic Ej. "/home/temperature" with QoS 2 
            client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });


        }

        private static void EnviarMensajeMQTT(string topic, string payload)
        {
            // create client instance 
            MqttClient client = new MqttClient(MQTT_BROKER_ADDRESS);

            try
            {
                Console.WriteLine("Enviando mensaje MQTT: topico {0}, valor: {1:D} \n", topic, payload);

                string clientId = Guid.NewGuid().ToString();
                client.Connect(clientId);

                string strValue = Convert.ToString(payload);

                // publish a message on "/home/temperature" topic with QoS 2 
                client.Publish(topic, Encoding.UTF8.GetBytes(strValue));

                //Console.WriteLine("Mensaje MQTT Enviando. Esperando {0:D} segundos para nueva carga...", SLEEP_TIME / 1000);
                //Espera 5 segundos antes de volver a enviar un mensaje al broker
                //Thread.Sleep(SLEEP_TIME);
                Console.WriteLine("Mensaje No. {0} MQTT Enviando con Exito.", ++nummensaje);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Console.WriteLine("Esperando {0:D} segundos para continuar con la siguiente medición", SLEEP_TIME / 1000);

                //Espera n segundos antes de volver a enviar un mensaje al broker y que este se restablezca
                Thread.Sleep(SLEEP_TIME);
            }
        }

        static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            // handle message received 
        }
        #endregion

    }


}
