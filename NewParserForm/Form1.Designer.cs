namespace NewParserForm
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Parse = new System.Windows.Forms.Button();
            this.TxtBox_InputCode = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checker = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.UseCaseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Parse
            // 
            this.Parse.Location = new System.Drawing.Point(15, 395);
            this.Parse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Parse.Name = "Parse";
            this.Parse.Size = new System.Drawing.Size(86, 23);
            this.Parse.TabIndex = 0;
            this.Parse.Text = "FlowChart";
            this.Parse.UseVisualStyleBackColor = true;
            this.Parse.Click += new System.EventHandler(this.Parse_Click);
            // 
            // TxtBox_InputCode
            // 
            this.TxtBox_InputCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBox_InputCode.Location = new System.Drawing.Point(12, 7);
            this.TxtBox_InputCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TxtBox_InputCode.Name = "TxtBox_InputCode";
            this.TxtBox_InputCode.Size = new System.Drawing.Size(543, 382);
            this.TxtBox_InputCode.TabIndex = 1;
            this.TxtBox_InputCode.TabStop = false;
            this.TxtBox_InputCode.Text = resources.GetString("TxtBox_InputCode.Text");
            this.TxtBox_InputCode.TextChanged += new System.EventHandler(this.TxtBox_InputCode_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Location = new System.Drawing.Point(580, 7);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(671, 506);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // checker
            // 
            this.checker.Location = new System.Drawing.Point(122, 395);
            this.checker.Margin = new System.Windows.Forms.Padding(4);
            this.checker.Name = "checker";
            this.checker.Size = new System.Drawing.Size(100, 28);
            this.checker.TabIndex = 3;
            this.checker.Text = "check";
            this.checker.UseVisualStyleBackColor = true;
            this.checker.Click += new System.EventHandler(this.checker_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(396, 400);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "classdiagram";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // UseCaseButton
            // 
            this.UseCaseButton.Location = new System.Drawing.Point(260, 395);
            this.UseCaseButton.Margin = new System.Windows.Forms.Padding(4);
            this.UseCaseButton.Name = "UseCaseButton";
            this.UseCaseButton.Size = new System.Drawing.Size(100, 28);
            this.UseCaseButton.TabIndex = 5;
            this.UseCaseButton.Text = "UseCase";
            this.UseCaseButton.UseVisualStyleBackColor = true;
            this.UseCaseButton.Click += new System.EventHandler(this.UseCaseButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(100, 100);
            this.ClientSize = new System.Drawing.Size(1288, 528);
            this.Controls.Add(this.UseCaseButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checker);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TxtBox_InputCode);
            this.Controls.Add(this.Parse);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Parse;
        private System.Windows.Forms.RichTextBox TxtBox_InputCode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button checker;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button UseCaseButton;
    }
}

