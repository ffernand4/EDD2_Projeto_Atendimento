using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08_10___Projeto_Atendimento
{
    public class Senhas
    {
        private int proximoAtendimento;
        private Queue<Senha> filaSenhas;

        public Senhas()
        {
            proximoAtendimento = 1;
            filaSenhas = new Queue<Senha>();
        }

        public void gerar()
        {
            Senha senha = new Senha(proximoAtendimento);
            filaSenhas.Enqueue(senha);
            proximoAtendimento++;
        }

        public int Quantidade
        {
            get { return filaSenhas.Count; }
        }

        public bool Vazia
        {
            get { return filaSenhas.Count == 0; }
        }

        public Queue<Senha> FilaSenhas
        {
            get { return new Queue<Senha>(filaSenhas); }
        }

        public Senha Proxima()
        {
            if (filaSenhas.Count == 0)
                return null;

            return filaSenhas.Peek();
        }

        public bool RetirarProxima(out Senha senha)
        {
            if (filaSenhas.Count == 0)
            {
                senha = null;
                return false;
            }

            senha = filaSenhas.Dequeue();
            return true;
        }
    }
}
