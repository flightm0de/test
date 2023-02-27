namespace GleitzeitControlPanel
{
    partial class Main
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.dgv_settings = new System.Windows.Forms.DataGridView();
            this.wochenstd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxwoche = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxgleitzeit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_setting = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_useruebersicht = new System.Windows.Forms.DataGridView();
            this.kw = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zeit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jahr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_user = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_gleitzeituebersicht = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vorname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gleitzeit_ges = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.real_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_gesuebersicht = new System.Windows.Forms.DataGridView();
            this.kw_uebersicht = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jahr_uebersicht = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ueberstunden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name_uebersicht = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vorname_uebersicht = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kw_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kw_vor = new System.Windows.Forms.Button();
            this.kw_zurueck = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.jahr_vor = new System.Windows.Forms.Button();
            this.jahr_zurueck = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.currKW_input = new System.Windows.Forms.MaskedTextBox();
            this.currJahr_input = new System.Windows.Forms.MaskedTextBox();
            this.lbl_settings = new System.Windows.Forms.Label();
            this.lbl_gleitzeituebersicht = new System.Windows.Forms.Label();
            this.lbl_useruebersicht = new System.Windows.Forms.Label();
            this.lbl_gesuebersicht = new System.Windows.Forms.Label();
            this.ts_menubar = new System.Windows.Forms.ToolStrip();
            this.get_data_from_excel = new System.Windows.Forms.ToolStripButton();
            this.btn_add_ma = new System.Windows.Forms.ToolStripButton();
            this.btn_del_ma = new System.Windows.Forms.ToolStripButton();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_settings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_useruebersicht)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_gleitzeituebersicht)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_gesuebersicht)).BeginInit();
            this.ts_menubar.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_settings
            // 
            this.dgv_settings.AllowUserToAddRows = false;
            this.dgv_settings.AllowUserToDeleteRows = false;
            this.dgv_settings.ColumnHeadersHeight = 46;
            this.dgv_settings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_settings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.wochenstd,
            this.maxwoche,
            this.maxgleitzeit,
            this.id_setting});
            this.dgv_settings.Location = new System.Drawing.Point(6, 57);
            this.dgv_settings.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dgv_settings.Name = "dgv_settings";
            this.dgv_settings.RowHeadersVisible = false;
            this.dgv_settings.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            this.dgv_settings.RowTemplate.Height = 33;
            this.dgv_settings.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgv_settings.Size = new System.Drawing.Size(330, 78);
            this.dgv_settings.TabIndex = 0;
            this.dgv_settings.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_settings_CellContentClick);
            // 
            // wochenstd
            // 
            this.wochenstd.HeaderText = "Wochenstunden";
            this.wochenstd.MinimumWidth = 10;
            this.wochenstd.Name = "wochenstd";
            this.wochenstd.Width = 110;
            // 
            // maxwoche
            // 
            this.maxwoche.HeaderText = "max. Überstunden / Woche";
            this.maxwoche.MinimumWidth = 10;
            this.maxwoche.Name = "maxwoche";
            this.maxwoche.Width = 110;
            // 
            // maxgleitzeit
            // 
            this.maxgleitzeit.HeaderText = "Gleitzeitkonto max.:";
            this.maxgleitzeit.MinimumWidth = 10;
            this.maxgleitzeit.Name = "maxgleitzeit";
            this.maxgleitzeit.Width = 110;
            // 
            // id_setting
            // 
            this.id_setting.HeaderText = "id";
            this.id_setting.MinimumWidth = 10;
            this.id_setting.Name = "id_setting";
            this.id_setting.ReadOnly = true;
            this.id_setting.Visible = false;
            this.id_setting.Width = 200;
            // 
            // dgv_useruebersicht
            // 
            this.dgv_useruebersicht.AllowUserToAddRows = false;
            this.dgv_useruebersicht.AllowUserToDeleteRows = false;
            this.dgv_useruebersicht.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_useruebersicht.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.kw,
            this.zeit,
            this.jahr,
            this.id_user});
            this.dgv_useruebersicht.Location = new System.Drawing.Point(6, 171);
            this.dgv_useruebersicht.Name = "dgv_useruebersicht";
            this.dgv_useruebersicht.RowHeadersWidth = 82;
            this.dgv_useruebersicht.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_useruebersicht.Size = new System.Drawing.Size(330, 150);
            this.dgv_useruebersicht.TabIndex = 1;
            this.dgv_useruebersicht.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_useruebersicht_CellContentClick);
            // 
            // kw
            // 
            this.kw.HeaderText = "Kalenderwoche";
            this.kw.MinimumWidth = 10;
            this.kw.Name = "kw";
            this.kw.ReadOnly = true;
            this.kw.Width = 75;
            // 
            // zeit
            // 
            this.zeit.HeaderText = "Überstunden (in h)";
            this.zeit.MinimumWidth = 10;
            this.zeit.Name = "zeit";
            this.zeit.Width = 75;
            // 
            // jahr
            // 
            this.jahr.HeaderText = "Jahr";
            this.jahr.MinimumWidth = 10;
            this.jahr.Name = "jahr";
            this.jahr.ReadOnly = true;
            this.jahr.Width = 79;
            // 
            // id_user
            // 
            this.id_user.HeaderText = "id";
            this.id_user.MinimumWidth = 10;
            this.id_user.Name = "id_user";
            this.id_user.ReadOnly = true;
            this.id_user.Visible = false;
            this.id_user.Width = 200;
            // 
            // dgv_gleitzeituebersicht
            // 
            this.dgv_gleitzeituebersicht.AllowUserToAddRows = false;
            this.dgv_gleitzeituebersicht.AllowUserToDeleteRows = false;
            this.dgv_gleitzeituebersicht.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_gleitzeituebersicht.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.vorname,
            this.gleitzeit_ges,
            this.id,
            this.real_name});
            this.dgv_gleitzeituebersicht.Location = new System.Drawing.Point(341, 57);
            this.dgv_gleitzeituebersicht.Name = "dgv_gleitzeituebersicht";
            this.dgv_gleitzeituebersicht.RowHeadersVisible = false;
            this.dgv_gleitzeituebersicht.RowHeadersWidth = 82;
            this.dgv_gleitzeituebersicht.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_gleitzeituebersicht.Size = new System.Drawing.Size(338, 264);
            this.dgv_gleitzeituebersicht.TabIndex = 2;
            this.dgv_gleitzeituebersicht.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_gleitzeituebersicht_CellContentClick);
            // 
            // name
            // 
            this.name.HeaderText = "Name";
            this.name.MinimumWidth = 10;
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 109;
            // 
            // vorname
            // 
            this.vorname.HeaderText = "Vorname";
            this.vorname.MinimumWidth = 10;
            this.vorname.Name = "vorname";
            this.vorname.ReadOnly = true;
            this.vorname.Width = 109;
            // 
            // gleitzeit_ges
            // 
            this.gleitzeit_ges.HeaderText = "Gesamte Gleitzeit";
            this.gleitzeit_ges.MinimumWidth = 10;
            this.gleitzeit_ges.Name = "gleitzeit_ges";
            this.gleitzeit_ges.ReadOnly = true;
            // 
            // id
            // 
            this.id.HeaderText = "ID";
            this.id.MinimumWidth = 10;
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            this.id.Width = 200;
            // 
            // real_name
            // 
            this.real_name.HeaderText = "real_name";
            this.real_name.Name = "real_name";
            this.real_name.ReadOnly = true;
            this.real_name.Visible = false;
            // 
            // dgv_gesuebersicht
            // 
            this.dgv_gesuebersicht.AllowUserToAddRows = false;
            this.dgv_gesuebersicht.AllowUserToDeleteRows = false;
            this.dgv_gesuebersicht.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_gesuebersicht.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.kw_uebersicht,
            this.jahr_uebersicht,
            this.ueberstunden,
            this.name_uebersicht,
            this.vorname_uebersicht,
            this.kw_id});
            this.dgv_gesuebersicht.Location = new System.Drawing.Point(4, 403);
            this.dgv_gesuebersicht.Name = "dgv_gesuebersicht";
            this.dgv_gesuebersicht.RowHeadersWidth = 82;
            this.dgv_gesuebersicht.Size = new System.Drawing.Size(670, 453);
            this.dgv_gesuebersicht.TabIndex = 3;
            // 
            // kw_uebersicht
            // 
            this.kw_uebersicht.HeaderText = "Kalenderwoche";
            this.kw_uebersicht.MinimumWidth = 10;
            this.kw_uebersicht.Name = "kw_uebersicht";
            this.kw_uebersicht.ReadOnly = true;
            // 
            // jahr_uebersicht
            // 
            this.jahr_uebersicht.HeaderText = "Jahr";
            this.jahr_uebersicht.MinimumWidth = 10;
            this.jahr_uebersicht.Name = "jahr_uebersicht";
            this.jahr_uebersicht.ReadOnly = true;
            // 
            // ueberstunden
            // 
            this.ueberstunden.HeaderText = "Überstunden (in h)";
            this.ueberstunden.MinimumWidth = 10;
            this.ueberstunden.Name = "ueberstunden";
            this.ueberstunden.Width = 129;
            // 
            // name_uebersicht
            // 
            this.name_uebersicht.HeaderText = "Name";
            this.name_uebersicht.MinimumWidth = 10;
            this.name_uebersicht.Name = "name_uebersicht";
            this.name_uebersicht.ReadOnly = true;
            this.name_uebersicht.Width = 120;
            // 
            // vorname_uebersicht
            // 
            this.vorname_uebersicht.HeaderText = "Vorname";
            this.vorname_uebersicht.MinimumWidth = 10;
            this.vorname_uebersicht.Name = "vorname_uebersicht";
            this.vorname_uebersicht.ReadOnly = true;
            this.vorname_uebersicht.Width = 120;
            // 
            // kw_id
            // 
            this.kw_id.HeaderText = "ID";
            this.kw_id.MinimumWidth = 10;
            this.kw_id.Name = "kw_id";
            this.kw_id.ReadOnly = true;
            this.kw_id.Visible = false;
            this.kw_id.Width = 10;
            // 
            // kw_vor
            // 
            this.kw_vor.Location = new System.Drawing.Point(125, 374);
            this.kw_vor.Name = "kw_vor";
            this.kw_vor.Size = new System.Drawing.Size(75, 23);
            this.kw_vor.TabIndex = 6;
            this.kw_vor.Text = ">";
            this.kw_vor.UseVisualStyleBackColor = true;
            this.kw_vor.Click += new System.EventHandler(this.kw_vor_Click);
            // 
            // kw_zurueck
            // 
            this.kw_zurueck.Location = new System.Drawing.Point(6, 374);
            this.kw_zurueck.Name = "kw_zurueck";
            this.kw_zurueck.Size = new System.Drawing.Size(75, 23);
            this.kw_zurueck.TabIndex = 5;
            this.kw_zurueck.Text = "<";
            this.kw_zurueck.UseVisualStyleBackColor = true;
            this.kw_zurueck.Click += new System.EventHandler(this.kw_zurueck_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 359);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Kalenderwoche Blättern";
            // 
            // jahr_vor
            // 
            this.jahr_vor.Location = new System.Drawing.Point(345, 374);
            this.jahr_vor.Name = "jahr_vor";
            this.jahr_vor.Size = new System.Drawing.Size(75, 23);
            this.jahr_vor.TabIndex = 8;
            this.jahr_vor.Text = ">";
            this.jahr_vor.UseVisualStyleBackColor = true;
            this.jahr_vor.Visible = false;
            this.jahr_vor.Click += new System.EventHandler(this.jahr_vor_Click);
            // 
            // jahr_zurueck
            // 
            this.jahr_zurueck.Location = new System.Drawing.Point(227, 374);
            this.jahr_zurueck.Name = "jahr_zurueck";
            this.jahr_zurueck.Size = new System.Drawing.Size(75, 23);
            this.jahr_zurueck.TabIndex = 7;
            this.jahr_zurueck.Text = "<";
            this.jahr_zurueck.UseVisualStyleBackColor = true;
            this.jahr_zurueck.Visible = false;
            this.jahr_zurueck.Click += new System.EventHandler(this.jahr_zurueck_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(289, 359);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Jahr Blättern";
            this.label2.Visible = false;
            // 
            // currKW_input
            // 
            this.currKW_input.Location = new System.Drawing.Point(87, 375);
            this.currKW_input.Name = "currKW_input";
            this.currKW_input.Size = new System.Drawing.Size(32, 20);
            this.currKW_input.TabIndex = 11;
            this.currKW_input.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // currJahr_input
            // 
            this.currJahr_input.Location = new System.Drawing.Point(308, 375);
            this.currJahr_input.Name = "currJahr_input";
            this.currJahr_input.Size = new System.Drawing.Size(32, 20);
            this.currJahr_input.TabIndex = 12;
            this.currJahr_input.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.currJahr_input.Visible = false;
            // 
            // lbl_settings
            // 
            this.lbl_settings.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_settings.Location = new System.Drawing.Point(4, 33);
            this.lbl_settings.Name = "lbl_settings";
            this.lbl_settings.Size = new System.Drawing.Size(196, 23);
            this.lbl_settings.TabIndex = 13;
            this.lbl_settings.Text = "Gleitzeit Einstellungen:";
            // 
            // lbl_gleitzeituebersicht
            // 
            this.lbl_gleitzeituebersicht.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_gleitzeituebersicht.Location = new System.Drawing.Point(338, 33);
            this.lbl_gleitzeituebersicht.Name = "lbl_gleitzeituebersicht";
            this.lbl_gleitzeituebersicht.Size = new System.Drawing.Size(224, 23);
            this.lbl_gleitzeituebersicht.TabIndex = 14;
            this.lbl_gleitzeituebersicht.Text = "Gesamt-Gleitzeit Übersicht:";
            // 
            // lbl_useruebersicht
            // 
            this.lbl_useruebersicht.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_useruebersicht.Location = new System.Drawing.Point(4, 145);
            this.lbl_useruebersicht.Name = "lbl_useruebersicht";
            this.lbl_useruebersicht.Size = new System.Drawing.Size(331, 23);
            this.lbl_useruebersicht.TabIndex = 15;
            this.lbl_useruebersicht.Text = "Benutzerübersicht:";
            // 
            // lbl_gesuebersicht
            // 
            this.lbl_gesuebersicht.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_gesuebersicht.Location = new System.Drawing.Point(4, 329);
            this.lbl_gesuebersicht.Name = "lbl_gesuebersicht";
            this.lbl_gesuebersicht.Size = new System.Drawing.Size(336, 23);
            this.lbl_gesuebersicht.TabIndex = 16;
            this.lbl_gesuebersicht.Text = "Gesamtübersicht aller Benutzer:";
            // 
            // ts_menubar
            // 
            this.ts_menubar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ts_menubar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.get_data_from_excel,
            this.btn_add_ma,
            this.btn_del_ma});
            this.ts_menubar.Location = new System.Drawing.Point(0, 0);
            this.ts_menubar.Name = "ts_menubar";
            this.ts_menubar.Size = new System.Drawing.Size(684, 31);
            this.ts_menubar.TabIndex = 17;
            this.ts_menubar.Text = "menubar";
            this.ts_menubar.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_menubar_ItemClicked);
            // 
            // get_data_from_excel
            // 
            this.get_data_from_excel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.get_data_from_excel.Image = ((System.Drawing.Image)(resources.GetObject("get_data_from_excel.Image")));
            this.get_data_from_excel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.get_data_from_excel.Name = "get_data_from_excel";
            this.get_data_from_excel.Size = new System.Drawing.Size(28, 28);
            this.get_data_from_excel.Text = "btn_import_data_from_excel";
            this.get_data_from_excel.ToolTipText = "Daten aus Excel übernehmen";
            this.get_data_from_excel.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // btn_add_ma
            // 
            this.btn_add_ma.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_add_ma.Image = ((System.Drawing.Image)(resources.GetObject("btn_add_ma.Image")));
            this.btn_add_ma.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_add_ma.Name = "btn_add_ma";
            this.btn_add_ma.Size = new System.Drawing.Size(28, 28);
            this.btn_add_ma.Text = "add_ma";
            this.btn_add_ma.ToolTipText = "Mitarbeiter hinzufügen";
            this.btn_add_ma.Click += new System.EventHandler(this.btn_add_ma_Click);
            // 
            // btn_del_ma
            // 
            this.btn_del_ma.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_del_ma.Image = ((System.Drawing.Image)(resources.GetObject("btn_del_ma.Image")));
            this.btn_del_ma.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_del_ma.Name = "btn_del_ma";
            this.btn_del_ma.Size = new System.Drawing.Size(28, 28);
            this.btn_del_ma.Text = "Mitarbeiter deaktivieren";
            this.btn_del_ma.ToolTipText = "Mitarbeiter deaktivieren";
            this.btn_del_ma.Click += new System.EventHandler(this.btn_del_ma_Click);
            // 
            // pgBar
            // 
            this.pgBar.Location = new System.Drawing.Point(446, 7);
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(228, 19);
            this.pgBar.TabIndex = 18;
            // 
            // Main
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 836);
            this.Controls.Add(this.pgBar);
            this.Controls.Add(this.ts_menubar);
            this.Controls.Add(this.lbl_gesuebersicht);
            this.Controls.Add(this.lbl_useruebersicht);
            this.Controls.Add(this.lbl_gleitzeituebersicht);
            this.Controls.Add(this.lbl_settings);
            this.Controls.Add(this.currJahr_input);
            this.Controls.Add(this.currKW_input);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.jahr_zurueck);
            this.Controls.Add(this.jahr_vor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.kw_zurueck);
            this.Controls.Add(this.kw_vor);
            this.Controls.Add(this.dgv_gesuebersicht);
            this.Controls.Add(this.dgv_gleitzeituebersicht);
            this.Controls.Add(this.dgv_useruebersicht);
            this.Controls.Add(this.dgv_settings);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(700, 875);
            this.MinimumSize = new System.Drawing.Size(700, 875);
            this.Name = "Main";
            this.Text = "Gleitzeit Control-Panel";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_settings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_useruebersicht)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_gleitzeituebersicht)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_gesuebersicht)).EndInit();
            this.ts_menubar.ResumeLayout(false);
            this.ts_menubar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_settings;
        private System.Windows.Forms.DataGridView dgv_useruebersicht;
        private System.Windows.Forms.DataGridView dgv_gleitzeituebersicht;
        private System.Windows.Forms.DataGridView dgv_gesuebersicht;
        private System.Windows.Forms.Button kw_vor;
        private System.Windows.Forms.Button kw_zurueck;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn wochenstd;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxwoche;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxgleitzeit;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_setting;
        private System.Windows.Forms.Button jahr_vor;
        private System.Windows.Forms.Button jahr_zurueck;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox currKW_input;
        private System.Windows.Forms.MaskedTextBox currJahr_input;
        private System.Windows.Forms.Label lbl_settings;
        private System.Windows.Forms.Label lbl_gleitzeituebersicht;
        private System.Windows.Forms.Label lbl_useruebersicht;
        private System.Windows.Forms.Label lbl_gesuebersicht;
        private System.Windows.Forms.DataGridViewTextBoxColumn kw_uebersicht;
        private System.Windows.Forms.DataGridViewTextBoxColumn jahr_uebersicht;
        private System.Windows.Forms.DataGridViewTextBoxColumn ueberstunden;
        private System.Windows.Forms.DataGridViewTextBoxColumn name_uebersicht;
        private System.Windows.Forms.DataGridViewTextBoxColumn vorname_uebersicht;
        private System.Windows.Forms.DataGridViewTextBoxColumn kw_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn kw;
        private System.Windows.Forms.DataGridViewTextBoxColumn zeit;
        private System.Windows.Forms.DataGridViewTextBoxColumn jahr;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_user;
        private System.Windows.Forms.ToolStrip ts_menubar;
        private System.Windows.Forms.ToolStripButton btn_add_ma;
        private System.Windows.Forms.ToolStripButton btn_del_ma;
        private System.Windows.Forms.ToolStripButton get_data_from_excel;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn vorname;
        private System.Windows.Forms.DataGridViewTextBoxColumn gleitzeit_ges;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn real_name;
        private System.Windows.Forms.ProgressBar pgBar;
    }
}

