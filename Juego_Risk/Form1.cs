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
        Mapa Prueba = new Mapa();
        bool Agregar = true;

        public Form1()
        {
          
            InitializeComponent();
        }
        private void Btn_Groenlandia_Click(object sender, EventArgs e)
        {
           

        }

        private void Btn_Empezar_Click(object sender, EventArgs e)
        {
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
            
            
        }
    }
}
