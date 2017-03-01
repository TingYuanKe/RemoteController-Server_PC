namespace RemoteController
{
    partial class RemoteController
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
            this.btn_startServer = new System.Windows.Forms.Button();
            this.lbl_serverIp = new System.Windows.Forms.Label();
            this.lbl_pos = new System.Windows.Forms.Label();
            this.btn_stopServer = new System.Windows.Forms.Button();
            this.grp_status = new System.Windows.Forms.GroupBox();
            this.lbl_serverStatus = new System.Windows.Forms.Label();
            this.lbl_clientStatus = new System.Windows.Forms.Label();
            this.grp_status.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_startServer
            // 
            this.btn_startServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btn_startServer.Location = new System.Drawing.Point(16, 14);
            this.btn_startServer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_startServer.Name = "btn_startServer";
            this.btn_startServer.Size = new System.Drawing.Size(149, 50);
            this.btn_startServer.TabIndex = 0;
            this.btn_startServer.Text = "開始";
            this.btn_startServer.UseVisualStyleBackColor = false;
            this.btn_startServer.Click += new System.EventHandler(this.btn_startServer_Click);
            // 
            // lbl_serverIp
            // 
            this.lbl_serverIp.Location = new System.Drawing.Point(16, 76);
            this.lbl_serverIp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_serverIp.Name = "lbl_serverIp";
            this.lbl_serverIp.Size = new System.Drawing.Size(199, 27);
            this.lbl_serverIp.ForeColor = System.Drawing.Color.Blue;
            this.lbl_serverIp.TabIndex = 11;
            this.lbl_serverIp.Text = "Server IP: xxx.xxx.xxx.xxx";
            this.lbl_serverIp.Click += new System.EventHandler(this.lbl_serverIp_Click);
            // 
            // lbl_pos
            // 
            this.lbl_pos.Location = new System.Drawing.Point(8, 85);
            this.lbl_pos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_pos.Name = "lbl_pos";
            this.lbl_pos.Size = new System.Drawing.Size(272, 27);
            this.lbl_pos.TabIndex = 12;
            this.lbl_pos.Text = "Mouse Pos: ";
            // 
            // btn_stopServer
            // 
            this.btn_stopServer.BackColor = System.Drawing.Color.Red;

            this.btn_stopServer.Enabled = true;
            this.btn_stopServer.Location = new System.Drawing.Point(173, 14);
            this.btn_stopServer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_stopServer.Name = "btn_stopServer";
            this.btn_stopServer.Size = new System.Drawing.Size(145, 50);
            this.btn_stopServer.TabIndex = 13;
            this.btn_stopServer.Text = "停止";
            this.btn_stopServer.UseVisualStyleBackColor = false;
            this.btn_stopServer.Click += new System.EventHandler(this.btn_stopServer_Click);
            // 
            // grp_status
            // 
            this.grp_status.Controls.Add(this.lbl_serverStatus);
            this.grp_status.Controls.Add(this.lbl_clientStatus);
            this.grp_status.Controls.Add(this.lbl_pos);
            this.grp_status.Location = new System.Drawing.Point(20, 106);
            this.grp_status.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grp_status.Name = "grp_status";
            this.grp_status.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grp_status.Size = new System.Drawing.Size(292, 133);
            this.grp_status.TabIndex = 14;
            this.grp_status.TabStop = false;
            this.grp_status.Text = "狀態";
            this.grp_status.Enter += new System.EventHandler(this.grp_status_Enter);
            // 
            // lbl_serverStatus
            // 
            this.lbl_serverStatus.AutoSize = true;
            this.lbl_serverStatus.Location = new System.Drawing.Point(8, 18);
            this.lbl_serverStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_serverStatus.Name = "lbl_serverStatus";
            this.lbl_serverStatus.Size = new System.Drawing.Size(114, 15);
            this.lbl_serverStatus.TabIndex = 1;
            this.lbl_serverStatus.Text = "伺服器狀態:  Off";
            this.lbl_serverStatus.Click += new System.EventHandler(this.lbl_serverStatus_Click);
            // 
            // lbl_clientStatus
            // 
            this.lbl_clientStatus.AutoSize = true;
            this.lbl_clientStatus.Location = new System.Drawing.Point(8, 48);
            this.lbl_clientStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_clientStatus.Name = "lbl_clientStatus";
            this.lbl_clientStatus.Size = new System.Drawing.Size(141, 15);
            this.lbl_clientStatus.TabIndex = 0;
            this.lbl_clientStatus.Text = "客戶端連線: False";
            this.lbl_clientStatus.Click += new System.EventHandler(this.lbl_clientStatus_Click);
 
            // 
            // WifiMouserServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 250);         
            this.Controls.Add(this.grp_status);
            this.Controls.Add(this.btn_stopServer);
            this.Controls.Add(this.lbl_serverIp);
            this.Controls.Add(this.btn_startServer);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "RemoteController";
            this.Text = "RemoteController";
            this.Load += new System.EventHandler(this.WifiMouserServer_Load);
            this.grp_status.ResumeLayout(false);
            this.grp_status.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_startServer;
        private System.Windows.Forms.Label lbl_serverIp;
        private System.Windows.Forms.Button btn_stopServer;
        private System.Windows.Forms.GroupBox grp_status;
        private System.Windows.Forms.Label lbl_pos;
        private System.Windows.Forms.Label lbl_clientStatus;
        private System.Windows.Forms.Label lbl_serverStatus;

    }
}

