using System;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;

namespace MSNPSharpClient
{
    using MSNPSharp;
    using MSNPSharp.Apps;
    using MSNPSharp.P2P;
    using MSNPSharp.Core;
    using MSNPSharp.IO;

    /// <summary>
    /// Summary description for ConversationForm.
    /// </summary>
    public class ConversationForm : System.Windows.Forms.Form
    {
        private IContainer components;

        #region Windows Form Designer generated code

        private Panel panel1;
        private TextBox inputTextBox;
        private Panel panel2;
        private RtfRichTextBox richTextHistory;
        private PictureBox displayOwner;
        private PictureBox displayUser;

        private OpenFileDialog openFileDialog;
        private ToolStrip tsMessage;
        private ToolStripComboBox cbMessageFontName;
        private ToolStripComboBox cbMessageFontSize;
        private ToolStripSeparator tssMessageSeperator1;
        private ToolStripButton bMessageBold;
        private ToolStripButton bMessageItalic;
        private ToolStripButton bMessageUnderline;
        private ToolStripSeparator tssMessageSeperator2;
        private ToolStripButton bMessageSend;
        private ToolStripButton bMessageSendNudge;
        private Button btnSendFiles;
        private Button btnCustomEmoticon;
        private Button btnActivityTest;
        private ToolStripDropDownButton bMessageInsertEmoticon;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem bigRinToolStripMenuItem;
        private ToolStripMenuItem sadToolStripMenuItem;
        private ToolStripMenuItem winkToolStripMenuItem;
        private ToolStripMenuItem tongueOutToolStripMenuItem;
        private ToolStripButton bMessageFontColor;
        private OpenFileDialog openCustomEmoticonDialog;
        private ContextMenuStrip activitiesMenuStrip1;
        private Label systemLabel;
        private Button btnMultiparty;
        private ContextMenuStrip multipartyMenu;
        private ToolStripMenuItem surprisedToolStripMenuItem;
        private ColorDialog dlgColor;

