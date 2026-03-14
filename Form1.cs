using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            dgvTokens.Columns.Clear();
            dgvTokens.Columns.Add("Token", "Token");
            dgvTokens.Columns.Add("Type", "Type");

            var scanner = new Scanner();
            var tokens = scanner.Scan(txtInput.Text);

            dgvTokens.Rows.Clear();
            foreach (var token in tokens)
                dgvTokens.Rows.Add(token.Value, token.Type.ToString());
        }
    }
}
