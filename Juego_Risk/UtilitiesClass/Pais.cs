using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_Risk.UtilitiesClass
{
    class Pais
    {
        private string Nombre;
        private int Tropas;
        private int Pertenencia;
        private int Imp;
        private int P_Asig;
        private int P_ATK;
        private int P_Fort;
        public Pais (string Nombre_Pais, int Tropas, int Pertenece_a, int Importancia, int Prioridad_Asig, int Prioridad_ATK, int Prioridad_Fort)
        {
            this.Nombre = Nombre_Pais;
            this.Tropas = Tropas;
            this.Pertenencia = Pertenece_a;
            this.Imp = Importancia;
            this.P_Asig = Prioridad_Asig;
            this.P_ATK = Prioridad_ATK;
            this.P_Fort = Prioridad_Fort;
        }
    }
}
