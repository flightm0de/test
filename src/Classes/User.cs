using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GleitzeitControlPanel.Classes
{
    internal class User
    // User Klasse die alle Nutzerinformationen beinhaltet
    {
        public string id { get; set; }
        public string vorname { get; set; }
        public string name { get; set; }
        public string displayname { get; set; }
        public string maxwoche { get; set; }
        public string maxgleitzeit { get; set; }
        public string wochenstd { get; set; }
        public string active { get; set; }
        public string stand { get; set; }
        public List<List<string>> stand_zeiten { get; set; }

        public List<List<string>> zeiten;
        // Diese Liste enthält alle von dem User in der Datenbank zugewiesenen Zeiten als Unterlisten
        // Die Unterlisten können per Index angesprochen werden und liefern diese Werte IMMER als string zurück.
        // 0 - kw | 1 - zeit | 2 - jahr

        public User(string id, string vorname, string name, string displayname, string maxwoche, string maxgleitzeit, string wochenstd, string active)
        {
            this.id = id;
            this.vorname = vorname;
            this.name = name;
            this.displayname = displayname;
            this.maxwoche = maxwoche;
            this.maxgleitzeit = maxgleitzeit;
            this.wochenstd = wochenstd;
            this.active = active;
            this.stand = "";
            this.stand_zeiten = new List<List<string>>();
            this.zeiten = new List<List<string>>();
        }

        public void add_stand(string jahr, string stand)
        {
            this.stand_zeiten.Add(new List<string> { jahr, stand });
        }

        public void change_stand_zeit(string jahr, string stand)
        {
            foreach (List<string> eintrag in this.stand_zeiten)
            {
                if (eintrag[0] == jahr)
                {
                    eintrag[1] = stand;
                }
            }
        }

        public string get_stand(string jahr)
        {
            foreach (List<string> eintrag in this.stand_zeiten)
            {
                if (eintrag[0] == jahr)
                {
                    return eintrag[1];
                }
            }

            return "undefined";
        }

        public void addZeit(string kw, string zeit, string jahr) 
        // Fügt einen Zeiteintrag in die zeiten Liste hinzu
        {
            List<string> eintrag = new List<string> { kw, zeit, jahr};
            zeiten.Add(eintrag);
        }

        public void change_zeit(string kw, string zeit, string jahr)
        {
            foreach (List<string> eintrag in this.zeiten)
            {
                if (eintrag[0] == kw && eintrag[2] == jahr)
                {
                    eintrag[1] = zeit;
                }
            }
        }

        public string get_name_by_displayname(string displayname)
        {
            if (displayname == this.displayname)
            {
                return this.name;
            }
            else
            {
                return "not_found";
            }
        }

        public string getZeitByKwAndJahr(string kw, string jahr)
        // Liefert einen Zeiteneintrag (Unterliste) aus der zeiten Liste anhand einer gegebenen KW & Jahr
        {
            foreach(List<string> eintrag in zeiten)
            {
                if (eintrag[0] == kw && eintrag[2] == jahr)
                {
                    return eintrag[1];
                }
            }

            return "not_existent";
        }
    }
}
