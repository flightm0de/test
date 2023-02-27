using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using GleitzeitControlPanel.Classes;
using GleitzeitControlPanel.Forms;

namespace GleitzeitControlPanel
{
    public partial class Main : Form // Mainform
    {
        private Sqlhelper Sqlconnhelper = new Sqlhelper("srvha\\sqlha_pm3", "gleitzeit"); // Initialisierung des Sqlhelpers 
        private List<User> Userlist = new List<User>(); // Initialisierung einer Liste mit User Objekten; jedes Objekt stellt einen User als Klasse da (Siehe Classes/User.cs)
       
        private int currKW;
        private int currJahr;

        // Farbkonstanten die bestimmen welche Farbe ein Feld erhält abhängig von den geleisteten Überstunden

        private Color TOO_MANY_HOURS_COLOR = Color.Red; 
        private Color EQUAL_HOURS_COLOR = Color.Yellow;
        private Color LESS_HOURS_COLOR = Color.White;
        private Color NOT_ENOUGH_HOURS_COLOR = Color.DarkCyan;

        private const bool AUTO_CALC_GLEITZEIT_STAND = false; // Bestimmt ob der Gleitzeit-Stand als Fix-Wert aus der DB gezogen oder automatisch anhand des Archivs berechnet wird

        public Main()
        {
            InitializeComponent();

            KeyPreview = true;

            // Initialiserung der Kalender Klasse um aktuelles Jahr & KW zu bestimmen

            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;

            currKW = cal.GetWeekOfYear(DateTime.Today.AddDays(-7), dfi.CalendarWeekRule, dfi.FirstDayOfWeek); // aktuelle KW & Jahr -> Wert im Hintergrund des Programms
            currJahr = cal.GetYear(DateTime.Today.AddDays(-7));
            //currJahr = 2022;

            currJahr_input.Text = currJahr.ToString(); // Setzen des Ausgangswerts des aktuellen Jahres
            currJahr_input.Leave += jahr_input; // Ereignis das Eintritt wenn der Fokus das Jahr-Eingabefeld verlässt
            currKW_input.Text = currKW.ToString(); // Setzen des Ausgangswerts der aktuellen KW 
            currKW_input.Leave += kw_input; // Ereignis das Eintritt wenn der Fokus das KW-Eingabefeld verlässt

            if (AUTO_CALC_GLEITZEIT_STAND)
            {
                dgv_gleitzeituebersicht.Columns[2].ReadOnly = true;
            }
            else
            {
                dgv_gleitzeituebersicht.Columns[2].ReadOnly = false;
            }
            

            // "Blätter" Buttons zu Programmbeginn aktivieren bzw deaktivieren basierend darauf, ob entsprechende Werte in der Datenbank existieren

            if (!Sqlconnhelper.check_kw_exist(currKW - 1, currJahr))
            {
                //kw_zurueck.Enabled = false;
            }

            else if (!Sqlconnhelper.check_kw_exist(currKW + 1, currJahr))
            {
                //kw_vor.Enabled = false;
            }

            if (!Sqlconnhelper.check_jahr_exist(currJahr - 1))
            {
                jahr_zurueck.Enabled = false;
            }

            // -------------------------------------------------


            // Zu Programmbeginn füllen aller DataGrids mit entsprechenden Daten sowie ensprechende Ereignisverweise die Eintreten sollen
            // bei Änderungen von Zelleninhalten

            fill_userlist_on_start();
            fill_usertime_on_start();
            fill_user_stand_on_start();

            fill_dgv_gleitzeituebersicht();
            fill_dgv_gesuebersicht();
            dgv_gesuebersicht_colorRedOrYellow();


            dgv_settings.CellValidating += validate_dgv_settings_input;
            dgv_gesuebersicht.CellValidating += validate_dgv_gesuebersicht_input;
            dgv_useruebersicht.CellValidating += validate_dgv_useruebersucht_input;

            dgv_gleitzeituebersicht.CellClick += switch_user;

            if (!AUTO_CALC_GLEITZEIT_STAND)
            {
                dgv_gleitzeituebersicht.CellValueChanged += dgv_gleitzeituebersicht_on_change;
            }

            dgv_gesuebersicht.CellValueChanged += dgv_gesuebersicht_on_change;
            dgv_useruebersicht.CellValueChanged += dgv_useruebersicht_on_change;
            dgv_settings.CellValueChanged += dgv_settings_on_change;

            fill_dgv_useruebersicht("1");
            lbl_useruebersicht.Text = "Benutzerübersicht: " + get_ma_by_id("1").name + " " + get_ma_by_id("1").vorname;
            dgv_useruebersicht_colorRedOrYellow();
            dgv_settings.Rows.Add(Userlist[0].wochenstd, Userlist[0].maxwoche, Userlist[0].maxgleitzeit, Userlist[0].id);

            currKW_input.KeyPress += validate_char_input;
            currJahr_input.KeyPress += validate_char_input;

            // -------------------------------------------------
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dgv_settings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // unsused
        }

