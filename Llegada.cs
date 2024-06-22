using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace borrador_de_tp4
{
    public class Llegada
    {
        private double tiempoEntreLlegada;
        private double proximaLlegada;
        private int media;

        public double TiempoEntreLlegada
        {
            get { return tiempoEntreLlegada; }
            set { tiempoEntreLlegada = value; }
        }

        public double ProximaLlegada
        {
            get { return proximaLlegada; }
            set { proximaLlegada = value; }
        }

        public int Media
        {
            get { return media; }
            set { media = value; }
        }
    }
}
