using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_Risk.UtilitiesClass
{
    public class Pais
    {
        // atributos de cada pais
        public string Nombre { get; set; }
        public int Tropas { get; set; }
        // 1 = Jugador; 2 = IA; 3 = neutro
        public int Pertenencia { get; set; }
        public int Imp { get; set; }
        public double P_Asig { get; set; }
        public int P_ATK { get; set; }
        public int P_Fort { get; set; }
        public int Id_Pais { get; set; }
        public bool Orilla { get; set; }
        public List<int> pais_vecinos { get; set; }

        /// <summary>
        /// Metodo que guarda la informacion recibida para cada pais
        /// </summary>
        /// <param name="Nombre_Pais"></param>
        /// <param name="Tropas"></param>
        /// <param name="Pertenece_a"></param>
        /// <param name="Importancia"></param>
        /// <param name="Prioridad_Asig"></param>
        /// <param name="Prioridad_ATK"></param>
        /// <param name="Prioridad_Fort"></param>
        /// <param name="Id"></param>
        /// <param name="_pais_vecinos"></param>
        public void AgregarInfo(string Nombre_Pais, int Tropas, int Pertenece_a, int Importancia, int Prioridad_Asig, int Prioridad_ATK, int Prioridad_Fort, int Id, bool orilla, List<int> _pais_vecinos)
        {
            this.Nombre = Nombre_Pais;
            this.Tropas = Tropas;
            this.Pertenencia = Pertenece_a;
            this.Imp = Importancia;
            this.P_Asig = Prioridad_Asig;
            this.P_ATK = Prioridad_ATK;
            this.P_Fort = Prioridad_Fort;
            this.Id_Pais = Id;
            this.Orilla = orilla;
            this.pais_vecinos = _pais_vecinos;
        }
    }
}
