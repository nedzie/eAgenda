using eAgenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // Email
using System.Text.RegularExpressions; // Telefone

namespace eAgenda.ConsoleApp.ModuloContato
{
    public class TelaCadastroContato : TelaBase
    {
        #region Atributos
        private readonly RepositorioContato repositorioContato;
        #endregion
        public TelaCadastroContato(RepositorioContato repositorioContato) : base("Gerenciando Contatos")
        {
            this.repositorioContato = repositorioContato;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Contatos");

            Contato novoContato = ObterContato();

            repositorioContato.Inserir(novoContato);

            Notificador.ApresentarMensagem("Contato cadastrado com sucesso!", TipoMensagemEnum.Sucesso);
        }
        public void Editar()
        {
            MostrarTitulo("Editando Contato");

            bool temAlgo = Visualizar();
            if (!temAlgo)
            {
                Notificador.ApresentarMensagem("Nenhum contato inserido para editar.", TipoMensagemEnum.Atencao);
                return;
            }

            int numeroContato = ObterNumeroContato();// Daqui pra frente significa que tem algo para editar
            Contato novoContato = ObterContato(); // Pega o novo contato

            bool deuPraEditar = repositorioContato.Editar(numeroContato, novoContato);

            if (!deuPraEditar)
                Notificador.ApresentarMensagem("Não deu pra editar.", TipoMensagemEnum.Erro);
            else
                Notificador.ApresentarMensagem("Editado com sucesso.", TipoMensagemEnum.Sucesso);
        }

        internal void Excluir()
        {
            MostrarTitulo("Excluindo Contato");

            bool temAlgo = Visualizar();
            if (!temAlgo)
            {
                Notificador.ApresentarMensagem("Nenhum contato inserido para excluir.", TipoMensagemEnum.Atencao);
                return;
            }

            int numeroContato = ObterNumeroContato();// Daqui pra frente significa que tem algo para editar

            bool deuPraExcluir = repositorioContato.Excluir(numeroContato);

            if (!deuPraExcluir)
                Notificador.ApresentarMensagem("Não deu pra excluir.", TipoMensagemEnum.Erro);
            else
                Notificador.ApresentarMensagem("Excluído com sucesso.", TipoMensagemEnum.Sucesso);
        }

        public int ObterNumeroContato()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;
            do
            {
                Console.Write("Digite o ID do contato que deseja: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = repositorioContato.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    Notificador.ApresentarMensagem("ID do Contato não foi encontrado, digite novamente", TipoMensagemEnum.Atencao);

            } while (numeroRegistroEncontrado == false);
            return numeroRegistro;
        }

        public bool Visualizar()
        {
            List<Contato> contatos = repositorioContato.SelecionarTodos();
            if (contatos.Count == 0)
            {
                Notificador.ApresentarMensagem("Nenhum contato inserido.", TipoMensagemEnum.Atencao);
                return false;
            }
            else
            {
                foreach (Contato contato in contatos)
                {
                    Console.WriteLine(contato.ToString() + "\n");
                }
                Console.ReadKey();
                return true;
            }
        }

        #region Métodos privados
        private Contato ObterContato()
        {
            Console.WriteLine("Nome:");
            string nome = Console.ReadLine();
            string email = "";
            do
            {
                Console.WriteLine("E-mail:");
                email = Console.ReadLine();
            } while (!(ValidarEmail(email))); //Verifica o e-mail e já obtem o retorno

            bool telefoneValido = false;
            string telefone = "";
            do
            {
                Console.WriteLine("Telefone:");
                Console.WriteLine("Padrão: (00) 00000-0000");
                telefone = Console.ReadLine();
                telefoneValido = ValidarTelefone(telefone);
                if (!telefoneValido)
                    Notificador.ApresentarMensagem("Telefone inválido, tente novamente", TipoMensagemEnum.Erro);
            } while (!telefoneValido);

            Console.WriteLine("Empresa:");
            string empresa = Console.ReadLine();
            Console.WriteLine("Cargo:");
            string cargo = Console.ReadLine();

            return new Contato(nome, email, telefone, empresa, cargo);
        }
        private bool ValidarEmail(string email)
        {
            EmailAddressAttribute e = new();
            if (e.IsValid(email))
                return true;
            else
            {
                Notificador.ApresentarMensagem("E-mail inválido, tente novamente!", TipoMensagemEnum.Erro);
                return false;
            }
        }
        private bool ValidarTelefone(string telefone)
        {
            return Regex.Match(telefone, @"^\([1-9]{2}\) (?:[2-8]|9[1-9])[0-9]{3}\-[0-9]{4}$").Success;
        }
        #endregion
    }
}