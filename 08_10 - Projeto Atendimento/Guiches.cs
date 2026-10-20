using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08_10___Projeto_Atendimento
{
    public class Guiches
    {
        private List<Guiche> listaGuiches;

        public Guiches()
        {
            listaGuiches = new List<Guiche>();
        }

        public void adicionar(Guiche guiche)
        {
            if (guiche == null)
                return;

            if (listaGuiches.Exists(g => g.Id == guiche.Id))
                return;

            listaGuiches.Add(guiche);
        }

        public IReadOnlyList<Guiche> ListaGuiches
        {
            get { return listaGuiches.AsReadOnly(); }
        }

        public Guiche BuscarPorId(int id)
        {
            return listaGuiches.Find(g => g.Id == id);
        }

        public int Quantidade
        {
            get { return listaGuiches.Count; }
        }
    }
}
