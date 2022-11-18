﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using NCalc;

namespace ExpressionMaker
{
    public partial class Dashboard : Form
    {
        /// <summary>
        /// All possible valid syntax
        /// </summary>
        List<string> validSyntax = new List<string>();
        /// <summary>
        /// All possible invalid syntax
        /// </summary>
        List<string> invalidSyntax = new List<string>();
        // Used to move form around screen
        private bool mouseDown;
        private Point lastLocation;
        //Expression test = new Expression("ln(tan(cos(3719^19272)*cos(183928^192384)))");
        Expression test = new Expression("Log(10,Tan(Cos(5^5)*Cos(5^5)))");
        /// <summary>
        /// Initialize Components
        /// </summary>
        public Dashboard()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Initialize valid syntax list
        /// </summary>
        private void InitializeValidSyntax() {
            validSyntax.Add("({i})");
            validSyntax.Add("-{i}");
            validSyntax.Add("{i}+{i}");
            validSyntax.Add("{i}-{i}");
            validSyntax.Add("{i}*{i}");
            validSyntax.Add("{i}/{i}");
            validSyntax.Add("{i}^{i}");
            validSyntax.Add("Sin({i})");
            validSyntax.Add("Cos({i})");
            validSyntax.Add("Tan({i})");
            validSyntax.Add("Csc({i})");
            validSyntax.Add("Log(10,{i})");
            //validSyntax.Add("ln({i})");
        }
        /// <summary>
        /// Initialize invalid syntax list
        /// </summary>
        private void InitilizeInvalidSyntax() { 
        
        }
        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                mouseDown = true;
                lastLocation = e.Location;
            }
            catch (Exception a)
            {
            }
        }
        private void Main_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                mouseDown = false;
            }
            catch (Exception a)
            {
            }
        }
        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (mouseDown)
                {
                    this.Location = new Point(
                        (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                    this.Update();
                }
            }
            catch (Exception a)
            {
            }
        }
        /// <summary>
        /// When Form loads
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeValidSyntax();
            test.Parameters["e"] = Math.E;
            
            Console.WriteLine(test.Evaluate().ToString());
        }
        /// <summary>
        /// Generate any number of equations
        /// </summary>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear(); // Clear Textbox
            lbEmpty.Visible = false; // Hide label


            btnGenerate.Enabled = false; // Disable button to prevent same thread from being initialized twice
            Generator.RunWorkerAsync(); // Initialize thread
            btnGenerate.Enabled = true; // Enable button
            
        }
        /// <summary>
        /// Writes to rich text box
        /// </summary>
        /// <param name="Message">Print first part as normal color</param>
        /// <param name="redMessage">Print second part as specified color</param>
        /// <param name="color">Color of second part</param>
        private void writeLog(string Message, string colorMessage, Color color) {
            /// Normal
            richTextBox1.AppendText(Message);
            richTextBox1.SelectionStart = richTextBox1.TextLength;

            /// Red
            richTextBox1.SelectionColor = color;
            richTextBox1.AppendText(colorMessage);

            /// Normal
            richTextBox1.SelectionColor = richTextBox1.ForeColor;
            
            // Scroll to end of textbox
            richTextBox1.ScrollToCaret();
        }

        private void Generator_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int a = 0; a < (int)numEquations.Value; a++)
            {
                string expression = "{i}";
                // for each variable in the expressin replacee it with a validSyntax expression
                for (int j = 0; j < (int)numIterations.Value; j++)
                {
                    int i = 0;
                    expression = Regex.Replace(expression, "{i}", m => string.Format("{{0}}", i++), RegexOptions.IgnoreCase); // replace all i with number
                    string[] expressionsToAdd = new string[i];
                    while (i > 0)
                    {
                        i--;
                        Random rand = new Random(Guid.NewGuid().GetHashCode());
                        expressionsToAdd[i] = validSyntax[rand.Next(validSyntax.Count())];
                    }
                    expression = String.Format(expression, expressionsToAdd);
                    expression = Regex.Replace(expression, @"[0-9]+", m => "i", RegexOptions.IgnoreCase);
                }
                // Report current generation percent
                Generator.ReportProgress((int)((a / numEquations.Value) * 100), "Working...");
                // Delegate to allow changes on sperate UI
                this.Invoke((MethodInvoker)delegate{
                    writeLog(expression + " ---------------- ", "PASSED\n", Color.Green);
                });
            }
            Generator.ReportProgress(100, "Complete!");
        }

        private void Generator_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //Console.WriteLine(e.ProgressPercentage);
            progressBar1.Value = e.ProgressPercentage;
        }
    }
}
