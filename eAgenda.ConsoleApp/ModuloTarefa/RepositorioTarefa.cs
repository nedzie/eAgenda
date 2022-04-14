using eAgenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;

namespace eAgenda.ConsoleApp.ModuloTarefa
{
    public class RepositorioTarefa : RepositorioBase<Tarefa>
    {
        public override bool Editar(int idSelecionado, Tarefa novoRegistro)
        {
            novoRegistro.id = idSelecionado;
            registros.Insert(novoRegistro.id, novoRegistro);
            return registros.Remove(registros.Find(x => x.id == idSelecionado));
        }

        internal Tarefa CriarParaEditar(Tarefa tarefaAntiga, Tarefa tarefaNova)
        {
            PrioridadeEnum prioridade = tarefaNova.Prioridade;
            string titulo = tarefaNova.Titulo;
            DateTime dataCriacao = tarefaAntiga.DataCriacao;
            DateTime dataConclusao = tarefaNova.DataConclusao;
            decimal percentual = tarefaNova.PercentualConclusao;
            return new Tarefa(prioridade, titulo, dataCriacao, dataConclusao, percentual);
        }
    }
}