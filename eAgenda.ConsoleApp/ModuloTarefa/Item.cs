using eAgenda.ConsoleApp.Compartilhado;
using System.Text;

namespace eAgenda.ConsoleApp.ModuloItem
{
    public class Item : EntidadeBase
    {
        private readonly string descricao;
        public bool concluido;

        public string Descricao { get => descricao; }
        public Item(string descricao, bool concluido)
        {
            this.descricao = descricao;
            this.concluido = concluido;
        }

        public override string ToString()
        {
            string status = concluido ? "Concluído" : "Pendente";
            return "ID: " + id + "\n" +
                   "Descrição: " + Descricao + "\n" +
                   "Status: " + status;
        }
    }
}