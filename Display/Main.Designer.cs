namespace Display
{
    partial class Main
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
            this.DependenciesView = new System.Windows.Forms.TreeView();
            this.CloseButton = new System.Windows.Forms.Button();
            this.txtStoredProcedureName = new System.Windows.Forms.TextBox();
            this.lblStoredProcedureName = new System.Windows.Forms.Label();
            this.GetDependenciesButton = new System.Windows.Forms.Button();
            this.ProcedureNamesList = new System.Windows.Forms.ListBox();
            this.lblSelectedStoredProcedure = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DependenciesView
            // 
            this.DependenciesView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DependenciesView.Location = new System.Drawing.Point(396, 80);
            this.DependenciesView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DependenciesView.Name = "DependenciesView";
            this.DependenciesView.Size = new System.Drawing.Size(707, 452);
            this.DependenciesView.TabIndex = 1;
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Location = new System.Drawing.Point(932, 565);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(172, 28);
            this.CloseButton.TabIndex = 4;
            this.CloseButton.Text = "&Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // txtStoredProcedureName
            // 
            this.txtStoredProcedureName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStoredProcedureName.Location = new System.Drawing.Point(416, 565);
            this.txtStoredProcedureName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtStoredProcedureName.Name = "txtStoredProcedureName";
            this.txtStoredProcedureName.Size = new System.Drawing.Size(327, 22);
            this.txtStoredProcedureName.TabIndex = 2;
            // 
            // lblStoredProcedureName
            // 
            this.lblStoredProcedureName.AutoSize = true;
            this.lblStoredProcedureName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStoredProcedureName.Location = new System.Drawing.Point(12, 53);
            this.lblStoredProcedureName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStoredProcedureName.Name = "lblStoredProcedureName";
            this.lblStoredProcedureName.Size = new System.Drawing.Size(195, 17);
            this.lblStoredProcedureName.TabIndex = 3;
            this.lblStoredProcedureName.Text = "Stored Procedure Names:";
            // 
            // GetDependenciesButton
            // 
            this.GetDependenciesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GetDependenciesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GetDependenciesButton.Location = new System.Drawing.Point(752, 565);
            this.GetDependenciesButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GetDependenciesButton.Name = "GetDependenciesButton";
            this.GetDependenciesButton.Size = new System.Drawing.Size(172, 28);
            this.GetDependenciesButton.TabIndex = 3;
            this.GetDependenciesButton.Text = "&Get Dependencies";
            this.GetDependenciesButton.UseVisualStyleBackColor = true;
            this.GetDependenciesButton.Click += new System.EventHandler(this.GetDependenciesButton_Click);
            // 
            // ProcedureNamesList
            // 
            this.ProcedureNamesList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ProcedureNamesList.FormattingEnabled = true;
            this.ProcedureNamesList.ItemHeight = 16;
            this.ProcedureNamesList.Location = new System.Drawing.Point(16, 80);
            this.ProcedureNamesList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ProcedureNamesList.Name = "ProcedureNamesList";
            this.ProcedureNamesList.Size = new System.Drawing.Size(371, 452);
            this.ProcedureNamesList.TabIndex = 0;
            this.ProcedureNamesList.SelectedIndexChanged += new System.EventHandler(this.ProcedureNamesList_SelectedIndexChanged);
            // 
            // lblSelectedStoredProcedure
            // 
            this.lblSelectedStoredProcedure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelectedStoredProcedure.AutoSize = true;
            this.lblSelectedStoredProcedure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedStoredProcedure.Location = new System.Drawing.Point(189, 569);
            this.lblSelectedStoredProcedure.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedStoredProcedure.Name = "lblSelectedStoredProcedure";
            this.lblSelectedStoredProcedure.Size = new System.Drawing.Size(209, 17);
            this.lblSelectedStoredProcedure.TabIndex = 5;
            this.lblSelectedStoredProcedure.Text = "Selected Stored Procedure:";
            // 
            // Main
            // 
            this.AcceptButton = this.GetDependenciesButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(1137, 608);
            this.Controls.Add(this.lblSelectedStoredProcedure);
            this.Controls.Add(this.ProcedureNamesList);
            this.Controls.Add(this.GetDependenciesButton);
            this.Controls.Add(this.lblStoredProcedureName);
            this.Controls.Add(this.txtStoredProcedureName);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.DependenciesView);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Main";
            this.Text = "Show Dependencies";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView DependenciesView;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.TextBox txtStoredProcedureName;
        private System.Windows.Forms.Label lblStoredProcedureName;
        private System.Windows.Forms.Button GetDependenciesButton;
        private System.Windows.Forms.ListBox ProcedureNamesList;
        private System.Windows.Forms.Label lblSelectedStoredProcedure;

    }
}

