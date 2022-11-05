namespace TechnicianMap
{
    partial class Form1
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.MainPanel = new System.Windows.Forms.Panel();
			this.ListGrid = new System.Windows.Forms.DataGridView();
			this.ButtonPanel = new System.Windows.Forms.Panel();
			this.Refresh = new System.Windows.Forms.Button();
			this.Update = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.Free = new System.Windows.Forms.RadioButton();
			this.Busy = new System.Windows.Forms.RadioButton();
			this.AllFreeBusy = new System.Windows.Forms.RadioButton();
			this.DriversGraph = new System.Windows.Forms.ProgressBar();
			this.MaximumLabel = new System.Windows.Forms.Label();
			this.ValueLabel = new System.Windows.Forms.Label();
			this.MainPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ListGrid)).BeginInit();
			this.ButtonPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainPanel
			// 
			this.MainPanel.Controls.Add(this.ListGrid);
			this.MainPanel.Controls.Add(this.ButtonPanel);
			this.MainPanel.Location = new System.Drawing.Point(12, 12);
			this.MainPanel.Name = "MainPanel";
			this.MainPanel.Size = new System.Drawing.Size(764, 726);
			this.MainPanel.TabIndex = 0;
			// 
			// ListGrid
			// 
			this.ListGrid.AllowUserToAddRows = false;
			this.ListGrid.AllowUserToDeleteRows = false;
			this.ListGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.ListGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ListGrid.Location = new System.Drawing.Point(3, 3);
			this.ListGrid.Name = "ListGrid";
			this.ListGrid.ReadOnly = true;
			this.ListGrid.Size = new System.Drawing.Size(594, 665);
			this.ListGrid.TabIndex = 1;
			this.ListGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ListGrid_ColumnHeaderMouseClick);
			// 
			// ButtonPanel
			// 
			this.ButtonPanel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.ButtonPanel.Controls.Add(this.ValueLabel);
			this.ButtonPanel.Controls.Add(this.MaximumLabel);
			this.ButtonPanel.Controls.Add(this.DriversGraph);
			this.ButtonPanel.Controls.Add(this.AllFreeBusy);
			this.ButtonPanel.Controls.Add(this.Busy);
			this.ButtonPanel.Controls.Add(this.Free);
			this.ButtonPanel.Controls.Add(this.Refresh);
			this.ButtonPanel.Controls.Add(this.Update);
			this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ButtonPanel.Location = new System.Drawing.Point(0, 674);
			this.ButtonPanel.Name = "ButtonPanel";
			this.ButtonPanel.Size = new System.Drawing.Size(764, 52);
			this.ButtonPanel.TabIndex = 0;
			// 
			// Refresh
			// 
			this.Refresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Refresh.BackgroundImage")));
			this.Refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.Refresh.Dock = System.Windows.Forms.DockStyle.Left;
			this.Refresh.Location = new System.Drawing.Point(0, 0);
			this.Refresh.Name = "Refresh";
			this.Refresh.Size = new System.Drawing.Size(65, 52);
			this.Refresh.TabIndex = 1;
			this.toolTip1.SetToolTip(this.Refresh, "Refresh");
			this.Refresh.UseVisualStyleBackColor = true;
			this.Refresh.Click += new System.EventHandler(this.Refresh_Click);
			// 
			// Update
			// 
			this.Update.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.Update.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Update.BackgroundImage")));
			this.Update.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.Update.Dock = System.Windows.Forms.DockStyle.Right;
			this.Update.Location = new System.Drawing.Point(698, 0);
			this.Update.Name = "Update";
			this.Update.Size = new System.Drawing.Size(66, 52);
			this.Update.TabIndex = 0;
			this.toolTip1.SetToolTip(this.Update, "Update");
			this.Update.UseVisualStyleBackColor = false;
			this.Update.Click += new System.EventHandler(this.Update_Click);
			// 
			// Free
			// 
			this.Free.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.Free.AutoSize = true;
			this.Free.Location = new System.Drawing.Point(72, 16);
			this.Free.Name = "Free";
			this.Free.Size = new System.Drawing.Size(88, 20);
			this.Free.TabIndex = 2;
			this.Free.Text = "Διαθέσιμοι";
			this.Free.UseVisualStyleBackColor = true;
			this.Free.Click += new System.EventHandler(this.Free_Click);
			// 
			// Busy
			// 
			this.Busy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.Busy.AutoSize = true;
			this.Busy.Location = new System.Drawing.Point(166, 16);
			this.Busy.Name = "Busy";
			this.Busy.Size = new System.Drawing.Size(103, 20);
			this.Busy.TabIndex = 3;
			this.Busy.Text = "Δεσμευμένοι";
			this.Busy.UseVisualStyleBackColor = true;
			this.Busy.Click += new System.EventHandler(this.Free_Click);
			// 
			// AllFreeBusy
			// 
			this.AllFreeBusy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.AllFreeBusy.AutoSize = true;
			this.AllFreeBusy.Checked = true;
			this.AllFreeBusy.Location = new System.Drawing.Point(275, 16);
			this.AllFreeBusy.Name = "AllFreeBusy";
			this.AllFreeBusy.Size = new System.Drawing.Size(55, 20);
			this.AllFreeBusy.TabIndex = 4;
			this.AllFreeBusy.TabStop = true;
			this.AllFreeBusy.Text = "Ολοι";
			this.AllFreeBusy.UseVisualStyleBackColor = true;
			this.AllFreeBusy.Click += new System.EventHandler(this.Free_Click);
			// 
			// DriversGraph
			// 
			this.DriversGraph.Location = new System.Drawing.Point(336, 13);
			this.DriversGraph.Name = "DriversGraph";
			this.DriversGraph.Size = new System.Drawing.Size(240, 23);
			this.DriversGraph.TabIndex = 5;
			// 
			// MaximumLabel
			// 
			this.MaximumLabel.AutoSize = true;
			this.MaximumLabel.Location = new System.Drawing.Point(582, 18);
			this.MaximumLabel.Name = "MaximumLabel";
			this.MaximumLabel.Size = new System.Drawing.Size(99, 16);
			this.MaximumLabel.TabIndex = 6;
			this.MaximumLabel.Text = "MaximumLabel";
			// 
			// ValueLabel
			// 
			this.ValueLabel.AutoSize = true;
			this.ValueLabel.BackColor = System.Drawing.Color.Transparent;
			this.ValueLabel.Location = new System.Drawing.Point(339, 16);
			this.ValueLabel.Name = "ValueLabel";
			this.ValueLabel.Size = new System.Drawing.Size(77, 16);
			this.ValueLabel.TabIndex = 7;
			this.ValueLabel.Text = "ValueLabel";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(788, 741);
			this.Controls.Add(this.MainPanel);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "Extra Assistance Live ver 05112022";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.MainPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ListGrid)).EndInit();
			this.ButtonPanel.ResumeLayout(false);
			this.ButtonPanel.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
		private System.Windows.Forms.Panel ButtonPanel;
		private System.Windows.Forms.Button Update;
		private System.Windows.Forms.DataGridView ListGrid;
		private System.Windows.Forms.Button Refresh;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.RadioButton AllFreeBusy;
		private System.Windows.Forms.RadioButton Busy;
		private System.Windows.Forms.RadioButton Free;
		private System.Windows.Forms.ProgressBar DriversGraph;
		private System.Windows.Forms.Label MaximumLabel;
		private System.Windows.Forms.Label ValueLabel;
	}
}

