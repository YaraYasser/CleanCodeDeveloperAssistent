using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewParserForm
{
    public class Node
    {
        public string CodeLine;
        public StatementTypes Type; //no3 el satr dah command 3ady wla if wla loop
        public List<Node> TrueChildren;//dah zay msln law fe if (condition){  if (){} / aw aian kan ely gwa fa hwa child  } el if aw aian kan  el tanya deh child
                                       //we law loop bardo hakza ely gwa child 
        public List<Node> FalseChildren;

        public Node()
        {
            CodeLine = string.Empty;
            TrueChildren = new List<Node>();
            FalseChildren = new List<Node>();
        }
    }
}
