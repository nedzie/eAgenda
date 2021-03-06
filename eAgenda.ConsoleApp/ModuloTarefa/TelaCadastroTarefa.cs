using eAgenda.ConsoleApp.Compartilhado;
using eAgenda.ConsoleApp.ModuloItem;
using System;
using System.Collections.Generic;

namespace eAgenda.ConsoleApp.ModuloTarefa
{
    public class TelaCadastroTarefa : TelaBase
    {
        #region Atributos
        private readonly RepositorioTarefa repositorioTarefa;

        #endregion

        #region Construtor
        public TelaCadastroTarefa(RepositorioTarefa repositorioTarefa) : base("Gerenciando Tarefas")
        {
            this.repositorioTarefa = repositorioTarefa;
        }
        #endregion

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Tarefas");

            Tarefa novoTarefa = ObterTarefa();

            repositorioTarefa.Inserir(novoTarefa);

            Notificador.ApresentarMensagem("Tarefa cadastrada com sucesso!", TipoMensagemEnum.Sucesso);
        }
        public void Editar()
        {
            MostrarTitulo("Editando Tarefa");

            bool temAlgo = Visualizar();
            if (!temAlgo)
            {
                Notificador.ApresentarMensagem("Nenhuma tarefa inserida para editar.", TipoMensagemEnum.Atencao);
                return;
            }

            int numeroTarefa = ObterNumeroTarefa();// Daqui pra frente significa que tem algo para editar

            Tarefa tarefaAntiga = repositorioTarefa.SelecionarRegistro(numeroTarefa);

            Tarefa novaTarefa = ObterTarefa();

            Tarefa atualizada = repositorioTarefa.CriarParaEditar(tarefaAntiga, novaTarefa);

            bool deuPraEditar = repositorioTarefa.Editar(numeroTarefa, atualizada);

            if (!deuPraEditar)
                Notificador.ApresentarMensagem("Não deu pra editar.", TipoMensagemEnum.Erro);
            else
                Notificador.ApresentarMensagem("Editado com sucesso.", TipoMensagemEnum.Sucesso);
        }

        internal void Excluir()
        {
            MostrarTitulo("Excluindo Tarefa");

            bool temAlgo = Visualizar();
            if (!temAlgo)
            {
                Notificador.ApresentarMensagem("Nenhuma tarefa inserido para excluir.", TipoMensagemEnum.Atencao);
                return;
            }

            int numeroTarefa = ObterNumeroTarefa();

            bool deuPraExcluir = repositorioTarefa.Excluir(numeroTarefa);

            if (!deuPraExcluir)
                Notificador.ApresentarMensagem("Não deu pra excluir.", TipoMensagemEnum.Erro);
            else
                Notificador.ApresentarMensagem("Excluído com sucesso.", TipoMensagemEnum.Sucesso);
        }

        public int ObterNumeroTarefa()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;
            do
            {
                Console.Write("Digite o ID da tarefa que deseja: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = repositorioTarefa.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    Notificador.ApresentarMensagem("ID da Tarefa não foi encontrado, digite novamente", TipoMensagemEnum.Atencao);

            } while (numeroRegistroEncontrado == false);
            return numeroRegistro;
        }

        public bool Visualizar()
        {
            List<Tarefa> tarefas = repositorioTarefa.SelecionarTodos();
            tarefas.Sort();
            if (tarefas.Count == 0)
            {
                Notificador.ApresentarMensagem("Nenhuma tarefa inserida.", TipoMensagemEnum.Atencao);
                return false;
            }
            else
            {
                foreach (Tarefa tarefa in tarefas)
                {
                    Console.WriteLine(tarefa.ToString() + "\n");
                }
                Console.ReadKey();
                return true;
            }
        }

