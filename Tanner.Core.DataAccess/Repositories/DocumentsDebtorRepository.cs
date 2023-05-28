using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Tanner.Core.API.Interfaces;
using Tanner.Core.API.Model;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Models;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.DataAccess.Repositories
{
    public class DocumentsDebtorRepository : IDocumentsDebtorRepository
    {
        private readonly IDebtorRepository _repository;
        private readonly IAPIManagerClient _aPIManagerClient;
        private readonly TelemetryClient telemetry;
        private string guidId;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="aPIManagerClient"></param>
        public DocumentsDebtorRepository(IDebtorRepository repository, IAPIManagerClient aPIManagerClient, TelemetryClient _telemetry)
        {
            _repository = repository;
            _aPIManagerClient = aPIManagerClient;
            telemetry = _telemetry;
        }

        /// <summary>
        /// Get resource embedded
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static string GetEmbeddedResource(string resourceName, Assembly assembly)
        {
            resourceName = FormatResourceName(assembly, resourceName);
            using (Stream resourceStream = assembly.GetManifestResourceStream(resourceName))
            {
                if (resourceStream == null)
                    return null;

                using (StreamReader reader = new StreamReader(resourceStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Format to resource name
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        private static string FormatResourceName(Assembly assembly, string resourceName)
        {
            return assembly.GetName().Name + "." + resourceName.Replace(" ", "_")
                                                               .Replace("\\", ".")
                                                               .Replace("/", ".");
        }

        /// <summary>
        /// Method that get the debtors and send email
        /// </summary>
        /// <returns></returns>
        public async Task<OperationCollectionResult<DocumentDebtorResource>> GetDebtors()
        {
            List<EmailAddress> list = new List<EmailAddress>();
            EmailAddress emailAddress = new EmailAddress
            {
                To = "felipe.cifuentes@tanner.cl",
                ToName = "Felipe Cifuentes"
            };
            list.Add(emailAddress);

            var body = GetEmbeddedResource("Resources\\CobranzaAutomotriz.html", Assembly.GetExecutingAssembly());
            OperationCollectionResult<DocumentDebtorResource> documents = await _repository.GetDataDocumentsDebtorAsync();

            var result = documents.DataCollection.GroupBy(x => new {x.CompleteDebtorRUT, x.DebtorEmail}).Select(x => x.FirstOrDefault());

            if (result != null)
            {
                IEnumerable<EmailData> emails = result.Select(x => new EmailData { DebtorName = x.SocialReasonDebtor, EmailDebtor = x.DebtorEmail, CompleteDebtorRUT = x.CompleteDebtorRUT });

                Task[] tasks = new Task[emails.Count()];
                int index = 0;
                foreach (var email in emails)
                {
                    IEnumerable<DocumentDebtorResource> documentsDebtor = documents.DataCollection.Where(x => x.CompleteDebtorRUT == email.CompleteDebtorRUT && x.DebtorEmail == email.EmailDebtor);
                    string bodyCurrent = body;
                    bodyCurrent = bodyCurrent.Replace("#SocialReasonDebtor#", email.DebtorName);
                    bodyCurrent = bodyCurrent.Replace("#SystemDate#", DateTime.Now.Date.ToString("dd-MM-yyyy"));

                    string rows = string.Empty;
                    foreach (var item in documentsDebtor)
                    {
                        rows += rutBox.Replace("#CompleteDebtorRUT#", item.ClientRUT)
                            + debtorBox.Replace("#SocialReasonDebtor#", item.SocialReasonClient)
                            + documentBox.Replace("#IdDocument#", item.Folio.ToString())
                            + amountBox.Replace("#DocumentAmount#", item.DocumentAmount)
                            + expiredDateBox.Replace("#ExpiredDate#", item.ExpiredDate.ToString("dd-MM-yyyy"));
                    }
                    bodyCurrent = bodyCurrent.Replace("#Rows#", rows);
                    List<EmailAddress> tosend = new List<EmailAddress>
                    {
                        new EmailAddress { To = email.EmailDebtor, ToName = email.DebtorName }
                    };
                    telemetry.TrackTrace($"Se envía el correo a los deudores: [{tosend}]");
                    //descomentar cuando haya que enviar los correos a los deudores                    
                    tasks[index] = IsSended(bodyCurrent, tosend);

                    //tasks[index] = IsSended(bodyCurrent, list);
                    index++;
                }
                await Task.WhenAll(tasks);
            }           
            return documents;
        }

        public async Task<bool> IsSended(string body, List<EmailAddress> toaddress)
        {
            var emailRequest = new EmailRequest
            {
                FromName = "Cobranza Automotriz",
                CcAddress = new List<EmailAddress>(),
                CcoAddress = new List<EmailAddress>(),
                Attachments = new List<Attachment>(),
                ToAddress = toaddress,
                Body = body,
                IsBodyHtml = true,
                Subject = "Aviso Tanner - documentos por vencer"

            };
            bool response = await _aPIManagerClient.SendEmailAsync(emailRequest);
            telemetry.TrackTrace($"El resultado del envío de correos es: [{response}]");
            return response;
        }

        private string rutBox = @"<tr style='mso-yfti-irow:1;mso-yfti-lastrow:yes;height:17.0pt'>
            <td width=95 nowrap style='width:70.9pt;border:solid windowtext 1.0pt;
            border-top:none;background:#D9E2F3;padding:0cm 3.5pt 0cm 3.5pt;height:17.0pt'>
            <span
                style='font-size:10.0pt;color:black;mso-color-alt:windowtext'>
            <p class=MsoNormal style='text-align:justify'><span style='mso-bookmark:_Hlk19522537'><span style='font-size:10.0pt'><o:p></o:p></span><span
            style='font-family:'Calibri Light',sans-serif;color:black'>#CompleteDebtorRUT#<o:p></o:p></span></span></p>
            </td>";

        private string debtorBox = @"<span style='mso-bookmark:_Hlk19522537'></span>
            <td width=312 nowrap style='width:233.9pt;border-top:none;border-left:none;
            border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
            background:#D9E2F3;padding:0cm 3.5pt 0cm 3.5pt;height:17.0pt'>
<span
                style='font-size:10.0pt;color:black;mso-color-alt:windowtext'>
            <p class=MsoNormal style='text-align:justify'><span style='mso-bookmark:_Hlk19522537'><span style='font-size:10.0pt'><o:p></o:p></span>
            <span
            style='font-family:'Calibri Light',sans-serif;color:black'>#SocialReasonDebtor#</span></span><span style='mso-bookmark:_Hlk19522537'>
            <span style='font-size:10.0pt'><o:p></o:p></span></span></p>
            </td>";

        private string documentBox = @"<td width=95 nowrap style='width:70.9pt;border:solid windowtext 1.0pt;
            border-top:none;background:#D9E2F3;padding:0cm 3.5pt 0cm 3.5pt;height:17.0pt'><span
                style='font-size:10.0pt;color:black;mso-color-alt:windowtext'>
            <p class=MsoNormal style='text-align:justify'><span style='mso-bookmark:_Hlk19522537'><span style='font-size:10.0pt'><o:p></o:p></span><span
            style='font-family:'Calibri Light',sans-serif;color:black'>#IdDocument#<o:p></o:p></span></span></p>
            </td>";

        private string amountBox = @"<span style='mso-bookmark:_Hlk19522537'></span>
            <td width=76 nowrap style='width:2.0cm;border-top:none;border-left:none;
            border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
            background:#D9E2F3;padding:0cm 3.5pt 0cm 3.5pt;height:17.0pt'>
            <p class=MsoNormal style='text-align:justify'><span style='mso-bookmark:_Hlk19522537'><span
            style='font-family:'Calibri Light',sans-serif;color:black'>#DocumentAmount#</span></span><span
            style ='mso-bookmark:_Hlk19522537'><span style='font-size:10.0pt'><o:p></o:p></span></span></p>
            </td>";

        private string expiredDateBox = @"<span style='mso-bookmark:_Hlk19522537'></span>
            <td width=76 nowrap style='width:2.0cm;border-top:none;border-left:none;
            border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
            background:#D9E2F3;padding:0cm 3.5pt 0cm 3.5pt;height:17.0pt'>
            <p class=MsoNormal style='text-align:justify'><span style='mso-bookmark:_Hlk19522537'><span
            style='font-family:'Calibri Light',sans-serif;color:black'>#ExpiredDate#</span></span><span
            style ='mso-bookmark:_Hlk19522537'><span style='font-size:10.0pt'><o:p></o:p></span></span></p>
            </td>";
    }
}
