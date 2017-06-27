namespace UnpackZipClient
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.lstClientInfo = new System.Windows.Forms.ListBox();
            this.btnSendFile = new System.Windows.Forms.Button();
            this.btnUnpack = new System.Windows.Forms.Button();
            this.lstFilesInZip = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(12, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(260, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lstClientInfo
            // 
            this.lstClientInfo.FormattingEnabled = true;
            this.lstClientInfo.Location = new System.Drawing.Point(12, 41);
            this.lstClientInfo.Name = "lstClientInfo";
            this.lstClientInfo.Size = new System.Drawing.Size(260, 368);
            this.lstClientInfo.TabIndex = 1;
            // 
            // btnSendFile
            // 
            this.btnSendFile.Location = new System.Drawing.Point(278, 12);
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.Size = new System.Drawing.Size(482, 23);
            this.btnSendFile.TabIndex = 2;
            this.btnSendFile.Text = "Send File";
            this.btnSendFile.UseVisualStyleBackColor = true;
            this.btnSendFile.Click += new System.EventHandler(this.btnSendFile_Click);
            // 
            // btnUnpack
            // 
            this.btnUnpack.Location = new System.Drawing.Point(278, 41);
            this.btnUnpack.Name = "btnUnpack";
            this.btnUnpack.Size = new System.Drawing.Size(482, 23);
            this.btnUnpack.TabIndex = 3;
            this.btnUnpack.Text = "Unpack Files";
            this.btnUnpack.UseVisualStyleBackColor = true;
            this.btnUnpack.Click += new System.EventHandler(this.btnUnpack_Click);
            // 
            // lstFilesInZip
            // 
            this.lstFilesInZip.FormattingEnabled = true;
            this.lstFilesInZip.Location = new System.Drawing.Point(278, 70);
            this.lstFilesInZip.Name = "lstFilesInZip";
            this.lstFilesInZip.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstFilesInZip.Size = new System.Drawing.Size(482, 199);
            this.lstFilesInZip.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 421);
            this.Controls.Add(this.lstFilesInZip);
            this.Controls.Add(this.btnUnpack);
            this.Controls.Add(this.btnSendFile);
            this.Controls.Add(this.lstClientInfo);
            this.Controls.Add(this.btnConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ListBox lstClientInfo;
        private System.Windows.Forms.Button btnSendFile;
        private System.Windows.Forms.Button btnUnpack;
        private System.Windows.Forms.ListBox lstFilesInZip;
    }
}

