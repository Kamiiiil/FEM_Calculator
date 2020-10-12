using System;
using System.IO;
using Newtonsoft.Json;

namespace FEM
{
    class Program
    {
        static void Main(string[] args)
        {
            FemCalculator calculator = new FemCalculator();
            calculator.Calculate();
        }
    }
}
