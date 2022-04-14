using eAgenda.ConsoleApp.Compartilhado;
using eAgenda.ConsoleApp.ModuloContato;
using System;
using System.Collections.Generic;

namespace eAgenda.ConsoleApp.ModuloCompromisso
{
    public class TelaCadastroCompromisso : TelaBase
    {
        #region Atributos
        private readonly RepositorioCompromisso repositorioCompromisso;
        private readonly RepositorioContato repositorioContato;
        private readonly TelaCadastroContato telaCadastroContato;
        #endregion

        #region Construtor
        public TelaCadastroCompromisso(RepositorioCompromisso repositorioCompromisso, TelaCadastroContato telaCadastroContato, RepositorioContato repositorioContato) : base("Gerenciando Compromissos")
        {
            this.repositorioCompromisso = repositorioCompromisso;
            this.telaCadastroContato = telaCadastroContato;
            this.repositorioContato = repositorioContato;
        }
        #endregion


        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar");
            Console.WriteLine("Digite 5 para Visualizar por período");

            string opcao = Console.ReadLine();
            return opcao;
        }
        public void Inserir()
        {
            MostrarTitulo("Cadastro de Compromissos");

            int temRegistros = repositorioContato.TemRegistros();
            if(temRegistros == 0)
            {
                Notificador.ApresentarMensagem("Sem contatos inseridos para criar um compromisso.", TipoMensagemEnum.Atencao);
                return;
            }
            Compromisso novoCompromisso = ObterCompromisso();

            repositorioCompromisso.Inserir(novoCompromisso);

            Notificador.ApresentarMensagem("Compromisso cadastrado com sucesso!", TipoMensagemEnum.Sucesso);
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

            int numeroCompromisso = ObterNumeroCompromisso();// Daqui pra frente significa que tem algo para editar
            Compromisso novoCompromisso = ObterCompromisso(); // Pega o novo contato

            bool deuPraEditar = repositorioCompromisso.Editar(numeroCompromisso, novoCompromisso);

            if (!deuPraEditar)
                Notificador.ApresentarMensagem("Não deu pra editar.", TipoMensagemEnum.Erro);
            else
                Notificador.ApresentarMensagem("Editado com sucesso.", TipoMensagemEnum.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Compromisso");

            bool temAlgo = Visualizar();
            if (!temAlgo)
            {
                Notificador.ApresentarMensagem("Nenhum contato inserido para excluir.", TipoMensagemEnum.Atencao);
                return;
            }

            int numeroCompromisso = ObterNumeroCompromisso();// Daqui pra frente significa que tem algo para editar

            bool deuPraExcluir = repositorioCompromisso.Excluir(numeroCompromisso);

            if (!deuPraExcluir)
                Notificador.ApresentarMensagem("Não deu pra excluir.", TipoMensagemEnum.Erro);
            else
                Notificador.ApresentarMensagem("Excluído com sucesso.", TipoMensagemEnum.Sucesso);
        }

        public bool Visualizar()
        {
            List<Compromisso> compromissos = repositorioCompromisso.SelecionarTodos();
            if (compromissos.Count == 0)
            {
                Notificador.ApresentarMensagem("Nenhum compromisso inserido.", TipoMensagemEnum.Atencao);
                return false;
            }
            else
            {
                foreach (Compromisso compromisso in compromissos)
                {
                    Console.WriteLine(compromisso.ToString() + "\n");
                }
                Console.ReadKey();
                return true;
            }
        }

        public void VisualizarPorPeriodo()
        {
            if (!repositorioCompromisso.TemAlgo())
            {
                Notificador.ApresentarMensagem("Sem compromissos...", TipoMensagemEnum.Atencao);
                return;
            }
            Console.WriteLine("Informe a data inicial: ");
            DateTime dataInicio = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Informa a data final: ");
            DateTime dataFim = DateTime.Parse(Console.ReadLine());

            List<Compromisso> compromissosNoPeriodo = repositorioCompromisso.FiltrarEmIntervalo(x => x.DataInicio >= dataInicio, x => x.DataInicio <= dataFim);
            if (compromissosNoPeriodo == null)
            {
                Notificador.ApresentarMensagem("Sem tarefas neste período", TipoMensagemEnum.Atencao);
                return;
            }

            Console.WriteLine("Compromissos encontrados neste período: ");

            foreach (Compromisso compromisso in compromissosNoPeriodo)
            {
                Console.WriteLine(compromisso.ToString() + "\n");
            }
            Console.ReadKey();
        }

        private Compromisso ObterCompromisso()
        {
            Console.WriteLine("Digite o assunto do compromisso: ");
            string assunto = Console.ReadLine();
            Console.WriteLine("Informe o local do compromisso: ");
            string local = Console.ReadLine();
            Console.WriteLine("Informe a data/hora de início do compromisso (dd/MM/aaaa hh:mm)");
            DateTime dataInicio = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Informe a data/hora de finalização do compromisso (dd/MM/aaaa hh:mm)");
            DateTime dataFim = DateTime.Parse(Console.ReadLine());
            telaCadastroContato.Visualizar();
            int numeroContato = telaCadastroContato.ObterNumeroContato();
            Contato contatoSelecionado = repositorioContato.SelecionarRegistro(numeroContato);
            return new Compromisso(assunto, local, dataInicio, dataFim, contatoSelecionado);
        }

        private int ObterNumeroCompromisso()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;
            do
            {
                Console.Write("Digite o ID do Compromisso que deseja: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = repositorioContato.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    Notificador.ApresentarMensagem("ID do Contato não foi encontrado, digite novamente", TipoMensagemEnum.Atencao);

            } while (numeroRegistroEncontrado == false);
            return numeroRegistro;
        }
    }
}