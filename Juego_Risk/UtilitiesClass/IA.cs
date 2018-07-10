using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.MachineLearning;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using Accord.Math;
using Accord.Statistics.Filters;

namespace Juego_Risk.UtilitiesClass
{
    public class IA
    {
        /* Attributes */

        /* Path's of Files Data Training*/
        private string path_file_data_assignment;
        private string path_file_data_attack;
        private string path_file_data_reinforcement;

        /* Variables of new Data*/
        private string newDataAssignment;
        private string newDataAttack;
        private string newDataReinforcement;

        /* IA's teacher's */
        private C45Learning teacherAssignment;
        private C45Learning teacherAttack;
        private C45Learning teacherReinforcement;

        /* Decision Trees */
        private DecisionTree treeAssignment;
        private DecisionTree treeAttack;
        private DecisionTree treeReinforcement;

        /* Coodebooks for Trees*/
        private Codification codebookAssignment;
        private Codification codebookAttack;
        private Codification codebookReinforcement;

        /*Singleton of Map*/
        // Map world;
        Mapa world;

        /* Variables */
        string data = "";

        /* Colas auxiliares*/
        public Queue<string> Attacks;
        public Queue<int> Assignments;
        public Queue<string> Reinforcements;

        /* Builder */
        public IA()
        {
            /* teachers training*/
            this.teacherAssignment = new C45Learning();
            this.teacherAttack = new C45Learning();
            this.teacherReinforcement = new C45Learning();

            //paths of files data
            this.path_file_data_assignment = "training-data-assignment.csv";
            this.path_file_data_attack = "training-data-attack.csv";
            this.path_file_data_reinforcement = "training-data-reinforcement.csv";

            /* Training decision trees*/
            TrainingTreeAssignment();
            //TrainingTreeAttack();
            //TrainingTreeReinforcement();

            /* Init variables*/
            this.newDataAssignment = string.Empty;
            this.newDataAttack = string.Empty;
            this.newDataReinforcement = string.Empty;

            /* Instance Singleton*/
            this.world = Singleton.Instance.map;

            /*Init Queues*/
            Attacks = new Queue<string>();
            Assignments = new Queue<int>();
            Reinforcements = new Queue<string>();
        }

        /* Private Classes*/
        private void TrainingTreeAssignment()
        {
            string content = string.Empty;

            /* Read all content of file */
            using (StreamReader sr = new StreamReader(path_file_data_assignment))
            {
                content = sr.ReadToEnd();
                sr.Close();
            }

            /* Get data in array string */
            string[][] data_training = content.Split(new[] { "\r\n" },
                StringSplitOptions.RemoveEmptyEntries).Apply(x => x.Split(';'));

            /*Separate data and labels*/

            //X_TRAIN
            double[][] X_train = data_training.GetColumns(0, 1, 2, 3, 4, 5, 6, 7).To<double[][]>();

            //Y_TRAIN
            string[] labels = data_training.GetColumn(8);

            this.codebookAssignment = new Codification("Output", labels);
            // With the codebook, we can convert the labels:
            int[] Y_train = this.codebookAssignment.Translate("Output", labels);

            /* Learn a decision tree for the XOR problem */
            this.treeAssignment = this.teacherAssignment.Learn(X_train, Y_train);
        }

        private void TrainingTreeAttack()
        {
            string content = string.Empty;

            /* Read all content of file */
            using (StreamReader sr = new StreamReader(path_file_data_attack))
            {
                content = sr.ReadToEnd();
                sr.Close();
            }

            /* Get data in array string */
            string[][] data_training = content.Split(new[] { "\r\n" },
                StringSplitOptions.RemoveEmptyEntries).Apply(x => x.Split(';'));

            /*Separate data and labels*/

            //X_TRAIN
            double[][] X_train = data_training.GetColumns(0, 1, 2, 3, 4, 5, 6, 7).To<double[][]>();

            //Y_TRAIN
            string[] labels = data_training.GetColumn(8);

            this.codebookAttack = new Codification("Output", labels);
            // With the codebook, we can convert the labels:
            int[] Y_train = this.codebookAttack.Translate("Output", labels);

            /* Learn a decision tree for the XOR problem */
            this.treeAttack = this.teacherAttack.Learn(X_train, Y_train);
        }

