namespace NewParserForm
{
    partial class FirstForm
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
            this.btn_UseCase = new System.Windows.Forms.Button();
            this.btn_ClassDiagram = new System.Windows.Forms.Button();
            this.btn_Check = new System.Windows.Forms.Button();
            this.btn_FlowChart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_ChangeWord = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btn_SuggestWords = new System.Windows.Forms.Button();
            this.txt_btn_Domain = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_UseCase
            // 
            this.btn_UseCase.BackColor = System.Drawing.Color.LightGray;
            this.btn_UseCase.Location = new System.Drawing.Point(296, 217);
            this.btn_UseCase.Margin = new System.Windows.Forms.Padding(4);
            this.btn_UseCase.Name = "btn_UseCase";
            this.btn_UseCase.Size = new System.Drawing.Size(189, 60);
            this.btn_UseCase.TabIndex = 7;
            this.btn_UseCase.Text = "Use Case";
            this.btn_UseCase.UseVisualStyleBackColor = false;
            this.btn_UseCase.Click += new System.EventHandler(this.btn_UseCase_Click);
            // 
            // btn_ClassDiagram
            // 
            this.btn_ClassDiagram.BackColor = System.Drawing.Color.LightGray;
            this.btn_ClassDiagram.Location = new System.Drawing.Point(573, 217);
            this.btn_ClassDiagram.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_ClassDiagram.Name = "btn_ClassDiagram";
            this.btn_ClassDiagram.Size = new System.Drawing.Size(189, 60);
            this.btn_ClassDiagram.TabIndex = 7;
            this.btn_ClassDiagram.Text = "Class Diagram";
            this.btn_ClassDiagram.UseVisualStyleBackColor = false;
            this.btn_ClassDiagram.Click += new System.EventHandler(this.btn_ClassDiagram_Click);
            // 
            // btn_Check
            // 
            this.btn_Check.BackColor = System.Drawing.Color.LightGray;
            this.btn_Check.Location = new System.Drawing.Point(1401, 697);
            this.btn_Check.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Check.Name = "btn_Check";
            this.btn_Check.Size = new System.Drawing.Size(189, 60);
            this.btn_Check.TabIndex = 7;
            this.btn_Check.Text = "Check Variables Names";
            this.btn_Check.UseVisualStyleBackColor = false;
            this.btn_Check.Click += new System.EventHandler(this.btn_Check_Click);
            // 
            // btn_FlowChart
            // 
            this.btn_FlowChart.BackColor = System.Drawing.Color.LightGray;
            this.btn_FlowChart.Location = new System.Drawing.Point(24, 217);
            this.btn_FlowChart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_FlowChart.Name = "btn_FlowChart";
            this.btn_FlowChart.Size = new System.Drawing.Size(189, 60);
            this.btn_FlowChart.TabIndex = 7;
            this.btn_FlowChart.Text = "Flow Chart";
            this.btn_FlowChart.UseVisualStyleBackColor = false;
            this.btn_FlowChart.Click += new System.EventHandler(this.btn_FlowChart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(17, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(355, 29);
            this.label1.TabIndex = 10;
            this.label1.Text = "Please choose your operation ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 23F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label2.Location = new System.Drawing.Point(15, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(858, 44);
            this.label2.TabIndex = 11;
            this.label2.Text = "Welcome to Developer assistant for Clean Code";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.SeaShell;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(984, 95);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(972, 579);
            this.richTextBox1.TabIndex = 13;
            this.richTextBox1.Text = "using System;\nusing System.Linq;\nclass Test\n        {\n            public void Mai" +
    "n(string[] args)\n            {\n\n                          }\n}\n";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label4.Location = new System.Drawing.Point(1317, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(331, 39);
            this.label4.TabIndex = 14;
            this.label4.Text = "Write your code here";
            // 
            // btn_ChangeWord
            // 
            this.btn_ChangeWord.Location = new System.Drawing.Point(803, 547);
            this.btn_ChangeWord.Name = "btn_ChangeWord";
            this.btn_ChangeWord.Size = new System.Drawing.Size(123, 23);
            this.btn_ChangeWord.TabIndex = 24;
            this.btn_ChangeWord.Text = "Change Word";
            this.btn_ChangeWord.UseVisualStyleBackColor = true;
            this.btn_ChangeWord.Click += new System.EventHandler(this.btn_ChangeWord_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.SeaShell;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(573, 546);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(171, 24);
            this.comboBox1.TabIndex = 23;
            // 
            // btn_SuggestWords
            // 
            this.btn_SuggestWords.Location = new System.Drawing.Point(803, 466);
            this.btn_SuggestWords.Name = "btn_SuggestWords";
            this.btn_SuggestWords.Size = new System.Drawing.Size(123, 23);
            this.btn_SuggestWords.TabIndex = 22;
            this.btn_SuggestWords.Text = "SuggestWords";
            this.btn_SuggestWords.UseVisualStyleBackColor = true;
            this.btn_SuggestWords.Click += new System.EventHandler(this.btn_SuggestWords_Click);
            // 
            // txt_btn_Domain
            // 
            this.txt_btn_Domain.BackColor = System.Drawing.Color.SeaShell;
            this.txt_btn_Domain.Location = new System.Drawing.Point(573, 466);
            this.txt_btn_Domain.Name = "txt_btn_Domain";
            this.txt_btn_Domain.Size = new System.Drawing.Size(140, 22);
            this.txt_btn_Domain.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label5.Location = new System.Drawing.Point(302, 466);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(170, 17);
            this.label5.TabIndex = 27;
            this.label5.Text = "Please enter your domain";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label6.Location = new System.Drawing.Point(302, 550);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(210, 17);
            this.label6.TabIndex = 28;
            this.label6.Text = "Please select your needed word";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label3.Location = new System.Drawing.Point(19, 371);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(591, 17);
            this.label3.TabIndex = 29;
            this.label3.Text = "please note your variables should be meaningful or you will not be able to choose" +
    " any option";
            // 
            // FirstForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1924, 783);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_ChangeWord);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btn_SuggestWords);
            this.Controls.Add(this.txt_btn_Domain);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_UseCase);
            this.Controls.Add(this.btn_ClassDiagram);
            this.Controls.Add(this.btn_Check);
            this.Controls.Add(this.btn_FlowChart);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FirstForm";
            this.Text = "FirstForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FirstForm_FormClosed);
            this.Load += new System.EventHandler(this.FirstForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_UseCase;
        private System.Windows.Forms.Button btn_ClassDiagram;
        private System.Windows.Forms.Button btn_Check;
        private System.Windows.Forms.Button btn_FlowChart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_ChangeWord;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btn_SuggestWords;
        private System.Windows.Forms.TextBox txt_btn_Domain;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
    }
}