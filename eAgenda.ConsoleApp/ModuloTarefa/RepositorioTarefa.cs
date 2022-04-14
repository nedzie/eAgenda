using eAgenda.ConsoleApp.Compartilhado;
using eAgenda.ConsoleApp.ModuloItem;
using System;
using System.Collections.Generic;

namespace eAgenda.ConsoleApp.ModuloTarefa
{
    public class RepositorioTarefa : RepositorioBase<Tarefa>
    {

        public override bool Excluir(int idSelecionado)
        {
            int menosUm = idSelecionado - 1;
            if (registros[menosUm].concluida == true)
            {
                return registros.Remove(registros.Find(x => x.id == idSelecionado));
            }
            else
                return false;
        }

        internal Tarefa CriarParaEditar(Tarefa tarefaAntiga, Tarefa tarefaNova)
        {
            PrioridadeEnum prioridade = tarefaNova.Prioridade;
            string titulo = tarefaNova.Titulo;
            DateTime dataCriacao = tarefaAntiga.DataCriacao;
            DateTime dataConclusao = tarefaNova.DataConclusao;
            decimal percentual = tarefaAntiga.PercentualConclusao;
            List<Item> itens = tarefaAntiga.itens;
            return new Tarefa(prioridade, titulo, dataCriacao, dataConclusao, percentual, itens);
        }
    }
}