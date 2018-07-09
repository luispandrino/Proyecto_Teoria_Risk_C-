using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_Risk.UtilitiesClass
{
    public class Pais
    {
        public string Nombre { get; set; }
        public int Tropas { get; set; }
        public int Pertenencia { get; set; }
        public int Imp { get; set; }
        public int P_Asig { get; set; }
        public int P_ATK { get; set; }
        public int P_Fort { get; set; }
        public int Id_Pais { get; set; }
        public bool Orilla { get; set; }
        public List<int> pais_vecinos { get; set; }


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

        public int AsignarNumTropas(int NumTropas)
        {
            Tropas = NumTropas;
            return NumTropas;
        }
        public int AsignarPertenencia(int Adueño)
        {
            Pertenencia = Adueño;
            return Adueño;
        }

    }
}
