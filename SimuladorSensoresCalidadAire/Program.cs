using System;
using System.Threading;

namespace SimuladorSensoresCalidadAire
{
    class Program
    {
        static void Main(string[] args)
        {
            //ContyaminacionAire.CrearMediciones();

            SimularSensoresCA.Simular();
            
            //Pruebas del metodo de muestras se activa solo si se hacen cambios
            //int i=0,j = 0, k=0, l=0, m=0;
            //int numero,cannum = 1000;
            //while (i < cannum)
            //{
            //    numero = Muestra(38, 55, 151, 25, 25, 25, 25);
            //    Console.WriteLine("Aleatorio {0}: {1} \n", i, numero);
            //    if (numero < 38)
            //    {
            //        j++; ;
            //    }
            //    else if (numero >=38 && numero <=55)
            //    {
            //        k++;
            //    }
            //    else if (numero >= 56 && numero <= 150)
            //    {
            //        l++;
            //    }
            //    else if (numero >= 151)
            //    {
            //        m++;
            //    }
            //    //Thread.Sleep(1000);
            //    i++;
            //}
            //int prob = j * 100 / cannum;
            //Console.WriteLine("probabilidad Emergencia: {0} \n", m * 100 / cannum);
            //Console.WriteLine("probabilidad Alerta: {0} \n", l * 100 / cannum);
            //Console.WriteLine("probabilidad Prevencion: {0} \n", k * 100 / cannum);
            //Console.WriteLine("probabilidad Normal: {0} \n", j * 100 / cannum);
            //Console.ReadKey();
        }
    }
}
