using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08_10___Projeto_Atendimento
{
    public class Senha
    {
        private int id;
        private DateTime dataGerac;
        private DateTime horaGerac;
        private DateTime dataAtend;
        private DateTime horaAtend;

        public int Id
        {
            get { return id; }
        }

        public DateTime DataGerac
        {
            get { return dataGerac; }
        }

        public DateTime HoraGerac
        {
            get { return horaGerac; }
        }

        public DateTime DataAtend
        {
            get { return dataAtend; }
        }

        public DateTime HoraAtend
        {
            get { return horaAtend; }
        }

        public Senha(int id)
        {
            this.id = id;
            dataGerac = DateTime.Today;
            horaGerac = DateTime.Now;
            dataAtend = DateTime.MinValue;
            horaAtend = DateTime.MinValue;
        }

        public void RegistrarAtendimento()
        {
            dataAtend = DateTime.Today;
            horaAtend = DateTime.Now;
        }

        public string dadosParciais()
        {
            return id + " - " +
                   dataGerac.ToString("dd/MM/yyyy") + " - " +
                   horaGerac.ToString("HH:mm:ss");
        }

        public string dadosCompletos()
        {
            string dataAtendimento =
                dataAtend == DateTime.MinValue
                ? "Pendente"
                : dataAtend.ToString("dd/MM/yyyy");

            string horaAtendimento =
                horaAtend == DateTime.MinValue
                ? "Pendente"
                : horaAtend.ToString("HH:mm:ss");

            return id + " - " +
                   dataGerac.ToString("dd/MM/yyyy") + " - " +
                   horaGerac.ToString("HH:mm:ss") + " - " +
                   dataAtendimento + " - " +
                   horaAtendimento;
        }

        public override string ToString()
        {
            return dadosParciais();
        }
    }
}