        private void validate_char_input(object sender, KeyPressEventArgs e)
        // Prüft ob eingegebener Wert ein Buchstabe oder Backspace ist, wenn nicht wird die Eingabe ignoriert
        {
            if (!char.IsDigit(e.KeyChar) && (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
            if ((int)e.KeyChar == 13)
            {
                kw_input(null, null);
                jahr_input(null, null);
            }
        }

        private void kw_input(object sender, EventArgs e)
        // Ereignis das Eintritt, wenn man die gewünschte KW manuell eintippt um gezielt zu einer KW zu springen wenn diese existiert
        {
            try
            {
                if (Sqlconnhelper.check_kw_exist(Int32.Parse(currKW_input.Text), Int32.Parse(currJahr_input.Text)))
                {
                    currKW = Int32.Parse(currKW_input.Text);
                    reset_dgv_gesuebersicht();
                }
                else
                {
                    MessageBox.Show("Für KW: " + currKW_input.Text + " im Jahr: " + currJahr_input.Text + " existieren keine Daten!", "Fehler");
                    currKW_input.Text = currKW.ToString();
                    currJahr_input.Text = currJahr.ToString();
                    this.ActiveControl = currKW_input;
                }

                set_button_states();
            }
            catch (FormatException)
            {
                currKW_input.Text = currKW.ToString();
                currJahr_input.Text = currJahr.ToString();
                this.ActiveControl = currKW_input;
            }
        }

        private void jahr_input(object sender, EventArgs e)
        // Ereignis das Eintritt, wenn man das gewünschte Jahr manuell eintippt um gezielt zum Jahr zu springen wenn dieses existiert
        {
            try
            {
                if (Sqlconnhelper.check_kw_exist(Int32.Parse(currKW_input.Text), Int32.Parse(currJahr_input.Text)))
                {
                    currJahr = Int32.Parse(currJahr_input.Text);
                    reset_dgv_gesuebersicht();
                    reset_dgv_gleitzeituebersicht();
                }
                else
                {
                    MessageBox.Show("Für KW: " + currKW_input.Text + " im Jahr: " + currJahr_input.Text + " existieren keine Daten!", "Fehler");
                    currJahr_input.Text = currJahr.ToString();
                    this.ActiveControl = currJahr_input;
                }

                set_button_states();
            }
            catch (FormatException)
            {
                currJahr_input.Text = currJahr.ToString();
                this.ActiveControl = currJahr_input;
            }
        }

        private void fill_dgv_gesuebersicht()
        // füllt das dgv_gesuebersicht datagrid mit Daten abhängig von der eingetragnen KW & Jahr
        {
            foreach (User ma in Userlist)
            {
                if (ma.active != "0")
                {
                    foreach (List<string> eintrag in ma.zeiten)
                    {
                        if (eintrag[0] == currKW.ToString() && eintrag[2] == currJahr.ToString())
                        {
                            if (ma.displayname != "")
                            {
                                dgv_gesuebersicht.Rows.Add(eintrag[0], eintrag[2], eintrag[1], ma.displayname, ma.vorname, ma.id);
                            }
                            else
                            {
                                dgv_gesuebersicht.Rows.Add(eintrag[0], eintrag[2], eintrag[1], ma.name, ma.vorname, ma.id);
                            }
                        }
                    }
                }
            }
        }
        private void fill_dgv_gleitzeituebersicht()
        // füllt das dgv_gleitzeituebersicht datagrid mit Daten abhängig vom gewählten Jahr
        {
            foreach (User ma in Userlist)
            {
                if (ma.active != "0")
                {
                    double currGleitzeit = 0.0;

                    if (AUTO_CALC_GLEITZEIT_STAND)
                    {
                        foreach (List<string> zeit in ma.zeiten)
                        {
                            if (zeit[2] == currJahr.ToString())
                            {
                                currGleitzeit += Double.Parse(check_for_punkt(zeit[1]));
                            }
                        }
                    }
                    else if (!AUTO_CALC_GLEITZEIT_STAND)
                    {
                        try
                        {
                            foreach (List<string> row in ma.stand_zeiten)
                            {
                                if (row[0] == currJahr.ToString())
                                {
                                    currGleitzeit = Convert.ToDouble(row[1]);
                                }
                            }                          
                        }
                        catch
                        {
                            currGleitzeit = 0.0;
                        }
                    }

                    if (ma.displayname != "")
                    {
                        dgv_gleitzeituebersicht.Rows.Add(ma.displayname, ma.vorname, currGleitzeit, ma.id, ma.name);
                    }
                    else
                    {
                        dgv_gleitzeituebersicht.Rows.Add(ma.name, ma.vorname, currGleitzeit, ma.id, ma.name);
                    }

                }
            }
            dgv_gleitzeituebersicht_colorRedOrYellow();
        }

        private void dgv_gesuebersicht_colorRedOrYellow()
        // Färbt die Zellen des dgv_gesuebersicht datagrid abhängig von den geleisteten und den erlaubten Überstunden in den festgelegten Farben ein
        {
            for (int row = 0; row < dgv_gesuebersicht.RowCount; row++)
            {
                string id = dgv_gesuebersicht.Rows[row].Cells[5].Value.ToString();
                double ueberstunden = Double.Parse(check_for_punkt(dgv_gesuebersicht.Rows[row].Cells[2].Value.ToString()));
                double maxUeberstunden = 0;

                foreach (User ma in Userlist)
                {
                    if (ma.id == id)
                    {
                        maxUeberstunden = Double.Parse(check_for_punkt(ma.maxwoche));
                        break;
                    }
                }
              
                if (ueberstunden > maxUeberstunden)
                {
                    dgv_gesuebersicht.Rows[row].Cells[2].Style.BackColor = TOO_MANY_HOURS_COLOR;
                }
                else if (ueberstunden == maxUeberstunden)
                {
                    dgv_gesuebersicht.Rows[row].Cells[2].Style.BackColor = EQUAL_HOURS_COLOR;
                }
                else if (ueberstunden < maxUeberstunden && ueberstunden >= 0.0)
                {
                    dgv_gesuebersicht.Rows[row].Cells[2].Style.BackColor = LESS_HOURS_COLOR;
                }
                else if (ueberstunden < 0.0)
                {
                    dgv_gesuebersicht.Rows[row].Cells[2].Style.BackColor = NOT_ENOUGH_HOURS_COLOR;
                }
            }
        }

        private void dgv_gleitzeituebersicht_colorRedOrYellow()
        // Färbt die Zellen des dgv_gleitzeituebersicht datagrid abhängig von den geleisteten und den erlaubten Überstunden in den festgelegten Farben ein
        {
            for (int row = 0; row < dgv_gleitzeituebersicht.RowCount; row++)
            {
                string id = dgv_gleitzeituebersicht.Rows[row].Cells[3].Value.ToString();
                double currGleitzeit = Double.Parse(check_for_punkt(dgv_gleitzeituebersicht.Rows[row].Cells[2].Value.ToString()));
                double maxgleitzeit = 0;

                foreach (User ma in Userlist)
                {
                    if (ma.id == id)
                    {
                        maxgleitzeit = Double.Parse(ma.maxgleitzeit);
                        break;
                    }
                }

                if (currGleitzeit > maxgleitzeit)
                {
                    dgv_gleitzeituebersicht.Rows[row].Cells[2].Style.BackColor = TOO_MANY_HOURS_COLOR;
                }
                else if (currGleitzeit == maxgleitzeit)
                {
                    dgv_gleitzeituebersicht.Rows[row].Cells[2].Style.BackColor = EQUAL_HOURS_COLOR;
                }
                else if (currGleitzeit < maxgleitzeit && currGleitzeit >= 0.0)
                {
                    dgv_gleitzeituebersicht.Rows[row].Cells[2].Style.BackColor = LESS_HOURS_COLOR;
                }
                else if (currGleitzeit < 0.0)
                {
                    dgv_gleitzeituebersicht.Rows[row].Cells[2].Style.BackColor = NOT_ENOUGH_HOURS_COLOR;
                }
            }
        }

        private void dgv_useruebersicht_colorRedOrYellow()
        // Färbt die Zellen des dgv_useruebersicht datagrid abhängig von den geleisteten und den erlaubten Überstunden in den festgelegten Farben ein
        {
            for (int row = 0; row < dgv_useruebersicht.RowCount; row++)
            {
                string id = dgv_useruebersicht.Rows[row].Cells[3].Value.ToString();
                double currGleitzeit = Double.Parse(check_for_punkt(dgv_useruebersicht.Rows[row].Cells[1].Value.ToString()));
                double maxgleitzeit = 0;

                foreach (User ma in Userlist)
                {
                    if (ma.id == id)
                    {
                        maxgleitzeit = Double.Parse(check_for_punkt(ma.maxwoche));
                        break;
                    }
                }

                if (currGleitzeit > maxgleitzeit)
                {
                    dgv_useruebersicht.Rows[row].Cells[1].Style.BackColor = TOO_MANY_HOURS_COLOR;
                }
                else if (currGleitzeit == maxgleitzeit)
                {
                    dgv_useruebersicht.Rows[row].Cells[1].Style.BackColor = EQUAL_HOURS_COLOR;
                }
                else if (currGleitzeit < maxgleitzeit && currGleitzeit >= 0.0)
                {
                    dgv_useruebersicht.Rows[row].Cells[1].Style.BackColor = LESS_HOURS_COLOR;
                }
                if (currGleitzeit < 0.0)
                {
                    dgv_useruebersicht.Rows[row].Cells[1].Style.BackColor = NOT_ENOUGH_HOURS_COLOR;
                }
            }
        }

        private void fill_userlist_on_start()
        // Füllt die 
        {
            List<List<string>> res = Sqlconnhelper.SelComm("SELECT * FROM mitarbeiter ORDER BY nachname, vorname");

            foreach (List<string> row in res)
            {
                Userlist.Add(new User(
                    row[0].TrimEnd(),
                    row[1].TrimEnd(),
                    row[2].TrimEnd(),
                    row[7].TrimEnd(),
                    row[3].TrimEnd(),
                    row[4].TrimEnd(),
                    row[5].TrimEnd(),
                    row[6].TrimEnd()
                    )); ;
            }
        }

        private void fill_usertime_on_start()
        // Füllt die Userlist mit den User-Objekten zu Programmbeginn
        {
            List<List<string>> res = Sqlconnhelper.SelComm("SELECT * FROM archiv ORDER BY kw");

            foreach (List<string> row in res)
            {
                foreach (User ma in Userlist)
                {
                    if (ma.id == row[0].TrimEnd())
                    {
                        ma.addZeit(row[1], row[2], row[3]);
                    }
                }
            }
        }

        private void fill_user_stand_on_start()
        {
            List<List<string>> res = Sqlconnhelper.SelComm("SELECT * FROM stand_archiv");
            
            foreach (List<string> row in res)
            {
                foreach (User ma in Userlist)
                {
                    if (ma.id == row[0].TrimEnd())
                    {
                        ma.add_stand(row[1], row[2]);
                    }
                }
            }
        }

        private void add_new_usertime(int jahr)
        // Fragt alle Zeiten ab für das angegebene Jahr und fügt diese in die zeiten-Liste des User-Objekts ein; siehe Classes/User.cs für nähere Struktur
        {
            List<List<string>> res = Sqlconnhelper.SelComm("SELECT * FROM archiv WHERE jahr = " + jahr.ToString());

            foreach (List<string> row in res)
            {
                foreach (User ma in Userlist)
                {
                    if (ma.id == row[0].TrimEnd())
                    {
                        ma.addZeit(row[1], row[2], row[3]);
                    }
                }
            }
        }

        private void kw_zurueck_Click(object sender, EventArgs e)
        // Tritt ein wenn die KW durch den "<" Button verringert wird; verringert kw um 1 und füllt das datagrid mit den neuen Werten
        {
            if (currKW - 1 > 0)
            {
                currKW--;
                currKW_input.Text = currKW.ToString();
                reset_dgv_gesuebersicht();
            }
            set_button_states();
        }

        private void kw_vor_Click(object sender, EventArgs e)
        // Tritt ein wenn die KW durch den ">" Button vergrößert wird; vergrößert kw um 1 und füllt das datagrid mit den neuen Werten
        {
            currKW++;
            currKW_input.Text = currKW.ToString();
            reset_dgv_gesuebersicht();
            set_button_states();
        }

        private void switch_user(object sender, DataGridViewCellEventArgs e)
        // Tritt ein wenn im dgv_gleitzeituebersicht eine Zeile angeklickt wird -> NUR bei tatsächlichem Mausklick, nicht focusen der Zelle durch Tastatur
        // Füllt das dgv_settings und dgv_useruebersicht anhand des gewählten Users mit Daten 
        {
            int rowIndex = dgv_gleitzeituebersicht.CurrentCell.RowIndex;

            foreach (User ma in Userlist)
            {
                if (ma.name == dgv_gleitzeituebersicht.Rows[rowIndex].Cells[4].Value.ToString() && ma.vorname == dgv_gleitzeituebersicht.Rows[rowIndex].Cells[1].Value.ToString())
                {
                    dgv_settings.Rows.Clear();
                    dgv_settings.Rows.Add(ma.wochenstd, ma.maxwoche, ma.maxgleitzeit, ma.id);
                    reset_dgv_useruebersicht(Int32.Parse(ma.id));
                    if (ma.displayname != "")
                    {
                        lbl_useruebersicht.Text = "Benutzerübersicht: " + ma.displayname + " " + ma.vorname;
                    }
                    else
                    {
                        lbl_useruebersicht.Text = "Benutzerübersicht: " + ma.name + " " + ma.vorname;
                    }
                    
                    break;
                    //focus_same_kw(currKW.ToString());
                }
            }

            
        }
        private void fill_dgv_useruebersicht(string id)
        // füllt das dgv_useruebersicht datagrid anhand einer gegebenen ID mit Daten 
        {
            foreach (User ma in Userlist)
            {
                if (ma.id == id && ma.active != "0")
                {
                    foreach (List<string> eintrag in ma.zeiten)
                    {
                        if (eintrag[2] == currJahr.ToString())
                        {
                            dgv_useruebersicht.Rows.Add(Int32.Parse(eintrag[0]), eintrag[1], eintrag[2], ma.id);
                        }
                    }
                }
            }
        }

        private void dgv_gleitzeituebersicht_on_change(object sender, EventArgs e)
        {
            int rowIndex = dgv_gleitzeituebersicht.CurrentCell.RowIndex;
            string id = dgv_gleitzeituebersicht.Rows[rowIndex].Cells[3].Value.ToString();
            string stand = check_for_komma(dgv_gleitzeituebersicht.Rows[rowIndex].Cells[2].Value.ToString());

            Sqlconnhelper.add_stand_zeit(Convert.ToInt32(id), currJahr, stand);

            foreach (User ma in Userlist)
            {
                if (ma.id == id)
                {
                    foreach (List<string> row in ma.stand_zeiten)
                    {
                        if (row[0] == currJahr.ToString())
                        {
                            ma.stand = row[1];
                        }
                    }
                }
            }
            dgv_gleitzeituebersicht_colorRedOrYellow();
        }

        private void dgv_gesuebersicht_on_change(object sender, EventArgs e)
        // Tritt ein wenn eine Zelle des dgv_gesuebersicht datagrid verändert wird
        // Dabei wird der neue Wert zuerst validiert und daraufhin an die DB abgeschickt.
        // Am Ende werden noch die entsprechenden Zellen eingefärbt, die das datagrid zurückgesetzt und mit den
        // abgeänderten Werten gefüllt sowie die entsprechenden Einträge der Useruebersicht gefocused
        {
            int rowIndex = dgv_gesuebersicht.CurrentCell.RowIndex;
            string id = dgv_gesuebersicht.Rows[rowIndex].Cells[5].Value.ToString();
            string kw = dgv_gesuebersicht.Rows[rowIndex].Cells[0].Value.ToString();
            string jahr = dgv_gesuebersicht.Rows[rowIndex].Cells[1].Value.ToString();
            string zeitAfter = dgv_gesuebersicht.Rows[rowIndex].Cells[2].Value.ToString();
            string zeitBefore = "";
            User currUser = get_ma_by_id(id);

            foreach (List<string> eintrag in currUser.zeiten)
            {
                if (eintrag[0] == kw  && eintrag[2] == jahr)
                {
                    zeitBefore = eintrag[1];
                    eintrag[1] = zeitAfter;
                }
            }

            //MessageBox.Show(zeitBefore + " ### " + zeitAfter);

            string sqlTask = "UPDATE archiv SET zeit=" + check_for_komma(zeitAfter) + " WHERE id=" + id + " AND kw=" + kw + " AND jahr=" + jahr + " AND zeit=" + check_for_komma(zeitBefore);
            //MessageBox.Show(sqlTask);
            Sqlconnhelper.ExecComm(sqlTask);
            dgv_gesuebersicht_colorRedOrYellow();
            reset_dgv_gleitzeituebersicht();
            reset_dgv_useruebersicht(Int32.Parse(id));
            dgv_settings.Rows.Clear();
            dgv_settings.Rows.Add(currUser.wochenstd, currUser.maxwoche, currUser.maxgleitzeit, currUser.id);
            focus_same_kw(kw);
            focus_same_user(id);
            User ma = get_ma_by_id(id);
            if (ma.displayname != "")
            {
                lbl_useruebersicht.Text = "Benutzerübersicht: " + ma.displayname + " " + ma.vorname;
            }
            else
            {
                lbl_useruebersicht.Text = "Benutzerübersicht: " + ma.name + " " + ma.vorname;
            }
            
        }
        private void focus_same_kw(string kw)
        // Fokussiert die angegebene KW im dgv_useruebersicht datagrid
        {
            for (int i = 0; i < dgv_useruebersicht.RowCount; i++)
            {
                if (dgv_useruebersicht.Rows[i].Cells[0].Value.ToString() == kw)
                {                 
                    dgv_useruebersicht.FirstDisplayedScrollingRowIndex = i;
                    //dgv_useruebersicht.CurrentCell = dgv_gesuebersicht[0, i];
                }
            }
        }

        private void focus_same_user(string id)
        // Fokussiert einen User anhand der gegebenen ID im dgv_gleitzeituebersicht datagrid
        {
            for (int i = 0; i < dgv_gleitzeituebersicht.RowCount; i++)
            {
                if (dgv_gleitzeituebersicht.Rows[i].Cells[3].Value.ToString() == id)
                {
                    dgv_gleitzeituebersicht.FirstDisplayedScrollingRowIndex = i;
                    dgv_gleitzeituebersicht.CurrentCell = dgv_gleitzeituebersicht[0, i];
                }
            }
        }

        private void dgv_useruebersicht_on_change(object sender, EventArgs e)
        // Tritt ein wenn ein Zellwert des dgv_useruebersicht datagrids geändert wird
        {
            int rowIndex = dgv_useruebersicht.CurrentCell.RowIndex;
            string id = dgv_useruebersicht.Rows[rowIndex].Cells[3].Value.ToString();
            string kw = dgv_useruebersicht.Rows[rowIndex].Cells[0].Value.ToString();
            string jahr = dgv_useruebersicht.Rows[rowIndex].Cells[2].Value.ToString();
            string zeitAfter = dgv_useruebersicht.Rows[rowIndex].Cells[1].Value.ToString();
            string zeitBefore = "";

            foreach (List<string> eintrag in get_ma_by_id(id).zeiten)
            {
                if (eintrag[0] == kw && eintrag[2] == jahr)
                {
                    zeitBefore = eintrag[1];
                    eintrag[1] = zeitAfter;
                }
            }

            string sqlTask = "UPDATE archiv SET zeit=" + check_for_komma(zeitAfter) + " WHERE id=" + id + " AND kw=" + kw + " AND jahr=" + jahr + " AND zeit=" + check_for_komma(zeitBefore);
            Sqlconnhelper.ExecComm(sqlTask);
            reset_dgv_gesuebersicht();
            dgv_useruebersicht_colorRedOrYellow();
            reset_dgv_gleitzeituebersicht();
            focus_same_user(id);
        }

        private void dgv_settings_on_change(object sender, EventArgs e)
        // Tritt ein wenn ein Zellwert des dgv_settings datagrid geändert wird
        {
            string id = dgv_settings.Rows[0].Cells[3].Value.ToString();
            string wochenstd = dgv_settings.Rows[0].Cells[0].Value.ToString();
            string maxwoche = dgv_settings.Rows[0].Cells[1].Value.ToString();
            string maxgleitzeit = check_for_punkt((dgv_settings.Rows[0].Cells[2].Value.ToString()));

            foreach (User ma in Userlist)
            {
                if (ma.id == id)
                {
                    ma.wochenstd = wochenstd;
                    ma.maxwoche = maxwoche;
                    ma.maxgleitzeit = maxgleitzeit;
                }
            }

            string sqlTask = "UPDATE mitarbeiter SET maxwoche=" + check_for_komma(maxwoche) + ", maxgleitzeit=" + check_for_komma(maxgleitzeit) + ", wochenstd=" + check_for_komma(wochenstd) + " WHERE id=" + id;
            //MessageBox.Show(sqlTask);
            Sqlconnhelper.ExecComm(sqlTask);

            //dgv_gleitzeituebersicht.Rows.Clear();
            //fill_dgv_gleitzeituebersicht();
            dgv_gleitzeituebersicht_colorRedOrYellow();
            dgv_useruebersicht_colorRedOrYellow();
            dgv_gesuebersicht_colorRedOrYellow();
        }

        private void validate_dgv_settings_input(object sender, DataGridViewCellValidatingEventArgs e)
        // Prüft ob ein eingegebener Zellwert des dgv_settings datagrid eine Fließkommazahl ist sowie 
        // max. 6 Zeichen lang UND max. 2 Nachkommastellen hat.
        // Wenn dies nicht der Fall ist, wird der eingegebene Wert abgewiesen
        {
            Double parsed;
            if (!Double.TryParse(e.FormattedValue.ToString(), out parsed))
            {
                this.dgv_settings.CancelEdit();
            }

            if (!valid_double(e.FormattedValue.ToString()))
            {
                this.dgv_settings.CancelEdit();
            }

        }

        private string check_for_komma(string str)
        // Durchläuft einen gegebenen String und ersetzt alle " , " mit " . " und gibt den neuen String zurück
        {
            string retStr = "";

            foreach (char c in str)
            {
                if (c == ',')
                {
                    retStr += '.';
                }
                else
                {
                    retStr += c;
                }
            }
            return retStr;
        }

        private bool valid_double(string str)
        // Prüft ob ein gegebener Wert eine zulässige Kommazahl ist -> wird aber als String an diese Methode übergeben !
        // Zulässige Zahlen sind die, mit einer max. Gesamtzeichenlänge von 6 UND max. 2 Nachkommazahlen
        // Trifft dies zu wird true zurückgegeben, andernfalls false
        {
            if (str.Contains(",") || str.Contains("."))
            {
                string vor = "";
                string nach = "";
                bool punktFound = false;

                foreach (char c in str)
                {
                    if (c != '.' && c != ',' && !punktFound)
                    {
                        vor += c;
                    }
                    else if (c != '.' && c != ',' && punktFound)
                    {
                        nach += c;
                    }
                    else if (c == '.' || c == ',')
                    {
                        punktFound = true;
                    }
                }

                if (vor.Length <= 4 && nach.Length <= 2)
                {
                    return true;
                }
            }

            else
            {
                if (str.Length <= 4)
                {
                    return true;
                }
            }

            return false;
        }

        private string check_for_punkt(string str)
        // DUrchläuft einen gegebenen String und ersetzt alle " . " mit " , " und gibt den neuen String zurück
        {
            string retStr = "";

            foreach (char c in str)
            {
                if (c == '.')
                {
                    retStr += ',';
                }
                else
                {
                    retStr += c;
                }
            }
            return retStr;
        }
        private User get_ma_by_id(string id)
        // Liefert das User-Objekt eines Mitarbeiters anhand einer gegebenen ID
        {
            foreach (User ma in Userlist)
            {
                if (ma.id == id)
                {
                    return ma;
                }
            }

            return new User("user_not_found", "", "", "", "", "", "", "");
        }

        private void validate_dgv_gesuebersicht_input(object sender, DataGridViewCellValidatingEventArgs e)
        // Prüft ob im dgv_gesuebersicht datagrid eingegebene Werte zulässige Fließkommazahlen sind
        // Wenn nicht, wird die Eingabe abgewiesen
        {
            //Double parsed;
            if (!Double.TryParse(e.FormattedValue.ToString(), out _))
            {
                this.dgv_gesuebersicht.CancelEdit();
            }

            if (!valid_double(e.FormattedValue.ToString()))
            {
                this.dgv_gesuebersicht.CancelEdit();
            }
        }

        private void validate_dgv_useruebersucht_input(object sender, DataGridViewCellValidatingEventArgs e)
        // Prüft ob im dgv_useruebersicht datagrid eingegebene Werte zulässige Fließkommazahlen sind
        // Wenn nicht, wird die Eingabe abgewiesen
        {
            //Double parsed;
            if (!Double.TryParse(e.FormattedValue.ToString(), out _))
            {
                this.dgv_useruebersicht.CancelEdit();
            }

            if (!valid_double(e.FormattedValue.ToString()))
            {
                this.dgv_useruebersicht.CancelEdit();
            }
        }

        private void dgv_useruebersicht_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // unused
        }

        private void jahr_zurueck_Click(object sender, EventArgs e)
        // Tritt ein wenn das Jahr per Buttonklick auf "<" verkleinert wird
        // und füllt das dgv_gesuebersicht und dgv_gleitzeituebersicht mit den
        // neu gewählten Werten
        {
            //MessageBox.Show(currJahr.ToString());
            currJahr--;
            reset_dgv_gesuebersicht();
            reset_dgv_gleitzeituebersicht();
            currJahr_input.Text = currJahr.ToString();
            //MessageBox.Show(currJahr.ToString());
            set_button_states();
        }

        private void jahr_vor_Click(object sender, EventArgs e)
        // Tritt ein wenn das Jahr per Buttonklick auf ">" vergrößert wird
        // und füllt das dgv_gesuebersicht und dgv_gleitzeituebersicht mit den
        // neu gewählten Werten
        // Ist das neue Jahr noch nicht vorhanden, erfolgt die Abfrage ob die benötigten Daten in der DB erstellt werden sollen
        {
            //if (Sqlconnhelper.check_jahr_exist(currJahr+1))
            //{
                currJahr++;
                reset_all_dgv();
                currJahr_input.Text = currJahr.ToString();
            /*}
            else
            {
                string message = "Das Jahr " + (currJahr + 1) + " existiert noch nicht in der Datenbank.\nSollen die benötigten Einträge erstellt werden ?";

                if (MessageBox.Show(message, "Achtung", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    currJahr++;
                    Sqlconnhelper.createKwDummy(Userlist.Count, currJahr);
                    add_new_usertime(currJahr);
                    reset_dgv_gesuebersicht();
                    reset_dgv_gleitzeituebersicht();;
                    currJahr_input.Text = currJahr.ToString();
                }
            }*/
            //MessageBox.Show(currJahr.ToString());

            set_button_states();
        }

        private void reset_dgv_useruebersicht(int userid=-1)
        // Resettet die Werte des dgv_useruebersicht und füllt diese erneut anhand einer gegebenen ID
        {
            dgv_useruebersicht.Rows.Clear();
            if (userid == -1)
            {
                fill_dgv_useruebersicht(dgv_settings.Rows[0].Cells[3].Value.ToString());
            }
            else
            {
                fill_dgv_useruebersicht(userid.ToString());
            }
            dgv_useruebersicht_colorRedOrYellow();
            focus_same_kw(currKW.ToString());
        }

        private void reset_dgv_gleitzeituebersicht()
        // Resettet die Werte des dgv_gleitzeituebersicht und füllt diese erneut anhand der KW & des Jahres
        {
            dgv_gleitzeituebersicht.Rows.Clear();
            fill_dgv_gleitzeituebersicht();
            dgv_gleitzeituebersicht_colorRedOrYellow();
        }

        private void reset_dgv_gesuebersicht()
        // Resettet die Werte des dgv_gesuebersicht und füllt diese erneut anhand der KW & des Jahres

        {
            dgv_gesuebersicht.Rows.Clear();
            fill_dgv_gesuebersicht();
            dgv_gesuebersicht_colorRedOrYellow();
        }

        private void reset_all_dgv(int id=-1)
        // Resettet die Werte ALLER datagrids
        {
            reset_dgv_useruebersicht(id);
            reset_dgv_gleitzeituebersicht();
            reset_dgv_gesuebersicht();
        }

        private void set_button_states()
        // Prüft ob die nächste/vorherige KW/Jahr als Wert in der DB existiert
        // Ist dies nicht der Fall, werden die Buttons deaktiviert bzw aktiviert
        {
            //MessageBox.Show(currKW.ToString() + " # " + currJahr.ToString());

            if(Sqlconnhelper.check_kw_exist(currKW - 1, currJahr))
            {
                //kw_zurueck.Enabled = true;
            }
            else
            {
                //kw_zurueck.Enabled = false;
            }

            if (Sqlconnhelper.check_kw_exist(currKW + 1, currJahr))
            {
                //kw_vor.Enabled = true;
            }
            else
            {
                //kw_vor.Enabled = false;
            }

            if (Sqlconnhelper.check_kw_exist(currKW, currJahr - 1))
            {
                jahr_zurueck.Enabled = true;
            }
            else
            {
                jahr_zurueck.Enabled = false;
            }
        }
        private void dgv_gleitzeituebersicht_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // unused
        }

