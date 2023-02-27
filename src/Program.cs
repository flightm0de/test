using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace GleitzeitControlPanel
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }

        public static List<List<string>> get_kw_data_from_excel(string jahr, int kw = 0)
        {
            //get current in useing excel
            List<List<string>> result = new List<List<string>>();
            Process[] excelProcsOld = Process.GetProcessesByName("EXCEL");
            try
            {
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\vital\Desktop\Check Stundenschreibung " + jahr + ".xlsx", 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
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
                        //MessageBox.Show(xlWorksheet.Cells[col, row].Value.ToString() + " ### " + row.ToString());
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

                for (int i = 3; i <= xlRange.Rows.Count; i++)
                {

                    result.Add(new List<string> {
                                              xlWorksheet.Cells[i, 1].Value.ToString(),
                                              xlWorksheet.Cells[i, (indexFirstKW + kw)].Value.ToString(),
                                              xlWorksheet.Cells[i, 6].Value.ToString(),
                                              xlWorksheet.Cells[i, 3].Value.ToString(),
                                              xlWorksheet.Cells[i, 4].Value.ToString(),
                                              xlWorksheet.Cells[i, 5].Value.ToString()
                                            });
                }
                MessageBox.Show("Daten erfolgreich für die gewählte KW & Jahr importiert");
                //xlWorkbook.Save();
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                MessageBox.Show("Excel Tabelle konnte nicht gefunden werden");
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
            {
                MessageBox.Show("In der Excel konnten keine Zeiten\nfür die gewählte KW & Jahr gefunden werden");
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
            return result;
        }
    }
}
