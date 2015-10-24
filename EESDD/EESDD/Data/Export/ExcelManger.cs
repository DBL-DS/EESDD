using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Reflection;

namespace EESDD.Data.Export
{
    class ExcelManger
    {
        private Application app;
        private Workbooks wbks;
        private _Workbook _wbk;
        private Sheets shs;
        private _Worksheet currentWorksheet;
        private int currentSheet = 1;
        //private string templateFilePath;
        private string templateFilePath = System.IO.Directory.GetCurrentDirectory() + "\\data\\export\\template\\template.xlsx";
        private string targetFilePath;
        private string fileName;

        public ExcelManger(string fileName)
        {
            app = new Application();
            wbks = app.Workbooks;
            this.fileName = fileName;
            targetFilePath = System.IO.Directory.GetCurrentDirectory() + "\\data\\export\\" + fileName;
        }

        public void setTemplateFilePath(string filePath)
        {
            this.templateFilePath = filePath;
        }
        public void setTargetFilePath(string filePath)
        {
            this.targetFilePath = filePath;
        }
        public bool openTemplate()
        {
            if (File.Exists(templateFilePath))
            {
                _wbk = wbks.Add(templateFilePath);
                shs = _wbk.Sheets;
                return true;
            }

            return false;
        }

        public bool copyTemplate()
        {
            if (File.Exists(templateFilePath))
            {
                FileInfo file = new FileInfo(templateFilePath);
                file.CopyTo(targetFilePath);
                return true;
            }
            return false;
        }

        public void openTargetExcel()
        {
            _wbk = wbks.Add(targetFilePath);
            shs = _wbk.Sheets;
        }

        public _Worksheet createNewSheet(string sheetName)
        {
            _Worksheet _wsh = shs.get_Item(currentSheet++);
            _wsh.Name = sheetName;

            currentWorksheet = _wsh;
            return _wsh;
        }

        public void setCellValue(int row, int column, string value)
        {
            currentWorksheet.Cells[row, column] = value;
        }

        public void setCellImage(int row, int column, string imagePath)
        {
            
        }

        public void saveFileAndExit()
        {
            //app.DisplayAlerts = false;
            app.AlertBeforeOverwriting = false;
            _wbk.SaveAs(targetFilePath);

            app.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
            app = null;
        }
    }
}
