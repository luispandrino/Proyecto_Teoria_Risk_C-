﻿using System;
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
            TrainingTreeAttack();
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
            double[][] X_train = data_training.GetColumns(0, 1, 2, 3, 4, 5).To<double[][]>();

            //Y_TRAIN
            string[] labels = data_training.GetColumn(6);

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

        private string PredictAttack(string data)
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
        public void AutoTraining()
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

        public void PredictAllAttacks(List<int> territoriesAlly)
        {
            List<int> territoriesEnemy = new List<int>();

            //Agrega paises neutros y enemigos a la lista
            foreach (var country in world.Lista_Paises)
            {
                if (!territoriesAlly.Contains(country.Id_Pais))
                {
                    territoriesEnemy.Add(country.Id_Pais);
                }
            }

            //Analisis para cada territorio enemigo
            foreach (var country in territoriesEnemy)
            {
                int neutros = 0;
                int enemigos = 0;
                foreach (var element in world.Lista_Paises[country - 1].pais_vecinos)
                {
                    switch (world.Lista_Paises[element - 1].Pertenencia)
                    {
                        case 1: enemigos++; break;
                        case 3: neutros++; break;
                    }
                }

                data = world.Lista_Paises[country - 1].Id_Pais.ToString() + ";";
                data += world.Lista_Paises[country - 1].Tropas.ToString() + ";";
                data += (world.Lista_Paises[country - 1].Pertenencia == 3) ? "0.5;" : "1;";
                data += enemigos.ToString() + ";";
                data += world.Lista_Paises[country - 1].Imp.ToString() + ";";
                data += neutros.ToString();

                //Prediction

                world.Lista_Paises[country - 1].P_ATK = double.Parse(PredictAttackCountry(data));
            }

        }

        public void PredictEnemyAttacks(List<int> territoriesAlly)
        {
            List<int> territoriesEnemy = new List<int>();

            //Agrega paises neutros y enemigos a la lista
            foreach (var country in world.Lista_Paises)
            {
                if (!territoriesAlly.Contains(country.Id_Pais))
                {
                    territoriesEnemy.Add(country.Id_Pais);
                }
            }

            //Analisis para cada territorio enemigo
            foreach (var country in territoriesEnemy)
            {
                int neutros = 0;
                int enemigos = 0;
                foreach (var element in world.Lista_Paises[country - 1].pais_vecinos)
                {
                    switch (world.Lista_Paises[element - 1].Pertenencia)
                    {
                        case 1: enemigos++; break;
                        case 3: neutros++; break;
                    }
                }

                data = world.Lista_Paises[country - 1].Id_Pais.ToString() + ";";
                data += world.Lista_Paises[country - 1].Tropas.ToString() + ";";
                data += (world.Lista_Paises[country - 1].Pertenencia == 3) ? "0.5;" : "1;";
                data += enemigos.ToString() + ";";
                data += world.Lista_Paises[country - 1].Imp.ToString() + ";";
                data += neutros.ToString();

                //Prediction
                world.Lista_Paises[country - 1].P_ATK = double.Parse(PredictAttack(data));
            }
        }


        public void Attack(List<int> Allies)
        {
            //Ataques mayores a 0.75
            var attacks = PossiblesAttacks(Allies);
            //var bestAttacks = FilterAttacks(temp1);

            if (attacks.Count > 0)
            {
                //Execute attacks
                foreach (var attack in attacks)
                {
                    if (CheckAttack(attack))
                    {
                        ExecuteAttack(attack);
                    }
                }
            }
            else
            {
                //Ataques de 0.30 a 0.60
                //Tecnicamente cuando la maquina este perdiendo
                attacks = PossiblesLowAttacks(Allies);

                //Execute attacks
                foreach (var attack in attacks)
                {
                    if (CheckLowAttack(attack))
                    {
                        ExecuteLowAttack(attack);
                    }
                }

            }

        }

        public bool CheckAttack(int[] attack)
        {
            var countryA = world.Lista_Paises[attack[0] - 1];
            var countryE = world.Lista_Paises[attack[1] - 1];

            if (countryE.Pertenencia == 1 || countryE.Pertenencia == 3)
            {
                if ((countryA.Tropas - countryE.Tropas) >= 2)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckAttackEnemy(int[] attack)
        {
            var countryA = world.Lista_Paises[attack[0] - 1];
            var countryE = world.Lista_Paises[attack[1] - 1];

            if (countryE.Pertenencia == 2 || countryE.Pertenencia == 3)
            {
                if ((countryA.Tropas - countryE.Tropas) >= 2)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckLowAttack(int[] attack)
        {
            var countryA = world.Lista_Paises[attack[0] - 1];
            var countryE = world.Lista_Paises[attack[1] - 1];

            if (countryE.Pertenencia == 1 || countryE.Pertenencia == 3)
            {
                if (((countryA.Tropas - countryE.Tropas) <= 1) && ((countryA.Tropas - countryE.Tropas) >= -4))
                {
                    return true;
                }
            }

            return false;
        }

        private void ExecuteAttack(int[] attack)
        {
            int rest = world.Lista_Paises[attack[0] - 1].Tropas - world.Lista_Paises[attack[1] - 1].Tropas;
            Attacks.Enqueue(attack[0].ToString() + ";" + attack[1].ToString());
            world.Lista_Paises[attack[1] - 1].Pertenencia = 2;

            //Movimiento de territorios entre jugadores
            world.IA.Add(attack[1]);
            world.Jugador.Remove(attack[1]);

            switch (rest)
            {
                case 2:
                    world.Lista_Paises[attack[0] - 1].Tropas = 1;
                    world.Lista_Paises[attack[1] - 1].Tropas = 1;
                    break;
                case 3:
                    world.Lista_Paises[attack[0] - 1].Tropas = 1;
                    world.Lista_Paises[attack[1] - 1].Tropas = 2;
                    break;
                case 4:
                    world.Lista_Paises[attack[0] - 1].Tropas = 2;
                    world.Lista_Paises[attack[1] - 1].Tropas = 2;
                    break;
                case 5:
                    world.Lista_Paises[attack[0] - 1].Tropas = 2;
                    world.Lista_Paises[attack[1] - 1].Tropas = 3;
                    break;
                case 6:
                    world.Lista_Paises[attack[0] - 1].Tropas = 2;
                    world.Lista_Paises[attack[1] - 1].Tropas = 4;
                    break;
                case 7:
                    world.Lista_Paises[attack[0] - 1].Tropas = 3;
                    world.Lista_Paises[attack[1] - 1].Tropas = 4;
                    break;
                case 8:
                    world.Lista_Paises[attack[0] - 1].Tropas = 3;
                    world.Lista_Paises[attack[1] - 1].Tropas = 5;
                    break;
                case 9:
                    world.Lista_Paises[attack[0] - 1].Tropas = 3;
                    world.Lista_Paises[attack[1] - 1].Tropas = 6;
                    break;
                case 10:
                    world.Lista_Paises[attack[0] - 1].Tropas = 4;
                    world.Lista_Paises[attack[1] - 1].Tropas = 6;
                    break;
                default:

                    if (rest % 2 == 0)
                    {
                        world.Lista_Paises[attack[0] - 1].Tropas = rest / 2;
                        world.Lista_Paises[attack[1] - 1].Tropas = rest / 2;
                    }
                    else
                    {
                        world.Lista_Paises[attack[0] - 1].Tropas = rest / 2;
                        world.Lista_Paises[attack[1] - 1].Tropas = (rest / 2) + 1;
                    }
                    break;
            }

        }

        private void ExecuteLowAttack(int[] attack)
        {
            int rest = world.Lista_Paises[attack[0] - 1].Tropas - world.Lista_Paises[attack[1] - 1].Tropas;
            Attacks.Enqueue(attack[0].ToString() + ";" + attack[1].ToString());

            if (rest >= 1)
            {
                world.Lista_Paises[attack[1] - 1].Pertenencia = 2;

                //Movimiento de territorios entre jugadores
                world.IA.Add(attack[1]);
                world.Jugador.Remove(attack[1]);
            }

            switch (rest)
            {
                case 1:
                    world.Lista_Paises[attack[0] - 1].Tropas = 0;
                    world.Lista_Paises[attack[1] - 1].Tropas = 1;
                    break;
                case 0:
                    world.Lista_Paises[attack[0] - 1].Tropas = 0;
                    world.Lista_Paises[attack[1] - 1].Tropas = 0;
                    break;
                case -1:
                    world.Lista_Paises[attack[0] - 1].Tropas = 0;
                    world.Lista_Paises[attack[1] - 1].Tropas = 1;
                    break;
                case -2:
                    world.Lista_Paises[attack[0] - 1].Tropas = 0;
                    world.Lista_Paises[attack[1] - 1].Tropas = 2;
                    break;
                case -3:
                    world.Lista_Paises[attack[0] - 1].Tropas = 0;
                    world.Lista_Paises[attack[1] - 1].Tropas = 3;
                    break;
                case -4:
                    world.Lista_Paises[attack[0] - 1].Tropas = 0;
                    world.Lista_Paises[attack[1] - 1].Tropas = 4;
                    break;

            }
        }

        public List<int[]> PossiblesAttacks(List<int> countries)
        {
            List<int[]> possibles = new List<int[]>();

            foreach (var element in countries)
            {
                Pais country = world.Lista_Paises[element - 1];

                foreach (var neighbour in country.pais_vecinos)
                {
                    Pais countryNeighbour = world.Lista_Paises[neighbour - 1];
                    int diferenciaTropas = 0;

                    switch (countryNeighbour.Pertenencia)
                    {
                        case 1:

                            if (countryNeighbour.P_ATK >= 0.75)
                            {
                                diferenciaTropas = country.Tropas - countryNeighbour.Tropas;

                                if (diferenciaTropas >= 2)
                                {
                                    possibles.Add(new int[] { country.Id_Pais, countryNeighbour.Id_Pais });
                                }
                            }
                            break;

                        case 3:
                            if (countryNeighbour.P_ATK >= 0.80)
                            {
                                possibles.Add(new int[] { country.Id_Pais, countryNeighbour.Id_Pais });
                            }
                            break;
                    }
                }
            }

            return possibles;
        }

        private List<int[]> PossiblesLowAttacks(List<int> countries)
        {
            List<int[]> possibles = new List<int[]>();

            foreach (var element in countries)
            {
                Pais country = world.Lista_Paises[element - 1];

                foreach (var neighbour in country.pais_vecinos)
                {
                    Pais countryNeighbour = world.Lista_Paises[neighbour - 1];
                    int diferenciaTropas = 0;

                    switch (countryNeighbour.Pertenencia)
                    {
                        case 1:

                            if (countryNeighbour.P_ATK >= 0.30 && countryNeighbour.P_ATK <= 0.70)
                            {
                                diferenciaTropas = country.Tropas - countryNeighbour.Tropas;

                                if (diferenciaTropas <= 1 && diferenciaTropas >= -4)
                                {
                                    possibles.Add(new int[] { country.Id_Pais, countryNeighbour.Id_Pais });
                                }
                            }
                            break;

                        case 3:
                            if (countryNeighbour.P_ATK >= 0.40 && countryNeighbour.P_ATK <= 0.75)
                            {
                                possibles.Add(new int[] { country.Id_Pais, countryNeighbour.Id_Pais });
                            }
                            break;
                    }
                }
            }

            return possibles;
        }

        private List<int[]> FilterAttacks(List<int[]> options)
        {
            List<int[]> newBestsAttacks = new List<int[]>();

            for (int i = 0; i < options.Count; i++)
            {
                var element = options[i];
                bool add = true;

                for (int j = 0; j < options.Count; j++)
                {
                    if (!(j == i))
                    {
                        var element2 = options[j];

                        if (element[1] == element2[1])
                        {
                            add = false;
                            var enemy1 = world.Lista_Paises[element[0] - 1];
                            var enemy2 = world.Lista_Paises[element2[0] - 1];

                            if (enemy1.P_Asig > enemy2.P_Asig)
                            {
                                newBestsAttacks.Add(element);
                            }
                            else if (enemy1.P_Asig == enemy2.P_Asig)
                            {
                                Random r = new Random();
                                var result = r.Next(0, 3);

                                switch (result)
                                {
                                    case 0: newBestsAttacks.Add(element); break;
                                    case 1: newBestsAttacks.Add(element); break;
                                    case 2: newBestsAttacks.Add(element2); break; ;
                                }
                            }
                            else
                            {
                                newBestsAttacks.Add(element2);
                            }
                        }

                    }
                }

                if (add) { newBestsAttacks.Add(element); }
            }

            newBestsAttacks = newBestsAttacks.Distinct().ToList();
            return newBestsAttacks;
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
       
            int ter = world.IA.Count;
            int tropas = world.tropaAsigamiento;
            int cantidad = Convert.ToInt16(Math.Round(ter * 0.33,0));
            double suma = 0;
            int t = tropas;
            List<Pais> paises_escojer = new List<Pais>();
            for (int i = 0; i < world.Lista_Paises.Count; i++)
            {
                if (world.Lista_Paises[i].Pertenencia == 2)
                {
                    paises_escojer.Add(world.Lista_Paises[i]);
                }

            }

            paises_escojer = paises_escojer.OrderByDescending(p => p.P_Asig).ToList();
            for (int i = 0; i < cantidad; i++)
            {
                suma += paises_escojer[i].P_Asig;

            }
            for (int i = 0; i < cantidad; i++)
            {
                int valor = Convert.ToInt32(Math.Round(t * paises_escojer[i].P_Asig / suma, 0));
                tropas -= valor;
                world.Lista_Paises[paises_escojer[i].Id_Pais - 1].Tropas += valor;
                Assignments.Enqueue(paises_escojer[i].Id_Pais);
            }
            if (tropas > 0)
            {
                world.Lista_Paises[paises_escojer[0].Id_Pais - 1].Tropas += tropas;
            }

        }

        public void Reinforcement()
        {
            int cont = 0, aux = 0; int Recibe = 0; int Dona = 0; double mayor = 0; double menor = 0;
            List<Pais> Paisemaquina = new List<Pais>();
            List<Pais> paisvecinos = new List<Pais>();

            for (int i = 0; i < 42; i++)
            {
                if (world.Lista_Paises[i].Pertenencia == 2)
                {
                    world.Lista_Paises[i].P_Fort = ThreatFactor(world.Lista_Paises[i].Id_Pais);
                    Paisemaquina.Add(world.Lista_Paises[i]);
                }

            }
            Paisemaquina = Paisemaquina.OrderBy(p => p.P_Fort).ToList();
            Dona = Paisemaquina[0].Id_Pais;
            aux = Paisemaquina[0].Tropas;
            world.Lista_Paises[Dona - 1].Tropas -= aux;

            if (Paisemaquina[0].pais_vecinos.Count > 0)
            {
                for (int i = 0; i < Paisemaquina.Count; i++)
                {
                    for (int j = 0; j < Paisemaquina[0].pais_vecinos.Count; j++)
                    {
                        if (Paisemaquina[i].Id_Pais == Paisemaquina[0].pais_vecinos[j])
                        {
                            paisvecinos.Add(Paisemaquina[i]);

                        }
                    }
                }
                paisvecinos = paisvecinos.OrderByDescending(p => p.P_Fort).ToList();
                Recibe = paisvecinos[0].Id_Pais;
                world.Lista_Paises[Recibe - 1].Tropas += aux;
            }

            //foreach (var item in world.Lista_Paises)
            //{
            //    for (int i = 0; i < world.IA.Count; i++)
            //    {
            //        if (world.Lista_Paises[cont].Id_Pais == world.IA[i])
            //        {
            //            world.Lista_Paises[cont].P_Fort = ThreatFactor(world.Lista_Paises[cont].Id_Pais);
            //            double comparar = world.Lista_Paises[cont].P_Fort;

            //            if ((mayor == 0) && (menor == 0))
            //            {
            //                mayor = comparar;
            //                menor = comparar;
            //            }
            //            else if (comparar > mayor)
            //            {
            //                mayor = comparar;
            //            }
            //            if (comparar < menor)
            //            {
            //                menor = comparar;
            //            }
            //        }

            //    }
            //    cont++;
            //    for (int i = 0; i < world.Lista_Paises.Count; i++)
            //    {
            //        if (world.Lista_Paises[i].P_Fort == menor && world.Lista_Paises[i].Pertenencia == 2 && world.Lista_Paises[i].Tropas > 3)
            //        {
            //            Dona = world.Lista_Paises[i].Id_Pais;
            //            aux = world.Lista_Paises[i].Tropas - 3;
            //            world.Lista_Paises[i].Tropas = 3;
            //        }
            //    }
            //    for (int i = 0; i < world.Lista_Paises.Count; i++)
            //    {
            //        if (world.Lista_Paises[i].P_Fort == mayor && world.Lista_Paises[i].Pertenencia == 2)
            //        {
            //            Recibe = world.Lista_Paises[i].Id_Pais;
            //            world.Lista_Paises[i].Tropas += aux;
            //        }
            //    }

            //}
            Reinforcements.Enqueue(Dona + ";" + Recibe);
        }

        public double ThreatFactor(int count)
        {
            double result = 0; int i = 0;

            if (world.IA.Contains(count))
            {
                for (i = 0; i < 42; i++)
                {
                    if (world.Lista_Paises[i].Id_Pais == count && world.Lista_Paises[i].Imp == 1)
                    {
                        result = 0.05;

                    }
                    if (world.Lista_Paises[i].Id_Pais == count && world.Lista_Paises[i].Imp == 2)
                    {
                        result = 0.10;
                    }
                    if (world.Lista_Paises[i].Id_Pais == count && world.Lista_Paises[i].Imp == 3)
                    {
                        result = 0.15;
                    }
                    if (world.Lista_Paises[i].Pertenencia == 1)
                    {
                        for (int j = 0; j < 42; j++)
                        {
                            if (world.Lista_Paises[j].Id_Pais == count)
                            {
                                if ((world.Lista_Paises[i].Tropas - world.Lista_Paises[j].Tropas) < -5)
                                { result += 0.05; }
                                if ((world.Lista_Paises[i].Tropas - world.Lista_Paises[j].Tropas) < -2 && (world.Lista_Paises[i].Tropas - world.Lista_Paises[j].Tropas) > -5)
                                { result += 0.10; }
                                if ((world.Lista_Paises[i].Tropas - world.Lista_Paises[j].Tropas) < 0 && (world.Lista_Paises[i].Tropas - world.Lista_Paises[j].Tropas) > 2)
                                { result += 0.20; }
                                if ((world.Lista_Paises[i].Tropas - world.Lista_Paises[j].Tropas) == 0)
                                { result += 0; }
                                if ((world.Lista_Paises[i].Tropas - world.Lista_Paises[j].Tropas) <= 5 && (world.Lista_Paises[i].Tropas - world.Lista_Paises[j].Tropas) >= 1)
                                { result += 0.30; }
                                if ((world.Lista_Paises[i].Tropas - world.Lista_Paises[j].Tropas) <= 10 && (world.Lista_Paises[i].Tropas - world.Lista_Paises[j].Tropas) >= 6)
                                { result += 0.40; }
                                if ((world.Lista_Paises[i].Tropas - world.Lista_Paises[j].Tropas) > 11)
                                { result += 0.50; }
                            }
                        }

                    }
                }
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
