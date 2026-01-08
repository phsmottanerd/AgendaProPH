using AgendaProPH.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AgendaProPH
{
    public partial class Form1 : Form
    {
        private List<Projeto> listaProjetos = new List<Projeto>();
        private Projeto? projetoSelecionado = null;
        private int projetoCounter = 1;
        private int tarefaCounter = 1;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load!;
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            ConfigurarForm();
        }

        private void ConfigurarForm()
        {
            // Configurações da janela principal
            this.Text = "AgendaProPH - Gerenciador de Projetos";
            this.Size = new Size(1200, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 240);

            // Painel principal
            Panel panelPrincipal = new Panel();
            panelPrincipal.Dock = DockStyle.Fill;
            panelPrincipal.BackColor = Color.FromArgb(240, 240, 240);
            this.Controls.Add(panelPrincipal);

            // Painel superior - Gerenciar Projetos
            GroupBox gbProjetos = CriarGrupoProjetos();
            gbProjetos.Location = new Point(15, 15);
            gbProjetos.Size = new Size(450, 280);
            panelPrincipal.Controls.Add(gbProjetos);

            // DataGridView de Projetos
            DataGridView dgvProjetos = CriarDataGridViewProjetos();
            dgvProjetos.Location = new Point(15, 310);
            dgvProjetos.Size = new Size(450, 400);
            panelPrincipal.Controls.Add(dgvProjetos);

            // Painel de Tarefas (lado direito)
            GroupBox gbTarefas = CriarGrupoTarefas();
            gbTarefas.Location = new Point(480, 15);
            gbTarefas.Size = new Size(700, 280);
            panelPrincipal.Controls.Add(gbTarefas);

            // DataGridView de Tarefas
            DataGridView dgvTarefas = CriarDataGridViewTarefas();
            dgvTarefas.Location = new Point(480, 310);
            dgvTarefas.Size = new Size(700, 400);
            panelPrincipal.Controls.Add(dgvTarefas);
        }

        private GroupBox CriarGrupoProjetos()
        {
            GroupBox gb = new GroupBox();
            gb.Text = "Cadastrar Projeto";
            gb.ForeColor = Color.FromArgb(40, 40, 40);
            gb.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            int yPos = 25;
            int xPos = 15;

            // Titulo
            Label lblTitulo = new Label() { Text = "Título:", Location = new Point(xPos, yPos), AutoSize = true };
            TextBox txtTitulo = new TextBox() { Name = "txtTitulo", Location = new Point(xPos + 90, yPos), Width = 330, Height = 30 };
            gb.Controls.Add(lblTitulo);
            gb.Controls.Add(txtTitulo);

            yPos += 40;

            // Descrição
            Label lblDescricao = new Label() { Text = "Descrição:", Location = new Point(xPos, yPos), AutoSize = true };
            TextBox txtDescricao = new TextBox() { Name = "txtDescricao", Location = new Point(xPos + 90, yPos), Width = 330, Height = 30, Multiline = true };
            gb.Controls.Add(lblDescricao);
            gb.Controls.Add(txtDescricao);

            yPos += 45;

            // Responsável
            Label lblResponsavel = new Label() { Text = "Responsável:", Location = new Point(xPos, yPos), AutoSize = true };
            TextBox txtResponsavel = new TextBox() { Name = "txtResponsavel", Location = new Point(xPos + 90, yPos), Width = 330, Height = 30 };
            gb.Controls.Add(lblResponsavel);
            gb.Controls.Add(txtResponsavel);

            yPos += 40;

            // Status
            Label lblStatus = new Label() { Text = "Status:", Location = new Point(xPos, yPos), AutoSize = true };
            ComboBox cmbStatus = new ComboBox()
            {
                Name = "cmbStatusProjeto",
                Location = new Point(xPos + 90, yPos),
                Width = 330,
                Height = 30,
                DropDownStyle = ComboBoxStyle.DropDown
            };
            cmbStatus.Items.AddRange(new string[] { "Pendente", "Andamento", "Concluído" });
            cmbStatus.SelectedIndex = 0;
            gb.Controls.Add(lblStatus);
            gb.Controls.Add(cmbStatus);

            yPos += 45;

            // Botão Cadastrar
            Button btnCadastrarProjeto = new Button()
            {
                Text = "Cadastrar Projeto",
                Location = new Point(xPos + 90, yPos),
                Width = 330,
                Height = 35,
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Name = "btnCadastrarProjeto"
            };
            btnCadastrarProjeto.FlatAppearance.BorderSize = 0;
            btnCadastrarProjeto.Click += BtnCadastrarProjeto_Click;
            gb.Controls.Add(btnCadastrarProjeto);

            return gb;
        }

        private GroupBox CriarGrupoTarefas()
        {
            GroupBox gb = new GroupBox();
            gb.Text = "Adicionar Tarefa ao Projeto Selecionado";
            gb.ForeColor = Color.FromArgb(40, 40, 40);
            gb.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            int yPos = 25;
            int xPos = 15;

            // ID Tarefa
            Label lblIdTarefa = new Label() { Text = "ID:", Location = new Point(xPos, yPos), AutoSize = true };
            TextBox txtIdTarefa = new TextBox() { Name = "txtIdTarefa", Location = new Point(xPos + 70, yPos), Width = 60, Height = 30, ReadOnly = true };
            gb.Controls.Add(lblIdTarefa);
            gb.Controls.Add(txtIdTarefa);

            // Título Tarefa
            Label lblTituloTarefa = new Label() { Text = "Título:", Location = new Point(xPos + 150, yPos), AutoSize = true };
            TextBox txtTituloTarefa = new TextBox() { Name = "txtTituloTarefa", Location = new Point(xPos + 220, yPos), Width = 460, Height = 30 };
            gb.Controls.Add(lblTituloTarefa);
            gb.Controls.Add(txtTituloTarefa);

            yPos += 40;

            // Descrição Tarefa
            Label lblDescricaoTarefa = new Label() { Text = "Descrição:", Location = new Point(xPos, yPos), AutoSize = true };
            TextBox txtDescricaoTarefa = new TextBox() { Name = "txtDescricaoTarefa", Location = new Point(xPos + 80, yPos), Width = 600, Height = 40, Multiline = true };
            gb.Controls.Add(lblDescricaoTarefa);
            gb.Controls.Add(txtDescricaoTarefa);

            yPos += 50;

            // Responsável Tarefa
            Label lblResponsavelTarefa = new Label() { Text = "Responsável:", Location = new Point(xPos, yPos), AutoSize = true };
            TextBox txtResponsavelTarefa = new TextBox() { Name = "txtResponsavelTarefa", Location = new Point(xPos + 100, yPos), Width = 290, Height = 30 };
            gb.Controls.Add(lblResponsavelTarefa);
            gb.Controls.Add(txtResponsavelTarefa);

            // Status Tarefa
            Label lblStatusTarefa = new Label() { Text = "Status:", Location = new Point(xPos + 410, yPos), AutoSize = true };
            ComboBox cmbStatusTarefa = new ComboBox()
            {
                Name = "cmbStatusTarefa",
                Location = new Point(xPos + 480, yPos),
                Width = 200,
                Height = 30,
                DropDownStyle = ComboBoxStyle.DropDown
            };
            cmbStatusTarefa.Items.AddRange(new string[] { "Pendente", "Andamento", "Concluída" });
            cmbStatusTarefa.SelectedIndex = 0;
            gb.Controls.Add(lblStatusTarefa);
            gb.Controls.Add(cmbStatusTarefa);

            yPos += 40;

            // Data Entrega
            Label lblDataEntrega = new Label() { Text = "Data Entrega:", Location = new Point(xPos, yPos), AutoSize = true };
            DateTimePicker dtpDataEntrega = new DateTimePicker() { Name = "dtpDataEntrega", Location = new Point(xPos + 100, yPos), Width = 290, Height = 30 };
            gb.Controls.Add(lblDataEntrega);
            gb.Controls.Add(dtpDataEntrega);

            yPos += 40;

            // Botões de Ação
            Button btnAdicionarTarefa = new Button()
            {
                Text = "Adicionar Tarefa",
                Location = new Point(xPos + 80, yPos),
                Width = 290,
                Height = 35,
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Name = "btnAdicionarTarefa"
            };
            btnAdicionarTarefa.FlatAppearance.BorderSize = 0;
            btnAdicionarTarefa.Click += BtnAdicionarTarefa_Click;
            gb.Controls.Add(btnAdicionarTarefa);

            Button btnListarTarefas = new Button()
            {
                Text = "Listar Tarefas",
                Location = new Point(xPos + 390, yPos),
                Width = 290,
                Height = 35,
                BackColor = Color.FromArgb(155, 89, 182),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Name = "btnListarTarefas"
            };
            btnListarTarefas.FlatAppearance.BorderSize = 0;
            btnListarTarefas.Click += BtnListarTarefas_Click;
            gb.Controls.Add(btnListarTarefas);

            return gb;
        }

        private DataGridView CriarDataGridViewProjetos()
        {
            DataGridView dgv = new DataGridView();
            dgv.Name = "dgvProjetos";
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.BackgroundColor = Color.White;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.RowHeadersVisible = false;

            dgv.Columns.Add("ID", "ID");
            dgv.Columns.Add("Titulo", "Título");
            dgv.Columns.Add("Descricao", "Descrição");
            dgv.Columns.Add("Responsavel", "Responsável");
            dgv.Columns.Add("Status", "Status");
            dgv.Columns.Add("NumTarefas", "Nº Tarefas");

            dgv.Columns["ID"].Width = 40;
            dgv.Columns["Titulo"].Width = 100;
            dgv.Columns["Descricao"].Width = 120;
            dgv.Columns["Responsavel"].Width = 100;
            dgv.Columns["Status"].Width = 90;
            dgv.Columns["NumTarefas"].Width = 80;

            dgv.SelectionChanged += DgvProjetos_SelectionChanged;

            return dgv;
        }

        private DataGridView CriarDataGridViewTarefas()
        {
            DataGridView dgv = new DataGridView();
            dgv.Name = "dgvTarefas";
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.BackgroundColor = Color.White;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(155, 89, 182);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.RowHeadersVisible = false;

            dgv.Columns.Add("ID", "ID");
            dgv.Columns.Add("Titulo", "Título");
            dgv.Columns.Add("Descricao", "Descrição");
            dgv.Columns.Add("Responsavel", "Responsável");
            dgv.Columns.Add("Status", "Status");
            dgv.Columns.Add("DataEntrega", "Data Entrega");

            dgv.Columns["ID"].Width = 40;
            dgv.Columns["Titulo"].Width = 100;
            dgv.Columns["Descricao"].Width = 120;
            dgv.Columns["Responsavel"].Width = 100;
            dgv.Columns["Status"].Width = 90;
            dgv.Columns["DataEntrega"].Width = 100;

            return dgv;
        }

        private void BtnCadastrarProjeto_Click(object? sender, EventArgs e)
        {
            // Validar campos
            if (!ValidarCamposProjeto())
                return;

            // Criar novo projeto
            Projeto novoProjeto = new Projeto
            {
                ID = projetoCounter++,
                Titulo = GetTextBoxValue("txtTitulo"),
                Descricao = GetTextBoxValue("txtDescricao"),
                Responsavel = GetTextBoxValue("txtResponsavel"),
                Status = GetComboBoxValue("cmbStatusProjeto"),
                ListaTarefas = new List<Tarefa>()
            };

            listaProjetos.Add(novoProjeto);

            MessageBox.Show($"Projeto '{novoProjeto.Titulo}' cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Limpar campos
            LimparCamposProjeto();

            // Atualizar DataGridView
            AtualizarDataGridViewProjetos();
        }

        private void BtnAdicionarTarefa_Click(object? sender, EventArgs e)
        {
            if (projetoSelecionado == null)
            {
                MessageBox.Show("Selecione um projeto primeiro!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarCamposTarefa())
                return;

            // Criar nova tarefa
            Tarefa novaTarefa = new Tarefa
            {
                ID = tarefaCounter++,
                Titulo = GetTextBoxValue("txtTituloTarefa"),
                Descricao = GetTextBoxValue("txtDescricaoTarefa"),
                Responsavel = GetTextBoxValue("txtResponsavelTarefa"),
                Status = GetComboBoxValue("cmbStatusTarefa"),
                DataEntrega = GetDateTimePickerValue("dtpDataEntrega")
            };

            projetoSelecionado.ListaTarefas.Add(novaTarefa);

            MessageBox.Show($"Tarefa '{novaTarefa.Titulo}' adicionada ao projeto '{projetoSelecionado.Titulo}'!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Limpar campos
            LimparCamposTarefa();

            // Atualizar DataGridViews
            AtualizarDataGridViewProjetos();
        }

        private void BtnListarTarefas_Click(object? sender, EventArgs e)
        {
            if (projetoSelecionado == null)
            {
                MessageBox.Show("Selecione um projeto primeiro!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AtualizarDataGridViewTarefas();
        }

        private void DgvProjetos_SelectionChanged(object? sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (dgv != null && dgv.SelectedRows.Count > 0)
            {
                object? idObj = dgv.SelectedRows[0].Cells["ID"].Value;
                if (idObj != null && int.TryParse(idObj.ToString(), out int projetoID))
                {
                    projetoSelecionado = listaProjetos.Find(p => p.ID == projetoID);

                    if (projetoSelecionado != null)
                    {
                        AtualizarDataGridViewTarefas();
                    }
                }
            }
        }

        private void AtualizarDataGridViewProjetos()
        {
            Control[] controls = this.Controls.Find("dgvProjetos", true);
            if (controls.Length > 0 && controls[0] is DataGridView dgv)
            {
                dgv.Rows.Clear();

                foreach (var projeto in listaProjetos)
                {
                    dgv.Rows.Add(projeto.ID, projeto.Titulo, projeto.Descricao, projeto.Responsavel, projeto.Status, projeto.ListaTarefas.Count);
                }
            }
        }

        private void AtualizarDataGridViewTarefas()
        {
            Control[] controls = this.Controls.Find("dgvTarefas", true);
            if (controls.Length > 0 && controls[0] is DataGridView dgv)
            {
                dgv.Rows.Clear();

                if (projetoSelecionado != null)
                {
                    foreach (var tarefa in projetoSelecionado.ListaTarefas)
                    {
                        dgv.Rows.Add(tarefa.ID, tarefa.Titulo, tarefa.Descricao, tarefa.Responsavel, tarefa.Status, tarefa.DataEntrega.ToShortDateString());
                    }
                }
            }
        }

        private bool ValidarCamposProjeto()
        {
            string titulo = GetTextBoxValue("txtTitulo");
            string descricao = GetTextBoxValue("txtDescricao");
            string responsavel = GetTextBoxValue("txtResponsavel");

            if (string.IsNullOrWhiteSpace(titulo))
            {
                MessageBox.Show("Preencha o Título do Projeto!", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(descricao))
            {
                MessageBox.Show("Preencha a Descrição do Projeto!", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(responsavel))
            {
                MessageBox.Show("Preencha o Responsável do Projeto!", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool ValidarCamposTarefa()
        {
            string titulo = GetTextBoxValue("txtTituloTarefa");
            string descricao = GetTextBoxValue("txtDescricaoTarefa");
            string responsavel = GetTextBoxValue("txtResponsavelTarefa");

            if (string.IsNullOrWhiteSpace(titulo))
            {
                MessageBox.Show("Preencha o Título da Tarefa!", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(descricao))
            {
                MessageBox.Show("Preencha a Descrição da Tarefa!", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(responsavel))
            {
                MessageBox.Show("Preencha o Responsável da Tarefa!", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void LimparCamposProjeto()
        {
            Control[] controls = this.Controls.Find("txtTitulo", true);
            if (controls.Length > 0 && controls[0] is TextBox txt1) txt1.Text = "";

            controls = this.Controls.Find("txtDescricao", true);
            if (controls.Length > 0 && controls[0] is TextBox txt2) txt2.Text = "";

            controls = this.Controls.Find("txtResponsavel", true);
            if (controls.Length > 0 && controls[0] is TextBox txt3) txt3.Text = "";

            controls = this.Controls.Find("cmbStatusProjeto", true);
            if (controls.Length > 0 && controls[0] is ComboBox cmb) cmb.SelectedIndex = 0;
        }

        private void LimparCamposTarefa()
        {
            Control[] controls = this.Controls.Find("txtIdTarefa", true);
            if (controls.Length > 0 && controls[0] is TextBox txt1) txt1.Text = tarefaCounter.ToString();

            controls = this.Controls.Find("txtTituloTarefa", true);
            if (controls.Length > 0 && controls[0] is TextBox txt2) txt2.Text = "";

            controls = this.Controls.Find("txtDescricaoTarefa", true);
            if (controls.Length > 0 && controls[0] is TextBox txt3) txt3.Text = "";

            controls = this.Controls.Find("txtResponsavelTarefa", true);
            if (controls.Length > 0 && controls[0] is TextBox txt4) txt4.Text = "";

            controls = this.Controls.Find("cmbStatusTarefa", true);
            if (controls.Length > 0 && controls[0] is ComboBox cmb1) cmb1.SelectedIndex = 0;

            controls = this.Controls.Find("dtpDataEntrega", true);
            if (controls.Length > 0 && controls[0] is DateTimePicker dtp) dtp.Value = DateTime.Now;
        }

        private string GetTextBoxValue(string controlName)
        {
            Control[] controls = this.Controls.Find(controlName, true);
            if (controls.Length > 0 && controls[0] is TextBox txt)
                return txt.Text;
            return "";
        }

        private string GetComboBoxValue(string controlName)
        {
            Control[] controls = this.Controls.Find(controlName, true);
            if (controls.Length > 0 && controls[0] is ComboBox cmb)
                return cmb.SelectedItem?.ToString() ?? "";
            return "";
        }

        private DateTime GetDateTimePickerValue(string controlName)
        {
            Control[] controls = this.Controls.Find(controlName, true);
            if (controls.Length > 0 && controls[0] is DateTimePicker dtp)
                return dtp.Value;
            return DateTime.Now;
        }
    }
}
