using System;
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
using Flee;
using Flee.PublicTypes;

namespace ExpressionMaker
{
    public partial class Dashboard : Form
    {
        /// <summary>
        /// All possible valid syntax
        /// </summary>
        List<string> validSyntax = new List<string>();
        // Used to move form around screen
        private bool mouseDown;
        private Point lastLocation;
        

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
            validSyntax.Add("-({i})");
            validSyntax.Add("{i}+{i}");
            validSyntax.Add("{i}-{i}");
            validSyntax.Add("{i}*{i}");
            validSyntax.Add("{i}/{i}");
            validSyntax.Add("{i}^{i}");
            validSyntax.Add("sin({i})");
            validSyntax.Add("cos({i})");
            validSyntax.Add("tan({i})");
            //validSyntax.Add("csc({i})");
            validSyntax.Add("log({i})");
            validSyntax.Add("ln({i})");
        }
        #region moveForm
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
        #endregion
        /// <summary>
        /// When Form loads
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeValidSyntax();
            //test.Parameters["e"] = Math.E;
            //Console.WriteLine(test.Evaluate().ToString());

            ExpressionContext context = new ExpressionContext();
            context.Imports.AddType(typeof(CustomFunctions));

            IDynamicExpression eDynamic = context.CompileDynamic("log(ln(-(3144^5157.02594))*-(7185^2314.95975))");
            Console.WriteLine(eDynamic.Evaluate().ToString());
            /*
             -(cos(tan(2259)/tan(7749.65836)-tan(6568)/tan(4758.85968))) 
             */
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
            for (int a = 0; a < (int)numEquations.Value; a++) // Generate given number of equation
            {
                string expression = "{i}"; // start expression with single variable
                // for each variable in the expressin replace it with a validSyntax expression
                for (int j = 0; j < (int)numIterations.Value; j++)
                {
                    
                    int i = 0; // number of expressions in equation
                    expression = Regex.Replace(expression, "{i}", m => string.Format("{{0}}", i++)); // replace all i with number
                    string[] expressionsToAdd = new string[i]; // create empty array with size equal to num expressions in equtaion
                    // foreach expression in eqution, replace it with a random valid expression
                    while (i > 0)
                    {
                        i--;
                        Random rand = new Random(Guid.NewGuid().GetHashCode());
                        expressionsToAdd[i] = validSyntax[rand.Next(validSyntax.Count())];
                    }
                    expression = String.Format(expression, expressionsToAdd); // format expression
                    expression = Regex.Replace(expression, @"[0-9]+", m => "i"); // replace all {0}...{n} with {i} : where n is an integer
                }

                // Convert {i} to random number/double
                Random random = new Random(Guid.NewGuid().GetHashCode());
                expression = Regex.Replace(expression, "{i}", m => (random.Next()%2==0) ? random.Next(9999).ToString() : Math.Round(random.NextDouble()*9999, 5).ToString());
                
                // Report current generation percent
                Generator.ReportProgress((int)((a / numEquations.Value) * 100), "Working...");


                // Delegate to allow changes on sperate UI
                this.Invoke((MethodInvoker)delegate{
                    try
                    {

                        ExpressionContext context = new ExpressionContext();
                        context.Imports.AddType(typeof(CustomFunctions));
                        IDynamicExpression eDynamic = context.CompileDynamic(expression);
                        writeLog(expression + " ---------------- ", eDynamic.Evaluate().ToString() + "\n", Color.Green);

                    }
                    catch (EvaluationException) {
                        Console.WriteLine(expression);
                    }
                    
                });
            }
            Generator.ReportProgress(100, "Complete!");
        }

        private void Generator_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
    }
}
// log(1565^1780+5456.55142^8360x6963.96565^5247.23636+610^6593)xlog(8160.07017^1190.95751+1773^5121.51305x8875^8401.86607+3336^5097.35324)
public static class CustomFunctions {
    public static double sin(double val) { 
        return Math.Sin((Math.PI / 180) * val);
    }
    public static double cos(double val) {
        return Math.Cos((Math.PI/180) * val);
    }
    public static double tan(double val) {
        return Math.Tan((Math.PI / 180) * val);
    }
    public static double cot(double val) {
        return 1 / Math.Tan((Math.PI / 180) * val);
    }
    public static double log(double val) {
        return Math.Log(val, 10);
    }
    public static double ln(double val) {
        return Math.Log(val);
    }
}