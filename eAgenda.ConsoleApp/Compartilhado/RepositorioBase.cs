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

        public virtual bool Excluir(int idSelecionado)
        {
            return registros.Remove(registros.Find(x => x.id == idSelecionado));
        }

        public List<T> SelecionarTodos() // Auxiliar para o Visualizar presente em cada Tela
        {
            return registros;
        }

        public List<T> Filtrar(Predicate<T> condicao)
        {
            List<T> registrosFiltrados = new List<T>();

            foreach (T registro in registros)
                if (condicao(registro))
                    registrosFiltrados.Add(registro);

            return registrosFiltrados;
        }

        public List<T> FiltrarEmIntervalo(Predicate<T> condicao, Predicate<T> condicao2)
        {
            List<T> registrosFiltrados = new List<T>();

            foreach (T registro in registros)
                if (condicao(registro) && condicao2(registro))
                    registrosFiltrados.Add(registro);

            return registrosFiltrados;
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

        public bool TemAlgo()
        {
            foreach (T item in registros)
            {
                if(item != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}