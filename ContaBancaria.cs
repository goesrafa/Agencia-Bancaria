using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AgenciaBancaria.Dominio
{
    
    public class ContaBancaria
    {
        public ContaBancaria(Cliente cliente)
        {
            Random random = new Random();

            NumConta = random.Next(50000, 100000);
            DigitoVerificador = random.Next(0, 9);

            Situacao = SituacaoConta.Criada;

            Cliente = cliente ?? throw new Exception("Informe o cliente");
        }

        public void Abrir(string senha)
        {
            SetaSenha(senha);
            
            Situacao = SituacaoConta.Aberta;
            DataAbertura = DateTime.Now;
        }
        public virtual void Sacar(decimal valor, string senha)
        {
            if (Senha != senha)
            {
                throw new Exception("Senha inválida!!");
            }

            if (Saldo < valor)
            {
                throw new Exception("Saldo insuficiente");
            }

            Saldo -= valor;
        }

        private void SetaSenha(string senha)
        {
           senha = senha.ValidaStringVazia();

            if (!Regex.IsMatch(senha, @"^(?=.*?[a-z])(?=.*?[0-9]).{8,}$")) //Valida se tem pelo menos uma letra, um numero e 8 caracteres.
            {
                throw new Exception("Senha inválida!!!");
            }

            Senha = senha;
        }

        public int NumConta { get; init; }
        public int DigitoVerificador { get; init; }
        public decimal Saldo { get; protected set; }
        public DateTime? DataAbertura { get; private set; } //? -> permite que a data seja nula
        public DateTime? DataEncerramento { get; private set; } //? -> permite que a data seja nula
        public SituacaoConta Situacao { get; private set; }
        public string Senha { get; private set; }
        public Cliente Cliente { get; init; }

    }
}
