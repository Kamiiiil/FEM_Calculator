using System.Collections.Generic;

namespace FEM
{
    public class Element
    {
        public List<Node> nodes;
        public Element(List<Node> nodes)
        {
            this.nodes = nodes;
        }
    }
}