using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.CSharp.Resolver;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using System.Reflection;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace NewParserForm
{
    public partial class Form2 : Form
    {

        public System.Windows.Forms.TreeView csharpTreeView;
        IEnumerator nodeEnumerator;
        ParserHelper parserHelper = new ParserHelper();
        public Graphics panelGraphics { get; set; }
        Pen pen = new Pen(Color.Black);
        public avalable_names gogo = new avalable_names();

        List<ClassData> un_checked_classes = new List<ClassData>();



        /// <summary>  class diagraaaam////////////////////////////////////////////////////////////////////////////
        public Rectangle classz = new Rectangle();

        public int class_posx = 10;
        public int class_posy = 10;
        public int y = 0;
        public Graphics PG { get; set; }
        public List<Point> classes_pos = new List<Point>();


        public int class_order = 0;
        //public Dictionary<ClassData, List<FunctionsInEachClass>> class_code_nodes = new Dictionary<ClassData, List<FunctionsInEachClass>>();


      

        //public void draw_class(&parserhelper)
        //public void draw_class(string class_name,int X, int Y,List<string> attributes , List<FunctionsInEachClass> func)
        public void draw_class(/*Dictionary<ClassData, List<FunctionsInEachClass>> class_code_nodes*/)
        {
            

            foreach (var el in parserHelper.CodeNodesForEachClass)
            {

                

                classz = new Rectangle(class_posx, class_posy, 100, (y + 15));
                //classes_pos.Add(new Point(classz.X, classz.Y));
                un_checked_classes[class_order].x = classz.X;
                un_checked_classes[class_order].y = classz.Y;

                panelGraphics.DrawString(el.Key.ClassName, DefaultFont, Brushes.Red, new Point(classz.X + 30, classz.Y + y));
                y += 12;
                panelGraphics.DrawLine(Pens.Yellow, new Point(classz.X, classz.Y + y), new Point(classz.X + classz.Width, classz.Y + y));
                // y += 30; //atterbute

                for (int i = 0; i < el.Key.ClassAttributes.Count; i++)    //attributes
                {
                    panelGraphics.DrawString(el.Key.ClassAttributes[i], DefaultFont, Brushes.Red, new Point(classz.X + 30, classz.Y + y));
                    y += 10;
                }                                            //attributes

                panelGraphics.DrawLine(Pens.Yellow, new Point(classz.X, classz.Y + y), new Point(classz.X + classz.Width, classz.Y + y));

                for (int i = 0; i < el.Value.Count; i++)
                {
                    panelGraphics.DrawString(el.Value[i].FunctionName, DefaultFont, Brushes.Red, new Point(classz.X + 30, classz.Y + y));
                    y += 10;
                }
                classz = new Rectangle(class_posx, class_posy, 100, (y + 5));

                un_checked_classes[class_order].width = classz.Width;
                un_checked_classes[class_order].height = classz.Height;

                class_order++;

                panelGraphics.DrawRectangle(Pens.Green, classz);
                class_posx += 200;
                y = 0;
            }
           // class_posx = 2;
            //class_posy = 2;
        }


        public void draw_assosiation()
        {
            
           // un_checked_classes = parserHelper.CodeNodesForEachClass.Keys.ToList();
            bool uni_association_i = false;
            bool uni_association_j = false;
            bool reflexive_assoiation_i = false;
            bool reflexive_assoiation_j = false;
            bool bi_association = false;
            Pen association_pen = new Pen(Color.Aqua);
            association_pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            //pen.EndCap = System.Drawing.Drawing2D.LineCap.Triangle;

            for (int i=0;i<un_checked_classes.Count;i++)
               
            {
                for(int j=0;j<un_checked_classes.Count;j++)
                {
                    if (i == j && un_checked_classes.Count !=1) { continue; }
                    for (int y = 0; y < un_checked_classes[i].ClassAttributes.Count; y++) {
                        if (un_checked_classes[i].ClassAttributes[y].Contains(un_checked_classes[i].ClassName)){
                            reflexive_assoiation_i = true;
                        }
                        if (un_checked_classes.Count>1&&un_checked_classes[i].ClassAttributes[y].Contains(un_checked_classes[j].ClassName) && un_checked_classes.Count!=1) {
                            uni_association_i = true;/*break;*/ 
                        }
                    }
                    for (int y=0; y<un_checked_classes[j].ClassAttributes.Count;y++)
                    {
                        if (uni_association_i == true && un_checked_classes.Count!=1) {
                            if (un_checked_classes.Count > 1 && un_checked_classes[j].ClassAttributes[y].Contains(un_checked_classes[i].ClassName))
                            {
                                bi_association = true;
                            }
                                }
                    }

                    
                        if ( bi_association == true)
                        {
                            uni_association_i = false;
                        uni_association_j = false;
                            bi_association = false;
                        //panelGraphics.DrawLine(Pens.Aqua, new Point(classes_pos[i].X+100, classes_pos[i].Y +12), new Point(classes_pos[j].X, classes_pos[j].Y+12));
                        if (i < j)
                        {
                            panelGraphics.DrawLine(Pens.Aqua, new Point(un_checked_classes[i].x + 100, un_checked_classes[i].y + 12), new Point(un_checked_classes[j].x, un_checked_classes[j].y + 12));
                        }

                        else if (j < i)
                        {
                            panelGraphics.DrawLine(Pens.Aqua, new Point(un_checked_classes[i].x, un_checked_classes[i].y + 12), new Point(un_checked_classes[j].x+100, un_checked_classes[j].y + 12));
                        }

                        }
                       
                        else if (uni_association_i == true && uni_association_j==false && bi_association == false)
                        {
                            uni_association_i = false;
                        //panelGraphics.DrawLine(association_pen, new Point(classes_pos[i].X+100, classes_pos[i].Y+12), new Point(classes_pos[j].X, classes_pos[j].Y+12));
                        if (i < j)
                        {
                            panelGraphics.DrawLine(association_pen, new Point(un_checked_classes[i].x + 100, un_checked_classes[i].y + 12), new Point(un_checked_classes[j].x, un_checked_classes[j].y + 12));
                        }

                        else if (j < i)
                        {
                            panelGraphics.DrawLine(association_pen, new Point(un_checked_classes[i].x , un_checked_classes[i].y + 12), new Point(un_checked_classes[j].x+100, un_checked_classes[j].y + 12));
                        }

                    }

                    else if (uni_association_j == true && uni_association_i==false && bi_association == false)
                    {
                        uni_association_j = false;
                        //panelGraphics.DrawLine(association_pen, new Point(classes_pos[j].X, classes_pos[j].Y+12), new Point(classes_pos[i].X+100, classes_pos[i].Y+12));
                        if (j<i)
                        {
                            panelGraphics.DrawLine(association_pen, new Point(un_checked_classes[j].x, un_checked_classes[j].y + 12), new Point(un_checked_classes[i].x + 100, un_checked_classes[i].y + 12));
                        }

                        else if (i < j)
                        {
                            panelGraphics.DrawLine(association_pen, new Point(un_checked_classes[j].x + 100, un_checked_classes[j].y + 12), new Point(un_checked_classes[i].x, un_checked_classes[i].y + 12));
                        }

                    }
                   
                    if (reflexive_assoiation_i == true)
                    {
                        reflexive_assoiation_i = false;
                            //panelGraphics.DrawLine(new Pen(Color.Aqua), new Point(classes_pos[i].X + classes_pos[i].Width / 2, classes_pos[i].Y + classz.Height-5), new Point(classes_pos[i].X + classz.Width / 2, classes_pos[i].Y + classz.Height + 6));
                            panelGraphics.DrawLine(new Pen(Color.Aqua), new Point(un_checked_classes[i].x + un_checked_classes[i].width / 2, un_checked_classes[i].y + un_checked_classes[i].height), new Point(un_checked_classes[i].x + un_checked_classes[i].width / 2, un_checked_classes[i].y + un_checked_classes[i].height + 6));
                            //panelGraphics.DrawLine(new Pen(Color.Aqua), new Point(classes_pos[i].X + classz.Width / 2, classes_pos[i].Y + classz.Height + 6), new Point(classes_pos[i].X + classz.Width + 6, classes_pos[i].Y + classz.Height + 6));
                            panelGraphics.DrawLine(new Pen(Color.Aqua), new Point(un_checked_classes[i].x + un_checked_classes[i].width / 2, un_checked_classes[i].y + un_checked_classes[i].height + 6), new Point(un_checked_classes[i].x + un_checked_classes[i].width + 6, un_checked_classes[i].y + un_checked_classes[i].height + 6));
                            //panelGraphics.DrawLine(new Pen(Color.Aqua), new Point(classes_pos[i].X + classz.Width + 6, classes_pos[i].Y + classz.Height + 6), new Point(classes_pos[i].X + classz.Width + 6, classes_pos[i].Y + classz.Height / 2));
                            panelGraphics.DrawLine(new Pen(Color.Aqua), new Point(un_checked_classes[i].x + un_checked_classes[i].width + 6, un_checked_classes[i].y + un_checked_classes[i].height + 6), new Point(un_checked_classes[i].x + un_checked_classes[i].width + 6, un_checked_classes[i].y + un_checked_classes[i].height / 2));
                            //panelGraphics.DrawLine(association_pen, new Point(classes_pos[i].X + classz.Width + 6, classes_pos[i].Y + classz.Height / 2), new Point(classes_pos[i].X + classz.Width, classes_pos[i].Y + classz.Height / 2));
                            panelGraphics.DrawLine(association_pen, new Point(un_checked_classes[i].x + un_checked_classes[i].width + 6, un_checked_classes[i].y + un_checked_classes[i].height / 2), new Point(un_checked_classes[i].x + un_checked_classes[i].width, un_checked_classes[i].y + un_checked_classes[i].height / 2));
                       
                    }
                    //}

                    //un_checked_classes.Remove(un_checked_classes[j]);
                    //}

                }
               // un_checked_classes.Remove(un_checked_classes[i]);
            }
        }
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="location"></param>
        /// <returns></returns>/////////////////////////////////////////////////////////////////////////////////////////////////////




        int GetOffset(RichTextBox textBox, ICSharpCode.NRefactory.TextLocation location)  /*Get Node From Here*/
        {
            return textBox.GetFirstCharIndexFromLine(location.Line - 1) + location.Column - 1;
        }

        bool SelectCurrentNode(TreeNodeCollection c)
        {
            int selectionStart = richTextBox1.SelectionStart;
            int selectionEnd = selectionStart + richTextBox1.SelectionLength;
            foreach (TreeNode t in c)
            {
                AstNode node = t.Tag as AstNode;
                if (node != null
                    && selectionStart >= GetOffset(richTextBox1, node.StartLocation)
                    && selectionEnd <= GetOffset(richTextBox1, node.EndLocation))
                {
                    if (selectionStart == selectionEnd
                        && (selectionStart == GetOffset(richTextBox1, node.StartLocation)
                            || selectionStart == GetOffset(richTextBox1, node.EndLocation)))
                    {
                        // caret is on border of this node; don't expand
                        csharpTreeView.SelectedNode = t;
                    }
                    else
                    {
                        t.Expand();
                        if (!SelectCurrentNode(t.Nodes))
                            csharpTreeView.SelectedNode = t;
                    }
                    return true;
                }
            }
            return false;
        }
        //-----------------------------------------------------------------------





        /// <param name="node"></param>
        /// <returns></returns>
        public string GetNodeTitle(AstNode node)
        {  // negrb node.child
            StringBuilder b = new StringBuilder();
            b.Append(node.Role.ToString());
            b.Append(": ");
            b.Append(node.GetType().Name);
            string TempNodeData = node.ToString();
            bool hasProperties = false;
            foreach (PropertyInfo p in node.GetType().GetProperties(System.Reflection.BindingFlags.Public | BindingFlags.Instance))
            {
                if (p.Name == "NodeType" || p.Name == "IsNull" || p.Name == "IsFrozen" || p.Name == "HasChildren")
                    continue;
                if (p.PropertyType == typeof(string) || p.PropertyType.IsEnum || p.PropertyType == typeof(bool))
                {
                    if (!hasProperties)
                    {
                        hasProperties = true;
                        b.Append(" (");
                    }
                    else
                    {
                        b.Append(", ");
                    }
                    b.Append(p.Name);
                    b.Append(" = ");
                    try
                    {
                        object val = p.GetValue(node, null);
                        b.Append(val != null ? val.ToString() : "**null**");
                    }
                    catch (TargetInvocationException ex)
                    {
                        b.Append("**" + ex.InnerException.GetType().Name + "**");
                    }
                }
            }
            if (hasProperties)
                b.Append(")");
            return b.ToString();
        }


        public TreeNode MakeTreeNode(AstNode node)
        {
            TreeNode t = new TreeNode(GetNodeTitle(node));
            t.Tag = node;
            foreach (AstNode child in node.Children)
            {
                t.Nodes.Add(MakeTreeNode(child));
            }

            return t;
        }


        public Form2()
        {
            InitializeComponent();

            panel1.AutoScroll = true;
            panel1.AutoScrollMinSize = new Size(1000, 1000);

        }

        public bool ClassDiagramClicked { get; set; }






        private void panel1_Paint(object sender, PaintEventArgs e)
        {

           

            if (ClassDiagramClicked)
            {
               
                panel1.AutoScroll = true;

                panel1.AutoScrollMinSize = new Size(10000, 10000);

                e.Graphics.Clear(Color.Black);
                //parserHelper.CodeNodes.Clear();
                parserHelper.CodeNodesForEachClass.Clear();
                y = 0;
                class_posx = 2;
                class_posy = 2;
                classes_pos.Clear();
                un_checked_classes.Clear();
                class_order = 0;

                e.Graphics.TranslateTransform(panel1.AutoScrollPosition.X, panel1.AutoScrollPosition.Y);
                panelGraphics = e.Graphics;

                

                DrawClassDiagram();
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClassDiagramClicked = true;
            panel1.Refresh();
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            gogo.aval_words();
        }

        ///////////////////////////////////////////////////////////////////////////

        public bool IsMeaningfull()
        {

            this.csharpTreeView = new System.Windows.Forms.TreeView();

            SyntaxTree syntaxTree = new CSharpParser().Parse(richTextBox1.Text.ToString());
            csharpTreeView.Nodes.Clear();

            foreach (var element in syntaxTree.Children)
            {
                csharpTreeView.Nodes.Add(MakeTreeNode(element));
                nodeEnumerator = csharpTreeView.Nodes.GetEnumerator();
            }

            SelectCurrentNode(csharpTreeView.Nodes);

            parserHelper.FillNodeList(nodeEnumerator, richTextBox1.Text);


            FunctionsInEachClass tempf = new FunctionsInEachClass();
            foreach (var el in parserHelper.CodeNodesForEachClass)
            {
                for (int i = 0; i < el.Value.Count; i++)
                {
                    if (el.Value[i].FunctionName == "Main")
                    {
                        //FunctionsInEachClass tempf = new FunctionsInEachClass();
                        tempf = el.Value[i];
                    }
                }
            }




            gogo.declerations_collector(tempf.nodesInEachFunction);
            gogo.detect_wrong();
            gogo.coloring_wrong(richTextBox1);
            return (gogo.wrong_names.Count == 0);
        }
        private void DrawClassDiagram()
        {
            if (IsMeaningfull()) { 
            if (!(richTextBox1.Text == "" || richTextBox1.Text == null))
            {
                this.csharpTreeView = new System.Windows.Forms.TreeView();

                SyntaxTree syntaxTree = new CSharpParser().Parse(richTextBox1.Text.ToString());
                csharpTreeView.Nodes.Clear();


                foreach (var element in syntaxTree.Children)
                {
                    csharpTreeView.Nodes.Add(MakeTreeNode(element));
                    nodeEnumerator = csharpTreeView.Nodes.GetEnumerator();
                }

                SelectCurrentNode(csharpTreeView.Nodes);

                parserHelper.FillNodeList(nodeEnumerator, richTextBox1.Text);
                // parserHelper.CodeNodes[0].ToString();

                // FunctionsInEachClass tempf = new FunctionsInEachClass();

                //class_code_nodes = parserHelper.CodeNodesForEachClass;

                //foreach (var el in parserHelper.CodeNodesForEachClass)
                //{
                //draw_class(el.Key.ClassName,el.Key.X,el.Key.Y, el.Key.ClassAttributes, el.Value);

                un_checked_classes = parserHelper.CodeNodesForEachClass.Keys.ToList();

                draw_class(/*parserHelper.CodeNodesForEachClass*/);
                draw_assosiation();
                //draw_class(&el);
                //}
            }

            else
            {
                MessageBox.Show("Please, Enter some classes!");
            }
        }
            else
            {
                MessageBox.Show("You Must Choose Meaningful Names For Your Variables First");

            }

        }

        ///////////////////////////////////////////////////////////////////////////
    }
}
