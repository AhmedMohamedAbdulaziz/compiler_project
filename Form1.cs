using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace project_compiler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnScanner_Click(object sender, EventArgs e)
        {
            string inputString = txtInput.Text;

            string keywords = @"\b(int|float|string|read|write|repeat|until|if|elseif|else|then|return|endl|main)\b";
            string identifiers = @"\b[a-zA-Z][a-zA-Z0-9]*\b";
            string numbers = @"\b[0-9]+(\.[0-9]+)?\b";
            string stringLit = @"""[^""]*""";
            string comments = @"/\*.*?\*/";
            string operators = @":=|<>|&&|\|\||<=|>=|[+\-*/=<>]";
            string symbols = @"[;(){},]";

            string masterPattern = $"{comments}|{stringLit}|{keywords}|{numbers}|{identifiers}|{operators}|{symbols}";

            DataTable dt = new DataTable();
            dt.Columns.Add("Lexeme");
            dt.Columns.Add("Token Type");
            MatchCollection matches = Regex.Matches(inputString, masterPattern, RegexOptions.Singleline);

            foreach (Match m in matches)
            {
                string lex = m.Value;
                string type = "";

                if (Regex.IsMatch(lex, @"^/\*.*\*/$", RegexOptions.Singleline))
                    type = "Comment";

                else if (Regex.IsMatch(lex, @"^""[^""]*""$"))
                    type = "String_Literal";

                else if (Regex.IsMatch(lex, keywords))
                    type = "Keyword";

                else if (Regex.IsMatch(lex, numbers))
                    type = "Number";

                else if (Regex.IsMatch(lex, identifiers))
                    type = "Identifier";

                // Specific classification of operators
                else if (lex == ":=") type = "Assignment_Op";
                else if (lex == "=") type = "Equal_Op";
                else if (lex == "<>") type = "NotEqual_Op";
                else if (lex == "&&") type = "AND_Op";
                else if (lex == "||") type = "OR_Op";
                else if (lex == "+") type = "Plus_Op";
                else if (lex == "-") type = "Minus_Op";
                else if (lex == "*") type = "Multiply_Op";
                else if (lex == "/") type = "Divide_Op";
                else if (lex == ">") type = "Greater_Than_Op";
                else if (lex == "<") type = "Less_Than_Op";
                else if (lex == "<=") type = "LessOrEqual_Op";
                else if (lex == ">=") type = "GreaterOrEqual_Op";

                // Classifying symbols in a specific way
                else if (lex == ";") type = "Semicolon";
                else if (lex == "(") type = "Left_Paren";
                else if (lex == ")") type = "Right_Paren";
                else if (lex == "{") type = "Left_Brace";
                else if (lex == "}") type = "Right_Brace";
                else if (lex == ",") type = "Comma";

                else type = "Unknown";

                dt.Rows.Add(lex, type);
            }

            dgvTokens.DataSource = dt;
        }
    }

    }

