using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using Newtonsoft.Json;

namespace FEM
{
    public class FemCalculator
    {
        Matrix<double> PVector;
        Matrix<double> HMatrix;
        Matrix<double> CMatrix;
        Matrix<double> temp;

        public void Calculate()
        {
            Stopwatch stopwatch = new Stopwatch();
            Configuration config = getConfiguration();
            GridManager grid = new GridManager(config);
            JacobiTransformationManager jacobi = new JacobiTransformationManager(config);
            HMatrixManager hMatrix = new HMatrixManager(jacobi, config);
            CMatrixManager cMatrix = new CMatrixManager(jacobi, config);
            HbcMatrixManager hbcMatrix = new HbcMatrixManager(jacobi, config);
            VectorPManager vectorP = new VectorPManager(config);
            MatrixAgregator global = new MatrixAgregator(hMatrix,cMatrix, grid, config, hbcMatrix, vectorP); 

            
            FillMatrixes(global, grid);
            stopwatch.Start();
            Matrix<double> CMatrixDt = CMatrix/config.simulationStepTime;
            Matrix<double> tempHMatrix = CMatrixDt+HMatrix;
            Matrix<double> InverseTempHMatrix = tempHMatrix.Inverse();
            for(int i = (int)config.simulationStepTime; i <= config.simulationTime; i+=(int)config.simulationStepTime)
            {
                Matrix<double> tempCMatrix = CMatrixDt*temp - PVector;
                Matrix<double> NewTemp = InverseTempHMatrix*tempCMatrix;
                temp = NewTemp;
                Console.WriteLine("Time: " + i + " MinTemp: " + temp.Enumerate().Min() + " MaxTemp: " + temp.Enumerate().Max());
            }
            stopwatch.Stop();
            Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
        }
        
        private Configuration getConfiguration()
        {
            StreamReader r = new StreamReader("configuration.json");
            var json = r.ReadToEnd();
            Configuration configuration = JsonConvert.DeserializeObject<Configuration>(json);
            configuration.completeConfiig();
            return configuration;
        }
        private void FillMatrixes(MatrixAgregator global, GridManager grid)
        {
            temp = Matrix<double>.Build.Dense(grid.nodesGrid.Count, 1);
            PVector = Matrix<double>.Build.Dense(grid.nodesGrid.Count, 1);
            HMatrix = Matrix<double>.Build.DenseOfArray(global.HGlobalMatrix);
            CMatrix = Matrix<double>.Build.DenseOfArray(global.CGlobalMatrix);
            for (int i = 0; i < grid.nodesGrid.Count; i++)
            {
                PVector[i, 0] = global.GlobalPVector[i]; 
                temp[i, 0] = grid.nodesGrid[i].temp;
            }  
        }
    }
}