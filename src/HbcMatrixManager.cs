using System;

namespace FEM
{
    public class HbcMatrixManager
    {
        public double[,] Pow1NValues { get; set; }
        public double[,] Pow2NValues { get; set; }
        public double[,] Pow3NValues { get; set; }
        public double[,] Pow4NValues { get; set; }
        public double[,] Pow1HbcMatrix {get; set;}
        public double[,] Pow2HbcMatrix {get; set;}
        public double[,] Pow3HbcMatrix {get; set;}
        public double[,] Pow4HbcMatrix {get; set;}
        public double XDeterminant {get; set;}
        public double YDeterminant {get; set;}

        public HbcMatrixManager(JacobiTransformationManager jacobi, Configuration config)
        {
            Pow1HbcMatrix = new double[4,4];
            Pow2HbcMatrix = new double[4,4];
            Pow3HbcMatrix = new double[4,4];
            Pow4HbcMatrix = new double[4,4];

            XDeterminant = (config.W/(config.nW-1))/2;
            YDeterminant = (config.H/(config.nH-1))/2;

            CalculateNMatrixes();
            CalculateHbcMatrix(jacobi, config);
        }
        private double[,] FillNMatrix(double [] KsiValues, double [] EtaValues)
        {
            double[,] Hbc = new double[2,4];
            for (int i = 0; i < 2; i++)
            {
                Hbc[i, 0] = UniversalElementCalculator.N1Function(KsiValues[i], EtaValues[i]);
                Hbc[i, 1] = UniversalElementCalculator.N2Function(KsiValues[i], EtaValues[i]);
                Hbc[i, 2] = UniversalElementCalculator.N3Function(KsiValues[i], EtaValues[i]);
                Hbc[i, 3] = UniversalElementCalculator.N4Function(KsiValues[i], EtaValues[i]);
            }
            return Hbc;
        }     
        private void CalculateHbcMatrix(JacobiTransformationManager jacobi, Configuration config)
        {
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    Pow1HbcMatrix[i,j] = config.alfa*XDeterminant*(Pow1NValues[0,i]*Pow1NValues[0,j] + Pow1NValues[1,i]*Pow1NValues[1,j]);
                    Pow2HbcMatrix[i,j] = config.alfa*YDeterminant*(Pow2NValues[0,i]*Pow2NValues[0,j] + Pow2NValues[1,i]*Pow2NValues[1,j]);
                    Pow3HbcMatrix[i,j] = config.alfa*XDeterminant*(Pow3NValues[0,i]*Pow3NValues[0,j] + Pow3NValues[1,i]*Pow3NValues[1,j]);
                    Pow4HbcMatrix[i,j] = config.alfa*YDeterminant*(Pow4NValues[0,i]*Pow4NValues[0,j] + Pow4NValues[1,i]*Pow4NValues[1,j]);
                }
            }
        }
        private void CalculateNMatrixes()
        {
            double [] KsiValues = new double[2] {-1/(Math.Sqrt(3)), 1 / (Math.Sqrt(3))};
            double [] EtaValues = new double[2] { -1, -1 };
            Pow1NValues = FillNMatrix(KsiValues, EtaValues);
            KsiValues = new double[2] { 1, 1};
            EtaValues = new double[2] { -1 / (Math.Sqrt(3)), 1  / (Math.Sqrt(3))};
            Pow2NValues = FillNMatrix(KsiValues, EtaValues);
            KsiValues = new double[2] {-1/(Math.Sqrt(3)), 1 / (Math.Sqrt(3))};
            EtaValues = new double[2] { 1, 1 };
            Pow3NValues = FillNMatrix(KsiValues, EtaValues);
            KsiValues = new double[2] { -1, -1};
            EtaValues = new double[2] { -1 / (Math.Sqrt(3)), 1  / (Math.Sqrt(3))};
            Pow4NValues = FillNMatrix(KsiValues, EtaValues);
        }
    }
}