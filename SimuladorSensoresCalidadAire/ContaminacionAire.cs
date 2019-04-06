using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

public class ContyaminacionAire
{
    //Parametros del Brpker MQTT
    const string MQTT_BROKER_ADDRESS = "iot.eclipse.org";

    public static void CrearMediciones()
    {

        //El simulador crea 5 dispositivos que miden la calidad del aire. Cada dispositivo simula los sensores:
        //PST-24 horas,PM10-24 horas, SO2-24 horas, NO2- 1 horas, O3- 1 horas, CO- 8 horas.
        //Según la norma tecnica Colombiana esos son los periodos de observación de cada una de las estaciones.

        //Identificadores feedXively de los sensores a simular que estan en los JSON del indice semántico
        string[] feedSimular = new string[5] { "541602029", "2003665973", "1898258902", "1509142040", "1422637955" };

        //const int samplingPeriod = 3600000;   // 1 hora
        const int samplingPeriod = 30000;   // 30 seg

        double value = 0;

        //En este bucle se crean todas las mediciones simuladas
        while (true)
        {
            //string sample
            WaitUntilNextPeriod(samplingPeriod);

            String sample = "";


//POPAYAN
//MUESTRA DE CO - 8 HORAS
                ///////////////////////////////////////////////
                ///////////////////////////////////////////////
                ///////////////////////////////////////////////
                ///////////////////////////////////////////////
                ///////////////////////////////////////////////


 //-----------------------------------------------------------

            //CO - Cajibío
            //intervalo 8 horas
            if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 9) //1 hora entre intervalos ya que se envia cada 1 hora
            //if (DateTime.Now.Second >= 16 && DateTime.Now.Second < 26) //simula (8*2=16) - (32) - (48) 10seg entre intervalos ya que se envia cada 10seg
            {
                value = MuestraCO();

                sample = feedSimular[0] + "/Monoxido_de_Carbono_CO";

                EnviarMensajeMQTT(sample, value.ToString("n"));

            }
            //////////////////////////////////////////////


            ///////////////////////////////////////////////
            ///////////////////////////////////////////////
            //CO - Cajibío
            //intervalo 8 horas
            if (DateTime.Now.Hour >= 16 && DateTime.Now.Hour < 17)
            //if (DateTime.Now.Second >= 32 && DateTime.Now.Second < 42) //simula (8*2=16) - (32) - (48) con los primeros 10seg de cada min ya que se envia cada 10min
            {
                value = MuestraCO();

                sample = feedSimular[0] + "/Monoxido_de_Carbono_CO";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }
            //////////////////////////////////////////////

            ///////////////////////////////////////////////
            ///////////////////////////////////////////////
            //CO  - Cajibío
            //intervalo 8 horas
            if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 1)
            //if (DateTime.Now.Second >= 48 && DateTime.Now.Second < 58) //simula (8*2=16) - (32) - (48) con los primeros 10seg de cada min ya que se envia cada 10min
            {
                value = MuestraCO();

                sample = feedSimular[0] + "/Monoxido_de_Carbono_CO";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }


            ////---------------------------------------------------------
            //System.Threading.Timer timer1 =
            //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
            //Thread.Sleep(Timeout.Infinite);
            ////---------------------------------------------------------

