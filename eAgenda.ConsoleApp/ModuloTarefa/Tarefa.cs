using eAgenda.ConsoleApp.Compartilhado;
using eAgenda.ConsoleApp.ModuloItem;
using System;
using System.Collections.Generic;
using System.Text;

namespace eAgenda.ConsoleApp.ModuloTarefa
{
    public class Tarefa : EntidadeBase
    {
        private readonly PrioridadeEnum prioridade;
        private readonly string titulo;
        private readonly DateTime dataCriacao;
        private readonly DateTime dataConclusao;
        public decimal percentualConclusao;
        public bool concluida;
        public List<Item> itens;

        public PrioridadeEnum Prioridade { get => prioridade; }
        public string Titulo { get => titulo; }
        public DateTime DataCriacao { get => dataCriacao; }
        public DateTime DataConclusao { get => dataConclusao; }
        public decimal PercentualConclusao { get => percentualConclusao; }
        //public List<Item> Itens { get => itens; }
        public Tarefa(PrioridadeEnum prioridade, string titulo, DateTime dataCriacao, DateTime dataConclusao, decimal percentual)
        {
            this.prioridade = prioridade;
            this.titulo = titulo;
            this.dataCriacao = dataCriacao;
            this.dataConclusao = dataConclusao;
            this.percentualConclusao = percentual;
            this.concluida = false;
            this.itens = new List<Item>();
        }

        public Tarefa(PrioridadeEnum prioridade, string titulo, DateTime dataCriacao, DateTime dataConclusao, decimal percentual, List<Item> itens)
        {
            this.prioridade = prioridade;
            this.titulo = titulo;
            this.dataCriacao = dataCriacao;
            this.dataConclusao = dataConclusao;
            this.percentualConclusao = percentual;
            this.concluida = false;
            this.itens = itens;
        }

        public override string ToString()
        {
            string status = concluida ? "Concluída" : "Pendente";
            return "ID:" + id + "\n" +
                   "Titulo: " + titulo + "\n" +
                   "Criação: " + dataCriacao + "\n" +
                   "Conclusão: " + dataConclusao + "\n" +
                   "Percentual: " + percentualConclusao + "%\n" +
                   "Estado: " + status + "\n" +
                   "Itens da tarefa: " + ToStringDeItens();
        }
        public string ToStringDeItens()
        {
            StringBuilder itemlist = new();
            foreach (Item item in itens)
            {
                string status = item.concluido ? "Concluído" : "Pendente";
                itemlist.Append(Environment.NewLine + item.id + " - " + item.Descricao + " - " + status + Environment.NewLine);
            }

            return itemlist.ToString();
        }
        public void AdicionarItem(Item item)
        {
            int contID = itens.Count;
            contID++;
            item.id = contID;
            this.itens.Add(item);
            AtualizarPercentual();
        }
        public void ConcluirItem(int idSelecionado)
        {
            if(this.itens[idSelecionado].concluido == true)
            {
                Notificador.ApresentarMensagem("Item já concluído, escolha outro!", TipoMensagemEnum.Erro);
                return;
            }
            this.itens[idSelecionado].concluido = true;
            AtualizarPercentual();
        }
        private void AtualizarPercentual()
        {
            int totalItens = this.itens.Count;
            int itensConcluidos = 0;
            foreach (Item item in itens)
            {
                if (item.concluido == true)
                    itensConcluidos++;
            }
            if (itensConcluidos == 0)
                return;
            this.percentualConclusao = 0;
            this.percentualConclusao = ((itensConcluidos * 100)/ totalItens); // 2 * 100 = 200 / 4 = 50
            if (this.percentualConclusao == 100)
                this.concluida = true;
        }
    }
}