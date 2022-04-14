using eAgenda.ConsoleApp.ModuloItem;
using System;
using System.Collections.Generic;


namespace eAgenda.ConsoleApp.Compartilhado
{
    public abstract class RepositorioBase<T> where T : EntidadeBase
    {
        protected readonly List<T> registros;
        protected int contadorID;

        #region Construtor
        public RepositorioBase()
        {
            registros = new List<T>();
        }
        #endregion

        public virtual void Inserir(T registro)
        {
            registro.id = ++contadorID;
            registros.Add(registro);
        }

        public virtual bool Editar(int idSelecionado, T novoRegistro)
        {
            novoRegistro.id = idSelecionado;
            registros.Insert(novoRegistro.id, novoRegistro);
            return registros.Remove(registros.Find(x => x.id == idSelecionado));
        }

        public bool Excluir(int idSelecionado)
        {
            return registros.Remove(registros.Find(x => x.id == idSelecionado));
        }

        public List<T> SelecionarTodos() // Auxiliar para o Visualizar presente em cada Tela
        {
            return registros;
        }

        public T SelecionarRegistro(int idSelecionado)
        {
            foreach (T registro in registros)
            {
                if (idSelecionado == registro.id)
                    return registro;
            }
            return null;
        }

        public bool ExisteRegistro(int idSelecionado)
        {
            return registros.Exists(x => x.id == idSelecionado);
        }

        public int TemRegistros()
        {
            return registros.Count;
        }
    }
}