                ///////////////////////////////////////////////
                //CO - Popayan
                //intervalo 8 horas
            if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 9) //1 hora entre intervalos ya que se envia cada 1 hora
                //if (DateTime.Now.Second >= 16 && DateTime.Now.Second < 26) //simula (8*2=16) - (32) - (48) 10seg entre intervalos ya que se envia cada 10seg
                {
                value = MuestraCO();

                sample = feedSimular[1] + "/Monoxido_de_Carbono_CO";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }
            //////////////////////////////////////////////


                ///////////////////////////////////////////////
                ///////////////////////////////////////////////
                //CO - Popayan
                //intervalo 8 horas
            if (DateTime.Now.Hour >= 16 && DateTime.Now.Hour < 17)
                //if (DateTime.Now.Second >= 32 && DateTime.Now.Second < 42) //simula (8*2=16) - (32) - (48) con los primeros 10seg de cada min ya que se envia cada 10min
                {
                value = MuestraCO();

                sample = feedSimular[1] + "/Monoxido_de_Carbono_CO";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }
            //////////////////////////////////////////////

                ///////////////////////////////////////////////
                ///////////////////////////////////////////////
                //CO - Popayan
                //intervalo 8 horas
            if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 1)
                //if (DateTime.Now.Second >= 48 && DateTime.Now.Second < 58) //simula (8*2=16) - (32) - (48) con los primeros 10seg de cada min ya que se envia cada 10min
                {
                value = MuestraCO();

                sample = feedSimular[1] + "/Monoxido_de_Carbono_CO";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }

            ////---------------------------------------------------------
            //System.Threading.Timer timer2 =
            //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
            //Thread.Sleep(Timeout.Infinite);
            ////---------------------------------------------------------

                //CO - Timbío
                //intervalo 8 horas
            if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 9) //1 hora entre intervalos ya que se envia cada 1 hora
                //if (DateTime.Now.Second >= 16 && DateTime.Now.Second < 26) //simula (8*2=16) - (32) - (48) 10seg entre intervalos ya que se envia cada 10seg
                {
                value = MuestraCO();

                sample = feedSimular[2] + "/Monoxido_de_Carbono_CO";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }
                //////////////////////////////////////////////


                ///////////////////////////////////////////////
                ///////////////////////////////////////////////
                //CO - Timbío
                //intervalo 8 horas
            if (DateTime.Now.Hour >= 16 && DateTime.Now.Hour < 17)
                //if (DateTime.Now.Second >= 32 && DateTime.Now.Second < 42) //simula (8*2=16) - (32) - (48) con los primeros 10seg de cada min ya que se envia cada 10min
                {
                value = MuestraCO();

                sample = feedSimular[2] + "/Monoxido_de_Carbono_CO";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }
                //////////////////////////////////////////////

                ///////////////////////////////////////////////
                ///////////////////////////////////////////////
                //CO  - Timbío
                //intervalo 8 horas
            if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 1)
                //if (DateTime.Now.Second >= 48 && DateTime.Now.Second < 58) //simula (8*2=16) - (32) - (48) con los primeros 10seg de cada min ya que se envia cada 10min
                {
                value = MuestraCO();

                sample = feedSimular[2] + "/Monoxido_de_Carbono_CO";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }

            ////---------------------------------------------------------
            //System.Threading.Timer time3 =
            //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
            //Thread.Sleep(Timeout.Infinite);
            ////---------------------------------------------------------

                //CO - Toribío
                //intervalo 8 horas
            if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 9) //1 hora entre intervalos ya que se envia cada 1 hora
                //if (DateTime.Now.Second >= 16 && DateTime.Now.Second < 26) //simula (8*2=16) - (32) - (48) 10seg entre intervalos ya que se envia cada 10seg
                {
                value = MuestraCO();

                sample = feedSimular[3] + "/Monoxido_de_Carbono_CO";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }
                //////////////////////////////////////////////


                ///////////////////////////////////////////////
                ///////////////////////////////////////////////
                //CO - Toribío
                //intervalo 8 horas
            if (DateTime.Now.Hour >= 16 && DateTime.Now.Hour < 17)
                //if (DateTime.Now.Second >= 32 && DateTime.Now.Second < 42) //simula (8*2=16) - (32) - (48) con los primeros 10seg de cada min ya que se envia cada 10min
                {
                value = MuestraCO();

                sample = feedSimular[3] + "/Monoxido_de_Carbono_CO";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }
                //////////////////////////////////////////////

                ///////////////////////////////////////////////
                ///////////////////////////////////////////////
                //CO  - Toribío
                //intervalo 8 horas
            if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 1)
                //if (DateTime.Now.Second >= 48 && DateTime.Now.Second < 58) //simula (8*2=16) - (32) - (48) con los primeros 10seg de cada min ya que se envia cada 10min
                {
                value = MuestraCO();

                sample = feedSimular[3] + "/Monoxido_de_Carbono_CO";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }

            ////---------------------------------------------------------
            //System.Threading.Timer timer4 =
            //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
            //Thread.Sleep(Timeout.Infinite);
            ////---------------------------------------------------------

                //CO - El Tambo
                //intervalo 8 horas
            if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 9) //1 hora entre intervalos ya que se envia cada 1 hora
                //if (DateTime.Now.Second >= 16 && DateTime.Now.Second < 26) //simula (8*2=16) - (32) - (48) 10seg entre intervalos ya que se envia cada 10seg
                {
                value = MuestraCO();

                sample = feedSimular[4] + "/Monoxido_de_Carbono_CO";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }
                //////////////////////////////////////////////


                ///////////////////////////////////////////////
                ///////////////////////////////////////////////
                //CO - El Tambo
                //intervalo 8 horas
            if (DateTime.Now.Hour >= 16 && DateTime.Now.Hour < 17)
                //if (DateTime.Now.Second >= 32 && DateTime.Now.Second < 42) //simula (8*2=16) - (32) - (48) con los primeros 10seg de cada min ya que se envia cada 10min
                {
                value = MuestraCO();

                sample = feedSimular[4] + "/Monoxido_de_Carbono_CO";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }
                //////////////////////////////////////////////

                ///////////////////////////////////////////////
                ///////////////////////////////////////////////
                //CO  - El Tambo
                //intervalo 8 horas
            if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 1)
                //if (DateTime.Now.Second >= 48 && DateTime.Now.Second < 58) //simula (8*2=16) - (32) - (48) con los primeros 10seg de cada min ya que se envia cada 10min
                {
                value = MuestraCO();

                sample = feedSimular[4] + "/Monoxido_de_Carbono_CO";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }

            ////---------------------------------------------------------
            //System.Threading.Timer timer5 =
            //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
            //Thread.Sleep(Timeout.Infinite);
            ////---------------------------------------------------------

                ///////////////////////////////////////////////
                //NO2 - Cajibío
                //intervalo cada hora

                value = MuestraNO2();

                sample = feedSimular[0] + "/Dioxido_de_nitrogeno_NO2";

                EnviarMensajeMQTT(sample, value.ToString("n"));


            //////////////////////////////////////////////

            ////---------------------------------------------------------
            //System.Threading.Timer timer6 =
            //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
            //Thread.Sleep(Timeout.Infinite);
            ////---------------------------------------------------------

            ///////////////////////////////////////////////
            //NO2 - El Tambo
            //intervalo cada hora

            value = MuestraNO2();

            sample = feedSimular[4] + "/Dioxido_de_nitrogeno_NO2";

            EnviarMensajeMQTT(sample, value.ToString("n"));
            //////////////////////////////////////////////

            ////---------------------------------------------------------
            //System.Threading.Timer timer7 =
            //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
            //Thread.Sleep(Timeout.Infinite);
            ////---------------------------------------------------------

            ///////////////////////////////////////////////
            //NO2 - Popayan
            //intervalo cada hora

            value = MuestraNO2();

            sample = feedSimular[1] + "/Dioxido_de_nitrogeno_NO2";

            EnviarMensajeMQTT(sample, value.ToString("n"));
            //////////////////////////////////////////////

            ////---------------------------------------------------------
            //System.Threading.Timer timer8 =
            //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
            //Thread.Sleep(Timeout.Infinite);
            ////---------------------------------------------------------

            ///////////////////////////////////////////////
            //NO2 - Timbío
            //intervalo cada hora

            value = MuestraNO2();

            sample = feedSimular[2] + "/Dioxido_de_nitrogeno_NO2";

            EnviarMensajeMQTT(sample, value.ToString("n"));
            //////////////////////////////////////////////

            ////---------------------------------------------------------
            //System.Threading.Timer timer9 =
            //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
            //Thread.Sleep(Timeout.Infinite);
            ////---------------------------------------------------------

            ///////////////////////////////////////////////
            //NO2 - Toribío
            //intervalo cada hora

            value = MuestraNO2();

            sample = feedSimular[3] + "/Dioxido_de_nitrogeno_NO2";

            EnviarMensajeMQTT(sample, value.ToString("n"));
            //////////////////////////////////////////////

            ////---------------------------------------------------------
            //System.Threading.Timer timer10 =
            //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
            //Thread.Sleep(Timeout.Infinite);
            ////---------------------------------------------------------

            ///////////////////////////////////////////////
            //O3 - Cajibío
            //intervalo cada hora

            value = MuestraO3();

            sample = feedSimular[0] + "/Ozono_03";

            EnviarMensajeMQTT(sample, value.ToString("n"));


            //////////////////////////////////////////////

            ////---------------------------------------------------------
            //System.Threading.Timer timer11 =
            //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
            //Thread.Sleep(Timeout.Infinite);
            ////---------------------------------------------------------

            ///////////////////////////////////////////////
            //O3 - El Tambo
            //intervalo cada hora

            value = MuestraO3();

            sample = feedSimular[4] + "/Ozono_03";

            EnviarMensajeMQTT(sample, value.ToString("n"));
            //////////////////////////////////////////////

            ////---------------------------------------------------------
            //System.Threading.Timer timer12 =
            //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
            //Thread.Sleep(Timeout.Infinite);
            ////---------------------------------------------------------

            ///////////////////////////////////////////////
            //O3 - Popayan
            //intervalo cada hora

            value = MuestraO3();

            sample = feedSimular[1] + "/Ozono_03";

            EnviarMensajeMQTT(sample, value.ToString("n"));
            //////////////////////////////////////////////

            ////---------------------------------------------------------
            //System.Threading.Timer timer13 =
            //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
            //Thread.Sleep(Timeout.Infinite);
            ////---------------------------------------------------------

            ///////////////////////////////////////////////
            //O3 - Timbío
            //intervalo cada hora

            value = MuestraO3();

            sample = feedSimular[2] + "/Ozono_03";

            EnviarMensajeMQTT(sample, value.ToString("n"));
            //////////////////////////////////////////////

            ////---------------------------------------------------------
            //System.Threading.Timer timer14 =
            //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
            //Thread.Sleep(Timeout.Infinite);
            ////---------------------------------------------------------

            ///////////////////////////////////////////////
            //O3 - Toribío
            //intervalo cada hora

            value = MuestraO3();

            value = MuestraO3();

            sample = feedSimular[3] + "/Ozono_03";

            EnviarMensajeMQTT(sample, value.ToString("n"));
            //////////////////////////////////////////////

            ////---------------------------------------------------------
            //System.Threading.Timer timer15 =
            //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
            //Thread.Sleep(Timeout.Infinite);
            ////---------------------------------------------------------

            ///////////////////////////////////////////////
            //PM10 - Cajibío
            //intervalo 24 horas

            if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 1)
                //if (DateTime.Now.Second >= 0 && DateTime.Now.Second < 10) //simula 0:00 - 1:00 con los primeros 10seg de cada min ya que se envia cada 10seg
                {
                value = MuestraPM10();
                sample = feedSimular[0] + "/PM10_Material_Particulado";

                EnviarMensajeMQTT(sample, value.ToString("n"));

                }
                //////////////////////////////////////////////

                ////---------------------------------------------------------
                //System.Threading.Timer timer16 =
                //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
                //Thread.Sleep(Timeout.Infinite);
                ////---------------------------------------------------------

                ///////////////////////////////////////////////
                //PM10 - El Tambo
                //intervalo 24 horas

                if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 1)
                //if (DateTime.Now.Second >= 0 && DateTime.Now.Second < 10) //simula 0:00 - 1:00 con los primeros 10seg de cada min ya que se envia cada 10seg
                {
                value = MuestraPM10();
                sample = feedSimular[4] + "/PM10_Material_Particulado";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }
                //////////////////////////////////////////////

                ////---------------------------------------------------------
                //System.Threading.Timer timer17 =
                //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
                //Thread.Sleep(Timeout.Infinite);
                ////---------------------------------------------------------


                ///////////////////////////////////////////////
                //PM10 - Popayan
                //intervalo 24 horas

                if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 1)
                //if (DateTime.Now.Second >= 0 && DateTime.Now.Second < 10) //simula 0:00 - 1:00 con los primeros 10seg de cada min ya que se envia cada 10seg
                {
                value = MuestraPM10();
                sample = feedSimular[1] + "/PM10_Material_Particulado";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }
                //////////////////////////////////////////////

                ////---------------------------------------------------------
                //System.Threading.Timer timer18 =
                //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
                //Thread.Sleep(Timeout.Infinite);
                ////---------------------------------------------------------

                ///////////////////////////////////////////////
                //PM10 - Timbío
                //intervalo 24 horas

                if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 1)
                //if (DateTime.Now.Second >= 0 && DateTime.Now.Second < 10) //simula 0:00 - 1:00 con los primeros 10seg de cada min ya que se envia cada 10seg
                {
                value = MuestraPM10();
                sample = feedSimular[2] + "/PM10_Material_Particulado";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }
                //////////////////////////////////////////////

                ////---------------------------------------------------------
                //System.Threading.Timer timer19 =
                //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
                //Thread.Sleep(Timeout.Infinite);
                ////---------------------------------------------------------

                ///////////////////////////////////////////////
                //PM10 - Toribío
                //intervalo 24 horas

                if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 1)
                //if (DateTime.Now.Second >= 0 && DateTime.Now.Second < 10) //simula 0:00 - 1:00 con los primeros 10seg de cada min ya que se envia cada 10seg
                {
                value = MuestraPM10();
                sample = feedSimular[3] + "/PM10_Material_Particulado";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }
                //////////////////////////////////////////////

                ////---------------------------------------------------------
                //System.Threading.Timer timer20 =
                //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
                //Thread.Sleep(Timeout.Infinite);
                ////---------------------------------------------------------


                ///////////////////////////////////////////////
                //PST - Cajibío
                //intervalo 24 horas

                if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 1)
                //if (DateTime.Now.Second >= 0 && DateTime.Now.Second < 10) //simula 0:00 - 1:00 con los primeros 10seg de cada min ya que se envia cada 10seg
                {
                value = MuestraPST();
                sample = feedSimular[0] + "/PST_Material_Particulado_Total";

                EnviarMensajeMQTT(sample, value.ToString("n"));

                }
                //////////////////////////////////////////////

                ////---------------------------------------------------------
                //System.Threading.Timer timer21 =
                //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
                //Thread.Sleep(Timeout.Infinite);
                ////---------------------------------------------------------

                ///////////////////////////////////////////////
                //PST - El Tambo
                //intervalo 24 horas

                if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 1)
                //if (DateTime.Now.Second >= 0 && DateTime.Now.Second < 10) //simula 0:00 - 1:00 con los primeros 10seg de cada min ya que se envia cada 10seg
                {
                value = MuestraPST();
                sample = feedSimular[4] + "/PST_Material_Particulado_Total";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }
                //////////////////////////////////////////////

                ////---------------------------------------------------------
                //System.Threading.Timer timer22 =
                //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
                //Thread.Sleep(Timeout.Infinite);
                ////---------------------------------------------------------

                ///////////////////////////////////////////////
                //PST - Popayan
                //intervalo 24 horas

                if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 1)
                //if (DateTime.Now.Second >= 0 && DateTime.Now.Second < 10) //simula 0:00 - 1:00 con los primeros 10seg de cada min ya que se envia cada 10seg
                {
                value = MuestraPST();
                sample = feedSimular[1] + "/PST_Material_Particulado_Total";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }
                //////////////////////////////////////////////

                ////---------------------------------------------------------
                //System.Threading.Timer timer23 =
                //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
                //Thread.Sleep(Timeout.Infinite);
                ////---------------------------------------------------------

                ///////////////////////////////////////////////
                //PST - Timbío
                //intervalo 24 horas

                if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 1)
                //if (DateTime.Now.Second >= 0 && DateTime.Now.Second < 10) //simula 0:00 - 1:00 con los primeros 10seg de cada min ya que se envia cada 10seg
                {
                value = MuestraPST();
                sample = feedSimular[2] + "/PST_Material_Particulado_Total";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }
                //////////////////////////////////////////////

                ////---------------------------------------------------------
                //System.Threading.Timer timer24 =
                //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
                //Thread.Sleep(Timeout.Infinite);
                ////---------------------------------------------------------

                ///////////////////////////////////////////////
                //PST - Toribío
                //intervalo 24 horas

                if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 1)
                //if (DateTime.Now.Second >= 0 && DateTime.Now.Second < 10) //simula 0:00 - 1:00 con los primeros 10seg de cada min ya que se envia cada 10seg
                {
                value = MuestraPST();
                sample = feedSimular[3] + "/PST_Material_Particulado_Total";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }
                //////////////////////////////////////////////

                ////---------------------------------------------------------
                //System.Threading.Timer timer25 =
                //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
                //Thread.Sleep(Timeout.Infinite);
                ////---------------------------------------------------------


                ///////////////////////////////////////////////
                //SO2 - Cajibío
                //intervalo 24 horas

                if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 1)
                //if (DateTime.Now.Second >= 0 && DateTime.Now.Second < 10) //simula 0:00 - 1:00 con los primeros 10seg de cada min ya que se envia cada 10seg
                {
                value = MuestraSO2();
                sample = feedSimular[0] + "/Dioxido_de_Azufre_SO2";

                EnviarMensajeMQTT(sample, value.ToString("n"));

                }
                //////////////////////////////////////////////

                ////---------------------------------------------------------
                //System.Threading.Timer timer26 =
                //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
                //Thread.Sleep(Timeout.Infinite);
                ////---------------------------------------------------------

                ///////////////////////////////////////////////
                //SO2 - El Tambo
                //intervalo 24 horas

                if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 1)
                //if (DateTime.Now.Second >= 0 && DateTime.Now.Second < 10) //simula 0:00 - 1:00 con los primeros 10seg de cada min ya que se envia cada 10seg
                {
                value = MuestraSO2();
                sample = feedSimular[4] + "/Dioxido_de_Azufre_SO2";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }
                //////////////////////////////////////////////

                ////---------------------------------------------------------
                //System.Threading.Timer timer27 =
                //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
                //Thread.Sleep(Timeout.Infinite);
                ////---------------------------------------------------------

                ///////////////////////////////////////////////
                //SO2 - Popayan
                //intervalo 24 horas

                if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 1)
                //if (DateTime.Now.Second >= 0 && DateTime.Now.Second < 10) //simula 0:00 - 1:00 con los primeros 10seg de cada min ya que se envia cada 10seg
                {
                value = MuestraSO2();
                sample = feedSimular[1] + "/Dioxido_de_Azufre_SO2";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }
                //////////////////////////////////////////////

                ////---------------------------------------------------------
                //System.Threading.Timer timer28 =
                //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
                //Thread.Sleep(Timeout.Infinite);
                ////---------------------------------------------------------

                ///////////////////////////////////////////////
                //SO2 - Timbío
                //intervalo 24 horas

                if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 1)
                //if (DateTime.Now.Second >= 0 && DateTime.Now.Second < 10) //simula 0:00 - 1:00 con los primeros 10seg de cada min ya que se envia cada 10seg
                {
                value = MuestraSO2();
                sample = feedSimular[2] + "/Dioxido_de_Azufre_SO2";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }
                //////////////////////////////////////////////

                ////---------------------------------------------------------
                //System.Threading.Timer timer29 =
                //    new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 5000);
                //Thread.Sleep(Timeout.Infinite);
                ////---------------------------------------------------------

                ///////////////////////////////////////////////
                //SO2 - Toribío
                //intervalo 24 horas

                if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 1)
                //if (DateTime.Now.Second >= 0 && DateTime.Now.Second < 10) //simula 0:00 - 1:00 con los primeros 10seg de cada min ya que se envia cada 10seg
                {
                value = MuestraSO2();
                sample = feedSimular[3] + "/Dioxido_de_Azufre_SO2";

                EnviarMensajeMQTT(sample, value.ToString("n"));
            }
                //////////////////////////////////////////////

                //----------------------------------------------------------

            //////////////////////////////////////////////
            ///////////////////////////////////////////////
            ///////////////////////////////////////////////
            ///////////////////////////////////////////////
            ///////////////////////////////////////////////
            ///////////////////////////////////////////////
            ///////////////////////////////////////////////
        }
    }



    #region   "Procedimientos de cada una de las mediciones de cada sensor"

    //Procedimineto que calcula un nuevo periodo de mediciones
    static void WaitUntilNextPeriod(int period)
    {
        long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        var offset = (int)(now % period);
        int delay = period - offset;
        Debug.Print("sleep for " + delay + " ms\r\n");
        Thread.Sleep(delay);
    }

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
        Debug.Print("Enviando mensaje MQTT...");

        // create client instance 
        MqttClient client = new MqttClient(MQTT_BROKER_ADDRESS);

        // register to message received 
        client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

        string clientId = Guid.NewGuid().ToString();
        client.Connect(clientId);

        // subscribe to the topic Ej. "/home/temperature" with QoS 2 
        client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
 
        Debug.Print("Mensaje MQTT Enviando");
    }

    private static void EnviarMensajeMQTT(string topic, string payload)
    {
        // create client instance 
        MqttClient client = new MqttClient(MQTT_BROKER_ADDRESS);

        string clientId = Guid.NewGuid().ToString();
        client.Connect(clientId);

        string strValue = Convert.ToString(payload);

        // publish a message on "/home/temperature" topic with QoS 2 
        client.Publish(topic, Encoding.UTF8.GetBytes(strValue));
    }

    static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        // handle message received 
    }
    #endregion
}