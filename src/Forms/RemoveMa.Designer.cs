namespace GleitzeitControlPanel.Forms
{
    partial class RemoveMa
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
            this.dgv_uebersicht = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vorname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aktive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_uebersicht)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_uebersicht
            // 
            this.dgv_uebersicht.AllowUserToAddRows = false;
            this.dgv_uebersicht.AllowUserToDeleteRows = false;
            this.dgv_uebersicht.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.vorname,
            this.aktive,
            this.id});
            this.dgv_uebersicht.Location = new System.Drawing.Point(13, 13);
            this.dgv_uebersicht.MultiSelect = false;
            this.dgv_uebersicht.Name = "dgv_uebersicht";
            this.dgv_uebersicht.Size = new System.Drawing.Size(364, 324);
            this.dgv_uebersicht.TabIndex = 0;
            this.dgv_uebersicht.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_uebersicht_CellContentClick);
            // 
            // name
            // 
            this.name.HeaderText = "Name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 130;
            // 
            // vorname
            // 
            this.vorname.HeaderText = "Vorname";
            this.vorname.Name = "vorname";
            this.vorname.ReadOnly = true;
            this.vorname.Width = 130;
            // 
            // aktive
            // 
            this.aktive.HeaderText = "Aktiv";
            this.aktive.Name = "aktive";
            this.aktive.Width = 44;
            // 
            // id
            // 
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // RemoveMa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 349);
            this.Controls.Add(this.dgv_uebersicht);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RemoveMa";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mitarbeiter deaktivieren";
            this.Load += new System.EventHandler(this.RemoveMa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_uebersicht)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_uebersicht;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn vorname;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aktive;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
    }
}