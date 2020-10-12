using System.Collections.Generic;
using System;

namespace FEM{
    public class GridManager{
        public List<Node> nodesGrid {set; get;}
        public List<Element> elementsGrid {set; get;}

        public GridManager(Configuration config){
            nodesGrid = new List<Node>();
            createNodesGrid(config);
            elementsGrid = new List<Element>();
            CreateElementsGrid(config);
        }
        private void createNodesGrid(Configuration config){
            double Lx = config.W / (config.nW - 1);
            double Ly = config.H / (config.nH - 1);
            for (int i = 0; i < config.nW; i ++) {
                for (int j = 0; j < config.nH; j ++) {
                    nodesGrid.Add(new Node(i*Lx, j*Ly, 100, setBC(i*Lx,j*Ly, config)));
                }
            }
            for (int i = 0; i < nodesGrid.Count; i++) {
                nodesGrid[i].Id=i;	
            }
        }
        private void CreateElementsGrid(Configuration config){
            for (int i = 0; i < config.nW - 1; i++) {
                for (int j = 0; j < config.nH - 1; j++) {
                    List<Node> nodesForElement = new List<Node>();
                    nodesForElement.Add(nodesGrid[config.nH*i+j]);
                    nodesForElement.Add(nodesGrid[config.nH*i+j+ config.nH]);
                    nodesForElement.Add(nodesGrid[config.nH*i+j+ config.nH + 1]);
                    nodesForElement.Add(nodesGrid[config.nH*i +j+1]);
                    elementsGrid.Add(new Element(nodesForElement));
                }
            }
        }
        private bool setBC(double x, double y, Configuration config){
            if (x == 0 || y == 0 || x == config.W || y == config.H) {
                return true;
            }
            else {
                return false;
            }
        }
        public void PrintElement(int id) {
            if(id < this.elementsGrid.Count) {
                Console.WriteLine("Element " + id + " : ");
                for(int i = 0; i < elementsGrid[id].nodes.Count; i ++){
                    Console.WriteLine("( "+ elementsGrid[id].nodes[i].x  + ", " + elementsGrid[id].nodes[i].y + " )");
                }
            }
        }
    }
}