        #region Flashing window interop code
        [DllImport("user32.dll")]
        static extern Int32 FlashWindowEx(ref FLASHWINFO pwfi);
        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            public UInt32 cbSize;
            public IntPtr hwnd;
            public Int32 dwFlags;
            public UInt32 uCount;
            public Int32 dwTimeout;
        }
        // stop flashing
        const int FLASHW_STOP = 0;
        // flash the window title 
        const int FLASHW_CAPTION = 1;
        // flash the taskbar button
        const int FLASHW_TRAY = 2;
        // 1 | 2
        const int FLASHW_ALL = 3;
        // flash continuously 
        const int FLASHW_TIMER = 4;
        // flash until the window comes to the foreground 
        const int FLASHW_TIMERNOFG = 12;
        #endregion

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConversationForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tsMessage = new System.Windows.Forms.ToolStrip();
            this.bMessageInsertEmoticon = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.bigRinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.winkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tongueOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.surprisedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bMessageSendNudge = new System.Windows.Forms.ToolStripButton();
            this.tssMessageSeperator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bMessageFontColor = new System.Windows.Forms.ToolStripButton();
            this.bMessageBold = new System.Windows.Forms.ToolStripButton();
            this.bMessageItalic = new System.Windows.Forms.ToolStripButton();
            this.bMessageUnderline = new System.Windows.Forms.ToolStripButton();
            this.cbMessageFontName = new System.Windows.Forms.ToolStripComboBox();
            this.cbMessageFontSize = new System.Windows.Forms.ToolStripComboBox();
            this.tssMessageSeperator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bMessageSend = new System.Windows.Forms.ToolStripButton();
            this.displayOwner = new System.Windows.Forms.PictureBox();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnMultiparty = new System.Windows.Forms.Button();
            this.systemLabel = new System.Windows.Forms.Label();
            this.btnActivityTest = new System.Windows.Forms.Button();
            this.btnCustomEmoticon = new System.Windows.Forms.Button();
            this.btnSendFiles = new System.Windows.Forms.Button();
            this.displayUser = new System.Windows.Forms.PictureBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.dlgColor = new System.Windows.Forms.ColorDialog();
            this.openCustomEmoticonDialog = new System.Windows.Forms.OpenFileDialog();
            this.activitiesMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.multipartyMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.richTextHistory = new MSNPSharpClient.RtfRichTextBox();
            this.panel1.SuspendLayout();
            this.tsMessage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.displayOwner)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.displayUser)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(163)))), ((int)(((byte)(186)))));
            this.panel1.Controls.Add(this.tsMessage);
            this.panel1.Controls.Add(this.displayOwner);
            this.panel1.Controls.Add(this.inputTextBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 272);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(663, 111);
            this.panel1.TabIndex = 0;
            // 
            // tsMessage
            // 
            this.tsMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tsMessage.AutoSize = false;
            this.tsMessage.Dock = System.Windows.Forms.DockStyle.None;
            this.tsMessage.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMessage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bMessageInsertEmoticon,
            this.bMessageSendNudge,
            this.tssMessageSeperator1,
            this.bMessageFontColor,
            this.bMessageBold,
            this.bMessageItalic,
            this.bMessageUnderline,
            this.cbMessageFontName,
            this.cbMessageFontSize,
            this.tssMessageSeperator2,
            this.bMessageSend});
            this.tsMessage.Location = new System.Drawing.Point(109, 3);
            this.tsMessage.Name = "tsMessage";
            this.tsMessage.Padding = new System.Windows.Forms.Padding(3, 0, 1, 0);
            this.tsMessage.Size = new System.Drawing.Size(550, 25);
            this.tsMessage.TabIndex = 8;
            // 
            // bMessageInsertEmoticon
            // 
            this.bMessageInsertEmoticon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bMessageInsertEmoticon.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.bigRinToolStripMenuItem,
            this.sadToolStripMenuItem,
            this.winkToolStripMenuItem,
            this.tongueOutToolStripMenuItem,
            this.surprisedToolStripMenuItem});
            this.bMessageInsertEmoticon.Image = global::MSNPSharpClient.Properties.Resources.smile;
            this.bMessageInsertEmoticon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bMessageInsertEmoticon.Name = "bMessageInsertEmoticon";
            this.bMessageInsertEmoticon.Size = new System.Drawing.Size(29, 22);
            this.bMessageInsertEmoticon.Text = "toolStripDropDownButton1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::MSNPSharpClient.Properties.Resources.smile;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(137, 22);
            this.toolStripMenuItem1.Text = "Simley";
            this.toolStripMenuItem1.ToolTipText = ":)";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.emotionDropDown_Click);
            // 
            // bigRinToolStripMenuItem
            // 
            this.bigRinToolStripMenuItem.Image = global::MSNPSharpClient.Properties.Resources.openmouthedsmile;
            this.bigRinToolStripMenuItem.Name = "bigRinToolStripMenuItem";
            this.bigRinToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.bigRinToolStripMenuItem.Text = "Big Grin";
            this.bigRinToolStripMenuItem.ToolTipText = ":d";
            this.bigRinToolStripMenuItem.Click += new System.EventHandler(this.emotionDropDown_Click);
            // 
            // sadToolStripMenuItem
            // 
            this.sadToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sadToolStripMenuItem.Image")));
            this.sadToolStripMenuItem.Name = "sadToolStripMenuItem";
            this.sadToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.sadToolStripMenuItem.Text = "Sad";
            this.sadToolStripMenuItem.ToolTipText = ":(";
            this.sadToolStripMenuItem.Click += new System.EventHandler(this.emotionDropDown_Click);
            // 
            // winkToolStripMenuItem
            // 
            this.winkToolStripMenuItem.Image = global::MSNPSharpClient.Properties.Resources.winking;
            this.winkToolStripMenuItem.Name = "winkToolStripMenuItem";
            this.winkToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.winkToolStripMenuItem.Text = "Wink";
            this.winkToolStripMenuItem.ToolTipText = ";)";
            this.winkToolStripMenuItem.Click += new System.EventHandler(this.emotionDropDown_Click);
            // 
            // tongueOutToolStripMenuItem
            // 
            this.tongueOutToolStripMenuItem.Image = global::MSNPSharpClient.Properties.Resources.smilewithtongue;
            this.tongueOutToolStripMenuItem.Name = "tongueOutToolStripMenuItem";
            this.tongueOutToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.tongueOutToolStripMenuItem.Text = "Tongue Out";
            this.tongueOutToolStripMenuItem.ToolTipText = ":p";
            this.tongueOutToolStripMenuItem.Click += new System.EventHandler(this.emotionDropDown_Click);
            // 
            // surprisedToolStripMenuItem
            // 
            this.surprisedToolStripMenuItem.Image = global::MSNPSharpClient.Properties.Resources.surprised;
            this.surprisedToolStripMenuItem.Name = "surprisedToolStripMenuItem";
            this.surprisedToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.surprisedToolStripMenuItem.Text = "Surprised";
            this.surprisedToolStripMenuItem.ToolTipText = ":o";
            this.surprisedToolStripMenuItem.Click += new System.EventHandler(this.emotionDropDown_Click);
            // 
            // bMessageSendNudge
            // 
            this.bMessageSendNudge.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bMessageSendNudge.Image = global::MSNPSharpClient.Properties.Resources.nudge;
            this.bMessageSendNudge.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.bMessageSendNudge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bMessageSendNudge.Name = "bMessageSendNudge";
            this.bMessageSendNudge.Size = new System.Drawing.Size(28, 22);
            this.bMessageSendNudge.Text = "Send a &nudge";
            this.bMessageSendNudge.Click += new System.EventHandler(this.bMessageSendNudge_Click);
            // 
            // tssMessageSeperator1
            // 
            this.tssMessageSeperator1.Name = "tssMessageSeperator1";
            this.tssMessageSeperator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bMessageFontColor
            // 
            this.bMessageFontColor.Image = global::MSNPSharpClient.Properties.Resources.Color_fontHS;
            this.bMessageFontColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bMessageFontColor.Name = "bMessageFontColor";
            this.bMessageFontColor.Size = new System.Drawing.Size(56, 22);
            this.bMessageFontColor.Text = "Color";
            this.bMessageFontColor.Click += new System.EventHandler(this.bMessageFontColor_Click);
            // 
            // bMessageBold
            // 
            this.bMessageBold.CheckOnClick = true;
            this.bMessageBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bMessageBold.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bMessageBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bMessageBold.Name = "bMessageBold";
            this.bMessageBold.Size = new System.Drawing.Size(23, 22);
            this.bMessageBold.Text = "B";
            this.bMessageBold.ToolTipText = "Bold";
            this.bMessageBold.CheckedChanged += new System.EventHandler(this.bMessageBoldItalicUnderline_CheckedChanged);
            // 
            // bMessageItalic
            // 
            this.bMessageItalic.CheckOnClick = true;
            this.bMessageItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bMessageItalic.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bMessageItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bMessageItalic.Name = "bMessageItalic";
            this.bMessageItalic.Size = new System.Drawing.Size(23, 22);
            this.bMessageItalic.Text = "I";
            this.bMessageItalic.ToolTipText = "Italic";
            this.bMessageItalic.CheckedChanged += new System.EventHandler(this.bMessageBoldItalicUnderline_CheckedChanged);
            // 
            // bMessageUnderline
            // 
            this.bMessageUnderline.CheckOnClick = true;
            this.bMessageUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bMessageUnderline.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bMessageUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bMessageUnderline.Name = "bMessageUnderline";
            this.bMessageUnderline.Size = new System.Drawing.Size(23, 22);
            this.bMessageUnderline.Text = "U";
            this.bMessageUnderline.ToolTipText = "Underline";
            this.bMessageUnderline.CheckedChanged += new System.EventHandler(this.bMessageBoldItalicUnderline_CheckedChanged);
            // 
            // cbMessageFontName
            // 
            this.cbMessageFontName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbMessageFontName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbMessageFontName.DropDownWidth = 200;
            this.cbMessageFontName.MaxDropDownItems = 12;
            this.cbMessageFontName.Name = "cbMessageFontName";
            this.cbMessageFontName.Size = new System.Drawing.Size(140, 25);
            this.cbMessageFontName.ToolTipText = "Font Name";
            this.cbMessageFontName.SelectedIndexChanged += new System.EventHandler(this.cbMessageFontName_SelectedIndexChanged);
            this.cbMessageFontName.Validating += new System.ComponentModel.CancelEventHandler(this.cbMessageFontName_Validating);
            this.cbMessageFontName.Validated += new System.EventHandler(this.cbMessageFontName_Validated);
            // 
            // cbMessageFontSize
            // 
            this.cbMessageFontSize.Items.AddRange(new object[] {
            "8",
            "9",
            "10",
            "11",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "28",
            "36",
            "48",
            "72"});
            this.cbMessageFontSize.MaxDropDownItems = 12;
            this.cbMessageFontSize.Name = "cbMessageFontSize";
            this.cbMessageFontSize.Size = new System.Drawing.Size(75, 25);
            this.cbMessageFontSize.ToolTipText = "Font Size";
            this.cbMessageFontSize.SelectedIndexChanged += new System.EventHandler(this.cbMessageFontSize_SelectedIndexChanged);
            this.cbMessageFontSize.Validating += new System.ComponentModel.CancelEventHandler(this.cbMessageFontSize_Validating);
            this.cbMessageFontSize.Validated += new System.EventHandler(this.cbMessageFontSize_Validated);
            // 
            // tssMessageSeperator2
            // 
            this.tssMessageSeperator2.Name = "tssMessageSeperator2";
            this.tssMessageSeperator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bMessageSend
            // 
            this.bMessageSend.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.bMessageSend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bMessageSend.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.bMessageSend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bMessageSend.Name = "bMessageSend";
            this.bMessageSend.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.bMessageSend.Size = new System.Drawing.Size(57, 22);
            this.bMessageSend.Text = "   &Send   ";
            this.bMessageSend.Click += new System.EventHandler(this.bMessageSend_Click);
            // 
            // displayOwner
            // 
            this.displayOwner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.displayOwner.BackColor = System.Drawing.Color.White;
            this.displayOwner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.displayOwner.Location = new System.Drawing.Point(3, 3);
            this.displayOwner.Name = "displayOwner";
            this.displayOwner.Size = new System.Drawing.Size(100, 100);
            this.displayOwner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.displayOwner.TabIndex = 0;
            this.displayOwner.TabStop = false;
            // 
            // inputTextBox
            // 
            this.inputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputTextBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputTextBox.Location = new System.Drawing.Point(109, 31);
            this.inputTextBox.Multiline = true;
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.inputTextBox.Size = new System.Drawing.Size(550, 72);
            this.inputTextBox.TabIndex = 1;
            this.inputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputTextBox_KeyDown);
            this.inputTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.inputTextBox_KeyPress);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnMultiparty);
            this.panel2.Controls.Add(this.systemLabel);
            this.panel2.Controls.Add(this.btnActivityTest);
            this.panel2.Controls.Add(this.btnCustomEmoticon);
            this.panel2.Controls.Add(this.btnSendFiles);
            this.panel2.Controls.Add(this.displayUser);
            this.panel2.Controls.Add(this.richTextHistory);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(663, 272);
            this.panel2.TabIndex = 0;
            // 
            // btnMultiparty
            // 
            this.btnMultiparty.Location = new System.Drawing.Point(3, 82);
            this.btnMultiparty.Name = "btnMultiparty";
            this.btnMultiparty.Size = new System.Drawing.Size(100, 23);
            this.btnMultiparty.TabIndex = 6;
            this.btnMultiparty.Text = "Multiparty chat";
            this.btnMultiparty.UseVisualStyleBackColor = true;
            this.btnMultiparty.Click += new System.EventHandler(this.btnMultiparty_Click);
            // 
            // systemLabel
            // 
            this.systemLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.systemLabel.AutoSize = true;
            this.systemLabel.BackColor = System.Drawing.Color.Transparent;
            this.systemLabel.Location = new System.Drawing.Point(109, 253);
            this.systemLabel.Name = "systemLabel";
            this.systemLabel.Size = new System.Drawing.Size(22, 13);
            this.systemLabel.TabIndex = 5;
            this.systemLabel.Text = "-----";
            this.systemLabel.Visible = false;
            // 
            // btnActivityTest
            // 
            this.btnActivityTest.Location = new System.Drawing.Point(3, 136);
            this.btnActivityTest.Name = "btnActivityTest";
            this.btnActivityTest.Size = new System.Drawing.Size(100, 24);
            this.btnActivityTest.TabIndex = 4;
            this.btnActivityTest.Text = "Start Activity";
            this.btnActivityTest.UseVisualStyleBackColor = true;
            this.btnActivityTest.Visible = false;
            this.btnActivityTest.Click += new System.EventHandler(this.btnActivityTest_Click);
            // 
            // btnCustomEmoticon
            // 
            this.btnCustomEmoticon.Location = new System.Drawing.Point(3, 41);
            this.btnCustomEmoticon.Name = "btnCustomEmoticon";
            this.btnCustomEmoticon.Size = new System.Drawing.Size(100, 23);
            this.btnCustomEmoticon.TabIndex = 3;
            this.btnCustomEmoticon.Text = "Custom Emoticon...";
            this.btnCustomEmoticon.UseVisualStyleBackColor = true;
            this.btnCustomEmoticon.Click += new System.EventHandler(this.bMessageSendCustomEmoticon_Click);
            // 
            // btnSendFiles
            // 
            this.btnSendFiles.Location = new System.Drawing.Point(3, 12);
            this.btnSendFiles.Name = "btnSendFiles";
            this.btnSendFiles.Size = new System.Drawing.Size(100, 23);
            this.btnSendFiles.TabIndex = 1;
            this.btnSendFiles.Text = "Send Files...";
            this.btnSendFiles.UseVisualStyleBackColor = true;
            this.btnSendFiles.Click += new System.EventHandler(this.bMessageSendFiles_Click);
            // 
            // displayUser
            // 
            this.displayUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.displayUser.BackColor = System.Drawing.Color.White;
            this.displayUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.displayUser.Location = new System.Drawing.Point(3, 166);
            this.displayUser.Name = "displayUser";
            this.displayUser.Size = new System.Drawing.Size(100, 100);
            this.displayUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.displayUser.TabIndex = 0;
            this.displayUser.TabStop = false;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Multiselect = true;
            // 
            // openCustomEmoticonDialog
            // 
            this.openCustomEmoticonDialog.Filter = "Image File (*.png, *.jpg, *.bmp, *.gif|*.png;*.jpg;*.bmp;*.gif";
            this.openCustomEmoticonDialog.Title = "Open Image to transfer as an custom emoticon";
            // 
            // activitiesMenuStrip1
            // 
            this.activitiesMenuStrip1.Name = "activitiesMenuStrip1";
            this.activitiesMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // multipartyMenu
            // 
            this.multipartyMenu.Name = "multipartyMenu";
            this.multipartyMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // richTextHistory
            // 
            this.richTextHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextHistory.BackColor = System.Drawing.Color.Snow;
            this.richTextHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextHistory.HiglightColor = MSNPSharpClient.RtfRichTextBox.RtfColor.White;
            this.richTextHistory.Location = new System.Drawing.Point(109, 3);
            this.richTextHistory.Name = "richTextHistory";
            this.richTextHistory.ReadOnly = true;
            this.richTextHistory.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextHistory.Size = new System.Drawing.Size(554, 264);
            this.richTextHistory.TabIndex = 0;
            this.richTextHistory.TabStop = false;
            this.richTextHistory.Text = "";
            this.richTextHistory.TextColor = MSNPSharpClient.RtfRichTextBox.RtfColor.Black;
            // 
            // ConversationForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(663, 383);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ConversationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Conversation - MSNPSharp";
            this.Activated += new System.EventHandler(this.ConversationForm_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ConversationForm_Closing);
            this.Load += new System.EventHandler(this.ConversationForm_Load);
            this.Shown += new System.EventHandler(this.ConversationForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tsMessage.ResumeLayout(false);
            this.tsMessage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.displayOwner)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.displayUser)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private Messenger _messenger = null;
        private Contact remoteContact;
        private DateTime lastTypingMessageSent = DateTime.Now;

        public Contact RemoteContact
        {
            get
            {
                return remoteContact;
            }
        }

        protected ConversationForm()
        {
        }

        /// <summary>
        /// For sending and receiving YIM messages.
        /// </summary>
        /// <param name="messenger"></param>
        /// <param name="contact"></param>
        public ConversationForm(Messenger messenger, Contact contact)
        {
            InitializeComponent();
            this.GotFocus += ConversationForm_GotFocus;
            _messenger = messenger;
            remoteContact = contact;

            if (contact.ClientType != IMAddressInfoType.WindowsLive)
            {
                btnActivityTest.Enabled = false;
                btnCustomEmoticon.Enabled = false;
                btnSendFiles.Enabled = false;
                btnMultiparty.Enabled = false;
            }
        }

        private void ConversationForm_GotFocus(object sender, EventArgs e)
        {
            Flash(true);
        }

        public void OnMessageReceived(object sender, MessageArrivedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<MessageArrivedEventArgs>(OnMessageReceived), new object[] { sender, e });
            }
            else
            {
                if (e is NudgeArrivedEventArgs)
                {
                    MakeVisible(sender, e);
                    PrintNudge(e as NudgeArrivedEventArgs);
                }
                else if (e is TypingArrivedEventArgs)
                {
                    MakeVisible(sender, e);
                    PrintTyping(e as TypingArrivedEventArgs);
                }
                else if (e is TextMessageArrivedEventArgs)
                {
                    MakeVisible(sender, e);
                    NewMessageNotifier(sender, e);
                    PrintText(e.Sender, (e as TextMessageArrivedEventArgs).TextMessage);
                }
                else if (e is EmoticonArrivedEventArgs)
                {
                    MakeVisible(sender, e);

                    Emoticon emo = (e as EmoticonArrivedEventArgs).Emoticon;
                    if (emo != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        byte[] byt = new byte[emo.OpenStream().Length];
                        emo.OpenStream().Seek(0, SeekOrigin.Begin);
                        emo.OpenStream().Read(byt, 0, byt.Length);
                        ms.Write(byt, 0, byt.Length);

                        richTextHistory.Emotions[emo.Shortcut] = new Bitmap(Image.FromStream(ms));

                        ms.Close();
                    }

                    while (richTextHistory.HasEmotion)
                    {
                        richTextHistory.InsertEmotion();
                    }
                }
                else if (e is WinkEventArgs)
                {
                    MakeVisible(sender, e);

                    Wink wink = (e as WinkEventArgs).Wink;
                    if (wink != null)
                    {
                        string path = Path.Combine(Settings.SavePath, Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".cab");
                        FileStream fs = new FileStream(path, FileMode.Create);

                        byte[] byt = new byte[wink.OpenStream().Length];
                        wink.OpenStream().Seek(0, SeekOrigin.Begin);
                        wink.OpenStream().Read(byt, 0, byt.Length);
                        fs.Write(byt, 0, byt.Length);
                        fs.Close();

                        DisplaySystemMessage("WINK saved as " + path);
                    }


                }
            }
        }

        private void NewMessageNotifier(object sender, MessageArrivedEventArgs e)
        {
            Flash(false);
        }

        private void Flash(bool stop)
        {
            FLASHWINFO fw = new FLASHWINFO();
            fw.cbSize = Convert.ToUInt32(Marshal.SizeOf(typeof(FLASHWINFO)));
            fw.hwnd = Handle;
            if (!stop)
                fw.dwFlags = 2;
            else
                fw.dwFlags = 0;
            fw.uCount = UInt32.MaxValue;
            FlashWindowEx(ref fw);
        }


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }


        private void inputTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Return) && (e.Alt || e.Control || e.Shift))
            {
                return;
            }

            try
            {
                // To avoid RateLimitExceed Exception from server, we should be carefull
                // with sending the typing message.
                if(DateTime.Now - lastTypingMessageSent >= new TimeSpan(0, 5, 0)){
                    remoteContact.SendTypingMessage();
                    lastTypingMessageSent = DateTime.Now;
                }

            }
            catch (Exception)
            {
            }

            if (e.KeyCode == Keys.Return)
            {
                if (!inputTextBox.Text.Equals(String.Empty))
                {
                    bMessageSend.PerformClick();
                }
                e.Handled = true;
            }


        }

        private void inputTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\x001b')
            {
                Close();
            }
            else if ((e.KeyChar == '\r') && inputTextBox.Text.Equals(string.Empty))
            {
                e.Handled = true;
            }
        }

        private void MakeVisible(object sender, EventArgs e)
        {
            Show();
        }

        private void PrintNudge(NudgeArrivedEventArgs e)
        {
            PerformNudge();

            string message = e.OriginalSender + " has sent a nudge!";

            if (e.OriginalSender != e.Sender)
            {
                message += " (By: " + e.Sender.Account + ")";
            }

            DisplaySystemMessage(message);
        }

        private void PrintTyping(TypingArrivedEventArgs e)
        {
            string message = e.OriginalSender + " is typing...";

            if (e.OriginalSender != e.Sender)
            {
                message += " (By: " + e.Sender.Account + ")";
            }

            DisplaySystemMessage(message);
        }

        private delegate void DisplaySystemMessageDelegate(string status);
        public void DisplaySystemMessage(string systemMessage)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new DisplaySystemMessageDelegate(DisplaySystemMessage), new object[] { systemMessage });
                return;
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            systemLabel.Text = systemMessage;
            systemLabel.Visible = true;

            while (stopwatch.ElapsedMilliseconds < 3000)
            {
                System.Threading.Thread.Sleep(10);
                Application.DoEvents();
            }

            systemLabel.Visible = false;
            systemLabel.Text = String.Empty;
            stopwatch.Stop();
        }


        private void ConversationForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (remoteContact.ClientType == IMAddressInfoType.TemporaryGroup)
            {
                _messenger.Nameserver.LeaveMultiparty(remoteContact);
            }

            remoteContact.DisplayImageChanged -= Contact_DisplayImageChanged;
            remoteContact.DisplayImageContextChanged -= Contact_DisplayImageConextChanged;

            _messenger.Nameserver.JoinedGroupChat -= (Nameserver_JoinedGroupChat);
            _messenger.Nameserver.LeftGroupChat -= (Nameserver_LeftGroupChat);
        }

        private void PrintText(Contact c, TextMessage message)
        {
            richTextHistory.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            richTextHistory.AppendText(Environment.NewLine);
            //richTextHistory.SelectionColor = c.Account == _messenger.Owner.Account ? Color.Blue : Color.Black;
            richTextHistory.SelectionColor = Color.Gray;
            richTextHistory.AppendText(c.Name + " says ");

            richTextHistory.AppendText("(" + DateTime.Now.ToLongTimeString() + ")" + ": " + Environment.NewLine);
            
            richTextHistory.SelectionColor = message.Color;

            richTextHistory.AppendText(message.Text);
            richTextHistory.AppendText(Environment.NewLine);
            richTextHistory.ScrollToCaret();

            while (richTextHistory.HasEmotion)
            {
                richTextHistory.InsertEmotion();
            }
        }

        private Image CreateImageFromColor(Color color, Size buttonSize)
        {
            Bitmap bitmap = new Bitmap(buttonSize.Width, buttonSize.Height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(color);
            }
            return bitmap;
        }

        private void UpdateTextFonts()
        {
            FontStyle fontStyle = FontStyle.Regular;

            if (bMessageBold.Checked)
                fontStyle |= FontStyle.Bold;
            if (bMessageItalic.Checked)
                fontStyle |= FontStyle.Italic;
            if (bMessageUnderline.Checked)
                fontStyle |= FontStyle.Underline;

            Single fontSize = Single.Parse(cbMessageFontSize.Text);
            Font messageFont = null;

        CreateFont:
            try
            {
                messageFont = new Font(cbMessageFontName.Text, fontSize, fontStyle, GraphicsUnit.Point);
            }
            catch (ArgumentException)
            {
                fontStyle++;
                if (fontStyle <= (FontStyle.Strikeout | FontStyle.Underline | FontStyle.Italic | FontStyle.Bold))
                    goto CreateFont;
            }

            if (messageFont == null)
                return;

            bMessageBold.Checked = (fontStyle & FontStyle.Bold) != FontStyle.Regular;
            bMessageItalic.Checked = (fontStyle & FontStyle.Italic) != FontStyle.Regular;
            bMessageUnderline.Checked = (fontStyle & FontStyle.Underline) != FontStyle.Regular;

            richTextHistory.Font = new Font(richTextHistory.Font.FontFamily, messageFont.Size);
            inputTextBox.Font = messageFont;
        }

        private void SetUserTileToPictureBox(object sender, ObjectEventArgs e)
        {
            if (displayUser.InvokeRequired)
            {
                displayUser.Invoke(new EventHandler<ObjectEventArgs>(SetUserTileToPictureBox), new object[] { sender, e });
            }
            else
            {
                try
                {
                    Image img = Image.FromStream(new MemoryStream((byte[])e.Object));
                    displayUser.Image = img;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Get UserTile error: " + ex.Message);
                }
            }
        }

        private void ConversationForm_Load(object sender, EventArgs e)
        {
            Text = "Conversation with " + remoteContact.Account + " - MSNPSharp";

            switch (remoteContact.ClientType)
            {
                case IMAddressInfoType.Yahoo:
                    Icon = Properties.Resources.yahoo_ico;
                    break;
                case IMAddressInfoType.Connect:
                    Icon = Properties.Resources.facebook_icon;
                    break;
                default:
                    Icon = Properties.Resources.msn_ico;
                    break;
            }

            if (_messenger.Owner.DisplayImage.Image != null)
                displayOwner.Image = _messenger.Owner.DisplayImage.Image;
            else
                displayOwner.Image = DisplayImage.DefaultImage;

            lock (richTextHistory.Emotions)
            {
                richTextHistory.Emotions[":)"] = Properties.Resources.smile;
                richTextHistory.Emotions[":d"] = Properties.Resources.openmouthedsmile;
                richTextHistory.Emotions[":("] = Properties.Resources.sad;
                richTextHistory.Emotions[";)"] = Properties.Resources.winking;
                richTextHistory.Emotions[":p"] = Properties.Resources.smilewithtongue;
                richTextHistory.Emotions[":o"] = Properties.Resources.surprised;
            }

            foreach (FontFamily ff in FontFamily.Families)
            {
                cbMessageFontName.Items.Add(ff.Name);
            }
            cbMessageFontName.Text = inputTextBox.Font.Name;
            cbMessageFontSize.Text = inputTextBox.Font.Size.ToString();
            //bMessageFontColor.Image = CreateImageFromColor(inputTextBox.ForeColor, inputTextBox.Size);

            cbMessageFontName.Tag = cbMessageFontName.Text;
            cbMessageFontSize.Tag = cbMessageFontSize.Text;

            UpdateTextFonts();

            inputTextBox.Select();

            List<ActivityInfo> activities = new List<ActivityInfo>();
            ActivityInfo ai = new ActivityInfo();
            ai.AppID = 7;
            ai.ActivityName = "Test Activity";
            ai.ActivityData = @"http://code.google.com/p/msnp-sharp/";
            activities.Add(ai);

            ai = new ActivityInfo();
            ai.AppID = 10551613;
            ai.ActivityName = "Tic Tac Toe";
            ai.ActivityData = @"http://code.google.com/p/msnp-sharp/";
            activities.Add(ai);



            ai = new ActivityInfo();
            ai.AppID = 10551728;
            ai.ActivityName = "Messenger Fun";
            ai.ActivityData = @"http://code.google.com/p/msnp-sharp/";
            activities.Add(ai);

            ai = new ActivityInfo();
            ai.AppID = 10551801;
            ai.ActivityName = "Messenger TV";
            ai.ActivityData = @"http://code.google.com/p/msnp-sharp/";
            activities.Add(ai);

            foreach (ActivityInfo aix in activities)
            {
                ToolStripMenuItem tsi = new ToolStripMenuItem();
                tsi.Tag = aix;
                tsi.Text = aix.ActivityName;
                tsi.Click += activitiesMenu_Click;

                activitiesMenuStrip1.Items.Add(tsi);
            }

            _messenger.Nameserver.JoinedGroupChat += (Nameserver_JoinedGroupChat);
            _messenger.Nameserver.LeftGroupChat += (Nameserver_LeftGroupChat);

        }


        private void ConversationForm_Shown(object sender, EventArgs e)
        {
            switch (remoteContact.ClientType)
            {
                case IMAddressInfoType.Connect:
                    displayUser.Image = Properties.Resources.facebook_logo.Clone() as Image;
                    break;
                case IMAddressInfoType.Yahoo:
                    displayUser.Image = Properties.Resources.YahooMessenger_logo.Clone() as Image;
                    break;
                default:
                    if (remoteContact.DisplayImage.Image != null)
                        displayUser.Image = remoteContact.DisplayImage.Image;
                    else
                        displayUser.Image = DisplayImage.DefaultImage;
                    break;
            }

            remoteContact.DisplayImageChanged += new EventHandler<DisplayImageChangedEventArgs>(Contact_DisplayImageChanged);
            remoteContact.DisplayImageContextChanged += new EventHandler<DisplayImageChangedEventArgs>(Contact_DisplayImageConextChanged);

            // request the image, if not already available
            if (remoteContact.Status != PresenceStatus.Offline)
            {
                if (remoteContact.DisplayImage != remoteContact.UserTileLocation)
                {
                    try
                    {
                        RequestDisplayImage(remoteContact, null);
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLineIf(Settings.TraceSwitch.TraceError, ex.Message + "\r\n StackTrace: " + ex.StackTrace);
                    }
                }
            }

            if (remoteContact.UserTileURL != null && remoteContact.ClientType != IMAddressInfoType.WindowsLive) // For WLM display image, shouldn't overwrite with user tile.
            {
                HttpAsyncDataDownloader.BeginDownload(remoteContact.UserTileURL.AbsoluteUri, new EventHandler<ObjectEventArgs>(SetUserTileToPictureBox), _messenger.ConnectivitySettings);
            }
        }

        private void RequestDisplayImage(Contact remoteContact, DisplayImage updateImage)
        {
            if (remoteContact.P2PVersionSupported != P2PVersion.None &&
                remoteContact.ClientType == IMAddressInfoType.WindowsLive &&
                updateImage != remoteContact.UserTileLocation)
            {
                if (updateImage == null)
                    updateImage = remoteContact.DisplayImage;

                ObjectTransfer ot = _messenger.RequestMsnObject(remoteContact, updateImage);
                ot.TransferFinished += delegate
                {
                    Trace.WriteLineIf(Settings.TraceSwitch.TraceInfo,
                        "MSNObject transfered... See contact.DisplayImageChanged event");
                };
            }
        }

        private void Contact_DisplayImageConextChanged(object sender, DisplayImageChangedEventArgs e)
        {
            try
            {
                RequestDisplayImage(remoteContact, null);
            }
            catch (Exception ex)
            {
                Trace.WriteLineIf(Settings.TraceSwitch.TraceError, ex.Message + "\r\n StackTrace: " + ex.StackTrace);
            }
        }

        private void Contact_DisplayImageChanged(object sender, DisplayImageChangedEventArgs e)
        {
            if (displayUser.InvokeRequired)
            {
                Invoke(new EventHandler<DisplayImageChangedEventArgs>(Contact_DisplayImageChanged), new object[] { sender, e });
            }
            else
            {
                displayUser.Image = e.NewDisplayImage.Image;
            }
        }

        private void emotionDropDown_Click(object sender, EventArgs args)
        {
            ToolStripItem item = (ToolStripItem)sender;
            inputTextBox.AppendText(item.ToolTipText);
            inputTextBox.Focus();
        }

        private void PerformNudge()
        {
            Stopwatch stopwatch = new Stopwatch();

            Random rnd = new Random();
            int x = Left;
            int y = Top;

            stopwatch.Start();
            while (stopwatch.ElapsedMilliseconds < 500)
            {
                Left = rnd.Next(Location.X - 5, Location.X + 5);
                Top = rnd.Next(Location.Y - 1, Location.Y + 1);

                System.Threading.Thread.Sleep(10);
                Application.DoEvents();
            }
            stopwatch.Stop();

            Left = x;
            Top = y;
        }

        private void bMessageSend_Click(object sender, EventArgs e)
        {
            if (inputTextBox.Text.Length == 0)
                return;

            TextMessage message = new TextMessage(inputTextBox.Text);
            message.Font = inputTextBox.Font.Name;
            message.Color = inputTextBox.ForeColor;

            if (inputTextBox.Font.Bold)
                message.Decorations |= TextDecorations.Bold;
            if (inputTextBox.Font.Italic)
                message.Decorations |= TextDecorations.Italic;
            if (inputTextBox.Font.Underline)
                message.Decorations |= TextDecorations.Underline;

            inputTextBox.Clear();
            inputTextBox.Focus();

            remoteContact.SendMessage(message);
            PrintText(_messenger.Owner, message);
        }

        private void bMessageSendNudge_Click(object sender, EventArgs e)
        {
            try
            {
                remoteContact.SendNudge();
                DisplaySystemMessage("You send a nudge.");
                PerformNudge();
            }
            catch (Exception)
            {

                DisplaySystemMessage("Remote contact not online.");
            }
        }

        private void bMessageSendFiles_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (!remoteContact.Online)
                {
                    DisplaySystemMessage("All contacts are offline or this contact doesn't support receiving files.");
                    return;
                }

                try
                {

                    foreach (string filename in openFileDialog.FileNames)
                    {

                        FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                        FileTransfer fileTransferApp = _messenger.SendFile(remoteContact, filename, fileStream);

                        //FileTransferForm ftForm = new FileTransferForm(fileTransferApp.P2PSession);
                        //ftForm.Show(this);

                    }

                }
                catch (MSNPSharpException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void bMessageSendCustomEmoticon_Click(object sender, EventArgs e)
        {
            if (openCustomEmoticonDialog.ShowDialog() != DialogResult.OK)
                return;

            MemoryStream mem = new MemoryStream();
            Bitmap img = new Bitmap(Image.FromFile(openCustomEmoticonDialog.FileName));
            img.Save(mem, ImageFormat.Png);
            Emoticon emotest = new Emoticon(_messenger.Owner.Account, mem, Path.GetFileName(openCustomEmoticonDialog.FileName), Path.GetFileName(openCustomEmoticonDialog.FileName));
            MSNObjectCatalog.GetInstance().Add(emotest);
            List<Emoticon> emolist = new List<Emoticon>();
            emolist.Add(emotest);

            if (!richTextHistory.Emotions.ContainsKey(emotest.Shortcut))
            {
                richTextHistory.Emotions[emotest.Shortcut] = img;
            }

            try
            {
                remoteContact.SendEmoticonDefinitions(emolist, EmoticonType.StaticEmoticon);

                TextMessage emotxt = new TextMessage("Hey, this is a custom emoticon: " + emotest.Shortcut);
                remoteContact.SendMessage(emotxt);

                DisplaySystemMessage("You send a custom emoticon with text message: Hey, this is a custom emoticon: [" + emotest.Shortcut + "].");
            }
            catch (Exception)
            {
                DisplaySystemMessage("Remote contact not online, emoticon will not be sent.");
            }
        }

        private void bMessageBoldItalicUnderline_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTextFonts();
            inputTextBox.Select();
        }

        private void bMessageFontColor_Click(object sender, EventArgs e)
        {
            if (dlgColor.ShowDialog() == DialogResult.OK)
            {
                bMessageFontColor.Image = CreateImageFromColor(dlgColor.Color, bMessageFontColor.Image.Size);
                inputTextBox.ForeColor = dlgColor.Color;
            }
        }

        private void cbMessageFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Validate();
        }

        private void cbMessageFontName_Validated(object sender, EventArgs e)
        {
            cbMessageFontName.Tag = cbMessageFontName.Text;
            UpdateTextFonts();
            inputTextBox.Select();
        }

        private void cbMessageFontName_Validating(object sender, CancelEventArgs e)
        {
            if (cbMessageFontName.FindStringExact(cbMessageFontName.Text) == -1 && cbMessageFontName.Tag != null)
            {
                cbMessageFontName.Text = cbMessageFontName.Tag.ToString();
            }

        }

        private void cbMessageFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Validate();
        }

        private void cbMessageFontSize_Validated(object sender, EventArgs e)
        {
            cbMessageFontSize.Tag = cbMessageFontSize.Text;
            UpdateTextFonts();
            inputTextBox.Select();
        }

        private void cbMessageFontSize_Validating(object sender, CancelEventArgs e)
        {
            float fontSize = float.MinValue;
            float.TryParse(cbMessageFontSize.Text, out fontSize);

            if (fontSize < 8f || fontSize > 72f && cbMessageFontSize.Tag != null)
            {
                cbMessageFontSize.Text = cbMessageFontSize.Tag.ToString();
            }
        }



        public class ActivityInfo
        {
            public uint AppID;
            public string ActivityName;
            public string ActivityData;
        }

        private void btnActivityTest_Click(object sender, EventArgs e)
        {
            int x = btnActivityTest.Bounds.Right + 10 + btnActivityTest.Width;
            int y = btnActivityTest.Bounds.Bottom + 10 + btnActivityTest.Height + 20;

            activitiesMenuStrip1.Show(x, y);
            activitiesMenuStrip1.Focus();
        }

        private void activitiesMenu_Click(object sender, EventArgs args)
        {
            ActivityInfo ai = ((ToolStripItem)sender).Tag as ActivityInfo;

            P2PActivity p2pActivity =
                _messenger.StartActivity(remoteContact, ai.AppID, ai.ActivityName, ai.ActivityData);

            p2pActivity.P2PSession.Closed += new EventHandler<ContactEventArgs>(p2pActivity_Closed);
        }

        private void p2pActivity_Closed(object sender, ContactEventArgs e)
        {
            if (!richTextHistory.InvokeRequired)
            {
                DisplaySystemMessage("Activity session closed by " + e.Contact.Account);
            }
            else
            {
                richTextHistory.Invoke(new EventHandler<ContactEventArgs>(p2pActivity_Closed), new object[] { sender, e });
            }
        }

        private void btnMultiparty_Click(object sender, EventArgs e)
        {
            int x = Location.X + 10 + btnMultiparty.Width;
            int y = Location.Y + 10 + btnMultiparty.Height + 20;

            multipartyMenu.Items.Clear();
            foreach (Contact c in _messenger.ContactList.Forward)
            {
                if (c.Online && c.AppearOnline && c.SupportsMultiparty)
                {
                    multipartyMenu.Items.Add(c.Account, null, multipartyMenu_Click).ToolTipText = c.Account;
                }
            }

            multipartyMenu.Show(x, y);
            multipartyMenu.Focus();
        }

        private List<Contact> multipartyContacts = new List<Contact>();
        private void multipartyMenu_Click(object sender, EventArgs args)
        {
            ToolStripItem item = (ToolStripItem)sender;
            if (_messenger.ContactList.HasContact(item.ToolTipText, IMAddressInfoType.WindowsLive))
            {
                Contact c = _messenger.ContactList.GetContact(item.ToolTipText, IMAddressInfoType.WindowsLive);

                // Multipart already created, direct invite...
                if (remoteContact.ClientType == IMAddressInfoType.TemporaryGroup)
                {
                    _messenger.Nameserver.InviteContactToMultiparty(c, remoteContact);

                    multipartyContacts.Clear();
                }
                else
                {
                    // Not created yet

                    // Invite original contact
                    if (!multipartyContacts.Contains(remoteContact))
                        multipartyContacts.Add(remoteContact);

                    // Invite selected
                    if (!multipartyContacts.Contains(c))
                        multipartyContacts.Add(c);

                    _messenger.Nameserver.CreateMultiparty(multipartyContacts,
                        delegate(object s, MultipartyCreatedEventArgs e)
                        {
                            // This is Multiparty now
                            remoteContact = e.Group;
                        });
                }
            }
            else
            {
                DisplaySystemMessage("Cannot find PassportMember: " + item.ToolTipText);
            }
        }

        private void Nameserver_JoinedGroupChat(object sender, GroupChatParticipationEventArgs e)
        {
            if (e.Via == this.remoteContact)
            {
                DisplaySystemMessage(e.Contact + " joined group chat.Group has " +
                    e.Via.ContactList[IMAddressInfoType.None].Count + " members now.");
            }
        }

        private void Nameserver_LeftGroupChat(object sender, GroupChatParticipationEventArgs e)
        {
            if (e.Via == this.remoteContact)
            {
                DisplaySystemMessage(e.Contact + " left group chat. Group has " +
                    e.Via.ContactList[IMAddressInfoType.None].Count + " members now.");
            }

        }

        private void ConversationForm_Activated(object sender, EventArgs e)
        {
            Flash(true);
        }
    }
};
