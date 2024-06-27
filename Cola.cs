using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace borrador_de_tp4
{
    public class Cola
    {
        private List<ClienteTemporal> clientes;
        private int cantEsperas = 0;
        private double acTiempoEspera = 0;
        private double prmTiempoEspera = 0;
        //Porcentaje Tiempo Fuera
        private double prcTiempoFuera = 0;

        public List<ClienteTemporal> Clientes
        {
            get { return clientes; }
            set { clientes = value; }
        }

        public int CantEsperas
        {
            get { return cantEsperas; }
            set {  cantEsperas = value; }
        }

        public double AcTiempoEspera
        {
            get { return acTiempoEspera; }
            set {  acTiempoEspera = value; }
        }

        public double PrmTiempoEspera
        {
            get { return prmTiempoEspera; }
            set {  prmTiempoEspera = value; }
        }

        public double PrcTiempoFuera
        {
            get { return prcTiempoFuera; }
            set { prcTiempoFuera = value; }
        }
    }
}
