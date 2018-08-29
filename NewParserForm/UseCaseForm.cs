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

namespace NewParserForm
{
    public partial class UseCaeForm : Form
    {
        //FirstForm first_form = new FirstForm();

        public System.Windows.Forms.TreeView csharpTreeView;
        IEnumerator nodeEnumerator;
        ParserHelper parserHelper = new ParserHelper();
        public avalable_names gogo = new avalable_names();

        public List<string> listofitems = new List<string>();
        public string stringItem;


        /*************************************USE CASE********************************************************/
        public Graphics panelGraphics { get; set; }
        int pos_multi_X1;
        int pos_multi_Y1;
        int pos_multi_X2;
        int pos_multi_Y2;
        int UC_next_pos_x;
        int UC_next_pos_y;
        String drawString;
        Font drawFont = new Font("Arial", 14);
        SolidBrush drawBrush = new SolidBrush(Color.Black);

        public bool IsMeaningfull()
        {
            bool checker = true;
            this.csharpTreeView = new System.Windows.Forms.TreeView();

            SyntaxTree syntaxTree = new CSharpParser().Parse(richTextBoxUseCase.Text.ToString());
            csharpTreeView.Nodes.Clear();

            foreach (var element in syntaxTree.Children)
            {
                csharpTreeView.Nodes.Add(MakeTreeNode(element));
                nodeEnumerator = csharpTreeView.Nodes.GetEnumerator();
            }

            SelectCurrentNode(csharpTreeView.Nodes);

            parserHelper.FillNodeList(nodeEnumerator, richTextBoxUseCase.Text);


            FunctionsInEachClass tempf = new FunctionsInEachClass();
            foreach (var el in parserHelper.CodeNodesForEachClass)
            {
                for (int i = 0; i < el.Value.Count; i++)
                {
                
                        //FunctionsInEachClass tempf = new FunctionsInEachClass();
                        tempf = el.Value[i];
                    /*gogo.declerations_collector(tempf.nodesInEachFunction);
                    gogo.detect_wrong();
                    gogo.coloring_wrong(richTextBoxUseCase);
                    if (gogo.wrong_names.Count !=0) {
                        checker = false;
                    }*/
                }
            }





            //return checker;
            return true;
        }

        public void DrawActor()
        {
            //draw head
            panelGraphics = panel1.CreateGraphics();
            Pen pen = new Pen(Color.Red, 1);
            panelGraphics.DrawEllipse(pen, 50, 50, 25, 25);
            SolidBrush head = new SolidBrush(Color.White);
            panelGraphics.FillEllipse(head, 50, 50, 25, 25);
            //draw body

            panelGraphics.DrawLine(pen, 60, 75, 60, 125);

            //draw arms
            panelGraphics.DrawLine(pen, 80, 90, 40, 90);

            //draw legs
            panelGraphics.DrawLine(pen, 60, 125, 50, 140);
            panelGraphics.DrawLine(pen, 60, 125, 70, 140);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            drawString = "Actor";
            panelGraphics.DrawString(drawString, drawFont, drawBrush, 60, 180, stringFormat);


        }
        public void draw_arrow(int init_pos_x1, int init_pos_y1, int init_pos_x2, int init_pos_y2)
        {
            //Graphics g = panel1.CreateGraphics();

            Pen pen = new Pen(Color.Red, 2);
            pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            panelGraphics.DrawLine(pen, init_pos_x1, init_pos_y1, init_pos_x2, init_pos_y2);
            pen.Dispose();

            pos_multi_X1 = init_pos_x1 - 5;
            pos_multi_Y1 = init_pos_y1 + 20;
            pos_multi_X2 = init_pos_x2 - 5;
            pos_multi_Y2 = init_pos_y2 + 60;
        }

        public void draw_usecase(int UC_init_pos_x, int UC_init_pos_y)
        {
            // UC_init_pos_x = 210;
            //UC_init_pos_y = 50;
            Pen pen = new Pen(Color.Black, 1);
            panelGraphics.DrawEllipse(pen, UC_init_pos_x, UC_init_pos_y, 100, 25);
            SolidBrush solid = new SolidBrush(Color.Yellow);
            panelGraphics.FillEllipse(solid, UC_init_pos_x, UC_init_pos_y, 100, 25);
            UC_next_pos_x = UC_init_pos_x - 5;
            UC_next_pos_y = UC_init_pos_y + 60;


            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            drawString = stringItem;
            // drawString = "use case";
            panelGraphics.DrawString(drawString, drawFont, drawBrush, (UC_next_pos_x + UC_init_pos_x + 100) / 2, (UC_next_pos_y + UC_init_pos_y + 20) / 2, stringFormat);

        }
        /*************************************END OF USE CASE*************************************************/

        int GetOffset(RichTextBox textBox, ICSharpCode.NRefactory.TextLocation location)  //////////////Get Node From Here
        {
            // TextBox uses 0-based coordinates, TextLocation is 1-based 
            return textBox.GetFirstCharIndexFromLine(location.Line - 1) + location.Column - 1;
        }
        bool SelectCurrentNode(TreeNodeCollection c)
        {
            int selectionStart = richTextBoxUseCase.SelectionStart;
            int selectionEnd = selectionStart + richTextBoxUseCase.SelectionLength;
            foreach (TreeNode t in c)
            {
                AstNode node = t.Tag as AstNode;
                if (node != null
                    && selectionStart >= GetOffset(richTextBoxUseCase, node.StartLocation)
                    && selectionEnd <= GetOffset(richTextBoxUseCase, node.EndLocation))
                {
                    if (selectionStart == selectionEnd
                        && (selectionStart == GetOffset(richTextBoxUseCase, node.StartLocation)
                            || selectionStart == GetOffset(richTextBoxUseCase, node.EndLocation)))
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
        public UseCaeForm()
        {
            InitializeComponent();
            StringAccessors accessor = StringAccessors.Instance;
           richTextBoxUseCase.Text= accessor.getCode();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //ParserHelper parserHelper = new ParserHelper();

            this.csharpTreeView = new System.Windows.Forms.TreeView();
            if (IsMeaningfull())
            {
                if (parserHelper.FunctionNames.Count == 0)
                {
                    MessageBox.Show("ERROR You have entered a code that doesn't contain any functions");

                }
                for (int i = 0; i < parserHelper.FunctionNames.Count; i++)
                {

                    if (i == 0)
                    {
                        DrawActor();
                        stringItem = parserHelper.FunctionNames[i];
                        draw_arrow(100, 75, 210, 60); draw_arrow(100, 75, 210, 60);
                        draw_usecase(210, 50);
                    }
                    else
                    {
                        stringItem = parserHelper.FunctionNames[i];
                        draw_arrow(pos_multi_X1, pos_multi_Y1, pos_multi_X2, pos_multi_Y2);
                        draw_usecase(UC_next_pos_x, UC_next_pos_y);
                    }



                    /*if (parserHelper.FunctionNames.Count == 0)
                    {
                        MessageBox.Show("ERROR");

                    }*/

                }
            }
            else {
                MessageBox.Show("You Must Choose Meaningful Names For Your Variables First");

            }

            parserHelper.FunctionNames.Clear();
            

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void btn_BackIcon_Click(object sender, EventArgs e)
        {
            FirstForm first = new FirstForm();
            this.Hide();
            first.Show();
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void UseCaeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
