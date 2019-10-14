using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NewBlazor.Data
{
    public class SurveyEmailSender
    {
        private const string HtmlBody = @"<html>
<body>
Hello Friend,
<p>
Your are invited to take a survey on <strong>@@title</strong>. Please click the below link to take the survey.
</p>
<p>
@@link
<p/>
Thanks
<br>
SurveyShrike Team
</body>
</html>
";
        public IConfiguration Configuration { get; }
        public SurveyEmailSender(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async Task SendSurvey(SurveyDispatchInfo surveyDispatchInfo)
        {
            foreach (var email in surveyDispatchInfo.RecipientList)
            {
                await SendEmail(PrepareBody(surveyDispatchInfo.SelectedSurvey, surveyDispatchInfo.Title), email);
            }
        }

        private string PrepareBody(Guid surveyId, string title ="")
        {
            return HtmlBody.Replace("@@title", title).Replace("@@link", "http://localhost:16625/surveylink/" + surveyId.ToString());
        }

        private async Task SendEmail(string body, string to)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(Configuration.GetSection("EmailCredentials")["SenderEmail"]);
                message.To.Add(new MailAddress(to));
                message.Subject = "Welcome to SurveyShrike!";
                message.IsBodyHtml = true;
                message.Body = body;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(Configuration.GetSection("EmailCredentials")["SenderEmail"], Configuration.GetSection("EmailCredentials")["SenderPassword"]);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                await smtp.SendMailAsync(message);
            }
            catch (AggregateException ex) { }
        }
    }
}
