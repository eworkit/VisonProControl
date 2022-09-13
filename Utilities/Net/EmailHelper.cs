using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Utilities.Net
{
    public class EmailHelper
    {
        public static void SendEmail(string title, string text,string[] Files=null,string[] Imgs=null)
        {
            SmtpClient client = new SmtpClient();
            //这个地方的用户名你可以用split从发送人中截取
            client.Credentials = new System.Net.NetworkCredential("itetest@hotmail.com", Properties.Resources.String1);
            client.Port = 25;
            client.Host = "smtp.live.com";
            client.EnableSsl = true;
            MailMessage m_Mail = new MailMessage();
            //发件人
            m_Mail.From = new MailAddress("itetest@hotmail.com");
            //收件人
            m_Mail.To.Add(new MailAddress("svtem@hotmail.com"));
            //主题
            m_Mail.Subject = title;
            //内容
            m_Mail.Body = text;
            m_Mail.IsBodyHtml = true;
            //邮件主题和正文编码格式
            m_Mail.SubjectEncoding = System.Text.Encoding.UTF8;
            m_Mail.BodyEncoding = System.Text.Encoding.UTF8;
            //邮件正文是Html编码
            m_Mail.IsBodyHtml = true;
            //优先级
            m_Mail.Priority = System.Net.Mail.MailPriority.High;
            //添加附件,可以添加多个
            //m_Mail.Attachments.Add(new Attachment("f:\\1.txt"));
            //密件抄送收件人
            m_Mail.Bcc.Add("svtem@hotmail.com");
            //抄送收件人
            m_Mail.CC.Add("svtem@hotmail.com");

             
            if (Files !=null)
            {
                foreach (string file in Files)
                {
                    System.Net.Mail.Attachment attachment1 = new System.Net.Mail.Attachment(file);//添加附件 
                    attachment1.Name = System.IO.Path.GetFileName(file);
                    attachment1.NameEncoding = System.Text.Encoding.GetEncoding("gb2312");
                    attachment1.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                    attachment1.ContentDisposition.Inline = true;
                    attachment1.ContentDisposition.DispositionType = System.Net.Mime.DispositionTypeNames.Inline;
                    string cid = attachment1.ContentId;//关键性的地方，这里得到一个id数值 
                    m_Mail.Attachments.Add(attachment1);
                  //  m_Mail.Body += "<table width='100%'><tr><td><img src ='cid:" + cid + "'/></td></tr>";
                }
            }
            if(Imgs!=null)
            {
                foreach (string file in Imgs)
                {
                    System.Net.Mail.Attachment attachment1 = new System.Net.Mail.Attachment(file);//添加附件 
                    attachment1.Name = System.IO.Path.GetFileName(file);
                    attachment1.NameEncoding = System.Text.Encoding.GetEncoding("gb2312");
                    attachment1.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                    attachment1.ContentDisposition.Inline = true;
                    attachment1.ContentDisposition.DispositionType = System.Net.Mime.DispositionTypeNames.Inline;
                    string cid = attachment1.ContentId;//关键性的地方，这里得到一个id数值 
                    m_Mail.Attachments.Add(attachment1);
                     m_Mail.Body += "<table width='100%'><tr><td><img src ='cid:" + cid + "'/></td></tr>";
                }
            }
            client.Send(m_Mail);
        }
    } 
}
