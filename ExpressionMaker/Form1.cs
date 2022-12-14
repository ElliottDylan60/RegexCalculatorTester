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
using ExpressionMaker;

namespace ExpressionMaker
{
    public partial class Dashboard : Form
    {
        /// <summary>
        /// All possible valid syntax
        /// </summary>
        List<string> validSyntax = new List<string>();
        List<string> syntaxErrors = new List<string>() { "NaN", "Divide By Zero", "-∞", "∞", "Syntax Error", "Overflow"};
        // Used to move form around screen
        private bool mouseDown;
        private Point lastLocation;
        int passed = 0;
        int total = 0;
        bool stop = false;
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
            validSyntax.Add("{i}—{i}");
            validSyntax.Add("{i}*{i}");
            validSyntax.Add("{i}/{i}");
            validSyntax.Add("{i}^{i}");
            validSyntax.Add("sin({i})");
            validSyntax.Add("cos({i})");
            validSyntax.Add("tan({i})");
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
        #region UI
        /// <summary>
        /// Generate any number of equations
        /// </summary>
        private void btnGenerate_Click(object sender, EventArgs e)
        {


            if (stop)
            {
                btnGenerate.Text = "Generate";
                stop = false;
                Console.WriteLine("Stopping");
                return;
            }
            stop = true;
            btnGenerate.Text = "Stop";
            Console.WriteLine("Starting");

            passed = 0;
            total = (int)numEquations.Value;
            richTextBox1.Clear(); // Clear Textbox
            lbEmpty.Visible = false; // Hide label

            Generator.RunWorkerAsync(); // Initialize thread

        }
        #endregion
        #region HelperFunctins
        /// <summary>
        /// Writes to rich text box
        /// </summary>
        /// <param name="Message">Print first part as normal color</param>
        /// <param name="redMessage">Print second part as specified color</param>
        /// <param name="color">Color of second part</param>
        private void writeLog(string Message, string colorMessage, Color color)
        {
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
        /// <summary>
        /// Updates the total passed equations
        /// </summary>
        private void updateTotal()
        {
            lbPassed.Text = passed + "/" + total + " Passed";
        }
        #endregion

        #region ExpressionGenerator
        private void Generator_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int a = 0; a < (int)numEquations.Value; a++) // Generate given number of equation
            {
                if (!stop)
                {
                    total = a;
                    stop = false;
                    Generator.ReportProgress(100, "Complete!");
                    return;
                }
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
                expression = Regex.Replace(expression, "{i}", m => (random.Next() % 2 == 0) ? Math.Round(random.NextDouble() * 100, 5).ToString() : Math.Round(random.NextDouble() * 9999, 5).ToString());

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
        private void EvalualteExpressoins(string expression)
        {
            // Delegate allows changes to UI on seperate thread
            this.Invoke((MethodInvoker)delegate {
                try
                {
                    Calc calculator = new Calc();
                    string myAns = calculator.PostFixEvaluator(calculator.toPostFix(calculator.TokenizeEquation(expression)));

                    expression = Regex.Replace(expression, "—", "-");

                    string oracleAns;
                    double oracle = 0;
                    try
                    {
                        ExpressionContext context = new ExpressionContext();
                        context.Imports.AddType(typeof(MyMath));
                        IDynamicExpression eDynamic = context.CompileDynamic(expression);
                        oracleAns = eDynamic.Evaluate().ToString();
                        oracle = (double)eDynamic.Evaluate();
                    }
                    catch (OverflowException)
                    {
                        oracleAns = "Overflow";
                    }
                    catch (Exception)
                    {
                        oracleAns = "Syntax Error";
                    }

                    if (syntaxErrors.Contains(oracleAns) || syntaxErrors.Contains(myAns))
                    {
                        passed++;
                        writeLog(expression + " ---------------- ", "Passed" + "\n", Color.Green);
                        return;
                    }


                    double ans = Convert.ToDouble(myAns);

                    oracleAns = oracle.ToString("E3");
                    myAns = ans.ToString("E3");
                    if (Math.Round(ans, 2) == Math.Round(oracle, 2) || myAns.Equals(oracleAns) || Math.Abs(ans) - Math.Abs(oracle) < 1)
                    {
                        passed++;
                        writeLog(expression + " ---------------- ", "Passed" + "\n", Color.Green);
                    }
                    else
                    {
                        writeLog(expression + " ---------------- ", "Failed" + "\n", Color.Red);
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
            updateTotal();
        }

        private void Generator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnGenerate.Text = "Generate";
            stop = false;
        }


        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
#region CustomFleeFunctions
/*
    Custom functinos for Flee to use 
*/
public static class CustomFunctions {
    public static double sin(double val) {
        return MyMath.sin(val);
    }
    public static double cos(double val) {
        return MyMath.cos(val);
    }
    public static double tan(double val) {
        return MyMath.tan(val); ;
    }
    public static double cot(double val) {
        return 1 / Math.Tan(val);
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
#endregion