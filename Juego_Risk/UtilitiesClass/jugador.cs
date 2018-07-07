using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_Risk.UtilitiesClass
{
    public class jugador
    {
        public string name;
        public int numerTroops = 10;
        public int numerTeritories = 8;
        public List<int> terrytorios = new List<int>();
    }
}

