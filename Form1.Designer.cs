namespace motronictablefinder
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.selectbin = new System.Windows.Forms.Button();
            this.findtables = new System.Windows.Forms.Button();
            this.settings = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // selectbin
            // 
            this.selectbin.Location = new System.Drawing.Point(12, 12);
            this.selectbin.Name = "selectbin";
            this.selectbin.Size = new System.Drawing.Size(120, 83);
            this.selectbin.TabIndex = 0;
            this.selectbin.Text = "Select BIN";
            this.selectbin.UseVisualStyleBackColor = true;
            this.selectbin.Click += new System.EventHandler(this.selectbin_Click);
            // 
            // findtables
            // 
            this.findtables.Location = new System.Drawing.Point(264, 12);
            this.findtables.Name = "findtables";
            this.findtables.Size = new System.Drawing.Size(120, 83);
            this.findtables.TabIndex = 3;
            this.findtables.Text = "Find Tables";
            this.findtables.UseVisualStyleBackColor = true;
            this.findtables.Click += new System.EventHandler(this.findtables_Click);
            // 
            // settings
            // 
            this.settings.Location = new System.Drawing.Point(138, 12);
            this.settings.Name = "settings";
            this.settings.Size = new System.Drawing.Size(120, 83);
            this.settings.TabIndex = 4;
            this.settings.Text = "Settings";
            this.settings.UseVisualStyleBackColor = true;
            this.settings.Click += new System.EventHandler(this.settings_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.dataGridView1.Location = new System.Drawing.Point(12, 101);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 29;
            this.dataGridView1.Size = new System.Drawing.Size(725, 600);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 713);
            this.Controls.Add(this.settings);
            this.Controls.Add(this.findtables);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.selectbin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Motronic 1.7.x Table Finder";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button selectbin;
        private Button findtables;
        private Button settings;
        private DataGridView dataGridView1;
    }
}