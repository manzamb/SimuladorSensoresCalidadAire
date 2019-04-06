using System;
using System.Collections.Generic;
using System.Text;

namespace SimuladorSensoresCalidadAire.Logica_Aplicacion
{
    class Medida
    {
        //Campos
        DateTime fechaMedida;
        Double valor;

        public Medida()
        {
        }

        //Propiedades
        public DateTime FechaMedida { get => fechaMedida; set => fechaMedida = value; }
        public double Valor { get => valor; set => valor = value; }

        //Metodos
        //public Medida(DateTime fechaMedida, double valor)
        //{
        //    FechaMedida = fechaMedida;
        //    Valor = valor;
        //}

    }
}
