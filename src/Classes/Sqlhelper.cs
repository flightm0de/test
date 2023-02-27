using GleitzeitControlPanel.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GleitzeitControlPanel.Classes
{
    internal class Sqlhelper
    // Sqlhelper Klasse die vorgefertige Methoden zum ausführen von SQL Befehlen liefert.
    {
        private SqlConnection conn;
        private string connectionString;

        public Sqlhelper(string host, string db)
        {
            connectionString = "Data Source=" + host + ";Initial Catalog=" + db + ";Integrated Security=false;user id=sa;password=Hallo2008";
            conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                conn.Close();
                // MessageBox.Show("Verbindung erfolgreich!");
            }
            catch (Exception)
            {
                //MessageBox.Show(e.ToString());
                MessageBox.Show("Fehler beim Verbinden mit der Datenbank.\nAnwendung wird geschlossen!");
            }

        }

        public int ExecComm(string sqlCom)
        // Führt einen Befehl aus und liefert KEINE Nutzdaten zurück, lediglich 1 oder -1 basierend darauf ob irgendwelche
        // Werte durch den Befehl geänderten wurden
        {
            SqlCommand comm = new SqlCommand(sqlCom, conn);
            int result = -1;

            comm.Connection.Open();
            try
            {
                result = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(sqlCom);
                MessageBox.Show(ex.ToString());
            }
            comm.Connection.Close();

            return result;
        }

        public List<List<String>> SelComm(string sqlCom)
        // Führt einen Befehl aus und liefert die betroffenen Nutzdaten zurück
        {
            conn.Open();

            SqlCommand comm = new SqlCommand(sqlCom, conn);
            SqlDataReader reader = comm.ExecuteReader();
            DataTable schemaTable = reader.GetSchemaTable();
            List<String> retValues = new List<String>();
            List<List<String>> retRows = new List<List<String>>();
            int columnCount = 0;
            int index = 0;

            foreach (DataRow row in schemaTable.Rows)
            {
                foreach (DataColumn column in schemaTable.Columns)
                {
                    if (column.ColumnName == "ColumnName")
                    {
                        columnCount++;
                    }
                }
            }

            while (reader.Read())
            {
                for (int i = 0; i < columnCount; i++)
                {
                    //retValues.Add(new List<String>());
                    retValues.Add(reader.GetValue(i).ToString());
                    
                }
                index++;
                retRows.Add(retValues);
                retValues = new List<string> { };
            }

            reader.Close();
            conn.Close();

            return retRows;
        }

        public bool check_stand_for_jahr_exist(int id, int jahr)
        {
            string sqlTask = "SELECT * FROM stand_archiv where id=" + id + " AND jahr=" + jahr + ";";

            int rowCount = SelComm(sqlTask).Count;

            if (rowCount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void add_stand_zeit(int id, int jahr, string zeit)
        {
            string sqlTask = "";

            if (check_stand_for_jahr_exist(id, jahr))
            {
                sqlTask = "UPDATE stand_archiv SET stand=" + zeit + " WHERE id=" + id + " AND jahr=" + jahr;
            }
            else
            {
                sqlTask = "INSERT INTO stand_archiv(id, jahr, stand) VALUES(" + id + ", " + jahr + ", " + zeit + ");";
            }

            ExecComm(sqlTask);
        }

        public double get_stand_zeit(int id, int jahr)
        {
            string sqlTask = "SELECT stand FROM stand_archiv WHERE id=" + id + " AND jahr=" + jahr;

            try
            {
                return Convert.ToDouble(SelComm(sqlTask)[0][0]);
            }
            catch (ArgumentOutOfRangeException)
            {
                return -1.0;
            }

        }

        public void createKwDummy(int anzUser, int jahr)
        // Erstellt basierend auf der Useranzahl & dem Jahr die benötigten Archiv-Einträge 
        {
            int anzKw = 53;
            int totalPgBarSteps = anzUser * anzKw;

            Waitwindow frmPgBar = new Waitwindow();
            frmPgBar.ShowDialog();

            for (int i = 1; i <= anzUser; i++)
            {
                for (int e = 1; e <= anzKw; e++)
                {
                    string sqlTask = "INSERT INTO archiv (id, kw, zeit, jahr) VALUES (" + i.ToString() + ", " + e.ToString() + ", " + "0, " +  jahr.ToString() + ");";
                    ExecComm(sqlTask);
                }
                
            }

            MessageBox.Show("Vorgang erfolgreich abgeschlossen", "Erfolg");
            frmPgBar.Close();
        }

        public void createKwDummyForNewUser(int id)
        {
            int jahr = Int32.Parse(SelComm("SELECT jahr FROM archiv ORDER BY jahr DESC")[0][0]);
            for (int i = 1; i < 53; i++)
            {
                string sqlTask = "INSERT INTO archiv (id, kw, zeit, jahr) VALUES (" + id + ", " + i + ", " + "0, " + jahr + ");";
                //MessageBox.Show(sqlTask);
                ExecComm(sqlTask);
            }
        }

        public bool check_jahr_exist(int jahr)
        // Prüft ob Datensätze für das angegebene Jahr existieren; liefert true oder false
        {
            string sqlTask = "SELECT * FROM archiv where jahr=" + jahr + ";";

            int rowCount = SelComm(sqlTask).Count;

            if (rowCount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool check_kw_exist(int kw, int jahr)
        // Prüft ob Datensätze für die angegebene KW existieren; liefert true oder false
        {
            string sqlTask = "SELECT * FROM archiv where kw=" + kw + " AND jahr=" + jahr + ";";

            int rowCount = SelComm(sqlTask).Count;

            if (rowCount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool check_kw_exist_with_id(int kw, int jahr, int id)
        {
            string sqlTask = "SELECT * FROM archiv where kw=" + kw + " AND jahr=" + jahr + " AND id=" + id + ";";

            int rowCount = SelComm(sqlTask).Count;

            if (rowCount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        public int get_kleinste_kw(int jahr)
        // Liefert die kleinste vorhandene KW eines angegebenen Jahres
        {
            string sqlTask = " SELECT kw FROM archiv where jahr=" + jahr + " ORDER BY kw";
            int res = -1;

            List<List<string>> resultSet = SelComm(sqlTask);

            if (resultSet.Count > 0)
            {
                res = Int32.Parse(SelComm(sqlTask)[0][0]);
            }

            return res;
        }

        public int get_groesste_kw(int jahr)
        // Liefert die größte vorhandede KW eines angegebenen Jahres
        {
            string sqlTask = " SELECT kw FROM archiv where jahr=" + jahr + " ORDER BY kw DESC";
            int res = -1;

            List<List<string>> resultSet = SelComm(sqlTask);

            if (resultSet.Count > 0)
            {
                res = Int32.Parse(SelComm(sqlTask)[0][0]);
            }

            return res;
        }

        public int get_next_id()
        {
            string sqlTask = "SELECT id FROM mitarbeiter ORDER BY id DESC;";

            List<List<string>> resultSet = SelComm(sqlTask);

            return Int32.Parse(resultSet[0][0]) + 1;
        }

        public bool check_user_color(int id)
        // ungenutzte Funktion 
        {
            string sqlTask = "SELECT * FROM user_settings WHERE id=" + id;

            List<List<string>> resultSet = SelComm(sqlTask);

            if (resultSet.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<string> get_user_color(int id)
        // ungenutzte Funktion
        {
            string sqlTask = "SELECT * From user_settings WHERE id=" + id;

            List<List<string>> resultSet = SelComm(sqlTask);

            if (resultSet.Count > 0)
            {
                return resultSet[0];
            }
            else
            {
                return new List<string>(); ;
            }
        }

        public void set_user_color(int id, string too, string equal, string less, string not)
        // ungenutzte Funktion
        {
            bool exists = check_user_color(id);
            string sqlTask = "";

            if (exists)
            {
                sqlTask = "UPDATE color_settings SET too='" + too + "', equal='" + equal + "' less='" + less + "', not='" + not + "' WHERE id=" + id + ";";
            }
            else
            {
                sqlTask = "INSERT INTO color_settings (too, equal, less, not) VALUES ('" + too + "', '" + equal + "', '" + less + "', '" + not + "';";
            }
            MessageBox.Show(sqlTask);
        }

        public bool get_ma_state(int id)
        {
            string sqlTask = "SELECT active FROM mitarbeiter WHERE id=" + id + ";";
            string res = SelComm(sqlTask)[0][0];


            if (res != "0")
            {
                return true;
            }
            else
            {
                return false;
            }            
        }

        public void update_archiv_zeiten_by_kw(int kw, List<List<string>> zeiten)
        {

        }
    }
}
