namespace FEM
{
    public class CMatrixManager
    {
        public double[,] C1Matrix {get; set;}
        public double[,] C2Matrix {get; set;}
        public double[,] C3Matrix {get; set;}
        public double[,] C4Matrix {get; set;}
        public double[,] CMatrix {get; set;}

        public CMatrixManager(JacobiTransformationManager jacobi, Configuration config)
        {
            C1Matrix = new double[4,4];
            C2Matrix = new double[4,4];
            C3Matrix = new double[4,4];
            C4Matrix = new double[4,4];
            CMatrix = new double[4,4];
            
            MultiplyAndAddMatrixes(jacobi,config);
            AddMatrixes();
        }
        private void MultiplyAndAddMatrixes(JacobiTransformationManager jacobi, Configuration config)
        {
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    C1Matrix[i,j] = config.specificHeat*config.density*jacobi.Determinant[0]*(jacobi.universalElement.NValuesMatrix[0,i]*jacobi.universalElement.NValuesMatrix[0,j]);
                    C2Matrix[i,j] = config.specificHeat*config.density*jacobi.Determinant[1]*(jacobi.universalElement.NValuesMatrix[1,i]*jacobi.universalElement.NValuesMatrix[1,j]);
                    C3Matrix[i,j] = config.specificHeat*config.density*jacobi.Determinant[2]*(jacobi.universalElement.NValuesMatrix[2,i]*jacobi.universalElement.NValuesMatrix[2,j]);
                    C4Matrix[i,j] = config.specificHeat*config.density*jacobi.Determinant[3]*(jacobi.universalElement.NValuesMatrix[3,i]*jacobi.universalElement.NValuesMatrix[3,j]);
                }
            }
        }
        private void AddMatrixes()
        {
             for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    CMatrix[i,j] = C1Matrix[i,j] + C2Matrix[i,j] + C3Matrix[i,j] + C4Matrix[i,j];
                }
            }
        }
    }
}