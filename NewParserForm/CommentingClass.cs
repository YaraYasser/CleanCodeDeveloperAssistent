using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.NRefactory.CSharp;


namespace NewParserForm
{
    class CommentingClass
    {
        public string CommentCode(List<string> functionNames)
        {
            List<string> ProtoTyples = new List<string>();
            ProtoTyples.Add("int");
            ProtoTyples.Add("bool");
            ProtoTyples.Add("float");
            ProtoTyples.Add("long");
            ProtoTyples.Add("double");
            ProtoTyples.Add("string");

            StringAccessors accessor = StringAccessors.Instance;
            string[] CodeSplited = accessor.getCode().Split('\n');
            string tempCommentedCodeForParameters = "";
            string commentedCode = "";
            List<string> arrayReturned = new List<string>();
            int j = 0;
            bool WrittenChecker = false;
            for (int i = 0; i < CodeSplited.Count(); i++)
            {

                if ((CodeSplited[i].Contains(functionNames[j])) && (!CodeSplited[i - 1].Contains("//")))
                {
                    string[] getParameters = CodeSplited[i].Split('(');

                    for(int kk = 0; kk < ProtoTyples.Count(); kk++)
                    {
                        if (getParameters.Count() > 1)
                        { 
                     if (getParameters[1].Contains(ProtoTyples[kk]))
                     {
                        string[] getParameters2 = getParameters[1].Split(')');
                        string TheComment;
                        if (getParameters2[0] != "")
                        {
                            TheComment = "//<summary> \n //parameters used by function " + getParameters2[0];

                        }
                        else
                        {
                            TheComment = "//<summary> \n //parameters used by function none";

                        }
                        tempCommentedCodeForParameters += TheComment + " \n ";
                        tempCommentedCodeForParameters += CodeSplited[i] + " \n ";
                        j++;
                            WrittenChecker = true;
                        continue;

                     }
                    
                        }
                    }

                }
                else if (CodeSplited[i].Contains("return"))
                {
                    string[] getParameters22 = CodeSplited[i].Split(' ');
                    for(int d= 0; d < getParameters22.Count(); d++)
                    {
                        if (getParameters22[d] != "") { 
                    string[] getParameters2 = getParameters22[d+1].Split(';');
                    arrayReturned.Add(getParameters2[0]);
                            break;
                        }
                    }

                }
                if (WrittenChecker == false) { 
                    tempCommentedCodeForParameters += CodeSplited[i] + "\n ";
                }
                else
                {
                    WrittenChecker = false;
                }
            }

            int k = 0;

            string[] Code2 = tempCommentedCodeForParameters.Split('\n');
            if (arrayReturned.Count() != 0)
            {
                for (int i = 0; i < Code2.Count(); i++)
                {

                    if ((Code2[i].Contains("//parameters used by function")) && (!Code2[i + 1].Contains("void")) && (Code2[i - 1].Contains("//<summary>")))
                    {
                        commentedCode += Code2[i] + "and returned value " + arrayReturned[k] + "\n //</summary> \n";

                        k++;
                    }
                    else if((Code2[i].Contains("//parameters used by function")) && (Code2[i - 1].Contains("//<summary>")))
                    {
                        commentedCode += Code2[i] + "\n //</summary> \n";

                    }
                    else
                    {
                        commentedCode += Code2[i] + "\n ";
                    }
                }
                return commentedCode;
            }
            else
            {

                for (int ii = 0; ii < Code2.Count(); ii++)
                {
                 
                        if (Code2[ii].Contains("//parameters used by function"))
                        {
                            commentedCode += Code2[ii] + "\n </summary> \n";
                       
                    }
                    else
                        {
                            commentedCode += Code2[ii] + "\n ";
                        }
                   
                }
                return commentedCode;
            }
        
     
        }
    }

}