        private void btn_add_ma_Click(object sender, EventArgs e)
        {
            AddMa AddMaForm = new AddMa();
            AddMaForm.MdiParent = this.MdiParent;
            AddMaForm.ShowDialog();
        }

        private void btn_del_ma_Click(object sender, EventArgs e)
        {
            RemoveMa RemoveMaForm = new RemoveMa();
            RemoveMaForm.MdiParent = this.MdiParent;
            RemoveMaForm.ShowDialog();
        }

        //private void toolStripButton1_Click(object sender, EventArgs e)
        //{
        //    string sqlTask = "";
        //    int last_id = 0;

        //    foreach (List<string> ma_row in Excelreader.get_excel_data(currJahr.ToString()))
        //    {
        //        string output = "";
        //        foreach (string s in ma_row)
        //        {
        //            output += s + ", ";
        //        }


        //        foreach (User ma in Userlist)
        //        {
        //            string full_name = "";

        //            if (ma.displayname != "")
        //            {
        //                full_name = ma.displayname + " " + ma.vorname;
        //            }
        //            else
        //            {
        //                full_name = ma.name + " " + ma.vorname;
        //            }


        //            if (full_name == ma_row[0])
        //            {
        //                for (int kw = 5; kw < ma_row.Count; kw++) // Start bei kw = 5, da bis einschließlich row[4] Benutzerdaten wie Name und Vertragszeiten stehen
        //                {
        //                    //if (ma_row[0] == "Hamp Martin") { MessageBox.Show(ma_row[0] + " # " + "kw: " + Convert.ToString(kw - 4) + " # " + ma_row[kw]); }
        //                    if (Sqlconnhelper.check_kw_exist_with_id(kw - 4, currJahr, Int32.Parse(ma.id)))
        //                    {
        //                        sqlTask += "UPDATE archiv SET zeit='" + check_for_komma(ma_row[kw]) + "' WHERE id=" + ma.id + " AND kw=" + Convert.ToString(kw-4) + " AND jahr=" + currJahr + ";\n";
        //                        ma.change_zeit(kw.ToString(), check_for_komma(Math.Round(Double.Parse(ma_row[kw]), 2).ToString()), currJahr.ToString());
        //                    }
        //                    else
        //                    {
        //                        sqlTask += "INSERT INTO archiv (id, kw, zeit, jahr) VALUES(" + ma.id + ", " + Convert.ToString(kw - 4) + ", " + check_for_komma(ma_row[kw]) + ", " + currJahr + ");\n";
        //                        ma.addZeit(kw.ToString(), check_for_komma(Math.Round(Double.Parse(ma_row[kw]), 2).ToString()), currJahr.ToString());
        //                    }

