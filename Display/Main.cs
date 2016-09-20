using System;
using System.Data;
using Dependencies;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Display
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            PopulateProcedureNamesList();

            GetDependencies();
        }

        private void GetDependenciesButton_Click(object sender, EventArgs e)
        {
            GetDependencies();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ProcedureNamesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtStoredProcedureName.Text = ProcedureNamesList.SelectedItem.ToString();
        }

        private void PopulateProcedureNamesList()
        {
            using (DBconnector dbc = new DBconnector())
            {
                string command = "SELECT name FROM sys.procedures ORDER BY name";

                dbc.ExecuteCommand(command, ProcedureType.Read);
                DataTable table = dbc.GetResultTable();

                foreach (DataRow row in table.Rows)
                    ProcedureNamesList.Items.Add(row[0].ToString());
            }
        }

        private void GetDependencies()
        {
            DependenciesView.Nodes.Clear();

            StoredProcedure storedProcedure = GetStoredProcedure();

            List<StoredProcedure> procedures = storedProcedure.Procedures;
            List<string> tables = storedProcedure.Dependencies;
            List<string> views = storedProcedure.Dependencies;

            DependenciesView.BeginUpdate();

            DependenciesView.Nodes.Add(storedProcedure.Name, storedProcedure.Name);
            AddDependencies(DependenciesView.Nodes, storedProcedure);

            if (procedures.Count > 0)
                AddNodes(DependenciesView.Nodes[storedProcedure.Name].Nodes, procedures);

            DependenciesView.ExpandAll();
            DependenciesView.EndUpdate();
        }

        private StoredProcedure GetStoredProcedure()
        {
            string procedureName = txtStoredProcedureName.Text;

            if (!procedureName.StartsWith("dbo."))
                procedureName = "dbo." + procedureName;

            StoredProcedure storedProcedure = new StoredProcedure(procedureName);
            storedProcedure.LookForDependencies();

            return storedProcedure;
        }

        private void AddNodes(TreeNodeCollection treeNodeCollection, List<StoredProcedure> procedures)
        {
            foreach (var procedure in procedures)
            {
                treeNodeCollection.Add(procedure.Name, procedure.Name);

                AddDependencies(treeNodeCollection, procedure);

                if (procedure.Procedures.Count > 0)
                    AddNodes(treeNodeCollection[procedure.Name].Nodes, procedure.Procedures);
            }
        }

        private void AddDependencies(TreeNodeCollection treeNodeCollection, StoredProcedure procedure)
        {
            foreach (var view in procedure.Dependencies)
                treeNodeCollection[procedure.Name].Nodes.Add(view.ToString());
        }

    }
}
