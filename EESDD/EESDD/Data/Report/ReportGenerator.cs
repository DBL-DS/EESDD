using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using EESDD.Control.User;
using iTextSharp.text;
using System.IO;
using EESDD.Public;
using EESDD.Control.Operation;
using System.Threading;
using System.Windows.Media;

namespace EESDD.Data.Report
{
    class ReportGenerator
    {
        Document document;
        string savePath;
        PdfWriter writer;
        string errorMessage;
        BaseFont kaiFont;
        Font titleFont;
        Font contentFont;
        Font chartTitleFont;
        float tableWidth;

        public ReportGenerator()
        {
            kaiFont = BaseFont.CreateFont(DirectoryDef.KaiFontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            titleFont = new Font(kaiFont, 24, Font.BOLD);
            contentFont = new Font(kaiFont, 12, Font.NORMAL);
            chartTitleFont = new Font(kaiFont, 8, Font.NORMAL);
            tableWidth = 213f;
        }
        public void generate()
        {
            document = new Document(PageSize.A4);
            savePath = getSavePath();

            if (savePath.Equals(""))
                return;

            try
            {
                writer = PdfWriter.GetInstance(document, new FileStream(savePath, FileMode.Create));
                document.Open();
                setPdfContent();
            }
            catch (DocumentException de)
            {
                this.errorMessage = de.Message;
            }
            catch (IOException ioe)
            {
                this.errorMessage = ioe.Message;
            }

            document.Close();
        }

        private string getSavePath()
        {
            SaveFileDialog s = new SaveFileDialog();
            // 设置文件类型过滤
            s.Filter = " pdf files(*.pdf)|*.pdf|All files(*.*)|*.*";
            User user = PageList.Main.User;
            s.FileName = user.LoginName + "-" + user.RealName + "-分心驾驶评估报告";

            s.RestoreDirectory = true;
            if (s.ShowDialog() == true)
            {
                string localFilePath = s.FileName.ToString();
                return localFilePath;
            }

            return "";
        }

        private void setPdfContent()
        {
            PageList.Evaluation.reportPrinting();
            setTitle();
            PageList.Evaluation.reportProgress(5);
            setUserInfo();
            PageList.Evaluation.reportProgress(15);
            setImageSample();
            setImages();
            PageList.Evaluation.reportProgress(75);
            setComment();
            PageList.Evaluation.reportPrinted();
        }

        private void setTitle()
        {
            Paragraph title = new Paragraph("分心驾驶体验评估报告", titleFont);
            title.Alignment = 1;
            document.Add(title);
        }

        private void setUserInfo()
        {
            User user = PageList.Main.User;
            PdfPTable table = new PdfPTable(4);
            table.TotalWidth = tableWidth;

            table.SpacingBefore = 20f;
            table.HorizontalAlignment = 1;
            

            PdfPCell loginName = new PdfPCell(new Phrase("编号：" + user.LoginName, contentFont));
            loginName.Colspan = 4;
            loginName.HorizontalAlignment = 2;
            loginName.Padding = 5;
            loginName.PaddingRight = 20;
            table.AddCell(loginName);

            addUnitToTable(TextDef.CNRealName, user.RealName, table);
            addUnitToTable(TextDef.CNGender, user.Gender, table);
            addUnitToTable(TextDef.CNHeight, user.Height + "", table);
            addUnitToTable(TextDef.CNWeight, user.Weight + "", table);
            addUnitToTable(TextDef.CNAge, user.Age + "", table);
            addUnitToTable(TextDef.CNDrivingAge, user.DrivingAge + "", table);
            addUnitToTable(TextDef.CNContact, user.Contact, table);

            PdfPCell empty = new PdfPCell(new Phrase("", contentFont));
            empty.Colspan = 2;
            table.AddCell(empty);

            document.Add(table);
        }

        private void addUnitToTable(string attribute, string value, PdfPTable table){
            PdfPCell attrCell = new PdfPCell(new Phrase(attribute, contentFont));
            attrCell.Padding = 3;
            attrCell.PaddingLeft = 10;
            PdfPCell valueCell = new PdfPCell(new Phrase(value, contentFont));
            valueCell.Padding = 3;
            valueCell.PaddingLeft = 10;
            table.AddCell(attrCell);
            table.AddCell(valueCell);
        }

        private void setImageSample()
        {
            List<string> sampleNames = new List<string>() { "正常模式", "微信语音", "微信短信", "调谐收音机", "行车导航" };
            List<Color> sampleColors = new List<Color>() { ColorDef.Normal, ColorDef.DistractA, ColorDef.DistractB, ColorDef.DistractC, ColorDef.DistractD };
            int sampleCounts = sampleNames.Count;

            PdfPTable sampleTable = new PdfPTable(sampleCounts);

            int count = 0;
            foreach (string name in sampleNames)
            {
                PdfPCell cell = new PdfPCell(new Phrase(name, chartTitleFont));
                cell.HorizontalAlignment = 1;

                if (++count == 1)
                    cell.BorderWidthRight = 0;
                else if (count == sampleCounts)
                    cell.BorderWidthLeft = 0;
                else
                    cell.BorderWidthLeft = cell.BorderWidthRight = 0;
                
                sampleTable.AddCell(cell);
            }

            drawSampleColor(100, 647, 5, 83, sampleColors);

            document.Add(sampleTable);
        }

        private void drawSampleColor(double startX, double startY, double length, double interval, List<Color> colors)
        {
            PdfContentByte drawer = writer.DirectContent;

            int count = 0;
            foreach (Color color in colors)
            {
                double x = startX + count * interval;
                drawer.MoveTo(x, startY);
                drawer.Rectangle(x, startY, length, length);

                drawer.SetRGBColorFill(color.R, color.G, color.B);
                drawer.Fill();

                count++;
            }

            drawer.SetRGBColorFill(0, 0, 0);
        }

        private void AddImageSample(PdfPTable table)
        {

        }

        private void setImages()
        {
            PdfPTable table = new PdfPTable(3);
            table.TotalWidth = tableWidth;
            table.SetWidths(new float[]{13f, 100f, 100f});

            Thread screenShotThread = new Thread(PageList.Evaluation.ScreenShotTaker);
            screenShotThread.Start();

            // 柱状图  跟驰刹车场景-刹车阶段   应对刹车反应时
            // 柱状图  前车并线场景           应对前车并线反应时
            AddRowTitle("行车安全性", table);
            AddBarImageToCell(UserSelections.SceneBrake, BarDetailCluster.ReactTime, DirectoryDef.PictureTempPath, table, "应对前车刹车反应时", 25);
            AddBarImageToCell(UserSelections.SceneLaneChange, BarDetailCluster.ReactTime, DirectoryDef.PictureTempPath, table, "应对前车并线反应时", 55);

            // 折线图  跟驰刹车场景-跟驰阶段 速度-距离
            // 柱状图  跟弛刹车场景-跟驰阶段 跟驰距离标准差
            AddRowTitle("乘客舒适度", table);
            AddLineImageToCell(UserSelections.SceneBrake, "Speed", DirectoryDef.PictureTempPath, 1, table, "行车速度曲线 速度-行驶距离", 45);
            AddBarImageToCell(UserSelections.SceneBrake, BarDetailCluster.VarianceDistanceToNext, DirectoryDef.PictureTempPath, table, "跟驰距离标准差", 55);

            // 柱状图  路口等灯场景   平均排队长度
            // 柱状图  路口等灯场景   平均延误
            AddRowTitle("行车顺畅度", table);
            AddBarImageToCell(UserSelections.SceneIntersection, BarDetailCluster.AverageQueueLength, DirectoryDef.PictureTempPath, table, 65);
            AddBarImageToCell(UserSelections.SceneIntersection, BarDetailCluster.AverageDelay, DirectoryDef.PictureTempPath, table, 75);

            screenShotThread.Abort();
            document.Add(table);
        }

        private void AddRowTitle(string title, PdfPTable table)
        {
            PdfPCell rowTitle = new PdfPCell(new Phrase(title, contentFont));
            rowTitle.PaddingLeft = rowTitle.PaddingRight = 5f;
            rowTitle.HorizontalAlignment = 1;
            rowTitle.PaddingTop = 40f;
            rowTitle.MinimumHeight = 150f;
            table.AddCell(rowTitle);
        }

        private void AddBarImageToCell(int scene, BarDetail detail, string filePath, PdfPTable table, string title, int progress)
        {
            AddBarImageToCell(scene, detail.ChangeTitle(title), filePath, table, progress);
        }
        private void AddBarImageToCell(int scene, BarDetail detail, string filePath, PdfPTable table, int progress)
        {
            int waitForFile = 100;

            PageList.Evaluation.RefreshPrintBar(scene, detail, filePath);
            waitImageGenerating();
            Thread.Sleep(waitForFile);
            Image image = Image.GetInstance(filePath);
            PdfPCell imageCell = new PdfPCell(image);
            imageCell.HorizontalAlignment = 1;
            imageCell.PaddingTop = 25;
            table.AddCell(imageCell);
            image.ScaleAbsolute(120, 100);
            PageList.Evaluation.reportProgress(progress);
        }

        private void AddLineImageToCell(int scene, string variable, string filePath, int xAxis, PdfPTable table, string title, int progress)
        {
            int waitForFile = 100;

            PageList.Evaluation.RefreshPrintLine(scene, variable, filePath, xAxis);
            waitImageGenerating();
            Thread.Sleep(waitForFile);
            Image image = Image.GetInstance(filePath);
            PdfPCell imageCell = new PdfPCell(image);
            imageCell.HorizontalAlignment = 1;
            imageCell.PaddingTop = 15;
            imageCell.BorderColor = BaseColor.WHITE;
            image.ScaleAbsolute(180, 100);


            PdfPTable imageUnit = new PdfPTable(1);
            imageUnit.AddCell(imageCell);

            PdfPCell lineTitle = new PdfPCell(new Phrase(title, chartTitleFont));
            lineTitle.HorizontalAlignment = 1;
            lineTitle.BorderColor = BaseColor.WHITE;
            lineTitle.PaddingTop = 5;

            imageUnit.AddCell(lineTitle);

            table.AddCell(imageUnit);
            PageList.Evaluation.reportProgress(progress);
        }

        private void waitImageGenerating()
        {
            ManualResetEvent imageWaitFlag = PageList.Evaluation.generateWaitFlag;
            imageWaitFlag.Reset();
            imageWaitFlag.WaitOne();
        }

        private void setComment()
        {
            PdfPTable table = new PdfPTable(2);
            table.TotalWidth = 213f;
            table.SetWidths(new float[] { 13f, 200f });

            PdfPCell commentTitle = new PdfPCell(new Phrase("评价建议", contentFont));
            commentTitle.PaddingLeft = commentTitle.PaddingRight = 5f;
            commentTitle.MinimumHeight = 150f;
            commentTitle.HorizontalAlignment = 1;
            commentTitle.PaddingTop = 40f;
            table.AddCell(commentTitle);

            PdfPCell comment = new PdfPCell();
            Paragraph paragraph1 = new Paragraph(TextDef.ReportComment, contentFont);
            paragraph1.FirstLineIndent = 25f;
            comment.AddElement(paragraph1);
            comment.AddElement(paragraph1);

            table.AddCell(comment);

            document.Add(table);
        }

        private void generateImages()
        {

        }
    }
}
