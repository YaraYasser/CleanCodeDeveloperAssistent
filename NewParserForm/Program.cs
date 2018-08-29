using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.CSharp.Resolver;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using System.Reflection;
using System.Windows.Forms;

namespace NewParserForm
{

    class Program
    {
      
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]


      static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            Application.Run(new FirstForm());
        }

       
    }
}
