using System;
using System.Windows.Forms;

namespace DataGridView_Lib
{
    public class DataGridViewContextMenu
    {
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private DataGridView dataGridView;

        public DataGridViewContextMenu(DataGridView dgv)
        {
            dataGridView = dgv;
            InitializeContextMenu();
        }

        private void InitializeContextMenu()
        {
            // Create ContextMenuStrip
            contextMenuStrip = new ContextMenuStrip();

            // Create ToolStripMenuItems
            copyToolStripMenuItem = new ToolStripMenuItem("Copy");
            deleteToolStripMenuItem = new ToolStripMenuItem("Delete");

            // Add ToolStripMenuItems to ContextMenuStrip
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { copyToolStripMenuItem, deleteToolStripMenuItem });

            // Assign ContextMenuStrip to DataGridView
            dataGridView.ContextMenuStrip = contextMenuStrip;

            // Attach event handlers
            copyToolStripMenuItem.Click += CopyToolStripMenuItem_Click;
            deleteToolStripMenuItem.Click += DeleteToolStripMenuItem_Click;
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Implement copy logic here
            if (dataGridView.SelectedCells.Count > 0)
            {
                Clipboard.SetText(dataGridView.SelectedCells[0].Value.ToString());
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Implement delete logic here
            if (dataGridView.SelectedRows.Count > 0)
            {
                dataGridView.Rows.RemoveAt(dataGridView.SelectedRows[0].Index);
            }
        }
    }
}
