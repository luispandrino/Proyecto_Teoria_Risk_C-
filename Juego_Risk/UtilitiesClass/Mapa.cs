using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_Risk.UtilitiesClass
{
    class Mapa
    {
        Pais Aux;
        List<Pais> Lista_Paises = new List<Pais>();
        
        public void Asignar_Paises()
        {
            Lista_Paises.Add(Aux = new Pais("AFganistan", 0, 3, 1, 0, 0, 0));
            Lista_Paises.Add(Aux = new Pais("", 0, 3, 1, 0, 0, 0));

        }
       
      
    }
    
}
