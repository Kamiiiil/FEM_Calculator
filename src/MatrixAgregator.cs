namespace FEM
{
    public class MatrixAgregator
    {
        public double[,] HGlobalMatrix {get; set;}
        public double[,] CGlobalMatrix {get; set;}
        public MatrixAgregator(HMatrixManager hMatrix, CMatrixManager cMatrix, GridManager grid, Configuration config, HbcMatrixManager hbcMatrix)
        {
            HGlobalMatrix = new double[grid.nodesGrid.Count, grid.nodesGrid.Count];
            CGlobalMatrix = new double[grid.nodesGrid.Count, grid.nodesGrid.Count];
            GenerateGlobalMatrixes(hMatrix ,cMatrix, grid, config, hbcMatrix);
        }
        private void GenerateGlobalMatrixes(HMatrixManager hMatrix, CMatrixManager cMatrix, GridManager grid, Configuration config, HbcMatrixManager hbcMatrix)
        {
            for(int i = 0; i < grid.elementsGrid.Count; i++)
            {
                double[,] HMatrix=hMatrix.HMatrix;
                if(grid.elementsGrid[i].nodes[0].x == 0) 
                    HMatrix = AddMatrixes(HMatrix, hbcMatrix.Pow4HbcMatrix);
                if(grid.elementsGrid[i].nodes[1].x == config.W)
                    HMatrix = AddMatrixes(HMatrix, hbcMatrix.Pow2HbcMatrix);
                if(grid.elementsGrid[i].nodes[0].y == 0) 
                    HMatrix = AddMatrixes(HMatrix, hbcMatrix.Pow1HbcMatrix);
                if(grid.elementsGrid[i].nodes[1].y == config.H)
                    HMatrix = AddMatrixes(HMatrix, hbcMatrix.Pow3HbcMatrix);

                for(int j = 0; j < 4; j++)
                {
                    for(int k = 0; k < 4; k++)
                    {
                        HGlobalMatrix[grid.elementsGrid[i].nodes[j].Id, grid.elementsGrid[i].nodes[k].Id] += HMatrix[j,k];
                        CGlobalMatrix[grid.elementsGrid[i].nodes[j].Id, grid.elementsGrid[i].nodes[k].Id] += HMatrix[j,k];
                    }
                }
            }
        }
        private double[,] AddMatrixes(double[,] Matrix1, double[,] Matrix2)
        {
            for(int i=0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    Matrix1[i,j] += Matrix2[i,j];
                }
            }
            return Matrix1;
        }
    }
}