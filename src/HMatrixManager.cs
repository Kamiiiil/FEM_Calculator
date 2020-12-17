namespace FEM
{
    public class HMatrixManager
    {
        public double[,] H1Matrix {get; set;}
        public double[,] H2Matrix {get; set;}
        public double[,] H3Matrix {get; set;}
        public double[,] H4Matrix {get; set;}
        public double[,] HMatrix {get; set;}

        public HMatrixManager(JacobiTransformationManager jacobi, Configuration config)
        {
            H1Matrix = new double[4,4];
            H2Matrix = new double[4,4];
            H3Matrix = new double[4,4];
            H4Matrix = new double[4,4];
            HMatrix = new double[4,4];

            MultiplyAndAddMatrixes(jacobi,config);
            AddMatrixes();
        }
        private void MultiplyAndAddMatrixes(JacobiTransformationManager jacobi, Configuration config)
        {
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    H1Matrix[i,j] = config.conductivity*jacobi.Determinant[0]*(jacobi.DNDXValues[0,i]*jacobi.DNDXValues[0,j] + jacobi.DNDYValues[0,i]*jacobi.DNDYValues[0,j]);
                    H2Matrix[i,j] = config.conductivity*jacobi.Determinant[1]*(jacobi.DNDXValues[1,i]*jacobi.DNDXValues[1,j] + jacobi.DNDYValues[1,i]*jacobi.DNDYValues[1,j]);
                    H3Matrix[i,j] = config.conductivity*jacobi.Determinant[2]*(jacobi.DNDXValues[2,i]*jacobi.DNDXValues[2,j] + jacobi.DNDYValues[2,i]*jacobi.DNDYValues[2,j]);
                    H4Matrix[i,j] = config.conductivity*jacobi.Determinant[3]*(jacobi.DNDXValues[3,i]*jacobi.DNDXValues[3,j] + jacobi.DNDYValues[3,i]*jacobi.DNDYValues[3,j]);
                }
            }
        }
        private void AddMatrixes()
        {
             for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    HMatrix[i,j] = H1Matrix[i,j] + H2Matrix[i,j] + H3Matrix[i,j] + H4Matrix[i,j];
                }
            }
        }
    }
}