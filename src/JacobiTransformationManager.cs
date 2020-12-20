namespace FEM
{
    public class JacobiTransformationManager
    {
        public UniversalElement universalElement {get; set;}
        public double[] XValues {get; set;}
        public double[] YValues {get; set;}
        public double[] DXDEtaValues {get; set;}
        public double[] DXDKsiValues {get; set;}
        public double[] DYDEtaValues {get; set;}
        public double[] DYDKsiValues {get; set;}
        public double[] Determinant {get; set;}
        public double[,] DNDXValues {get; set;}
        public double[,] DNDYValues {get; set;}
        public JacobiTransformationManager(Configuration config)
        {
            XValues = new double[4]{0, config.W/(config.nW-1), config.W/(config.nW-1) , 0};
            YValues = new double[4]{0, 0, config.H/(config.nH-1) , config.H/(config.nH-1)};
            DXDEtaValues = new double[4];
            DXDKsiValues = new double[4];
            DYDEtaValues = new double[4];
            DYDKsiValues = new double[4];
            Determinant = new double[4];
            DNDXValues = new double[4,4];
            DNDYValues = new double[4,4];

            universalElement = new UniversalElement(); 
            FillDX_YDEta_KsiValues();
            CalculateDeterminant(); 
            CalculateDNDX_YValues();       
        }
        private void FillDX_YDEta_KsiValues()
        {
            for(int i=0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    DXDEtaValues[i] += XValues[j]*universalElement.DNDEtaValuesMatrix[i,j]; 
                    DXDKsiValues[i] += XValues[j]*universalElement.DNDKsiValuesMatrix[i,j];
                    DYDEtaValues[i] += YValues[j]*universalElement.DNDEtaValuesMatrix[i,j];
                    DYDKsiValues[i] += YValues[j]*universalElement.DNDKsiValuesMatrix[i,j];
                }
            }
        }
        private void CalculateDeterminant()
        {
            for(int i = 0; i < 4; i++)
            {
                Determinant[i] = DXDKsiValues[i]*DYDEtaValues[i] - DXDEtaValues[i]*DYDKsiValues[i];
            }
        }
        private void CalculateDNDX_YValues()
        {
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    DNDXValues[i,j] = (1/Determinant[i])*(DYDEtaValues[i]*universalElement.DNDKsiValuesMatrix[i,j] - DYDKsiValues[i]*universalElement.DNDEtaValuesMatrix[i,j]);
                    DNDYValues[i,j] = (1/Determinant[i])*(DXDKsiValues[i]*universalElement.DNDEtaValuesMatrix[i,j] - DXDEtaValues[i]*universalElement.DNDKsiValuesMatrix[i,j]);
                }
            }
        }
    }
}