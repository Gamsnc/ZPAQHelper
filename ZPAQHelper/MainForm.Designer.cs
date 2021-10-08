namespace ZPAQHelper
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.textBox_cmdinfo = new System.Windows.Forms.TextBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_add = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_addfolder = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_forceextract = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_cancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_clearinfo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_fileassociate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_restartexplorer = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton_priority = new System.Windows.Forms.ToolStripDropDownButton();
            this.aboveNormalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.belowNormalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.listView_files = new System.Windows.Forms.ListView();
            this.comboBox_level = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_threads = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer_main = new System.Windows.Forms.SplitContainer();
            this.splitContainer_right = new System.Windows.Forms.SplitContainer();
            this.groupBox_extract = new System.Windows.Forms.GroupBox();
            this.splitContainer_extractsetting = new System.Windows.Forms.SplitContainer();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBox_destinationpath = new System.Windows.Forms.TextBox();
            this.button_browsefolder = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listView_zpaq = new System.Windows.Forms.ListView();
            this.groupBox_compress = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox_exe = new System.Windows.Forms.CheckBox();
            this.label_optimum_thread = new System.Windows.Forms.Label();
            this.label_optimum_level = new System.Windows.Forms.Label();
            this.button_autoset = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox_archivefolder = new System.Windows.Forms.TextBox();
            this.button_browse = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_archivename = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.splitContainer_rightbottom = new System.Windows.Forms.SplitContainer();
            this.button_extractall = new System.Windows.Forms.Button();
            this.openFileDialog_files = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog_folder = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog_zpaq = new System.Windows.Forms.SaveFileDialog();
            this.timer_checkthread = new System.Windows.Forms.Timer(this.components);
            this.timer_singlethreadcheck = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_main)).BeginInit();
            this.splitContainer_main.Panel1.SuspendLayout();
            this.splitContainer_main.Panel2.SuspendLayout();
            this.splitContainer_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_right)).BeginInit();
            this.splitContainer_right.Panel1.SuspendLayout();
            this.splitContainer_right.Panel2.SuspendLayout();
            this.splitContainer_right.SuspendLayout();
            this.groupBox_extract.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_extractsetting)).BeginInit();
            this.splitContainer_extractsetting.Panel1.SuspendLayout();
            this.splitContainer_extractsetting.Panel2.SuspendLayout();
            this.splitContainer_extractsetting.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox_compress.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_rightbottom)).BeginInit();
            this.splitContainer_rightbottom.Panel1.SuspendLayout();
            this.splitContainer_rightbottom.Panel2.SuspendLayout();
            this.splitContainer_rightbottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_cmdinfo
            // 
            this.textBox_cmdinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_cmdinfo.Location = new System.Drawing.Point(0, 0);
            this.textBox_cmdinfo.Multiline = true;
            this.textBox_cmdinfo.Name = "textBox_cmdinfo";
            this.textBox_cmdinfo.ReadOnly = true;
            this.textBox_cmdinfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_cmdinfo.Size = new System.Drawing.Size(486, 246);
            this.textBox_cmdinfo.TabIndex = 0;
            // 
            // button_ok
            // 
            this.button_ok.Dock = System.Windows.Forms.DockStyle.Right;
            this.button_ok.Location = new System.Drawing.Point(411, 0);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 25);
            this.button_ok.TabIndex = 1;
            this.button_ok.Text = "Go";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_add,
            this.toolStripButton_addfolder,
            this.toolStripSeparator2,
            this.toolStripButton_forceextract,
            this.toolStripButton_cancel,
            this.toolStripSeparator1,
            this.toolStripButton_clearinfo,
            this.toolStripButton_fileassociate,
            this.toolStripSeparator3,
            this.toolStripButton_restartexplorer,
            this.toolStripSeparator4,
            this.toolStripDropDownButton_priority,
            this.toolStripSeparator5});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(867, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_add
            // 
            this.toolStripButton_add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_add.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_add.Image")));
            this.toolStripButton_add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_add.Name = "toolStripButton_add";
            this.toolStripButton_add.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_add.Text = "Add Files to List";
            this.toolStripButton_add.Click += new System.EventHandler(this.toolStripButton_add_Click);
            // 
            // toolStripButton_addfolder
            // 
            this.toolStripButton_addfolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_addfolder.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_addfolder.Image")));
            this.toolStripButton_addfolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_addfolder.Name = "toolStripButton_addfolder";
            this.toolStripButton_addfolder.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_addfolder.Text = "Add Folders to List";
            this.toolStripButton_addfolder.Click += new System.EventHandler(this.toolStripButton_addfolder_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_forceextract
            // 
            this.toolStripButton_forceextract.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_forceextract.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_forceextract.Image")));
            this.toolStripButton_forceextract.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_forceextract.Name = "toolStripButton_forceextract";
            this.toolStripButton_forceextract.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_forceextract.Text = "switch mode";
            this.toolStripButton_forceextract.Click += new System.EventHandler(this.toolStripButton_forceextract_Click);
            // 
            // toolStripButton_cancel
            // 
            this.toolStripButton_cancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_cancel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_cancel.Image")));
            this.toolStripButton_cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_cancel.Name = "toolStripButton_cancel";
            this.toolStripButton_cancel.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_cancel.Text = "Cancel";
            this.toolStripButton_cancel.Click += new System.EventHandler(this.toolStripButton_cancel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_clearinfo
            // 
            this.toolStripButton_clearinfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_clearinfo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_clearinfo.Image")));
            this.toolStripButton_clearinfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_clearinfo.Name = "toolStripButton_clearinfo";
            this.toolStripButton_clearinfo.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_clearinfo.Text = "Clear the info box";
            this.toolStripButton_clearinfo.Click += new System.EventHandler(this.toolStripButton_clearinfo_Click);
            // 
            // toolStripButton_fileassociate
            // 
            this.toolStripButton_fileassociate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_fileassociate.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_fileassociate.Image")));
            this.toolStripButton_fileassociate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_fileassociate.Name = "toolStripButton_fileassociate";
            this.toolStripButton_fileassociate.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_fileassociate.Text = "File type associate";
            this.toolStripButton_fileassociate.Click += new System.EventHandler(this.toolStripButton_fileassociate_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_restartexplorer
            // 
            this.toolStripButton_restartexplorer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_restartexplorer.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_restartexplorer.Image")));
            this.toolStripButton_restartexplorer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_restartexplorer.Name = "toolStripButton_restartexplorer";
            this.toolStripButton_restartexplorer.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_restartexplorer.Text = "restart explorer";
            this.toolStripButton_restartexplorer.Click += new System.EventHandler(this.toolStripButton_restartexplorer_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton_priority
            // 
            this.toolStripDropDownButton_priority.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton_priority.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboveNormalToolStripMenuItem,
            this.normalToolStripMenuItem,
            this.belowNormalToolStripMenuItem,
            this.lowToolStripMenuItem});
            this.toolStripDropDownButton_priority.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton_priority.Image")));
            this.toolStripDropDownButton_priority.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton_priority.Name = "toolStripDropDownButton_priority";
            this.toolStripDropDownButton_priority.Size = new System.Drawing.Size(73, 22);
            this.toolStripDropDownButton_priority.Text = "CPU Level";
            // 
            // aboveNormalToolStripMenuItem
            // 
            this.aboveNormalToolStripMenuItem.Name = "aboveNormalToolStripMenuItem";
            this.aboveNormalToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.aboveNormalToolStripMenuItem.Text = "Above normal";
            this.aboveNormalToolStripMenuItem.Click += new System.EventHandler(this.aboveNormalToolStripMenuItem_Click);
            // 
            // normalToolStripMenuItem
            // 
            this.normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            this.normalToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.normalToolStripMenuItem.Text = "Normal";
            this.normalToolStripMenuItem.Click += new System.EventHandler(this.normalToolStripMenuItem_Click);
            // 
            // belowNormalToolStripMenuItem
            // 
            this.belowNormalToolStripMenuItem.Name = "belowNormalToolStripMenuItem";
            this.belowNormalToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.belowNormalToolStripMenuItem.Text = "Below normal";
            this.belowNormalToolStripMenuItem.Click += new System.EventHandler(this.belowNormalToolStripMenuItem_Click);
            // 
            // lowToolStripMenuItem
            // 
            this.lowToolStripMenuItem.Name = "lowToolStripMenuItem";
            this.lowToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.lowToolStripMenuItem.Text = "Low";
            this.lowToolStripMenuItem.Click += new System.EventHandler(this.lowToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // listView_files
            // 
            this.listView_files.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_files.FullRowSelect = true;
            this.listView_files.Location = new System.Drawing.Point(0, 0);
            this.listView_files.Name = "listView_files";
            this.listView_files.Size = new System.Drawing.Size(377, 495);
            this.listView_files.TabIndex = 3;
            this.listView_files.UseCompatibleStateImageBehavior = false;
            this.listView_files.View = System.Windows.Forms.View.Details;
            // 
            // comboBox_level
            // 
            this.comboBox_level.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_level.FormattingEnabled = true;
            this.comboBox_level.Items.AddRange(new object[] {
            "Fast",
            "Normal",
            "Good",
            "Better",
            "Best"});
            this.comboBox_level.Location = new System.Drawing.Point(100, 26);
            this.comboBox_level.Name = "comboBox_level";
            this.comboBox_level.Size = new System.Drawing.Size(121, 21);
            this.comboBox_level.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Compress Level";
            // 
            // comboBox_threads
            // 
            this.comboBox_threads.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_threads.FormattingEnabled = true;
            this.comboBox_threads.Location = new System.Drawing.Point(100, 53);
            this.comboBox_threads.Name = "comboBox_threads";
            this.comboBox_threads.Size = new System.Drawing.Size(121, 21);
            this.comboBox_threads.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Threads";
            // 
            // splitContainer_main
            // 
            this.splitContainer_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_main.Location = new System.Drawing.Point(0, 25);
            this.splitContainer_main.Name = "splitContainer_main";
            // 
            // splitContainer_main.Panel1
            // 
            this.splitContainer_main.Panel1.Controls.Add(this.listView_files);
            // 
            // splitContainer_main.Panel2
            // 
            this.splitContainer_main.Panel2.Controls.Add(this.splitContainer_right);
            this.splitContainer_main.Size = new System.Drawing.Size(867, 495);
            this.splitContainer_main.SplitterDistance = 377;
            this.splitContainer_main.TabIndex = 6;
            // 
            // splitContainer_right
            // 
            this.splitContainer_right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_right.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_right.Name = "splitContainer_right";
            this.splitContainer_right.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_right.Panel1
            // 
            this.splitContainer_right.Panel1.Controls.Add(this.groupBox_extract);
            this.splitContainer_right.Panel1.Controls.Add(this.groupBox_compress);
            // 
            // splitContainer_right.Panel2
            // 
            this.splitContainer_right.Panel2.Controls.Add(this.splitContainer_rightbottom);
            this.splitContainer_right.Size = new System.Drawing.Size(486, 495);
            this.splitContainer_right.SplitterDistance = 216;
            this.splitContainer_right.TabIndex = 6;
            // 
            // groupBox_extract
            // 
            this.groupBox_extract.Controls.Add(this.splitContainer_extractsetting);
            this.groupBox_extract.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_extract.Location = new System.Drawing.Point(0, 0);
            this.groupBox_extract.Name = "groupBox_extract";
            this.groupBox_extract.Size = new System.Drawing.Size(486, 216);
            this.groupBox_extract.TabIndex = 9;
            this.groupBox_extract.TabStop = false;
            this.groupBox_extract.Text = "Settings";
            this.groupBox_extract.Visible = false;
            // 
            // splitContainer_extractsetting
            // 
            this.splitContainer_extractsetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_extractsetting.Location = new System.Drawing.Point(3, 16);
            this.splitContainer_extractsetting.Name = "splitContainer_extractsetting";
            this.splitContainer_extractsetting.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_extractsetting.Panel1
            // 
            this.splitContainer_extractsetting.Panel1.Controls.Add(this.groupBox5);
            // 
            // splitContainer_extractsetting.Panel2
            // 
            this.splitContainer_extractsetting.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer_extractsetting.Size = new System.Drawing.Size(480, 197);
            this.splitContainer_extractsetting.SplitterDistance = 76;
            this.splitContainer_extractsetting.TabIndex = 8;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBox_destinationpath);
            this.groupBox5.Controls.Add(this.button_browsefolder);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(480, 76);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Destination path";
            // 
            // textBox_destinationpath
            // 
            this.textBox_destinationpath.Location = new System.Drawing.Point(10, 19);
            this.textBox_destinationpath.Multiline = true;
            this.textBox_destinationpath.Name = "textBox_destinationpath";
            this.textBox_destinationpath.Size = new System.Drawing.Size(379, 51);
            this.textBox_destinationpath.TabIndex = 6;
            // 
            // button_browsefolder
            // 
            this.button_browsefolder.Location = new System.Drawing.Point(396, 24);
            this.button_browsefolder.Name = "button_browsefolder";
            this.button_browsefolder.Size = new System.Drawing.Size(75, 25);
            this.button_browsefolder.TabIndex = 2;
            this.button_browsefolder.Text = "Browse";
            this.button_browsefolder.UseVisualStyleBackColor = true;
            this.button_browsefolder.Click += new System.EventHandler(this.button_browsefolder_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listView_zpaq);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(480, 117);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "zpaq list";
            // 
            // listView_zpaq
            // 
            this.listView_zpaq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_zpaq.Location = new System.Drawing.Point(3, 16);
            this.listView_zpaq.MultiSelect = false;
            this.listView_zpaq.Name = "listView_zpaq";
            this.listView_zpaq.Size = new System.Drawing.Size(474, 98);
            this.listView_zpaq.TabIndex = 0;
            this.listView_zpaq.UseCompatibleStateImageBehavior = false;
            this.listView_zpaq.View = System.Windows.Forms.View.SmallIcon;
            this.listView_zpaq.SelectedIndexChanged += new System.EventHandler(this.listView_zpaq_SelectedIndexChanged);
            // 
            // groupBox_compress
            // 
            this.groupBox_compress.Controls.Add(this.groupBox3);
            this.groupBox_compress.Controls.Add(this.groupBox2);
            this.groupBox_compress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_compress.Location = new System.Drawing.Point(0, 0);
            this.groupBox_compress.Name = "groupBox_compress";
            this.groupBox_compress.Size = new System.Drawing.Size(486, 216);
            this.groupBox_compress.TabIndex = 16;
            this.groupBox_compress.TabStop = false;
            this.groupBox_compress.Text = "Settings";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox_exe);
            this.groupBox3.Controls.Add(this.comboBox_level);
            this.groupBox3.Controls.Add(this.label_optimum_thread);
            this.groupBox3.Controls.Add(this.label_optimum_level);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.button_autoset);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.comboBox_threads);
            this.groupBox3.Location = new System.Drawing.Point(8, 17);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(469, 85);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Compress Settings";
            // 
            // checkBox_exe
            // 
            this.checkBox_exe.AutoSize = true;
            this.checkBox_exe.Location = new System.Drawing.Point(380, 57);
            this.checkBox_exe.Name = "checkBox_exe";
            this.checkBox_exe.Size = new System.Drawing.Size(79, 17);
            this.checkBox_exe.TabIndex = 6;
            this.checkBox_exe.Text = "save as sfx";
            this.checkBox_exe.UseVisualStyleBackColor = true;
            // 
            // label_optimum_thread
            // 
            this.label_optimum_thread.AutoSize = true;
            this.label_optimum_thread.Location = new System.Drawing.Point(227, 56);
            this.label_optimum_thread.Name = "label_optimum_thread";
            this.label_optimum_thread.Size = new System.Drawing.Size(79, 13);
            this.label_optimum_thread.TabIndex = 5;
            this.label_optimum_thread.Text = "optimum thread";
            // 
            // label_optimum_level
            // 
            this.label_optimum_level.AutoSize = true;
            this.label_optimum_level.Location = new System.Drawing.Point(227, 29);
            this.label_optimum_level.Name = "label_optimum_level";
            this.label_optimum_level.Size = new System.Drawing.Size(75, 13);
            this.label_optimum_level.TabIndex = 5;
            this.label_optimum_level.Text = "optimum Level";
            // 
            // button_autoset
            // 
            this.button_autoset.Location = new System.Drawing.Point(380, 26);
            this.button_autoset.Name = "button_autoset";
            this.button_autoset.Size = new System.Drawing.Size(75, 25);
            this.button_autoset.TabIndex = 2;
            this.button_autoset.Text = "Optimum";
            this.button_autoset.UseVisualStyleBackColor = true;
            this.button_autoset.Click += new System.EventHandler(this.button_autoset_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox_archivefolder);
            this.groupBox2.Controls.Add(this.button_browse);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBox_archivename);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(8, 108);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(470, 100);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Archive Settings";
            // 
            // textBox_archivefolder
            // 
            this.textBox_archivefolder.Location = new System.Drawing.Point(100, 19);
            this.textBox_archivefolder.Multiline = true;
            this.textBox_archivefolder.Name = "textBox_archivefolder";
            this.textBox_archivefolder.Size = new System.Drawing.Size(365, 46);
            this.textBox_archivefolder.TabIndex = 6;
            // 
            // button_browse
            // 
            this.button_browse.Location = new System.Drawing.Point(390, 67);
            this.button_browse.Name = "button_browse";
            this.button_browse.Size = new System.Drawing.Size(75, 25);
            this.button_browse.TabIndex = 2;
            this.button_browse.Text = "Browse";
            this.button_browse.UseVisualStyleBackColor = true;
            this.button_browse.Click += new System.EventHandler(this.button_browse_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Archive Path";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Archive Name";
            // 
            // textBox_archivename
            // 
            this.textBox_archivename.Location = new System.Drawing.Point(100, 70);
            this.textBox_archivename.Name = "textBox_archivename";
            this.textBox_archivename.Size = new System.Drawing.Size(224, 20);
            this.textBox_archivename.TabIndex = 6;
            this.textBox_archivename.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Archive Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(330, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = ".zpaq";
            // 
            // splitContainer_rightbottom
            // 
            this.splitContainer_rightbottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_rightbottom.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer_rightbottom.IsSplitterFixed = true;
            this.splitContainer_rightbottom.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_rightbottom.Name = "splitContainer_rightbottom";
            this.splitContainer_rightbottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_rightbottom.Panel1
            // 
            this.splitContainer_rightbottom.Panel1.Controls.Add(this.textBox_cmdinfo);
            // 
            // splitContainer_rightbottom.Panel2
            // 
            this.splitContainer_rightbottom.Panel2.Controls.Add(this.button_extractall);
            this.splitContainer_rightbottom.Panel2.Controls.Add(this.button_ok);
            this.splitContainer_rightbottom.Size = new System.Drawing.Size(486, 275);
            this.splitContainer_rightbottom.SplitterDistance = 246;
            this.splitContainer_rightbottom.TabIndex = 0;
            // 
            // button_extractall
            // 
            this.button_extractall.Dock = System.Windows.Forms.DockStyle.Right;
            this.button_extractall.Location = new System.Drawing.Point(336, 0);
            this.button_extractall.Name = "button_extractall";
            this.button_extractall.Size = new System.Drawing.Size(75, 25);
            this.button_extractall.TabIndex = 2;
            this.button_extractall.Text = "Extract All";
            this.button_extractall.UseVisualStyleBackColor = true;
            this.button_extractall.Click += new System.EventHandler(this.button_extractall_Click);
            // 
            // openFileDialog_files
            // 
            this.openFileDialog_files.Filter = "*|*.*";
            this.openFileDialog_files.Multiselect = true;
            // 
            // folderBrowserDialog_folder
            // 
            this.folderBrowserDialog_folder.ShowNewFolderButton = false;
            // 
            // saveFileDialog_zpaq
            // 
            this.saveFileDialog_zpaq.Filter = "*.zpaq|*.zpaq";
            // 
            // timer_checkthread
            // 
            this.timer_checkthread.Interval = 1000;
            this.timer_checkthread.Tick += new System.EventHandler(this.timer_checkthread_Tick);
            // 
            // timer_singlethreadcheck
            // 
            this.timer_singlethreadcheck.Interval = 1000;
            this.timer_singlethreadcheck.Tick += new System.EventHandler(this.timer_singlethreadcheck_Tick);
            // 
            // MainForm
            // 
            this.AcceptButton = this.button_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 520);
            this.Controls.Add(this.splitContainer_main);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZPAQHelper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer_main.Panel1.ResumeLayout(false);
            this.splitContainer_main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_main)).EndInit();
            this.splitContainer_main.ResumeLayout(false);
            this.splitContainer_right.Panel1.ResumeLayout(false);
            this.splitContainer_right.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_right)).EndInit();
            this.splitContainer_right.ResumeLayout(false);
            this.groupBox_extract.ResumeLayout(false);
            this.splitContainer_extractsetting.Panel1.ResumeLayout(false);
            this.splitContainer_extractsetting.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_extractsetting)).EndInit();
            this.splitContainer_extractsetting.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox_compress.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.splitContainer_rightbottom.Panel1.ResumeLayout(false);
            this.splitContainer_rightbottom.Panel1.PerformLayout();
            this.splitContainer_rightbottom.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_rightbottom)).EndInit();
            this.splitContainer_rightbottom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_cmdinfo;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ListView listView_files;
        private System.Windows.Forms.ComboBox comboBox_level;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_threads;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitContainer_main;
        private System.Windows.Forms.SplitContainer splitContainer_right;
        private System.Windows.Forms.SplitContainer splitContainer_rightbottom;
        private System.Windows.Forms.GroupBox groupBox_compress;
        private System.Windows.Forms.ToolStripButton toolStripButton_add;
        private System.Windows.Forms.OpenFileDialog openFileDialog_files;
        private System.Windows.Forms.ToolStripButton toolStripButton_addfolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_folder;
        private System.Windows.Forms.TextBox textBox_archivename;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_browse;
        private System.Windows.Forms.SaveFileDialog saveFileDialog_zpaq;
        private System.Windows.Forms.TextBox textBox_archivefolder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_autoset;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label_optimum_thread;
        private System.Windows.Forms.Label label_optimum_level;
        private System.Windows.Forms.GroupBox groupBox_extract;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textBox_destinationpath;
        private System.Windows.Forms.Button button_browsefolder;
        private System.Windows.Forms.SplitContainer splitContainer_extractsetting;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listView_zpaq;
        private System.Windows.Forms.ToolStripButton toolStripButton_forceextract;
        private System.Windows.Forms.Button button_extractall;
        private System.Windows.Forms.ToolStripButton toolStripButton_cancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton_clearinfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Timer timer_checkthread;
        private System.Windows.Forms.ToolStripButton toolStripButton_fileassociate;
        private System.Windows.Forms.Timer timer_singlethreadcheck;
        private System.Windows.Forms.ToolStripButton toolStripButton_restartexplorer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton_priority;
        private System.Windows.Forms.ToolStripMenuItem aboveNormalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem belowNormalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lowToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.CheckBox checkBox_exe;
    }
}

