using SAED.Core.Interfaces;

namespace SAED.Core.Entities
{
    public class Cpf : IEntity
    {
        public int Id { get; set; }
        public string Codigo { get; set; }

        public Cpf()
        {
            
        }

        public Cpf(string codigo)
        {
            Codigo = codigo;
        }

        public static Cpf Parse(string codigo)
        {
            if (TryParse(codigo, out var result))
            {
                return result;
            }

            return null;
            //throw new ArgumentException("CPF Inválido");
        }

        public static bool TryParse(string codigo, out Cpf cpf)
        {
            //.. validation Logic 
            cpf = new Cpf(codigo);
            return true;
        }

        public string Normalize() => Codigo.Replace(".", string.Empty).Replace("-", string.Empty);

        public override string ToString() => Codigo;

        public static implicit operator Cpf(string codigo) => Parse(codigo);
    }
}
