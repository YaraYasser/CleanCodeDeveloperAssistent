﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
    public partial class ClassDiagramForm : Form
    {

        //public FirstForm first_form = new FirstForm();


        public System.Windows.Forms.TreeView csharpTreeView;
        IEnumerator nodeEnumerator;
        ParserHelper parserHelper = new ParserHelper();
        public Graphics panelGraphics { get; set; }
        Pen pen = new Pen(Color.Black);
        public avalable_names gogo = new avalable_names();
        public Dictionary<int, int> largest_levels_height = new Dictionary<int, int>();
        List<ClassData> un_checked_classes = new List<ClassData>();
        List<List<FunctionsInEachClass>> un_checked_functions = new List<List<FunctionsInEachClass>>();
        public bool changedsize = false;
        public string[] flip;


        /// <summary>  class diagraaaam////////////////////////////////////////////////////////////////////////////
        public Rectangle classz = new Rectangle();

        public int class_posx = 20;
        public int class_posy = 50;
        public int y = 0;
        public int y_dash = 0;
        public Graphics PG { get; set; }
        public List<Point> classes_pos = new List<Point>();
        
        public Font drawFont = new Font("Arial",12);
        
        

        public int class_order = 0;
        //public Dictionary<ClassData, List<FunctionsInEachClass>> class_code_nodes = new Dictionary<ClassData, List<FunctionsInEachClass>>();

        public int largest_height = 0;
        public int space_bet_lines = 10;

        //public void draw_class(&parserhelper)
        //public void draw_class(string class_name,int X, int Y,List<string> attributes , List<FunctionsInEachClass> func)
        public void draw_class(/*Dictionary<ClassData, List<FunctionsInEachClass>> class_code_nodes*/)
        {


            foreach (var el in parserHelper.CodeNodesForEachClass)
            {



                //classz = new Rectangle(class_posx, class_posy, 100, (y + 15));
                classz = new Rectangle(class_posx, class_posy, 200, (y_dash + 15));
                //classes_pos.Add(new Point(classz.X, classz.Y));
                un_checked_classes[class_order].x = classz.X;
                un_checked_classes[class_order].y = classz.Y;



                //----------------------------------------------------
                y += 15;

                for (int i = 0; i < el.Key.ClassAttributes.Count; i++)    //attributes
                {
                    y += 15;
                }

                for (int i = 0; i < el.Value.Count; i++)
                {
                   
                    y += 15;
                }

                classz = new Rectangle(class_posx, class_posy, 200, (y + 5));

                //-------------------------------------------------------------------------------------------------------
                List<string> all_info = new List<string>();
                all_info.Add(el.Key.ClassName);
                for (int att = 0; att < el.Key.ClassAttributes.Count; att++)
                {
                    all_info.Add(el.Key.ClassAttributes[att]);
                }

                for (int func = 0; func < el.Value.Count; func++)
                {
                    all_info.Add(el.Value[func].FunctionName);
                }

                while (FindFont(panelGraphics,all_info,classz.Size,drawFont).Size < 8)
                {
                    classz.Width *= 2;
                    //classz.Height *= 2;
                    changedsize = true;
                }
                //----------------------------------------------------------
                SolidBrush fill = new SolidBrush(Color.Yellow);
                panelGraphics.FillRectangle(fill, classz);
                //---------------------------------------------------



                if (changedsize == false)
                {

                    panelGraphics.DrawString(el.Key.ClassName, FindFont(panelGraphics,all_info,classz.Size,drawFont), Brushes.DarkViolet, new Point(classz.X + 30, classz.Y + y_dash));

                }

                else
                {
                    panelGraphics.DrawString(el.Key.ClassName, DefaultFont, Brushes.DarkViolet, new Point(classz.X + 30, classz.Y + y_dash)); 
                }

                //panelGraphics.DrawString(el.Key.ClassName, DefaultFont, Brushes.DarkViolet, new Point(classz.X + 30, classz.Y + y_dash));


                y_dash += 15;


                panelGraphics.DrawLine(Pens.Red, new Point(classz.X, classz.Y + y_dash), new Point(classz.X + classz.Width, classz.Y + y_dash));
                // y += 30; //atterbute
                 
                
                for (int i = 0; i < el.Key.ClassAttributes.Count; i++)    //attributes
                {
                    if (el.Key.ClassAttributes[i].Contains("="))
                    {
                        flip = el.Key.ClassAttributes[i].Split('=');
                    }
                    else { flip = el.Key.ClassAttributes[i].Split(';'); }

                    string[] flip2 = flip[0].Split(' ');
                    for (int check = 0; check < flip2.Length; check++) {
                        if (flip2[check].Contains("")) {
                            List < string >flip2_list= new List<string>(flip2);
                            flip2_list.Remove("");
                            flip2 = flip2_list.ToArray();
                        }
                    }
                    if (el.Key.ClassAttributesModifierTypes[i] == Modifier.Public)
                    {
                        
                        if (changedsize == false)
                        {
                            if (flip2.Length == 2)
                            {
                                panelGraphics.DrawString("+ " + flip2[1] + ' ' + ':' + ' ' + flip2[0], FindFont(panelGraphics, all_info, classz.Size, drawFont), Brushes.Black, new Point(classz.X + 10, classz.Y + y_dash));
                            }
                            else if (flip2.Length == 3)
                            {
                                panelGraphics.DrawString("+ " + flip2[2] + ' ' + ':' + ' ' + flip2[1], FindFont(panelGraphics, all_info, classz.Size, drawFont), Brushes.Black, new Point(classz.X+10+  classz.Y + y_dash));

                            }
                            else if (flip2.Length == 4)
                            {
                                panelGraphics.DrawString("+ " + flip2[3] + ' ' + ':' + ' ' + flip2[2], FindFont(panelGraphics, all_info, classz.Size, drawFont), Brushes.Black, new Point(classz.X + 10, classz.Y + y_dash));

                            }

                        }

                        else
                        {
                            if (flip2.Length == 2)
                            {
                                panelGraphics.DrawString("+ " + flip2[1] + ':' + flip2[0], DefaultFont, Brushes.Black, new Point(classz.X + 10, classz.Y + y_dash));
                            }
                            else if (flip2.Length == 3)
                            {
                                panelGraphics.DrawString("+ " + flip2[2] + ':' + flip2[1], DefaultFont, Brushes.Black, new Point(classz.X + 10, classz.Y + y_dash));
                            }

                            else if (flip2.Length == 4)
                            {
                                
                                
                                    panelGraphics.DrawString("+ " + flip2[3] + ':' + flip2[2], DefaultFont, Brushes.Black, new Point(classz.X + 30, classz.Y + y_dash));
                                
                            }
                        }

                    }

                    else
                    {
                       
                        if (changedsize == false)
                        {

                            if (flip2.Length == 2)
                            {

                                panelGraphics.DrawString("- " + flip2[1] + ' ' + ':' + ' ' + flip2[0], FindFont(panelGraphics, all_info, classz.Size, drawFont), Brushes.Black, new Point(classz.X + 10, classz.Y + y_dash));

                            }

                            else if (flip2.Length == 3)
                            {
                                panelGraphics.DrawString("- " + flip2[2] + ' ' + ':' + ' ' + flip2[1], FindFont(panelGraphics, all_info, classz.Size, drawFont), Brushes.Black, new Point(classz.X + 10, classz.Y + y_dash));
                            }

                            else if (flip2.Length == 4)
                            {

                                if (flip2[0].ToLower() == "public")
                                {
                                    panelGraphics.DrawString("+ " + flip2[3] + ' ' + ':' + ' ' + flip2[2], FindFont(panelGraphics, all_info, classz.Size, drawFont), Brushes.Black, new Point(classz.X + 10, classz.Y + y_dash));
                                }

                                else
                                {
                                    panelGraphics.DrawString("- " + flip2[3] + ' ' + ':' + ' ' + flip2[2], FindFont(panelGraphics, all_info, classz.Size, drawFont), Brushes.Black, new Point(classz.X + 10, classz.Y + y_dash));
                                }
                            }

                        }

                        else
                        {
                            if (flip2.Length == 2)
                            {
                                panelGraphics.DrawString("- " + flip2[1] + ':' + flip2[0], DefaultFont, Brushes.Black, new Point(classz.X + 10, classz.Y + y_dash));
                            }

                            else if (flip2.Length==3)
                            {
                                panelGraphics.DrawString("- " + flip2[2] + ':' + flip2[1], DefaultFont, Brushes.Black, new Point(classz.X + 10, classz.Y + y_dash));
                            }

                            else if (flip2.Length == 4)
                            {
                                panelGraphics.DrawString("- " + flip2[3] + ':' + flip2[2], DefaultFont, Brushes.Black, new Point(classz.X + 10, classz.Y + y_dash));
                            }
                        }

                    }
                    // panelGraphics.DrawString(el.Key.ClassAttributes[i], DefaultFont, Brushes.Black, new Point(classz.X + 30, classz.Y + y_dash));
                    y_dash += 15;
                }                                            //attributes

                panelGraphics.DrawLine(Pens.Red, new Point(classz.X, classz.Y + y_dash), new Point(classz.X + classz.Width, classz.Y + y_dash));

                for (int i = 0; i < el.Value.Count; i++)
                {
                    if (el.Value[i].FunctionModifierType == Modifier.Public)
                    {

                        if (changedsize == false)
                        {

                            panelGraphics.DrawString("+ " + el.Value[i].FunctionName + ' ' + '(' + el.Value[i].FunctionParameter + ')', FindFont(panelGraphics, all_info, classz.Size, drawFont), Brushes.Black, new Point(classz.X + 10, classz.Y + y_dash));

                        }

                        else
                        {
                            panelGraphics.DrawString("+ "+el.Value[i].FunctionName + ' ' + '(' + el.Value[i].FunctionParameter + ')', DefaultFont, Brushes.Black, new Point(classz.X + 10, classz.Y + y_dash));
                        }
                    }


                    else 
                    {

                        if (changedsize == false)
                        {

                            panelGraphics.DrawString("- " + el.Value[i].FunctionName + ' ' + '(' + el.Value[i].FunctionParameter + ')', FindFont(panelGraphics, all_info, classz.Size, drawFont), Brushes.Black, new Point(classz.X + 10, classz.Y + y_dash));

                        }

                        else
                        {
                            panelGraphics.DrawString("- " + el.Value[i].FunctionName +' '+'('+ el.Value[i].FunctionParameter+')', DefaultFont, Brushes.Black, new Point(classz.X + 10, classz.Y + y_dash));
                        }
                    }

                    //panelGraphics.DrawString(el.Value[i].FunctionName, DefaultFont, Brushes.Black, new Point(classz.X + 30, classz.Y + y_dash));
                    y_dash += 15;
                }
                //classz = new Rectangle(class_posx, class_posy, 100, (y + 5));

                un_checked_classes[class_order].width = classz.Width;
                un_checked_classes[class_order].height = classz.Height;

                if (classz.Height > largest_height)
                {
                    largest_height = classz.Height;
                }

                class_order++;

                panelGraphics.DrawRectangle(Pens.Red, classz);
              
                if (class_order == un_checked_classes.Count)
                {
                    largest_levels_height.Add(class_order, largest_height);
                }
                else if (class_posx < 1100)
                {
                    class_posx +=400;
                }
                else if (class_posx >= 1100)
                {
                    class_posx = 20;
                    class_posy += largest_height + 200;
                    largest_levels_height.Add(class_order, largest_height);
                    largest_height = 0;
                }


                y = 0;
                y_dash = 0;
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
            Pen association_pen = new Pen(Color.Blue);
            association_pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            //pen.EndCap = System.Drawing.Drawing2D.LineCap.Triangle;


            for (int i = 0; i < un_checked_classes.Count; i++)

            {
                for (int j = 0; j < un_checked_classes.Count; j++)
                {
                    if (i == j && un_checked_classes.Count != 1) { continue; }
                    for (int y = 0; y < un_checked_classes[i].ClassAttributes.Count; y++)
                    {
                        if (un_checked_classes[i].ClassAttributes[y].Contains(un_checked_classes[i].ClassName + " "))
                        {
                            reflexive_assoiation_i = true;
                        }
                        if (un_checked_classes.Count > 1 && un_checked_classes[i].ClassAttributes[y].Contains(un_checked_classes[j].ClassName + " ") && un_checked_classes.Count != 1)
                        {
                            uni_association_i = true;/*break;*/
                        }
                    }
                    for (int y = 0; y < un_checked_classes[j].ClassAttributes.Count; y++)
                    {
                        if (uni_association_i == true && un_checked_classes.Count != 1)
                        {
                            if (un_checked_classes.Count > 1 && un_checked_classes[j].ClassAttributes[y].Contains(un_checked_classes[i].ClassName + " "))
                            {
                                bi_association = true;
                            }
                        }
                    }


                    if (bi_association == true)
                    {
                        uni_association_i = false;
                        uni_association_j = false;
                        bi_association = false;
                        //panelGraphics.DrawLine(Pens.Aqua, new Point(classes_pos[i].X+100, classes_pos[i].Y +12), new Point(classes_pos[j].X, classes_pos[j].Y+12));
                        if (un_checked_classes[i].x < un_checked_classes[j].x && un_checked_classes[j].x - un_checked_classes[i].x == 200)
                        {
                            panelGraphics.DrawLine(Pens.Blue, new Point(un_checked_classes[i].x + un_checked_classes[i].width, un_checked_classes[i].y + 12), new Point(un_checked_classes[j].x, un_checked_classes[j].y + 12));
                        }

                        else if (un_checked_classes[i].x > un_checked_classes[j].x && un_checked_classes[i].x - un_checked_classes[j].x == 200)
                        {
                            panelGraphics.DrawLine(Pens.Blue, new Point(un_checked_classes[i].x, un_checked_classes[i].y + 12), new Point(un_checked_classes[j].x + un_checked_classes[j].width, un_checked_classes[j].y + 12));
                        }

                        else if (un_checked_classes[i].x < un_checked_classes[j].x && Math.Abs(un_checked_classes[j].x - un_checked_classes[i].x) > 200)
                        {
                            for (int k = 0; k < largest_levels_height.Count; k++)
                            {
                                if (i < largest_levels_height.ElementAt(k).Key)
                                {

                                    panelGraphics.DrawLine(Pens.Blue, new Point(un_checked_classes[i].x + un_checked_classes[i].width, un_checked_classes[i].y + 12), new Point(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12));
                                    panelGraphics.DrawLine(Pens.Blue, new Point(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12), new Point(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12 + largest_levels_height.ElementAt(k).Value + space_bet_lines));
                                    panelGraphics.DrawLine(Pens.Blue, new Point(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12 + largest_levels_height.ElementAt(k).Value + space_bet_lines), new Point(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[i].y + 12 + largest_levels_height.ElementAt(k).Value + space_bet_lines));
                                    panelGraphics.DrawLine(Pens.Blue, new Point(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[i].y + 12 + largest_levels_height.ElementAt(k).Value + space_bet_lines), new Point(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[j].y + 12));
                                    panelGraphics.DrawLine(Pens.Blue, new Point(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[j].y + 12), new Point(un_checked_classes[j].x + un_checked_classes[j].width, un_checked_classes[j].y + 12));
                                    space_bet_lines += 10;

                                    break;

                                }


                            }
                        }



                        else if (un_checked_classes[j].x < un_checked_classes[i].x && Math.Abs(un_checked_classes[i].x - un_checked_classes[j].x) > 200)
                        {
                            for (int k = 0; k < largest_levels_height.Count; k++)
                            {
                                if (i < largest_levels_height.ElementAt(k).Key)
                                {

                                    panelGraphics.DrawLine(Pens.Blue, new Point(un_checked_classes[i].x + un_checked_classes[i].width, un_checked_classes[i].y + 12), new Point(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12));
                                    panelGraphics.DrawLine(Pens.Blue, new Point(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12), new Point(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12 + largest_levels_height.ElementAt(k).Value + space_bet_lines));
                                    panelGraphics.DrawLine(Pens.Blue, new Point(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12 + largest_levels_height.ElementAt(k).Value + space_bet_lines), new Point(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[i].y + 12 + largest_levels_height.ElementAt(k).Value + space_bet_lines));
                                    panelGraphics.DrawLine(Pens.Blue, new Point(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[i].y + 12 + largest_levels_height.ElementAt(k).Value + space_bet_lines), new Point(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[j].y + 12));
                                    panelGraphics.DrawLine(Pens.Blue, new Point(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[j].y + 12), new Point(un_checked_classes[j].x + un_checked_classes[j].width, un_checked_classes[j].y + 12));
                                    space_bet_lines += 10;

                                    break;

                                }


                            }
                        }


                        else
                        {
                            /* if (un_checked_classes[i].y < un_checked_classes[j].y)
                             {
                                 panelGraphics.DrawLine(Pens.Aqua, new Point(un_checked_classes[i].x + un_checked_classes[i].width / 2, un_checked_classes[i].y + un_checked_classes[i].height), new Point(un_checked_classes[j].x + un_checked_classes[j].width / 2, un_checked_classes[j].y));
                             }
                             else if (un_checked_classes[i].y > un_checked_classes[j].y)
                             {
                                 panelGraphics.DrawLine(Pens.Aqua, new Point(un_checked_classes[i].x + un_checked_classes[i].width / 2, un_checked_classes[i].y), new Point(un_checked_classes[j].x + un_checked_classes[j].width / 2, un_checked_classes[j].y + un_checked_classes[j].height));
                             }*/
                            /* else*/
                            if (un_checked_classes[i].y != un_checked_classes[j].y && un_checked_classes[i].x == un_checked_classes[j].x)
                            {
                                panelGraphics.DrawLine(Pens.Blue, un_checked_classes[i].x + un_checked_classes[i].width, un_checked_classes[i].y + 12, un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12);
                                panelGraphics.DrawLine(Pens.Blue, un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12, un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[j].y + 12);
                                panelGraphics.DrawLine(Pens.Blue, un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[j].y + 12, un_checked_classes[j].x + un_checked_classes[j].width, un_checked_classes[j].y + 12);
                            }
                        }

                    }

                    else if (uni_association_i == true && uni_association_j == false && bi_association == false)
                    {
                        uni_association_i = false;
                        //panelGraphics.DrawLine(association_pen, new Point(classes_pos[i].X+100, classes_pos[i].Y+12), new Point(classes_pos[j].X, classes_pos[j].Y+12));
                        if (un_checked_classes[i].x < un_checked_classes[j].x && un_checked_classes[j].x - un_checked_classes[i].x == 200)
                        {
                            panelGraphics.DrawLine(association_pen, new Point(un_checked_classes[i].x + un_checked_classes[i].width, un_checked_classes[i].y + 12), new Point(un_checked_classes[j].x, un_checked_classes[j].y + 12));
                        }


                        else if (un_checked_classes[j].x < un_checked_classes[i].x && un_checked_classes[i].x - un_checked_classes[j].x == 200)
                        {
                            panelGraphics.DrawLine(association_pen, new Point(un_checked_classes[i].x, un_checked_classes[i].y + 12), new Point(un_checked_classes[j].x + un_checked_classes[j].width, un_checked_classes[j].y + 12));
                        }

                        else if (un_checked_classes[i].x < un_checked_classes[j].x && Math.Abs(un_checked_classes[j].x - un_checked_classes[i].x) > 200)
                        {
                            for (int k = 0; k < largest_levels_height.Count; k++)
                            {
                                if (i < largest_levels_height.ElementAt(k).Key)
                                {

                                    panelGraphics.DrawLine(Pens.Blue, new Point(un_checked_classes[i].x + un_checked_classes[i].width, un_checked_classes[i].y + 12), new Point(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12));
                                    panelGraphics.DrawLine(Pens.Blue, new Point(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12), new Point(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12 + largest_levels_height.ElementAt(k).Value + space_bet_lines));
                                    panelGraphics.DrawLine(Pens.Blue, new Point(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12 + largest_levels_height.ElementAt(k).Value + space_bet_lines), new Point(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[i].y + 12 + largest_levels_height.ElementAt(k).Value + space_bet_lines));
                                    panelGraphics.DrawLine(Pens.Blue, new Point(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[i].y + 12 + largest_levels_height.ElementAt(k).Value + space_bet_lines), new Point(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[j].y + 12));
                                    panelGraphics.DrawLine(association_pen, new Point(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[j].y + 12), new Point(un_checked_classes[j].x + un_checked_classes[j].width, un_checked_classes[j].y + 12));
                                    space_bet_lines += 10;

                                    break;

                                }


                            }


                            //panelGraphics.DrawLine(association_pen, new Point(un_checked_classes[i].x, un_checked_classes[i].y + 12), new Point(un_checked_classes[j].x + 100, un_checked_classes[j].y + 12));
                        }
                        else if (un_checked_classes[j].x < un_checked_classes[i].x && Math.Abs(un_checked_classes[i].x - un_checked_classes[j].x) > 200)
                        {
                            for (int k = 0; k < largest_levels_height.Count; k++)
                            {
                                if (i < largest_levels_height.ElementAt(k).Key)
                                {

                                    panelGraphics.DrawLine(Pens.Blue, new Point(un_checked_classes[i].x + un_checked_classes[i].width, un_checked_classes[i].y + 12), new Point(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12));
                                    panelGraphics.DrawLine(Pens.Blue, new Point(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12), new Point(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12 + largest_levels_height.ElementAt(k).Value + space_bet_lines));
                                    panelGraphics.DrawLine(Pens.Blue, new Point(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12 + largest_levels_height.ElementAt(k).Value + space_bet_lines), new Point(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[i].y + 12 + largest_levels_height.ElementAt(k).Value + space_bet_lines));
                                    panelGraphics.DrawLine(Pens.Blue, new Point(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[i].y + 12 + largest_levels_height.ElementAt(k).Value + space_bet_lines), new Point(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[j].y + 12));
                                    panelGraphics.DrawLine(association_pen, new Point(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[j].y + 12), new Point(un_checked_classes[j].x + un_checked_classes[j].width, un_checked_classes[j].y + 12));
                                    space_bet_lines += 10;

                                    break;

                                }


                            }


                            //panelGraphics.DrawLine(association_pen, new Point(un_checked_classes[i].x, un_checked_classes[i].y + 12), new Point(un_checked_classes[j].x + 100, un_checked_classes[j].y + 12));
                        }

                        else
                        {
                            /*if (un_checked_classes[i].y < un_checked_classes[j].y)
                            {
                                panelGraphics.DrawLine(association_pen, new Point(un_checked_classes[i].x + un_checked_classes[i].width/2, un_checked_classes[i].y + un_checked_classes[i].height), new Point(un_checked_classes[j].x + un_checked_classes[j].width/2, un_checked_classes[j].y ));
                            }

                            else if (un_checked_classes[i].y > un_checked_classes[j].y)
                            {
                                panelGraphics.DrawLine(association_pen, new Point(un_checked_classes[i].x + un_checked_classes[i].width / 2, un_checked_classes[i].y ), new Point(un_checked_classes[j].x + un_checked_classes[j].width / 2, un_checked_classes[j].y + un_checked_classes[j].height));
                            }
                            */
                            /*else */
                            if (un_checked_classes[i].y != un_checked_classes[j].y && un_checked_classes[i].x == un_checked_classes[j].x)
                            {
                                panelGraphics.DrawLine(Pens.Blue, un_checked_classes[i].x + un_checked_classes[i].width, un_checked_classes[i].y + 12, un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12);
                                panelGraphics.DrawLine(Pens.Blue, un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12, un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[j].y + 12);
                                panelGraphics.DrawLine(association_pen, un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[j].y + 12, un_checked_classes[j].x + un_checked_classes[j].width, un_checked_classes[j].y + 12);
                            }
                        }

                    }

                    /*else if (uni_association_j == true && uni_association_i==false && bi_association == false)
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

                    }*/

                    if (reflexive_assoiation_i == true)
                    {
                        reflexive_assoiation_i = false;
                        //panelGraphics.DrawLine(new Pen(Color.Aqua), new Point(classes_pos[i].X + classes_pos[i].Width / 2, classes_pos[i].Y + classz.Height-5), new Point(classes_pos[i].X + classz.Width / 2, classes_pos[i].Y + classz.Height + 6));
                        panelGraphics.DrawLine(new Pen(Color.Blue), new Point(un_checked_classes[i].x + un_checked_classes[i].width / 2, un_checked_classes[i].y + un_checked_classes[i].height), new Point(un_checked_classes[i].x + un_checked_classes[i].width / 2, un_checked_classes[i].y + un_checked_classes[i].height + 6));
                        //panelGraphics.DrawLine(new Pen(Color.Aqua), new Point(classes_pos[i].X + classz.Width / 2, classes_pos[i].Y + classz.Height + 6), new Point(classes_pos[i].X + classz.Width + 6, classes_pos[i].Y + classz.Height + 6));
                        panelGraphics.DrawLine(new Pen(Color.Blue), new Point(un_checked_classes[i].x + un_checked_classes[i].width / 2, un_checked_classes[i].y + un_checked_classes[i].height + 6), new Point(un_checked_classes[i].x + un_checked_classes[i].width + 6, un_checked_classes[i].y + un_checked_classes[i].height + 6));
                        //panelGraphics.DrawLine(new Pen(Color.Aqua), new Point(classes_pos[i].X + classz.Width + 6, classes_pos[i].Y + classz.Height + 6), new Point(classes_pos[i].X + classz.Width + 6, classes_pos[i].Y + classz.Height / 2));
                        panelGraphics.DrawLine(new Pen(Color.Blue), new Point(un_checked_classes[i].x + un_checked_classes[i].width + 6, un_checked_classes[i].y + un_checked_classes[i].height + 6), new Point(un_checked_classes[i].x + un_checked_classes[i].width + 6, un_checked_classes[i].y + un_checked_classes[i].height / 2));
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

        /////////////////
        public void draw_inheritance()
        {
            Pen inheritance_pen = new Pen(Color.Brown);
            Pen end = new Pen(Color.Green, 5);
            end.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

            for (int i = 0; i < un_checked_classes.Count; i++)
            {
                for (int j = 0; j < un_checked_classes.Count; j++)
                {
                    if (i == j) { continue; }
                    if (un_checked_classes[i].ParentClassName != null)
                    {
                        if (un_checked_classes[i].ParentClassName.Contains(un_checked_classes[j].ClassName))
                        {

                            // if (un_checked_classes[i].x < un_checked_classes[j].x)
                            //{


                            for (int k = 0; k < largest_levels_height.Count; k++)
                            {
                                if (i < largest_levels_height.ElementAt(k).Key)
                                {
                                    panelGraphics.DrawLine(inheritance_pen, un_checked_classes[i].x + un_checked_classes[i].width / 2, un_checked_classes[i].y, un_checked_classes[i].x + un_checked_classes[i].width / 2, un_checked_classes[i].y - 7);
                                    panelGraphics.DrawLine(inheritance_pen, un_checked_classes[i].x + un_checked_classes[i].width / 2, un_checked_classes[i].y - 7, un_checked_classes[i].x - 7, un_checked_classes[i].y - 7);
                                    panelGraphics.DrawLine(inheritance_pen, un_checked_classes[i].x - 7, un_checked_classes[i].y - 7, un_checked_classes[i].x - 7, un_checked_classes[j].y + largest_levels_height.ElementAt(k).Value + space_bet_lines);
                                    panelGraphics.DrawLine(inheritance_pen, un_checked_classes[i].x - 7, un_checked_classes[j].y + largest_levels_height.ElementAt(k).Value + space_bet_lines, un_checked_classes[j].x + un_checked_classes[j].width / 2, un_checked_classes[j].y + largest_levels_height.ElementAt(k).Value + space_bet_lines);
                                    panelGraphics.DrawLine(inheritance_pen, un_checked_classes[j].x + un_checked_classes[j].width / 2, un_checked_classes[j].y + largest_levels_height.ElementAt(k).Value + space_bet_lines, un_checked_classes[j].x + un_checked_classes[j].width / 2, un_checked_classes[j].y + un_checked_classes[j].height);
                                    panelGraphics.DrawLine(end, un_checked_classes[j].x + un_checked_classes[j].width / 2, un_checked_classes[j].y + un_checked_classes[j].height + 5, un_checked_classes[j].x + un_checked_classes[j].width / 2, un_checked_classes[j].y + un_checked_classes[j].height);
                                    space_bet_lines += 10;
                                    break;
                                }

                            }
                            //}



                            /*else if (un_checked_classes[i].x > un_checked_classes[j].x)
                            {


                                for (int k = 0; k < largest_levels_height.Count; k++)
                                {
                                    if (i < largest_levels_height.ElementAt(k).Key)
                                    {
                                        panelGraphics.DrawLine(inheritance_pen, un_checked_classes[i].x + un_checked_classes[i].width / 2, un_checked_classes[i].y, un_checked_classes[i].x + un_checked_classes[i].width / 2, un_checked_classes[i].y - 7);
                                        panelGraphics.DrawLine(inheritance_pen, un_checked_classes[i].x + un_checked_classes[i].width / 2, un_checked_classes[i].y - 7, un_checked_classes[i].x - 7, un_checked_classes[i].y - 7);
                                        panelGraphics.DrawLine(inheritance_pen, un_checked_classes[i].x - 7, un_checked_classes[i].y - 7, un_checked_classes[i].x - 7, un_checked_classes[j].y + largest_levels_height.ElementAt(k).Value + space_bet_lines);
                                        panelGraphics.DrawLine(inheritance_pen, un_checked_classes[i].x - 7, un_checked_classes[j].y + largest_levels_height.ElementAt(k).Value + space_bet_lines, un_checked_classes[j].x + un_checked_classes[j].width / 2, un_checked_classes[j].y + largest_levels_height.ElementAt(k).Value + space_bet_lines);
                                        panelGraphics.DrawLine(inheritance_pen, un_checked_classes[j].x + un_checked_classes[j].width / 2, un_checked_classes[j].y + largest_levels_height.ElementAt(k).Value + space_bet_lines, un_checked_classes[j].x + un_checked_classes[j].width / 2, un_checked_classes[j].y + un_checked_classes[j].height);
                                        panelGraphics.DrawLine(end, un_checked_classes[j].x + un_checked_classes[j].width / 2, un_checked_classes[j].y + un_checked_classes[j].height + 5, un_checked_classes[j].x + un_checked_classes[j].width / 2, un_checked_classes[j].y + un_checked_classes[j].height);
                                        space_bet_lines += 10;
                                        break;
                                    }
                                }
                            }

                            else
                            {
                                if (un_checked_classes[i].y < un_checked_classes[j].y)
                                {
                                    panelGraphics.DrawLine(inheritance_pen, new Point(un_checked_classes[i].x + un_checked_classes[i].width, un_checked_classes[i].y + un_checked_classes[i].height), new Point(un_checked_classes[j].x + un_checked_classes[j].width, un_checked_classes[j].y));
                                }
                                else
                                {
                                    panelGraphics.DrawLine(inheritance_pen, new Point(un_checked_classes[i].x + un_checked_classes[i].width, un_checked_classes[i].y), new Point(un_checked_classes[j].x + un_checked_classes[j].width, un_checked_classes[j].y + un_checked_classes[j].height));
                                }

                            }*/

                        }
                    }
                }
            }
        }
        //////////////////



        /// <summary>
        public void draw_dependency()
        {
            Pen dependency_pen = new Pen(Color.Purple, 3);
            dependency_pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

            for (int i = 0; i < un_checked_classes.Count; i++)
            {
                for (int j = 0; j < un_checked_classes.Count; j++)
                {
                    if (i == j) { continue; }
                    for (int f = 0; f < un_checked_functions[i].Count; f++)
                    {

                        if (un_checked_functions[i][f].FunctionParameter.Contains(un_checked_classes[j].ClassName + " ") || un_checked_functions[i][f].FunctionParameter.Contains(un_checked_classes[j].ClassName + ","))
                        {



                            if (un_checked_classes[i].x < un_checked_classes[j].x && un_checked_classes[j].x - un_checked_classes[i].x == 200)
                            {
                                dash_arrow(un_checked_classes[i].x + un_checked_classes[i].width, un_checked_classes[i].y, un_checked_classes[j].x , un_checked_classes[j].y);
                                panelGraphics.DrawLine(dependency_pen, un_checked_classes[j].x -1, un_checked_classes[j].y, un_checked_classes[j].x , un_checked_classes[j].y);
                            }
                            else if (un_checked_classes[j].x < un_checked_classes[i].x && un_checked_classes[i].x - un_checked_classes[j].x == 200)
                            {
                                dash_arrow(un_checked_classes[i].x, un_checked_classes[j].y, un_checked_classes[j].x + un_checked_classes[j].width, un_checked_classes[j].y);
                                panelGraphics.DrawLine(dependency_pen, un_checked_classes[j].x + un_checked_classes[j].width, un_checked_classes[j].y, un_checked_classes[j].x + un_checked_classes[j].width - 1, un_checked_classes[j].y);
                            }

                            else if (un_checked_classes[i].x < un_checked_classes[j].x && Math.Abs(un_checked_classes[j].x - un_checked_classes[i].x) > 200)
                            {
                                for (int k = 0; k < largest_levels_height.Count; k++)
                                {
                                    if (i < largest_levels_height.ElementAt(k).Key)
                                    {
                                        /*panelGraphics.DrawLine(Pens.Aqua, new Point(un_checked_classes[i].x + un_checked_classes[i].width, un_checked_classes[i].y + 12), new Point(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12));
                                    panelGraphics.DrawLine(Pens.Aqua, new Point(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12), new Point(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12 + largest_levels_height.ElementAt(k).Value + space_bet_lines));
                                    panelGraphics.DrawLine(Pens.Aqua, new Point(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12 + largest_levels_height.ElementAt(k).Value + space_bet_lines), new Point(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[i].y + 12 + largest_levels_height.ElementAt(k).Value + space_bet_lines));
                                    panelGraphics.DrawLine(Pens.Aqua, new Point(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[i].y + 12 + largest_levels_height.ElementAt(k).Value + space_bet_lines), new Point(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[j].y + 12));
                                    panelGraphics.DrawLine(association_pen, new Point(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[j].y + 12), new Point(un_checked_classes[j].x + un_checked_classes[j].width, un_checked_classes[j].y + 12));*/

                                        dash_line(un_checked_classes[i].x + un_checked_classes[i].width, un_checked_classes[i].y, un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y);
                                        dash_line(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y, un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + largest_levels_height.ElementAt(k).Value + space_bet_lines);
                                        dash_line(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + largest_levels_height.ElementAt(k).Value + space_bet_lines, un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[i].y + largest_levels_height.ElementAt(k).Value + space_bet_lines);
                                        dash_line(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[i].y + largest_levels_height.ElementAt(k).Value + space_bet_lines, un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[j].y);
                                        dash_arrow(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[j].y, un_checked_classes[j].x + un_checked_classes[j].width + 4, un_checked_classes[j].y);
                                        panelGraphics.DrawLine(dependency_pen, un_checked_classes[j].x + un_checked_classes[j].width + 4, un_checked_classes[j].y, un_checked_classes[j].x + un_checked_classes[j].width, un_checked_classes[j].y);

                                        space_bet_lines += 10;

                                        break;

                                    }


                                }





                            }

                            else if (un_checked_classes[j].x < un_checked_classes[i].x && Math.Abs(un_checked_classes[i].x - un_checked_classes[j].x) > 200)
                            {
                                for (int k = 0; k < largest_levels_height.Count; k++)
                                {
                                    if (i < largest_levels_height.ElementAt(k).Key)
                                    {
                                        /*panelGraphics.DrawLine(Pens.Aqua, new Point(un_checked_classes[i].x + un_checked_classes[i].width, un_checked_classes[i].y + 12), new Point(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12));
                                    panelGraphics.DrawLine(Pens.Aqua, new Point(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12), new Point(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12 + largest_levels_height.ElementAt(k).Value + space_bet_lines));
                                    panelGraphics.DrawLine(Pens.Aqua, new Point(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + 12 + largest_levels_height.ElementAt(k).Value + space_bet_lines), new Point(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[i].y + 12 + largest_levels_height.ElementAt(k).Value + space_bet_lines));
                                    panelGraphics.DrawLine(Pens.Aqua, new Point(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[i].y + 12 + largest_levels_height.ElementAt(k).Value + space_bet_lines), new Point(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[j].y + 12));
                                    panelGraphics.DrawLine(association_pen, new Point(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[j].y + 12), new Point(un_checked_classes[j].x + un_checked_classes[j].width, un_checked_classes[j].y + 12));*/

                                        dash_line(un_checked_classes[i].x + un_checked_classes[i].width, un_checked_classes[i].y, un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y);
                                        dash_line(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y, un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + largest_levels_height.ElementAt(k).Value + space_bet_lines);
                                        dash_line(un_checked_classes[i].x + un_checked_classes[i].width + 5, un_checked_classes[i].y + largest_levels_height.ElementAt(k).Value + space_bet_lines, un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[i].y + largest_levels_height.ElementAt(k).Value + space_bet_lines);
                                        dash_line(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[i].y + largest_levels_height.ElementAt(k).Value + space_bet_lines, un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[j].y);
                                        dash_arrow(un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[j].y, un_checked_classes[j].x + un_checked_classes[j].width + 4, un_checked_classes[j].y);
                                        panelGraphics.DrawLine(dependency_pen, un_checked_classes[j].x + un_checked_classes[j].width + 4, un_checked_classes[j].y, un_checked_classes[j].x + un_checked_classes[j].width, un_checked_classes[j].y);
                                        space_bet_lines += 10;

                                        break;

                                    }


                                }





                            }

                            else if (un_checked_classes[i].y != un_checked_classes[j].y && un_checked_classes[i].x == un_checked_classes[j].x)
                            {
                                dash_line(un_checked_classes[i].x + un_checked_classes[i].width, un_checked_classes[i].y, un_checked_classes[i].x + un_checked_classes[i].width + 10, un_checked_classes[i].y);
                                dash_line(un_checked_classes[i].x + un_checked_classes[i].width + 10, un_checked_classes[i].y, un_checked_classes[i].x + un_checked_classes[i].width + 10, un_checked_classes[j].y);
                                dash_line(un_checked_classes[i].x + un_checked_classes[i].width + 10, un_checked_classes[j].y, un_checked_classes[j].x + un_checked_classes[j].width, un_checked_classes[j].y);
                                panelGraphics.DrawLine(dependency_pen, un_checked_classes[j].x + un_checked_classes[j].width + 5, un_checked_classes[j].y, un_checked_classes[j].x + un_checked_classes[j].width, un_checked_classes[j].y);
                            }

                        }
                    }
                }
            }
        }


        public void dash_arrow(float x1, float y1, float x2, float y2)
        {
            // Set the SmoothingMode property to smooth the line.
            panelGraphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Create a new Pen object.
            Pen greenPen = new Pen(Color.Purple);

            // Set the width to 6.
            greenPen.Width = 1.0F;

            // Set the DashCap to round.
            greenPen.DashCap = System.Drawing.Drawing2D.DashCap.Round;
            //greenPen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

            // Create a custom dash pattern.
            greenPen.DashPattern = new float[] { 4.0F, 4.0F, 4.0F, 4.0F };

            // Draw a line.
            panelGraphics.DrawLine(greenPen, x1, y1, x2, y2);

            // Change the SmoothingMode to none.
            panelGraphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.None;
            // Dispose of the custom pen.
            greenPen.Dispose();
        }

        public void dash_line(float x1, float y1, float x2, float y2)
        {
            // Set the SmoothingMode property to smooth the line.
            panelGraphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Create a new Pen object.
            Pen greenPen = new Pen(Color.Purple);

            // Set the width to 6.
            greenPen.Width = 1.0F;

            // Set the DashCap to round.
            greenPen.DashCap = System.Drawing.Drawing2D.DashCap.Round;

            // Create a custom dash pattern.
            greenPen.DashPattern = new float[] { 4.0F, 4.0F, 4.0F, 4.0F };

            // Draw a line.
            panelGraphics.DrawLine(greenPen, x1, y1, x2, y2);

            // Change the SmoothingMode to none.
            panelGraphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.None;
            // Dispose of the custom pen.
            greenPen.Dispose();
        }
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="location"></param>
        /// <returns></returns>



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


        //---------------------------------------------------------------------------------
        private Font FindFont(System.Drawing.Graphics g, List<string> longString, Size Room, Font PreferedFont)
        {
            //you should perform some scale functions!!!
            SizeF RealSize = new SizeF();
            for (int i = 0; i < longString.Count; i++)
            {
               
                if (RealSize.Width > g.MeasureString(longString[i], PreferedFont).Width)
                {
                    RealSize = g.MeasureString(longString[i], PreferedFont);
                }
            }
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
        //--------------------------------------------------------------------------------


        public ClassDiagramForm()
        {

            InitializeComponent();

            StringAccessors accessor = StringAccessors.Instance;
            richTextBox1.Text=accessor.getCode();

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

                e.Graphics.Clear(Color.SeaShell);
                //parserHelper.CodeNodes.Clear();
                parserHelper.CodeNodesForEachClass.Clear();
                y = 0;
                y_dash = 0;
                //class_posx = 2;
                class_posx = 20;
                // class_posy = 2;
                class_posy = 50;
                classes_pos.Clear();
                un_checked_classes.Clear();
                largest_levels_height.Clear();
                class_order = 0;
                space_bet_lines = 10;
                e.Graphics.TranslateTransform(panel1.AutoScrollPosition.X, panel1.AutoScrollPosition.Y);
                panelGraphics = e.Graphics;



                DrawClassDiagram();
            }


        }






        private void btn_ClassDiagram_Click(object sender, EventArgs e)
        {
            
                ClassDiagramClicked = true;
                panel1.Refresh();
            
            
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
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


            List<FunctionsInEachClass> tempf = new List<FunctionsInEachClass>();
            FunctionsInEachClass f = new FunctionsInEachClass();
            bool IsClean = true;
            foreach (var el in parserHelper.CodeNodesForEachClass)
            {
                if (el.Key.ClassAttributes.Count() != 0) {
                    for(int l = 0;l< el.Key.ClassAttributes.Count(); l++)
                    {
                        Node ss = new Node();
                        ss.CodeLine = el.Key.ClassAttributes[l];
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


            for (int h = 0; h < tempf.Count(); h++)
            {

                gogo.declerations_collector(tempf[h].nodesInEachFunction);
                gogo.detect_wrong();
                gogo.coloring_wrong(richTextBox1);
                IsClean = (gogo.wrong_names.Count == 0);
                if(IsClean == false)
                {
                    break;
                }
            }
            return IsClean;
        }
         private void ClassDiagramForm_Load(object sender, EventArgs e)
        {
            gogo.aval_words();
        }

        ///////////////////////////////////////////////////////////////////////////

        private void DrawClassDiagram()
        {
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
                if (parserHelper.CodeNodesForEachClass.Keys.Count != 0)
                {

                    un_checked_classes = parserHelper.CodeNodesForEachClass.Keys.ToList();
                    un_checked_functions = parserHelper.CodeNodesForEachClass.Values.ToList();

                    draw_class(/*parserHelper.CodeNodesForEachClass*/);
                    draw_assosiation();
                    draw_inheritance();
                    draw_dependency();
                }
                else { MessageBox.Show("ERROR You have entered a code that doesn't contain any functions"); }
                //draw_class(&el);
                //}
            }

           /* else
            {
                MessageBox.Show("ERROR You have entered a code that doesn't contain any functions");
            }*/

        }


        private void btn_Back_Click(object sender, EventArgs e)
        {

            FirstForm first = new FirstForm();
            this.Hide();
            first.Show();
        }

        private void ClassDiagramForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        ///////////////////////////////////////////////////////////////////////////
    }
}
