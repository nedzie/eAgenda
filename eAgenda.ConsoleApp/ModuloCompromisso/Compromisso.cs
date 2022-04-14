using eAgenda.ConsoleApp.Compartilhado;
using eAgenda.ConsoleApp.ModuloContato;
using System;
using System.Collections.Generic;

namespace eAgenda.ConsoleApp.ModuloCompromisso
{
    public class Compromisso : EntidadeBase
    {
        #region Atributos
        private readonly string assunto;
        private readonly string local;
        private readonly DateTime dataInicio;
        private readonly DateTime dataFim;
        private readonly Contato contato;
        #endregion

        #region Construtor
        public Compromisso(string assunto, string local, DateTime dataInicio, DateTime dataFim, Contato contato)
        {
            this.assunto = assunto;
            this.local = local;
            this.dataInicio = dataInicio;
            this.dataFim = dataFim;
            this.contato = contato;
        }
        #endregion


        #region Métodos privados
        public override string ToString()
        {
            return "id: " + id + "\n" +
                   "Assunto: " + assunto + "\n" +
                   "Local: " + local + "\n" +
                   "Data: " + dataInicio.Day + "\n" +
                   "Data inicial: " + dataInicio + "\n" +
                   "Data fim: " + dataFim + "\n" +
                   "Contato: " + contato.Nome + "\n";
        }
        #endregion
    }
}