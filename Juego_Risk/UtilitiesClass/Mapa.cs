using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Juego_Risk.UtilitiesClass
{
    class Mapa
    {
        int i = 0;
        int x = 0;
        Pais Afganistan = new Pais();
        Pais Africa_del_Norte = new Pais();
        Pais Africa_Oriental = new Pais();
        Pais Alaska = new Pais();
        Pais Alberta = new Pais();
        Pais Argentina = new Pais();
        Pais Australia_Occidental = new Pais();
        Pais Australia_Oriental = new Pais();
        Pais Brasil = new Pais();
        Pais Centro_America = new Pais();
        Pais China = new Pais();
        Pais Congo = new Pais();
        Pais Egipto = new Pais();
        Pais Escandinavia = new Pais();
        Pais Este_USA = new Pais();
        Pais EUA_Norte = new Pais();
        Pais EUA_Occidental = new Pais();
        Pais EUA_sur = new Pais();
        Pais Gran_Bretaña = new Pais();
        Pais Groenlandia = new Pais();
        Pais India = new Pais();
        Pais Indonesia = new Pais();
        Pais Irkutsk = new Pais();
        Pais Islanlandia = new Pais();
        Pais Japón = new Pais();
        Pais Kamchatka = new Pais();
        Pais Madagascar = new Pais();
        Pais Mongolia = new Pais();
        Pais Nueva_Guienea = new Pais();
        Pais Occidental_USA = new Pais();
        Pais Ontario = new Pais();
        Pais Oriente_Medio = new Pais();
        Pais Perú = new Pais();
        Pais Quebec = new Pais();
        Pais Siam = new Pais();
        Pais Siberia = new Pais();
        Pais Sudáfrica = new Pais();
        Pais Terrirorio_del_Noroeste = new Pais();
        Pais Ucrania = new Pais();
        Pais Ural = new Pais();
        Pais Venezuela = new Pais();
        Pais Yakutsk = new Pais();

        List<Pais> Lista_Paises = new List<Pais>();
        List<Pais> Jugador = new List<Pais>();
        List<Pais> IA = new List<Pais>();
        Random Num = new Random();
        public Mapa()
        {
            //se ingresa la informacion de cada pais
            Afganistan.AgregarInfo("Afganistan", 0, 3, 1, 0, 0, 0);
            Africa_del_Norte.AgregarInfo("Africa del Norte", 0, 3, 2, 0, 0, 0);
            Africa_Oriental.AgregarInfo("Africa Oriental", 0, 3, 3, 0, 0, 0);
            Alaska.AgregarInfo("Alaska", 0, 3, 3, 0, 0, 0);
            Alberta.AgregarInfo("Alberta", 0, 3, 2, 0, 0, 0);
            Argentina.AgregarInfo("Argentina", 0, 3, 1, 0, 0, 0);
            Australia_Occidental.AgregarInfo("Australia Occidental", 0, 3, 1, 0, 0, 0);
            Australia_Oriental.AgregarInfo("Australia Oriental", 0, 3, 1, 0, 0, 0);
            Brasil.AgregarInfo("Brasil", 0, 3, 2, 0, 0, 0);
            Centro_America.AgregarInfo("Centro America", 0, 3, 1, 0, 0, 0);
            China.AgregarInfo("China", 0, 3, 1, 0, 0, 0);
            Congo.AgregarInfo("Congo", 0, 3, 1, 0, 0, 0);
            Egipto.AgregarInfo("Egipto", 0, 3, 2, 0, 0, 0);
            Escandinavia.AgregarInfo("Escandinavia", 0, 3, 2, 0, 0, 0);
            Este_USA.AgregarInfo("Este USA", 0, 3, 1, 0, 0, 0);
            EUA_Norte.AgregarInfo("EUA Norte", 0, 3, 2, 0, 0, 0);
            EUA_Occidental.AgregarInfo("EUA Occidental", 0, 3, 2, 0, 0, 0);
            EUA_sur.AgregarInfo("EUA sur", 0, 3, 2, 0, 0, 0);
            Gran_Bretaña.AgregarInfo("Gran Bretaña", 0, 3, 3, 0, 0, 0);
            Groenlandia.AgregarInfo("Groenlandia", 0, 3, 3, 0, 0, 0);
            India.AgregarInfo("India", 0, 3, 2, 0, 0, 0);
            Indonesia.AgregarInfo("Indonesia", 0, 3, 3, 0, 0, 0);
            Irkutsk.AgregarInfo("Irktsk", 0, 3, 1, 0, 0, 0);
            Islanlandia.AgregarInfo("Islandia", 0, 3, 3, 0, 0, 0);
            Japón.AgregarInfo("Japón", 0, 3, 2, 0, 0, 0);
            Kamchatka.AgregarInfo("Kamchatka", 0, 3, 3, 0, 0, 0);
            Madagascar.AgregarInfo("Madagascar", 0, 3, 3, 0, 0, 0);
            Mongolia.AgregarInfo("Mongolia", 0, 3, 1, 0, 0, 0);
            Nueva_Guienea.AgregarInfo("Nueva Guinea", 0, 3, 2, 0, 0, 0);
            Occidental_USA.AgregarInfo("Occidental USA", 0, 3, 1, 0, 0, 0);
            Ontario.AgregarInfo("Ontario", 0, 3, 2, 0, 0, 0);
            Oriente_Medio.AgregarInfo("Oriente Medio", 0, 3, 2, 0, 0, 0);
            Perú.AgregarInfo("Perú", 0, 3, 1, 0, 0, 0);
            Quebec.AgregarInfo("Quebec", 0, 3, 2, 0, 0, 0);
            Siam.AgregarInfo("Siam", 0, 3, 2, 0, 0, 0);
            Siberia.AgregarInfo("Siberia", 0, 3, 1, 0, 0, 0);
            Sudáfrica.AgregarInfo("Sudáfrica", 0, 3, 2, 0, 0, 0);
            Terrirorio_del_Noroeste.AgregarInfo("Territorio del Noroeste", 0, 3, 2, 0, 0, 0);
            Ucrania.AgregarInfo("Ucrania", 0, 3, 1, 0, 0, 0);
            Ural.AgregarInfo("Ural", 0, 3, 1, 0, 0, 0);
            Venezuela.AgregarInfo("Venezuela", 0, 3, 1, 0, 0, 0);
            Yakutsk.AgregarInfo("Yakutsk", 0, 3, 1, 0, 0, 0);
            //Se agregan los paises a la lista principal 
            Lista_Paises.Add(Afganistan);
            Lista_Paises.Add(Afganistan);
            Lista_Paises.Add(Africa_del_Norte);
            Lista_Paises.Add(Africa_Oriental);
            Lista_Paises.Add(Alaska);
            Lista_Paises.Add(Alberta);
            Lista_Paises.Add(Argentina);
            Lista_Paises.Add(Australia_Occidental);
            Lista_Paises.Add(Australia_Oriental);
            Lista_Paises.Add(Brasil);
            Lista_Paises.Add(Centro_America);
            Lista_Paises.Add(China);
            Lista_Paises.Add(Congo);
            Lista_Paises.Add(Egipto);
            Lista_Paises.Add(Escandinavia);
            Lista_Paises.Add(Este_USA);
            Lista_Paises.Add(EUA_Norte);
            Lista_Paises.Add(EUA_Occidental);
            Lista_Paises.Add(EUA_sur);
            Lista_Paises.Add(Gran_Bretaña);
            Lista_Paises.Add(Groenlandia);
            Lista_Paises.Add(India);
            Lista_Paises.Add(Indonesia);
            Lista_Paises.Add(Irkutsk);
            Lista_Paises.Add(Islanlandia);
            Lista_Paises.Add(Japón);
            Lista_Paises.Add(Kamchatka);
            Lista_Paises.Add(Madagascar);
            Lista_Paises.Add(Mongolia);
            Lista_Paises.Add(Nueva_Guienea);
            Lista_Paises.Add(Occidental_USA);
            Lista_Paises.Add(Ontario);
            Lista_Paises.Add(Oriente_Medio);
            Lista_Paises.Add(Perú);
            Lista_Paises.Add(Quebec);
            Lista_Paises.Add(Siam);
            Lista_Paises.Add(Siberia);
            Lista_Paises.Add(Sudáfrica);
            Lista_Paises.Add(Terrirorio_del_Noroeste);
            Lista_Paises.Add(Ucrania);
            Lista_Paises.Add(Ural);
            Lista_Paises.Add(Venezuela);
            Lista_Paises.Add(Yakutsk);


        }

        public string MostrarJugador()
        {
            while( i != 8)
            {
                string nombre =  Jugador[i].Nombre;
                i++;
                return nombre;
                
            }           
            return "ultima Linea";
        }
        public string MostrarIA()
        {
            while (x != 8)
            {
                string nombre = IA[x].Nombre;
                x++;
                return nombre;

            }
            return "ultima Linea";
        }


        public void AsignarJugador()
        {
            for (int i = 0; i < 8; i++)
            {
                Jugador.Add(Lista_Paises[Num.Next(0, 41)]);
            }
        }
        public void AsignarIA()
        {
            for (int i = 0; i < 8; i++)
            {
                IA.Add(Lista_Paises[Num.Next(0, 41)]);
            }
        }


    }
    
}
