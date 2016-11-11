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
      this._cmdParser = new System.Windows.Forms.ToolStripButton();
      this._ss = new System.Windows.Forms.StatusStrip();
      this._tlp = new System.Windows.Forms.TableLayoutPanel();
      this._sc = new System.Windows.Forms.SplitContainer();
      this._dgvTgt = new System.Windows.Forms.DataGridView();
      this._txtLogs = new System.Windows.Forms.TextBox();
      this._lblSrc = new System.Windows.Forms.Label();
      this._lblTgt = new System.Windows.Forms.Label();
      this._txtSrc = new System.Windows.Forms.TextBox();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this._ts.SuspendLayout();
      this._tlp.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this._sc)).BeginInit();
      this._sc.Panel1.SuspendLayout();
      this._sc.Panel2.SuspendLayout();
      this._sc.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this._dgvTgt)).BeginInit();
      this.SuspendLayout();
      // 
      // _ts
      // 
      this._ts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._cmdParser,
            this.toolStripSeparator1,
            this.toolStripButton1});
      this._ts.Location = new System.Drawing.Point(0, 0);
      this._ts.Name = "_ts";
      this._ts.Size = new System.Drawing.Size(562, 25);
      this._ts.TabIndex = 0;
      this._ts.Text = "toolStrip1";
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
      this._tlp.Controls.Add(this._txtSrc, 1, 0);
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
      this._sc.Panel1.Controls.Add(this._dgvTgt);
      // 
      // _sc.Panel2
      // 
      this._sc.Panel2.Controls.Add(this._txtLogs);
      this._sc.Size = new System.Drawing.Size(556, 177);
      this._sc.SplitterDistance = 97;
      this._sc.TabIndex = 0;
      // 
      // _dgvTgt
      // 
      this._dgvTgt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this._dgvTgt.Dock = System.Windows.Forms.DockStyle.Fill;
      this._dgvTgt.Location = new System.Drawing.Point(0, 0);
      this._dgvTgt.Name = "_dgvTgt";
      this._dgvTgt.Size = new System.Drawing.Size(556, 97);
      this._dgvTgt.TabIndex = 0;
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
      // _txtSrc
      // 
      this._txtSrc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this._txtSrc.Location = new System.Drawing.Point(203, 5);
      this._txtSrc.Name = "_txtSrc";
      this._txtSrc.Size = new System.Drawing.Size(356, 20);
      this._txtSrc.TabIndex = 3;
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton1.Text = "Exit";
      this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
      this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
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
      ((System.ComponentModel.ISupportInitialize)(this._dgvTgt)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _ts;
        private System.Windows.Forms.ToolStripButton _cmdParser;
        private System.Windows.Forms.StatusStrip _ss;
        private System.Windows.Forms.TableLayoutPanel _tlp;
        private System.Windows.Forms.SplitContainer _sc;
        private System.Windows.Forms.DataGridView _dgvTgt;
        private System.Windows.Forms.TextBox _txtLogs;
        private System.Windows.Forms.Label _lblSrc;
        private System.Windows.Forms.Label _lblTgt;
        private System.Windows.Forms.TextBox _txtSrc;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}

