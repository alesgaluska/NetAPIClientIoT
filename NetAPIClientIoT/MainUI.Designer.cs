namespace NetAPIClientIoT
{
    partial class MainUI
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
            this.label1 = new System.Windows.Forms.Label();
            this.IPPortTx = new System.Windows.Forms.TextBox();
            this.APIKeyTx = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DataToSentTx = new System.Windows.Forms.TextBox();
            this.SentMessageBt = new System.Windows.Forms.Button();
            this.DataResponseTx = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:port";
            // 
            // IPPortTx
            // 
            this.IPPortTx.Location = new System.Drawing.Point(132, 30);
            this.IPPortTx.Name = "IPPortTx";
            this.IPPortTx.Size = new System.Drawing.Size(243, 20);
            this.IPPortTx.TabIndex = 1;
            // 
            // APIKeyTx
            // 
            this.APIKeyTx.Location = new System.Drawing.Point(132, 71);
            this.APIKeyTx.Name = "APIKeyTx";
            this.APIKeyTx.Size = new System.Drawing.Size(243, 20);
            this.APIKeyTx.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(83, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Api Key";
            // 
            // DataToSentTx
            // 
            this.DataToSentTx.Location = new System.Drawing.Point(132, 97);
            this.DataToSentTx.Multiline = true;
            this.DataToSentTx.Name = "DataToSentTx";
            this.DataToSentTx.Size = new System.Drawing.Size(670, 164);
            this.DataToSentTx.TabIndex = 4;
            // 
            // SentMessageBt
            // 
            this.SentMessageBt.Location = new System.Drawing.Point(700, 30);
            this.SentMessageBt.Name = "SentMessageBt";
            this.SentMessageBt.Size = new System.Drawing.Size(102, 61);
            this.SentMessageBt.TabIndex = 5;
            this.SentMessageBt.Text = "Sent Message";
            this.SentMessageBt.UseVisualStyleBackColor = true;
            this.SentMessageBt.Click += new System.EventHandler(this.SentMessageBt_Click);
            // 
            // DataResponseTx
            // 
            this.DataResponseTx.Location = new System.Drawing.Point(132, 267);
            this.DataResponseTx.Multiline = true;
            this.DataResponseTx.Name = "DataResponseTx";
            this.DataResponseTx.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DataResponseTx.Size = new System.Drawing.Size(670, 356);
            this.DataResponseTx.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "POST content";
            // 
            // MainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 635);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DataResponseTx);
            this.Controls.Add(this.SentMessageBt);
            this.Controls.Add(this.DataToSentTx);
            this.Controls.Add(this.APIKeyTx);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.IPPortTx);
            this.Controls.Add(this.label1);
            this.Name = "MainUI";
            this.Text = "IoT Client App ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainUI_FormClosing);
            this.Load += new System.EventHandler(this.MainUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox IPPortTx;
        private System.Windows.Forms.TextBox APIKeyTx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox DataToSentTx;
        private System.Windows.Forms.Button SentMessageBt;
        private System.Windows.Forms.TextBox DataResponseTx;
        private System.Windows.Forms.Label label3;
    }
}

