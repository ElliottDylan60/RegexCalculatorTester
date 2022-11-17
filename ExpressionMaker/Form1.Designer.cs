namespace ExpressionMaker
{
    partial class Dashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.lbEmpty = new System.Windows.Forms.Label();
            this.numIterations = new System.Windows.Forms.NumericUpDown();
            this.panel13 = new System.Windows.Forms.Panel();
            this.lbIterations = new System.Windows.Forms.Label();
            this.lbInfo = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numIterations)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.richTextBox1.ForeColor = System.Drawing.Color.LightGray;
            this.richTextBox1.Location = new System.Drawing.Point(12, 180);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(776, 258);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.panel1.Location = new System.Drawing.Point(12, 172);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 5);
            this.panel1.TabIndex = 1;
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btnGenerate.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.ForeColor = System.Drawing.Color.LightGray;
            this.btnGenerate.Location = new System.Drawing.Point(699, 131);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(89, 35);
            this.btnGenerate.TabIndex = 41;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // lbEmpty
            // 
            this.lbEmpty.AutoSize = true;
            this.lbEmpty.Font = new System.Drawing.Font("Gadugi", 24F);
            this.lbEmpty.ForeColor = System.Drawing.Color.LightGray;
            this.lbEmpty.Location = new System.Drawing.Point(215, 274);
            this.lbEmpty.Name = "lbEmpty";
            this.lbEmpty.Size = new System.Drawing.Size(365, 39);
            this.lbEmpty.TabIndex = 42;
            this.lbEmpty.Text = "No Equations Generated";
            // 
            // numIterations
            // 
            this.numIterations.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.numIterations.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numIterations.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.numIterations.ForeColor = System.Drawing.Color.LightGray;
            this.numIterations.Location = new System.Drawing.Point(12, 44);
            this.numIterations.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numIterations.Name = "numIterations";
            this.numIterations.Size = new System.Drawing.Size(176, 22);
            this.numIterations.TabIndex = 46;
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.LightGray;
            this.panel13.Location = new System.Drawing.Point(12, 68);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(176, 3);
            this.panel13.TabIndex = 45;
            // 
            // lbIterations
            // 
            this.lbIterations.AutoSize = true;
            this.lbIterations.Font = new System.Drawing.Font("Gadugi", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIterations.ForeColor = System.Drawing.Color.LightGray;
            this.lbIterations.Location = new System.Drawing.Point(12, 9);
            this.lbIterations.Name = "lbIterations";
            this.lbIterations.Size = new System.Drawing.Size(99, 25);
            this.lbIterations.TabIndex = 47;
            this.lbIterations.Text = "Iterations";
            // 
            // lbInfo
            // 
            this.lbInfo.AutoSize = true;
            this.lbInfo.Font = new System.Drawing.Font("Gadugi", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInfo.ForeColor = System.Drawing.Color.LightGray;
            this.lbInfo.Location = new System.Drawing.Point(194, 36);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(44, 42);
            this.lbInfo.TabIndex = 48;
            this.lbInfo.Text = "ⓘ";
            this.toolTip1.SetToolTip(this.lbInfo, "Complexity of generated equation");
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbInfo);
            this.Controls.Add(this.lbIterations);
            this.Controls.Add(this.numIterations);
            this.Controls.Add(this.panel13);
            this.Controls.Add(this.lbEmpty);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.richTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Dashboard";
            this.Text = "Calculator Tester";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numIterations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label lbEmpty;
        private System.Windows.Forms.NumericUpDown numIterations;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label lbIterations;
        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

