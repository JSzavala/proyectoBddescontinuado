namespace proyectoBddescontinuado
{
    partial class FrmLista
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
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.btnDescontinuar = new System.Windows.Forms.Button();
            this.txtBarras = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvProductos
            // 
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Location = new System.Drawing.Point(12, 12);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.Size = new System.Drawing.Size(865, 418);
            this.dgvProductos.TabIndex = 0;
            // 
            // btnDescontinuar
            // 
            this.btnDescontinuar.Location = new System.Drawing.Point(12, 457);
            this.btnDescontinuar.Name = "btnDescontinuar";
            this.btnDescontinuar.Size = new System.Drawing.Size(95, 23);
            this.btnDescontinuar.TabIndex = 1;
            this.btnDescontinuar.Text = "Descontinuar";
            this.btnDescontinuar.UseVisualStyleBackColor = true;
            this.btnDescontinuar.Click += new System.EventHandler(this.btnDescontinuar_Click);
            // 
            // txtBarras
            // 
            this.txtBarras.Location = new System.Drawing.Point(162, 460);
            this.txtBarras.Name = "txtBarras";
            this.txtBarras.Size = new System.Drawing.Size(164, 20);
            this.txtBarras.TabIndex = 2;
            this.txtBarras.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBarras_KeyUp);
            // 
            // FrmLista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 492);
            this.Controls.Add(this.txtBarras);
            this.Controls.Add(this.btnDescontinuar);
            this.Controls.Add(this.dgvProductos);
            this.Name = "FrmLista";
            this.Text = "FrmLista";
            this.Load += new System.EventHandler(this.FrmLista_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Button btnDescontinuar;
        private System.Windows.Forms.TextBox txtBarras;
    }
}