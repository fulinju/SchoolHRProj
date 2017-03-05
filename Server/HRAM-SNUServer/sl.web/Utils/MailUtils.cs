using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net.Mail;
using System.IO;
using System.Net;
using System.Text;

namespace common.utils
{
    public class MailUtils
    {

        public static string SendMailByQQ(string to,string senderName,string emailSubject, string emailBody,string successMsg,string failedMsg)
        {
            try
            {
                MailMessage msg = new MailMessage();

                msg.To.Add(to);//收件人地址  
                //msg.CC.Add("@qq.com");//抄送人地址  

                msg.From = new MailAddress("862573026@qq.com", senderName);//发件人邮箱，名称可定死  

                msg.Subject = emailSubject;//邮件标题  
                msg.SubjectEncoding = Encoding.UTF8;//标题格式为UTF8  

                msg.Body = emailBody;//邮件内容  
                msg.BodyEncoding = Encoding.UTF8;//内容格式为UTF8  

                SmtpClient client = new SmtpClient();

                client.Host = "smtp.qq.com";//SMTP服务器地址  
                client.Port = 587;//SMTP端口，QQ邮箱填写587  

                client.EnableSsl = true;//启用SSL加密  

                client.Credentials = new NetworkCredential("862573026@qq.com", "garjmjzdetmdbeje");//发件人邮箱账号，密码(是打开pop3/smtp提示的那个复制过来)

                client.Send(msg);//发送邮件  

                return successMsg;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return failedMsg;
            }
           

        }
    }
}