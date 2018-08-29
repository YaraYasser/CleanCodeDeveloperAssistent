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
using System.Diagnostics;

namespace NewParserForm
{
    public partial class FirstForm : Form
    {

        public System.Windows.Forms.TreeView csharpTreeView;
        IEnumerator nodeEnumerator;
        ParserHelper parserHelper = new ParserHelper();
        string PythonScriptPath = @"D:\ForthYear\GP\Final1.withBugs(3)\21-11-17\NewParserForm\pyscript.py";
        string PythonExeFullPath = @"C:\Users\user\PycharmProjects\SuggestNames\venv\Scripts\python.exe";

        public avalable_names gogo = new avalable_names();


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

        public FirstForm()
        {
            InitializeComponent();

        }

        private void btn_FlowChart_Click(object sender, EventArgs e)
        {

            StringAccessors accessor = StringAccessors.Instance;
            accessor.setCode(richTextBox1.Text.ToString());


            this.Hide();

            FlowChartForm flowChart = new FlowChartForm();
            flowChart.Show();
        }
        private void CommentCode()
        {
            CommentingClass cc = new CommentingClass();


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

            string commentedCode = cc.CommentCode(parserHelper.FunctionNames);
            richTextBox1.Text = commentedCode;
        }

        private void btn_Check_Click(object sender, EventArgs e)
        {
            StringAccessors accessor = StringAccessors.Instance;
            accessor.setCode(richTextBox1.Text.ToString());

          

            //---------------------------------------------------------------------------------------
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


            List<FunctionsInEachClass> tempf = new List<FunctionsInEachClass>();
          
            foreach (var el in parserHelper.CodeNodesForEachClass)
            {
                FunctionsInEachClass f = new FunctionsInEachClass();

                if (el.Key.ClassAttributes.Count() != 0)
                {
                    for (int l = 0; l < el.Key.ClassAttributes.Count(); l++)
                    {
                        Node ss = new Node();
                        ss.CodeLine = el.Key.ClassAttributes[l];
                        ss.Type = StatementTypes.VariableDeclaration;
                        f.nodesInEachFunction.Add(ss);
                        tempf.Add(f);
                    }
                }
                for (int i = 0; i < el.Value.Count; i++)
                {
                    //FunctionsInEachClass tempf = new FunctionsInEachClass();

                    tempf.Add(el.Value[i]);

                }
            }
            gogo.aval_words();

            for (int h = 0; h < tempf.Count(); h++)
            {

                gogo.declerations_collector(tempf[h].nodesInEachFunction);
             
             
            }
            gogo.detect_wrong();
            CommentCode();
            gogo.coloring_wrong(richTextBox1);

            //--------------------------------------------------------------------------------------
            
            parserHelper.CodeNodes.Clear();
            tempf.Clear();
         

        }

        private void btn_UseCase_Click(object sender, EventArgs e)
        {


            StringAccessors accessor = StringAccessors.Instance;
            accessor.setCode(richTextBox1.Text.ToString());

            this.Hide();

            UseCaeForm UseCase = new UseCaeForm();
            UseCase.Show();

        }

        private void btn_ClassDiagram_Click(object sender, EventArgs e)
        {


            StringAccessors accessor = StringAccessors.Instance;
            accessor.setCode(richTextBox1.Text.ToString());

            this.Hide();

            ClassDiagramForm ClassDiagramForm = new ClassDiagramForm();
            ClassDiagramForm.Show();
        }

        private void FirstForm_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void FirstForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private string run_cmd(string cmd, string args)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = this.PythonExeFullPath;
            start.Arguments = string.Format("{0} {1}", cmd, args);
            start.UseShellExecute = false;
            start.CreateNoWindow = true;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    return result;
                }
            }
        }
        private void btn_SuggestWords_Click(object sender, EventArgs e)
        {

            string PythonDataFilePath = @"D:\ForthYear\GP\Final1.withBugs(3)\21-11-17\NewParserForm\datafile.txt";
            string requiredDomain = txt_btn_Domain.Text.ToString();
            System.IO.File.WriteAllText(PythonDataFilePath, requiredDomain);
            var result = this.run_cmd(PythonScriptPath, PythonDataFilePath);
            string[] SuggestedWords1 = result.Split('{');
            string[] SuggestedWords2 = SuggestedWords1[1].Split('}');
            string[] SuggestedWords3 = SuggestedWords2[0].Split(',');
            List<string> FinalSuggested = new List<string>();
            for (int i = 0; i < SuggestedWords3.Count(); i++)
            {
                string[] s = SuggestedWords3[i].Split('\'');
                string[] ss2 = s[1].Split('\'');
                FinalSuggested.Add(ss2[0]);
            }
            for (int j = 0; j < FinalSuggested.Count(); j++)
            {
                comboBox1.Items.Add(FinalSuggested[j]);
            }



            System.IO.File.WriteAllText(PythonDataFilePath, string.Empty);

        }

        private void btn_ChangeWord_Click(object sender, EventArgs e)
        {
            int index = richTextBox1.Text.IndexOf(richTextBox1.SelectedText);
            int length = (richTextBox1.SelectedText).Length;

            richTextBox1.Select(index, length);
            richTextBox1.SelectionColor = Color.Black;

            richTextBox1.SelectedText = comboBox1.SelectedItem.ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void btn_comment_Click(object sender, EventArgs e)
        {

        }
    }
}
