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
    public class avalable_names
    {
        public string line;
        public List<string> dictinary = new List<string>();
        public List<string> declerations = new List<string>();
        public List<string> wrong_names = new List<string>();
        public bool is_found = false;
        //english nouns (4500+)
        public string splitting;

        public void aval_words()
        {
            var lines = File.ReadLines(@"sorted_dict.txt");


            foreach (var l in lines)
            {
                line = l.Replace("\t", "");

                dictinary.Add(line);
            }

        }
        public void declerations_collector(List<Node> x)
        {
            for (int i = 0; i <= x.Count - 1; i++)
            {
                if (x[i].Type == StatementTypes.VariableDeclaration)
                {
                    string[] words = x[i].CodeLine.Split(' ');
                    for (int p = 1; p < words.Length; p++)
                    {
                        if (words[p].Contains('='))
                        {
                            break;
                        }
                        else if (words[p].Contains(';'))
                        {
                            string[] words2 = words[p].Split(';');
                            declerations.Add(words2[0] + ';');
                            //declerations.Add(words2[0]);
                        }
                        else if (words[p].Contains(','))
                        {
                            /* string[] words2 = words[p].Split(',');
                             declerations.Add(words2[0]);*/
                            declerations.Add(words[p]);
                        }
                        else if (words[p + 1] == "=")
                        {
                            declerations.Add(words[p] + '=');
                        }

                    }
                    /*if (words[1].Contains(','))
                    {
                        string[] words2 = words[1].Split(',');
                        for(int m=0;m<words2.Length;m++)
                        {
                            if(words2[m].Contains(';'))
                            {
                                string[] words3 = words2[m].Split(';');
                                declerations.Add(words3[0]);
                            }
                            else
                            {
                                declerations.Add(words2[m]);
                            }
                        }
                    }
                    else if (words[1].Contains(';'))
                    {
                        string[] words1 = words[1].Split(';');
                        declerations.Add(words1[0]);
                    }
         
                    else
                    {
                        declerations.Add(words[1]);
                    }*/
                }
                if (x[i].Type != StatementTypes.VariableDeclaration && x[i].TrueChildren.Count != 0)
                {
                    declerations_collector(x[i].TrueChildren);
                }
                if (x[i].Type != StatementTypes.VariableDeclaration && x[i].FalseChildren.Count != 0)
                {
                    declerations_collector(x[i].FalseChildren);
                }
            }
        }
        public void detect_wrong()
        {

            for (int i = 0; i < declerations.Count; i++)
            {
                if (!(declerations[i].Contains('_')))
                {

                    if (declerations[i].Contains(','))
                    {
                        string[] splitter = declerations[i].Split(',');
                        splitting = splitter[0];
                    }
                    else if (declerations[i].Contains(';'))
                    {
                        string[] splitter = declerations[i].Split(';');
                        splitting = splitter[0];
                    }
                    else if (declerations[i].Contains('='))
                    {
                        string[] splitter = declerations[i].Split('=');
                        splitting = splitter[0];
                    }
                    is_found = false;
                    for (int j = 0; j < dictinary.Count; j++)
                    {
                        //if (declerations[i].ToLower() == dictinary[j])
                        if (splitting.ToLower() == dictinary[j])
                        {
                            is_found = true;
                            //return;
                            break;
                        }

                    }
                    if (is_found == false)
                    {
                        wrong_names.Add(declerations[i]);
                    }
                }

                else if (declerations[i].Contains('_'))
                {


                    if (declerations[i].Contains(','))
                    {
                        string[] splitter = declerations[i].Split(',');
                        splitting = splitter[0];
                    }
                    else if (declerations[i].Contains(';'))
                    {
                        string[] splitter = declerations[i].Split(';');
                        splitting = splitter[0];
                    }
                    else if (declerations[i].Contains('='))
                    {
                        string[] splitter = declerations[i].Split('=');
                        splitting = splitter[0];
                    }


                    // string[] seperated_words = declerations[i].Split('_');
                    string[] seperated_words = splitting.Split('_');
                    //---------------------------------------------------------------
                    for (int m = 0; m < seperated_words.Count(); m++)
                    {
                        is_found = false;
                        for (int j = 0; j < dictinary.Count; j++)
                        {
                            if (seperated_words[m].ToLower() == dictinary[j])
                            {
                                is_found = true;
                                //return;
                                break;
                            }

                        }
                        if (is_found == true) { break; }
                    }

                    if (is_found == false)
                    {
                        wrong_names.Add(declerations[i]);
                    }
                    //-----------------------------------------------------------------
                }
                splitting = "";
            }
        }


        public void coloring_wrong(RichTextBox input)
        {
            string[] splitter;
            for (int i = 0; i < wrong_names.Count; i++)
            {
                if (wrong_names[i].Contains(','))
                {
                    splitter = wrong_names[i].Split(',');
                    splitting = splitter[0];
                }
                else if (wrong_names[i].Contains(';'))
                {
                    splitter = wrong_names[i].Split(';');
                    splitting = splitter[0];
                }
                else if (wrong_names[i].Contains('='))
                {
                    splitter = wrong_names[i].Split('=');
                    splitting = splitter[0];
                }

                if (input.Text.Contains(wrong_names[i]))
                {
                    int index = input.Text.IndexOf(wrong_names[i]);
                    int length = (splitting).Length;

                    input.Select(index, length);
                    input.SelectionColor = Color.Red;


                }

            }
        }
    }
}
