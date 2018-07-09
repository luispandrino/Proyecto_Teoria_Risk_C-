using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Juego_Risk.UtilitiesClass;


namespace Juego_Risk
{
    
    public partial class Form1 : Form
    {
        Mapa Tablero = new Mapa();
        bool Agregar = true;
        jugador j1 = new jugador();
        jugador j2 = new jugador();
        int ja;
        
        Dictionary<int, Button> Listbtn = new Dictionary<int, Button>();

        public Form1()
        {

            InitializeComponent();
            
            j1.name = Microsoft.VisualBasic.Interaction.InputBox("Ingrese su nombre plox:v :", "Risk", "");
            lbljugadorname.Text = j1.name;
            Listbtn.Add(1, Btn_Afganistan);
            Listbtn.Add(2, Btn_AfricaN);
            Listbtn.Add(3, Btn_AfricaOriente);
            Listbtn.Add(4, Btn_Alaska);
            Listbtn.Add(5, Btn_Alberta);
            Listbtn.Add(6, Btn_Argentina);
            Listbtn.Add(7, Btn_AustraliaOccidental);
            Listbtn.Add(8, Btn_AustraliaOriental);
            Listbtn.Add(9, Btn_Brazil);
            Listbtn.Add(10, Btn_CA);
            Listbtn.Add(11, Btn_China);
            Listbtn.Add(12, Btn_Congo);
            Listbtn.Add(13, Btn_Egipto);
            Listbtn.Add(14, Btn_Escandinavia);
            Listbtn.Add(15, Btn_USAEste);
            Listbtn.Add(16, Btn_EUANorte);
            Listbtn.Add(17, Btn_EUAOccidental);
            Listbtn.Add(18, Btn_EUASur);
            Listbtn.Add(19, Btn_GranBretaña);
            Listbtn.Add(20, Btn_Groenlandia);
            Listbtn.Add(21, Btn_India);
            Listbtn.Add(22, Btn_Indonecia);
            Listbtn.Add(23, Btn_Irkutsk);
            Listbtn.Add(24, Btn_Islandia);
            Listbtn.Add(25, Btn_Japon);
            Listbtn.Add(26, Kamchatka);
            Listbtn.Add(27, Btn_Madagascar);
            Listbtn.Add(28, Btn_Mongolia);
            Listbtn.Add(29, Btn_NuevaGuinea);
            Listbtn.Add(30, Btn_USAOccidental);
            Listbtn.Add(31, Btn_Ontario);
            Listbtn.Add(32, Btn_OrienteMed);
            Listbtn.Add(33, Btn_Peru);
            Listbtn.Add(34, Btn_Quebec);
            Listbtn.Add(35, Btn_Siam);
            Listbtn.Add(36, Btn_Siberia);
            Listbtn.Add(37, Btn_Sudafrica);
            Listbtn.Add(38, Btn_TNorte);
            Listbtn.Add(39, Btn_Ucrania);
            Listbtn.Add(40, Btn_Ural);
            Listbtn.Add(41, Btn_Venezuela);
            Listbtn.Add(42, Btn_Yakutks);
            initializer_terrtorios();
        }
        //
        //

        public void initializer_terrtorios()
        {
            Random rnd = new Random();
            List<int> twoplayers = new List<int>();
            int i = 0;
            while (i < 16)
            {

                int ter = rnd.Next(1, 42);
                if (!twoplayers.Exists(t => t == ter))
                {
                    twoplayers.Add(ter);

                    if (i < 8)
                    {
                        j1.terrytorios.Add(ter);
                        Listbtn[ter].BackColor = System.Drawing.Color.Green;
                        Listbtn[ter].Text = "5";
                        Tablero.Lista_Paises[ter].Tropas = Convert.ToInt32(Listbtn[ter].Text);
                        Tablero.Lista_Paises[ter].Pertenencia = 1;
                    }
                    else
                    {
                        j2.terrytorios.Add(ter);
                        Listbtn[ter].BackColor = System.Drawing.Color.Blue;
                        Listbtn[ter].Text = "5";
                        Tablero.Lista_Paises[ter].Tropas = Convert.ToInt32(Listbtn[ter].Text);
                        Tablero.Lista_Paises[ter].Pertenencia = 2;
                    }
                    i++;
                }
            }



        }
        private void Btn_Groenlandia_Click(object sender, EventArgs e)
        {
        }

        private void Btn_Empezar_Click(object sender, EventArgs e)
        {
            /*
            Prueba.AsignarJugador();
            Prueba.AsignarIA();
            if (Agregar)
            {
                for (int i = 0; i < 8; i++)
                {
                    CB_jugador.Items.Add(Prueba.MostrarJugador());
                }
                for (int i = 0; i < 8; i++)
                {
                    CB_IA.Items.Add(Prueba.MostrarIA());
                }
                Agregar = false;
            }
            
            */
        }
    }
}
