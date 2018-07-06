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
        
        public Mapa()
        {
           Lista_Paises.Add(Aux = new Pais("Afganistan", 0, 3, 1, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Africa del Norte", 0, 3, 2, 0, 0,0));
           Lista_Paises.Add(Aux = new Pais("Africa Oriental", 0, 3, 3, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Alaska", 0, 3, 3, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Alberta", 0, 3, 2, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Argentina", 0, 3, 1, 0, 0,0));
           Lista_Paises.Add(Aux = new Pais("Australia Occidental", 0, 3, 1, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Australia Oriental", 0, 3, 1, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Brasil", 0, 3, 2, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Centro America", 0, 3, 1, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("China", 0, 3, 1, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Congo", 0, 3, 1, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Egipto", 0, 3, 2, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Escandinavia", 0, 3, 2, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Este USA", 0, 3, 1, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("EUA Norte", 0, 3, 2, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("EUA Occidental", 0, 3, 2, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("EUA sur", 0, 3, 2, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Gran Bretaña", 0, 3, 3, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Groenlandia", 0, 3, 3, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("India", 0, 3, 2, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Indonesia", 0, 3, 3, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Irktsk", 0, 3, 1, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Islandia", 0, 3, 3, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Japón", 0, 3, 2, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Kamchatka", 0, 3, 3, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Madagascar", 0, 3, 3, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Mongolia", 0, 3, 1, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Nueva Guinea", 0, 3, 2, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Occidental USA", 0, 3, 1, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Ontario", 0, 3, 2, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Oriente Medio", 0, 3, 2, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Perú", 0, 3, 1, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Quebec", 0, 3, 2, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Siam", 0, 3, 2, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Siberia", 0, 3, 1, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Sudáfrica", 0, 3, 2, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Territorio del Noroeste", 0, 3, 2, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Ucrania", 0, 3, 1, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Ural", 0, 3, 1, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Venezuela", 0, 3, 1, 0, 0, 0));
           Lista_Paises.Add(Aux = new Pais("Yakutsk", 0, 3, 1, 0, 0, 0));
        }
       
      
    }
    
}
