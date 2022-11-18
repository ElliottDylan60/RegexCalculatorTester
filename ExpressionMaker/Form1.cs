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
            validSyntax.Add("sin({i})");
            validSyntax.Add("cos({i})");
            validSyntax.Add("tan({i})");
            validSyntax.Add("csc({i})");
            validSyntax.Add("log({i})");
            validSyntax.Add("ln({i})");
        }
        /// <summary>
        /// Initialize invalid syntax list
        /// </summary>
        private void InitilizeInvalidSyntax() { 
        
        }
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
            richTextBox1.Clear();
            lbEmpty.Visible = false;
            Generator.RunWorkerAsync();
            
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
