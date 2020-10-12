namespace FEM{
    public class Node{
        public double x {get;}
        public double y {get;}
        public double temp {get;}
        public bool boundaryCondition {get;}
        public int Id {set; get;}
        public Node(double x, double y, double temp, bool bc){
            this.x = x;
            this.y = y;
            this.temp = temp;
            this.boundaryCondition = bc;
        }
    }
}