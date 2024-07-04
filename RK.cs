using System;
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
        public List<ResultadoRK> Resultados { get; private set; } = new List<ResultadoRK>();
        //Variable de lista de filas de ejecucion RK
        public List<FilaRK> TablaRK { get; private set; } = new List<FilaRK>();
        public bool tablaSeteada = false;

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
        //Xo = 0, Yo = tiempo de reloj
        public float ejecutarRK(float x0, float y0, int fila)
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

                if (!tablaSeteada)
                {
                    TablaRK.Add(new FilaRK(x, y, k1, k2, k3, k4, siguienteY()));
                }

                //calculo de las nuevas x e y
                x = x + h;
                y = siguienteY();

                
            }

            if (!tablaSeteada)
            {
                tablaSeteada = true;
            } 

            //Guardamos los resultados en la lista de resultados
            Resultados.Add(new ResultadoRK(fila, x, y, x * 30));

            //Devuelve el tiempo t en el que vuelve la luz (en segundos)
            return x*30;
        }

    }

    public class ResultadoRK
    {
        public int Fila { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Tiempo { get; set; }

        public ResultadoRK(int fila, float x, float y, float tiempo)
        {
            Fila = fila;
            X = x;
            Y = y;
            Tiempo = tiempo;
        }
    }

    public class FilaRK
    {
        public float x;
        public float y;
        public float k1;
        public float k2;
        public float k3;
        public float k4;
        public float sigY;

        public FilaRK(float X, float Y, float K1, float K2, float K3, float K4, float siguY)
        {
            x= X;
            y= Y;
            k1 = K1;
            k2 = K2;
            k3 = K3;
            k4 = K4;
            sigY = siguY;
        }

    }
}
