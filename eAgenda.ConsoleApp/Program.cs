using eAgenda.ConsoleApp.Compartilhado;
using eAgenda.ConsoleApp.ModuloCompromisso;
using eAgenda.ConsoleApp.ModuloContato;
using eAgenda.ConsoleApp.ModuloTarefa;
using System;

namespace eAgenda.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TelaMenuPrincipal menuPrincipal = new TelaMenuPrincipal(); // Cria tudo e mostra as opções

            while (true)
            {
                TelaBase telaSelecionada = menuPrincipal.ObterTela(); // Obtém o tipo de tela
                string opcaoSelecionada = telaSelecionada.MostrarOpcoes(); // Mostra as opções da tela selecionada

                switch (telaSelecionada)
                {
                    case TelaCadastroContato:
                        TelaCadastroContato telaContato = (TelaCadastroContato)telaSelecionada;
                        switch (opcaoSelecionada)
                        {
                            case "1":
                                telaContato.Inserir();
                                break;
                            case "2":
                                telaContato.Editar();
                                break;
                            case "3":
                                telaContato.Excluir();
                                break;
                            case "4":
                                telaContato.Visualizar();
                                break;
                            case "s":
                                break;
                            default:
                                Notificador.ApresentarMensagem("Opção inválida.", TipoMensagemEnum.Erro);
                                break;
                        }
                        break;

                    case TelaCadastroCompromisso:
                        TelaCadastroCompromisso telaCompromisso = (TelaCadastroCompromisso)telaSelecionada;
                        switch (opcaoSelecionada)
                        {
                            case "1":
                                telaCompromisso.Inserir();
                                break;
                            case "2":
                                telaCompromisso.Editar();
                                break;
                            case "3":
                                telaCompromisso.Excluir();
                                break;
                            case "4":
                                telaCompromisso.Visualizar();
                                break;
                            case "s":
                                break;
                            default:
                                Notificador.ApresentarMensagem("Opção inválida.", TipoMensagemEnum.Erro);
                                break;
                        }
                        break;

                    case TelaCadastroTarefa:
                        TelaCadastroTarefa telaTarefa = (TelaCadastroTarefa)telaSelecionada;
                        switch (opcaoSelecionada)
                        {
                            case "1":
                                telaTarefa.Inserir();
                                break;
                            case "2":
                                telaTarefa.Editar();
                                break;
                            case "3":
                                telaTarefa.Excluir();
                                break;
                            case "4":
                                telaTarefa.Visualizar();
                                break;
                            case "5":
                                telaTarefa.AdicionarItens();
                                break;
                            case "6":
                                telaTarefa.ConcluirItens();
                                break;
                            case "s":
                                break;
                            default:
                                Notificador.ApresentarMensagem("Opção inválida.", TipoMensagemEnum.Erro);
                                break;
                        }
                        break;
                }
            }
        }
    }
}