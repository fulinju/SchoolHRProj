using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Data;

namespace common.utils
{
    public class WorkbookUtils
    {
        /// <summary>
        /// 获得标题
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static XSSFWorkbook BuildWorkbookTitle(DataRow title)
        {
            var book = new XSSFWorkbook();
            ISheet sheet = book.CreateSheet("Sheet1");
            //Data Rows
            IRow drow = sheet.CreateRow(0);

            for (int i = 0; i < title.ItemArray.Length; i++)
            {
                ICell cell = drow.CreateCell(i, CellType.String);
                cell.SetCellValue(title[i].ToString());
            }

            //自动列宽
            for (int i = 0; i <= title.ItemArray.Length; i++)
            {
                sheet.AutoSizeColumn(i, true);
            }

            return book;
        }

        //高版本
        public static XSSFWorkbook BuildWorkbook(DataTable dt)
        {
            var book = new XSSFWorkbook();
            ISheet sheet = book.CreateSheet("Sheet1");

            //Data Rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow drow = sheet.CreateRow(i);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = drow.CreateCell(j, CellType.String);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }
            //自动列宽
            for (int i = 0; i <= dt.Columns.Count; i++)
                sheet.AutoSizeColumn(i, true);

            return book;
        }

        /// <summary>
        /// 获得表格的文件名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetExcelFileName(string fileName = "")
        {
            //web 下载
            if (fileName == "")
                fileName = string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now);
            fileName = fileName.Trim();
            string ext = Path.GetExtension(fileName);

            if (ext.ToLower() == ".xls" || ext.ToLower() == ".xlsx")
            {
                fileName = fileName.Replace(ext, string.Empty);
            }
            return fileName;
        }

    }
}