        //                    last_id = Int32.Parse(ma.id);
        //                }
        //            }
        //        }

        //        Sqlconnhelper.ExecComm("UPDATE mitarbeiter SET maxwoche=" + check_for_komma(ma_row[2]) + ", maxgleitzeit=" + check_for_komma(ma_row[3]) + ", wochenstd=" + check_for_komma(ma_row[1]) + " WHERE id=" + last_id + ";");
        //        //MessageBox.Show(Sqlconnhelper.ExecComm(sqlTask).ToString());

        //        if (!AUTO_CALC_GLEITZEIT_STAND)
        //        {
        //            Sqlconnhelper.add_stand_zeit(Convert.ToInt32(last_id), currJahr, check_for_komma(ma_row[4]));
        //        }

        //        //MessageBox.Show("UPDATE mitarbeiter SET maxwoche=" + check_for_komma(ma_row[2]) + ", maxgleitzeit=" + check_for_komma(ma_row[3]) + ", wochenstd=" + check_for_komma(ma_row[1]) + " WHERE id=" + last_id + ";");
        //        //MessageBox.Show(ma_row[0]);
        //        //MessageBox.Show(sqlTask);
        //        //MessageBox.Show(sqlTask);
        //        Sqlconnhelper.ExecComm(sqlTask);
        //        //MessageBox.Show(sqlTask);
        //        sqlTask = "";
        //    }
        //    reset_all_dgv(1);
        //    MessageBox.Show("Daten wurden importiert, die Anwendung wird neu gestartet.");
        //    //System.Diagnostics.Process.Start(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\GleitzeitControlPanel.exe");
        //    //Application.Exit();
        //}