        private void TrainingTreeReinforcement()
        {
            string content = string.Empty;

            /* Read all content of file */
            using (StreamReader sr = new StreamReader(path_file_data_reinforcement))
            {
                content = sr.ReadToEnd();
                sr.Close();
            }

            /* Get data in array string */
            string[][] data_training = content.Split(new[] { "\r\n" },
                StringSplitOptions.RemoveEmptyEntries).Apply(x => x.Split(';'));

            /*Separate data and labels*/

            //X_TRAIN
            double[][] X_train = data_training.GetColumns(0, 1, 2, 3, 4, 5, 6, 7).To<double[][]>();

            //Y_TRAIN
            string[] labels = data_training.GetColumn(8);

            this.codebookReinforcement = new Codification("Output", labels);
            // With the codebook, we can convert the labels:
            int[] Y_train = this.codebookReinforcement.Translate("Output", labels);

            /* Learn a decision tree for the XOR problem */
            this.treeReinforcement = this.teacherReinforcement.Learn(X_train, Y_train);
        }


        /* Prediction Functions, only one Country o Territory  */
        private string PredictAssignmentCountry(string data)
        {
            /* Example variable data: "13;2;0;1;3;8;6;0.5" */
            var aux = data.Split(';');
            double[] query = new double[aux.Length];

            for (int i = 0; i < query.Length; i++)
            {
                query[i] = double.Parse(aux[i]);
            }

            int predicted = this.treeAssignment.Decide(query);
            string answer = this.codebookAssignment.Revert("Output", predicted);

            /* Save data if IA win the game */
            this.newDataAssignment += data + ";" + answer + "\n";

            return answer;
        }

        private string PredictAttackCountry(string data)
        {
            /* Example variable data: "13;2;0;1;3;8" */
            var aux = data.Split(';');
            double[] query = new double[aux.Length];

            for (int i = 0; i < query.Length; i++)
            {
                query[i] = double.Parse(aux[i]);
            }

            int predicted = this.treeAttack.Decide(query);
            string answer = this.codebookAttack.Revert("Output", predicted);

            /* Save data if IA win the game */
            this.newDataAttack += data + ";" + answer + "\n";

            return answer;
        }

        private string PredictReinforcementCountry(string data)
        {
            /* Example variable data: "13;2;0;1;3;8;0.6" */
            var aux = data.Split(';');
            double[] query = new double[aux.Length];

            for (int i = 0; i < query.Length; i++)
            {
                query[i] = double.Parse(aux[i]);
            }

            int predicted = this.treeReinforcement.Decide(query);
            string answer = this.codebookReinforcement.Revert("Output", predicted);

            /* Save data if IA win the game */
            this.newDataReinforcement += data + ";" + answer + "\n";

            return answer;
        }

        /* Public Classes */
        public void AutoTraining(bool winner)
        {
            if (winner)
            {
                //Add new  data for phase of assignment
                using (StreamWriter sw = File.AppendText(path_file_data_assignment))
                {
                    sw.Write(newDataAssignment);
                    sw.Close();
                }

                //Add new data for phase of attack
                using (StreamWriter sw = File.AppendText(path_file_data_attack))
                {
                    sw.Write(newDataAttack);
                    sw.Close();
                }

                //Add new data for phase of reinforcement
                using (StreamWriter sw = File.AppendText(path_file_data_reinforcement))
                {
                    sw.Write(newDataReinforcement);
                    sw.Close();
                }

            }
        }

