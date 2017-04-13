using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;


namespace CompetitionDog
{
    class ReportExcel
    {
        private DataTable dt = null;
        private string filename;
        Dictionary<string, string> headName = null;
        private Excel.Worksheet workSheet = null;

        public ReportExcel(DataTable dt, string filename, Dictionary<string, string> headName)
        {
            this.dt = dt;
            this.filename = filename;
            this.headName = headName;
            report();
        }

        private void setHeaderName()
        {
            foreach (KeyValuePair<string, string> pair in headName)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    if (pair.Key == dt.Columns[k].ColumnName)
                    {
                        workSheet.Cells[1, k + 1] = pair.Value;
                    }
                }
            }
        }

        void report()
        {
            Excel.Application exApp = new Excel.Application();
            Excel.Workbook workbook = exApp.Workbooks.Add();
            workSheet=workbook.ActiveSheet;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    workSheet.Cells[i + 2, j + 1] = dt.Rows[i][j].ToString();
                }
            }
            setHeaderName();
            workSheet.Columns.AutoFit();
            workbook.SaveAs(filename);
            workbook.Close();
        }
    }
}
