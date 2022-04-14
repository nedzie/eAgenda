using eAgenda.ConsoleApp.Compartilhado;
using System;

namespace eAgenda.ConsoleApp.ModuloContato
{
    public class Contato : EntidadeBase
    {
        #region Atributos
        private readonly string nome;
        private readonly string email;
        private readonly string telefone;
        private readonly string empresa;
        private readonly string cargo;
        #endregion

        public string Nome { get => nome; }
        #region Construtor
        public Contato(string nome, string email, string telefone, string empresa, string cargo)
        {
            this.nome = nome;
            this.email = email;
            this.telefone = telefone;
            this.empresa = empresa;
            this.cargo = cargo;
        }
        #endregion

        #region Override toString()
        public override string ToString()
        {
            return "id: " + id + "\n" +
                   "Nome: " + nome + "\n" +
                   "Email: " + email + "\n" +
                   "Telefone: " + telefone + "\n" +
                   "Empresa: " + empresa + "\n" +
                   "Cargo: " + cargo + "\n";
        }
        #endregion
    }
}