        private void start_pg_bar(int anzSteps)
        {
            pgBar.Visible = true;
            pgBar.Value += 35;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            List<List<string>> exceldata = Excelreader.get_excel_data(currJahr.ToString());

            foreach (User ma in Userlist)
            {
                Application.DoEvents();
                pgBar.Value += 100 / Userlist.Count;
                string full_name = "";

                if (ma.displayname != "")
                {
                    full_name = ma.displayname + " " + ma.vorname;
                }
                else
                {
                    full_name = ma.name + " " + ma.vorname;
                }

                foreach (List<string> row in exceldata)
                {
                    if (row[0] == full_name)
                    {
                        if (!AUTO_CALC_GLEITZEIT_STAND)
                        {
                            Sqlconnhelper.add_stand_zeit(Convert.ToInt32(ma.id), currJahr, check_for_komma(row[5]));
                        }

                        string sqlTask = "";

                        for (int i = 7; i < row.Count; i++)
                        {
                            string zeit = row[i];

                            if (zeit == "NULL")
                            {
                                zeit = "0";
                            }

                            if (Sqlconnhelper.check_kw_exist_with_id(i - 6, currJahr, Int32.Parse(ma.id)))
                            {
                                sqlTask += "UPDATE archiv SET zeit='" + check_for_komma(zeit) + "' WHERE id=" + ma.id + " AND kw=" + Convert.ToString(i - 6) + " AND jahr=" + currJahr + ";\n";
                            }
                            else
                            {
                                sqlTask += "INSERT INTO archiv (id, kw, zeit, jahr) VALUES(" + ma.id + ", " + Convert.ToString(i - 6) + ", " + check_for_komma(zeit) + ", " + currJahr + ");\n";
                            }
                        }

                        Sqlconnhelper.ExecComm(sqlTask);
                       //MessageBox.Show(sqlTask);
                    }
                }
            }
            pgBar.Value = 100;
            MessageBox.Show("Daten wurden importiert, die Anwendung wird nun geschlossen.");
            //System.Diagnostics.Process.Start(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\GleitzeitControlPanel.exe");
            Application.Exit();
        }

