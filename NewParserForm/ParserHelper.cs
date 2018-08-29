using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewParserForm
{
    public class FunctionsInEachClass
    {

        public string FunctionName;
        public string FunctionParameter;
        public Modifier FunctionModifierType=Modifier.Private;
        public List<Node> nodesInEachFunction = new List<Node>();
    }
    public class ClassData
    {
        public string ClassName;
        public bool HasConstructor = false;
        public List<Modifier> ClassAttributesModifierTypes = new List<Modifier>();
        
        public List<string> ClassAttributes = new List<string>();
        public string ParentClassName;
        public int width = 0;
        public int height = 0;
        public int x = 0;
        public int y = 0;
        
    }
    public class ParserHelper
    {
        public Dictionary<ClassData, List<FunctionsInEachClass>> CodeNodesForEachClass = new Dictionary<ClassData, List<FunctionsInEachClass>>();
        public List<Node> CodeNodes = new List<Node>();
        public List<string> ClassNames = new List<string>();
        public String AllCode = "";
        string LastClassName = "";
        string LastFunctionName = "";
        bool ClassHasContent = false;

        string classParentNameForCurrentClass = "";
        List<FunctionsInEachClass> c = new List<FunctionsInEachClass>();

        public List<string> FunctionNames = new List<string>();
        bool BoolWhileLoop = false;
        bool HasConstractor = false;
        List<string> VariablesNames = new List<string>();

        public List<Node> MethodCodeNodes = new List<Node>();
        ClassData TempclassData = new ClassData();
        public void FillNodeList(IEnumerator nodeEnumerator, String coode)
        {
            AllCode = coode;
            while (nodeEnumerator.MoveNext())
            {

                if (((TreeNode)nodeEnumerator.Current).Text.Contains("ClassType = Class"))
                { //Name = tryOrDont)

                    if (((TreeNode)nodeEnumerator.Current).Text.Contains("Name = "))
                    {
                        string s = ((TreeNode)nodeEnumerator.Current).Text;
                        string toBeSearched = "Name = ";
                        string code = s.Substring(s.IndexOf(toBeSearched) + toBeSearched.Length);
                        string[] arr = code.Split(')');
                        ClassNames.Add(arr[0]);
                        LastClassName = arr[0];
                    }
                    foreach (var node in ((TreeNode)nodeEnumerator.Current).Nodes)
                    {  //"Identifier: 
                        if (((TreeNode)node).Text.Contains("BaseType: SimpleType (Identifier"))
                        {
                            string[] ArrToGetParent = ((TreeNode)node).Text.Split('=');
                            string[] arr2 = ArrToGetParent[1].Split(')');
                            classParentNameForCurrentClass = arr2[0];
                        }

                        if (((TreeNode)node).Text.Contains("FieldDeclaration"))
                        {
                            Modifier localModifier;
                            if (((TreeNode)node).Text.Contains("Modifiers = Public"))
                            {
                                localModifier = Modifier.Public;
                                TempclassData.ClassAttributesModifierTypes.Add(localModifier);
                            }
                            else if (((TreeNode)node).Text.Contains("Modifiers = Private"))
                            {
                                localModifier = Modifier.Private;
                                TempclassData.ClassAttributesModifierTypes.Add(localModifier);
                            }
                            else if (((TreeNode)node).Text.Contains("Modifiers = Protected"))
                            {
                                localModifier = Modifier.Protected;
                                TempclassData.ClassAttributesModifierTypes.Add(localModifier);
                            }

                            else
                            {
                                localModifier = Modifier.Private;
                                TempclassData.ClassAttributesModifierTypes.Add(localModifier);
                            }

                            String tempString = ((TreeNode)node).Tag.ToString();
                            TempclassData.ClassAttributes.Add(tempString);
                            ClassHasContent = true;
                        }
                        else if (((TreeNode)node).Text.Contains("SymbolKind = Constructor"))
                        {
                            HasConstractor = true;
                        }
                        if (((TreeNode)node).Text.Contains("Main"))
                        {
                            GetDataFromBody((TreeNode)node);

                        }

                        else if (((TreeNode)node).Text.Contains("SymbolKind = Method"))
                        {

                            GetDataFromBody((TreeNode)node);

                        }


                    }

                    if (ClassHasContent == false)
                    {
                        c = new List<FunctionsInEachClass>();
                        TempclassData.ClassName = LastClassName;
                        if (HasConstractor == true)
                        {
                            TempclassData.HasConstructor = true;
                            HasConstractor = false;
                        }
                        CodeNodesForEachClass.Add(TempclassData, c);
                        TempclassData = new ClassData();
                    }
                    else
                    {
                        ClassHasContent = false;
                    }
                }
                else if (((TreeNode)nodeEnumerator.Current).Text.Contains("NamespaceDeclaration"))
                {
                    if (((TreeNode)nodeEnumerator.Current).Text.Contains("Name = "))
                    {
                        string s = ((TreeNode)nodeEnumerator.Current).Text;
                        string toBeSearched = "Name = ";
                        string code = s.Substring(s.IndexOf(toBeSearched) + toBeSearched.Length);
                        string[] arr = code.Split(')');
                        ClassNames.Add(arr[0]);
                        LastClassName = arr[0];
                    }
                    foreach (var node in ((TreeNode)nodeEnumerator.Current).Nodes)
                    {

                        if (((TreeNode)node).Text.Contains("ClassType = Class"))
                        {
                            foreach (var node2 in ((TreeNode)node).Nodes)
                            {
                                if (((TreeNode)node2).Text.Contains("Main"))
                                {
                                    GetDataFromBody((TreeNode)node2);
                                }
                                else if ((((TreeNode)node2).Text.Contains("SymbolKind = Method")) || (((TreeNode)node2).Text.Contains("Main")))
                                {
                                    GetDataFromBody((TreeNode)node2);
                                }
                            }
                            break;
                        }
                    }
                }

            }



        }

        private void InnerNodes(TreeNode BodyNode, List<Node> appendTo)
        {
            // chick for function name here
            if (BodyNode.Text.Contains("IfElseStatement") || BodyNode.Text.Contains("WhileStatement") || BodyNode.Text.Contains("ForStatement") || BodyNode.Text.Contains("If") || BodyNode.Text.Contains("else"))
            {
                if (BodyNode.Text.Contains("WhileStatement"))
                {
                    BoolWhileLoop = true;
                }
                NewParserForm.Node ifNode = new Node();
                NewParserForm.Node iterator = null;
                int IteratorFirstTimeChecker = 0;

                foreach (TreeNode IfNodes in ((TreeNode)BodyNode).Nodes)
                {
                    if (IfNodes.Text.Contains("Condition"))
                    {
                        ifNode.CodeLine = IfNodes.Tag.ToString();
                        if (BodyNode.Text.Contains("IfElseStatement"))
                        {
                            ifNode.Type = StatementTypes.Condition;
                        }
                        else
                        {
                            if (BoolWhileLoop)
                            {
                                ifNode.Type = StatementTypes.WhileLoop;
                            }
                            else
                            {
                                ifNode.Type = StatementTypes.ForLoop;
                            }
                            BoolWhileLoop = false;
                        }

                    }

                    else if (IfNodes.Text.Contains("IfElseStatement"))
                    {
                        InnerNodes(IfNodes, ifNode.FalseChildren);
                    }
                    else if (IfNodes.Text.Contains("False"))
                    {
                        BodyRec(IfNodes, ifNode.FalseChildren);
                        if (iterator != null)
                        {
                            ifNode.TrueChildren.Add(iterator);
                        }
                    }
                    else if (IfNodes.Text.Contains("Initializer"))
                    {
                        appendTo.Add(new Node()
                        {
                            Type = StatementTypes.Statement,
                            CodeLine = IfNodes.Tag.ToString()
                        });
                    }
                    else if (IfNodes.Text.Contains("Iterator"))
                    {
                        IteratorFirstTimeChecker = 1;
                        iterator = new Node()
                        {
                            Type = StatementTypes.Statement,
                            CodeLine = IfNodes.Tag.ToString()
                        };

                    }
                    // NewLine: NewLineNode (NewLineType = CRLF)
                    else if (IfNodes.Text.Contains("): CSharpTokenNode") || (IfNodes.Text.Contains("NewLine: NewLineNode (NewLineType = CRLF)")))
                    {
                        continue;
                    }
                    else
                    {
                        BodyRec(IfNodes, ifNode.TrueChildren);

                        if (iterator != null)
                        {
                            if (IteratorFirstTimeChecker == 1)
                            {
                                IteratorFirstTimeChecker++;
                            }

                            else
                            {
                                ifNode.TrueChildren.Add(iterator);
                                IteratorFirstTimeChecker = 0;
                            }
                        }


                    }
                }
                appendTo.Add(ifNode);

            }
            else if (BodyNode.Text.Contains("Statement"))
            {
                if (BodyNode.Text.Contains("VariableDeclarationStatement"))
                {
                    string[] arr = BodyNode.Tag.ToString().Split();
                    VariablesNames.Add(arr[1]);
                    appendTo.Add(new NewParserForm.Node()
                    {
                        CodeLine = BodyNode.Tag.ToString(),
                        Type = StatementTypes.VariableDeclaration
                    });
                }
                else if (BodyNode.FirstNode.Text.Contains("(Operator = Add)"))
                {
                    appendTo.Add(new NewParserForm.Node()
                    {
                        CodeLine = BodyNode.Tag.ToString(),
                        Type = StatementTypes.AddOperator
                    });
                }
                else if (BodyNode.FirstNode.Text.Contains("(Operator = PostIncrement)"))
                {
                    appendTo.Add(new NewParserForm.Node()
                    {
                        CodeLine = BodyNode.Tag.ToString(),
                        Type = StatementTypes.PostIncrement
                    });
                }

                else if (BodyNode.FirstNode.Text.Contains("(Operator = Assign)"))
                {
                    appendTo.Add(new NewParserForm.Node()
                    {
                        CodeLine = BodyNode.Tag.ToString(),
                        Type = StatementTypes.Assign
                    });
                }
                else
                {
                    appendTo.Add(new NewParserForm.Node()
                    {
                        CodeLine = BodyNode.Tag.ToString(),
                        Type = StatementTypes.Statement
                    });
                }
            }

        }

        private void BodyRec(TreeNode n, List<Node> appendTo)
        {
            foreach (TreeNode BodyNode in n.Nodes)
            {
                InnerNodes(BodyNode, appendTo);

            }

        }

        private void GetDataFromBody(TreeNode node)
        {
            c = new List<FunctionsInEachClass>();


            FunctionsInEachClass dc = new FunctionsInEachClass();

            foreach (TreeNode n in ((TreeNode)node).Nodes)
            {
                if (((TreeNode)n).Text.Contains("Parameter: ParameterDeclaration"))
                {
                    dc.FunctionParameter = ((TreeNode)n).Tag.ToString();

                }
                if (((TreeNode)node).Text.Contains("Modifiers = Public"))
                {
                    dc.FunctionModifierType = Modifier.Public;
                }
                else if (((TreeNode)node).Text.Contains("Modifiers = Private"))
                {
                    dc.FunctionModifierType = Modifier.Private;
                }
                else if (((TreeNode)node).Text.Contains("Modifiers = Protected"))
                {
                    dc.FunctionModifierType = Modifier.Protected;
                }
                if (((TreeNode)n).Text.Contains("(Name = "))
                {
                    string s = ((TreeNode)n).Text;
                    string toBeSearched = "Name = ";
                    string code = s.Substring(s.IndexOf(toBeSearched) + toBeSearched.Length);
                    string[] arr = code.Split(')');
                    string[] arr2 = arr[0].Split(',');
                    FunctionNames.Add(arr2[0]);
                    LastFunctionName = arr2[0];
                }
                else if (((TreeNode)n).Text.Contains("Body: BlockStatement"))
                {
                    bool ClassExist = false;
                    BodyRec(n, CodeNodes);
                    List<Node> temp = new List<Node>();
                    temp = new List<Node>(CodeNodes);

                    dc.FunctionName = LastFunctionName;
                    dc.nodesInEachFunction = temp;
                    c.Add(dc);

                    TempclassData.ClassName = LastClassName;
                    if (classParentNameForCurrentClass != "")
                    {
                        TempclassData.ParentClassName = classParentNameForCurrentClass;
                        classParentNameForCurrentClass = "";
                    }
                    foreach (var i in CodeNodesForEachClass)
                    {
                        if (i.Key.ClassName == TempclassData.ClassName)
                        {
                            ClassExist = true;
                            i.Value.Add(dc);
                        }

                    }
                    if (ClassExist == false)
                    {
                        if (HasConstractor == true)
                        {
                            TempclassData.HasConstructor = true;
                            HasConstractor = false;
                        }

                        CodeNodesForEachClass.Add(TempclassData, c);
                        CodeNodes.Clear();

                    }
                    else ClassExist = false;
                    TempclassData = new ClassData();
                }
                ((TreeNode)n).Tag.ToString();
                String sgg = ((TreeNode)n).Text;

            }
            ClassHasContent = true;
            TempclassData = new ClassData();
        }
    }
}