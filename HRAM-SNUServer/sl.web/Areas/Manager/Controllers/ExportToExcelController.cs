using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.UI;
using sl.model;
using PetaPoco;
using System.Reflection;
using System.Data;
using sl.extension;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.Text;
using sl.service.manager;

namespace sl.web.Areas.Manager.Controllers
{
    public class ExportToExcelController : Controller
    {
        //
        // GET: /Manager/ExportToExcel/
        public ActionResult ToExcelView()
        {
            return View();
        }

        private List<T_User> GetCoaches()
        {
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_User");
            List<T_User> users = HRAManagerService.database.Fetch<T_User>(sql);
            return users;
        }

        public void TestExcelWrite()
        {
            ExportExcel(DTExtensions.ToDataTable(GetCoaches()), "123");
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

        public void ExportExcel(DataTable dt, string fileName = "")
        {
            //生成Excel
            IWorkbook book = BuildWorkbook(dt);

            //web 下载
            if (fileName == "")
                fileName = string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now);
            fileName = fileName.Trim();
            string ext = Path.GetExtension(fileName);

            if (ext.ToLower() == ".xls" || ext.ToLower() == ".xlsx")
                fileName = fileName.Replace(ext, string.Empty);

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = Encoding.UTF8.BodyName;
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName + ".xls");
            Response.ContentEncoding = Encoding.UTF8;
            Response.ContentType = "application/vnd.ms-excel; charset=UTF-8";
            book.Write(Response.OutputStream);
            Response.End();
        }



        public void DataTableToExcel(DataTable datas, string p)
        {

            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.SheetsInNewWorkbook = 1;
            app.Workbooks.Add();
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)app.ActiveWorkbook.Worksheets[1];

            for (int i = 0; i < datas.Columns.Count; i++)
            {
                sheet.Cells[1, i + 1] = datas.Columns[i].ColumnName;
            }

            for (int i = 0; i < datas.Rows.Count; i++)
            {
                for (int j = 0; j < datas.Columns.Count; j++)
                {
                    sheet.Cells[2 + i, j + 1] = datas.Rows[i][j].ToString();
                }
            }

            app.Visible = true;
            System.Threading.Thread.Sleep(500);
            try
            {
                app.ActiveWorkbook.SaveAs(p);
            }
            catch { }
            app.Quit();
        }

        public void ExportClientsListToExcel()
        {
            var grid = new System.Web.UI.WebControls.GridView();

            grid.DataSource = from item in GetCoaches()
                              select new
                              {
                                  编号 = item.pk_id,
                                  登录名 = item.U_LoginName,
                                  登陆密码 = item.U_Password,
                                  用户名 = item.U_UserName,
                                  手机号 = item.U_Phone,
                                  邮箱 = item.U_MaiBox
                              };

            grid.DataBind();
            string FileName = "123";

            Response.ClearContent();
            Response.AppendHeader("content-disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");


            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        /// <summary>
        /// Excle导出数据
        /// </summary>
        /// <typeparam name="T">类对象</typeparam>
        /// <param name="list">对象数据</param>
        /// <param name="column">类字段，字段对应列名</param>
        /// <param name="filename">excel表名</param>

        public void OutExcel<T>(List<T> list, Dictionary<string, string> column, string filename)
        {
            if (list == null || list.Count == 0 || column == null || column.Count == 0)
            {
                return;
            }
            StringWriter sw = new StringWriter();
            //-------------------------------表头读取开始------------------------------------------------
            string title = string.Empty;
            foreach (KeyValuePair<string, string> kvp in column)
            {
                title += kvp.Value + "/t";
            }

            title = title.Substring(0, title.LastIndexOf("/t"));
            sw.WriteLine(title);
            //-------------------------------表头读取结束--------------------------------------------------------

            //--------------------------------数据读取start----------------------------------------------------------------------------------
            Type objType = typeof(T);
            BindingFlags bf = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;//反射标识 
            PropertyInfo[] propInfoArr = objType.GetProperties(bf); //获取映射列表
            foreach (T model in list)
            {
                System.Text.StringBuilder data = new System.Text.StringBuilder();
                foreach (string key in column.Keys)
                {
                    foreach (PropertyInfo propInfo in propInfoArr)
                    {
                        if (key == propInfo.Name)//判断头相对应的字段 
                        {
                            PropertyInfo modelProperty = model.GetType().GetProperty(propInfo.Name);
                            if (modelProperty != null)
                            {
                                object objResult = modelProperty.GetValue(model, null);//获取值                        
                                data.Append(((objResult == null) ? string.Empty : objResult) + "/t");
                            }
                        }
                    }
                }
                var temp = data.ToString();
                temp = temp.Substring(0, temp.LastIndexOf("/t"));
                sw.WriteLine(temp);
            }

            //------------------------------------------end----------------------------------------------------------------------------------
            sw.Close();//读取数据结束
            //-----------------------------------输出excel-------------------------------------------------------------
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GBK");
            Response.Write(sw.ToString());
            Response.End();
            //-------------------------------------------------------------------------------------------------------------               

        }

        //public void ExportClientsListToExcel()
        //{
        //    //接收需要导出的数据
        //    List<T_User> list = GetCoaches();
        //    //命名导出表格的StringBuilder变量
        //    StringBuilder sHtml = new StringBuilder(string.Empty);
        //    //打印表头
        //    sHtml.Append("<table border=\"1\" width=\"100%\">");
        //    sHtml.Append("<tr height=\"40\"><td colspan=\"8\" align=\"center\" style='font-size:24px'><b>大客户部通讯录" + "</b></td></tr>");
        //    //打印列名
        //    sHtml.Append("<tr height=\"20\" align=\"center\" ><td>员工号</td><td>大客户经理</td><td>联系方式</td><td>职务</td><td>行业</td>"
        //        + "<td>省份</td><td>负责区域</td><td>直属经理</td></tr>");
        //    //循环读取List集合 
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        sHtml.Append("<tr height=\"20\" align=\"left\"><td>" + list[i].pk_id
        //            + "</td><td>" + list[i].U_LoginName + "</td><td>" + list[i].U_LoginTypeID + "</td><td>" + list[i].U_Password
        //            + "</td><td>" + list[i].U_UserName + "</td><td>" + list[i].U_Phone + "</td><td>" + list[i].U_MaiBox
        //            + "</td><td>" + list[i].IsDeleted
        //            + "</td></tr>");
        //    }
        //    //打印表尾
        //    sHtml.Append("</table>");
        //    //调用输出Excel表的方法
        //    ExportToExcel("application/ms-excel", "大客户部通讯录.xls", sHtml.ToString());
        //}
        //public void ExportToExcel(string FileType, string FileName, string ExcelContent)
        //{
        //    System.Web.HttpContext.Current.Response.Charset = "UTF-8";
        //    System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        //    System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8).ToString());
        //    System.Web.HttpContext.Current.Response.ContentType = FileType;
        //    System.IO.StringWriter tw = new System.IO.StringWriter();
        //    System.Web.HttpContext.Current.Response.Output.Write(ExcelContent.ToString());
        //    System.Web.HttpContext.Current.Response.Flush();
        //    System.Web.HttpContext.Current.Response.End();
        //}
    }
}