        internal void VisualizarPendentes()
        {
            if(!repositorioTarefa.TemAlgo())
            {
                Notificador.ApresentarMensagem("Sem tarefas...", TipoMensagemEnum.Atencao);
                return;
            }    
            List<Tarefa> tarefasPendentes = repositorioTarefa.Filtrar(x => x.concluida == false);
            if(tarefasPendentes.Count == 0)
            {
                Notificador.ApresentarMensagem("Sem tarefas pendentes", TipoMensagemEnum.Atencao);
                return;
            }
            tarefasPendentes.Sort();
            foreach (Tarefa tarefa in tarefasPendentes)
            {
                Console.WriteLine(tarefa.ToString() + "\n");
            }
            Console.ReadKey();
        }

        internal void VisualizarConcluidas()
        {
            if (!repositorioTarefa.TemAlgo())
            {
                Notificador.ApresentarMensagem("Sem tarefas pendentes", TipoMensagemEnum.Atencao);
                return;
            }
            List<Tarefa> tarefasConcluidas = repositorioTarefa.Filtrar(x => x.concluida == true);
            tarefasConcluidas.Sort();
            foreach (Tarefa tarefa in tarefasConcluidas)
            {
                Console.WriteLine(tarefa.ToString() + "\n");
            }
            Console.ReadKey();
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar todas");
            Console.WriteLine("Digite 5 para Visualizar concluídas");
            Console.WriteLine("Digite 6 para Visualizar pendentes");
            Console.WriteLine("Digite 7 para Adicionar itens a uma tarefa existente");
            Console.WriteLine("Digite 8 para Concluir itens:");
            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();
            return opcao;
        }
        #region Métodos privados
        private Tarefa ObterTarefa()
        {
            Console.WriteLine("Prioridade [3 - Alta, 2 - Normal, 1 - Baixa]");
            int prioridade = int.Parse(Console.ReadLine());
            PrioridadeEnum prioridadeTarefa = PrioridadeEnum.Baixa;
            switch (prioridade)
            {
                case 1:
                    prioridadeTarefa = PrioridadeEnum.Baixa;
                    break;
                case 2:
                    prioridadeTarefa = PrioridadeEnum.Normal;
                    break;
                case 3:
                    prioridadeTarefa = PrioridadeEnum.Alta;
                    break;
                default:
                    Notificador.ApresentarMensagem("Prioridade inválida, tente novamente!", TipoMensagemEnum.Atencao);
                    break;
            }
            Console.WriteLine("Titulo:");
            string titulo = Console.ReadLine();

            Console.WriteLine("Conclusão:");
            DateTime conclusao = DateTime.Parse(Console.ReadLine());

            int percentual = 0;

            return new Tarefa(prioridadeTarefa, titulo, DateTime.Now, conclusao, percentual);
        }
        #endregion

        public void AdicionarItens()
        {
            bool temTarefas = Visualizar();
            if (!temTarefas)
                return;
            int numeroTarefa = ObterNumeroTarefa();
            Tarefa tarefaSelecionada = repositorioTarefa.SelecionarRegistro(numeroTarefa);
            Item novoItem = ObterItem();
            tarefaSelecionada.AdicionarItem(novoItem);
        }

        public void ConcluirItens()
        {
            bool temTarefas = Visualizar(); // Dá pra melhorar essa parte toda
            if (!temTarefas)
                return;
            int numeroTarefa = ObterNumeroTarefa();
            Tarefa tarefaSelecionada = repositorioTarefa.SelecionarRegistro(numeroTarefa);
            if (tarefaSelecionada.percentualConclusao == 100)
            {
                Notificador.ApresentarMensagem("Todos os itens desta tarefa já foram concluídos!", TipoMensagemEnum.Erro);
                return;
            }
            if(tarefaSelecionada.itens.Count == 0)
            {
                Notificador.ApresentarMensagem("Sem itens nesta tarefa para concluir.", TipoMensagemEnum.Erro);
                return;
            }
            Console.WriteLine("E qual dos itens você gostaria de concluir?");
            int idEscolhido = int.Parse(Console.ReadLine()) - 1;
            tarefaSelecionada.ConcluirItem(idEscolhido);
        }

        public Item ObterItem()
        {
            Console.WriteLine("Descreva descrição do item: ");
            string descricao = Console.ReadLine();
            bool concluido = false;
            return new Item(descricao, concluido);
        }
    }
}