using GleitzeitControlPanel.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GleitzeitControlPanel.Forms
{
    public partial class AddMa : Form
    {
        private Sqlhelper Sqlconnhelper;
        DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
        public AddMa()
        {
            InitializeComponent();
            Sqlconnhelper = new Sqlhelper("srvha\\sqlha_pm3", "gleitzeit"); // Initialisierung des Sqlhelpers 
            tb_name.KeyPress += validate_letter_input;
            tb_vorname.KeyPress += validate_letter_input;
            tb_maxueberstd.KeyPress += validate_digit_input;
            tb_gleitzeitmax.KeyPress += validate_digit_input;
            tb_wochenstd.KeyPress += validate_digit_input;

        }

        private void btn_addMa_Click(object sender, EventArgs e)
        {
            int id = Sqlconnhelper.get_next_id();
            string sqlTask = "INSERT INTO mitarbeiter (id, vorname, nachname, maxwoche, maxgleitzeit, wochenstd, displayname) VALUES (" + id.ToString() + ", '"
                                                                                                                           + tb_vorname.Text.Trim() + "', '"
                                                                                                                           + check_for_komma(tb_name.Text.Trim()) + "', '"
                                                                                                                           + check_for_komma(tb_maxueberstd.Text.Trim()) + "', '"
                                                                                                                           + check_for_komma(tb_gleitzeitmax.Text.Trim()) + "', '"
                                                                                                                           + check_for_komma(tb_wochenstd.Text.Trim()) + "', '"
                                                                                                                           + check_for_komma(tb_name.Text.Trim()) + "');";
            int result = Sqlconnhelper.ExecComm(sqlTask);
            if (result != -1)
            {
                Sqlconnhelper.createKwDummyForNewUser(id);
                MessageBox.Show("Der Mitarbeiter wurde erfolgreich angelegt!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Ein unbekannter Fehler ist aufgetreten!");
            }
        }

        private string check_for_komma(string str)
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

        private void validate_digit_input(object sender, KeyPressEventArgs e)
        // Prüft ob eingegebener Wert ein Buchstabe oder Backspace ist, wenn nicht wird die Eingabe ignoriert
        {
            if (!char.IsDigit(e.KeyChar) && (int)e.KeyChar != 8 && (int)e.KeyChar != 44 && (int)e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }

        private void validate_letter_input(object sender, KeyPressEventArgs e)
        // Prüft ob eingegebener Wert ein Buchstabe oder Backspace ist, wenn nicht wird die Eingabe ignoriert
        {
            if (!char.IsLetter(e.KeyChar) && (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void AddMa_Load(object sender, EventArgs e)
        {

        }
    }
}
