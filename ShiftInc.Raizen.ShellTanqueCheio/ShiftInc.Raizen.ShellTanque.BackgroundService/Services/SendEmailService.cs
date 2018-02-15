using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using ShiftInc.Raizen.ShellTanqueCheio.Entity;

namespace ShiftInc.Raizen.ShellTanque.BackgroundService.Services
{
    class SendEmailService : AbstractService, IService
    {
        public override string GetName()
        {
            return "SendEmailService";
        }

        public override void Execute()
        {
            var news = ShiftInc.Raizen.ShellTanqueCheio.Business.NewsSending.GetToSend(50);

            foreach (var n in news)
            {
                try
                {
                    n.dtSending = DateTime.Now;                    
                    NetworkCredential NetworkCred = new NetworkCredential("naoresponda@tanquecheiotodahora.com.br", "Sm3548br");

                    using (MailMessage mm = new MailMessage("naoresponda@tanquecheiotodahora.com.br", n.Destination))
                    {
                        var bodyContent = System.IO.File.ReadAllText("News/" + n.fileName + ".html");

                        mm.Subject = ProcessContent(n.Subject, n);
                        mm.Body = ProcessContent(bodyContent, n);

                        mm.IsBodyHtml = true;

                        SmtpClient smtp = new SmtpClient();
                        //smtp.Host = "cloud717.hospedagem.w3br.com";
                        smtp.Host = "192.168.0.4";
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.EnableSsl = false;
                        smtp.Port = 587;

                        smtp.Send(mm);
                    }

                    n.Status = 1;
                }
                catch (Exception ex)
                {
                    n.Status = 2;
                    n.StatusDetail = ex.ToString();
                }
                n.Person = null;
                ShiftInc.Raizen.ShellTanqueCheio.Business.NewsSending.Save(n);
            }
        }

