using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FEM{
    public class FemCalculator{
        public void Calculate(){
            Configuration config = getConfiguration();
            GridManager grid = new GridManager(config);
            grid.PrintElement(2);
        }
        private Configuration getConfiguration(){
            StreamReader r = new StreamReader("configuration.json");
            var json = r.ReadToEnd();
            Configuration configuration = JsonConvert.DeserializeObject<Configuration>(json);
            configuration.completeConfiig();
            return configuration;
        }
    }
}