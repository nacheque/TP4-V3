﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace borrador_de_tp4
{
    public class RK
    {
        public float h = 0.001f;
        public float x;
        public float y;
        public float k1;
        public float k2;
        public float k3;
        public float k4;
        //Variable de lista de resultados

        //Ejecuta la funcion de la ED
        public float funcion(float x, float y)
        {
            return 0.025f*x - 0.5f*y - 12.85f;
        }

        //Calcula la y(i+1) para la siguiente iteración
        public float siguienteY()
        {
            return y + h / 6 * (k1 + 2*k2 + 2*k3 + k4);
        }
        
        //Funcion que ejecuta el RK hasta que y (c) sea menor a 0 y devuelve la x (tiempo t) en segundos
        public float ejecutarRK(float parametroCorte, float x0, float y0)
        {
            x = x0;
            y = y0;

            while (y >= 0)
            {
                //calculo de las k
                k1 = funcion(x, y);
                k2 = funcion(x + h/2, y + k1*h/2);
                k3 = funcion(x + h/2, y +k2*h/2);
                k4 = funcion(x + h/2, y + k3*h);

                //calculo de las nuevas x e y
                x = x + h;
                y = siguienteY();
            }

            //devuelve el tiempo t en el que vuelve la luz (en segundos)
            return x*30;
        }

    }
}
