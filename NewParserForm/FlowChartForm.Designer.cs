namespace NewParserForm
{
    partial class FlowChartForm
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
            this.Parse = new System.Windows.Forms.Button();
            this.TxtBox_InputCode = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Parse
            // 
            this.Parse.BackColor = System.Drawing.Color.LightGray;
            this.Parse.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Parse.Location = new System.Drawing.Point(15, 395);
            this.Parse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Parse.Name = "Parse";
            this.Parse.Size = new System.Drawing.Size(145, 49);
            this.Parse.TabIndex = 0;
            this.Parse.Text = "FlowChart";
            this.Parse.UseVisualStyleBackColor = false;
            this.Parse.Click += new System.EventHandler(this.Parse_Click);
            // 
            // TxtBox_InputCode
            // 
            this.TxtBox_InputCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBox_InputCode.Location = new System.Drawing.Point(12, 7);
            this.TxtBox_InputCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TxtBox_InputCode.Name = "TxtBox_InputCode";
            this.TxtBox_InputCode.Size = new System.Drawing.Size(543, 382);
            this.TxtBox_InputCode.TabIndex = 1;
            this.TxtBox_InputCode.TabStop = false;
            this.TxtBox_InputCode.Text = "using System;\nusing System.Linq;\nclass Test\n        {\n            public void Mai" +
    "n(string[] args)\n            {\n\n                          }\n}";
            this.TxtBox_InputCode.TextChanged += new System.EventHandler(this.TxtBox_InputCode_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.SeaShell;
            this.panel1.ForeColor = System.Drawing.Color.SeaShell;
            this.panel1.Location = new System.Drawing.Point(580, 7);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1356, 761);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightGray;
            this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button1.Location = new System.Drawing.Point(12, 449);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 49);
            this.button1.TabIndex = 3;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FlowChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(100, 100);
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1924, 783);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TxtBox_InputCode);
            this.Controls.Add(this.Parse);
            this.ForeColor = System.Drawing.Color.SeaShell;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FlowChartForm";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FlowChartForm_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.LocationChanged += new System.EventHandler(this.f);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Parse;
        private System.Windows.Forms.RichTextBox TxtBox_InputCode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
    }
}