        /* Prediction Functions, All Countries o Territories*/
        private void PredictAllAssignment(int[] territoriesAlly)
        {
            foreach (var country in territoriesAlly)
            {
                data = world.Lista_Paises[country - 1].Id_Pais.ToString() + ";";
                data += world.Lista_Paises[country - 1].Imp.ToString() + ";";
                data += numero_Enemigos(country - 1) + ";";
                data += numero_neutros(country - 1) + ";";
                data += (world.Lista_Paises[country - 1].pais_vecinos.Count - numero_Enemigos(country - 1) - numero_neutros(country - 1) + ";");
                data += world.Lista_Paises[country - 1].Tropas.ToString() + ";";
                data += world.Jugador.Count + ";";
                data += 0.05 * world.Lista_Paises[country - 1].Imp + FA(numero_Enemigos(country - 1), (world.Lista_Paises[country - 1].pais_vecinos.Count - numero_Enemigos(country - 1) - numero_neutros(country - 1)));

                world.Lista_Paises[country - 1].P_Asig = Convert.ToDouble(PredictAssignmentCountry(data));
            }

        }

        private void PredictAllAttacks(int[] territoriesEnemy)
        {
            foreach (var country in territoriesEnemy)
            {
                data = world.Lista_Paises[country].Id_Pais.ToString() + ",";
                data += world.Lista_Paises[country].Tropas.ToString() + ",";
                data += world.Lista_Paises[country].Pertenencia.ToString() + ",";
                data += world.Jugador.Count.ToString() + ",";
                data += world.Lista_Paises[country].Imp.ToString() + ",";
                data += (world.Lista_Paises.Count - world.IA.Count - world.Jugador.Count).ToString();

                PredictAttackCountry(data);
            }

        }

        private void PredictAllReinforcement(int[] territoriesAlly)
        {
            foreach (var country in territoriesAlly)
            {
                data = world.Lista_Paises[country].Id_Pais.ToString() + ",";
                data += world.IA.Count.ToString() + ",";
                data += world.Lista_Paises[country].Tropas.ToString() + ",";
                data += world.Jugador.Count.ToString() + ",";
                data += world.Lista_Paises[country].Imp.ToString() + ",";
                data += (world.Lista_Paises.Count - world.IA.Count - world.Jugador.Count).ToString();
                data += ThreatFactor(country);

                PredictReinforcementCountry(data);
            }


        }


        /* Actions of IA*/

        public void Assignment()
        {
            PredictAllAssignment(world.IA.ToArray());
            int ter = world.Jugador.Count;
            int tropas = world.tropaAsigamiento;
            int cantidad = Convert.ToInt16(ter * 0.33);
            double suma = 0;
            int t = tropas;
            List<Pais> paises_escojer = world.Lista_Paises;
            paises_escojer = paises_escojer.OrderByDescending(p => p.P_Asig).ToList();
            for (int i = 0; i < cantidad; i++)
            {
                suma += paises_escojer[i].P_Asig;

            }
            for (int i = 0; i < cantidad; i++)
            {
                int valor = Convert.ToInt32(t * paises_escojer[i].P_Asig / suma);
                world.Lista_Paises[paises_escojer[i].Id_Pais - 1].Tropas += valor;
                Assignments.Enqueue(paises_escojer[i].Id_Pais);
            }

        }

        public string Attack()
        {
            return string.Empty;
        }

        public void Reinforcement()
        {
            int cont = 0, aux = 0;
            foreach (var item in world.Lista_Paises)
            {
                if (world.Lista_Paises[cont].Id_Pais == world.IA[cont])
                {
                    if (world.Lista_Paises[cont].P_Fort >= 0.8)
                    {
                        if (world.Lista_Paises[cont].Tropas > 3 && world.Lista_Paises[cont].Imp != 3)
                        {
                            aux = world.Lista_Paises[cont].Tropas - 3;
                            world.Lista_Paises[cont].Tropas = 3;
                        }
                    }
                    else if (world.Lista_Paises[cont].P_Fort <= 0.2)
                    {
                        world.Lista_Paises[cont].Tropas += aux;
                    }

                }

                cont++;

            }

        }

