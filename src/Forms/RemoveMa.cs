using GleitzeitControlPanel.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GleitzeitControlPanel.Forms
{
    public partial class RemoveMa : Form
    {
        Sqlhelper Sqlconnhelper = new Sqlhelper("srvha\\sqlha_pm3", "gleitzeit"); // Initialisierung des Sqlhelpers 
        public RemoveMa()
        {
            InitializeComponent();
            fill_dgv_uebersicht();
            dgv_uebersicht.CellValueChanged += set_active_state;
            dgv_uebersicht.CellMouseUp += stop_edit;
        }

        private void RemoveMa_Load(object sender, EventArgs e)
        {

        }

        private void fill_dgv_uebersicht()
        {
            string sqlTask = "SELECT id, vorname, nachname, active FROM mitarbeiter";
            List<List<string>> resSet = Sqlconnhelper.SelComm(sqlTask);

            foreach (List<string> row in resSet)
            {
                bool active = true;
                try
                {
                    if (Int32.Parse(row[3]) == 0) { active = false; }
                    dgv_uebersicht.Rows.Add(row[2], row[1], active, row[0]);
                }
                catch
                {
                    dgv_uebersicht.Rows.Add(row[2], row[1], active, row[0]);
                }

                
            }
        }

        private void set_active_state(object sender, EventArgs e)
        {
            int rowIndex = dgv_uebersicht.CurrentCell.RowIndex;
            string sqlTask = "";

            if (dgv_uebersicht.Rows[rowIndex].Cells[2].Value.ToString() == "False")
            {
                sqlTask = "UPDATE mitarbeiter SET active=0 WHERE id=" + dgv_uebersicht.Rows[rowIndex].Cells[3].Value.ToString() + ";";
            }
            else if (dgv_uebersicht.Rows[rowIndex].Cells[2].Value.ToString() == "True")
            {
                sqlTask = "UPDATE mitarbeiter SET active=1 WHERE id=" + dgv_uebersicht.Rows[rowIndex].Cells[3].Value.ToString() + ";";
            }

            // MessageBox.Show(sqlTask);

            Sqlconnhelper.ExecComm(sqlTask);

        }

        private void stop_edit(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgv_uebersicht.EndEdit();
        }

        private void dgv_uebersicht_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
