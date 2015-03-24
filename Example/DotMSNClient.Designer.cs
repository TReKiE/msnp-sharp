using System.Windows.Forms;

namespace MSNPSharpClient
{
    partial class ClientForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientForm));
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.userMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sendIMMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendMIMMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.importContactsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createCircleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leaveCircleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.appearOfflineMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appearOnlineMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.liveProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.sortContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSortByStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSortBygroup = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDeleteGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.groupContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.WhatsUpPanel = new System.Windows.Forms.Panel();
            this.lblNewsLink = new System.Windows.Forms.LinkLabel();
            this.lblNews = new System.Windows.Forms.Label();
            this.pbNewsPicture = new System.Windows.Forms.PictureBox();
            this.cmdNext = new System.Windows.Forms.Button();
            this.cmdPrev = new System.Windows.Forms.Button();
            this.lblWhatsup = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTreeViewsContainer = new System.Windows.Forms.Panel();
            this.treeViewFilterList = new System.Windows.Forms.TreeView();
            this.treeViewFavoriteList = new System.Windows.Forms.TreeView();
            this.SortPanel = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnSortBy = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.pnlNameAndPM = new System.Windows.Forms.Panel();
            this.btnSetTheme = new System.Windows.Forms.Button();
            this.btnSetMusic = new System.Windows.Forms.Button();
            this.lblPM = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.TextBox();
            this.accountTextBox = new System.Windows.Forms.TextBox();
            this.loginButton = new System.Windows.Forms.Button();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.comboStatus = new System.Windows.Forms.ComboBox();
            this.comboPlaces = new System.Windows.Forms.ComboBox();
            this.displayImageBox = new System.Windows.Forms.PictureBox();
            this.logoImageBox = new System.Windows.Forms.PictureBox();
            this.statusBar = new System.Windows.Forms.Label();
            this.trayicon = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.userMenuStrip.SuspendLayout();
            this.sortContextMenu.SuspendLayout();
            this.groupContextMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.WhatsUpPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbNewsPicture)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.pnlTreeViewsContainer.SuspendLayout();
            this.SortPanel.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.pnlLogin.SuspendLayout();
            this.pnlNameAndPM.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.displayImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoImageBox)).BeginInit();
            this.cmsTray.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImageList1
            // 
            this.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.ImageList1.ImageSize = new System.Drawing.Size(10, 10);
            this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // userMenuStrip
            // 
            this.userMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendIMMenuItem,
            this.sendMIMMenuItem,
            this.toolStripMenuItem3,
            this.importContactsMenuItem,
            this.createCircleMenuItem,
            this.leaveCircleToolStripMenuItem,
            this.toolStripMenuItem2,
            this.appearOfflineMenuItem,
            this.appearOnlineMenuItem,
            this.deleteMenuItem,
            this.liveProfileToolStripMenuItem});
            this.userMenuStrip.Name = "contextMenuStrip1";
            this.userMenuStrip.Size = new System.Drawing.Size(161, 214);
            // 
            // sendIMMenuItem
            // 
            this.sendIMMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.sendIMMenuItem.Name = "sendIMMenuItem";
            this.sendIMMenuItem.Size = new System.Drawing.Size(160, 22);
            this.sendIMMenuItem.Text = "Send Message";
            this.sendIMMenuItem.Click += new System.EventHandler(this.sendMessageToolStripMenuItem_Click);
            // 
            // sendMIMMenuItem
            // 
            this.sendMIMMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.sendMIMMenuItem.Name = "sendMIMMenuItem";
            this.sendMIMMenuItem.Size = new System.Drawing.Size(160, 22);
            this.sendMIMMenuItem.Text = "Send SMS";
            this.sendMIMMenuItem.Click += new System.EventHandler(this.sendMIMMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(157, 6);
            // 
            // importContactsMenuItem
            // 
            this.importContactsMenuItem.Name = "importContactsMenuItem";
            this.importContactsMenuItem.Size = new System.Drawing.Size(160, 22);
            this.importContactsMenuItem.Text = "Import Contacts";
            this.importContactsMenuItem.Click += new System.EventHandler(this.importContactsMenuItem_Click);
            // 
            // createCircleMenuItem
            // 
            this.createCircleMenuItem.Name = "createCircleMenuItem";
            this.createCircleMenuItem.Size = new System.Drawing.Size(160, 22);
            this.createCircleMenuItem.Text = "Circle Tests";
            this.createCircleMenuItem.Click += new System.EventHandler(this.createCircleMenuItem_Click);
            // 
            // leaveCircleToolStripMenuItem
            // 
            this.leaveCircleToolStripMenuItem.Name = "leaveCircleToolStripMenuItem";
            this.leaveCircleToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.leaveCircleToolStripMenuItem.Text = "Leave circle";
            this.leaveCircleToolStripMenuItem.Visible = false;
            this.leaveCircleToolStripMenuItem.Click += new System.EventHandler(this.leaveCircleToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(157, 6);
            // 
            // appearOfflineMenuItem
            // 
            this.appearOfflineMenuItem.Name = "appearOfflineMenuItem";
            this.appearOfflineMenuItem.Size = new System.Drawing.Size(160, 22);
            this.appearOfflineMenuItem.Text = "Appear offline";
            this.appearOfflineMenuItem.Click += new System.EventHandler(this.blockToolStripMenuItem_Click);
            // 
            // appearOnlineMenuItem
            // 
            this.appearOnlineMenuItem.Name = "appearOnlineMenuItem";
            this.appearOnlineMenuItem.Size = new System.Drawing.Size(160, 22);
            this.appearOnlineMenuItem.Text = "Appear online";
            this.appearOnlineMenuItem.Click += new System.EventHandler(this.unblockMenuItem_Click);
            // 
            // deleteMenuItem
            // 
            this.deleteMenuItem.Name = "deleteMenuItem";
            this.deleteMenuItem.Size = new System.Drawing.Size(160, 22);
            this.deleteMenuItem.Text = "Delete contact";
            this.deleteMenuItem.Click += new System.EventHandler(this.deleteMenuItem_Click);
            // 
            // liveProfileToolStripMenuItem
            // 
            this.liveProfileToolStripMenuItem.Name = "liveProfileToolStripMenuItem";
            this.liveProfileToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.liveProfileToolStripMenuItem.Text = "Live profile";
            this.liveProfileToolStripMenuItem.Click += new System.EventHandler(this.liveProfileToolStripMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Multiselect = true;
            // 
            // openImageDialog
            // 
            this.openImageDialog.Filter = "Supported Images|*.png;*.jpg;*.jpeg;*.gif";
            this.openImageDialog.Multiselect = true;
            this.openImageDialog.Title = "Select image";
            // 
            // sortContextMenu
            // 
            this.sortContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSortByStatus,
            this.toolStripSortBygroup});
            this.sortContextMenu.Name = "sortContextMenu";
            this.sortContextMenu.ShowCheckMargin = true;
            this.sortContextMenu.ShowImageMargin = false;
            this.sortContextMenu.Size = new System.Drawing.Size(140, 48);
            // 
            // toolStripSortByStatus
            // 
            this.toolStripSortByStatus.Checked = true;
            this.toolStripSortByStatus.CheckOnClick = true;
            this.toolStripSortByStatus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripSortByStatus.Name = "toolStripSortByStatus";
            this.toolStripSortByStatus.ShowShortcutKeys = false;
            this.toolStripSortByStatus.Size = new System.Drawing.Size(139, 22);
            this.toolStripSortByStatus.Text = "Sort by status";
            this.toolStripSortByStatus.Click += new System.EventHandler(this.toolStripSortByStatus_Click);
            // 
            // toolStripSortBygroup
            // 
            this.toolStripSortBygroup.CheckOnClick = true;
            this.toolStripSortBygroup.Name = "toolStripSortBygroup";
            this.toolStripSortBygroup.ShowShortcutKeys = false;
            this.toolStripSortBygroup.Size = new System.Drawing.Size(139, 22);
            this.toolStripSortBygroup.Text = "Sort by group";
            this.toolStripSortBygroup.Click += new System.EventHandler(this.toolStripSortBygroup_Click);
            // 
            // toolStripDeleteGroup
            // 
            this.toolStripDeleteGroup.Name = "toolStripDeleteGroup";
            this.toolStripDeleteGroup.Size = new System.Drawing.Size(142, 22);
            this.toolStripDeleteGroup.Text = "Delete group";
            this.toolStripDeleteGroup.Click += new System.EventHandler(this.toolStripDeleteGroup_Click);
            // 
            // groupContextMenu
            // 
            this.groupContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDeleteGroup});
            this.groupContextMenu.Name = "sortContextMenu";
            this.groupContextMenu.ShowCheckMargin = true;
            this.groupContextMenu.ShowImageMargin = false;
            this.groupContextMenu.Size = new System.Drawing.Size(143, 26);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.WhatsUpPanel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.statusBar, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(624, 616);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // WhatsUpPanel
            // 
            this.WhatsUpPanel.BackColor = System.Drawing.Color.White;
            this.WhatsUpPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.WhatsUpPanel.Controls.Add(this.lblNewsLink);
            this.WhatsUpPanel.Controls.Add(this.lblNews);
            this.WhatsUpPanel.Controls.Add(this.pbNewsPicture);
            this.WhatsUpPanel.Controls.Add(this.cmdNext);
            this.WhatsUpPanel.Controls.Add(this.cmdPrev);
            this.WhatsUpPanel.Controls.Add(this.lblWhatsup);
            this.WhatsUpPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.WhatsUpPanel.Location = new System.Drawing.Point(3, 533);
            this.WhatsUpPanel.Name = "WhatsUpPanel";
            this.WhatsUpPanel.Size = new System.Drawing.Size(618, 50);
            this.WhatsUpPanel.TabIndex = 8;
            this.WhatsUpPanel.Visible = false;
            // 
            // lblNewsLink
            // 
            this.lblNewsLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNewsLink.Location = new System.Drawing.Point(487, 25);
            this.lblNewsLink.Name = "lblNewsLink";
            this.lblNewsLink.Size = new System.Drawing.Size(69, 21);
            this.lblNewsLink.TabIndex = 5;
            this.lblNewsLink.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNewsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblNewsLink_LinkClicked);
            // 
            // lblNews
            // 
            this.lblNews.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNews.AutoEllipsis = true;
            this.lblNews.BackColor = System.Drawing.Color.Transparent;
            this.lblNews.Location = new System.Drawing.Point(97, 3);
            this.lblNews.Name = "lblNews";
            this.lblNews.Size = new System.Drawing.Size(383, 42);
            this.lblNews.TabIndex = 4;
            this.lblNews.Text = " *";
            this.lblNews.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbNewsPicture
            // 
            this.pbNewsPicture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbNewsPicture.BackColor = System.Drawing.Color.Transparent;
            this.pbNewsPicture.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbNewsPicture.Location = new System.Drawing.Point(570, 1);
            this.pbNewsPicture.Name = "pbNewsPicture";
            this.pbNewsPicture.Size = new System.Drawing.Size(45, 45);
            this.pbNewsPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbNewsPicture.TabIndex = 3;
            this.pbNewsPicture.TabStop = false;
            // 
            // cmdNext
            // 
            this.cmdNext.Location = new System.Drawing.Point(42, 22);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(22, 22);
            this.cmdNext.TabIndex = 2;
            this.cmdNext.Text = ">";
            this.cmdNext.UseVisualStyleBackColor = true;
            this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
            // 
            // cmdPrev
            // 
            this.cmdPrev.Location = new System.Drawing.Point(15, 22);
            this.cmdPrev.Name = "cmdPrev";
            this.cmdPrev.Size = new System.Drawing.Size(22, 22);
            this.cmdPrev.TabIndex = 1;
            this.cmdPrev.Text = "<";
            this.cmdPrev.UseVisualStyleBackColor = true;
            this.cmdPrev.Click += new System.EventHandler(this.cmdPrev_Click);
            // 
            // lblWhatsup
            // 
            this.lblWhatsup.AutoSize = true;
            this.lblWhatsup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblWhatsup.Location = new System.Drawing.Point(11, 3);
            this.lblWhatsup.Name = "lblWhatsup";
            this.lblWhatsup.Size = new System.Drawing.Size(66, 13);
            this.lblWhatsup.TabIndex = 0;
            this.lblWhatsup.Text = "What\'s Up";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 123);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel1.Controls.Add(this.propertyGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(618, 403);
            this.splitContainer1.SplitterDistance = 244;
            this.splitContainer1.TabIndex = 0;
            // 
            // propertyGrid
            // 
            this.propertyGrid.BackColor = System.Drawing.Color.White;
            this.propertyGrid.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.propertyGrid.CommandsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.HelpBackColor = System.Drawing.Color.White;
            this.propertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.propertyGrid.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(244, 403);
            this.propertyGrid.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.pnlTreeViewsContainer, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.SortPanel, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(370, 403);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // pnlTreeViewsContainer
            // 
            this.pnlTreeViewsContainer.Controls.Add(this.treeViewFilterList);
            this.pnlTreeViewsContainer.Controls.Add(this.treeViewFavoriteList);
            this.pnlTreeViewsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTreeViewsContainer.Location = new System.Drawing.Point(3, 32);
            this.pnlTreeViewsContainer.Name = "pnlTreeViewsContainer";
            this.pnlTreeViewsContainer.Size = new System.Drawing.Size(364, 368);
            this.pnlTreeViewsContainer.TabIndex = 3;
            // 
            // treeViewFilterList
            // 
            this.treeViewFilterList.BackColor = System.Drawing.Color.White;
            this.treeViewFilterList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewFilterList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewFilterList.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.treeViewFilterList.FullRowSelect = true;
            this.treeViewFilterList.HideSelection = false;
            this.treeViewFilterList.Indent = 20;
            this.treeViewFilterList.ItemHeight = 20;
            this.treeViewFilterList.Location = new System.Drawing.Point(0, 0);
            this.treeViewFilterList.Name = "treeViewFilterList";
            this.treeViewFilterList.ShowLines = false;
            this.treeViewFilterList.ShowPlusMinus = false;
            this.treeViewFilterList.ShowRootLines = false;
            this.treeViewFilterList.Size = new System.Drawing.Size(364, 368);
            this.treeViewFilterList.TabIndex = 5;
            this.treeViewFilterList.Visible = false;
            this.treeViewFilterList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeViewFilterList.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // treeViewFavoriteList
            // 
            this.treeViewFavoriteList.AllowDrop = true;
            this.treeViewFavoriteList.BackColor = System.Drawing.Color.White;
            this.treeViewFavoriteList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewFavoriteList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewFavoriteList.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.treeViewFavoriteList.FullRowSelect = true;
            this.treeViewFavoriteList.HideSelection = false;
            this.treeViewFavoriteList.ImageIndex = 0;
            this.treeViewFavoriteList.ImageList = this.ImageList1;
            this.treeViewFavoriteList.Indent = 15;
            this.treeViewFavoriteList.ItemHeight = 20;
            this.treeViewFavoriteList.Location = new System.Drawing.Point(0, 0);
            this.treeViewFavoriteList.Name = "treeViewFavoriteList";
            this.treeViewFavoriteList.SelectedImageIndex = 0;
            this.treeViewFavoriteList.ShowLines = false;
            this.treeViewFavoriteList.ShowPlusMinus = false;
            this.treeViewFavoriteList.ShowRootLines = false;
            this.treeViewFavoriteList.Size = new System.Drawing.Size(364, 368);
            this.treeViewFavoriteList.TabIndex = 4;
            this.treeViewFavoriteList.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeViewFavoriteList_ItemDrag);
            this.treeViewFavoriteList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeViewFavoriteList.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            this.treeViewFavoriteList.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeViewFavoriteList_DragDrop);
            this.treeViewFavoriteList.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeViewFavoriteList_DragEnter);
            this.treeViewFavoriteList.DragOver += new System.Windows.Forms.DragEventHandler(this.treeViewFavoriteList_DragOver);
            // 
            // SortPanel
            // 
            this.SortPanel.BackColor = System.Drawing.Color.White;
            this.SortPanel.Controls.Add(this.txtSearch);
            this.SortPanel.Controls.Add(this.btnAddNew);
            this.SortPanel.Controls.Add(this.btnSortBy);
            this.SortPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SortPanel.Location = new System.Drawing.Point(1, 1);
            this.SortPanel.Margin = new System.Windows.Forms.Padding(1);
            this.SortPanel.Name = "SortPanel";
            this.SortPanel.Size = new System.Drawing.Size(368, 27);
            this.SortPanel.TabIndex = 2;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.txtSearch.Location = new System.Drawing.Point(8, 2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(249, 21);
            this.txtSearch.TabIndex = 9;
            this.txtSearch.Text = "Search contacts";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNew.BackColor = System.Drawing.SystemColors.Control;
            this.btnAddNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAddNew.Location = new System.Drawing.Point(319, 1);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(46, 22);
            this.btnAddNew.TabIndex = 7;
            this.btnAddNew.Text = "add";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnSortBy
            // 
            this.btnSortBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSortBy.BackColor = System.Drawing.SystemColors.Control;
            this.btnSortBy.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnSortBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSortBy.Location = new System.Drawing.Point(269, 1);
            this.btnSortBy.Name = "btnSortBy";
            this.btnSortBy.Size = new System.Drawing.Size(46, 22);
            this.btnSortBy.TabIndex = 0;
            this.btnSortBy.Text = "sort";
            this.btnSortBy.UseVisualStyleBackColor = true;
            this.btnSortBy.Click += new System.EventHandler(this.btnSortBy_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel3.BackgroundImage = global::MSNPSharpClient.Properties.Resources.my_scene;
            this.tableLayoutPanel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 260F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 388F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.logoImageBox, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(624, 120);
            this.tableLayoutPanel3.TabIndex = 9;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(238, 2);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 126F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 126F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(384, 116);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel5.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.displayImageBox, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(380, 122);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(274, 116);
            this.panel1.TabIndex = 4;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.pnlLogin, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(274, 116);
            this.tableLayoutPanel6.TabIndex = 5;
            // 
            // pnlLogin
            // 
            this.pnlLogin.Controls.Add(this.pnlNameAndPM);
            this.pnlLogin.Controls.Add(this.accountTextBox);
            this.pnlLogin.Controls.Add(this.loginButton);
            this.pnlLogin.Controls.Add(this.passwordTextBox);
            this.pnlLogin.Controls.Add(this.comboStatus);
            this.pnlLogin.Controls.Add(this.comboPlaces);
            this.pnlLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLogin.Location = new System.Drawing.Point(3, 3);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(268, 110);
            this.pnlLogin.TabIndex = 1;
            // 
            // pnlNameAndPM
            // 
            this.pnlNameAndPM.Controls.Add(this.btnSetTheme);
            this.pnlNameAndPM.Controls.Add(this.btnSetMusic);
            this.pnlNameAndPM.Controls.Add(this.lblPM);
            this.pnlNameAndPM.Controls.Add(this.lblName);
            this.pnlNameAndPM.Location = new System.Drawing.Point(0, 0);
            this.pnlNameAndPM.Name = "pnlNameAndPM";
            this.pnlNameAndPM.Size = new System.Drawing.Size(266, 73);
            this.pnlNameAndPM.TabIndex = 15;
            this.pnlNameAndPM.Visible = false;
            // 
            // btnSetTheme
            // 
            this.btnSetTheme.Location = new System.Drawing.Point(3, 48);
            this.btnSetTheme.Name = "btnSetTheme";
            this.btnSetTheme.Size = new System.Drawing.Size(57, 22);
            this.btnSetTheme.TabIndex = 9;
            this.btnSetTheme.Tag = "0";
            this.btnSetTheme.Text = "Theme";
            this.btnSetTheme.UseVisualStyleBackColor = true;
            this.btnSetTheme.Click += new System.EventHandler(this.btnSetTheme_Click);
            // 
            // btnSetMusic
            // 
            this.btnSetMusic.Location = new System.Drawing.Point(3, 25);
            this.btnSetMusic.Name = "btnSetMusic";
            this.btnSetMusic.Size = new System.Drawing.Size(57, 22);
            this.btnSetMusic.TabIndex = 8;
            this.btnSetMusic.Tag = "0";
            this.btnSetMusic.Text = "Music";
            this.btnSetMusic.UseVisualStyleBackColor = true;
            this.btnSetMusic.Click += new System.EventHandler(this.btnSetMusic_Click);
            // 
            // lblPM
            // 
            this.lblPM.Location = new System.Drawing.Point(66, 25);
            this.lblPM.Multiline = true;
            this.lblPM.Name = "lblPM";
            this.lblPM.Size = new System.Drawing.Size(197, 44);
            this.lblPM.TabIndex = 7;
            this.lblPM.Leave += new System.EventHandler(this.lblName_Leave);
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(3, 1);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(260, 21);
            this.lblName.TabIndex = 6;
            this.lblName.Leave += new System.EventHandler(this.lblName_Leave);
            // 
            // accountTextBox
            // 
            this.accountTextBox.Location = new System.Drawing.Point(3, 2);
            this.accountTextBox.Name = "accountTextBox";
            this.accountTextBox.Size = new System.Drawing.Size(260, 21);
            this.accountTextBox.TabIndex = 9;
            this.accountTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.login_KeyPress);
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(152, 26);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(111, 22);
            this.loginButton.TabIndex = 8;
            this.loginButton.Tag = "0";
            this.loginButton.Text = "> Sign in";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(3, 27);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(140, 21);
            this.passwordTextBox.TabIndex = 10;
            this.passwordTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.login_KeyPress);
            // 
            // comboStatus
            // 
            this.comboStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboStatus.FormattingEnabled = true;
            this.comboStatus.ItemHeight = 15;
            this.comboStatus.Items.AddRange(new object[] {
            "Online",
            "Busy",
            "Away",
            "Hidden",
            "Offline"});
            this.comboStatus.Location = new System.Drawing.Point(3, 75);
            this.comboStatus.Name = "comboStatus";
            this.comboStatus.Size = new System.Drawing.Size(140, 21);
            this.comboStatus.TabIndex = 5;
            this.comboStatus.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboStatus_DrawItem);
            this.comboStatus.SelectedIndexChanged += new System.EventHandler(this.comboStatus_SelectedIndexChanged);
            this.comboStatus.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboStatus_KeyPress);
            // 
            // comboPlaces
            // 
            this.comboPlaces.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboPlaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPlaces.DropDownWidth = 400;
            this.comboPlaces.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboPlaces.FormattingEnabled = true;
            this.comboPlaces.ItemHeight = 15;
            this.comboPlaces.Location = new System.Drawing.Point(152, 73);
            this.comboPlaces.Name = "comboPlaces";
            this.comboPlaces.Size = new System.Drawing.Size(111, 23);
            this.comboPlaces.TabIndex = 6;
            this.comboPlaces.Visible = false;
            this.comboPlaces.SelectedIndexChanged += new System.EventHandler(this.comboPlaces_SelectedIndexChanged);
            // 
            // displayImageBox
            // 
            this.displayImageBox.BackColor = System.Drawing.Color.Transparent;
            this.displayImageBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.displayImageBox.Location = new System.Drawing.Point(281, 6);
            this.displayImageBox.Margin = new System.Windows.Forms.Padding(1, 6, 1, 1);
            this.displayImageBox.Name = "displayImageBox";
            this.displayImageBox.Size = new System.Drawing.Size(96, 96);
            this.displayImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.displayImageBox.TabIndex = 3;
            this.displayImageBox.TabStop = false;
            this.displayImageBox.Click += new System.EventHandler(this.displayImageBox_Click);
            // 
            // logoImageBox
            // 
            this.logoImageBox.Location = new System.Drawing.Point(0, 0);
            this.logoImageBox.Margin = new System.Windows.Forms.Padding(0);
            this.logoImageBox.Name = "logoImageBox";
            this.logoImageBox.Size = new System.Drawing.Size(255, 120);
            this.logoImageBox.TabIndex = 1;
            this.logoImageBox.TabStop = false;
            this.logoImageBox.DoubleClick += new System.EventHandler(this.logoImageBox_DoubleClick);
            // 
            // statusBar
            // 
            this.statusBar.AutoSize = true;
            this.statusBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusBar.Location = new System.Drawing.Point(3, 586);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(618, 30);
            this.statusBar.TabIndex = 10;
            this.statusBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // trayicon
            // 
            this.trayicon.ContextMenuStrip = this.cmsTray;
            this.trayicon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayicon.Icon")));
            this.trayicon.Text = "Butterfly Revived";
            this.trayicon.Visible = true;
            this.trayicon.DoubleClick += new System.EventHandler(this.trayicon_DoubleClick);
            // 
            // cmsTray
            // 
            this.cmsTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmOpen,
            this.toolStripMenuItem1,
            this.tsmExit});
            this.cmsTray.Name = "cmsTray";
            this.cmsTray.Size = new System.Drawing.Size(153, 76);
            // 
            // tsmOpen
            // 
            this.tsmOpen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tsmOpen.Name = "tsmOpen";
            this.tsmOpen.Size = new System.Drawing.Size(152, 22);
            this.tsmOpen.Text = "Open";
            this.tsmOpen.Click += new System.EventHandler(this.tsmOpen_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // tsmExit
            // 
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.Size = new System.Drawing.Size(152, 22);
            this.tsmExit.Text = "Exit";
            this.tsmExit.Click += new System.EventHandler(this.tsmExit_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(624, 616);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "ClientForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Butterfly Revived";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientForm_FormClosing);
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.Resize += new System.EventHandler(this.ClientForm_Resize);
            this.userMenuStrip.ResumeLayout(false);
            this.sortContextMenu.ResumeLayout(false);
            this.groupContextMenu.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.WhatsUpPanel.ResumeLayout(false);
            this.WhatsUpPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbNewsPicture)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.pnlTreeViewsContainer.ResumeLayout(false);
            this.SortPanel.ResumeLayout(false);
            this.SortPanel.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            this.pnlNameAndPM.ResumeLayout(false);
            this.pnlNameAndPM.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.displayImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoImageBox)).EndInit();
            this.cmsTray.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private OpenFileDialog openFileDialog;
        private OpenFileDialog openImageDialog;
        private ImageList ImageList1;
        private ContextMenuStrip userMenuStrip;
        private ToolStripMenuItem sendIMMenuItem;
        private ToolStripMenuItem appearOfflineMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem appearOnlineMenuItem;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripMenuItem sendMIMMenuItem;
        private ContextMenuStrip sortContextMenu;
        private ToolStripMenuItem toolStripSortByStatus;
        private ToolStripMenuItem toolStripSortBygroup;
        private ToolStripMenuItem toolStripDeleteGroup;
        private ContextMenuStrip groupContextMenu;
        private ToolStripMenuItem importContactsMenuItem;
        private ToolStripMenuItem createCircleMenuItem;
        private ToolStripMenuItem deleteMenuItem;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel WhatsUpPanel;
        private LinkLabel lblNewsLink;
        private Label lblNews;
        private PictureBox pbNewsPicture;
        private Button cmdNext;
        private Button cmdPrev;
        private Label lblWhatsup;
        private SplitContainer splitContainer1;
        private PropertyGrid propertyGrid;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel SortPanel;
        private TextBox txtSearch;
        private Button btnAddNew;
        private Button btnSortBy;
        private TableLayoutPanel tableLayoutPanel3;
        private Panel pnlTreeViewsContainer;
        private TreeView treeViewFavoriteList;
        private Label statusBar;
        private TreeView treeViewFilterList;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel5;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel6;
        private Panel pnlLogin;
        private Panel pnlNameAndPM;
        private Button btnSetMusic;
        private TextBox lblPM;
        private TextBox lblName;
        private TextBox accountTextBox;
        private Button loginButton;
        private TextBox passwordTextBox;
        private ComboBox comboStatus;
        private PictureBox displayImageBox;
        private PictureBox logoImageBox;
        private Button btnSetTheme;
        private ComboBox comboPlaces;
        private ToolStripMenuItem leaveCircleToolStripMenuItem;
        private ToolStripMenuItem liveProfileToolStripMenuItem;
        private NotifyIcon trayicon;
        private ContextMenuStrip cmsTray;
        private ToolStripMenuItem tsmOpen;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem tsmExit;
    }
}
