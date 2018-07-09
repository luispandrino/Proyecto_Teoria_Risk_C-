using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Juego_Risk.UtilitiesClass
{
    public class Mapa
    {
        // objetos de los 42 paises del mapa
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

        // lista que contiene cada uno de los paises y sus datos
        public List<Pais> Lista_Paises = new List<Pais>();
        /// <summary>
        /// Constructor clase mapa
        /// </summary>
        public Mapa()
        {
            //se ingresa la informacion de cada pais
            Afganistan.AgregarInfo("Afganistan", 0, 3, 1, 0, 0, 0, 1, false, new List<int> { 32, 21, 11, 40, 39 });
            Africa_del_Norte.AgregarInfo("Africa del Norte", 0, 3, 2, 0, 0, 0, 2, false, new List<int> { 9, 12, 3, 13, 18, 17 });
            Africa_Oriental.AgregarInfo("Africa Oriental", 0, 3, 3, 0, 0, 0, 3, false, new List<int> { 13, 2, 12, 37, 27, 32 });
            Alaska.AgregarInfo("Alaska", 0, 3, 3, 0, 0, 0, 4, true, new List<int> { 38, 5, 26 });
            Alberta.AgregarInfo("Alberta", 0, 3, 2, 0, 0, 0, 5, false, new List<int> { 4, 38, 37, 30 });
            Argentina.AgregarInfo("Argentina", 0, 3, 1, 0, 0, 0, 6, false, new List<int> { 33, 9 });
            Australia_Occidental.AgregarInfo("Australia Occidental", 0, 3, 1, 0, 0, 0, 7, true, new List<int> { 22, 8, 29 });
            Australia_Oriental.AgregarInfo("Australia Oriental", 0, 3, 1, 0, 0, 0, 8, true, new List<int> { 7, 29 });
            Brasil.AgregarInfo("Brasil", 0, 3, 2, 0, 0, 0, 9, true, new List<int> { 2, 41, 33, 6 });
            Centro_America.AgregarInfo("Centro America", 0, 3, 1, 0, 0, 0, 10, false, new List<int> { 41, 30, 15 });
            China.AgregarInfo("China", 0, 3, 1, 0, 0, 0, 11, false, new List<int> { 35, 21, 28, 36, 40, 1 });
            Congo.AgregarInfo("Congo", 0, 3, 1, 0, 0, 0, 12, false, new List<int> { 2, 3, 37 });
            Egipto.AgregarInfo("Egipto", 0, 3, 2, 0, 0, 0, 13, true, new List<int> { 32, 18, 2, 3 });
            Escandinavia.AgregarInfo("Escandinavia", 0, 3, 2, 0, 0, 0, 14, true, new List<int> { 39, 16 });
            Este_USA.AgregarInfo("Este USA", 0, 3, 1, 0, 0, 0, 15, false, new List<int> { 15, 10, 31, 5 });
            EUA_Norte.AgregarInfo("EUA Norte", 0, 3, 2, 0, 0, 0, 16, true, new List<int> { 17, 18, 19, 39 });
            EUA_Occidental.AgregarInfo("EUA Occidental", 0, 3, 2, 0, 0, 0, 17, false, new List<int> { 18, 16, 19, 2 });
            EUA_sur.AgregarInfo("EUA sur", 0, 3, 2, 0, 0, 0, 18, false, new List<int> { 32, 13, 2, 17, 16, 39 });
            Gran_Bretaña.AgregarInfo("Gran Bretaña", 0, 3, 3, 0, 0, 0, 19, true, new List<int> { 17, 24, 16 });
            Groenlandia.AgregarInfo("Groenlandia", 0, 3, 3, 0, 0, 0, 20, true, new List<int> { 38, 31, 34, 24 });
            India.AgregarInfo("India", 0, 3, 2, 0, 0, 0, 21, false, new List<int> { 32, 1, 11, 35 });
            Indonesia.AgregarInfo("Indonesia", 0, 3, 3, 0, 0, 0, 22, true, new List<int> { 7, 29, 35 });
            Irkutsk.AgregarInfo("Irktsk", 0, 3, 1, 0, 0, 0, 23, false, new List<int> { 28, 36, 42, 26 });
            Islanlandia.AgregarInfo("Islandia", 0, 3, 3, 0, 0, 0, 24, true, new List<int> { 19, 20 });
            Japón.AgregarInfo("Japón", 0, 3, 2, 0, 0, 0, 25, true, new List<int> { 26, 28 });
            Kamchatka.AgregarInfo("Kamchatka", 0, 3, 3, 0, 0, 0, 26, true, new List<int> { 28, 23, 42, 4, 25 });
            Madagascar.AgregarInfo("Madagascar", 0, 3, 3, 0, 0, 0, 27, true, new List<int> { 37, 3 });
            Mongolia.AgregarInfo("Mongolia", 0, 3, 1, 0, 0, 0, 28, false, new List<int> { 25, 26, 23, 11, 36 });
            Nueva_Guienea.AgregarInfo("Nueva Guinea", 0, 3, 2, 0, 0, 0, 29, true, new List<int> { 7, 8, 22 });
            Occidental_USA.AgregarInfo("Occidental USA", 0, 3, 1, 0, 0, 0, 30, false, new List<int> { 10, 30, 31, 34 });
            Ontario.AgregarInfo("Ontario", 0, 3, 2, 0, 0, 0, 31, true, new List<int> { 20, 38, 5, 15, 30, 34 });
            Oriente_Medio.AgregarInfo("Oriente Medio", 0, 3, 2, 0, 0, 0, 32, true, new List<int> { 21, 1, 39, 18, 13, 3 });
            Perú.AgregarInfo("Perú", 0, 3, 1, 0, 0, 0, 33, false, new List<int> { 6, 9, 41 });
            Quebec.AgregarInfo("Quebec", 0, 3, 2, 0, 0, 0, 34, true, new List<int> { 20, 15, 31 });
            Siam.AgregarInfo("Siam", 0, 3, 2, 0, 0, 0, 35, true, new List<int> { 22, 11, 21 });
            Siberia.AgregarInfo("Siberia", 0, 3, 1, 0, 0, 0, 36, false, new List<int> { 40, 11, 28, 23, 42 });
            Sudáfrica.AgregarInfo("Sudáfrica", 0, 3, 2, 0, 0, 0, 37, true, new List<int> { 27, 3, 12 });
            Terrirorio_del_Noroeste.AgregarInfo("Territorio del Noroeste", 0, 3, 2, 0, 0, 0, 38, true, new List<int> { 4, 5, 31, 20 });
            Ucrania.AgregarInfo("Ucrania", 0, 3, 1, 0, 0, 0, 39, false, new List<int> { 40, 1, 32, 16, 14, 18 });
            Ural.AgregarInfo("Ural", 0, 3, 1, 0, 0, 0, 40, false, new List<int> { 36, 11, 1, 39 });
            Venezuela.AgregarInfo("Venezuela", 0, 3, 1, 0, 0, 0, 41, false, new List<int> { 9, 33, 10 });
            Yakutsk.AgregarInfo("Yakutsk", 0, 3, 1, 0, 0, 0, 42, false, new List<int> { 36, 23, 26 });
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


    }

}
