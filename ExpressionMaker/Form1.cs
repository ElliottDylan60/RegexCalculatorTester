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
using Calculator;

namespace ExpressionMaker
{
    public partial class Dashboard : Form
    {
        /// <summary>
        /// All possible valid syntax
        /// </summary>
        List<string> validSyntax = new List<string>();
        List<string> syntaxErrors = new List<string>() { "NaN", "Divide By Zero", "-∞", "∞"};
        // Used to move form around screen
        private bool mouseDown;
        private Point lastLocation;
        

        /// <summary>
        /// Initialize Components
        /// </summary>
        public Dashboard()
        {
            InitializeComponent();
            ExpressionContext context = new ExpressionContext();
            context.Imports.AddType(typeof(CustomFunctions));
            IDynamicExpression eDynamic = context.CompileDynamic("log(0)");
            Console.WriteLine(eDynamic.Evaluate().ToString());
            //Console.WriteLine(Math.Log10(0));
            writeLog("", Math.Log10(0).ToString(), Color.Red);
        }
        /// <summary>
        /// Initialize valid syntax list
        /// </summary>
        private void InitializeValidSyntax() {
            validSyntax.Add("({i})");
            validSyntax.Add("-({i})");
            validSyntax.Add("{i}+{i}");
            validSyntax.Add("{i}—{i}");
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

            /// Color
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

                EvalualteExpressoins(expression);
                
            }
            Generator.ReportProgress(100, "Complete!");
        }
        /// <summary>
        /// Compares given expression between oracle and my math function
        /// </summary>
        /// <param name="expression">Expression to evaluate</param>
        private void EvalualteExpressoins(string expression) {
            // Delegate allows changes to UI on seperate thread
            this.Invoke((MethodInvoker)delegate {
                try
                {
                    Calc calculator = new Calc();


                    string myAns = calculator.PostFixEvaluator(calculator.toPostFix(calculator.TokenizeEquation(expression)));

                    expression = Regex.Replace(expression, "—", "-");


                    expression = "Round(" + expression + ")";
                    string oracleAns;
                    try
                    {
                        ExpressionContext context = new ExpressionContext();
                        context.Imports.AddType(typeof(CustomFunctions));
                        IDynamicExpression eDynamic = context.CompileDynamic(expression);
                        oracleAns = eDynamic.Evaluate().ToString();
                    }
                    catch (DivideByZeroException) {
                        oracleAns = "Syntax Error";
                    }

                    if (syntaxErrors.Contains(oracleAns) || syntaxErrors.Contains(myAns))
                    {
                        oracleAns = "Syntax Error";
                        myAns = "Syntax Error";
                    }

                    if (myAns.Equals(oracleAns))
                    {
                        //writeLog(expression + " ---------------- ", "Passed" + "\n", Color.Green);
                    }
                    else
                    {
                        writeLog(expression + " ---------------- ", myAns + " != " + oracleAns + "\n", Color.Red);

                    }

                }
                catch (EvaluationException)
                {
                    Console.WriteLine(expression);
                }

            });
        }
        private void Generator_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
    }
}

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
    public static double Round(double val) {
        return Math.Round(val, 5);
    }
}