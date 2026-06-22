namespace GUI
{
    partial class frmGestionPerfiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGestionPerfiles));
            this.txtNombrePermiso = new System.Windows.Forms.TextBox();
            this.btnCrearPermiso = new System.Windows.Forms.Button();
            this.dgvPermisos = new System.Windows.Forms.DataGridView();
            this.btnEliminarPermiso = new System.Windows.Forms.Button();
            this.txtNombreFamilia = new System.Windows.Forms.TextBox();
            this.btnCrearFamilia = new System.Windows.Forms.Button();
            this.lstFamilias = new System.Windows.Forms.ListBox();
            this.clbPermisosDisp = new System.Windows.Forms.CheckedListBox();
            this.btnAsignarPermisos = new System.Windows.Forms.Button();
            this.btnQuitarPermisos = new System.Windows.Forms.Button();
            this.btnAsignarSubfamilia = new System.Windows.Forms.Button();
            this.clbPermisosAsig = new System.Windows.Forms.CheckedListBox();
            this.clbSubfamiliasDisp = new System.Windows.Forms.CheckedListBox();
            this.clbSubfamiliasAsig = new System.Windows.Forms.CheckedListBox();
            this.btnQuitarSubfamilia = new System.Windows.Forms.Button();
            this.btnEliminarFamilia = new System.Windows.Forms.Button();
            this.txtNombrePerfil = new System.Windows.Forms.TextBox();
            this.btnCrearPerfil = new System.Windows.Forms.Button();
            this.lstPerfiles = new System.Windows.Forms.ListBox();
            this.clbFamiliasDispPerfil = new System.Windows.Forms.CheckedListBox();
            this.btnAsignarFamiliaPerfil = new System.Windows.Forms.Button();
            this.clbFamiliasAsigPerfil = new System.Windows.Forms.CheckedListBox();
            this.btnQuitarFamiliaPerfil = new System.Windows.Forms.Button();
            this.btnEliminarPerfil = new System.Windows.Forms.Button();
            this.RbPermisos = new System.Windows.Forms.RadioButton();
            this.RbFamilias = new System.Windows.Forms.RadioButton();
            this.RbPerfiles = new System.Windows.Forms.RadioButton();
            this.panelPermisos = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelFamilias = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelPerfiles = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.BtnQuitarPermisoPerfil = new System.Windows.Forms.Button();
            this.BtnAsignarPermisoPerfil = new System.Windows.Forms.Button();
            this.clbPermisosAsigPerfil = new System.Windows.Forms.CheckedListBox();
            this.clbPermisosDispPerfil = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.btnMaximizar = new System.Windows.Forms.PictureBox();
            this.btnMinimizar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPermisos)).BeginInit();
            this.panelPermisos.SuspendLayout();
            this.panelFamilias.SuspendLayout();
            this.panelPerfiles.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNombrePermiso
            // 
            this.txtNombrePermiso.Location = new System.Drawing.Point(312, 30);
            this.txtNombrePermiso.Name = "txtNombrePermiso";
            this.txtNombrePermiso.Size = new System.Drawing.Size(130, 20);
            this.txtNombrePermiso.TabIndex = 0;
            // 
            // btnCrearPermiso
            // 
            this.btnCrearPermiso.Location = new System.Drawing.Point(312, 65);
            this.btnCrearPermiso.Name = "btnCrearPermiso";
            this.btnCrearPermiso.Size = new System.Drawing.Size(130, 23);
            this.btnCrearPermiso.TabIndex = 2;
            this.btnCrearPermiso.Text = "Crear permiso";
            this.btnCrearPermiso.UseVisualStyleBackColor = true;
            this.btnCrearPermiso.Click += new System.EventHandler(this.BtnCrearPermiso_Click);
            // 
            // dgvPermisos
            // 
            this.dgvPermisos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPermisos.Location = new System.Drawing.Point(3, 7);
            this.dgvPermisos.Name = "dgvPermisos";
            this.dgvPermisos.Size = new System.Drawing.Size(240, 150);
            this.dgvPermisos.TabIndex = 3;
            // 
            // btnEliminarPermiso
            // 
            this.btnEliminarPermiso.Location = new System.Drawing.Point(312, 94);
            this.btnEliminarPermiso.Name = "btnEliminarPermiso";
            this.btnEliminarPermiso.Size = new System.Drawing.Size(130, 23);
            this.btnEliminarPermiso.TabIndex = 4;
            this.btnEliminarPermiso.Text = "Eliminar";
            this.btnEliminarPermiso.UseVisualStyleBackColor = true;
            this.btnEliminarPermiso.Click += new System.EventHandler(this.BtnEliminarPermiso_Click);
            // 
            // txtNombreFamilia
            // 
            this.txtNombreFamilia.Location = new System.Drawing.Point(53, 138);
            this.txtNombreFamilia.Name = "txtNombreFamilia";
            this.txtNombreFamilia.Size = new System.Drawing.Size(100, 20);
            this.txtNombreFamilia.TabIndex = 5;
            // 
            // btnCrearFamilia
            // 
            this.btnCrearFamilia.Location = new System.Drawing.Point(53, 164);
            this.btnCrearFamilia.Name = "btnCrearFamilia";
            this.btnCrearFamilia.Size = new System.Drawing.Size(100, 23);
            this.btnCrearFamilia.TabIndex = 6;
            this.btnCrearFamilia.Text = "Crear familia";
            this.btnCrearFamilia.UseVisualStyleBackColor = true;
            this.btnCrearFamilia.Click += new System.EventHandler(this.BtnCrearFamilia_Click);
            // 
            // lstFamilias
            // 
            this.lstFamilias.FormattingEnabled = true;
            this.lstFamilias.Location = new System.Drawing.Point(21, 37);
            this.lstFamilias.Name = "lstFamilias";
            this.lstFamilias.Size = new System.Drawing.Size(156, 95);
            this.lstFamilias.TabIndex = 7;
            this.lstFamilias.SelectedIndexChanged += new System.EventHandler(this.LstFamilias_SelectedIndexChanged);
            // 
            // clbPermisosDisp
            // 
            this.clbPermisosDisp.FormattingEnabled = true;
            this.clbPermisosDisp.Location = new System.Drawing.Point(33, 420);
            this.clbPermisosDisp.Name = "clbPermisosDisp";
            this.clbPermisosDisp.Size = new System.Drawing.Size(120, 94);
            this.clbPermisosDisp.TabIndex = 8;
            // 
            // btnAsignarPermisos
            // 
            this.btnAsignarPermisos.Location = new System.Drawing.Point(33, 516);
            this.btnAsignarPermisos.Name = "btnAsignarPermisos";
            this.btnAsignarPermisos.Size = new System.Drawing.Size(120, 23);
            this.btnAsignarPermisos.TabIndex = 9;
            this.btnAsignarPermisos.Text = "Asignar";
            this.btnAsignarPermisos.UseVisualStyleBackColor = true;
            this.btnAsignarPermisos.Click += new System.EventHandler(this.BtnAsignarPermisos_Click);
            // 
            // btnQuitarPermisos
            // 
            this.btnQuitarPermisos.Location = new System.Drawing.Point(21, 359);
            this.btnQuitarPermisos.Name = "btnQuitarPermisos";
            this.btnQuitarPermisos.Size = new System.Drawing.Size(156, 23);
            this.btnQuitarPermisos.TabIndex = 10;
            this.btnQuitarPermisos.Text = "Quitar";
            this.btnQuitarPermisos.UseVisualStyleBackColor = true;
            this.btnQuitarPermisos.Click += new System.EventHandler(this.BtnQuitarPermisos_Click);
            // 
            // btnAsignarSubfamilia
            // 
            this.btnAsignarSubfamilia.Location = new System.Drawing.Point(233, 379);
            this.btnAsignarSubfamilia.Name = "btnAsignarSubfamilia";
            this.btnAsignarSubfamilia.Size = new System.Drawing.Size(165, 23);
            this.btnAsignarSubfamilia.TabIndex = 11;
            this.btnAsignarSubfamilia.Text = "Asignar";
            this.btnAsignarSubfamilia.UseVisualStyleBackColor = true;
            this.btnAsignarSubfamilia.Click += new System.EventHandler(this.BtnAsignarSubfamilia_Click);
            // 
            // clbPermisosAsig
            // 
            this.clbPermisosAsig.FormattingEnabled = true;
            this.clbPermisosAsig.Location = new System.Drawing.Point(21, 259);
            this.clbPermisosAsig.Name = "clbPermisosAsig";
            this.clbPermisosAsig.Size = new System.Drawing.Size(156, 94);
            this.clbPermisosAsig.TabIndex = 12;
            // 
            // clbSubfamiliasDisp
            // 
            this.clbSubfamiliasDisp.FormattingEnabled = true;
            this.clbSubfamiliasDisp.Location = new System.Drawing.Point(233, 279);
            this.clbSubfamiliasDisp.Name = "clbSubfamiliasDisp";
            this.clbSubfamiliasDisp.Size = new System.Drawing.Size(165, 94);
            this.clbSubfamiliasDisp.TabIndex = 13;
            // 
            // clbSubfamiliasAsig
            // 
            this.clbSubfamiliasAsig.FormattingEnabled = true;
            this.clbSubfamiliasAsig.Location = new System.Drawing.Point(233, 81);
            this.clbSubfamiliasAsig.Name = "clbSubfamiliasAsig";
            this.clbSubfamiliasAsig.Size = new System.Drawing.Size(165, 94);
            this.clbSubfamiliasAsig.TabIndex = 14;
            // 
            // btnQuitarSubfamilia
            // 
            this.btnQuitarSubfamilia.Location = new System.Drawing.Point(233, 181);
            this.btnQuitarSubfamilia.Name = "btnQuitarSubfamilia";
            this.btnQuitarSubfamilia.Size = new System.Drawing.Size(165, 23);
            this.btnQuitarSubfamilia.TabIndex = 15;
            this.btnQuitarSubfamilia.Text = "Quitar";
            this.btnQuitarSubfamilia.UseVisualStyleBackColor = true;
            this.btnQuitarSubfamilia.Click += new System.EventHandler(this.BtnQuitarSubfamilia_Click);
            // 
            // btnEliminarFamilia
            // 
            this.btnEliminarFamilia.Location = new System.Drawing.Point(53, 193);
            this.btnEliminarFamilia.Name = "btnEliminarFamilia";
            this.btnEliminarFamilia.Size = new System.Drawing.Size(100, 23);
            this.btnEliminarFamilia.TabIndex = 16;
            this.btnEliminarFamilia.Text = "Eliminar";
            this.btnEliminarFamilia.UseVisualStyleBackColor = true;
            this.btnEliminarFamilia.Click += new System.EventHandler(this.BtnEliminarFamilia_Click);
            // 
            // txtNombrePerfil
            // 
            this.txtNombrePerfil.Location = new System.Drawing.Point(89, 7);
            this.txtNombrePerfil.Name = "txtNombrePerfil";
            this.txtNombrePerfil.Size = new System.Drawing.Size(100, 20);
            this.txtNombrePerfil.TabIndex = 17;
            // 
            // btnCrearPerfil
            // 
            this.btnCrearPerfil.Location = new System.Drawing.Point(89, 34);
            this.btnCrearPerfil.Name = "btnCrearPerfil";
            this.btnCrearPerfil.Size = new System.Drawing.Size(100, 23);
            this.btnCrearPerfil.TabIndex = 18;
            this.btnCrearPerfil.Text = "Crear perfil";
            this.btnCrearPerfil.UseVisualStyleBackColor = true;
            this.btnCrearPerfil.Click += new System.EventHandler(this.BtnCrearPerfil_Click);
            // 
            // lstPerfiles
            // 
            this.lstPerfiles.FormattingEnabled = true;
            this.lstPerfiles.Location = new System.Drawing.Point(89, 88);
            this.lstPerfiles.Name = "lstPerfiles";
            this.lstPerfiles.Size = new System.Drawing.Size(120, 95);
            this.lstPerfiles.TabIndex = 19;
            this.lstPerfiles.SelectedIndexChanged += new System.EventHandler(this.LstPerfiles_SelectedIndexChanged);
            // 
            // clbFamiliasDispPerfil
            // 
            this.clbFamiliasDispPerfil.FormattingEnabled = true;
            this.clbFamiliasDispPerfil.Location = new System.Drawing.Point(89, 249);
            this.clbFamiliasDispPerfil.Name = "clbFamiliasDispPerfil";
            this.clbFamiliasDispPerfil.Size = new System.Drawing.Size(120, 94);
            this.clbFamiliasDispPerfil.TabIndex = 20;
            // 
            // btnAsignarFamiliaPerfil
            // 
            this.btnAsignarFamiliaPerfil.Location = new System.Drawing.Point(89, 345);
            this.btnAsignarFamiliaPerfil.Name = "btnAsignarFamiliaPerfil";
            this.btnAsignarFamiliaPerfil.Size = new System.Drawing.Size(120, 23);
            this.btnAsignarFamiliaPerfil.TabIndex = 21;
            this.btnAsignarFamiliaPerfil.Text = "Asignar";
            this.btnAsignarFamiliaPerfil.UseVisualStyleBackColor = true;
            this.btnAsignarFamiliaPerfil.Click += new System.EventHandler(this.BtnAsignarFamiliaPerfil_Click);
            // 
            // clbFamiliasAsigPerfil
            // 
            this.clbFamiliasAsigPerfil.FormattingEnabled = true;
            this.clbFamiliasAsigPerfil.Location = new System.Drawing.Point(259, 40);
            this.clbFamiliasAsigPerfil.Name = "clbFamiliasAsigPerfil";
            this.clbFamiliasAsigPerfil.Size = new System.Drawing.Size(120, 94);
            this.clbFamiliasAsigPerfil.TabIndex = 22;
            // 
            // btnQuitarFamiliaPerfil
            // 
            this.btnQuitarFamiliaPerfil.Location = new System.Drawing.Point(282, 140);
            this.btnQuitarFamiliaPerfil.Name = "btnQuitarFamiliaPerfil";
            this.btnQuitarFamiliaPerfil.Size = new System.Drawing.Size(75, 23);
            this.btnQuitarFamiliaPerfil.TabIndex = 23;
            this.btnQuitarFamiliaPerfil.Text = "Quitar";
            this.btnQuitarFamiliaPerfil.UseVisualStyleBackColor = true;
            this.btnQuitarFamiliaPerfil.Click += new System.EventHandler(this.BtnQuitarFamiliaPerfil_Click);
            // 
            // btnEliminarPerfil
            // 
            this.btnEliminarPerfil.Location = new System.Drawing.Point(89, 188);
            this.btnEliminarPerfil.Name = "btnEliminarPerfil";
            this.btnEliminarPerfil.Size = new System.Drawing.Size(120, 23);
            this.btnEliminarPerfil.TabIndex = 24;
            this.btnEliminarPerfil.Text = "Eliminar";
            this.btnEliminarPerfil.UseVisualStyleBackColor = true;
            this.btnEliminarPerfil.Click += new System.EventHandler(this.BtnEliminarPerfil_Click);
            // 
            // RbPermisos
            // 
            this.RbPermisos.AutoSize = true;
            this.RbPermisos.Checked = true;
            this.RbPermisos.Location = new System.Drawing.Point(1063, 83);
            this.RbPermisos.Name = "RbPermisos";
            this.RbPermisos.Size = new System.Drawing.Size(67, 17);
            this.RbPermisos.TabIndex = 25;
            this.RbPermisos.TabStop = true;
            this.RbPermisos.Text = "Permisos";
            this.RbPermisos.UseVisualStyleBackColor = true;
            this.RbPermisos.CheckedChanged += new System.EventHandler(this.RbPermisos_CheckedChanged);
            // 
            // RbFamilias
            // 
            this.RbFamilias.AutoSize = true;
            this.RbFamilias.Location = new System.Drawing.Point(1063, 106);
            this.RbFamilias.Name = "RbFamilias";
            this.RbFamilias.Size = new System.Drawing.Size(62, 17);
            this.RbFamilias.TabIndex = 26;
            this.RbFamilias.Text = "Familias";
            this.RbFamilias.UseVisualStyleBackColor = true;
            this.RbFamilias.CheckedChanged += new System.EventHandler(this.RbFamilias_CheckedChanged_1);
            // 
            // RbPerfiles
            // 
            this.RbPerfiles.AutoSize = true;
            this.RbPerfiles.Location = new System.Drawing.Point(1063, 129);
            this.RbPerfiles.Name = "RbPerfiles";
            this.RbPerfiles.Size = new System.Drawing.Size(59, 17);
            this.RbPerfiles.TabIndex = 27;
            this.RbPerfiles.Text = "Perfiles";
            this.RbPerfiles.UseVisualStyleBackColor = true;
            this.RbPerfiles.CheckedChanged += new System.EventHandler(this.RbPerfiles_CheckedChanged);
            // 
            // panelPermisos
            // 
            this.panelPermisos.Controls.Add(this.label1);
            this.panelPermisos.Controls.Add(this.dgvPermisos);
            this.panelPermisos.Controls.Add(this.txtNombrePermiso);
            this.panelPermisos.Controls.Add(this.btnCrearPermiso);
            this.panelPermisos.Controls.Add(this.btnEliminarPermiso);
            this.panelPermisos.Location = new System.Drawing.Point(13, 12);
            this.panelPermisos.Name = "panelPermisos";
            this.panelPermisos.Size = new System.Drawing.Size(460, 172);
            this.panelPermisos.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(259, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Nombre:";
            // 
            // panelFamilias
            // 
            this.panelFamilias.Controls.Add(this.label8);
            this.panelFamilias.Controls.Add(this.label7);
            this.panelFamilias.Controls.Add(this.label6);
            this.panelFamilias.Controls.Add(this.label5);
            this.panelFamilias.Controls.Add(this.label4);
            this.panelFamilias.Controls.Add(this.label2);
            this.panelFamilias.Controls.Add(this.clbSubfamiliasAsig);
            this.panelFamilias.Controls.Add(this.txtNombreFamilia);
            this.panelFamilias.Controls.Add(this.btnCrearFamilia);
            this.panelFamilias.Controls.Add(this.lstFamilias);
            this.panelFamilias.Controls.Add(this.clbPermisosDisp);
            this.panelFamilias.Controls.Add(this.btnAsignarPermisos);
            this.panelFamilias.Controls.Add(this.btnQuitarPermisos);
            this.panelFamilias.Controls.Add(this.btnAsignarSubfamilia);
            this.panelFamilias.Controls.Add(this.clbPermisosAsig);
            this.panelFamilias.Controls.Add(this.clbSubfamiliasDisp);
            this.panelFamilias.Controls.Add(this.btnQuitarSubfamilia);
            this.panelFamilias.Controls.Add(this.btnEliminarFamilia);
            this.panelFamilias.Location = new System.Drawing.Point(479, 19);
            this.panelFamilias.Name = "panelFamilias";
            this.panelFamilias.Size = new System.Drawing.Size(417, 611);
            this.panelFamilias.TabIndex = 29;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 230);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(152, 26);
            this.label8.TabIndex = 21;
            this.label8.Text = "Permisos asignados a la familia\r\nseleccionada\r\n";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(42, 404);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Permisos disponibles";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(61, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Lista de familias";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(263, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 26);
            this.label5.TabIndex = 18;
            this.label5.Text = "Familias asignadas \r\na la familia seleccionada";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(263, 259);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Familias disponibiles";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Nombre:";
            // 
            // panelPerfiles
            // 
            this.panelPerfiles.Controls.Add(this.label13);
            this.panelPerfiles.Controls.Add(this.label12);
            this.panelPerfiles.Controls.Add(this.label11);
            this.panelPerfiles.Controls.Add(this.label10);
            this.panelPerfiles.Controls.Add(this.label9);
            this.panelPerfiles.Controls.Add(this.BtnQuitarPermisoPerfil);
            this.panelPerfiles.Controls.Add(this.BtnAsignarPermisoPerfil);
            this.panelPerfiles.Controls.Add(this.clbPermisosAsigPerfil);
            this.panelPerfiles.Controls.Add(this.clbPermisosDispPerfil);
            this.panelPerfiles.Controls.Add(this.label3);
            this.panelPerfiles.Controls.Add(this.txtNombrePerfil);
            this.panelPerfiles.Controls.Add(this.btnCrearPerfil);
            this.panelPerfiles.Controls.Add(this.lstPerfiles);
            this.panelPerfiles.Controls.Add(this.clbFamiliasDispPerfil);
            this.panelPerfiles.Controls.Add(this.btnAsignarFamiliaPerfil);
            this.panelPerfiles.Controls.Add(this.clbFamiliasAsigPerfil);
            this.panelPerfiles.Controls.Add(this.btnEliminarPerfil);
            this.panelPerfiles.Controls.Add(this.btnQuitarFamiliaPerfil);
            this.panelPerfiles.Location = new System.Drawing.Point(16, 190);
            this.panelPerfiles.Name = "panelPerfiles";
            this.panelPerfiles.Size = new System.Drawing.Size(457, 551);
            this.panelPerfiles.TabIndex = 30;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(268, 379);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(136, 13);
            this.label13.TabIndex = 33;
            this.label13.Text = "Permisos asignados al perfil";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(256, 189);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(164, 13);
            this.label12.TabIndex = 32;
            this.label12.Text = "Permisos disponibles para el perfil";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(256, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(131, 13);
            this.label11.TabIndex = 31;
            this.label11.Text = "Familias asignadas al perfil";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(109, 72);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "Lista de perfiles";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(69, 218);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(159, 13);
            this.label9.TabIndex = 29;
            this.label9.Text = "Familias disponibles para el perfil";
            // 
            // BtnQuitarPermisoPerfil
            // 
            this.BtnQuitarPermisoPerfil.Location = new System.Drawing.Point(271, 495);
            this.BtnQuitarPermisoPerfil.Name = "BtnQuitarPermisoPerfil";
            this.BtnQuitarPermisoPerfil.Size = new System.Drawing.Size(120, 23);
            this.BtnQuitarPermisoPerfil.TabIndex = 28;
            this.BtnQuitarPermisoPerfil.Text = "Quitar permiso";
            this.BtnQuitarPermisoPerfil.UseVisualStyleBackColor = true;
            this.BtnQuitarPermisoPerfil.Click += new System.EventHandler(this.BtnQuitarPermisoPerfil_Click);
            // 
            // BtnAsignarPermisoPerfil
            // 
            this.BtnAsignarPermisoPerfil.Location = new System.Drawing.Point(282, 308);
            this.BtnAsignarPermisoPerfil.Name = "BtnAsignarPermisoPerfil";
            this.BtnAsignarPermisoPerfil.Size = new System.Drawing.Size(109, 23);
            this.BtnAsignarPermisoPerfil.TabIndex = 27;
            this.BtnAsignarPermisoPerfil.Text = "Asignar permiso";
            this.BtnAsignarPermisoPerfil.UseVisualStyleBackColor = true;
            this.BtnAsignarPermisoPerfil.Click += new System.EventHandler(this.BtnAsignarPermisoPerfil_Click);
            // 
            // clbPermisosAsigPerfil
            // 
            this.clbPermisosAsigPerfil.FormattingEnabled = true;
            this.clbPermisosAsigPerfil.Location = new System.Drawing.Point(259, 395);
            this.clbPermisosAsigPerfil.Name = "clbPermisosAsigPerfil";
            this.clbPermisosAsigPerfil.Size = new System.Drawing.Size(161, 94);
            this.clbPermisosAsigPerfil.TabIndex = 26;
            // 
            // clbPermisosDispPerfil
            // 
            this.clbPermisosDispPerfil.FormattingEnabled = true;
            this.clbPermisosDispPerfil.Location = new System.Drawing.Point(259, 208);
            this.clbPermisosDispPerfil.Name = "clbPermisosDispPerfil";
            this.clbPermisosDispPerfil.Size = new System.Drawing.Size(161, 94);
            this.clbPermisosDispPerfil.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Nombre:";
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(926, 160);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(121, 97);
            this.treeView1.TabIndex = 31;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Maroon;
            this.panel1.Controls.Add(this.btnCerrar);
            this.panel1.Controls.Add(this.btnMaximizar);
            this.panel1.Controls.Add(this.btnMinimizar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1190, 41);
            this.panel1.TabIndex = 32;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.Location = new System.Drawing.Point(1162, 3);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(25, 35);
            this.btnCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnCerrar.TabIndex = 8;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnMaximizar
            // 
            this.btnMaximizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaximizar.Image = ((System.Drawing.Image)(resources.GetObject("btnMaximizar.Image")));
            this.btnMaximizar.Location = new System.Drawing.Point(1131, 3);
            this.btnMaximizar.Name = "btnMaximizar";
            this.btnMaximizar.Size = new System.Drawing.Size(25, 35);
            this.btnMaximizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMaximizar.TabIndex = 7;
            this.btnMaximizar.TabStop = false;
            this.btnMaximizar.Click += new System.EventHandler(this.btnMaximizar_Click);
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimizar.Image = ((System.Drawing.Image)(resources.GetObject("btnMinimizar.Image")));
            this.btnMinimizar.Location = new System.Drawing.Point(1100, 3);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(25, 35);
            this.btnMinimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMinimizar.TabIndex = 6;
            this.btnMinimizar.TabStop = false;
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click);
            // 
            // frmGestionPerfiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1190, 753);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.panelPerfiles);
            this.Controls.Add(this.panelFamilias);
            this.Controls.Add(this.panelPermisos);
            this.Controls.Add(this.RbPerfiles);
            this.Controls.Add(this.RbFamilias);
            this.Controls.Add(this.RbPermisos);
            this.Name = "frmGestionPerfiles";
            this.Text = "frmGestionPerfil";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPermisos)).EndInit();
            this.panelPermisos.ResumeLayout(false);
            this.panelPermisos.PerformLayout();
            this.panelFamilias.ResumeLayout(false);
            this.panelFamilias.PerformLayout();
            this.panelPerfiles.ResumeLayout(false);
            this.panelPerfiles.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNombrePermiso;
        private System.Windows.Forms.Button btnCrearPermiso;
        private System.Windows.Forms.DataGridView dgvPermisos;
        private System.Windows.Forms.Button btnEliminarPermiso;
        private System.Windows.Forms.TextBox txtNombreFamilia;
        private System.Windows.Forms.Button btnCrearFamilia;
        private System.Windows.Forms.ListBox lstFamilias;
        private System.Windows.Forms.CheckedListBox clbPermisosDisp;
        private System.Windows.Forms.Button btnAsignarPermisos;
        private System.Windows.Forms.Button btnQuitarPermisos;
        private System.Windows.Forms.Button btnAsignarSubfamilia;
        private System.Windows.Forms.CheckedListBox clbPermisosAsig;
        private System.Windows.Forms.CheckedListBox clbSubfamiliasDisp;
        private System.Windows.Forms.CheckedListBox clbSubfamiliasAsig;
        private System.Windows.Forms.Button btnQuitarSubfamilia;
        private System.Windows.Forms.Button btnEliminarFamilia;
        private System.Windows.Forms.TextBox txtNombrePerfil;
        private System.Windows.Forms.Button btnCrearPerfil;
        private System.Windows.Forms.ListBox lstPerfiles;
        private System.Windows.Forms.CheckedListBox clbFamiliasDispPerfil;
        private System.Windows.Forms.Button btnAsignarFamiliaPerfil;
        private System.Windows.Forms.CheckedListBox clbFamiliasAsigPerfil;
        private System.Windows.Forms.Button btnQuitarFamiliaPerfil;
        private System.Windows.Forms.Button btnEliminarPerfil;
        private System.Windows.Forms.RadioButton RbPermisos;
        private System.Windows.Forms.RadioButton RbFamilias;
        private System.Windows.Forms.RadioButton RbPerfiles;
        private System.Windows.Forms.Panel panelPermisos;
        private System.Windows.Forms.Panel panelFamilias;
        private System.Windows.Forms.Panel panelPerfiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox clbPermisosDispPerfil;
        private System.Windows.Forms.Button BtnQuitarPermisoPerfil;
        private System.Windows.Forms.Button BtnAsignarPermisoPerfil;
        private System.Windows.Forms.CheckedListBox clbPermisosAsigPerfil;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.PictureBox btnMaximizar;
        private System.Windows.Forms.PictureBox btnMinimizar;
    }
}