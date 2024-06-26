using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Bloco_de_Notas
{
    public partial class Form1 : Form
    {
        //Para indicar se h� alguma altera��o no texto
        bool alterado = false;
        //V�riavel zoom que por padr�o ter� o valor 100
        int zoom = 100;
        public Form1()
        {
            InitializeComponent();
            this.Text = "";
            rchTxtBx.TextChanged += rchTxtBx_TextChanged;
            //Mostra a barra de status como ativada
            barraDeStatusToolStripMenuItem.Checked = true;
        }

        private void atualizaPosicao()
        {
            //Aqui iremos obter a localiza��o da linha e coluna atual que estamos digitando
            int linha = rchTxtBx.GetLineFromCharIndex(rchTxtBx.SelectionStart);
            int coluna = rchTxtBx.SelectionStart - rchTxtBx.GetFirstCharIndexFromLine(linha);


            //E aqui ser� na barra de status ser� atualizado com essas localiza��es
            tlStrpSttsLblCursor.Text = "Ln: " + (linha + 1).ToString() + " Col: " + coluna.ToString();
        }

        private void rchTxtBx_TextChanged(object sender, EventArgs e)
        {
            alterado = true;
            this.atualizaPosicao();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {   //Verifica se o texto foi alterado
            if (alterado)
            {
                if (MessageBox.Show("Seu arquivo foi alterado. Deseja salvar?", "Bloco de Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //Verifica se o nome do arquivo n�o est� vazio
                    if (this.Text != "")
                    {
                        this.salvar(this.Text);
                    }
                    else
                    {
                        //Se estiver tudo ok, ele chama o salvarComo e salva o arquivo
                        this.salvarComo();
                    }
                }
            }
            //Abre o explorador de arquivos
            if (opnFlDlgAbrir.ShowDialog() == DialogResult.OK)
            {
                //Pega o caminho/diret�rio do arquivo escolhido
                string filePath = opnFlDlgAbrir.FileName;
                //Coloca o nome do arquivo como titulo da janela
                this.Text = opnFlDlgAbrir.FileName;

                //Verifica se a string n�o � vazia
                if (!string.IsNullOrEmpty(filePath))
                {
                    //Verifica o tipo do arquivo
                    string fileExtension = Path.GetExtension(filePath);

                    //Verifica o tipo do arquivo se � .txt ou .rtf
                    if (fileExtension.Equals(".txt", StringComparison.OrdinalIgnoreCase) || fileExtension.Equals(".rtf", StringComparison.OrdinalIgnoreCase))
                    {
                        //Para ler todo o conte�do e colocar no richTextBox
                        using (StreamReader reader = new StreamReader(filePath))
                        {
                            //Se o tipo do arquivo for txt, ir� colocar o texto no RichTextBox
                            if (fileExtension.Equals(".txt", StringComparison.OrdinalIgnoreCase))
                            {
                                rchTxtBx.Text = reader.ReadToEnd();
                            }
                            //Se o tipo do arquivo for rtf, ir� colocar o texto Rtf no RichTextBox
                            else if (fileExtension.Equals(".rtf", StringComparison.OrdinalIgnoreCase))
                            {
                                rchTxtBx.Rtf = reader.ReadToEnd();
                            }
                            alterado = false;
                        }
                    }
                    else
                    {
                        //Cria uma nova janela, com o nome de Imagem
                        Form formImagem = new Form();
                        formImagem.Text = "Imagem";
                        //Controle de exibi��o
                        PictureBox pictureBox = new PictureBox();
                        //Preenche o espa�o disponivel no Forms Imagem
                        pictureBox.Dock = DockStyle.Fill;
                        //Carrega a imagem
                        pictureBox.Image = Image.FromFile(filePath);
                        //Adiciona a imagem no Forms Imagem
                        formImagem.Controls.Add(pictureBox);
                        //Abre a imagem ampliada
                        pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                        //Exibe o Forms Imagem como uma nova janela
                        formImagem.Show();
                    }
                }
            }
        }

        private void salvar(String arquivo)
        //Salvar arquivo
        {
            if (arquivo != "")
            {
                try
                {
                    //Salva o conte�do do RichTextBox no arquivo especificado
                    rchTxtBx.SaveFile(arquivo, RichTextBoxStreamType.RichText);
                    //Isso far� o nome da janela ser o nome do arquivo
                    this.Text = arquivo;
                    // Indica que o arquivo salvo n�o est� mais alterado
                    alterado = false;
                }
                //Caso tenho algum erro ao salvar, esse catch ir� fazer aparecer uma mensagem de erro
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar o arquivo: " + ex.Message, "Salvar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //Se o nome do arquivo for inv�lido, cair� aqui
            else
            {
                MessageBox.Show("Nome de arquivo � inv�lido.", "Salvar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void salvarComo()
        {
            if (svFlDlg.ShowDialog() == DialogResult.OK)
            {
                this.salvar(svFlDlg.FileName);
            }
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Text))
            {
                this.salvar(this.Text);
            }
            else
            {
                this.salvarComo();
            }
        }

        private void salvarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.salvarComo();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void desfazerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Desfaz a �ltima opera��o no RichTextBox
            rchTxtBx.Undo();
        }

        private void recortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rchTxtBx.SelectedRtf != "")
            {
                //Aqui ele vai salvar o texto recortado no Clipboard
                Clipboard.SetDataObject(rchTxtBx.SelectedRtf);
                //Aqui ele apagar� o texto recortado do RichTextBox
                rchTxtBx.SelectedRtf = "";
            }
        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rchTxtBx.SelectedRtf != "")
            {
                //Aqui ele vai salvar o texto recortado no Clipboard
                Clipboard.SetDataObject(rchTxtBx.SelectedRtf);
            }
        }

        private void colarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Verifica se h� texto no Clipboard
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            {
                //Cola o texto do Clipboard no RichTextBox
                rchTxtBx.SelectedRtf = Clipboard.GetData(DataFormats.Text) as string;
            }
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rchTxtBx.SelectedRtf != "")
            {
                rchTxtBx.SelectedRtf = "";
            }
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rchTxtBx.SelectedText != "")
            {
                //Para poder modificar os espa�os sem mudar o conteudo do RichTextBox
                string texto = rchTxtBx.SelectedText;
                //Troca todos os espa�os em branco para um sinal de +
                texto.Replace(' ', '+');
                string escapedQuery = Uri.EscapeDataString(texto);
                string url = "https://www.google.com/search?q=" + escapedQuery;

                System.Diagnostics.Process.Start(new ProcessStartInfo("cmd", $"/c start {url}")
                {
                    //Para n�o abrir nenhuma janela do cmd na execu��o
                    CreateNoWindow = true
                });
            }
            else
            {
                MessageBox.Show("Voc� n�o selecionou nada para a pesquisa.", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void selecionarTudoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rchTxtBx.SelectAll();
        }

        private void horaDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Vai quebrar a linha e adicionar a data/hora atual
            rchTxtBx.SelectedText = System.Environment.NewLine + DateTime.Now;
        }

        private void quebraAutomaticaDaLinhaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Verifica a op��o da quebra de linha
            if (quebraAutomaticaDaLinhaToolStripMenuItem.CheckState == CheckState.Checked)
            {
                //Se a quebra de linha estiver desmarcada, ela ficar� desativada
                quebraAutomaticaDaLinhaToolStripMenuItem.CheckState = CheckState.Unchecked;
                //Desativa a quebra de linha
                rchTxtBx.WordWrap = false;
            }
            else
            {
                //Se a quebra de linha estiver marcada, ela ficar� ativa
                quebraAutomaticaDaLinhaToolStripMenuItem.CheckState = CheckState.Checked;
                //Ativa a quebra de linha
                rchTxtBx.WordWrap = true;
            }
        }

        private void conToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pgStpDlg.ShowDialog();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prntDlg.ShowDialog();
        }

        private void novaJanelaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (alterado)
            {
                if (MessageBox.Show("Seu arquivo foi alterado. Deseja salvar?", "Bloco de Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (this.Text != "")
                    {
                        this.salvar(this.Text);
                    }
                    else
                    {
                        this.salvarComo();
                    }
                }
            }
            //Abre o explorador de arquivos
            if (opnFlDlgAbrir.ShowDialog() == DialogResult.OK)
            {
                //Pega o caminho/diret�rio do arquivo escolhido
                string filePath = opnFlDlgAbrir.FileName;
                //Coloca o nome do arquivo como titulo da janela
                this.Text = opnFlDlgAbrir.FileName;

                //Verifica se a string n�o � vazia
                if (!string.IsNullOrEmpty(filePath))
                {
                    //Verifica o tipo do arquivo
                    string fileExtension = Path.GetExtension(filePath);

                    //Verifica o tipo do arquivo se � .txt ou .rtf
                    if (fileExtension.Equals(".txt", StringComparison.OrdinalIgnoreCase) || fileExtension.Equals(".rtf", StringComparison.OrdinalIgnoreCase))
                    {
                        //Para ler todo o conte�do e colocar no richTextBox
                        using (StreamReader reader = new StreamReader(filePath))
                        {
                            //Se o tipo do arquivo for txt, ir� colocar o texto no RichTextBox
                            if (fileExtension.Equals(".txt", StringComparison.OrdinalIgnoreCase))
                            {
                                rchTxtBx.Text = reader.ReadToEnd();
                            }
                            //Se o tipo do arquivo for rtf, ir� colocar o texto Rtf no RichTextBox
                            else if (fileExtension.Equals(".rtf", StringComparison.OrdinalIgnoreCase))
                            {
                                rchTxtBx.Rtf = reader.ReadToEnd();
                            }
                            alterado = false;
                        }
                    }
                    else
                    {
                        //Cria uma nova janela, com o nome de Imagem Aber
                        Form formImagem = new Form();
                        formImagem.Text = "Imagem";
                        //Controle de exibi��o
                        PictureBox pictureBox = new PictureBox();
                        //Preenche o espa�o disponivel no Forms Imagem
                        pictureBox.Dock = DockStyle.Fill;
                        //Carrega a imagem
                        pictureBox.Image = Image.FromFile(filePath);
                        //Adiciona a imagem no Forms Imagem
                        formImagem.Controls.Add(pictureBox);
                        //Abre a imagem ampliada
                        pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                        //Exibe o Forms Imagem como uma nova janela
                        formImagem.Show();
                    }
                }
            }
        }

        private void fonteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fntDlg.ShowDialog() == DialogResult.OK)
            {
                //Abre a caixinha de fontes do VisualStudio
                rchTxtBx.SelectionFont = fntDlg.Font;
            }
        }

        private void barraDeStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Verifica se a barra de status est� ativada ou n�o
            barraDeStatusToolStripMenuItem.Checked = !barraDeStatusToolStripMenuItem.Checked;
            //Mostra ou tira a barra de status
            sttsStrp.Visible = !sttsStrp.Visible;
        }

        private void atualizaZoom()
        {
            //Atualiza o nivel de zoom na barra de status
            tlStrpSttsLblZoom.Text = this.zoom.ToString() + "%";
        }

        private void ampliarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Aumenta o zoom em 1
            this.zoom++;
            rchTxtBx.Font = new Font(rchTxtBx.Font.FontFamily, rchTxtBx.Font.Size + 1, rchTxtBx.Font.Style);
            this.atualizaZoom();
        }

        private void reduzirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Diminui o zoom em 1
            this.zoom--;
            rchTxtBx.Font = new Font(rchTxtBx.Font.FontFamily, rchTxtBx.Font.Size - 1, rchTxtBx.Font.Style);
            this.atualizaZoom();
        }

        private void restaurarZoomPadraoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.zoom = 100;
            rchTxtBx.Font = new Font(rchTxtBx.Font.FontFamily, 12, rchTxtBx.Font.Style);
            this.atualizaZoom();
        }

        private void corToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clrDlg.ShowDialog() == DialogResult.OK)
            {
                //Abre a caixinha de cores do VisualStudio
                rchTxtBx.SelectionColor = clrDlg.Color;
            }
        }

        private void substituirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Substituir frm = new Substituir();
            frm.txtBx1.Text = rchTxtBx.SelectedText;
            frm.Show(this);
        }

        private void localizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Localizar frm = new Localizar();
            frm.txtBxLoc.Text = rchTxtBx.SelectedText;
            frm.Show(this);
        }

        private void sobreOBlocoDeNotasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Raul M. Rodrigues 04722-030 \n\n Maria Clara Crescenzio 04722-064 \n\n Marcelo Botelho de Freitas 04722-074 \n\n Gabriel Aparecido Cespedes Estevo 04722-036", "Criadores do projeto", MessageBoxButtons.OK);
        }

        private void exibirAjudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Se tiver qualquer d�vida, chame um integrante do grupo.", "Ajuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void centralizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Centraliza o texto no RichTextBox
            rchTxtBx.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void alinharAEsquerdaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Alinha o texto � esquerda no RichTextBox
            rchTxtBx.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void alinharADireitaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Alinha o texto � direita no RichTextBox
            rchTxtBx.SelectionAlignment = HorizontalAlignment.Right;
        }
    }
}