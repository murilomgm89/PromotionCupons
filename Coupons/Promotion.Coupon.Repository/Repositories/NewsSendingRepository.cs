using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using Promotion.Coupon.Entity.Enum;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Repository.Repositories.Base;

namespace Promotion.Coupon.Repository.Repositories
{
    public class NewsSendingRepository : RepositoryBase<NewsSending>, INewsSendingRepository
    {
        public IEnumerable<NewsSending> GetToSend(int count)
        {
            using (var context = new GymPass())
            {
                return context.NewsSending
                    .Include(ns => ns.Person)
                    .Include(ns => ns.Receipt)
                    //.Include(ns => ns.Receipt.Product)
                    .Include(ns => ns.Receipt.Person)
                    //.Include(ns => ns.Receipt.LuckyCode)
                    .Where(ns => ns.dtSending == null)
                    .OrderBy(ns => ns.idNewsSending)
                    .Take(count)
                    .ToList();
            }
        }

        public void SetToSend(string destination, ENewsType type, int? idReceipt = null, int? idPerson = null)
        {
            throw new System.NotImplementedException();
        }
        [Obsolete]
        public void Save(NewsSending news)
        {
            Insert(news);
        }

        public string GetSubjectByNewsType(ENewsType type)
        {
            var subjects = new Dictionary<ENewsType, string>()
            {
                { ENewsType.DailyAlert, "Aprovações pendentes - Promoção Shell Tanque Cheio" },

                { ENewsType.CreatePersonVPower, "{{Person.firstName}}, seu cadastro foi realizado com sucesso." },
                { ENewsType.VPowerWinner, "{{Person.firstName}}, seu cupom foi premiado na promoção Shell Tanque Cheio!" },
                { ENewsType.VPowerNotWinner, "{{Person.firstName}}, não foi dessa vez, mas você ainda pode participar." },
                { ENewsType.VPowerWinnerBullet, "PARABÉNS, {{Person.firstName}}! Você acaba de ganhar R$200 para abastecer." },

                { ENewsType.CreatePersonLubrificante, "{{Person.firstName}}, seu cadastro foi realizado com sucesso." },
                { ENewsType.LubrificantesWinner, "PARABÉNS, {{Person.firstName}}! Você ganhou R$9.000 para abastecer o ano todo." },


                { ENewsType.CupomIlegivel, "Seu cupom foi desclassificado. Tente outra vez." },
                { ENewsType.DataInvalida, "Tanque Cheio alerta: seu cupom expirou. Tente novamente." },
                { ENewsType.ImagemNaoECupom, "Erro de confirmação na imagem da nota fiscal. Tente de novo." },
                { ENewsType.CupomIncompleto, "{{Person.firstName}}, seu cupom foi desclassificado por falta de informações." },
                { ENewsType.CupomDiferenteProduto, "{{Person.firstName}}, o cupom cadastrado está sem produtos válidos." },
                { ENewsType.CupomJaCadastrado, "{{Person.firstName}}, não é possível usar o mesmo cupom em outras promoções." },
                { ENewsType.ConsumidorAtingiuLimiteTotal, "{{Person.firstName}} excedeu seu limite de participações para essa promoção." },
                { ENewsType.ConsumidorAtingiuDiario, "{{Person.firstName}}, você atingiu o limite diário de participações." },
                { ENewsType.CupomValorAbaixo, "{{Person.firstName}}, o valor de cupom fiscal está abaixo do regulamento." },
                { ENewsType.CupomProdutoNaoParticipante, "{{Person.firstName}}, não constam produtos válidos no cupom cadastrado." },
                { ENewsType.CupomSemPreenchimento, "{{Person.firstName}}, seu cupom fiscal não foi preenchido corretamente." },

                { ENewsType.LubrificantesNewReceipt, "Olá {{Person.firstName}}, aqui estão seus números da sorte da Tanque Cheio." },
                { ENewsType.LubrificantesNotWinner, "{{Person.firstName}}, infelizmente seu cupom não foi válido para receber o prêmio" },
                { ENewsType.ResultadoSorteio, "Saiba quem ganhou e veja se você está entre os sorteados." }
            };

            return subjects[type];
        }
    }
}