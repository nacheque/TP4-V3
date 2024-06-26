using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace borrador_de_tp4
{
    public class FinAtencion
    {
        private double tiempoAtencion = 0.0;
        private double acTiempoAtencion = 0.0;
        private double prcOcupacion = 0.0;
        private List<double> horaFinAtencion;
        private int media = 0;
        private List<ClienteTemporal> cliente;

        
        
        public double TiempoAtencion
        {
            get{ return tiempoAtencion; }
            set{ tiempoAtencion = value; }
        }

        public double ACtiempoAtencion
        {
            get{ return acTiempoAtencion; }
            set{ acTiempoAtencion = value; }
        }

        public double PRCOcupacion
        {
            get{ return prcOcupacion; }
            set{ prcOcupacion = value; }
        }

        public List<double> HoraFinAtencion
        {
            get{ return horaFinAtencion; }
            set{ horaFinAtencion = value; }
        }

        public int Media
        {
            get{ return media; }
            set{ media = value; }
        }

        public List<ClienteTemporal> Cliente
        {
            get{ return cliente; }
            set{ cliente = value; }
        }
    }
}
