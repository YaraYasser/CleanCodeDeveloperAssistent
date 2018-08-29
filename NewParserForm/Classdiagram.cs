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
    public class Classdiagram
    {

        public Rectangle classz = new Rectangle();
        public int class_posx = 10;
        public int class_posy = 10;
        public int y = 20;
        public Graphics PG { get; set; }







        public void draw_class(string class_name, List<string> attributes, List<FunctionsInEachClass> func)
        {
            FlowChartForm formobj2 = new FlowChartForm();


            classz = new Rectangle(class_posx, class_posy, 100, (y + 20));
            formobj2.panelGraphics.DrawString(class_name, formobj2.drawFont, Brushes.Red, new Point(class_posx + 30, y + 20));
            y += 10;
            formobj2.panelGraphics.DrawLine(Pens.Yellow, new Point(classz.X, classz.Y + 20), new Point(classz.X + classz.Width, classz.Y + 20));

            y += 10;                                //attributes-------->
            for (int i = 0; i < func.Count; i++)
            {
                formobj2.panelGraphics.DrawString(attributes[i], formobj2.drawFont, Brushes.Red, new Point(class_posx + 30, y + 20));
                y += 10;
            }                                      //attributes<-----------

            formobj2.panelGraphics.DrawLine(Pens.Yellow, new Point(classz.X, classz.Y + 20), new Point(classz.X + classz.Width, classz.Y + 20));
            y += 10;
            for (int i = 0; i < func.Count; i++)
            {
                formobj2.panelGraphics.DrawString(func[i].FunctionName, formobj2.drawFont, Brushes.Red, new Point(class_posx + 30, y + 20));
                y += 10;
            }
            classz = new Rectangle(class_posx, class_posy, 100, (y + 20));
            formobj2.panelGraphics.DrawRectangle(Pens.Green, classz);

        }

    }
}
