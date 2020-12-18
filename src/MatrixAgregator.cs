namespace FEM
{
    public class GlobalMatrixManager
    {
        public double[,] HGlobalMatrix {get; set;}
        public double[,] CGlobalMatrix {get; set;}
        public GlobalMatrixManager(HMatrixManager hMatrix, CMatrixManager cMatrix, GridManager grid, Configuration config)
        {
            HGlobalMatrix = new double[grid.nodesGrid.Count, grid.nodesGrid.Count];
            CGlobalMatrix = new double[grid.nodesGrid.Count, grid.nodesGrid.Count];
            GenerateGlobalMatrixes(hMatrix ,cMatrix, grid, config);
        }
        private void GenerateGlobalMatrixes(HMatrixManager hMatrix, CMatrixManager cMatrix, GridManager grid, Configuration config)
        {
            for(int i = 0; i < grid.elementsGrid.Count; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    for(int k = 0; k < 4; k++)
                    {
                        HGlobalMatrix[grid.elementsGrid[i].nodes[j].Id, grid.elementsGrid[i].nodes[k].Id] += hMatrix.HMatrix[j,k];
                        CGlobalMatrix[grid.elementsGrid[i].nodes[j].Id, grid.elementsGrid[i].nodes[k].Id] += cMatrix.CMatrix[j,k];
                    }
                }
            }
        }
    }
}