        private void test(object sender, EventArgs e) // toolStripButton1_Click
        {
            string sqlTask = "";

            foreach (List<string> row in Program.get_kw_data_from_excel(currJahr.ToString(), currKW))
            {
                foreach (User ma in Userlist)
                {
                    string full_name = "";

                    if (ma.displayname != "")
                    {
                        full_name = ma.displayname + " " + ma.vorname;
                    }
                    else
                    {
                        full_name = ma.name + " " + ma.vorname;
                    }

                    if (full_name == row[0])
                    {
                        //MessageBox.Show(full_name);
                        if (Sqlconnhelper.check_kw_exist(currKW, currJahr))
                        {
                            sqlTask += "UPDATE archiv SET zeit='" + check_for_komma(row[1]) + "' WHERE id=" + ma.id + " AND kw=" + currKW + " AND jahr=" + currJahr + ";\n";
                            ma.change_zeit(currKW.ToString(), check_for_komma(Math.Round(Double.Parse(row[1]), 2).ToString()), currJahr.ToString());
                        }
                        else
                        {
                            sqlTask += "INSERT INTO archiv (id, kw, zeit, jahr) VALUES(" + ma.id + ", " + currKW + ", " + check_for_komma(row[1]) + ", " + currJahr + ");\n";
                            ma.addZeit(currKW.ToString(), check_for_komma(Math.Round(Double.Parse(row[1]), 2).ToString()), currJahr.ToString());
                        }

                        //MessageBox.Show("UPDATE mitarbeiter SET maxwoche=" + check_for_komma(row[4]) + ", maxgleitzeit=" + check_for_komma(row[5]) + ", wochenstd=" + check_for_komma(row[3]) + " WHERE id=" + ma.id + ";");
                        Sqlconnhelper.ExecComm("UPDATE mitarbeiter SET maxwoche=" + check_for_komma(row[4]) + ", maxgleitzeit=" + check_for_komma(row[5]) + ", wochenstd=" + check_for_komma(row[3]) + " WHERE id=" + ma.id + ";");
                        
                        dgv_settings.Rows.Clear();

                        ma.maxwoche = row[4];
                        ma.maxgleitzeit = row[5];
                        ma.wochenstd = row[3];

                        dgv_settings.Rows.Add(ma.wochenstd, ma.maxwoche, ma.maxgleitzeit, ma.id);


                        if (!AUTO_CALC_GLEITZEIT_STAND)
                        {
                            Sqlconnhelper.add_stand_zeit(Convert.ToInt32(ma.id), currJahr, check_for_komma(row[2]));
                            // Sqlconnhelper.ExecComm("UPDATE mitarbeiter SET stand=" + check_for_komma(row[2]) + " WHERE id=" + ma.id + ";");
                        }
                    }
                }
            }
            
            if (sqlTask != "")
            {
                MessageBox.Show(sqlTask);
                Sqlconnhelper.ExecComm(sqlTask);
                reset_all_dgv(1);
            }                 
        }

        private void ts_menubar_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
