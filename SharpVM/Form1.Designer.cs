
namespace SharpVM
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.bt_Logs = new System.Windows.Forms.TextBox();
            this.tbHostName = new System.Windows.Forms.TextBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bt_Logs
            // 
            this.bt_Logs.Location = new System.Drawing.Point(12, 51);
            this.bt_Logs.Multiline = true;
            this.bt_Logs.Name = "bt_Logs";
            this.bt_Logs.Size = new System.Drawing.Size(823, 525);
            this.bt_Logs.TabIndex = 0;
            // 
            // tbHostName
            // 
            this.tbHostName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SharpVM.Properties.Settings.Default, "HostName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbHostName.Location = new System.Drawing.Point(66, 25);
            this.tbHostName.Name = "tbHostName";
            this.tbHostName.Size = new System.Drawing.Size(100, 20);
            this.tbHostName.TabIndex = 2;
            this.tbHostName.Text = global::SharpVM.Properties.Settings.Default.HostName;
            // 
            // tbPort
            // 
            this.tbPort.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SharpVM.Properties.Settings.Default, "Port", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbPort.Location = new System.Drawing.Point(238, 25);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(100, 20);
            this.tbPort.TabIndex = 3;
            this.tbPort.Text = global::SharpVM.Properties.Settings.Default.Port;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(344, 24);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 20);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Başlat";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Server IP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(172, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Server Port";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 588);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.tbHostName);
            this.Controls.Add(this.bt_Logs);
            this.Name = "Form1";
            this.Text = "SharpVM";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox bt_Logs;
        public System.Windows.Forms.TextBox tbHostName;
        public System.Windows.Forms.TextBox tbPort;
    }
}

