using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Calculator
{
    /// <summary>
    /// Class used to calculate Infix and postfix equations
    /// </summary>
    public class Calc
    {
        /// <summary>
        /// Determines whether or not to calculate trig functions
        /// in radians or degrees
        /// </summary>
        public bool inDeg = false;
        /// <summary>
        /// List of Error Types to check for before calling syntax error
        /// </summary>
        List<string> ErrorTypes = new List<string>() { "Divide By Zero", "Syntax Error" };

        /// <summary>
        /// Determins the precedence of each operator
        /// </summary>
        /// <param name="op">operator as a string</param>
        /// <returns>precendce score as a number</returns>
        public int precedence(string op)
        {
            switch (op)
            {
                case "+":
                case "—":
                    return 1;
                case "x":
                case "/":
                    return 2;
                case "^":
                    return 3;
                case "sin":
                case "cos":
                case "tan":
                case "log":
                case "ln":
                case "cot":
                    return 5;
                case "-":
                    return 4;
            }
            return -1;
        }
        /// <summary>
        /// Tokenizes the equation to solve large and complex numbers/equations
        /// 
        /// We Tokenize large number (i.e. 100) all as one token (['100']) vs tokenizing
        /// them as three seperate tokens (['1', '0', '0']) Otherwise the program
        /// Will think this is three seperate numbers
        /// </summary>
        /// <param name="equation">Equatoin to be tokenized</param>
        /// <returns>List of strings, i.e. the tokenized equation</returns>
        public List<string> TokenizeEquation(string equation)
        {
            var delimiters = new[] { '(', '+', '—', 'x', '/', ')', '^', '-' }; // list of delimiters to separate from
            var buffer = ""; // buffer currently set to empty, used to determine large numbers
            var result = new List<string>(); // results stored as a list
            foreach (var ch in equation) // for each character in the equation
            {
                if (delimiters.Contains(ch)) // if the character is a delimiter
                {
                    if (buffer.Length > 0) result.Add(buffer); // add buffer to results
                    result.Add(ch.ToString()); // add character to results
                    buffer = ""; // clear buffer
                }
                else // if character is a number
                {
                    if (ch.Equals(' '))
                    {
                        if (buffer.Length > 0) result.Add(buffer);
                        buffer = "";
                        continue;
                    }

                    buffer += ch; // add character to buffer
                }
            }
            if (buffer.Length > 0) result.Add(buffer); // add buffer to results
            return result;
        }
        /// <summary>
        /// Converts Infix equation to a Postfix equation
        /// Original sample came from GeeksForGeeks, More understanding and
        /// conteptual ideas from Comp Sci in 5 from Youtube
        /// </summary>
        /// <param name="inFix">Tokenized original equation</param>
        /// <returns>Tokenized Infix equation</returns>
        public List<string> toPostFix(List<string> inFix)
        {
            try
            {
                List<string> result = new List<string>(); // result stack
                Stack<string> stack = new Stack<string>(); // operator stack
                for (int i = 0; i < inFix.Count; i++) // foreach character in tokenized infix equation
                {
                    string ch = inFix[i]; // get character
                    double chNum;
                    bool isNum = Double.TryParse(ch, out chNum); // check if character is a number
                    if (isNum) // if character is a number
                    {
                        result.Add(ch); // add character to results
                    }
                    else if (ch.Equals("(")) // if open parenthesis
                    {
                        stack.Push(ch); // push to temp stack
                    }
                    else if (ch.Equals(")")) // if closed parenthesis
                    {
                        while (stack.Count > 0 && stack.Peek() != "(") // while the stack has items and the item is not a closing bracket
                        {
                            result.Add(stack.Pop()); // add non-parehtnesis functions to results
                        }
                        if (stack.Count > 0 && stack.Peek() != "(") // if last element is not an open perenthesis
                        {
                            return new List<string>(); // couldnt find open parenthesis, return syntax error
                        }
                        else
                        {
                            stack.Pop(); // found opening parenthesis, remove from stack (we dont want parenthesis in postfix equation)
                        }

                    }
                    else
                    {
                        while (stack.Count > 0 && precedence(ch) <= precedence(stack.Peek()) && stack.Peek() != "(") // check the precedence of currenct function to that in the stack
                        {
                            result.Add(stack.Pop()); // add to result if precedence is less
                        }
                        stack.Push(ch); // add to stack if precedence is more
                    }
                }
                while (stack.Count > 0) // while stack has any leftover function
                {
                    result.Add(stack.Pop()); // add them to results
                }
                if (result.Contains("("))
                { // checks if original equation only had one open parenthesis
                    return new List<string>();
                }
                return result;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
                return new List<string>();
            }
        }
        /// <summary>
        /// Evaluates a Postfix Equation
        /// Original sample came from GeeksForGeeks, More understanding and
        /// conteptual ideas from Comp Sci in 5 from Youtube
        /// </summary>
        /// <param name="PostFix">Postfix equation given as a token</param>
        /// <returns>number result of postfix equation</returns>
        public string PostFixEvaluator(List<string> PostFix)
        {
            try
            {
                Stack stack = new Stack(); // temp stack
                double a, b, ans; // first in stack, second in stack, some combination of the two
                for (int i = 0; i < PostFix.Count; i++) // foreach character in postfix equation
                {
                    string ch = PostFix[i]; // get character in equation
                    if (ch.Equals("+")) // if character is a '+' sign
                    {
                        string sa = (string)stack.Pop(); // get first number in stack
                        string sb = (string)stack.Pop(); // get second number in stack
                        a = Convert.ToDouble(sb); // convert string to double
                        b = Convert.ToDouble(sa); // convert string to double
                        ans = a + b; // calculates first + second
                        stack.Push(ans.ToString()); // adds answer to stack
                    }
                    else if (ch.Equals("—")) // if character is a '-' sign
                    {
                        string sa = (string)stack.Pop(); // get first number in stack
                        string sb = (string)stack.Pop();
                        a = Convert.ToDouble(sb); // convert string to double
                        b = Convert.ToDouble(sa); // convert string to double
                        ans = a - b; // calculates first - second
                        stack.Push(ans.ToString()); // adds answer to stack
                    }
                    else if (ch.Equals("x")) // if character is a 'x' sign
                    {
                        string sa = (string)stack.Pop(); // get first number in stack
                        string sb = (string)stack.Pop(); // get second number in stack
                        a = Convert.ToDouble(sb); // convert string to double
                        b = Convert.ToDouble(sa); // convert string to double
                        ans = a * b; // calculates first x second
                        stack.Push(ans.ToString()); // adds answer to stack
                    }
                    else if (ch.Equals("/")) // if character is a '/' sign
                    {
                        string sa = (string)stack.Pop(); // get first number in stack
                        string sb = (string)stack.Pop(); // get second number in stack
                        a = Convert.ToDouble(sb); // convert string to double
                        b = Convert.ToDouble(sa); // convert string to double
                        if (b == 0)
                        { // is denominator is a zero
                            stack.Clear(); // no need to continue computing results
                            stack.Push("Divide By Zero"); // theres a divide by zero error
                            break; // break out of loop
                        }
                        ans = a / b; // calculates first / second    
                        stack.Push(ans.ToString()); // adds answer to stack
                    }
                    else if (ch.Equals("^")) // if character is a '^' sign
                    { // if character is a '^' sign
                        string sa = (string)stack.Pop(); // get first number in stack
                        string sb = (string)stack.Pop(); // get second number in stack
                        a = Convert.ToDouble(sb); // convert string to double
                        b = Convert.ToDouble(sa); // convert string to double
                        ans = Math.Pow(a, b); // calculates first / second
                        stack.Push(ans.ToString()); // adds answer to stack
                    }
                    else if (ch.Equals("ln")) // if token is an 'ln' function
                    {
                        string sa = (string)stack.Pop(); // get first number in stack
                        a = Convert.ToDouble(sa); // convert string to double
                        ans = Math.Log(a); // calculate log of number in base e
                        stack.Push(ans.ToString()); // push number to stack
                    }
                    else if (ch.Equals("log")) // if token is a 'log' function
                    {
                        string sa = (string)stack.Pop(); // get first number in stack
                        a = Convert.ToDouble(sa); // convert string to double
                        ans = Math.Log(a, 10); // calculate log of number in base 10
                        stack.Push(ans.ToString()); // push result to stack
                    }
                    else if (ch.Equals("sin")) // if token is a 'sin' function
                    {
                        if (inDeg) // if user wants result in degrees
                        {
                            string sa = (string)stack.Pop(); // get first number in stack
                            a = Convert.ToDouble(sa); // convert string to double
                            ans = Math.Sin(a); // get sin of number
                        }
                        else // if user wants results in radians
                        {
                            string sa = (string)stack.Pop(); // get first number in stack
                            a = Convert.ToDouble(sa); // convert string to double
                            ans = Math.Sin((Math.PI / 180) * a); // Multiple a by pi/180 to convert to radians
                        }
                        stack.Push(ans.ToString()); // push result to stacl

                    }
                    else if (ch.Equals("cos")) // if token is a 'cos' function
                    {
                        if (inDeg) // if user wants results in degrees
                        {
                            string sa = (string)stack.Pop(); // get first number in stack
                            a = Convert.ToDouble(sa); // convert string to double
                            ans = Math.Cos(a); // calculate cos of number
                        }
                        else // if user wants results in radians
                        {
                            string sa = (string)stack.Pop(); // get first number in stack
                            a = Convert.ToDouble(sa); // convert string to double
                            ans = Math.Cos((Math.PI / 180) * a); // Multiple a by pi/180 to convert to radians
                        }
                        stack.Push(ans.ToString()); // push result to stack
                    }
                    else if (ch.Equals("tan")) // if token is a 'tan' function
                    {
                        if (inDeg) // if user wants results in degrees
                        {
                            string sa = (string)stack.Pop(); // get first number from stack
                            a = Convert.ToDouble(sa); // convert string to double
                            ans = Math.Tan(a); // calculate tan of number
                        }
                        else // if user wants results in radians
                        {
                            string sa = (string)stack.Pop(); // get first number from stack
                            a = Convert.ToDouble(sa); // convert string to double
                            ans = Math.Tan((Math.PI / 180) * a); // Multiple a by pi/180 to convert to radians
                        }
                        stack.Push(ans.ToString()); // push result to stack
                    }
                    else if (ch.Equals("cot"))
                    {
                        if (inDeg) // if user wants results in degrees
                        {
                            string sa = (string)stack.Pop(); // get first number from stack
                            a = Convert.ToDouble(sa); // convert string to double
                            ans = (Math.Cos(a)) / (Math.Sin(a)); // calculate tan of number
                        }
                        else // if user wants results in radians
                        {
                            string sa = (string)stack.Pop(); // get first number from stack
                            a = Convert.ToDouble(sa); // convert string to double
                            ans = (Math.Cos((Math.PI / 180) * a)) / (Math.Sin((Math.PI / 180) * a)); // Multiple a by pi/180 to convert to radians
                        }
                        stack.Push(ans.ToString()); // push result to stack
                    }
                    else if (ch.Equals("-"))  // if character is an '-' function
                    {
                        if (stack.Count <= 0)
                        {

                            string temp = PostFix[i + 1];
                            if (temp.Equals("-"))
                            {
                                i++;
                                continue;
                            }
                            int num = Int32.Parse(temp);
                            num = -num;
                            PostFix[i + 1] = num.ToString();
                        }
                        else
                        {
                            string sa = (string)stack.Pop(); // get first number from stack
                            if (!sa.Equals("-"))
                            { // double negative, Cancels out do nothing
                                a = Convert.ToDouble(sa); // convert string to double
                                ans = -a; // switch the sign of the number (i.e. if positive, turn nagative and vise versa)
                                stack.Push(ans.ToString()); // push result to stack
                            }
                        }

                    }
                    else // if character is a number
                    {
                        stack.Push(PostFix[i]); // add number to stack
                    }


                }/*
                if (stack.Count > 1) {
                    return "Syntax Error";
                }*/
                string result = (string)stack.Pop(); // pop result

                if (ErrorTypes.Contains(result))// if result is an error
                {
                    return result; // return error type
                }
                double resultAns = Convert.ToDouble(result); // convert result to double
                resultAns = Math.Round(resultAns, 5); // round result to 5 decimal points
                return resultAns.ToString(); // final number in stack is the answer
            }
            catch (Exception err) // Error has ben caught, send syntax error to user
            {
                Console.WriteLine(err.ToString()); // print error for debugging
                return "Syntax Error"; // return message for user
            }
        }
    }
}
