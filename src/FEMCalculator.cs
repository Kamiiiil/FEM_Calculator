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
            UniversalElement universalElement = new UniversalElement();
            // for(int i = 0; i<4; i++)
            // {
            //     for(int j = 0; j<4; j++)
            //     {
            //         Console.Write(universalElement.DNDEtaValuesMatrix[i,j]+ " ");
            //     }
            //     Console.WriteLine("");
            // }
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