        public double ThreatFactor(int count)
        {
            double result = 0;
            for (int i = 0; i < 42; i++)
            {
                if (world.Lista_Paises[i].Id_Pais == world.IA[count] && world.Lista_Paises[i].Imp == 1)
                {
                    result = 0.05;
                }
                else if (world.Lista_Paises[i].Id_Pais == world.IA[count] && world.Lista_Paises[i].Imp == 2)
                    result = 0.10;
                else
                    result = 0.15;
                if ((world.Lista_Paises[i].Id_Pais == world.Jugador[count] && world.Lista_Paises[i].Id_Pais == world.IA[count]) && ((world.Lista_Paises[i].Tropas - world.Lista_Paises[i].Tropas) < -5))
                    result += 0.05;
                else if ((world.Lista_Paises[i].Id_Pais == world.Jugador[count] && world.Lista_Paises[i].Id_Pais == world.IA[count] && (world.Lista_Paises[i].Tropas - world.Lista_Paises[i].Tropas) < -2) && (world.Lista_Paises[i].Id_Pais == world.Jugador[count] && world.Lista_Paises[i].Id_Pais == world.IA[count] && (world.Lista_Paises[i].Tropas - world.Lista_Paises[i].Tropas) > -5))
                    result += 0.10;
                else if ((world.Lista_Paises[i].Id_Pais == world.Jugador[count] && world.Lista_Paises[i].Id_Pais == world.IA[count]) && ((world.Lista_Paises[i].Tropas - world.Lista_Paises[i].Tropas) < 0) && (world.Lista_Paises[i].Id_Pais == world.Jugador[count] && world.Lista_Paises[i].Id_Pais == world.IA[count]) && ((world.Lista_Paises[i].Tropas - world.Lista_Paises[i].Tropas) > 2))
                    result += 0.20;
                else if ((world.Lista_Paises[i].Id_Pais == world.Jugador[count] && world.Lista_Paises[i].Id_Pais == world.IA[count]) && ((world.Lista_Paises[i].Tropas - world.Lista_Paises[i].Tropas) == 0))
                    result += 0;
                else if ((world.Lista_Paises[i].Id_Pais == world.Jugador[count] && world.Lista_Paises[i].Id_Pais == world.IA[count]) && ((world.Lista_Paises[i].Tropas - world.Lista_Paises[i].Tropas) <= 5) && (world.Lista_Paises[i].Id_Pais == world.Jugador[count] && world.Lista_Paises[i].Id_Pais == world.IA[count]) && ((world.Lista_Paises[i].Tropas - world.Lista_Paises[i].Tropas) >= 1))
                    result += 0.30;
                else if ((world.Lista_Paises[i].Id_Pais == world.Jugador[count] && world.Lista_Paises[i].Id_Pais == world.IA[count]) && ((world.Lista_Paises[i].Tropas - world.Lista_Paises[i].Tropas) <= 10) && (world.Lista_Paises[i].Id_Pais == world.Jugador[count] && world.Lista_Paises[i].Id_Pais == world.IA[count]) && ((world.Lista_Paises[i].Tropas - world.Lista_Paises[i].Tropas) >= 6))
                    result += 0.40;
                else if ((world.Lista_Paises[i].Id_Pais == world.Jugador[count] && world.Lista_Paises[i].Id_Pais == world.IA[count]) && ((world.Lista_Paises[i].Tropas - world.Lista_Paises[i].Tropas) > 11))
                    result += 0.50;

            }
            return result;

        }

