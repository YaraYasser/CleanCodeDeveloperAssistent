namespace NewParserForm
{
    partial class UseCaeForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_UseCase = new System.Windows.Forms.Button();
            this.btn_BackIcon = new System.Windows.Forms.Button();
            this.richTextBoxUseCase = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SeaShell;
            this.panel1.ForeColor = System.Drawing.Color.SeaShell;
            this.panel1.Location = new System.Drawing.Point(580, 7);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2397, 2721);
            this.panel1.TabIndex = 3;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint_1);
            // 
            // btn_UseCase
            // 
            this.btn_UseCase.BackColor = System.Drawing.Color.LightGray;
            this.btn_UseCase.Location = new System.Drawing.Point(16, 396);
            this.btn_UseCase.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_UseCase.Name = "btn_UseCase";
            this.btn_UseCase.Size = new System.Drawing.Size(145, 49);
            this.btn_UseCase.TabIndex = 5;
            this.btn_UseCase.Text = "Use Case";
            this.btn_UseCase.UseVisualStyleBackColor = false;
            this.btn_UseCase.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_BackIcon
            // 
            this.btn_BackIcon.BackColor = System.Drawing.Color.LightGray;
            this.btn_BackIcon.Location = new System.Drawing.Point(16, 453);
            this.btn_BackIcon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_BackIcon.Name = "btn_BackIcon";
            this.btn_BackIcon.Size = new System.Drawing.Size(145, 49);
            this.btn_BackIcon.TabIndex = 6;
            this.btn_BackIcon.Text = "Back";
            this.btn_BackIcon.UseVisualStyleBackColor = false;
            this.btn_BackIcon.Click += new System.EventHandler(this.btn_BackIcon_Click);
            // 
            // richTextBoxUseCase
            // 
            this.richTextBoxUseCase.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxUseCase.Location = new System.Drawing.Point(12, 7);
            this.richTextBoxUseCase.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBoxUseCase.Name = "richTextBoxUseCase";
            this.richTextBoxUseCase.Size = new System.Drawing.Size(543, 382);
            this.richTextBoxUseCase.TabIndex = 7;
            this.richTextBoxUseCase.TabStop = false;
            this.richTextBoxUseCase.Text = "using System;\nusing System.Linq;\nclass Test\n        {\n            public void Mai" +
    "n(string[] args)\n            {\n\n                          }\n}";
            // 
            // UseCaeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1924, 783);
            this.Controls.Add(this.richTextBoxUseCase);
            this.Controls.Add(this.btn_BackIcon);
            this.Controls.Add(this.btn_UseCase);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "UseCaeForm";
            this.Text = "Form2";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UseCaeForm_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_UseCase;
        private System.Windows.Forms.Button btn_BackIcon;
        private System.Windows.Forms.RichTextBox richTextBoxUseCase;
    }
}