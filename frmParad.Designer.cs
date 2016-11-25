namespace ProgTor.ParAd
{
    partial class frmParad
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmParad));
      this._ts = new System.Windows.Forms.ToolStrip();
      this._cmdInit = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this._cmdParser = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this._cmdExit = new System.Windows.Forms.ToolStripButton();
      this._ss = new System.Windows.Forms.StatusStrip();
      this._tlp = new System.Windows.Forms.TableLayoutPanel();
      this._sc = new System.Windows.Forms.SplitContainer();
      this._scResult = new System.Windows.Forms.SplitContainer();
      this._tc = new System.Windows.Forms.TabControl();
      this._tcpS1 = new System.Windows.Forms.TabPage();
      this._dgvTgtS1 = new System.Windows.Forms.DataGridView();
      this._tcpS2 = new System.Windows.Forms.TabPage();
      this._dgvTgtS2 = new System.Windows.Forms.DataGridView();
      this._tv = new System.Windows.Forms.TreeView();
      this._txtLogs = new System.Windows.Forms.TextBox();
      this._lblSrc = new System.Windows.Forms.Label();
      this._lblTgt = new System.Windows.Forms.Label();
      this._cboSrc = new System.Windows.Forms.ComboBox();
      this._ts.SuspendLayout();
      this._tlp.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this._sc)).BeginInit();
      this._sc.Panel1.SuspendLayout();
      this._sc.Panel2.SuspendLayout();
      this._sc.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this._scResult)).BeginInit();
      this._scResult.Panel1.SuspendLayout();
      this._scResult.Panel2.SuspendLayout();
      this._scResult.SuspendLayout();
      this._tc.SuspendLayout();
      this._tcpS1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this._dgvTgtS1)).BeginInit();
      this._tcpS2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this._dgvTgtS2)).BeginInit();
      this.SuspendLayout();
      // 
      // _ts
      // 
      this._ts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._cmdInit,
            this.toolStripSeparator3,
            this._cmdParser,
            this.toolStripSeparator2,
            this._cmdExit});
      this._ts.Location = new System.Drawing.Point(0, 0);
      this._ts.Name = "_ts";
      this._ts.Size = new System.Drawing.Size(562, 25);
      this._ts.TabIndex = 0;
      this._ts.Text = "toolStrip1";
      // 
      // _cmdInit
      // 
      this._cmdInit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this._cmdInit.Image = ((System.Drawing.Image)(resources.GetObject("_cmdInit.Image")));
      this._cmdInit.ImageTransparentColor = System.Drawing.Color.Magenta;
      this._cmdInit.Name = "_cmdInit";
      this._cmdInit.Size = new System.Drawing.Size(23, 22);
      this._cmdInit.Text = "Init";
      this._cmdInit.Click += new System.EventHandler(this._cmdInit_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
      // 
      // _cmdParser
      // 
      this._cmdParser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this._cmdParser.Image = global::ProgTor.ParAd.Properties.Resources.navigate_down;
      this._cmdParser.ImageTransparentColor = System.Drawing.Color.Magenta;
      this._cmdParser.Name = "_cmdParser";
      this._cmdParser.Size = new System.Drawing.Size(23, 22);
      this._cmdParser.Text = "Parser";
      this._cmdParser.Click += new System.EventHandler(this._cmdParser_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // _cmdExit
      // 
      this._cmdExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this._cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("_cmdExit.Image")));
      this._cmdExit.ImageTransparentColor = System.Drawing.Color.Magenta;
      this._cmdExit.Name = "_cmdExit";
      this._cmdExit.Size = new System.Drawing.Size(23, 22);
      this._cmdExit.Text = "toolStripButton1";
      this._cmdExit.Click += new System.EventHandler(this._cmdExit_Click);
      // 
      // _ss
      // 
      this._ss.Location = new System.Drawing.Point(0, 258);
      this._ss.Name = "_ss";
      this._ss.Size = new System.Drawing.Size(562, 22);
      this._ss.TabIndex = 1;
      this._ss.Text = "statusStrip1";
      // 
      // _tlp
      // 
      this._tlp.ColumnCount = 2;
      this._tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
      this._tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this._tlp.Controls.Add(this._sc, 0, 2);
      this._tlp.Controls.Add(this._lblSrc, 0, 0);
      this._tlp.Controls.Add(this._lblTgt, 0, 1);
      this._tlp.Controls.Add(this._cboSrc, 1, 0);
      this._tlp.Dock = System.Windows.Forms.DockStyle.Fill;
      this._tlp.Location = new System.Drawing.Point(0, 25);
      this._tlp.Name = "_tlp";
      this._tlp.RowCount = 3;
      this._tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this._tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this._tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this._tlp.Size = new System.Drawing.Size(562, 233);
      this._tlp.TabIndex = 2;
      // 
      // _sc
      // 
      this._tlp.SetColumnSpan(this._sc, 2);
      this._sc.Dock = System.Windows.Forms.DockStyle.Fill;
      this._sc.Location = new System.Drawing.Point(3, 53);
      this._sc.Name = "_sc";
      this._sc.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // _sc.Panel1
      // 
      this._sc.Panel1.Controls.Add(this._scResult);
      // 
      // _sc.Panel2
      // 
      this._sc.Panel2.Controls.Add(this._txtLogs);
      this._sc.Size = new System.Drawing.Size(556, 177);
      this._sc.SplitterDistance = 97;
      this._sc.TabIndex = 0;
      // 
      // _scResult
      // 
      this._scResult.Dock = System.Windows.Forms.DockStyle.Fill;
      this._scResult.Location = new System.Drawing.Point(0, 0);
      this._scResult.Name = "_scResult";
      // 
      // _scResult.Panel1
      // 
      this._scResult.Panel1.Controls.Add(this._tc);
      // 
      // _scResult.Panel2
      // 
      this._scResult.Panel2.Controls.Add(this._tv);
      this._scResult.Size = new System.Drawing.Size(556, 97);
      this._scResult.SplitterDistance = 185;
      this._scResult.TabIndex = 1;
      // 
      // _tc
      // 
      this._tc.Controls.Add(this._tcpS1);
      this._tc.Controls.Add(this._tcpS2);
      this._tc.Dock = System.Windows.Forms.DockStyle.Fill;
      this._tc.Location = new System.Drawing.Point(0, 0);
      this._tc.Name = "_tc";
      this._tc.SelectedIndex = 0;
      this._tc.Size = new System.Drawing.Size(185, 97);
      this._tc.TabIndex = 1;
      // 
      // _tcpS1
      // 
      this._tcpS1.Controls.Add(this._dgvTgtS1);
      this._tcpS1.Location = new System.Drawing.Point(4, 22);
      this._tcpS1.Name = "_tcpS1";
      this._tcpS1.Padding = new System.Windows.Forms.Padding(3);
      this._tcpS1.Size = new System.Drawing.Size(177, 71);
      this._tcpS1.TabIndex = 0;
      this._tcpS1.Text = "Step 1";
      this._tcpS1.UseVisualStyleBackColor = true;
      // 
      // _dgvTgtS1
      // 
      this._dgvTgtS1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this._dgvTgtS1.Dock = System.Windows.Forms.DockStyle.Fill;
      this._dgvTgtS1.Location = new System.Drawing.Point(3, 3);
      this._dgvTgtS1.Name = "_dgvTgtS1";
      this._dgvTgtS1.Size = new System.Drawing.Size(171, 65);
      this._dgvTgtS1.TabIndex = 0;
      // 
      // _tcpS2
      // 
      this._tcpS2.Controls.Add(this._dgvTgtS2);
      this._tcpS2.Location = new System.Drawing.Point(4, 22);
      this._tcpS2.Name = "_tcpS2";
      this._tcpS2.Padding = new System.Windows.Forms.Padding(3);
      this._tcpS2.Size = new System.Drawing.Size(177, 71);
      this._tcpS2.TabIndex = 1;
      this._tcpS2.Text = "Step 2";
      this._tcpS2.UseVisualStyleBackColor = true;
      // 
      // _dgvTgtS2
      // 
      this._dgvTgtS2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this._dgvTgtS2.Dock = System.Windows.Forms.DockStyle.Fill;
      this._dgvTgtS2.Location = new System.Drawing.Point(3, 3);
      this._dgvTgtS2.Name = "_dgvTgtS2";
      this._dgvTgtS2.Size = new System.Drawing.Size(171, 65);
      this._dgvTgtS2.TabIndex = 0;
      // 
      // _tv
      // 
      this._tv.Dock = System.Windows.Forms.DockStyle.Fill;
      this._tv.Location = new System.Drawing.Point(0, 0);
      this._tv.Name = "_tv";
      this._tv.Size = new System.Drawing.Size(367, 97);
      this._tv.TabIndex = 0;
      // 
      // _txtLogs
      // 
      this._txtLogs.Dock = System.Windows.Forms.DockStyle.Fill;
      this._txtLogs.Location = new System.Drawing.Point(0, 0);
      this._txtLogs.Multiline = true;
      this._txtLogs.Name = "_txtLogs";
      this._txtLogs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this._txtLogs.Size = new System.Drawing.Size(556, 76);
      this._txtLogs.TabIndex = 0;
      // 
      // _lblSrc
      // 
      this._lblSrc.Anchor = System.Windows.Forms.AnchorStyles.Right;
      this._lblSrc.AutoSize = true;
      this._lblSrc.Location = new System.Drawing.Point(158, 8);
      this._lblSrc.Name = "_lblSrc";
      this._lblSrc.Size = new System.Drawing.Size(39, 13);
      this._lblSrc.TabIndex = 1;
      this._lblSrc.Text = "_lblSrc";
      // 
      // _lblTgt
      // 
      this._lblTgt.AutoSize = true;
      this._tlp.SetColumnSpan(this._lblTgt, 2);
      this._lblTgt.Location = new System.Drawing.Point(3, 30);
      this._lblTgt.Name = "_lblTgt";
      this._lblTgt.Size = new System.Drawing.Size(39, 13);
      this._lblTgt.TabIndex = 2;
      this._lblTgt.Text = "_lblTgt";
      // 
      // _cboSrc
      // 
      this._cboSrc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this._cboSrc.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this._cboSrc.FormattingEnabled = true;
      this._cboSrc.Location = new System.Drawing.Point(203, 3);
      this._cboSrc.Name = "_cboSrc";
      this._cboSrc.Size = new System.Drawing.Size(356, 28);
      this._cboSrc.TabIndex = 3;
      // 
      // frmParad
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(562, 280);
      this.Controls.Add(this._tlp);
      this.Controls.Add(this._ss);
      this.Controls.Add(this._ts);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmParad";
      this.Text = "ParAd";
      this._ts.ResumeLayout(false);
      this._ts.PerformLayout();
      this._tlp.ResumeLayout(false);
      this._tlp.PerformLayout();
      this._sc.Panel1.ResumeLayout(false);
      this._sc.Panel2.ResumeLayout(false);
      this._sc.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this._sc)).EndInit();
      this._sc.ResumeLayout(false);
      this._scResult.Panel1.ResumeLayout(false);
      this._scResult.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this._scResult)).EndInit();
      this._scResult.ResumeLayout(false);
      this._tc.ResumeLayout(false);
      this._tcpS1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this._dgvTgtS1)).EndInit();
      this._tcpS2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this._dgvTgtS2)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _ts;
        private System.Windows.Forms.ToolStripButton _cmdParser;
        private System.Windows.Forms.StatusStrip _ss;
        private System.Windows.Forms.TableLayoutPanel _tlp;
        private System.Windows.Forms.SplitContainer _sc;
        private System.Windows.Forms.DataGridView _dgvTgtS1;
        private System.Windows.Forms.TextBox _txtLogs;
        private System.Windows.Forms.Label _lblSrc;
        private System.Windows.Forms.Label _lblTgt;
        private System.Windows.Forms.ToolStripButton _cmdInit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton _cmdExit;
        private System.Windows.Forms.SplitContainer _scResult;
        private System.Windows.Forms.TreeView _tv;
        private System.Windows.Forms.TabControl _tc;
        private System.Windows.Forms.TabPage _tcpS1;
        private System.Windows.Forms.TabPage _tcpS2;
        private System.Windows.Forms.DataGridView _dgvTgtS2;
        private System.Windows.Forms.ComboBox _cboSrc;
    }
}

