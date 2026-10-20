using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08_10___Projeto_Atendimento
{
    public class Guiche
    {
        private int id;
        private Queue<Senha> atendimentos;

        public int Id
        {
            get { return id; }
        }

        public Queue<Senha> Atendimentos
        {
            get { return new Queue<Senha>(atendimentos); }
        }

        public Guiche()
        {
            id = 0;
            atendimentos = new Queue<Senha>();
        }

        public Guiche(int id)
        {
            this.id = id;
            atendimentos = new Queue<Senha>();
        }

        public bool chamar(Queue<Senha> filaSenhas)
        {
            if (filaSenhas == null || filaSenhas.Count == 0)
                return false;

            Senha senha = filaSenhas.Dequeue();
            senha.RegistrarAtendimento();
            atendimentos.Enqueue(senha);

            return true;
        }

        // Sobrecarga para trabalhar diretamente com
        // a classe Senhas, sem expor a fila interna.
        public bool chamar(Senhas senhas)
        {
            if (senhas == null)
                return false;

            Senha senha;

            if (!senhas.RetirarProxima(out senha))
                return false;

            senha.RegistrarAtendimento();
            atendimentos.Enqueue(senha);

            return true;
        }

        public override string ToString()
        {
            return "Guichê " + id;
        }
    }
}
