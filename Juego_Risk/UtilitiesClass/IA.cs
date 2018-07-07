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
        Mapa world = Singleton.Instance.map;
        
        /* Builder */
        public IA()
        {
            /* teachers training*/
            this.teacherAssignment = new C45Learning();
            this.teacherAttack = new C45Learning();
            this.teacherReinforcement = new C45Learning();

            /* Training decision trees*/
            TrainingTreeAssignment();
            TrainingTreeAttack();
            TrainingTreeReinforcement();
            
            //paths of files data
            this.path_file_data_assignment = "training-data-assignment.csv";
            this.path_file_data_attack = "training-data-attack.csv";
            this.path_file_data_reinforcement = "training-data-reinforcement.csv";
           
            /* Init variables*/
            this.newDataAssignment = string.Empty;
            this.newDataAttack = string.Empty;
            this.newDataReinforcement = string.Empty;

            /* Instance Singleton*/
            //this.world = Singleton.Instance.map;
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

            for(int i = 0; i < query.Length; i++){
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
            this.newDataAttack += data + ";" + answer + "\n";

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
        public void PredictAllAssignment(int[] territoriesAlly)
        {
            /*foreach(country in territoriesAlly)
            {

                world.Paises[country].probAssignment = PredictAssignmentCountry();
            }
            */
        }

        public void PredictAllAttacks(int[] territoriesEnemy)
        {

        }

        public void PredictAllReinforcement(int[] territoriesAlly)
        {

        }


        /* Actions of IA*/

        public void Assignment()
        {

        }

        public void Attack()
        {

        }

        public void Reinforcement()
        {

        }
        
    }
}
