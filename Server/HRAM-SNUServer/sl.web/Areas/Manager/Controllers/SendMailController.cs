using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net.Mail;
using SEWC_NetDevLib.SEWC_NetLibExtend;
using System.IO;
using System.Net;
using System.Text;

namespace sl.web.Areas.Manager.Controllers
{
    public class SendMailController : Controller
    {
        //
        // GET: /Manager/SendMail/
        public ActionResult SendView()
        {
            return View();
        }

        public void SendMail()
        {
            Dictionary<string, string> Paras = new Dictionary<string, string>();
            Paras.Add("uUserName", "123");
            Paras.Add("DefaultPwd", "234");
            string MailContent = GetMailContent("Registration.htm", Paras);
            NetHelper.SendEmail("admin@cherry-cafeteria.com", "admin@cherry-cafeteria.com", "Jeff@875057501", "ccc.surpass@qq.com", "Welcome to the Cafeteria", MailContent, false, "smtp.cherry-cafeteria.com");
        }

        public string GetMailContent(string TemplateName, Dictionary<string, string> Paras)
        {
            string TemplateContent = string.Empty;

            try
            {
                string TemplatePath = System.Web.HttpContext.Current.Server.MapPath(@"~\Html\" + TemplateName);

                StreamReader Steam = new StreamReader(TemplatePath, System.Text.Encoding.Unicode);
                TemplateContent = Steam.ReadToEnd();
                Steam.Close();

                foreach (KeyValuePair<string, string> CurPara in Paras)
                {
                    TemplateContent = TemplateContent.Replace("$$." + CurPara.Key, CurPara.Value);
                }

                return TemplateContent;
            }
            catch (Exception ex)
            {
                EventlogHelper.AddLog(ex.Message);
                return TemplateContent;
            }
        }

        public string SendMailByQQ()
        {
            MailMessage msg = new MailMessage();

            msg.To.Add("862573026@qq.com");//收件人地址  
            //msg.CC.Add("@qq.com");//抄送人地址  

            msg.From = new MailAddress("862573026@qq.com", "SL");//发件人邮箱，名称  

            msg.Subject = "以下内容为系统自动发送，请勿直接回复，谢谢。";//邮件标题  
            msg.SubjectEncoding = Encoding.UTF8;//标题格式为UTF8  

            msg.Body = "以下内容为系统自动发送，请勿直接回复，谢谢。";//邮件内容  
           
            msg.BodyEncoding = Encoding.UTF8;//内容格式为UTF8  

            SmtpClient client = new SmtpClient();

            client.Host = "smtp.qq.com";//SMTP服务器地址  
            client.Port = 587;//SMTP端口，QQ邮箱填写587  

            client.EnableSsl = true;//启用SSL加密  

            client.Credentials = new NetworkCredential("862573026@qq.com", "garjmjzdetmdbeje");//发件人邮箱账号，密码(是打开pop3/smtp提示的那个复制过来)

            client.Send(msg);//发送邮件  

            return "发送成功";
        }


        public string SendMailLocalhost()
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.To.Add("ccc.surpass@qq.com");
            msg.To.Add("b@b.com");
            /* msg.To.Add("b@b.com");  
            * msg.To.Add("b@b.com");  
            * msg.To.Add("b@b.com");可以发送给多人  
            */
            /*  
            * msg.CC.Add("c@c.com");  
            * msg.CC.Add("c@c.com");可以抄送给多人  
            */
            msg.From = new MailAddress("a@a.com", "AlphaWu", System.Text.Encoding.UTF8);
            /* 上面3个参数分别是发件人地址（可以随便写），发件人姓名，编码*/
            msg.Subject = "这是测试邮件";//邮件标题  
            msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码  
            msg.Body = "邮件内容";//邮件内容  
            msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码  
            msg.IsBodyHtml = false;//是否是HTML邮件  
            msg.Priority = MailPriority.High;//邮件优先级 

            SmtpClient client = new SmtpClient();
            client.Host = "localhost:8888";
            object userState = msg;
            string str;
            try
            {
                client.SendAsync(msg, userState);
                //简单一点儿可以client.Send(msg);  
                str = "发送成功";
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                str = "发送邮件出错"+ex.ToString();
            }

            return str;
        }
        //public string SendMailLocalhost()
        //{
        //    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
        //    msg.To.Add("ccc.surpass@qq.com");
        //    msg.To.Add("b@b.com");
        //    /* msg.To.Add("b@b.com");  
        //    * msg.To.Add("b@b.com");  
        //    * msg.To.Add("b@b.com");可以发送给多人  
        //    */

        //    /*  
        //    * msg.CC.Add("c@c.com");  
        //    * msg.CC.Add("c@c.com");可以抄送给多人  
        //    */
        //    msg.From = new MailAddress("master@boys90.com", "dulei", System.Text.Encoding.UTF8);
        //    /* 上面3个参数分别是发件人地址（可以随便写），发件人姓名，编码*/
        //    msg.Subject = "这是测试邮件";//邮件标题  
        //    msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码  
        //    msg.Body = "邮件内容";//邮件内容  
        //    msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码  
        //    msg.IsBodyHtml = false;//是否是HTML邮件  
        //    msg.Priority = MailPriority.High;//邮件优先级 
        //    SmtpClient client = new SmtpClient();
        //    client.Host = "localhost";
        //    object userState = msg;
        //    string str;
        //    try
        //    {
        //        client.SendAsync(msg, userState);
        //        //简单一点儿可以client.Send(msg);  
        //        str = "发送成功";
        //    }
        //    catch (System.Net.Mail.SmtpException ex)
        //    {
        //        str = "发送邮件出错";
        //    }

        //    return str;
        //}
    }
}