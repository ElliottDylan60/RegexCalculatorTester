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
    public partial class Form1 : Form
    {
        List<string> validSyntax = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }
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
        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeValidSyntax();
            string expression = "{i}";  
            Console.WriteLine(expression);
            // for each variable in the expressin replacee it with a validSyntax expression
            for (int j = 0; j < 5; j++) {
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
                Console.WriteLine(expression);
                expression = Regex.Replace(expression, @"[0-9]+", m => "i", RegexOptions.IgnoreCase);
            }




            /*
            var s = "({100}+cos({1}))";
            var i = 0;
            //var result = Regex.Replace(s, "i", m => string.Format("{0}", i++), RegexOptions.IgnoreCase); // replace in s, value "i", with m where m = i++
            var result = Regex.Replace(s, @"[0-9]+", m => "i", RegexOptions.IgnoreCase); // replaces any number in string s with an i
            Console.WriteLine(result);

            
            string[] test = new string[] { "test", "test", "test"};
            Console.WriteLine("First {0} Second {1} Third {2}", test);
            */
        }
    }
}
