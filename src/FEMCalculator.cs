using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FEM
{
    public class FemCalculator
    {
        public void Calculate()
        {
            Configuration config = getConfiguration();
            GridManager grid = new GridManager(config);
            JacobiTransformationManager jacobi = new JacobiTransformationManager(config);
            HMatrixManager hMatrix = new HMatrixManager(jacobi, config);
            CMatrixManager cMatrix = new CMatrixManager(jacobi, config);
            HbcMatrixManager hbcMatrix = new HbcMatrixManager(jacobi, config);
            VectorPManager vectorP = new VectorPManager(config);
            MatrixAgregator global = new MatrixAgregator(hMatrix,cMatrix, grid, config, hbcMatrix, vectorP);

            for(int i = 0; i < 16; i++)
            {
                Console.Write(global.GlobalPVector[i]+ "  ");
                // for(int j = 0; j < 16; j++)
                // {
                //     Console.Write(global.HGlobalMatrix[i,j]+ "     ");
                // }
                // Console.WriteLine("");
            }
        }
        private Configuration getConfiguration()
        {
            StreamReader r = new StreamReader("configuration.json");
            var json = r.ReadToEnd();
            Configuration configuration = JsonConvert.DeserializeObject<Configuration>(json);
            configuration.completeConfiig();
            return configuration;
        }
    }
}