        private string ProcessContent(string content, ShiftInc.Raizen.ShellTanqueCheio.Entity.NewsSending n)
        {

            var TextLegalVPower = "Participação válida para abastecimento no valor de R$ 50,00 com qualquer um dos produtos da família Shell V-Power (Shell V-Power Nitro+, Shell V-Power Etanol ou Shell V-Power Racing), em uma única compra. Promoção válida de 01/07/2016 a 02/10/2016, conforme disponibilidade dos produtos combustíveis na rede de postos Shell participantes. Esta promoção poderá ser encerrada antecipadamente, caso seja atingido o número total de premiações, conforme previsto em Regulamento. A previsibilidade do slogan “toda hora” utiliza como premissa a premiação do número total de 2.256 contemplados em 94 dias de promoção. A utilização do slogan “tanque cheio” tomou como base os modelos de carros mais vendidos em 2016, bem como o volume dos tanques destes modelos de carros, segundo dados da Federação Nacional da Distribuição de Veículos Automotores (FENABRAVE) e o preço médio da gasolina vendida nacionalmente, segundo dados oficiais da Agência Nacional do Petróleo (ANP) do ano de 2016. Esta premiação tomou como base um abastecimento de 54 (cinquenta e quatro) litros de produtos combustíveis. A entrega dos prêmios está condicionada ao cumprimento de todos os critérios de participação previstos em Regulamento. O abastecimento nos Postos Shell como premiação é meramente uma sugestão de utilização do prêmio, que será pago em cartão de débito no valor de R$ 200,00 (com função de saque bloqueada). Consulte condições de participação e Regulamento completo no site www.shell.com.br/tanquecheio. Certificado de Autorização CAIXA nº 5-1104/2016. A marca Shell é licenciada a Raízen, uma joint-venture entre Shell e Cosan.";
            var TextLegalLubrificantes = "Participação válida para a compra de, no mínimo, 03 (três) litros de óleos lubrificantes Shell Helix Ultra (Shell Helix Ultra SN 0W-20; Shell Helix Ultra ECT C2/C2 0W-30;- Shell Helix Ultra SN 5W-20; Shell Helix Ultra SN 5W-30; Shell Helix Ultra 5W-40, HX8 e/ou HX7), nos Postos Shell participantes, onde os Produtos estiverem disponíveis, em uma única compra. Promoção válida de 01/07/2016 a 02/10/2016, conforme disponibilidade dos produtos combustíveis na rede de postos Shell participantes. Caso seja atingido o número total de premiações, esta promoção será encerrada, conforme previsto em Regulamento. A entrega dos prêmios está condicionada ao cumprimento de todos os critérios de participação previstos em Regulamento. O abastecimento com Gasolina Shell V-Power como premiação é meramente uma sugestão de utilização do prêmio, que será pago em cartão de débito no valor total de R$ 9.000,00 (com função de saque bloqueada). O slogan “tanque cheio o ano todo” tomou como base o valor de R$ 9.000,00 e os modelos de carros mais vendidos em 2016, segundo dados da FENABRAVE, a eficiência energética e consumo de combustível dos veículos leves, conforme pesquisa disponibilizada pelo INMETRO e o preço médio da gasolina vendida nacionalmente, segundo dados oficiais da ANP do ano de 2016. O valor total do prêmio de cada contemplado será disponibilizado de uma única vez, recomendando-se, no entanto, que o contemplado utilize o valor mensal equivalente a R$ 750,00 (setecentos e cinquenta reais). Consulte condições de participação e Regulamento completo no site www.shell.com.br/tanquecheio. Certificado de Autorização CAIXA nº 4-1103/2016. O descarte inadequado de óleo lubrificante usado ou contaminado e de suas embalagens provoca danos à população e ao meio ambiente, podendo contaminar água e solo. O óleo usado e as embalagens são recicláveis. Entregue-os em um posto de serviços ou de coleta autorizado, conforme resolução Conama no 362/2005 e suas alterações vigentes. Imagens ilustrativas. A marca Shell é licenciada à Raízen, uma joint-venture entre a Shell e a Cosan.";
            var substituition = new Dictionary<string, string>();
            
            if(n.Receipt != null){
                substituition.Add("Person.name", n.Receipt.Person.name);
                substituition.Add("Person.firstName", n.Receipt.Person.name.Split(' ')[0]);
            }
            else if(n.Person != null)
            {
                substituition.Add("Person.name", n.Person.name);
                substituition.Add("Person.firstName", n.Person.name.Split(' ')[0]);
            }


            if (n.Receipt != null)
            {
                if(n.Receipt.Product.type == "v-power"){
                    substituition.Add("ProductText", TextLegalVPower);
                    substituition.Add("Promotion", "Tanque Cheio Toda Hora");
                    substituition.Add("Image", "https://www.tanquecheiotodahora.com.br/roboNews/News/images/headerVpower.jpg");
                }
                else
                {
                    substituition.Add("ProductText", TextLegalLubrificantes);
                    substituition.Add("Promotion", "Tanque Cheio Ano Todo");
                    substituition.Add("Image", "https://www.tanquecheiotodahora.com.br/roboNews/News/images/headerLubrificantes.jpg");
                    var contentCodes = "";
                    foreach (var luckyCodes in n.Receipt.LuckyCodes)
                    {
                        var dt = String.Format("{0:dd/MM/yyyy}", luckyCodes.dtRaffle);
                        contentCodes += "<tr>";
                        contentCodes += "<td style='border-right: 1px solid #dadada; padding-top:5px; padding-bottom:5px;'>" + dt + "</td>";
                        contentCodes += "<td style='border-right: 1px solid #dadada; padding-top:5px; padding-bottom:5px;'>" + (luckyCodes.code.ToString().Length == 6 ? luckyCodes.code.ToString().Insert(1, "-") : (luckyCodes.code.ToString().Length == 7 ? luckyCodes.code.ToString().Insert(2, "-") : (luckyCodes.code.ToString().Length == 8 ? luckyCodes.code.ToString().Insert(3, "-") : ""))) + "</td>";
						contentCodes += "</tr>";
                    }
                    substituition.Add("Codes", contentCodes);
                }
            }
            foreach (var subs in substituition)
            {
                content = content.Replace("{{" + subs.Key + "}}", subs.Value);
            }

            return content;
        }
    }
}
