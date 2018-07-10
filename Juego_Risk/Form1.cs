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
        IA playerIA = new IA();
        //Diccionario que contiene los botones que representan a cada pais
        Dictionary<int, Button> Listbtn = new Dictionary<int, Button>();
        int fase = 0;
        int jugador =0;
        string nombre;
        int contador = 0;
        int auxtimer = 0;


        public Form1()
        {

            InitializeComponent();
            //variable que guarda el nombre del jugador
            // Ciclo que se encarga de verificar que ingresen un nombre
            do
            {
                nombre = Tablero.Nombre_jugador(Microsoft.VisualBasic.Interaction.InputBox("Ingrese su nombre porfavor :", "Risk", ""));
                lbljugadorname.Text = nombre;
            } while (nombre == "") ;

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

            panel1.Enabled = false;
            lblAsignamiento.Text = "Asignación";
        }
        /// <summary>
        /// Metodo que reparte los territorios iniciales a cada jugador
        /// </summary>
        public void initializer_terrtorios()
        {
            Random rnd = new Random();
            //lista de almacenamiento de los paises al azar 
            List<int> twoplayers = new List<int>();
            int i = 0;
            while (i < 16)
            {

                int ter = rnd.Next(1, 42);
                if (!twoplayers.Exists(t => t == ter))
                {
                    twoplayers.Add(ter);
                    //Asigna los 8 territorios aleatoreos del jugador
                    if (i < 8)
                    {
                        Tablero.Jugador.Add(ter);
                        //color verde es jugador 1
                        Listbtn[ter].BackColor = System.Drawing.Color.Green;
                        Listbtn[ter].Text = "5";
                        Tablero.Lista_Paises[ter-1].Tropas = Convert.ToInt32(Listbtn[ter].Text);
                        Tablero.Lista_Paises[ter-1].Pertenencia = 1;
                        
                    }
                    else
                    {
                        //asigna los 8 territorios al azar de la IA
                        Tablero.IA.Add(ter);
                        Listbtn[ter].BackColor = System.Drawing.Color.Blue;
                        //color azul es IA
                        Listbtn[ter].Text = "5";
                        Tablero.Lista_Paises[ter-1].Tropas = Convert.ToInt32(Listbtn[ter].Text);
                        Tablero.Lista_Paises[ter-1].Pertenencia = 2;
                        
                    }
                    i++;
                }
            }



        }
        //manejador del Evento generico para los 42 botones
        public void Eventos_botones()
        {
            for (int i = 1; i < 43; i++)
            {
                Listbtn[i].Click += new System.EventHandler(this.Evento_Generico);
            }

        }
        //Evento que controla los botones
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
            else
            {
                txtPaisSeleccionado.Text = Id_encontrado + "." + Tablero.Lista_Paises[Id_encontrado - 1].Nombre;
                CB_vecinos.Items.Clear();
                if (Tablero.Jugador.Exists(x => x == Id_encontrado))
                {
                    for (int i = 0; i < Tablero.Lista_Paises[Id_encontrado - 1].pais_vecinos.Count; i++)
                    {
                        for (int j = 0; j < Tablero.Jugador.Count; j++)
                        {
                            if (Tablero.Lista_Paises[Id_encontrado - 1].pais_vecinos[i] == Tablero.Jugador[j])
                            {
                                CB_vecinos.Items.Add(Tablero.Lista_Paises[Id_encontrado - 1].pais_vecinos[i] + "." + Tablero.Lista_Paises[Tablero.Lista_Paises[Id_encontrado - 1].pais_vecinos[i] - 1].Nombre);
                            }
                        }
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

            if (fase == 0)
            {
                fase = 1;
                panel1.Enabled = true;
                lblAsignamiento.Text = "Ataque";
            }
            else if (fase == 1)
            {
                fase = 3;
                panel1.Enabled = true;
                lblAsignamiento.Text = "Reforzamiento";
            }
            else if (fase == 2)
            {
                fase = 0;
                panel1.Enabled = false;
                lblAsignamiento.Text = "Asignación";
            }
            else
            {
                 /*Turno de la IA*/

                Btn_Empezar.Enabled = false;
                panel1.Enabled = false;
                //Ejecuta las acciones de la Inteligencia Artificial
                lbljugadorname.Text = "IA";
                Tablero.tropaAsigamiento = 10;
                lblAsignamiento.Text = "Asignación";
                timer1.Enabled = true;
                /* Inicia jugada */
                //PlayIA();

                //Acción del boton que debe presionar al finalizar el turno
                //

            }
        }

        private void PlayIA()
        {
            /* Avisar en que turno esta la IA*/

            /* Asignment */
            IA_Assignment();

            /* Avisar en que turno esta la IA*/

            /* Attack */
            IA_Attack();

            /* Avisar en que turno esta la IA*/

            /* Reinforcement */
            IA_Reinforcement();

            /* Avisar en que la IA ha terminado su turno*/
        }

        private void IA_Assignment()
        {
            lblAsignamiento.Text = "Asignación";
            playerIA.Assignment();
            var aux = playerIA.Assignments.Count();
            int country = 0;

            for (int i = 0; i < aux; i++)
            {
                /* Debe retornar el id del pais al cual se le asigno el territorio
                * para actualizarlo en el mapa visual, esto lo hace la cola */
                country = playerIA.Assignments.Dequeue();
                RefreshCountries(Tablero.Lista_Paises[country - 1].Id_Pais, Tablero.Lista_Paises[country - 1].Pertenencia, Tablero.Lista_Paises[country - 1].Tropas);
                //Si quieren.. debemos agregar un tiempo de retardo entre cambios
            }
        }

        private void IA_Attack()
        {
            lblAsignamiento.Text = "Ataque";
            //Calculate posibilities attacks
            playerIA.PredictAllAttacks(Tablero.IA);

            //Execute the best's attacks
            playerIA.Attack(Tablero.IA);

            var aux = playerIA.Attacks.Count();

            for (int i = 0; i < aux; i++)
            {
                /* Debo retornar un string de los 2 paises que se ven afectados 
               en el ataque de la forma: [2;5] */
                string countries = playerIA.Attacks.Dequeue();

                int aux1 = int.Parse(countries.Split(';')[0]);
                RefreshCountries(Tablero.Lista_Paises[aux1 - 1].Id_Pais, Tablero.Lista_Paises[aux1 - 1].Pertenencia, Tablero.Lista_Paises[aux1 - 1].Tropas);

                //Tiempo de retardo entre cambios 

                int aux2 = int.Parse(countries.Split(';')[1]);
                RefreshCountries(Tablero.Lista_Paises[aux2 - 1].Id_Pais, Tablero.Lista_Paises[aux2 - 1].Pertenencia, Tablero.Lista_Paises[aux2 - 1].Tropas);

                //Tiempo de retardo entre cambios
            }
        }

        private void IA_Reinforcement()
        {
            lblAsignamiento.Text = "Reforzamiento";
            playerIA.Reinforcement();
            var aux = playerIA.Reinforcements.Count();

            for (int i = 0; i < aux; i++)
            {
                /* Debo retornar un string de los 2 paises que se ven afectados 
               en el ataque de la forma: [2;5] */
                string countries = playerIA.Reinforcements.Dequeue();

                int aux1 = int.Parse(countries.Split(';')[0]);
                RefreshCountries(Tablero.Lista_Paises[aux1].Id_Pais, Tablero.Lista_Paises[aux1].Pertenencia, Tablero.Lista_Paises[aux1].Tropas);

                //Tiempo de retardo entre cambios 

                int aux2 = int.Parse(countries.Split(';')[1]);
                RefreshCountries(Tablero.Lista_Paises[aux2].Id_Pais, Tablero.Lista_Paises[aux2].Pertenencia, Tablero.Lista_Paises[aux2].Tropas);

                //Tiempo de retardo entre cambios
            }
        }

        private void RefreshCountries(int id, int pertenencia, int tropas)
        {
            Listbtn[id].Text = tropas.ToString();

            switch (pertenencia)
            {
                case 1: //Jugador
                    Listbtn[id].BackColor = System.Drawing.Color.Green;
                    break;

                case 2: //IA
                    Listbtn[id].BackColor = System.Drawing.Color.Blue;
                    break;
            }
        }
        private void nUDtropas_ValueChanged(object sender, EventArgs e)
        {

        }

        private void CB_vecinos_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// Boton que corresponde a la acción de ataque
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            int tropasMovida;
            int id_seleccionado;
            int id_opcion;
            if (CB_vecinos.SelectedIndex == -1)
            {
                DialogResult result;

                // Displays the MessageBox.

                result = MessageBox.Show("Selecione un Pais en: Opciones de País", "ERROR");
            }
            else
            {

                tropasMovida = Convert.ToInt32(nUDtropas.Value);
                string[] aux1 = txtPaisSeleccionado.Text.Split('.');
                string[] aux2 = CB_vecinos.SelectedItem.ToString().Split('.');
                id_seleccionado = Convert.ToInt32(aux1[0]);
                id_opcion = Convert.ToInt32(aux2[0]);
                int diferencia;

                if (fase == 1 || fase== 3)
                {
                    diferencia = tropasMovida - Tablero.Lista_Paises[id_opcion - 1].Tropas;
                    if (diferencia >= 2)
                    {
                        Listbtn[id_opcion].BackColor = System.Drawing.Color.Green;


                        Tablero.Lista_Paises[id_seleccionado - 1].Tropas -= tropasMovida;
                        Listbtn[id_seleccionado].Text = Tablero.Lista_Paises[id_seleccionado - 1].Tropas.ToString();
                        if (Tablero.Lista_Paises[id_opcion - 1].Pertenencia == 2)
                        {
                            Tablero.Lista_Paises[id_opcion - 1].Tropas = diferencia;
                        }
                        else
                        {
                            Tablero.Lista_Paises[id_opcion - 1].Tropas += diferencia;
                        }
                        Listbtn[id_opcion].Text = Tablero.Lista_Paises[id_opcion - 1].Tropas.ToString();
                        Tablero.Lista_Paises[id_opcion - 1].Pertenencia = 1;

                    }
                    else
                    {
                        if (diferencia < 0)
                        {
                            Tablero.Lista_Paises[id_seleccionado - 1].Tropas -= tropasMovida;
                            Listbtn[id_seleccionado].Text = Tablero.Lista_Paises[id_seleccionado - 1].Tropas.ToString();
                            Tablero.Lista_Paises[id_opcion - 1].Tropas -= tropasMovida;
                            Listbtn[id_opcion].Text = Tablero.Lista_Paises[id_opcion - 1].Tropas.ToString();
                        }
                        else
                        {
                            Tablero.Lista_Paises[id_seleccionado - 1].Tropas -= tropasMovida;
                            Listbtn[id_seleccionado].Text = Tablero.Lista_Paises[id_seleccionado - 1].Tropas.ToString();
                            if (diferencia == 0)
                            {
                                Tablero.Lista_Paises[id_opcion - 1].Tropas -= tropasMovida;
                            }
                            else if (Tablero.Lista_Paises[id_opcion - 1].Pertenencia == 2 && Tablero.Lista_Paises[id_seleccionado - 1].Tropas >= 0)
                            {
                                Listbtn[id_opcion].BackColor = System.Drawing.Color.Green;
                                int aux = tropasMovida - Tablero.Lista_Paises[id_opcion - 1].Tropas;
                                Tablero.Lista_Paises[id_opcion - 1].Tropas = aux;
                            }
                            else
                            {
                                Tablero.Lista_Paises[id_opcion - 1].Tropas += tropasMovida;
                            }
                            Listbtn[id_opcion].Text = (Tablero.Lista_Paises[id_opcion - 1].Tropas).ToString();
                        }


                    }

                }
                txtPaisSeleccionado.Clear();
                CB_vecinos.Items.Clear();
                nUDtropas.Value = 0;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //var aux = playerIA.Assignments.Count();
            int country = 0;

            if (lblAsignamiento.Text == "Asignación")
            {
                if (contador == 0)
                {
                    playerIA.Assignment();
                    auxtimer = playerIA.Assignments.Count();

                }
                if (contador < auxtimer)
                {
                    country = playerIA.Assignments.Dequeue();
                    RefreshCountries(Tablero.Lista_Paises[country - 1].Id_Pais, Tablero.Lista_Paises[country - 1].Pertenencia, Tablero.Lista_Paises[country - 1].Tropas);
                    //Si quieren.. debemos agregar un tiempo de retardo entre cambios
                    contador++;
                }
                else
                {
                    lblAsignamiento.Text = "Ataque";
                    //Calculate posibilities attacks
                    playerIA.PredictAllAttacks(Tablero.IA);

                    //Execute the best's attacks
                    playerIA.Attack(Tablero.IA);


                    contador = 0;
                    //timer1.Enabled = true;
                    ////Execute the best's attacks
                    //playerIA.Attack(Tablero.IA);

                    auxtimer = playerIA.Attacks.Count();
                }
            }
            else if (lblAsignamiento.Text == "Ataque")
            {
                if (contador == 0)
                {


                }
                if (contador < auxtimer)
                {
                    string countries = playerIA.Attacks.Dequeue();

                    int aux1 = int.Parse(countries.Split(';')[0]);
                    RefreshCountries(Tablero.Lista_Paises[aux1 - 1].Id_Pais, Tablero.Lista_Paises[aux1 - 1].Pertenencia, Tablero.Lista_Paises[aux1 - 1].Tropas);

                    //Tiempo de retardo entre cambios 

                    int aux2 = int.Parse(countries.Split(';')[1]);
                    RefreshCountries(Tablero.Lista_Paises[aux2 - 1].Id_Pais, Tablero.Lista_Paises[aux2 - 1].Pertenencia, Tablero.Lista_Paises[aux2 - 1].Tropas);

                    //Tiempo de retardo entre cambios
                    contador++;
                }
                else
                {
                    lblAsignamiento.Text = "Reforzamiento";
                    playerIA.Reinforcement();
                    contador = 0;

                    auxtimer = playerIA.Reinforcements.Count();
                }
            }
            else if (lblAsignamiento.Text == "Reforzamiento")
            {
                if (contador == 0)
                {


                }
                if (contador < auxtimer)
                {
                    string countries = playerIA.Reinforcements.Dequeue();

                    int aux1 = int.Parse(countries.Split(';')[0]);
                    RefreshCountries(Tablero.Lista_Paises[aux1].Id_Pais, Tablero.Lista_Paises[aux1].Pertenencia, Tablero.Lista_Paises[aux1].Tropas);

                    //Tiempo de retardo entre cambios 

                    int aux2 = int.Parse(countries.Split(';')[1]);
                    RefreshCountries(Tablero.Lista_Paises[aux2].Id_Pais, Tablero.Lista_Paises[aux2].Pertenencia, Tablero.Lista_Paises[aux2].Tropas);

                    //Tiempo de retardo entre cambios
                    contador++;
                }
                else
                {
                    lblAsignamiento.Text = "Asignación";
                    fase = 0;
                    lbljugadorname.Text = Tablero.name;
                    Btn_Empezar.Enabled = true;
                    timer1.Enabled = false;
                }
            }

        }
    }
}
