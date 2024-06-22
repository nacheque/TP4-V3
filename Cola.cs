using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace borrador_de_tp4
{
    public class Cola
    {
        private List<ClienteTemporal> cantidad;
        private double tiempoEspera = 0;
        //Porcentaje Tiempo Fuera
        private double prcTiempoFuera;

        public List<ClienteTemporal> Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        public double TiempoEspera
        {
            get { return tiempoEspera; }
            set {  tiempoEspera = value; }
        }

        public double PrcTiempoFuera
        {
            get { return prcTiempoFuera; }
            set { prcTiempoFuera = value; }
        }
    }
}
