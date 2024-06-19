using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace borrador_de_tp4
{
    public class RK
    {
        public float h;
        public float x0;
        public float y0;
        public float k1;
        public float k2;
        public float k3;
        public float k4;
        // variable de lista de resultados

        public float funcion(float x, float y)
        {
            return h;
        }

        public float siguienteY()
        {
            return y0 + h / 6 * (k1 + 2*k2 + 2*k3 + k4);
        }
        
        public float ejecutarRK(float parametroCorte)
        {
            float resultado = 0;
            while (resultado < parametroCorte)
            {
                //calculo de la funcion y de las k

                //revalorizacion del resultado

                //calculo de las nuevas x e y
            }
            return resultado;
        }

    }
}