        public double BenefitFactor(int count)
        {
            double result = 0;
            for (int i = 0; i < 42; i++)
            {
                if (world.Lista_Paises[i].Id_Pais == world.Jugador[count] && world.Lista_Paises[i].Imp == 1)
                {
                    result = 0.05;
                }
                else if (world.Lista_Paises[i].Id_Pais == world.Jugador[count] && world.Lista_Paises[i].Imp == 2)
                    result = 0.10;
                else
                    result = 0.15;
                if ((world.Lista_Paises[i].Id_Pais == world.Jugador[count] && world.Lista_Paises[i].Id_Pais == world.IA[count]) && ((world.Lista_Paises[i].Tropas - world.Lista_Paises[i].Tropas) < -5))
                    result += 0.05;
                else if ((world.Lista_Paises[i].Id_Pais == world.Jugador[count] && world.Lista_Paises[i].Id_Pais == world.IA[count] && (world.Lista_Paises[i].Tropas - world.Lista_Paises[i].Tropas) < -2) && (world.Lista_Paises[i].Id_Pais == world.Jugador[count] && world.Lista_Paises[i].Id_Pais == world.IA[count] && (world.Lista_Paises[i].Tropas - world.Lista_Paises[i].Tropas) > -5))
                    result += 0.10;
                else if ((world.Lista_Paises[i].Id_Pais == world.Jugador[count] && world.Lista_Paises[i].Id_Pais == world.IA[count]) && ((world.Lista_Paises[i].Tropas - world.Lista_Paises[i].Tropas) < 0) && (world.Lista_Paises[i].Id_Pais == world.Jugador[count] && world.Lista_Paises[i].Id_Pais == world.IA[count]) && ((world.Lista_Paises[i].Tropas - world.Lista_Paises[i].Tropas) > 2))
                    result += 0.20;
                else if ((world.Lista_Paises[i].Id_Pais == world.Jugador[count] && world.Lista_Paises[i].Id_Pais == world.IA[count]) && ((world.Lista_Paises[i].Tropas - world.Lista_Paises[i].Tropas) == 0))
                    result += 0;
                else if ((world.Lista_Paises[i].Id_Pais == world.Jugador[count] && world.Lista_Paises[i].Id_Pais == world.IA[count]) && ((world.Lista_Paises[i].Tropas - world.Lista_Paises[i].Tropas) <= 5) && (world.Lista_Paises[i].Id_Pais == world.Jugador[count] && world.Lista_Paises[i].Id_Pais == world.IA[count]) && ((world.Lista_Paises[i].Tropas - world.Lista_Paises[i].Tropas) >= 1))
                    result += 0.30;
                else if ((world.Lista_Paises[i].Id_Pais == world.Jugador[count] && world.Lista_Paises[i].Id_Pais == world.IA[count]) && ((world.Lista_Paises[i].Tropas - world.Lista_Paises[i].Tropas) <= 10) && (world.Lista_Paises[i].Id_Pais == world.Jugador[count] && world.Lista_Paises[i].Id_Pais == world.IA[count]) && ((world.Lista_Paises[i].Tropas - world.Lista_Paises[i].Tropas) >= 6))
                    result += 0.40;
                else if ((world.Lista_Paises[i].Id_Pais == world.Jugador[count] && world.Lista_Paises[i].Id_Pais == world.IA[count]) && ((world.Lista_Paises[i].Tropas - world.Lista_Paises[i].Tropas) > 11))
                    result += 0.50;

            }
            return result;
        }
        public int numero_Enemigos(int id)
        {
            List<int> vecinos = world.Lista_Paises[id].pais_vecinos;
            int count = 0;
            for (int i = 0; i < vecinos.Count; i++)
            {

                if (world.Lista_Paises[vecinos[i] - 1].Pertenencia == 2)
                {
                    count++;
                }
            }
            return count;
        }
        public int numero_neutros(int id)
        {
            List<int> vecinos = world.Lista_Paises[id].pais_vecinos;
            int count = 0;
            for (int i = 0; i < vecinos.Count; i++)
            {

                if (world.Lista_Paises[vecinos[i] - 1].Pertenencia == 3)
                {
                    count++;
                }
            }
            return count;
        }
        public double FA(int enemigos, int aliados)
        {
            int aux = enemigos - aliados;
            double valor = 0;
            if (aux < -5)
            {
                valor = 0.05;
            }
            else if (aux >= -5 & aux < -2)
            {
                valor = 0.11;
            }
            else if (aux >= -2 & aux < 0)
            {
                valor = 0.2;
            }
            else if (aux >= 0 & aux < 6)
            {
                valor = 0.3;

            }
            else if (aux >= 6 & aux < 11)
            {
                valor = 0.4;
            }
            else if (aux >= 11)
            {
                valor = 0.5;
            }
            return valor;
        }


    }
}
