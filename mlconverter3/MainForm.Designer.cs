namespace mlconverter3
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gameNameTbx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openRomBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.addressLbl = new System.Windows.Forms.Label();
            this.SequenceEditBtn = new System.Windows.Forms.Button();
            this.exportAllMidiBtn = new System.Windows.Forms.Button();
            this.exportMidiBtn = new System.Windows.Forms.Button();
            this.sequenceLbx = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.exportSfBtn = new System.Windows.Forms.Button();
            this.exportSampleBtn = new System.Windows.Forms.Button();
            this.sfEditBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gameNameTbx);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.openRomBtn);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 102);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ROM";
            // 
            // gameNameTbx
            // 
            this.gameNameTbx.Location = new System.Drawing.Point(48, 72);
            this.gameNameTbx.Name = "gameNameTbx";
            this.gameNameTbx.ReadOnly = true;
            this.gameNameTbx.Size = new System.Drawing.Size(205, 20);
            this.gameNameTbx.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Game";
            // 
            // openRomBtn
            // 
            this.openRomBtn.Location = new System.Drawing.Point(7, 20);
            this.openRomBtn.Name = "openRomBtn";
            this.openRomBtn.Size = new System.Drawing.Size(246, 46);
            this.openRomBtn.TabIndex = 0;
            this.openRomBtn.Text = "Open ROM";
            this.openRomBtn.UseVisualStyleBackColor = true;
            this.openRomBtn.Click += new System.EventHandler(this.openRomBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.addressLbl);
            this.groupBox2.Controls.Add(this.SequenceEditBtn);
            this.groupBox2.Controls.Add(this.exportAllMidiBtn);
            this.groupBox2.Controls.Add(this.exportMidiBtn);
            this.groupBox2.Controls.Add(this.sequenceLbx);
            this.groupBox2.Location = new System.Drawing.Point(12, 121);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(259, 161);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sequence";
            // 
            // addressLbl
            // 
            this.addressLbl.AutoSize = true;
            this.addressLbl.Location = new System.Drawing.Point(133, 25);
            this.addressLbl.Name = "addressLbl";
            this.addressLbl.Size = new System.Drawing.Size(107, 13);
            this.addressLbl.TabIndex = 4;
            this.addressLbl.Text = "Address 0x08000000";
            // 
            // SequenceEditBtn
            // 
            this.SequenceEditBtn.Location = new System.Drawing.Point(132, 45);
            this.SequenceEditBtn.Name = "SequenceEditBtn";
            this.SequenceEditBtn.Size = new System.Drawing.Size(121, 30);
            this.SequenceEditBtn.TabIndex = 3;
            this.SequenceEditBtn.Text = "Edit";
            this.SequenceEditBtn.UseVisualStyleBackColor = true;
            this.SequenceEditBtn.Click += new System.EventHandler(this.SequenceEditBtn_Click);
            // 
            // exportAllMidiBtn
            // 
            this.exportAllMidiBtn.Location = new System.Drawing.Point(132, 117);
            this.exportAllMidiBtn.Name = "exportAllMidiBtn";
            this.exportAllMidiBtn.Size = new System.Drawing.Size(121, 30);
            this.exportAllMidiBtn.TabIndex = 2;
            this.exportAllMidiBtn.Text = "Export all to MIDI";
            this.exportAllMidiBtn.UseVisualStyleBackColor = true;
            this.exportAllMidiBtn.Click += new System.EventHandler(this.exportAllMidiBtn_Click);
            // 
            // exportMidiBtn
            // 
            this.exportMidiBtn.Location = new System.Drawing.Point(132, 81);
            this.exportMidiBtn.Name = "exportMidiBtn";
            this.exportMidiBtn.Size = new System.Drawing.Size(121, 30);
            this.exportMidiBtn.TabIndex = 1;
            this.exportMidiBtn.Text = "Export to MIDI";
            this.exportMidiBtn.UseVisualStyleBackColor = true;
            this.exportMidiBtn.Click += new System.EventHandler(this.exportMidiBtn_Click);
            // 
            // sequenceLbx
            // 
            this.sequenceLbx.FormattingEnabled = true;
            this.sequenceLbx.Location = new System.Drawing.Point(7, 18);
            this.sequenceLbx.Name = "sequenceLbx";
            this.sequenceLbx.Size = new System.Drawing.Size(120, 134);
            this.sequenceLbx.TabIndex = 0;
            this.sequenceLbx.SelectedIndexChanged += new System.EventHandler(this.sequenceLbx_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.exportSfBtn);
            this.groupBox3.Controls.Add(this.exportSampleBtn);
            this.groupBox3.Controls.Add(this.sfEditBtn);
            this.groupBox3.Location = new System.Drawing.Point(12, 288);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(259, 177);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Soundfont";
            // 
            // exportSfBtn
            // 
            this.exportSfBtn.Location = new System.Drawing.Point(6, 124);
            this.exportSfBtn.Name = "exportSfBtn";
            this.exportSfBtn.Size = new System.Drawing.Size(248, 46);
            this.exportSfBtn.TabIndex = 2;
            this.exportSfBtn.Text = "Export soundfont";
            this.exportSfBtn.UseVisualStyleBackColor = true;
            this.exportSfBtn.Click += new System.EventHandler(this.exportSfBtn_Click);
            // 
            // exportSampleBtn
            // 
            this.exportSampleBtn.Location = new System.Drawing.Point(6, 72);
            this.exportSampleBtn.Name = "exportSampleBtn";
            this.exportSampleBtn.Size = new System.Drawing.Size(248, 46);
            this.exportSampleBtn.TabIndex = 1;
            this.exportSampleBtn.Text = "Export all samples";
            this.exportSampleBtn.UseVisualStyleBackColor = true;
            this.exportSampleBtn.Click += new System.EventHandler(this.exportSampleBtn_Click);
            // 
            // sfEditBtn
            // 
            this.sfEditBtn.Location = new System.Drawing.Point(6, 20);
            this.sfEditBtn.Name = "sfEditBtn";
            this.sfEditBtn.Size = new System.Drawing.Size(247, 46);
            this.sfEditBtn.TabIndex = 0;
            this.sfEditBtn.Text = "Edit";
            this.sfEditBtn.UseVisualStyleBackColor = true;
            this.sfEditBtn.Click += new System.EventHandler(this.sfEditBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 472);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MainForm";
            this.Text = "GBA sequence tool";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button openRomBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox gameNameTbx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button exportMidiBtn;
        private System.Windows.Forms.ListBox sequenceLbx;
        private System.Windows.Forms.Button exportAllMidiBtn;
        private System.Windows.Forms.Label addressLbl;
        private System.Windows.Forms.Button SequenceEditBtn;
        private System.Windows.Forms.Button exportSfBtn;
        private System.Windows.Forms.Button exportSampleBtn;
        private System.Windows.Forms.Button sfEditBtn;
    }
}

