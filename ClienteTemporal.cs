using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace borrador_de_tp4
{
    public class ClienteTemporal
    {
        private string estado;
        private double inicioAtencion = 0;
        private int id;
        private int tipoServicio;
        private bool tomaServicio;
        private int nroFilaCliente;

        public int NroFilaCliente
        {
            get { return nroFilaCliente; }
            set { nroFilaCliente = value; }
        }
        
        public string Estado
        {
            get {  return estado; }
            set { estado = value; }
        }

        public double InicioAtencion
        {
            get { return inicioAtencion;}
            set {  inicioAtencion = value; }
        }

        public int Id
        {
            get { return id;}
            set { id = value; }
        }

        public int TipoServicio
        {
            get { return tipoServicio;}
            set { tipoServicio = value;}
        }

        public bool TomaServicio
        {
            get { return tomaServicio;}
            set { tomaServicio = value;}
        }

        public ClienteTemporal(String estado, double inicioAtencion, int id, int tipoServicio, bool tomaServicio, int nroFilaCliente){
            this.estado = estado;
            this.inicioAtencion = inicioAtencion;
            this.id = id;
            this.tipoServicio = tipoServicio;
            this.tomaServicio = tomaServicio;
            this.nroFilaCliente = nroFilaCliente;
        }
    }
}
