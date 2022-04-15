using eAgenda.ConsoleApp.ModuloCompromisso;
using eAgenda.ConsoleApp.ModuloContato;
using eAgenda.ConsoleApp.ModuloTarefa;
using System;

namespace eAgenda.ConsoleApp.Compartilhado
{
    public class TelaMenuPrincipal
    {
        #region Atributos da TelaMenuPrincipal
        private string opcaoSelecionada;
        private RepositorioContato repositorioContato;
        private TelaCadastroContato telaCadastroContato;

        private RepositorioCompromisso repositorioCompromisso;
        private TelaCadastroCompromisso telaCadastroCompromisso;

        private RepositorioTarefa repositorioTarefa;
        private TelaCadastroTarefa telaCadastroTarefa;
        #endregion

        #region Construtor
        public TelaMenuPrincipal()
        {
            repositorioContato = new RepositorioContato();
            telaCadastroContato = new TelaCadastroContato(repositorioContato);

            repositorioCompromisso = new RepositorioCompromisso();
            telaCadastroCompromisso = new TelaCadastroCompromisso(repositorioCompromisso, telaCadastroContato, repositorioContato);

            repositorioTarefa = new RepositorioTarefa();
            telaCadastroTarefa = new TelaCadastroTarefa(repositorioTarefa);
        }
        #endregion

        public string MostrarOpcoes()
        {
            Console.Clear();

            Console.WriteLine("-= eAgenda do JP =-");

            Console.WriteLine();

            Console.WriteLine("Digite 1 para gerenciar Compromissos");
            Console.WriteLine("Digite 2 para gerenciar Contatos");
            Console.WriteLine("Digite 3 para gerenciar Tarefas");

            Console.WriteLine("Digite s para sair");

            opcaoSelecionada = Console.ReadLine();
            return opcaoSelecionada;
        }

        public TelaBase ObterTela()
        {
            string opcao = MostrarOpcoes();

            TelaBase tela = null;

            switch(opcao)
            {
                case "1":
                    tela = telaCadastroCompromisso;
                    break;
                case "2":
                    tela = telaCadastroContato;
                    break;
                case "3":
                    tela = telaCadastroTarefa;
                    break;
                default:
                    Notificador.ApresentarMensagem("Opcao inválida", TipoMensagemEnum.Erro);
                    break;
            }
            return tela;
        }
    }
}