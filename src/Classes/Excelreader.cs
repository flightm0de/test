using GleitzeitControlPanel.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace GleitzeitControlPanel.Classes
{
    internal static class Excelreader
    {
        /*
         
        Statische Klasse zum Auslesen von Stundenschreibungen im Excel Format.
        Die Excel Datei muss immer 7 Spalten vor der ersten KW Spalte haben.
        Spalte 1: Nachname + Vorname
        Spalte 2: Ungenutzt aber Pflicht damit der Code funktioniert (ehemalig Passwort-Spalte)
        Spalte 3: Vertragliche Wochenstd.
        Spalte 4: max. Überstunden / Woche
        Spalte 5: max. Gesamtüberstunden
        Spalte 6: Gleitzeitkonto-Stand
        Spalte 7: Start -> Dient als "Markierung" dafür, dass die folgende Spalte die erste KW ist. 
          
        */
        /*public static List<List<string>> get_excel_data(string jahr)
        {
            List<List<string>> result = new List<List<string>>();
            result.Add(new List<string>());
            Process[] excelProcsOld = Process.GetProcessesByName("EXCEL");

            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;

            int currKW = cal.GetWeekOfYear(DateTime.Today.AddDays(-7), dfi.CalendarWeekRule, dfi.FirstDayOfWeek);

            try
            {
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\vital\Desktop\Check Stundenschreibung " + jahr + ".xlsx", 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                //Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\vital\Desktop\Check Stundenschreibung " + jahr + ".xlsx", 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                Excel._Worksheet xlWorksheet = (Excel._Worksheet)xlWorkbook.Sheets[1];
                Excel.Range xlRange = xlWorksheet.UsedRange;

                int indexFirstKW = -1;

                for (int row = 1; row < xlRange.Rows.Count; row++)
                {
                    if (row > 1 && row < 7)
                    {
                        row = 7;
                    }
                    try
                    {
                        if (xlWorksheet.Cells[2, row].Value.ToString() == "Start")
                        {
                            indexFirstKW = row;
                            break;
                        }
                    }
                    catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                    {
                        //MessageBox.Show("NULL");
                    }
                }

                for (int i = 3; i <= xlRange.Rows.Count; i++) // 1 - Name | 3 - Max. Stunden / Woche | 4 - max. Überstd / Woche | 5 - max. Stand | 6 - Stand
                {
                    result[result.Count - 1].Add(xlWorksheet.Cells[i, 1].Value.ToString());
                    result[result.Count - 1].Add(xlWorksheet.Cells[i, 3].Value.ToString());
                    result[result.Count - 1].Add(xlWorksheet.Cells[i, 4].Value.ToString());
                    result[result.Count - 1].Add(xlWorksheet.Cells[i, 5].Value.ToString());
                    result[result.Count - 1].Add(xlWorksheet.Cells[i, 6].Value.ToString());

                    try
                    {
                        for (int e = indexFirstKW + 1; e <= currKW + indexFirstKW; e++)
                        {
                            result[result.Count - 1].Add(xlWorksheet.Cells[i, e].Value.ToString());
                            //MessageBox.Show("e: " + Convert.ToString(e-7) + " ### " + xlWorksheet.Cells[i, e].Value.ToString());
                        }
                    }
                    catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                    {
                        // KW Spalte mit leerem Feld -> "nichts machen"
                    }


                    result.Add(new List<string>());
                }

            }
            catch (System.Runtime.InteropServices.COMException)
            {
                MessageBox.Show("Excel für das Jahr " + jahr + " konnte nicht gefunden werden");
            }
            finally
            {
                //Compare the EXCEL ID and Kill it 
                Process[] excelProcsNew = Process.GetProcessesByName("EXCEL");
                foreach (Process procNew in excelProcsNew)
                {
                    int exist = 0;
                    foreach (Process procOld in excelProcsOld)
                    {
                        if (procNew.Id == procOld.Id)
                        {
                            exist++;
                        }
                    }
                    if (exist == 0)
                    {
                        procNew.Kill();
                    }
                }
            }

            result.RemoveAt(result.Count - 1);

            return result;
        }*/

        public static List<List<string>> get_excel_data(string jahr)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"I:\Devel_DB\Include\Check Stundenschreibung " + jahr + ".xlsx", 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            //Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\vital\Desktop\Check Stundenschreibung " + jahr + ".xlsx", 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Excel._Worksheet xlWorksheet = (Excel._Worksheet)xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            Process[] excelProcsOld = Process.GetProcessesByName("Microsoft Excel");

            //new Thread(() => new Waitwindow().ShowDialog()).Start();
            //Waitwindow frmPgBar = new Waitwindow();
            //frmPgBar.ShowDialog();

            List<List<string>> exceldata = new List<List<string>>();
            for (int i = 3; i < xlRange.Rows.Count + 1; i++)
            {
                Application.DoEvents();
                List<string> output = new List<string>();
                for (int e = 1; e < xlRange.Columns.Count + 1; e++)
                {
                    try
                    {
                        output.Add(xlWorksheet.Cells[i, e].Value.ToString()); // INDEX 5 Stand / INDEX 7 KW: 1
                    }
                    catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                    {
                        output.Add("NULL");
                    }
                }
                exceldata.Add(output);
            }

            // EXCEL Prozess anhand der ID beenden

            Process[] excelProcsNew = Process.GetProcessesByName("Microsoft Excel");
            foreach (Process procNew in excelProcsNew)
            {
                int exist = 0;
                foreach (Process procOld in excelProcsOld)
                {
                    if (procNew.Id == procOld.Id)
                    {
                        exist++;
                    }
                }
                if (exist == 0)
                {
                    procNew.Kill();
                }
            }

            //frmPgBar.Close();

            return exceldata;
        }
    }
}
