
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;


namespace TC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.treeView1.NodeMouseClick +=
            new TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView2.NodeMouseClick +=
            new TreeNodeMouseClickEventHandler(this.treeView2_NodeMouseClick);
            comboBox1.DataSource = System.IO.DriveInfo.GetDrives();
            comboBox2.DataSource = System.IO.DriveInfo.GetDrives();
            comboBox1.SelectedIndex = 1;
            comboBox2.SelectedIndex = 0;
            comboBoxTypSzukaniaL.SelectedIndex = 0;
            comboBoxTypSzukaniaP.SelectedIndex = 0;
        }

        private void PopulateTreeView1()
        {
            string Part1 = comboBox1.Text;
            TreeNode rootNode;
            DirectoryInfo info = new DirectoryInfo(@"" + Part1);
            if (info.Exists)
            {
                rootNode = new TreeNode(info.Name);
                rootNode.Tag = info;
                GetDirectories(info.GetDirectories(), rootNode);
                treeView1.Nodes.Add(rootNode);
            }

        }

        private void PopulateTreeView2()
        {
            string Part2 = comboBox2.Text;
            TreeNode rootNode;
            DirectoryInfo info = new DirectoryInfo(@"" + Part2);
            if (info.Exists)
            {
                rootNode = new TreeNode(info.Name);
                rootNode.Tag = info;
                GetDirectories(info.GetDirectories(), rootNode);
                treeView2.Nodes.Add(rootNode);
            }

        }


        private void GetDirectories(DirectoryInfo[] subDirs,
   TreeNode nodeToAddTo)
        {
            TreeNode aNode;
            DirectoryInfo[] subSubDirs;

            try
            {
                foreach (DirectoryInfo subDir in subDirs)
                {
                    aNode = new TreeNode(subDir.Name, 1, 0);
                    aNode.Tag = subDir;
                    aNode.ImageKey = "folder";
                    subSubDirs = subDir.GetDirectories();
                    if (subSubDirs.Length != 0)
                    {
                        GetSubDirectories(subSubDirs, aNode);
                    }
                    nodeToAddTo.Nodes.Add(aNode);
                }
            }
            catch { }
        }


        private void GetSubDirectories(DirectoryInfo[] subDirs,
   TreeNode nodeToAddTo)
        {
            TreeNode aNode;
            DirectoryInfo[] subSubDirs;
            try
            {
                foreach (DirectoryInfo subDir in subDirs)
                {
                    aNode = new TreeNode(subDir.Name, 1, 0);
                    aNode.Tag = subDir;
                    aNode.ImageKey = "folder";
                    subSubDirs = subDir.GetDirectories();
                    nodeToAddTo.Nodes.Add(aNode);
                    GetSubDirectories(subSubDirs, aNode);
                }
            }
            catch { }
        }

        void treeView1_NodeMouseClick(object sender,
    TreeNodeMouseClickEventArgs e)
        {

            TreeNode newSelected = e.Node;
            listView1.Items.Clear();

            TreeNode CurrentNode = e.Node;
            string fullpath = CurrentNode.FullPath;
            textBox1.Text = fullpath;

            DirectoryInfo nodeDirInfo = (DirectoryInfo)newSelected.Tag;
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item = null;



            foreach (DirectoryInfo dir in nodeDirInfo.GetDirectories())
            {

                item = new ListViewItem(dir.Name, 0);
                subItems = new ListViewItem.ListViewSubItem[]
                          {new ListViewItem.ListViewSubItem(item, "Folder plików"),
                   new ListViewItem.ListViewSubItem(item,
                dir.LastAccessTime.ToShortDateString())};
                item.SubItems.AddRange(subItems);
                listView1.Items.Add(item);
            }

            foreach (FileInfo file in nodeDirInfo.GetFiles())
            {
                item = new ListViewItem(file.Name, 1);
                subItems = new ListViewItem.ListViewSubItem[]
                          { new ListViewItem.ListViewSubItem(item, "Plik"),
                   new ListViewItem.ListViewSubItem(item,
                file.LastAccessTime.ToShortDateString())};
                item.SubItems.AddRange(subItems);
                listView1.Items.Add(item);
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }


        void treeView2_NodeMouseClick(object sender,
    TreeNodeMouseClickEventArgs e)
        {

            TreeNode newSelected = e.Node;
            listView2.Items.Clear();

            TreeNode CurrentNode = e.Node;
            string fullpath2 = CurrentNode.FullPath;
            textBox2.Text = fullpath2;

            DirectoryInfo nodeDirInfo = (DirectoryInfo)newSelected.Tag;
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item = null;



            foreach (DirectoryInfo dir in nodeDirInfo.GetDirectories())
            {

                item = new ListViewItem(dir.Name, 0);
                subItems = new ListViewItem.ListViewSubItem[]
                          {new ListViewItem.ListViewSubItem(item, "Folder plików"),
                   new ListViewItem.ListViewSubItem(item,
                dir.LastAccessTime.ToShortDateString())};
                item.SubItems.AddRange(subItems);
                listView2.Items.Add(item);
            }

            foreach (FileInfo file in nodeDirInfo.GetFiles())
            {
                item = new ListViewItem(file.Name, 1);
                subItems = new ListViewItem.ListViewSubItem[]
                          { new ListViewItem.ListViewSubItem(item, "Plik"),
                   new ListViewItem.ListViewSubItem(item,
                file.LastAccessTime.ToShortDateString())};
                item.SubItems.AddRange(subItems);
                listView2.Items.Add(item);
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }


        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            treeView1.Nodes.Clear();
            PopulateTreeView1();

            string drive_name = comboBox1.Text.Substring(0, 1);
            DriveInfo di = new DriveInfo(drive_name);
            if (di.IsReady)
            {
                double tsbytes = di.TotalSize;
                double tsgb = tsbytes / (1073741824);

                double asbytes = di.AvailableFreeSpace;
                double asgb = asbytes / (1073741824);

                label1.Text = asgb.ToString("#.##") + "GB wolnych z " + tsgb.ToString("#.##") + "GB";
            }
        }
        private void comboBox2_SelectedIndexChanged_1(object sender, System.EventArgs e)
        {
            treeView2.Nodes.Clear();
            PopulateTreeView2();

            string drive_name = comboBox1.Text.Substring(0, 1);
            DriveInfo di = new DriveInfo(drive_name);
            if (di.IsReady)
            {
                double tsbytes = di.TotalSize;
                double tsgb = tsbytes / (1073741824);

                double asbytes = di.AvailableFreeSpace;
                double asgb = asbytes / (1073741824);

                label2.Text = asgb.ToString("#.##") + "GB wolnych z " + tsgb.ToString("#.##") + "GB";
            }
        }

        private void Open_Click(object sender, System.EventArgs e)
        {
            if(listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Nie wybrano pliku do otwarcia");
            }
            else
            {
                string ImageViewName = listView1.SelectedItems[0].Text;
                System.Diagnostics.Process.Start(@textBox1.Text + "/" + ImageViewName);
            }
        }

        private void Open2_Click(object sender, System.EventArgs e)
        {
            if (listView2.SelectedItems.Count == 0)
            {
                MessageBox.Show("Nie wybrano pliku do otwarcia");
            }
            else
            {
                string ImageViewName = listView2.SelectedItems[0].Text;
                System.Diagnostics.Process.Start(@textBox2.Text + "/" + ImageViewName);
            }

        }

        private void Delete_Click(object sender, System.EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Nie wybrano pliku do usunięcia");
            }
            else
            {
                string ImageViewName = listView1.SelectedItems[0].Text;
                File.Delete(@textBox1.Text + "/" + ImageViewName);
                listView1.Refresh();
            }
        }

        private void Delete2_Click_1(object sender, System.EventArgs e)
        {
            if (listView2.SelectedItems.Count == 0)
            {
                MessageBox.Show("Nie wybrano pliku do usunięcia");
            }
            else
            {
                string ImageViewName = listView2.SelectedItems[0].Text;
                File.Delete(@textBox2.Text + "/" + ImageViewName);
                listView2.Refresh();
            }
        }

        private void CopyL_Click(object sender, System.EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Nie wybrano pliku do skopiowania");
            }
            else
            {
                string ImageViewName = listView1.SelectedItems[0].Text;
                File.Copy(Path.Combine(@textBox1.Text, ImageViewName), Path.Combine(@textBox2.Text, ImageViewName));
            }
        }

        private void CopyR_Click(object sender, System.EventArgs e)
        {
            if (listView2.SelectedItems.Count == 0)
            {
                MessageBox.Show("Nie wybrano pliku do skopiowania");
            }
            else
            {
                string ImageViewName = listView2.SelectedItems[1].Text;
                File.Copy(@textBox2.Text + "/" + ImageViewName, @textBox1.Text + "/" + ImageViewName);
            }
        }

        private void MoveL_Click(object sender, System.EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Nie wybrano pliku do przeniesienia");
            }
            else
            {
                string ImageViewName = listView1.SelectedItems[0].Text;
                File.Move(@textBox1.Text + "/" + ImageViewName, @textBox2.Text + "/" + ImageViewName);
            }
        }

        private void MoveR_Click(object sender, System.EventArgs e)
        {
            if (listView2.SelectedItems.Count == 0)
            {
                MessageBox.Show("Nie wybrano pliku do przeniesienia");
            }
            else
            {
                string ImageViewName = listView2.SelectedItems[0].Text;
                File.Move(@textBox2.Text + "/" + ImageViewName, @textBox1.Text + "/" + ImageViewName);
            }
        }
        private void szukajL_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxSzukanieL.Text))
            {
                MessageBox.Show("Pole wyszukiwania jest puste");
            }
            else
            {
                listViewWynikiWyszukiwaniaL.Items.Clear();
                textBoxSciezkaPrzeszukanaL.Text = textBox1.Text;
                int j = 0;
                string pat = "";
                switch (comboBoxTypSzukaniaL.SelectedIndex)
                {
                    case 0:
                        pat = @"(" + textBoxSzukanieL.Text + ")";
                        break;

                    case 1:
                        pat = @"^(" + textBoxSzukanieL.Text + ")";
                        break;

                    case 2:
                        pat = @"(" + textBoxSzukanieL.Text + ")$";
                        break;
                }
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    string text = listView1.Items[i].SubItems[0].Text.ToString();
                    if (checkBoxWielkoscLiterL.Checked == false)
                    {
                        Regex r = new Regex(pat, RegexOptions.IgnoreCase);
                        Match m = r.Match(text);
                        while (m.Success)
                        {
                            listViewWynikiWyszukiwaniaL.Items.Add(listView1.Items[i].SubItems[0].Text.ToString());
                            listViewWynikiWyszukiwaniaL.Items[j].SubItems.Add(listView1.Items[i].SubItems[1].Text.ToString());
                            listViewWynikiWyszukiwaniaL.Items[j].SubItems.Add(listView1.Items[i].SubItems[2].Text.ToString());
                            j++;
                            break;
                        }
                    }
                    else
                    {
                        Regex r = new Regex(pat);
                        Match m = r.Match(text);
                        while (m.Success)
                        {
                            listViewWynikiWyszukiwaniaL.Items.Add(listView1.Items[i].SubItems[0].Text.ToString());
                            listViewWynikiWyszukiwaniaL.Items[j].SubItems.Add(listView1.Items[i].SubItems[1].Text.ToString());
                            listViewWynikiWyszukiwaniaL.Items[j].SubItems.Add(listView1.Items[i].SubItems[2].Text.ToString());
                            j++;
                            break;
                        }
                    }
                }
            }
        }

        private void szukajP_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxSzukanieP.Text))
            {
                MessageBox.Show("Pole wyszukiwania jest puste");
            }
            else
            {
                listViewWynikiWyszukiwaniaP.Items.Clear();
                textBoxSciezkaPrzeszukanaP.Text = textBox2.Text;
                int j = 0;
                string pat = "";
                switch (comboBoxTypSzukaniaP.SelectedIndex)
                {
                    case 0:
                        pat = @"(" + textBoxSzukanieP.Text + ")";
                        break;

                    case 1:
                        pat = @"^(" + textBoxSzukanieP.Text + ")";
                        break;

                    case 2:
                        pat = @"(" + textBoxSzukanieP.Text + ")$";
                        break;
                }
                for (int i = 0; i < listView2.Items.Count; i++)
                {
                    string text = listView2.Items[i].SubItems[0].Text.ToString();

                    if (checkBoxWielkoscLiterP.Checked == false)
                    {
                        Regex r = new Regex(pat, RegexOptions.IgnoreCase);
                        Match m = r.Match(text);
                        while (m.Success)
                        {
                            listViewWynikiWyszukiwaniaP.Items.Add(listView2.Items[i].SubItems[0].Text.ToString());
                            listViewWynikiWyszukiwaniaP.Items[j].SubItems.Add(listView2.Items[i].SubItems[1].Text.ToString());
                            listViewWynikiWyszukiwaniaP.Items[j].SubItems.Add(listView2.Items[i].SubItems[2].Text.ToString());
                            j++;
                            break;
                        }
                    }
                    else
                    {
                        Regex r = new Regex(pat);
                        Match m = r.Match(text);
                        while (m.Success)
                        {
                            listViewWynikiWyszukiwaniaP.Items.Add(listView2.Items[i].SubItems[0].Text.ToString());
                            listViewWynikiWyszukiwaniaP.Items[j].SubItems.Add(listView2.Items[i].SubItems[1].Text.ToString());
                            listViewWynikiWyszukiwaniaP.Items[j].SubItems.Add(listView2.Items[i].SubItems[2].Text.ToString());
                            j++;
                            break;
                        }
                    }
                }
            }
        }
        private void buttonWynikiWyszukiwaniaLOpen_Click(object sender, System.EventArgs e)
        {
            if (listViewWynikiWyszukiwaniaL.SelectedItems.Count == 0)
            {
                MessageBox.Show("Nie wybrano pliku do otwarcia");
            }
            else
            {
                string ImageViewName = listViewWynikiWyszukiwaniaL.SelectedItems[0].Text;
                System.Diagnostics.Process.Start(@textBoxSciezkaPrzeszukanaL.Text + "/" + ImageViewName);
            }
        }
        private void buttonWynikiWyszukiwaniaLDelete_Click(object sender, System.EventArgs e)
        {
            if (listViewWynikiWyszukiwaniaL.SelectedItems.Count == 0)
            {
                MessageBox.Show("Nie wybrano pliku do usunięcia");
            }
            else
            {
                string ImageViewName = listViewWynikiWyszukiwaniaL.SelectedItems[0].Text;
                File.Delete(@textBoxSciezkaPrzeszukanaL.Text + "/" + ImageViewName);
                listViewWynikiWyszukiwaniaL.Refresh();
            }
        }
        private void buttonWynikiWyszukiwaniaLCopy_Click(object sender, System.EventArgs e)
        {
            if (listViewWynikiWyszukiwaniaL.SelectedItems.Count == 0)
            {
                MessageBox.Show("Nie wybrano pliku do skopiowania");
            }
            else
            {
                string ImageViewName = listViewWynikiWyszukiwaniaL.SelectedItems[0].Text;
                File.Copy(Path.Combine(@textBoxSciezkaPrzeszukanaL.Text, ImageViewName), Path.Combine(@textBoxSciezkaPrzeszukanaP.Text, ImageViewName));
            }
        }
        private void buttonWynikiWyszukiwaniaLMove_Click(object sender, System.EventArgs e)
        {
            if (listViewWynikiWyszukiwaniaL.SelectedItems.Count == 0)
            {
                MessageBox.Show("Nie wybrano pliku do przeniesienia");
            }
            else
            {
                string ImageViewName = listViewWynikiWyszukiwaniaL.SelectedItems[0].Text;
                File.Move(@textBoxSciezkaPrzeszukanaL.Text + "/" + ImageViewName, @textBoxSciezkaPrzeszukanaP.Text + "/" + ImageViewName);
            }
        }
        private void buttonWynikiWyszukiwaniaPOpen_Click(object sender, System.EventArgs e)
        {
            if (listViewWynikiWyszukiwaniaP.SelectedItems.Count == 0)
            {
                MessageBox.Show("Nie wybrano pliku do otwarcia");
            }
            else
            {
                string ImageViewName = listViewWynikiWyszukiwaniaP.SelectedItems[0].Text;
                System.Diagnostics.Process.Start(@textBoxSciezkaPrzeszukanaP.Text + "/" + ImageViewName);
            }
        }
        private void buttonWynikiWyszukiwaniaPDelete_Click(object sender, System.EventArgs e)
        {
            if (listViewWynikiWyszukiwaniaP.SelectedItems.Count == 0)
            {
                MessageBox.Show("Nie wybrano pliku do usunięcia");
            }
            else
            {
                string ImageViewName = listViewWynikiWyszukiwaniaP.SelectedItems[0].Text;
                File.Delete(@textBoxSciezkaPrzeszukanaP.Text + "/" + ImageViewName);
                listViewWynikiWyszukiwaniaP.Refresh();
            }
        }
        private void buttonWynikiWyszukiwaniaPCopy_Click(object sender, System.EventArgs e)
        {
            if (listViewWynikiWyszukiwaniaP.SelectedItems.Count == 0)
            {
                MessageBox.Show("Nie wybrano pliku do skopiowania");
            }
            else
            {
                string ImageViewName = listViewWynikiWyszukiwaniaP.SelectedItems[0].Text;
                File.Copy(Path.Combine(@textBoxSciezkaPrzeszukanaP.Text, ImageViewName), Path.Combine(@textBoxSciezkaPrzeszukanaL.Text, ImageViewName));
            }
        }
        private void buttonWynikiWyszukiwaniaPMove_Click(object sender, System.EventArgs e)
        {
            if (listViewWynikiWyszukiwaniaP.SelectedItems.Count == 0)
            {
                MessageBox.Show("Nie wybrano pliku do przeniesienia");
            }
            else
            {
                string ImageViewName = listViewWynikiWyszukiwaniaP.SelectedItems[0].Text;
                File.Move(@textBoxSciezkaPrzeszukanaP.Text + "/" + ImageViewName, @textBoxSciezkaPrzeszukanaL.Text + "/" + ImageViewName);
            }
        }
    }
}
