namespace Promotion.Coupon.Entity.Poco
{
    public class InvalidateDescription
    {
        public static int GetIdByDescription(string desc)
        {
            switch (desc)
            {
                case "Cupom Fiscal Ilegível":
                    return 1;
                case "Data de participação inválida":
                    return 2;
                case "Imagem diferente de Cupom Fiscal":
                    return 3;
                case "Cupom Fiscal incompleto":
                    return 4;
                case "Produto descrito no Cupom Fiscal diferente do produto participante":
                    return 5;
                case "Cupom Fiscal já cadastrado":
                    return 6;
                case "Cupom Fiscal sem preenchimento (em branco)":
                    return 7;
                case "Cupom Fiscal com valor abaixo do estipulado em regulamento":
                    return 8;
                case "Cupom Fiscal com produto não participante":
                    return 9;
                default:
                    return 1;
            }
        }
    }
}