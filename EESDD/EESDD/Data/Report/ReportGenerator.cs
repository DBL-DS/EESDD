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

        public ReportGenerator()
        {
            kaiFont = BaseFont.CreateFont(DirectoryDef.KaiFontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            titleFont = new Font(kaiFont, 24, Font.BOLD);
            contentFont = new Font(kaiFont, 12, Font.NORMAL);
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
            table.TotalWidth = 213f;

            table.SpacingBefore = 20f;
            table.HorizontalAlignment = 1;
            

            PdfPCell loginName = new PdfPCell(new Phrase("编号：" + user.LoginName, contentFont));
            loginName.Colspan = 4;
            loginName.HorizontalAlignment = 2;
            loginName.Padding = 5;
            loginName.PaddingRight = 20;
            table.AddCell(loginName);

            table.AddCell(new Phrase(TextDef.CNRealName, contentFont));
            table.AddCell(new Phrase(user.RealName, contentFont));
            table.AddCell(new Phrase(TextDef.CNGender, contentFont));
            table.AddCell(new Phrase(user.Gender, contentFont));
            table.AddCell(new Phrase(TextDef.CNHeight, contentFont));
            table.AddCell(new Phrase(user.Height + "", contentFont));
            table.AddCell(new Phrase(TextDef.CNWeight, contentFont));
            table.AddCell(new Phrase(user.Weight + "", contentFont));
            table.AddCell(new Phrase(TextDef.CNAge, contentFont));
            table.AddCell(new Phrase(user.Age + "", contentFont));
            table.AddCell(new Phrase(TextDef.CNDrivingAge, contentFont));
            table.AddCell(new Phrase(user.DrivingAge + "", contentFont));
            table.AddCell(new Phrase(TextDef.CNContact, contentFont));
            table.AddCell(new Phrase(user.Contact + "", contentFont));

            PdfPCell empty = new PdfPCell(new Phrase("", contentFont));
            empty.Colspan = 2;
            table.AddCell(empty);

            
            document.Add(table);
        }

        private void setImages()
        {
            PdfPTable table = new PdfPTable(3);
            table.TotalWidth = 213f;
            table.SetWidths(new float[]{13f, 100f, 100f});

            // 柱状图  跟驰刹车场景-刹车阶段   应对刹车反应时
            // 柱状图  前车并线场景           应对前车并线反应时
            PdfPCell rowTitle1 = new PdfPCell(new Phrase("行车安全性", contentFont));
            rowTitle1.PaddingLeft = rowTitle1.PaddingRight = 5f;
            rowTitle1.MinimumHeight = 150f;
            rowTitle1.HorizontalAlignment = 1;
            rowTitle1.PaddingTop = 40f;
            table.AddCell(rowTitle1);
            PageList.Evaluation.SaveBarScreenShot(UserSelections.SceneBrake, BarDetailCluster.ReactTime, DirectoryDef.PictureTempPath);
            table.AddCell(Image.GetInstance(DirectoryDef.PictureTempPath));
            PageList.Evaluation.reportProgress(25);
            PageList.Evaluation.SaveBarScreenShot(UserSelections.SceneLaneChange, BarDetailCluster.ReactTime, DirectoryDef.PictureTempPath);
            table.AddCell(Image.GetInstance(DirectoryDef.PictureTempPath));
            PageList.Evaluation.reportProgress(35);

            // 折线图  跟驰刹车场景-跟驰阶段 速度-距离
            // 柱状图  跟弛刹车场景-跟驰阶段 跟驰距离标准差
            PdfPCell rowTitle2 = new PdfPCell(new Phrase("乘客舒适度", contentFont));
            rowTitle2.PaddingLeft = rowTitle2.PaddingRight = 5f;
            rowTitle2.MinimumHeight = 150f;
            rowTitle2.HorizontalAlignment = 1;
            rowTitle2.PaddingTop = 40f;
            table.AddCell(rowTitle2);
            PageList.Evaluation.SaveLineScreenShot(UserSelections.SceneBrake, "Speed", DirectoryDef.PictureTempPath, 1);
            table.AddCell(Image.GetInstance(DirectoryDef.PictureTempPath));
            PageList.Evaluation.reportProgress(45);
            PageList.Evaluation.SaveBarScreenShot(UserSelections.SceneBrake, BarDetailCluster.VarianceDistanceToNext, DirectoryDef.PictureTempPath);
            table.AddCell(Image.GetInstance(DirectoryDef.PictureTempPath));
            PageList.Evaluation.reportProgress(55);

            // 柱状图  路口等灯场景   平均排队长度
            // 柱状图  路口等灯场景   平均延误
            PdfPCell rowTitle3 = new PdfPCell(new Phrase("行车顺畅度", contentFont));
            rowTitle3.PaddingLeft = rowTitle3.PaddingRight = 5f;
            rowTitle3.MinimumHeight = 150f;
            rowTitle3.HorizontalAlignment = 1;
            rowTitle3.PaddingTop = 40f;
            table.AddCell(rowTitle3);
            PageList.Evaluation.SaveBarScreenShot(UserSelections.SceneIntersection, BarDetailCluster.AverageQueueLength, DirectoryDef.PictureTempPath);
            table.AddCell(Image.GetInstance(DirectoryDef.PictureTempPath));
            PageList.Evaluation.reportProgress(65);
            PageList.Evaluation.SaveBarScreenShot(UserSelections.SceneIntersection, BarDetailCluster.AverageDelay, DirectoryDef.PictureTempPath);
            table.AddCell(Image.GetInstance(DirectoryDef.PictureTempPath));
            PageList.Evaluation.reportProgress(75);

            document.Add(table);
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
