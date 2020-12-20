namespace FEM
{
    public class MatrixAgregator
    {
        public double[,] HGlobalMatrix {get; set;}
        public double[,] CGlobalMatrix {get; set;}
        public double[] GlobalPVector {get; set;}
        public MatrixAgregator(HMatrixManager hMatrix, CMatrixManager cMatrix, GridManager grid, Configuration config, HbcMatrixManager hbcMatrix, VectorPManager vectorP)
        {
            HGlobalMatrix = new double[grid.nodesGrid.Count, grid.nodesGrid.Count];
            CGlobalMatrix = new double[grid.nodesGrid.Count, grid.nodesGrid.Count];
            GlobalPVector = new double[grid.nodesGrid.Count];
            GenerateGlobalMatrixes(hMatrix ,cMatrix, grid, config, hbcMatrix, vectorP);
        }
        private void GenerateGlobalMatrixes(HMatrixManager hMatrix, CMatrixManager cMatrix, GridManager grid, Configuration config, HbcMatrixManager hbcMatrix, VectorPManager vectorP)
        {
            for(int i = 0; i < grid.elementsGrid.Count; i++)
            {
                double[,] HMatrix=new double[grid.nodesGrid.Count, grid.nodesGrid.Count];
                for(int j = 0; j < 4; j++)
                {
                    for(int k = 0; k < 4; k++)
                    {
                        HMatrix[j,k] = hMatrix.HMatrix[j,k];
                    }
                }
                if(grid.elementsGrid[i].nodes[3].boundaryCondition && grid.elementsGrid[i].nodes[0].boundaryCondition) 
                    HMatrix = AddMatrixes(HMatrix, hbcMatrix.Pow4HbcMatrix);
                if(grid.elementsGrid[i].nodes[1].boundaryCondition && grid.elementsGrid[i].nodes[2].boundaryCondition)
                    HMatrix = AddMatrixes(HMatrix, hbcMatrix.Pow2HbcMatrix);
                if(grid.elementsGrid[i].nodes[0].boundaryCondition && grid.elementsGrid[i].nodes[1].boundaryCondition) 
                    HMatrix = AddMatrixes(HMatrix, hbcMatrix.Pow1HbcMatrix);
                if(grid.elementsGrid[i].nodes[2].boundaryCondition && grid.elementsGrid[i].nodes[3].boundaryCondition)
                    HMatrix = AddMatrixes(HMatrix, hbcMatrix.Pow3HbcMatrix);

                for(int j = 0; j < 4; j++)
                {
                    for(int k = 0; k < 4; k++)
                    {
                        HGlobalMatrix[grid.elementsGrid[i].nodes[j].Id, grid.elementsGrid[i].nodes[k].Id] += HMatrix[j,k];
                        CGlobalMatrix[grid.elementsGrid[i].nodes[j].Id, grid.elementsGrid[i].nodes[k].Id] += cMatrix.CMatrix[j,k];
                    }
                    if(grid.elementsGrid[i].nodes[0].boundaryCondition && grid.elementsGrid[i].nodes[1].boundaryCondition)
                        GlobalPVector[grid.elementsGrid[i].nodes[j].Id] += vectorP.Pow1PVector[j];
                    if(grid.elementsGrid[i].nodes[1].boundaryCondition && grid.elementsGrid[i].nodes[2].boundaryCondition)
                        GlobalPVector[grid.elementsGrid[i].nodes[j].Id] += vectorP.Pow2PVector[j];
                    if(grid.elementsGrid[i].nodes[2].boundaryCondition && grid.elementsGrid[i].nodes[3].boundaryCondition)
                        GlobalPVector[grid.elementsGrid[i].nodes[j].Id] += vectorP.Pow3PVector[j];
                    if(grid.elementsGrid[i].nodes[3].boundaryCondition && grid.elementsGrid[i].nodes[0].boundaryCondition)
                        GlobalPVector[grid.elementsGrid[i].nodes[j].Id] += vectorP.Pow4PVector[j];
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