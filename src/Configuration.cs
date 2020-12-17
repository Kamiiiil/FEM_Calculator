namespace FEM
{
    public class Configuration 
    {
        public double initialTemperature { get; set; }
        public double simulationTime { get; set; }
        public double simulationStepTime { get; set; }
        public double alfa { get; set; }
        public double H { get; set; }
        public double W { get; set; }
        public int nH { get; set; }
        public int nW { get; set; }
        public int nN {get; set;}
        public int nE {get; set;}
        public double specificHeat { get; set; }
        public double conductivity { get; set; }
        public double density { get; set; }
        public void completeConfiig()
        {
            this.nN = this.nH * this.nW;
            this.nE = (this.nH - 1) * (this.nW - 1);
        }
    }
}
