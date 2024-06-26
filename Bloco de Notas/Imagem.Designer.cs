namespace Bloco_de_Notas
{
    partial class Imagem
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Imagem));
            pctrBx = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pctrBx).BeginInit();
            SuspendLayout();
            // 
            // pctrBx
            // 
            pctrBx.InitialImage = (Image)resources.GetObject("pctrBx.InitialImage");
            pctrBx.Location = new Point(1, 1);
            pctrBx.Name = "pctrBx";
            pctrBx.Size = new Size(1012, 451);
            pctrBx.SizeMode = PictureBoxSizeMode.CenterImage;
            pctrBx.TabIndex = 0;
            pctrBx.TabStop = false;
            // 
            // Imagem
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1014, 451);
            Controls.Add(pctrBx);
            Name = "Imagem";
            Text = "Imagem";
            ((System.ComponentModel.ISupportInitialize)pctrBx).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public PictureBox pctrBx;
    }
}