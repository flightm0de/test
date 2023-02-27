namespace GleitzeitControlPanel.Forms
{
    partial class AddMa
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
            this.tb_name = new System.Windows.Forms.TextBox();
            this.tb_vorname = new System.Windows.Forms.TextBox();
            this.lbl_name = new System.Windows.Forms.Label();
            this.lbl_vorname = new System.Windows.Forms.Label();
            this.tb_wochenstd = new System.Windows.Forms.TextBox();
            this.tb_maxueberstd = new System.Windows.Forms.TextBox();
            this.tb_gleitzeitmax = new System.Windows.Forms.TextBox();
            this.lbl_wochenstd = new System.Windows.Forms.Label();
            this.lbl_maxueberstd = new System.Windows.Forms.Label();
            this.lbl_gleitzeitmax = new System.Windows.Forms.Label();
            this.btn_addMa = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb_name
            // 
            this.tb_name.Location = new System.Drawing.Point(162, 12);
            this.tb_name.Name = "tb_name";
            this.tb_name.Size = new System.Drawing.Size(100, 20);
            this.tb_name.TabIndex = 0;
            // 
            // tb_vorname
            // 
            this.tb_vorname.Location = new System.Drawing.Point(162, 38);
            this.tb_vorname.Name = "tb_vorname";
            this.tb_vorname.Size = new System.Drawing.Size(100, 20);
            this.tb_vorname.TabIndex = 1;
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Location = new System.Drawing.Point(10, 15);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(38, 13);
            this.lbl_name.TabIndex = 2;
            this.lbl_name.Text = "Name:";
            // 
            // lbl_vorname
            // 
            this.lbl_vorname.AutoSize = true;
            this.lbl_vorname.Location = new System.Drawing.Point(10, 41);
            this.lbl_vorname.Name = "lbl_vorname";
            this.lbl_vorname.Size = new System.Drawing.Size(52, 13);
            this.lbl_vorname.TabIndex = 3;
            this.lbl_vorname.Text = "Vorname:";
            // 
            // tb_wochenstd
            // 
            this.tb_wochenstd.Location = new System.Drawing.Point(162, 64);
            this.tb_wochenstd.Name = "tb_wochenstd";
            this.tb_wochenstd.Size = new System.Drawing.Size(100, 20);
            this.tb_wochenstd.TabIndex = 4;
            // 
            // tb_maxueberstd
            // 
            this.tb_maxueberstd.Location = new System.Drawing.Point(162, 90);
            this.tb_maxueberstd.Name = "tb_maxueberstd";
            this.tb_maxueberstd.Size = new System.Drawing.Size(100, 20);
            this.tb_maxueberstd.TabIndex = 5;
            // 
            // tb_gleitzeitmax
            // 
            this.tb_gleitzeitmax.Location = new System.Drawing.Point(162, 116);
            this.tb_gleitzeitmax.Name = "tb_gleitzeitmax";
            this.tb_gleitzeitmax.Size = new System.Drawing.Size(100, 20);
            this.tb_gleitzeitmax.TabIndex = 6;
            // 
            // lbl_wochenstd
            // 
            this.lbl_wochenstd.AutoSize = true;
            this.lbl_wochenstd.Location = new System.Drawing.Point(10, 67);
            this.lbl_wochenstd.Name = "lbl_wochenstd";
            this.lbl_wochenstd.Size = new System.Drawing.Size(89, 13);
            this.lbl_wochenstd.TabIndex = 7;
            this.lbl_wochenstd.Text = "Wochenstunden:";
            // 
            // lbl_maxueberstd
            // 
            this.lbl_maxueberstd.AutoSize = true;
            this.lbl_maxueberstd.Location = new System.Drawing.Point(10, 93);
            this.lbl_maxueberstd.Name = "lbl_maxueberstd";
            this.lbl_maxueberstd.Size = new System.Drawing.Size(139, 13);
            this.lbl_maxueberstd.TabIndex = 8;
            this.lbl_maxueberstd.Text = "max. Überstunden / Woche";
            // 
            // lbl_gleitzeitmax
            // 
            this.lbl_gleitzeitmax.AutoSize = true;
            this.lbl_gleitzeitmax.Location = new System.Drawing.Point(10, 119);
            this.lbl_gleitzeitmax.Name = "lbl_gleitzeitmax";
            this.lbl_gleitzeitmax.Size = new System.Drawing.Size(99, 13);
            this.lbl_gleitzeitmax.TabIndex = 9;
            this.lbl_gleitzeitmax.Text = "Gleitzeitkonto max.:";
            // 
            // btn_addMa
            // 
            this.btn_addMa.Location = new System.Drawing.Point(83, 180);
            this.btn_addMa.Name = "btn_addMa";
            this.btn_addMa.Size = new System.Drawing.Size(112, 38);
            this.btn_addMa.TabIndex = 10;
            this.btn_addMa.Text = "Mitarbeiter hinzufügen";
            this.btn_addMa.UseVisualStyleBackColor = true;
            this.btn_addMa.Click += new System.EventHandler(this.btn_addMa_Click);
            // 
            // AddMa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 230);
            this.Controls.Add(this.btn_addMa);
            this.Controls.Add(this.lbl_gleitzeitmax);
            this.Controls.Add(this.lbl_maxueberstd);
            this.Controls.Add(this.lbl_wochenstd);
            this.Controls.Add(this.tb_gleitzeitmax);
            this.Controls.Add(this.tb_maxueberstd);
            this.Controls.Add(this.tb_wochenstd);
            this.Controls.Add(this.lbl_vorname);
            this.Controls.Add(this.lbl_name);
            this.Controls.Add(this.tb_vorname);
            this.Controls.Add(this.tb_name);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddMa";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mitarbeiter hinzufügen";
            this.Load += new System.EventHandler(this.AddMa_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_name;
        private System.Windows.Forms.TextBox tb_vorname;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.Label lbl_vorname;
        private System.Windows.Forms.TextBox tb_wochenstd;
        private System.Windows.Forms.TextBox tb_maxueberstd;
        private System.Windows.Forms.TextBox tb_gleitzeitmax;
        private System.Windows.Forms.Label lbl_wochenstd;
        private System.Windows.Forms.Label lbl_maxueberstd;
        private System.Windows.Forms.Label lbl_gleitzeitmax;
        private System.Windows.Forms.Button btn_addMa;
    }
}