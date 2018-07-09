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
        //Inicialización del mapa
        Mapa Tablero = Singleton.Instance.map;
        //Diccionario que contiene los botones que representan a cada pais
        Dictionary<int, Button> Listbtn = new Dictionary<int, Button>();
        int fase = 0;
        int jugador =0;

        public Form1()
        {

            InitializeComponent();
            //variable que guarda el nombre del jugador
            string nombre = Tablero.Nombre_jugador(Microsoft.VisualBasic.Interaction.InputBox("Ingrese su nombre plox:v :", "Risk", ""));
            lbljugadorname.Text = nombre;
            //Diccionario con los botones que representan a cada pais
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
            Eventos_botones();
            lblASignar.Text ="10";
        }
        /// <summary>
        /// Metodo que reparte los territorios iniciales a cada jugador
        /// </summary>
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
                        Tablero.Jugador.Add(ter);
                        Listbtn[ter].BackColor = System.Drawing.Color.Green;
                        Listbtn[ter].Text = "5";
                        Tablero.Lista_Paises[ter-1].Tropas = Convert.ToInt32(Listbtn[ter].Text);
                        Tablero.Lista_Paises[ter-1].Pertenencia = 1;
                        
                    }
                    else
                    {
                        Tablero.IA.Add(ter);
                        Listbtn[ter].BackColor = System.Drawing.Color.Blue;
                        Listbtn[ter].Text = "5";
                        Tablero.Lista_Paises[ter-1].Tropas = Convert.ToInt32(Listbtn[ter].Text);
                        Tablero.Lista_Paises[ter-1].Pertenencia = 2;
                        
                    }
                    i++;
                }
            }



        }
        public void Eventos_botones()
        {
            for (int i = 1; i < 43; i++)
            {
                Listbtn[i].Click += new System.EventHandler(this.Evento_Generico);
            }

        }

        private void Evento_Generico(object sender, EventArgs e)
        {
            Button Buttonaux = (Button)sender;
            int tropaAsignada=1;
            int Id_encontrado=0;
            int tropaActual;
            for (int i = 1; i < 43; i++)
            {
                if (Buttonaux == Listbtn[i])
                {
                    Id_encontrado = i;

                }
            }
            if (fase==0)
            {
                if (jugador==0)
                {
                    if (Tablero.tropaAsigamiento>0)
                    {
                        if (Tablero.Jugador.Exists(x => x == Id_encontrado))
                        {
                            Tablero.Lista_Paises[Id_encontrado - 1].Tropas++;
                            Tablero.tropaAsigamiento--;
                            Buttonaux.Text = Tablero.Lista_Paises[Id_encontrado - 1].Tropas.ToString();
                            lblASignar.Text = Tablero.tropaAsigamiento.ToString();
                        }

                    }


                }

            }
            else if (fase==1)
            {
                txtPaisSeleccionado.Text = Id_encontrado+"."+Tablero.Lista_Paises[Id_encontrado - 1].Nombre;
                CB_vecinos.Items.Clear();
                if (Tablero.Jugador.Exists(x => x == Id_encontrado))
                {
                    for (int i = 0; i < Tablero.Lista_Paises[Id_encontrado - 1].pais_vecinos.Count; i++)
                    {
                        CB_vecinos.Items.Add(Tablero.Lista_Paises[Id_encontrado - 1].pais_vecinos[i] + "."+Tablero.Lista_Paises[Tablero.Lista_Paises[Id_encontrado - 1].pais_vecinos[i] - 1].Nombre);
                    }
                    nUDtropas.Maximum = Tablero.Lista_Paises[Id_encontrado - 1].Tropas;
                }
               


            }
            
        }
        private void Btn_Groenlandia_Click(object sender, EventArgs e)
        {
        }

        private void Btn_Empezar_Click(object sender, EventArgs e)
        {
            
            if (fase==0)
            {
                fase = 1;
                lblAsignamiento.Text = "Ataque";
            }
            else if (fase == 1)
            {
                fase = 2;
                panel1.Enabled = true;
                lblAsignamiento.Text = "Reforsamiento";
            }
            else
            {
                fase = 0;
                panel1.Enabled = false;
                lblAsignamiento.Text = "Asignamiento";
            }
        }

        private void nUDtropas_ValueChanged(object sender, EventArgs e)
        {

        }

        private void CB_vecinos_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //FAlta Ataque 
            int tropasMovida;
            int id_seleccionado;
            int id_opcion;
            if (CB_vecinos.SelectedIndex == -1)
            {
                DialogResult result;

                // Displays the MessageBox.

                result = MessageBox.Show( "sellecione un Pais en: Opciones de Pais" , "ERROR");
            }
            else
            {
                tropasMovida = Convert.ToInt32(nUDtropas.Value);
                string[] aux1= txtPaisSeleccionado.Text.Split('.');
                string[] aux2 =CB_vecinos.SelectedItem.ToString().Split('.');
                id_seleccionado = Convert.ToInt32(aux1[0]);
                id_opcion = Convert.ToInt32(aux1[0]);

                if (Tablero.Lista_Paises[id_opcion-1].Pertenencia==2)
                {
                    Tablero.Lista_Paises[id_opcion - 1].Tropas = Tablero.Lista_Paises[id_opcion - 1].Tropas - Tablero.Lista_Paises[id_seleccionado - 1].Tropas;
                }
            }
        }
    }
}
