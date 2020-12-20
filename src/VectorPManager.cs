using System;

namespace FEM
{
    public class VectorPManager
    {
        public double[] Node1NValues { get; set; }
        public double[] Node2NValues { get; set; }
        public double[] Node3NValues { get; set; }
        public double[] Node4NValues { get; set; }
        public double[] Pow1PVector {get; set;}
        public double[] Pow2PVector {get; set;}
        public double[] Pow3PVector {get; set;}
        public double[] Pow4PVector {get; set;}
        public double XDeterminant {get; set;}
        public double YDeterminant {get; set;}
        
        public VectorPManager(Configuration config)
        {
            Node1NValues = new double[4];
            Node2NValues = new double[4];
            Node3NValues = new double[4];
            Node4NValues = new double[4];
            Pow1PVector = new double[4];
            Pow2PVector = new double[4];
            Pow3PVector = new double[4];
            Pow4PVector = new double[4];

            XDeterminant = (config.W/(config.nW-1))/2;
            YDeterminant = (config.H/(config.nH-1))/2;

            CalculateNValues();
            CalculatePVectors(config);
        }
        private void CalculatePVectors(Configuration config)
        {
             for(int i = 0; i < 4; i++)
            {
                Pow1PVector[i] = -XDeterminant*config.alfa*config.ambientTemperature*(Node1NValues[i] + Node2NValues[i]);
                Pow2PVector[i] = -YDeterminant*config.alfa*config.ambientTemperature*(Node2NValues[i] + Node3NValues[i]);
                Pow3PVector[i] = -XDeterminant*config.alfa*config.ambientTemperature*(Node3NValues[i] + Node4NValues[i]);
                Pow4PVector[i] = -YDeterminant*config.alfa*config.ambientTemperature*(Node4NValues[i] + Node1NValues[i]);
            }
        }
         private void CalculateNValues()
        {
            double KsiValues = -1;
            double EtaValues = -1;
            Node1NValues = FillNMatrix(KsiValues, EtaValues);
            KsiValues = 1;
            EtaValues = -1;
            Node2NValues = FillNMatrix(KsiValues, EtaValues);
            KsiValues = 1;
            EtaValues = 1;
            Node3NValues = FillNMatrix(KsiValues, EtaValues);
            KsiValues = -1;
            EtaValues = 1;
            Node4NValues = FillNMatrix(KsiValues, EtaValues);
        }
        private double[] FillNMatrix(double KsiValues, double EtaValues)
        {
            double[] NValuesForPVector = new double[4];
            NValuesForPVector[0] = UniversalElementCalculator.N1Function(KsiValues, EtaValues);
            NValuesForPVector[1] = UniversalElementCalculator.N2Function(KsiValues, EtaValues);
            NValuesForPVector[2] = UniversalElementCalculator.N3Function(KsiValues, EtaValues);
            NValuesForPVector[3] = UniversalElementCalculator.N4Function(KsiValues, EtaValues);
            return NValuesForPVector;
        }     
    }
}