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
    public partial class Form1 : Form
    {


        public Graphics panelGraphics { get; set; }
        public bool dummy = false;
        public bool ifdummy = false;
        public bool is_while_child = false;
        public bool is_while_end_child = false;
        public bool is_not_last_child = false;
        public bool is_forz_child = false;
        public bool is_if_child = false;
        public bool is_IF_false_child = false;
        public bool is_IF_gogo = false;
        public bool is_while_stat = false;
        public List<Point> while_parent = new List<Point>();
        public List<Point> while_child = new List<Point>();
        public int incrementer = 0;
        public avalable_names gogo = new avalable_names();
        public bool is_condition_child = false;
        public bool condchild = false;
        public int last_position_of_y;
        public int last_position_of_x;
        public bool islast_position_of_y = false;
        public bool is_loop_child = false;
        public System.Windows.Forms.TreeView csharpTreeView;
        IEnumerator nodeEnumerator;
        ParserHelper parserHelper = new ParserHelper();
        public List<Point> parentz = new List<Point>();
        public List<Point> childz = new List<Point>();
        public List<Point> loop_parentz = new List<Point>();
        public List<Point> loop_childz = new List<Point>();
        public Dictionary<string, string> dict { get; set; }
        public int shiftxaxis = 250;
        public bool islastifchild = false;
        public bool isforloopchild = false;
        public bool forchildis = false;
        public bool true_child = false;
        Pen pen = new Pen(Color.Black);
        /****************************Flow Chart**************************************************/


        String drawString;

        /* Create font and brush. */

       public Font drawFont = new Font("Arial", 16);
        SolidBrush drawBrush = new SolidBrush(Color.Green);

        bool IS_child = false;

        public List<string> listofitems = new List<string>();
        public string stringItem;
        public bool parameter_type;
        public int differnce = 125;

        /****************************************rectangle****************************/

        public Rectangle _rectangle = new Rectangle(300, 20, 150, 50);
        public static int posx = 300;
        public int posy = 10;

        public int right_posx;


        public int rect_width = 150;
        public int rect_height = 50;
        public int prev_x = 0;

        public int true_count;
        public int false_count;

        public List<Point> end_items = new List<Point>();

        public Point saving_loop_point = new Point();

        /*****************************************************************************/
        /*********************************polygon************************************/


        public Point[] polygon_points = { new Point(posx + 75, 10), new Point(posx - 25, 60), new Point(posx + 75, 110), new Point(posx + 175, 60) };


        /*****************************End of Flow Chart***********************************************/

        int GetOffset(RichTextBox textBox, ICSharpCode.NRefactory.TextLocation location)  /*Get Node From Here*/
        {
            return textBox.GetFirstCharIndexFromLine(location.Line - 1) + location.Column - 1;
        }
        public bool IsMeaningfull() {

            this.csharpTreeView = new System.Windows.Forms.TreeView();

            SyntaxTree syntaxTree = new CSharpParser().Parse(TxtBox_InputCode.Text.ToString());
            csharpTreeView.Nodes.Clear();

            foreach (var element in syntaxTree.Children)
            {
                csharpTreeView.Nodes.Add(MakeTreeNode(element));
                nodeEnumerator = csharpTreeView.Nodes.GetEnumerator();
            }

            SelectCurrentNode(csharpTreeView.Nodes);

            parserHelper.FillNodeList(nodeEnumerator, TxtBox_InputCode.Text);


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
            gogo.coloring_wrong(TxtBox_InputCode);
            return (gogo.wrong_names.Count == 0);
        }
        bool SelectCurrentNode(TreeNodeCollection c)
        {
            int selectionStart = TxtBox_InputCode.SelectionStart;
            int selectionEnd = selectionStart + TxtBox_InputCode.SelectionLength;
            foreach (TreeNode t in c)
            {
                AstNode node = t.Tag as AstNode;
                if (node != null
                    && selectionStart >= GetOffset(TxtBox_InputCode, node.StartLocation)
                    && selectionEnd <= GetOffset(TxtBox_InputCode, node.EndLocation))
                {
                    if (selectionStart == selectionEnd
                        && (selectionStart == GetOffset(TxtBox_InputCode, node.StartLocation)
                            || selectionStart == GetOffset(TxtBox_InputCode, node.EndLocation)))
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
        public string dictionary(string input)
        {
            for (int i = 0; i < dict.Count - 1; i++)
            {
                if (input.Contains(dict.ElementAt(i).Key))
                {

                    StringBuilder x = new StringBuilder(input);
                    x.Replace(dict.ElementAt(i).Key, dict.ElementAt(i).Value);
                    input = x.ToString();
                    break;
                }
            }
            return input;
        }
        //-----------------------------------------------------------------------------------
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
        public string line;
        public Form1()
        {
            InitializeComponent();
            dict = new Dictionary<string, string>();
            var lines = File.ReadLines(@"dictionary.txt");
            foreach (var l in lines)
            {
                line = l.Replace("\t", "");
                string[] x = line.Split('|');
                dict.Add(x[0], x[1]);
            }
            /**Flow Chart**/
            right_posx = posx + 250;
            /**Flow Chart**/

            panel1.AutoScroll = true;
            panel1.AutoScrollMinSize = new Size(1000, 1000);
        }
        /**************************Flow Chart***************************/

        public void draw_statment(int pos_x, int pos_y)
        {
            //Graphics g = panel1.CreateGraphics();
            _rectangle = new Rectangle(pos_x, pos_y, rect_width, rect_height);


            drawString = stringItem;
            drawString = dictionary(drawString);
            bool changedsize = false;

            while (FindFont(panelGraphics, drawString, _rectangle.Size, drawFont).Size < 8)
            {
                _rectangle.Width *= 2;
                _rectangle.Height *= 2;
                changedsize = true;
            }

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            panelGraphics.DrawRectangle(Pens.White, _rectangle);



            /*********************************************************************/
            if (changedsize == false)
            {

                panelGraphics.DrawString(drawString, FindFont(panelGraphics, drawString, _rectangle.Size, drawFont), drawBrush, _rectangle, stringFormat);

            }

            else
            {
                panelGraphics.DrawString(drawString, drawFont, drawBrush, _rectangle, stringFormat);
            }
            /*********************************************************************/



            posy += 125;
            polygon_points[0].Y += 125;
            polygon_points[1].Y += 125;
            polygon_points[3].Y += 125;
            polygon_points[2].Y += 125;
            parameter_type = true;
        }
        public void draw_condition(Point pos1, Point pos2, Point pos3, Point pos4)
        {

            Point[] points = { pos1, pos2, pos3, pos4 };
            //Graphics g = panel1.CreateGraphics();
            panelGraphics.DrawPolygon(Pens.Violet, points);


            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            drawString = stringItem;
            drawString = dictionary(drawString);
            panelGraphics.DrawString(drawString, drawFont, drawBrush,/*posx+75*/pos1.X, posy + 50, stringFormat);
            polygon_points[0].Y += 125;
            polygon_points[1].Y += 125;
            polygon_points[3].Y += 125;
            polygon_points[2].Y += 125;
            posy += 125;

            parameter_type = false;
        }
        public void draw_loop(Point pos1, Point pos2, Point pos3, Point pos4)
        {

            Point[] points = { pos1, pos2, pos3, pos4 };
            //Graphics g = panel1.CreateGraphics();
            panelGraphics.DrawPolygon(Pens.Blue, points);


            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            drawString = stringItem;
            drawString = dictionary(drawString);
            panelGraphics.DrawString(drawString, drawFont, drawBrush,/* posx + 75, posy + 50*/pos1.X, posy + 50, stringFormat);
            polygon_points[0].Y += 125;
            polygon_points[1].Y += 125;
            polygon_points[3].Y += 125;
            polygon_points[2].Y += 125;
            posy += 125;
            parameter_type = false;
        }

        public void start()
        {

            //Graphics g = panel1.CreateGraphics();
            Pen pen = new Pen(Color.Aquamarine, 1);
            panelGraphics.DrawEllipse(pen, 300, 10, 150, 50);
            drawString = "Start";
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            panelGraphics.DrawString(drawString, drawFont, drawBrush, posx + 75, posy + 25, stringFormat);

            posy += 125;
            polygon_points[0].Y += 125;
            polygon_points[1].Y += 125;
            polygon_points[3].Y += 125;
            polygon_points[2].Y += 125;

        }

        public void end()
        {

            //Graphics g = panel1.CreateGraphics();
            Pen pen = new Pen(Color.Yellow, 1);
            panelGraphics.DrawEllipse(pen, posx, posy, 150, 50);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            drawString = "End";
            panelGraphics.DrawString(drawString, drawFont, drawBrush, posx + 75, posy + 25, stringFormat);

        }

        /**************************************************************************/

        public bool child_if(List<Node> childlist)
        {
            for (int i = 0; i < childlist.Count; i++)
            {
                if (childlist[i].Type == StatementTypes.Condition)
                {
                    return true;
                }
            }
            return false;
        }
        public bool child_loop(List<Node> childlist)
        {
            for (int i = 0; i < childlist.Count; i++)
            {
                if (childlist[i].Type == StatementTypes.ForLoop) //kant statementtypes.loop
                {
                    return true;
                }
            }
            return false;
        }
        /*****************************************************************************/

        public void draw_arrow(int pos_x1, int pos_y1, int pos_x2, int pos_y2, string state)
        {
            //Graphics g = panel1.CreateGraphics();
            pen = new Pen(Color.Red);
            pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;



            if (pos_x1 != pos_x2 && pos_y1 < pos_y2)
            {
                if (pos_x2 < pos_x1)
                {
                    panelGraphics.DrawString(state, drawFont, drawBrush, pos_x1 + ((pos_x1 - pos_x2) / 2), pos_y1 - 15, stringFormat);
                }
                else { panelGraphics.DrawString(state, drawFont, drawBrush, pos_x1 + ((pos_x2 - pos_x1) / 2), pos_y1 - 15, stringFormat); }
                panelGraphics.DrawLine(Pens.Red, pos_x1, pos_y1, pos_x2, pos_y1);
                panelGraphics.DrawLine(pen, pos_x2, pos_y1, pos_x2, pos_y2);

            }
            else if (pos_x1 != pos_x2 && pos_y1 > pos_y2)
            {
                if (pos_x2 < pos_x1)
                {
                    panelGraphics.DrawString(state, drawFont, drawBrush, pos_x1 + ((pos_x1 - pos_x2) / 2), pos_y1 + 15, stringFormat);
                }
                else { panelGraphics.DrawString(state, drawFont, drawBrush, pos_x1 + ((pos_x2 - pos_x1) / 2), pos_y1 + 15, stringFormat); }
                // panelGraphics.DrawString(drawString, drawFont, drawBrush, posx + 75, posy + 25, stringFormat);
                panelGraphics.DrawLine(Pens.Red, pos_x1, pos_y1, pos_x1, pos_y2);
                panelGraphics.DrawLine(pen, pos_x1, pos_y2, pos_x2, pos_y2);
            }

            else
            {
                if (pos_y1 < pos_y2)
                {
                    panelGraphics.DrawString(state, drawFont, drawBrush, pos_x1 - 40, (pos_y1 + ((pos_y2 - pos_y1) / 2)) - 5, stringFormat);
                }
                else { panelGraphics.DrawString(state, drawFont, drawBrush, pos_x1 - 40, (pos_y1 + ((pos_y1 - pos_y2) / 2)) - 5, stringFormat); }
                //panelGraphics.DrawString(state, drawFont, drawBrush, posx + 75, posy + 25, stringFormat);
                panelGraphics.DrawLine(pen, pos_x1, pos_y1, pos_x2, pos_y2);
            }

            pen.Dispose();
        }


        private Font FindFont(System.Drawing.Graphics g, string longString, Size Room, Font PreferedFont)
        {
            //you should perform some scale functions!!!
            SizeF RealSize = g.MeasureString(longString, PreferedFont);
            RealSize.Width = RealSize.Width / 2; // Assuming we can write 2 lines per rectangle with the default font
            if (Room.Width >= RealSize.Width)
            {
                // If it fits don't change the font
                return PreferedFont;
            }

            //float HeightScaleRatio = Room.Height / RealSize.Height;
            float WidthScaleRatio = Room.Width / RealSize.Width;
            //float ScaleRatio = (HeightScaleRatio < WidthScaleRatio) ? ScaleRatio = HeightScaleRatio : ScaleRatio = WidthScaleRatio;
            float ScaleRatio = ScaleRatio = WidthScaleRatio;
            float ScaleFontSize = PreferedFont.Size * ScaleRatio;
            return new Font(PreferedFont.FontFamily, ScaleFontSize);
        }

        /******************************************************************************************/

        public Point draw_end_arrows(List<Point> p)
        {
            Point last_y = new Point();
            last_y.X = 0;
            last_y.Y = 0;
            for (int i = 0; i < p.Count; i++)
            {
                if (p[i].Y > last_y.Y)
                {
                    last_y.Y = p[i].Y;
                    last_y.X = p[i].X;
                }
            }
            return last_y;
        }

        /***************************************************************************************/

        public int children_number(List<Node> children)
        {

            for (int i = 0; i < children.Count; i++)
            {

                //if (children[i].TrueChildren.Count != 0) { incrementer += children[i].TrueChildren.Count; }
                if (children[i].TrueChildren.Count != 0) { children_number(children[i].TrueChildren); }
                //if (children[i].FalseChildren.Count != 0) { incrementer += children[i].FalseChildren.Count; }
                if (children[i].FalseChildren.Count != 0) { children_number(children[i].FalseChildren); }
                incrementer++;

            }
            return incrementer;
        }

        /**************************************************************************************/

        public void draw_last_arrows(List<Point> p, string state)
        {
            Point last_y = draw_end_arrows(p);
            for (int i = 0; i < p.Count; i++)
            {
                draw_arrow(p[i].X + (_rectangle.Width / 2), p[i].Y - differnce + (_rectangle.Height), posx + 75/*last_y.X+75*/, /*posy*/last_y.Y, state);
            }
        }

        /***********************************************************************************************/
        public void true_arrows(List<Point> parent, List<Point> child, string state)
        {

            // -----------------------------------el true arrows

            draw_arrow(parent[parent.Count - 1].X, parent[parent.Count - 1].Y, child[child.Count - 1].X, child[child.Count - 1].Y, state);
            parent.Remove(parent[parent.Count - 1]);
            child.Remove(child[child.Count - 1]);

        }

        public void loop_true_arrows(List<Point> loopz_per, List<Point> loopz_chi, string state)
        {
            if (loop_childz.Count != 0)
            {
                draw_arrow(loopz_per[loopz_per.Count - 1].X, loopz_per[loopz_per.Count - 1].Y, loopz_chi[loopz_chi.Count - 1].X, loopz_chi[loopz_chi.Count - 1].Y, state);
                loopz_per.Remove(loopz_per[loopz_per.Count - 1]);
                loopz_chi.Remove(loopz_chi[loopz_chi.Count - 1]);
            }
            else if (loop_childz.Count == 0)
            {
                IS_child = false;
            }
        }

        public void while_true_arrows(List<Point> whilez_per, List<Point> whilez_chi, string state)
        {
            draw_arrow(whilez_per[whilez_per.Count - 1].X, whilez_per[whilez_per.Count - 1].Y, whilez_chi[whilez_chi.Count - 1].X, whilez_chi[whilez_chi.Count - 1].Y, state);
            whilez_per.Remove(whilez_per[whilez_per.Count - 1]);
            //whilez_chi.Remove(whilez_chi[whilez_chi.Count - 1]);

        }
        /***********************************************************************************************/
        public void drawing(List<Node> x, int local_posx, int local_right_posx)
        {



            for (int i = 0; i < x.Count; i++)
            {
                stringItem = x[i].CodeLine;

                if (x[i].Type == StatementTypes.Condition)
                {
                    is_IF_false_child = false;

                    is_if_child = true;
                    if (is_condition_child == true)
                    {
                        condchild = true;
                        draw_condition(new Point(local_posx + 75, polygon_points[0].Y), new Point(local_posx - 25, polygon_points[1].Y), new Point(local_posx + 75, polygon_points[2].Y), new Point(local_posx + 175, polygon_points[3].Y));

                        if (is_while_child == true && while_child.Count != 0)
                        {
                            if (i == x.Count - 1)
                            {
                                while_parent.Add(new Point(local_posx + 175, polygon_points[3].Y - differnce));
                                while_true_arrows(while_parent, while_child, "");
                                is_while_end_child = true;
                            }

                            else
                            {
                                draw_arrow(local_posx + 75, polygon_points[2].Y - differnce, local_posx + 75, posy + (children_number(x[i].TrueChildren) * 125), "False");
                                incrementer = 0;
                                is_not_last_child = true;

                            }

                        }

                    }
                    else if (is_condition_child == false)
                    {
                        condchild = false;
                        draw_condition(polygon_points[0], polygon_points[1], polygon_points[2], polygon_points[3]);
                        is_while_child = false;
                    }

                    if (i == 0 && true_child == true)
                    {
                        childz.Add(new Point(local_posx + 75, polygon_points[0].Y - 125));/////law 2olna x[i].falsechildren.count*125 bygeb el x bta3t tany if ely hia child aslun msh akbar if
                        true_arrows(parentz, childz, "true");
                        true_child = false;
                    }


                    if (i != (x.Count - 1))
                    {
                        if (is_condition_child == true)
                        {
                            //------------------false
                            //draw_arrow(local_posx + 175, polygon_points[3].Y - differnce, local_right_posx + (_rectangle.Width / 2), posy + (x[i].FalseChildren.Count) * 125,"true");
                            //---------------------

                            parentz.Add(new Point(local_posx + 175, polygon_points[3].Y - 125));
                            if (x[i].FalseChildren.Count != 0)
                            {
                                is_IF_gogo = true;
                                //-------------false
                                draw_arrow(local_posx + 75, polygon_points[2].Y - differnce, local_posx + 75, posy, "false");
                            }
                            //draw_arrow(local_posx + 75, polygon_points[2].Y - differnce/*150 elmfrod difference*/, local_posx + 75, posy);
                            //is_condition_child = false;     //kanet m3mola false msh 3aref leh 
                        }

                        else if (is_condition_child == false)
                        {
                            //draw_arrow(polygon_points[3].X, polygon_points[3].Y - differnce, local_right_posx + (_rectangle.Width / 2), posy + (x[i].FalseChildren.Count) * 125);
                            parentz.Add(new Point(polygon_points[3].X, polygon_points[3].Y - 125));
                            if (x[i].FalseChildren.Count != 0)
                            {
                                //-------------false
                                draw_arrow(polygon_points[2].X, polygon_points[2].Y - differnce, local_posx + 75, posy, "false");
                            }

                        }

                    }

                    else
                    {
                        if (IS_child == true)
                        {
                            if (islast_position_of_y == true)
                            {
                                last_position_of_y = posy;


                            }

                            if (islast_position_of_y == false)
                            {
                                last_position_of_y = posy;
                                posy = last_position_of_y;

                            }

                        }


                        if (is_condition_child == true)
                        {
                            if (x[i].TrueChildren.Count != 0)
                            {
                                parentz.Add(new Point(local_posx + 175, polygon_points[3].Y - 125));
                                //draw_arrow(local_posx + 175, polygon_points[3].Y - differnce, local_right_posx + 75, posy + (x[i].FalseChildren.Count * 125)/* + differnce*/);
                            }
                            if (x[i].FalseChildren.Count != 0)
                            {
                                //-------------------false
                                draw_arrow(local_posx + 75, polygon_points[2].Y - differnce, local_posx + 75, posy, "false");
                            }
                            //is_condition_child = false;             kant brdo m3mola false msh fahm leh !!

                        }

                        else if (is_condition_child == false)
                        {
                            if (x[i].TrueChildren.Count != 0)
                            {
                                parentz.Add(new Point(polygon_points[3].X, polygon_points[3].Y - 125));
                                //draw_arrow(polygon_points[3].X, polygon_points[3].Y - differnce, local_right_posx + 75, posy + (x[i].FalseChildren.Count * 125) /*+ differnce*/); //e7na hnrg el diff
                            }
                            if (x[i].FalseChildren.Count != 0)
                            {
                                //----------------false
                                draw_arrow(polygon_points[2].X, polygon_points[2].Y - differnce, local_posx + 75, posy, "False");
                            }

                        }


                    }



                    if (x[i].FalseChildren.Count != 0)
                    {
                        is_IF_false_child = true;
                        islast_position_of_y = true;
                        IS_child = true;

                        //------------------------false

                        ifdummy = true;
                        drawing(x[i].FalseChildren, local_posx, local_right_posx);

                        //
                        //draw_arrow(local_posx + 75, polygon_points[2].Y - differnce, local_posx + 75, posy + (children_number(x[i].TrueChildren) * 125), "False");

                        if (i == x.Count - 1)
                        {
                            IS_child = false;

                        }
                    }
                    else if (is_while_child == true)
                    {
                        is_while_stat = true;
                        if (is_while_end_child == true)
                        {
                            is_while_child = false;
                            is_while_end_child = false;
                        }

                    }

                    else if (x[i].FalseChildren.Count == 0 && condchild == false)
                    {
                        //end_items.Add(new Point(local_right_posx - 175 - (_rectangle.Width / 2), polygon_points[2].Y - (_rectangle.Height)));
                        is_while_child = false;
                        draw_arrow(local_posx + 75, polygon_points[2].Y - differnce, local_posx + 75, posy + (children_number(x[i].TrueChildren) * 125), "False");
                        incrementer = 0;
                    }
                    else if (x[i].FalseChildren.Count == 0 && condchild == true && is_forz_child == false && i != x.Count - 1)
                    {
                        is_while_child = false;
                        islastifchild = true;
                        draw_arrow(local_posx + 75, polygon_points[2].Y - differnce, local_posx + 75, posy + (children_number(x[i].TrueChildren) * 125), "False");
                        incrementer = 0;
                    }
                    else if (x[i].FalseChildren.Count == 0 && condchild == true && is_forz_child == false)
                    {
                        if ((local_posx + 75) != (posx + 75))
                        {
                            is_while_child = false;
                            draw_arrow(local_posx + 75, polygon_points[2].Y - differnce, posx + 75, polygon_points[2].Y - differnce, "False");
                        }
                        else
                        {
                            is_while_child = false;
                            draw_arrow(local_posx + 75, polygon_points[2].Y - differnce, local_posx + 75, posy + (children_number(x[i].TrueChildren) * 125), "False");
                            incrementer = 0;
                        }
                    }
                    else if (x[i].FalseChildren.Count == 0 && condchild == true && is_forz_child == true)
                    {
                        is_while_child = false;
                        draw_arrow(local_posx + 75, polygon_points[2].Y - differnce, local_posx + 75, posy + (children_number(x[i].TrueChildren) * 125), "False");
                        incrementer = 0;
                    }
                    else if (x[i].FalseChildren.Count != 0 && condchild == true)
                    {
                        is_while_child = false;
                        ///mt3lsh 7aga
                    }


                    if (x[i].TrueChildren.Count != 0)
                    {

                        IS_child = true;
                        islast_position_of_y = false;

                        is_condition_child = true;

                        true_child = true;
                        ifdummy = true;
                        drawing(x[i].TrueChildren, local_right_posx, local_right_posx + (shiftxaxis));
                        //end_items.Add(new Point(local_right_posx, posy));


                        if (i == x.Count - 1)
                        {
                            IS_child = false;

                        }




                    }



                }

                else if (x[i].Type == StatementTypes.ForLoop /*&& dummy == false*/)
                {
                    isforloopchild = true;
                    if (is_while_child == false && is_if_child == false)
                    {
                        is_forz_child = true;
                    }

                    if (is_condition_child == true)
                    {

                        draw_loop(new Point(local_posx + 75, polygon_points[0].Y), new Point(local_posx - 25, polygon_points[1].Y), new Point(local_posx + 75, polygon_points[2].Y), new Point(local_posx + 175, polygon_points[3].Y));
                        //saving_loop_point = new Point(local_posx + 75, polygon_points[0].Y-125);
                        loop_childz.Add(new Point(local_posx + 75, polygon_points[0].Y - 125));

                        if (is_while_child == true && while_child.Count != 0)
                        {
                            if (i == x.Count - 1)
                            {
                                while_parent.Add(new Point(local_posx + 175, polygon_points[3].Y - differnce));  // kant: while_parent.Add(new Point(local_posx + 75, polygon_points[2].Y));
                                while_true_arrows(while_parent, while_child, "");
                                is_while_child = false;
                            }
                            else
                            {
                                draw_arrow(local_posx + 75, polygon_points[2].Y - differnce, /*posx*/ local_posx + 75, (posy) + (children_number(x[i].TrueChildren) * 125), ""/* + differnce*/);
                                incrementer = 0;
                            }

                        }

                    }
                    else if (is_condition_child == false)
                    {
                        draw_loop(polygon_points[0], polygon_points[1], polygon_points[2], polygon_points[3]);
                        //saving_loop_point =new Point( polygon_points[0].X,polygon_points[0].Y-125);
                        loop_childz.Add(new Point(polygon_points[0].X, polygon_points[0].Y - 125));
                    }
                    if (i != x.Count - 1)
                    {
                        if (x[i].TrueChildren.Count != 0)
                        {

                            draw_arrow(local_posx + 175, polygon_points[3].Y - differnce, local_right_posx + 75, posy + (x[i].FalseChildren.Count * 125), "True"/* + differnce*/);
                            // draw_arrow(local_posx + 75, polygon_points[2].Y - differnce, local_posx + 75, posy + (x[i].TrueChildren.Count * 125));
                        }

                        if (x[i].FalseChildren.Count == 0 && is_condition_child == true && is_if_child == true)
                        {
                            if (i == x.Count - 1)
                            {
                                draw_arrow(local_posx + 75, polygon_points[2].Y - differnce, posx + 75, polygon_points[2].Y - differnce, "False");
                            }
                            else if (i != x.Count - 1)
                            {
                                draw_arrow(local_posx + 75, polygon_points[2].Y - differnce, /*posx*/ local_posx + 75, (posy) + (children_number(x[i].TrueChildren) * 125), ""/* + differnce*/);
                                incrementer = 0;
                            }
                        }
                        else if ((x[i].FalseChildren.Count == 0 && is_condition_child == false) || (is_forz_child == true && IS_child == true))
                        {

                            draw_arrow(local_posx + 75, polygon_points[2].Y - differnce, /*posx*/ local_posx + 75, (posy) + (children_number(x[i].TrueChildren) * 125), ""/* + differnce*/);
                            incrementer = 0;

                        }

                        if (x[i].TrueChildren.Count != 0)
                        {

                            IS_child = true;
                            islast_position_of_y = false;

                            is_condition_child = true;
                            is_loop_child = true;
                            //forchildis = child_if(x[i].TrueChildren);
                            forchildis = child_loop(x[i].TrueChildren);
                            dummy = false;
                            ifdummy = false;
                            is_forz_child = true;
                            drawing(x[i].TrueChildren, local_right_posx, local_right_posx + 250);
                            dummy = false;
                            is_forz_child = true;
                            ///maybefalse
                            //is_loop_child = false;
                            //////////////

                            //kant gwa el if we sha3'ala
                            //IS_child = false;
                            ///////////

                            //end_items.Add(new Point(local_right_posx, posy));
                            if (i == x.Count - 1 && is_forz_child == false)
                            {
                                // is_loop_child = false;
                                isforloopchild = false;
                                IS_child = false; //kant msh gwa el if we btdrb 
                            }


                        }

                    }
                    else if (i == x.Count - 1)
                    {
                        if (x[i].TrueChildren.Count != 0)
                        {

                            draw_arrow(local_posx + 175, polygon_points[3].Y - differnce, local_right_posx + 75, posy + (x[i].FalseChildren.Count * 125), "True"/* + differnce*/);

                        }
                        if (x[i].FalseChildren.Count == 0 && is_condition_child == true && is_if_child == true && is_IF_false_child == true)
                        {
                            draw_arrow(local_posx + 75, polygon_points[2].Y - differnce, (local_posx + 75) - 250, polygon_points[2].Y - differnce, "False");
                        }
                        if (x[i].FalseChildren.Count == 0 && is_condition_child == true && is_if_child == true && is_IF_false_child == false)
                        {
                            draw_arrow(local_posx + 75, polygon_points[2].Y - differnce, posx + 75, polygon_points[2].Y - differnce, "False");
                        }
                        else if (x[i].FalseChildren.Count == 0 && is_condition_child == true && is_if_child == false)
                        {
                            if (posx == local_posx)
                            {
                                draw_arrow(local_posx + 75, polygon_points[2].Y - differnce, /*posx*/ local_posx + 75, (posy) + (children_number(x[i].TrueChildren) * 125), ""/* + differnce*/);
                                incrementer = 0;
                            }
                        }

                        else if ((x[i].FalseChildren.Count == 0 && is_condition_child == false) || (is_forz_child == true && IS_child == true))
                        {
                            draw_arrow(local_posx + 75, polygon_points[2].Y - differnce, /*posx*/ local_posx + 75, (posy) + (children_number(x[i].TrueChildren) * 125), ""/* + differnce*/);
                            incrementer = 0;
                            //end_items.Add(new Point(local_right_posx - 175 - (_rectangle.Width / 2), polygon_points[2].Y - (_rectangle.Height))); /*<!> msh efficent <!>*/
                        }


                        if (x[i].TrueChildren.Count != 0)
                        {

                            IS_child = true;
                            islast_position_of_y = false;

                            is_condition_child = true;
                            is_loop_child = true;
                            //forchildis = child_if(x[i].TrueChildren);
                            forchildis = child_loop(x[i].TrueChildren);
                            dummy = false;
                            ifdummy = false;
                            is_forz_child = true;
                            drawing(x[i].TrueChildren, local_right_posx, local_right_posx + 250);
                            //false
                            is_forz_child = true;
                            dummy = false;
                            //is_loop_child = false;
                            //////

                            //////false
                            //IS_child = false;
                            ///////
                            //end_items.Add(new Point(local_right_posx, posy));
                            if (i == x.Count - 1 && is_forz_child == false)
                            {
                                //is_loop_child = false;
                                IS_child = false;  //mkntsh gwa if 
                            }
                        }

                    }





                }
                else if (x[i].Type == StatementTypes.WhileLoop/* && dummy == true*/)
                {
                    is_while_child = true;
                    if (is_condition_child == true)
                    {

                        draw_loop(new Point(local_posx + 75, polygon_points[0].Y), new Point(local_posx - 25, polygon_points[1].Y), new Point(local_posx + 75, polygon_points[2].Y), new Point(local_posx + 175, polygon_points[3].Y));
                        while_child.Add(new Point(local_posx + 75, polygon_points[0].Y - 125));


                    }

                    else if (is_while_child == true && while_child.Count != 0 && IS_child == true)
                    {

                        while_parent.Add(new Point(local_posx + 175, polygon_points[3].Y));
                        while_true_arrows(while_parent, while_child, "");
                        //is_while_child = false;


                    }

                    else if (is_condition_child == false)
                    {

                        draw_loop(polygon_points[0], polygon_points[1], polygon_points[2], polygon_points[3]);
                        while_child.Add(new Point(polygon_points[0].X, polygon_points[0].Y - 125));

                    }
                    if (i == 0)
                    {
                        if (true_child == true)
                        {
                            childz.Add(new Point(local_posx + 75, polygon_points[0].Y - differnce));
                            true_arrows(parentz, childz, "True");
                            true_child = false;
                        }
                    }
                    if (i != x.Count - 1)
                    {
                        if (x[i].TrueChildren.Count != 0)
                        {

                            draw_arrow(local_posx + 175, polygon_points[3].Y - differnce, local_right_posx + 75, posy + (x[i].FalseChildren.Count * 125), "True"/* + differnce*/);
                            // draw_arrow(local_posx + 75, polygon_points[2].Y - differnce, local_posx + 75, posy + (x[i].TrueChildren.Count * 125));

                        }
                        if (x[i].FalseChildren.Count == 0)
                        {


                            draw_arrow(local_posx + 75, polygon_points[2].Y - differnce, local_posx + 75, posy + (children_number(x[i].TrueChildren) * 125), "False");
                            incrementer = 0;

                        }
                        if (true_child == true)
                        {
                            childz.Add(new Point(local_posx, posy - 125));
                            true_child = false;
                        }
                        if (x[i].TrueChildren.Count != 0)
                        {

                            IS_child = true;
                            islast_position_of_y = false;

                            is_condition_child = true;
                            is_loop_child = true;
                            //forchildis = child_if(x[i].TrueChildren);
                            forchildis = child_loop(x[i].TrueChildren);
                            dummy = true;
                            drawing(x[i].TrueChildren, local_right_posx, local_right_posx + 250);

                            ///maybefalse
                            //is_loop_child = false;
                            //////////////

                            //kant gwa el if we sha3'ala
                            //IS_child = false;
                            ///////////

                            // end_items.Add(new Point(local_right_posx, posy));
                            if (i == x.Count - 1)
                            {
                                // is_loop_child = false;
                                isforloopchild = false;
                                IS_child = false; //kant msh gwa el if we btdrb 
                            }


                        }



                    }
                    else if (i == x.Count - 1)
                    {
                        if (x[i].TrueChildren.Count != 0)
                        {


                            draw_arrow(local_posx + 175, polygon_points[3].Y - differnce, local_right_posx + 75, posy + (x[i].FalseChildren.Count * 125), "True"/* + differnce*/);

                        }


                        if (x[i].FalseChildren.Count == 0)
                        {

                            if ((is_while_child == true &&/*IS_child == false*/dummy == false && is_if_child == false))
                            {


                                draw_arrow(local_posx + 75, polygon_points[2].Y - differnce, local_posx + 75, posy + (children_number(x[i].TrueChildren) * 125), "False");
                                incrementer = 0;
                            }
                            if ((is_while_child == true && is_if_child == true && ifdummy == false))
                            {

                                draw_arrow(local_posx + 75, polygon_points[2].Y - differnce, local_posx + 75, posy + (children_number(x[i].TrueChildren) * 125), "False");
                                incrementer = 0;

                            }
                            //////////////////////////////////////////////////////////////////////  gogo byfty
                            if ((is_while_child == true && is_if_child == true && ifdummy == true))
                            {

                                draw_arrow(local_posx + 75, polygon_points[2].Y - differnce, posx + 75, polygon_points[2].Y - differnce, "False");

                            }
                            ////////////////////////////////////////////////////////////////////
                        }

                        if (x[i].TrueChildren.Count != 0)
                        {

                            IS_child = true;
                            islast_position_of_y = false;

                            is_condition_child = true;
                            is_loop_child = true;
                            //forchildis = child_if(x[i].TrueChildren);
                            forchildis = child_loop(x[i].TrueChildren);
                            dummy = true;

                            drawing(x[i].TrueChildren, local_right_posx, local_right_posx + 250);
                            //false
                            //is_loop_child = false;
                            //////

                            //////false
                            //IS_child = false;
                            ///////
                            // end_items.Add(new Point(local_right_posx, posy));
                            if (i == x.Count - 1)
                            {
                                //is_loop_child = false;
                                IS_child = false;  //mkntsh gwa if 
                            }
                        }

                    }

                }

                else
                {

                    if (x[i].Type == StatementTypes.Statement || x[i].Type == StatementTypes.PreIncrement || x[i].Type == StatementTypes.PostIncrement || x[i].Type == StatementTypes.AddOperator || x[i].Type == StatementTypes.Assign || x[i].Type == StatementTypes.VariableDeclaration)

                    {
                        draw_statment(local_posx, posy);



                        if (i == x.Count - 1)
                        {

                            if (IS_child == true)
                            {

                                /*if (IS_child == true && (isforloopchild == true) && forchildis == true)
                                {


                                    is_loop_child = false;
                                    isforloopchild = false;




                                }*/
                                //if (ifdummy == true&&condchild==true)
                                if (ifdummy == true)
                                {
                                    if (is_while_stat == true)
                                    {
                                        if (is_not_last_child == true)
                                        {
                                            draw_arrow(local_posx + 75, (_rectangle.Y + _rectangle.Height), (local_posx + 75) - 250, (_rectangle.Y + _rectangle.Height), "");
                                            is_while_stat = false;
                                            dummy = false;
                                            is_not_last_child = false;
                                        }
                                        else
                                        {
                                            while_parent.Add(new Point(_rectangle.X + (_rectangle.Width), _rectangle.Y + (_rectangle.Height / 2)));
                                            while_true_arrows(while_parent, while_child, "");
                                            //is_while_child = false;
                                            //dummy = false;
                                            is_while_stat = false;
                                            dummy = false;
                                        }
                                    }
                                    else if (is_forz_child == true && condchild == true || islastifchild == true)
                                    {
                                        if (is_IF_gogo == true)
                                        {
                                            draw_arrow(_rectangle.X + (_rectangle.Width / 2), _rectangle.Y + _rectangle.Height, local_posx + (_rectangle.Width / 2), posy, "");
                                            is_IF_gogo = false;
                                        }
                                        else
                                        {
                                            draw_arrow(local_posx + 75, (_rectangle.Y + _rectangle.Height), (local_posx + 75) - 250, (_rectangle.Y + _rectangle.Height), "");
                                            islastifchild = false;
                                        }
                                    }
                                    else if (is_IF_false_child == false && is_while_child == false)
                                    {
                                        draw_arrow(local_posx + 75, (_rectangle.Y + _rectangle.Height), posx + 75, (_rectangle.Y + _rectangle.Height), "");
                                    }

                                    else if (is_IF_false_child == true)
                                    {
                                        draw_arrow(_rectangle.X + (_rectangle.Width / 2), _rectangle.Y + _rectangle.Height, local_posx + (_rectangle.Width / 2), posy, "");
                                        // draw_arrow(local_posx + 75, polygon_points[2].Y - differnce, local_posx + 75, posy + (children_number(x[i-1].TrueChildren) * 125), "False");
                                        is_IF_false_child = false;
                                    }

                                    is_loop_child = false;
                                    isforloopchild = false;

                                    ifdummy = false;
                                    //is_condition_child = false;    nfs moshkla

                                }
                                /* else if (IS_child == true && (isforloopchild == false) && forchildis == true)
                                 {
                                     is_loop_child = true;
                                 }*/
                                else if (ifdummy == false)
                                {
                                    is_loop_child = true;
                                    //ifdummy = true;
                                    //is_condition_child = false;
                                    is_if_child = false;
                                }

                                //is_loop_child = true;
                                if (is_loop_child == true &&/* loop_childz.Count != 0&&*/dummy == false)  //el dummy mo2ktan
                                {
                                    loop_parentz.Add(new Point(_rectangle.X + (_rectangle.Width), _rectangle.Y + (_rectangle.Height / 2)));
                                    loop_true_arrows(loop_parentz, loop_childz, "");
                                    dummy = false;
                                    //is_condition_child = false;
                                    is_forz_child = false;

                                }


                                if (is_while_child == true && dummy == true && while_child.Count != 0)                //el dummy mo2ktan 
                                {
                                    while_parent.Add(new Point(_rectangle.X + (_rectangle.Width), _rectangle.Y + (_rectangle.Height / 2)));
                                    while_true_arrows(while_parent, while_child, "");
                                    is_while_child = false;
                                    dummy = false;

                                }


                                if (islast_position_of_y == true)
                                {
                                    last_position_of_y = posy;



                                }
                                if (IS_child == false)
                                {
                                    if (local_posx == posx)
                                    {
                                        draw_arrow(_rectangle.X + (_rectangle.Width / 2), _rectangle.Y + _rectangle.Height, local_posx + (_rectangle.Width / 2), posy, "");
                                    }
                                    else if (local_posx != posx)
                                    {
                                        if (is_while_child == true)
                                        {
                                            while_parent.Add(new Point(_rectangle.X + (_rectangle.Width), _rectangle.Y + (_rectangle.Height / 2)));
                                            while_true_arrows(while_parent, while_child, "");
                                            is_while_child = false;
                                            dummy = false;
                                        }
                                        else
                                        {
                                            draw_arrow(local_posx + 75, (_rectangle.Y + _rectangle.Height), posx + 75, (_rectangle.Y + _rectangle.Height), "");
                                        }
                                    }
                                }
                                if (islast_position_of_y == false)
                                {
                                    last_position_of_y = posy;
                                    posy = last_position_of_y;


                                }




                            }


                            else if (IS_child == false)
                            {
                                draw_arrow(_rectangle.X + (_rectangle.Width / 2), _rectangle.Y + _rectangle.Height, local_posx + (_rectangle.Width / 2), posy, "");
                            }







                        }




                        else
                        {

                            draw_arrow(_rectangle.X + (_rectangle.Width / 2), _rectangle.Y + _rectangle.Height, local_posx + (_rectangle.Width / 2), posy, "");
                        }


                        if (i == 0 && true_child == true)
                        {
                            childz.Add(new Point(local_posx, posy - 125));
                            true_arrows(parentz, childz, "True");
                            true_child = false;
                        }

                    }
                }
            }



        }

        /**************************End Of Flow Chart********************/
        public bool parseClicked { get; set; }
        public bool clearclicked { get; set; }
        public bool classdiagramclicked { get; set; }
        private void Parse_Click(object sender, EventArgs e)
        {
            parseClicked = true;

            //panel1.Dispose();
            // panel1.Controls.Clear();
            panel1.Refresh();
        }

        private void DrawFlowChart()
        {
            this.csharpTreeView = new System.Windows.Forms.TreeView();

            SyntaxTree syntaxTree = new CSharpParser().Parse(TxtBox_InputCode.Text.ToString());
            csharpTreeView.Nodes.Clear();

            foreach (var element in syntaxTree.Children)
            {
                csharpTreeView.Nodes.Add(MakeTreeNode(element));
                nodeEnumerator = csharpTreeView.Nodes.GetEnumerator();
            }

            SelectCurrentNode(csharpTreeView.Nodes);

            parserHelper.FillNodeList(nodeEnumerator, TxtBox_InputCode.Text);
           // parserHelper.CodeNodes[0].ToString();

            FunctionsInEachClass tempf = new FunctionsInEachClass() ;
            foreach (var el in parserHelper.CodeNodesForEachClass)
            {
                for (int i = 0; i < el.Value.Count; i++)
                {
                    if(el.Value[i].FunctionName == "Main")
                    {
                        //FunctionsInEachClass tempf = new FunctionsInEachClass();
                        tempf = el.Value[i];
                    }
                }
            }
            gogo.declerations_collector(tempf.nodesInEachFunction);
            gogo.detect_wrong();
            gogo.coloring_wrong(TxtBox_InputCode);
            if (gogo.wrong_names.Count == 0)
            {
                start();

                draw_arrow(375, 60, posx + (_rectangle.Width / 2), posy, "");

                //drawing(parserHelper.CodeNodes, posx, right_posx);
                drawing(tempf.nodesInEachFunction, posx, right_posx);


                draw_last_arrows(end_items, "");

                //true_arrows(parentz, childz);
                end();
                posy = 10;
            }
            else {
                MessageBox.Show("You Must Choose Meaningful Names For Your Variables First");

            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            gogo.aval_words();
        }

        private void TxtBox_InputCode_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>


        private void DrawClassDiagram()
        {
            this.csharpTreeView = new System.Windows.Forms.TreeView();

            SyntaxTree syntaxTree = new CSharpParser().Parse(TxtBox_InputCode.Text.ToString());
            csharpTreeView.Nodes.Clear();

            
            foreach (var element in syntaxTree.Children)
            {
                csharpTreeView.Nodes.Add(MakeTreeNode(element));
                nodeEnumerator = csharpTreeView.Nodes.GetEnumerator();
            }

            SelectCurrentNode(csharpTreeView.Nodes);

            parserHelper.FillNodeList(nodeEnumerator, TxtBox_InputCode.Text);
            // parserHelper.CodeNodes[0].ToString();

            // FunctionsInEachClass tempf = new FunctionsInEachClass();

            /*Classdiagram obj1 = new Classdiagram();
            foreach (var el in parserHelper.CodeNodesForEachClass)
            {
                obj1.draw_class(el.Key.ClassName, el.Value);
            }*/



        }


        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>




        //bool x = false;
        private void panel1_Paint(object sender, PaintEventArgs e)
        {


            if (parseClicked)
            {
                panel1.AutoScroll = true;

                panel1.AutoScrollMinSize = new Size(10000, 10000);

                e.Graphics.Clear(Color.Black);

                parserHelper.CodeNodes.Clear();
                end_items.Clear();
                parentz.Clear();
                childz.Clear();
                loop_parentz.Clear();
                loop_childz.Clear();
                while_parent.Clear();
                while_child.Clear();
                incrementer = 0;
                posx = 300;
                posy = 10;
                is_if_child = false;
                is_forz_child = false;
                is_while_child = false;
                is_condition_child = false;
                condchild = false;
                _rectangle = new Rectangle(300, 20, 150, 50);
                polygon_points[0] = new Point(posx + 75, 10);
                polygon_points[1] = new Point(posx - 25, 60);
                polygon_points[2] = new Point(posx + 75, 110);
                polygon_points[3] = new Point(posx + 175, 60);

                e.Graphics.TranslateTransform(panel1.AutoScrollPosition.X, panel1.AutoScrollPosition.Y);
                panelGraphics = e.Graphics;
                // drawing(parserHelper.CodeNodes, posx, right_posx);
                DrawFlowChart();


            }
            else if (clearclicked)
            {
                /*e.Graphics.Clear(Color.Black);
                panelGraphics = e.Graphics;
                clearclicked = false;
                //DrawFlowChart();*/



            }

           /* else if (classdiagramclicked)
            {
               
                panel1.AutoScroll = true;

                panel1.AutoScrollMinSize = new Size(10000, 10000);

                e.Graphics.Clear(Color.Black);
                e.Graphics.TranslateTransform(panel1.AutoScrollPosition.X, panel1.AutoScrollPosition.Y);
                panelGraphics = e.Graphics;                
                DrawClassDiagram();

            }*/




        }

       /* private void button1_Click(object sender, EventArgs e)
        {
            clearclicked = true;

            panel1.Refresh();
        }*/

            

        private void checker_Click(object sender, EventArgs e)
        {
            this.csharpTreeView = new System.Windows.Forms.TreeView();

            SyntaxTree syntaxTree = new CSharpParser().Parse(TxtBox_InputCode.Text.ToString());
            csharpTreeView.Nodes.Clear();

            foreach (var element in syntaxTree.Children)
            {
                csharpTreeView.Nodes.Add(MakeTreeNode(element));
                nodeEnumerator = csharpTreeView.Nodes.GetEnumerator();
            }

            SelectCurrentNode(csharpTreeView.Nodes);

            parserHelper.FillNodeList(nodeEnumerator, TxtBox_InputCode.Text);


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
            gogo.coloring_wrong(TxtBox_InputCode);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            classdiagramclicked = true;
            Form2 ClassDiagramForm = new Form2();
            ClassDiagramForm.Show();
        }

        private void UseCaseButton_Click(object sender, EventArgs e)
        {

            this.Hide();
            UseCaeForm UseCase = new UseCaeForm();
            UseCase.Show();

        }
    }
}
