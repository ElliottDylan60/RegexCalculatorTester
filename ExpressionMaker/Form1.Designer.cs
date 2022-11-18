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
            this.label1 = new System.Windows.Forms.Label();
            this.numEquations = new System.Windows.Forms.NumericUpDown();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.Generator = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.numIterations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEquations)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.richTextBox1.ForeColor = System.Drawing.Color.LightGray;
            this.richTextBox1.Location = new System.Drawing.Point(12, 92);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(776, 244);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.panel1.Location = new System.Drawing.Point(12, 84);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 5);
            this.panel1.TabIndex = 1;
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btnGenerate.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.ForeColor = System.Drawing.Color.LightGray;
            this.btnGenerate.Location = new System.Drawing.Point(699, 43);
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
            this.lbEmpty.Location = new System.Drawing.Point(215, 186);
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
            this.numIterations.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Gadugi", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(456, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 42);
            this.label1.TabIndex = 51;
            this.label1.Text = "ⓘ";
            this.toolTip1.SetToolTip(this.label1, "Total number of equations to generate");
            // 
            // numEquations
            // 
            this.numEquations.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.numEquations.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numEquations.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.numEquations.ForeColor = System.Drawing.Color.LightGray;
            this.numEquations.Location = new System.Drawing.Point(274, 44);
            this.numEquations.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numEquations.Name = "numEquations";
            this.numEquations.Size = new System.Drawing.Size(176, 22);
            this.numEquations.TabIndex = 50;
            this.numEquations.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            this.panel2.Location = new System.Drawing.Point(274, 68);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(176, 3);
            this.panel2.TabIndex = 49;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Gadugi", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.LightGray;
            this.label2.Location = new System.Drawing.Point(274, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 25);
            this.label2.TabIndex = 52;
            this.label2.Text = "Total Equations";
            // 
            // Generator
            // 
            this.Generator.WorkerReportsProgress = true;
            this.Generator.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Generator_DoWork);
            this.Generator.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Generator_ProgressChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 342);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(776, 10);
            this.progressBar1.TabIndex = 53;
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(800, 367);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numEquations);
            this.Controls.Add(this.panel2);
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
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Main_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Main_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.numIterations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEquations)).EndInit();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numEquations;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.ComponentModel.BackgroundWorker Generator;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

