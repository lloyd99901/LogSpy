using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LogSpy
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void LoadFolder(TreeNode parentNode)
        {
            // Get the full path of the folder
            string folderPath = (string)parentNode.Tag;

            try
            {
                // Load the contents of the folder
                foreach (string filePath in Directory.GetFiles(folderPath))
                {
                    // Create a new node for the file
                    TreeNode fileNode = new TreeNode(Path.GetFileName(filePath));
                    fileNode.Tag = filePath;
                    parentNode.Nodes.Add(fileNode);
                }
                //Todo Load sub folders when clicked on--
                //foreach (string subFolderPath in Directory.GetDirectories(folderPath))
                //{
                //    // Create a new node for the subfolder
                //    TreeNode folderNode = new TreeNode(Path.GetFileName(subFolderPath));
                //    folderNode.Tag = subFolderPath;
                //    parentNode.Nodes.Add(folderNode);

                //    // Recursively load the contents of the subfolder
                //    LoadFolder(folderNode);
                //}
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur while loading the folder contents
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() != DialogResult.Cancel)
            {
                // Get the root folder
                string rootFolder = folderBrowserDialog1.SelectedPath;

                // Create the root node
                TreeNode rootNode = new TreeNode(rootFolder);
                rootNode.Tag = rootFolder;
                RootTreeView.Nodes.Add(rootNode);

                // Load the contents of the root folder
                LoadFolder(rootNode);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //Create new form
            About AboutForm = new About();
            AboutForm.ShowDialog();
        }

        private void RootTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Get the full path of the clicked item
            string clickedItem = (string)e.Node.Tag;
            if (File.Exists(clickedItem))
            {
                LoadFile(clickedItem);
            }
        }

        //Load File
        public void LoadFile(string FileLoc)
        {
            //LogForm logFormChild = new LogForm(FileLoc);
            //logFormChild.TopLevel = false;
            //MFIPanel.Controls.Add(logFormChild); //Add it to the multiple form interface control
            //logFormChild.Show();

            LogForm logFormChild = new LogForm(FileLoc);
            logFormChild.MdiParent = this;
            logFormChild.Show();
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                foreach(string file in openFileDialog1.FileNames)
                {
                    LoadFile(file);
                    Refresh();
                }
            }
        }
    }
}
