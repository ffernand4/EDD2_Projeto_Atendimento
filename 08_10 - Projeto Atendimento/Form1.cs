namespace _08_10___Projeto_Atendimento
{
    public partial class Form1 : Form
    {
        private Senhas senhas;
        private Guiches guiches;

        private NumericUpDown numQuantidade;
        private ComboBox cmbGuiche;
        private ListBox lstSenhas;
        private ListBox lstAtendimentos;
        private Label lblStatus;

        public Form1()
        {
            InitializeComponent();

            senhas = new Senhas();
            guiches = new Guiches();

            MontarTela();
            AtualizarSenhas();
            AtualizarGuiches();
        }

        private void MontarTela()
        {
            Text = "Central de Atendimento";
            Size = new Size(950, 650);
            MinimumSize = new Size(800, 550);
            StartPosition = FormStartPosition.CenterScreen;

            Label titulo = new Label();
            titulo.Text = "Central de Atendimento";
            titulo.Font = new Font("Arial", 18, FontStyle.Bold);
            titulo.AutoSize = true;
            titulo.Location = new Point(20, 15);
            Controls.Add(titulo);

            // Gerar senha
            Button btnGerar = new Button();
            btnGerar.Text = "Gerar senha";
            btnGerar.Location = new Point(20, 65);
            btnGerar.Size = new Size(130, 35);
            btnGerar.Click += (s, e) => GerarSenha();
            Controls.Add(btnGerar);

            // Quantidade de guichês
            Label lblQuantidade = new Label();
            lblQuantidade.Text = "Quantidade de guichês:";
            lblQuantidade.Location = new Point(180, 73);
            lblQuantidade.AutoSize = true;
            Controls.Add(lblQuantidade);

            numQuantidade = new NumericUpDown();
            numQuantidade.Location = new Point(330, 70);
            numQuantidade.Width = 65;
            numQuantidade.Minimum = 1;
            numQuantidade.Maximum = 50;
            numQuantidade.Value = 1;
            Controls.Add(numQuantidade);

            Button btnAdicionarGuiche = new Button();
            btnAdicionarGuiche.Text = "Adicionar guichês";
            btnAdicionarGuiche.Location = new Point(410, 65);
            btnAdicionarGuiche.Size = new Size(145, 35);
            btnAdicionarGuiche.Click +=
                (s, e) => AdicionarGuiches();
            Controls.Add(btnAdicionarGuiche);

            // Seleção do guichê
            Label lblGuiche = new Label();
            lblGuiche.Text = "Guichê:";
            lblGuiche.Location = new Point(20, 125);
            lblGuiche.AutoSize = true;
            Controls.Add(lblGuiche);

            cmbGuiche = new ComboBox();
            cmbGuiche.Location = new Point(85, 120);
            cmbGuiche.Width = 150;
            cmbGuiche.DropDownStyle =
                ComboBoxStyle.DropDownList;
            Controls.Add(cmbGuiche);

            Button btnChamar = new Button();
            btnChamar.Text = "Chamar próxima senha";
            btnChamar.Location = new Point(255, 117);
            btnChamar.Size = new Size(180, 35);
            btnChamar.Click += (s, e) => ChamarSenha();
            Controls.Add(btnChamar);

            // Lista de senhas aguardando
            GroupBox grupoSenhas = new GroupBox();
            grupoSenhas.Text = "Senhas aguardando atendimento";
            grupoSenhas.Location = new Point(20, 175);
            grupoSenhas.Size = new Size(420, 360);
            Controls.Add(grupoSenhas);

            lstSenhas = new ListBox();
            lstSenhas.Dock = DockStyle.Fill;
            lstSenhas.Font = new Font("Consolas", 10);
            grupoSenhas.Controls.Add(lstSenhas);

            // Histórico de atendimentos
            GroupBox grupoAtendimentos = new GroupBox();
            grupoAtendimentos.Text = "Histórico por guichê";
            grupoAtendimentos.Location = new Point(460, 175);
            grupoAtendimentos.Size = new Size(450, 360);
            Controls.Add(grupoAtendimentos);

            lstAtendimentos = new ListBox();
            lstAtendimentos.Dock = DockStyle.Fill;
            lstAtendimentos.Font = new Font("Consolas", 9);
            grupoAtendimentos.Controls.Add(lstAtendimentos);

            // Mensagem de status
            lblStatus = new Label();
            lblStatus.Text = "Sistema iniciado.";
            lblStatus.Location = new Point(20, 555);
            lblStatus.Size = new Size(880, 30);
            Controls.Add(lblStatus);
        }

        private void GerarSenha()
        {
            senhas.gerar();

            AtualizarSenhas();

            Senha proxima = senhas.Proxima();

            lblStatus.Text =
                "Senha gerada. Próxima da fila: " +
                (proxima == null ? "-" : proxima.Id.ToString());
        }

        private void AdicionarGuiches()
        {
            int quantidade = (int)numQuantidade.Value;
            int adicionados = 0;

            for (int i = 0; i < quantidade; i++)
            {
                int novoId = guiches.Quantidade + 1;

                Guiche guiche = new Guiche(novoId);
                guiches.adicionar(guiche);
                adicionados++;
            }

            AtualizarGuiches();

            lblStatus.Text =
                adicionados + " guichê(s) adicionado(s).";
        }

        private void ChamarSenha()
        {
            if (cmbGuiche.SelectedItem == null)
            {
                MessageBox.Show(
                    "Cadastre e selecione um guichê.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (senhas.Vazia)
            {
                MessageBox.Show(
                    "Não há senhas aguardando atendimento.",
                    "Fila vazia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            int idGuiche = (int)cmbGuiche.SelectedItem;
            Guiche guiche = guiches.BuscarPorId(idGuiche);

            if (guiche == null)
            {
                MessageBox.Show("Guichê não encontrado.");
                return;
            }

            Senha senhaChamada = senhas.Proxima();

            if (guiche.chamar(senhas))
            {
                AtualizarSenhas();
                AtualizarAtendimentos();

                lblStatus.Text =
                    "Senha " + senhaChamada.Id +
                    " chamada para o guichê " + guiche.Id + ".";
            }
        }

        private void AtualizarSenhas()
        {
            lstSenhas.Items.Clear();

            foreach (Senha senha in senhas.FilaSenhas)
            {
                lstSenhas.Items.Add(senha.dadosParciais());
            }
        }

        private void AtualizarGuiches()
        {
            int? selecionado = cmbGuiche.SelectedItem is int id
                ? id
                : (int?)null;

            cmbGuiche.Items.Clear();

            foreach (Guiche guiche in guiches.ListaGuiches)
            {
                cmbGuiche.Items.Add(guiche.Id);
            }

            if (selecionado.HasValue &&
                cmbGuiche.Items.Contains(selecionado.Value))
            {
                cmbGuiche.SelectedItem = selecionado.Value;
            }
            else if (cmbGuiche.Items.Count > 0)
            {
                cmbGuiche.SelectedIndex = 0;
            }

            AtualizarAtendimentos();
        }

        private void AtualizarAtendimentos()
        {
            lstAtendimentos.Items.Clear();

            foreach (Guiche guiche in guiches.ListaGuiches)
            {
                lstAtendimentos.Items.Add(
                    "===== GUICHÊ " + guiche.Id + " =====");

                if (guiche.Atendimentos.Count == 0)
                {
                    lstAtendimentos.Items.Add(
                        "Nenhum atendimento realizado.");
                }
                else
                {
                    foreach (Senha senha in guiche.Atendimentos)
                    {
                        lstAtendimentos.Items.Add(
                            senha.dadosCompletos());
                    }
                }

                lstAtendimentos.Items.Add("");
            }
        }
    }
}
