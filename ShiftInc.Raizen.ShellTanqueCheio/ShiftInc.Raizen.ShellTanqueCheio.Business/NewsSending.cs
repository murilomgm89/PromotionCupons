using ShiftInc.Raizen.ShellTanqueCheio.Business.Repository;
using ShiftInc.Raizen.ShellTanqueCheio.Data;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftInc.Raizen.ShellTanqueCheio.Business
{
    public class NewsSending
    {
        public static IEnumerable<Entity.NewsSending> GetToSend(int count)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                return context.NewsSending
                    .Include(ns => ns.Receipt)                   
                    .Include(ns => ns.Receipt.Product)
                    .Include(ns => ns.Receipt.Person)
                    .Include(ns => ns.Receipt.LuckyCodes)
                    .Include(ns => ns.Person)
                    .Where(ns => ns.dtSending == null).OrderBy(ns => ns.idNewsSending).Take(count).ToList();
            }
        }

        public static void SetToSend(string destination, NewsType type, int? idReceipt = null, int? idPerson = null)
        {
            var news = new ShiftInc.Raizen.ShellTanqueCheio.Entity.NewsSending()
            {
                Destination = destination,
                fileName = type.ToString(),
                Subject = GetSubjectByNewsType(type),
                Status = 0,
                idReceipt = idReceipt,
                idPerson = idPerson
            };
            ShiftInc.Raizen.ShellTanqueCheio.Business.NewsSending.Save(news);
        }
        public static void Save(Entity.NewsSending news)
        {
            using (ShellTanqueCheioModel context = new ShellTanqueCheioModel())
            {
                if (news.idNewsSending == 0)
                {
                    context.NewsSending.Add(news);
                }
                else
                {
                    context.NewsSending.Attach(news);
                    context.Entry(news).State = EntityState.Modified;
                }

                context.SaveChanges();
            }
        }        
        public enum NewsType
        {
            CreatePersonVPower,
            CreatePersonLubrificante,
            DailyAlert,
            VPowerWinner,
            VPowerNotWinner,
            VPowerWinnerBullet,
            LubrificantesWinner,
            LubrificantesNewReceipt,
            LubrificantesNotWinner,
            CupomIlegivel,
            DataInvalida,
            ImagemNaoECupom,
            CupomIncompleto,
            CupomDiferenteProduto,
            CupomJaCadastrado,
            ConsumidorAtingiuLimiteTotal,
            ConsumidorAtingiuDiario,
            CupomValorAbaixo,
            CupomProdutoNaoParticipante,
            CupomSemPreenchimento,
            ResultadoSorteio
        }
        private static string GetSubjectByNewsType(NewsType type)
        {
            var subjects = new Dictionary<NewsType, string>()
            {               
                { NewsType.DailyAlert, "Aprovações pendentes - Promoção Shell Tanque Cheio" },
                
                { NewsType.CreatePersonVPower, "{{Person.firstName}}, seu cadastro foi realizado com sucesso." },
                { NewsType.VPowerWinner, "{{Person.firstName}}, seu cupom foi premiado na promoção Shell Tanque Cheio!" },
                { NewsType.VPowerNotWinner, "{{Person.firstName}}, não foi dessa vez, mas você ainda pode participar." },
                { NewsType.VPowerWinnerBullet, "PARABÉNS, {{Person.firstName}}! Você acaba de ganhar R$200 para abastecer." },
                
                { NewsType.CreatePersonLubrificante, "{{Person.firstName}}, seu cadastro foi realizado com sucesso." },
                { NewsType.LubrificantesWinner, "PARABÉNS, {{Person.firstName}}! Você ganhou R$9.000 para abastecer o ano todo." },

               
                { NewsType.CupomIlegivel, "Seu cupom foi desclassificado. Tente outra vez." },
                { NewsType.DataInvalida, "Tanque Cheio alerta: seu cupom expirou. Tente novamente." },
                { NewsType.ImagemNaoECupom, "Erro de confirmação na imagem da nota fiscal. Tente de novo." },
                { NewsType.CupomIncompleto, "{{Person.firstName}}, seu cupom foi desclassificado por falta de informações." },
                { NewsType.CupomDiferenteProduto, "{{Person.firstName}}, o cupom cadastrado está sem produtos válidos." },
                { NewsType.CupomJaCadastrado, "{{Person.firstName}}, não é possível usar o mesmo cupom em outras promoções." },   
                { NewsType.ConsumidorAtingiuLimiteTotal, "{{Person.firstName}} excedeu seu limite de participações para essa promoção." },
                { NewsType.ConsumidorAtingiuDiario, "{{Person.firstName}}, você atingiu o limite diário de participações." },
                { NewsType.CupomValorAbaixo, "{{Person.firstName}}, o valor de cupom fiscal está abaixo do regulamento." },
                { NewsType.CupomProdutoNaoParticipante, "{{Person.firstName}}, não constam produtos válidos no cupom cadastrado." },
                { NewsType.CupomSemPreenchimento, "{{Person.firstName}}, seu cupom fiscal não foi preenchido corretamente." },

                { NewsType.LubrificantesNewReceipt, "Olá {{Person.firstName}}, aqui estão seus números da sorte da Tanque Cheio." },
                { NewsType.LubrificantesNotWinner, "{{Person.firstName}}, infelizmente seu cupom não foi válido para receber o prêmio" },
                { NewsType.ResultadoSorteio, "Saiba quem ganhou e veja se você está entre os sorteados." }
            };

            return subjects[type];
        }
    }
}
