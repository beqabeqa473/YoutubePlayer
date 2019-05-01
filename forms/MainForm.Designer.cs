using YoutubePlayer.Classes;

namespace YoutubePlayer
{
    partial class MainForm
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
            this.MainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lbSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lbResults = new System.Windows.Forms.Label();
            this.lstResults = new System.Windows.Forms.ListBox();
            this.cmResults = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmOpenInBrowser = new System.Windows.Forms.ToolStripMenuItem();
            this.cmCopyToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.cmDownloadMp3 = new System.Windows.Forms.ToolStripMenuItem();
            this.lbMain = new System.Windows.Forms.Label();
            this.lbVolume = new System.Windows.Forms.Label();
            this.lbSeek = new System.Windows.Forms.Label();
            this.lbDevice = new System.Windows.Forms.Label();
            this.cbDevice = new System.Windows.Forms.ComboBox();
            this.mbMain = new System.Windows.Forms.MenuStrip();
            this.mbFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mbReportIssue = new System.Windows.Forms.ToolStripMenuItem();
            this.mbExit = new System.Windows.Forms.ToolStripMenuItem();
            this.lbDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.tmSeek = new System.Windows.Forms.Timer(this.components);
            this.sbStatus = new System.Windows.Forms.StatusStrip();
            this.tbVolume = new YoutubePlayer.Classes.MyTrackBar();
            this.tbSeek = new YoutubePlayer.Classes.MyTrackBar();
            this.MainPanel.SuspendLayout();
            this.cmResults.SuspendLayout();
            this.mbMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSeek)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.ColumnCount = 3;
            this.MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.MainPanel.Controls.Add(this.lbSearch, 0, 2);
            this.MainPanel.Controls.Add(this.txtSearch, 0, 3);
            this.MainPanel.Controls.Add(this.lbResults, 1, 2);
            this.MainPanel.Controls.Add(this.lstResults, 1, 3);
            this.MainPanel.Controls.Add(this.lbMain, 0, 0);
            this.MainPanel.Controls.Add(this.lbVolume, 0, 4);
            this.MainPanel.Controls.Add(this.tbVolume, 0, 5);
            this.MainPanel.Controls.Add(this.lbSeek, 1, 4);
            this.MainPanel.Controls.Add(this.tbSeek, 1, 5);
            this.MainPanel.Controls.Add(this.lbDevice, 2, 4);
            this.MainPanel.Controls.Add(this.sbStatus, 0, 6);
            this.MainPanel.Controls.Add(this.cbDevice, 2, 5);
            this.MainPanel.Controls.Add(this.mbMain, 0, 1);
            this.MainPanel.Controls.Add(this.lbDescription, 2, 2);
            this.MainPanel.Controls.Add(this.txtDescription, 2, 3);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.RowCount = 7;
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.MainPanel.Size = new System.Drawing.Size(800, 450);
            this.MainPanel.TabIndex = 0;
            // 
            // lbSearch
            // 
            this.lbSearch.AutoSize = true;
            this.lbSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSearch.Location = new System.Drawing.Point(3, 148);
            this.lbSearch.Name = "lbSearch";
            this.lbSearch.Size = new System.Drawing.Size(274, 58);
            this.lbSearch.TabIndex = 1;
            this.lbSearch.Text = "Поиск";
            // 
            // txtSearch
            // 
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.Location = new System.Drawing.Point(3, 209);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(274, 20);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDownAsync);
            // 
            // lbResults
            // 
            this.lbResults.AutoSize = true;
            this.lbResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbResults.Location = new System.Drawing.Point(283, 148);
            this.lbResults.Name = "lbResults";
            this.lbResults.Size = new System.Drawing.Size(234, 58);
            this.lbResults.TabIndex = 3;
            this.lbResults.Text = "Результаты поиска";
            // 
            // lstResults
            // 
            this.lstResults.ContextMenuStrip = this.cmResults;
            this.lstResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstResults.FormattingEnabled = true;
            this.lstResults.Location = new System.Drawing.Point(283, 209);
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size(234, 52);
            this.lstResults.TabIndex = 4;
            this.lstResults.SelectedIndexChanged += new System.EventHandler(this.lstResults_SelectedIndexChangedAsync);
            this.lstResults.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstResults_KeyDownAsync);
            // 
            // cmResults
            // 
            this.cmResults.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmOpenInBrowser,
            this.cmCopyToClipboard,
            this.cmDownloadMp3});
            this.cmResults.Name = "cmResults";
            this.cmResults.Size = new System.Drawing.Size(275, 70);
            // 
            // cmOpenInBrowser
            // 
            this.cmOpenInBrowser.Name = "cmOpenInBrowser";
            this.cmOpenInBrowser.Size = new System.Drawing.Size(274, 22);
            this.cmOpenInBrowser.Text = "Открыть в браузере";
            this.cmOpenInBrowser.Click += new System.EventHandler(this.cmOpenInBrowser_Click);
            // 
            // cmCopyToClipboard
            // 
            this.cmCopyToClipboard.Name = "cmCopyToClipboard";
            this.cmCopyToClipboard.Size = new System.Drawing.Size(274, 22);
            this.cmCopyToClipboard.Text = "Копировать ссылку в буфер обмена";
            this.cmCopyToClipboard.Click += new System.EventHandler(this.cmCopyToClipboard_Click);
            // 
            // cmDownloadMp3
            // 
            this.cmDownloadMp3.Name = "cmDownloadMp3";
            this.cmDownloadMp3.Size = new System.Drawing.Size(274, 22);
            this.cmDownloadMp3.Text = "Скачать в mp3";
            this.cmDownloadMp3.Click += new System.EventHandler(this.cmDownloadMp3_ClickAsync);
            // 
            // lbMain
            // 
            this.lbMain.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
            this.lbMain.AutoSize = true;
            this.MainPanel.SetColumnSpan(this.lbMain, 3);
            this.lbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbMain.Location = new System.Drawing.Point(3, 0);
            this.lbMain.Name = "lbMain";
            this.lbMain.Size = new System.Drawing.Size(794, 90);
            this.lbMain.TabIndex = 0;
            this.lbMain.Text = "Аудио Проигрыватель Видео записей Youtube";
            // 
            // lbVolume
            // 
            this.lbVolume.AutoSize = true;
            this.lbVolume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbVolume.Location = new System.Drawing.Point(3, 264);
            this.lbVolume.Name = "lbVolume";
            this.lbVolume.Size = new System.Drawing.Size(274, 58);
            this.lbVolume.TabIndex = 7;
            this.lbVolume.Text = "Громкость";
            // 
            // lbSeek
            // 
            this.lbSeek.AutoSize = true;
            this.lbSeek.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSeek.Location = new System.Drawing.Point(283, 264);
            this.lbSeek.Name = "lbSeek";
            this.lbSeek.Size = new System.Drawing.Size(234, 58);
            this.lbSeek.TabIndex = 9;
            this.lbSeek.Text = "Перемотка";
            // 
            // lbDevice
            // 
            this.lbDevice.AutoSize = true;
            this.lbDevice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDevice.Location = new System.Drawing.Point(523, 264);
            this.lbDevice.Name = "lbDevice";
            this.lbDevice.Size = new System.Drawing.Size(274, 58);
            this.lbDevice.TabIndex = 11;
            this.lbDevice.Text = "Устройство вывода";
            // 
            // cbDevice
            // 
            this.cbDevice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDevice.FormattingEnabled = true;
            this.cbDevice.Location = new System.Drawing.Point(523, 325);
            this.cbDevice.Name = "cbDevice";
            this.cbDevice.Size = new System.Drawing.Size(274, 21);
            this.cbDevice.TabIndex = 12;
            this.cbDevice.SelectedIndexChanged += new System.EventHandler(this.cbDevice_SelectedIndexChanged);
            // 
            // mbMain
            // 
            this.mbMain.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.MainPanel.SetColumnSpan(this.mbMain, 3);
            this.mbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mbFile});
            this.mbMain.Location = new System.Drawing.Point(0, 90);
            this.mbMain.Name = "mbMain";
            this.mbMain.Size = new System.Drawing.Size(800, 24);
            this.mbMain.TabIndex = 13;
            this.mbMain.Text = "Меню";
            // 
            // mbFile
            // 
            this.mbFile.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuPopup;
            this.mbFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mbReportIssue,
            this.mbExit});
            this.mbFile.Name = "mbFile";
            this.mbFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.mbFile.Size = new System.Drawing.Size(48, 20);
            this.mbFile.Text = "Файл";
            // 
            // mbReportIssue
            // 
            this.mbReportIssue.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuItem;
            this.mbReportIssue.Name = "mbReportIssue";
            this.mbReportIssue.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));
            this.mbReportIssue.Size = new System.Drawing.Size(359, 22);
            this.mbReportIssue.Text = "Сообщить об ошибке или предложить идею";
            this.mbReportIssue.Click += new System.EventHandler(this.mbReportIssue_Click);
            // 
            // mbExit
            // 
            this.mbExit.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuItem;
            this.mbExit.Name = "mbExit";
            this.mbExit.Size = new System.Drawing.Size(359, 22);
            this.mbExit.Text = "Выход";
            this.mbExit.Click += new System.EventHandler(this.mbExit_Click);
            // 
            // lbDescription
            // 
            this.lbDescription.AutoSize = true;
            this.lbDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDescription.Location = new System.Drawing.Point(523, 148);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(274, 58);
            this.lbDescription.TabIndex = 4;
            this.lbDescription.Text = "Описание";
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Location = new System.Drawing.Point(523, 209);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(274, 52);
            this.txtDescription.TabIndex = 5;
            // 
            // tmSeek
            // 
            this.tmSeek.Interval = 1000;
            this.tmSeek.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // sbStatus
            // 
            this.MainPanel.SetColumnSpan(this.sbStatus, 3);
            this.sbStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sbStatus.Location = new System.Drawing.Point(0, 380);
            this.sbStatus.Name = "sbStatus";
            this.sbStatus.Size = new System.Drawing.Size(800, 70);
            this.sbStatus.TabIndex = 14;
            this.sbStatus.Text = "Строка состояния";
            // 
            // tbVolume
            // 
            this.tbVolume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbVolume.LargeChange = 20;
            this.tbVolume.Location = new System.Drawing.Point(3, 325);
            this.tbVolume.Maximum = 100;
            this.tbVolume.Name = "tbVolume";
            this.tbVolume.Size = new System.Drawing.Size(274, 52);
            this.tbVolume.TabIndex = 8;
            this.tbVolume.Value = 50;
            this.tbVolume.Scroll += new System.EventHandler(this.tbVolume_Scroll);
            this.tbVolume.ValueChanged += new System.EventHandler(this.tbVolume_ValueChanged);
            // 
            // tbSeek
            // 
            this.tbSeek.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSeek.Location = new System.Drawing.Point(283, 325);
            this.tbSeek.Maximum = 100;
            this.tbSeek.Name = "tbSeek";
            this.tbSeek.Size = new System.Drawing.Size(234, 52);
            this.tbSeek.TabIndex = 10;
            this.tbSeek.Scroll += new System.EventHandler(this.tbSeek_Scroll);
            // 
            // MainForm
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Dialog;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MainPanel);
            this.KeyPreview = true;
            this.MainMenuStrip = this.mbMain;
            this.Name = "MainForm";
            this.Text = "Youtube audio player";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.cmResults.ResumeLayout(false);
            this.mbMain.ResumeLayout(false);
            this.mbMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSeek)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainPanel;
        private System.Windows.Forms.Label lbSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lbResults;
        private System.Windows.Forms.ListBox lstResults;
        private System.Windows.Forms.Label lbMain;
        private System.Windows.Forms.Label lbVolume;
        private System.Windows.Forms.Label lbSeek;
        private System.Windows.Forms.Label lbDevice;
        private System.Windows.Forms.ComboBox cbDevice;
        private System.Windows.Forms.Timer tmSeek;
        private System.Windows.Forms.MenuStrip mbMain;
        private System.Windows.Forms.ToolStripMenuItem mbFile;
        private System.Windows.Forms.ToolStripMenuItem mbReportIssue;
        private System.Windows.Forms.ToolStripMenuItem mbExit;
        private System.Windows.Forms.ContextMenuStrip cmResults;
        private System.Windows.Forms.ToolStripMenuItem cmOpenInBrowser;
        private System.Windows.Forms.ToolStripMenuItem cmCopyToClipboard;
        private System.Windows.Forms.ToolStripMenuItem cmDownloadMp3;
        private System.Windows.Forms.Label lbDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private MyTrackBar tbVolume;
        private MyTrackBar tbSeek;
        private System.Windows.Forms.StatusStrip sbStatus;
    }
}