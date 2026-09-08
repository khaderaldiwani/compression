namespace compression
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // أدوات الفورم
        private System.Windows.Forms.Button addFilesButton;
        private System.Windows.Forms.Button addFolderButton;
        private System.Windows.Forms.Button compressButton;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.Button resumeButton;
        private System.Windows.Forms.Button deCompressButton;

        private System.Windows.Forms.ListBox listBoxObject;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Label status;

        /// <summary>
        /// تنظيف الموارد المستخدمة.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region كود مُولّد بواسطة مصمم النماذج

        private void InitializeComponent()
        {
            this.addFilesButton = new System.Windows.Forms.Button();
            this.addFolderButton = new System.Windows.Forms.Button();
            this.compressButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.resumeButton = new System.Windows.Forms.Button();
            this.deCompressButton = new System.Windows.Forms.Button();
            this.listBoxObject = new System.Windows.Forms.ListBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.status = new System.Windows.Forms.Label();
            this.deCompressSelectButton = new System.Windows.Forms.Button();
            this.contextMenuButtonAdd = new System.Windows.Forms.Button();
            this.algorithmSelector = new System.Windows.Forms.ComboBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.contextMenuButtonRemove = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fastRadio = new System.Windows.Forms.RadioButton();
            this.normalRadio = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addFilesButton
            // 
            this.addFilesButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(169)))), ((int)(((byte)(100)))));
            this.addFilesButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.addFilesButton.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.addFilesButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(51)))), ((int)(((byte)(46)))));
            this.addFilesButton.Location = new System.Drawing.Point(558, 15);
            this.addFilesButton.Name = "addFilesButton";
            this.addFilesButton.Size = new System.Drawing.Size(89, 27);
            this.addFilesButton.TabIndex = 0;
            this.addFilesButton.Text = "إضافة ملفات";
            this.addFilesButton.UseVisualStyleBackColor = false;
            this.addFilesButton.Click += new System.EventHandler(this.addFilesButton_Click);
            // 
            // addFolderButton
            // 
            this.addFolderButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(169)))), ((int)(((byte)(100)))));
            this.addFolderButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.addFolderButton.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.addFolderButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(51)))), ((int)(((byte)(46)))));
            this.addFolderButton.Location = new System.Drawing.Point(674, 15);
            this.addFolderButton.Name = "addFolderButton";
            this.addFolderButton.Size = new System.Drawing.Size(87, 27);
            this.addFolderButton.TabIndex = 1;
            this.addFolderButton.Text = "إضافة مجلد";
            this.addFolderButton.UseVisualStyleBackColor = false;
            this.addFolderButton.Click += new System.EventHandler(this.addFolderButton_Click);
            // 
            // compressButton
            // 
            this.compressButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(169)))), ((int)(((byte)(100)))));
            this.compressButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.compressButton.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.compressButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(51)))), ((int)(((byte)(46)))));
            this.compressButton.Location = new System.Drawing.Point(602, 224);
            this.compressButton.Name = "compressButton";
            this.compressButton.Size = new System.Drawing.Size(120, 27);
            this.compressButton.TabIndex = 2;
            this.compressButton.Text = "ضغط الملفات";
            this.compressButton.UseVisualStyleBackColor = false;
            this.compressButton.Click += new System.EventHandler(this.compressButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(169)))), ((int)(((byte)(100)))));
            this.pauseButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.pauseButton.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.pauseButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(51)))), ((int)(((byte)(46)))));
            this.pauseButton.Location = new System.Drawing.Point(118, 342);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(81, 26);
            this.pauseButton.TabIndex = 3;
            this.pauseButton.Text = "إيقاف مؤقت";
            this.pauseButton.UseVisualStyleBackColor = false;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // resumeButton
            // 
            this.resumeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(169)))), ((int)(((byte)(100)))));
            this.resumeButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.resumeButton.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.resumeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(51)))), ((int)(((byte)(46)))));
            this.resumeButton.Location = new System.Drawing.Point(220, 345);
            this.resumeButton.Name = "resumeButton";
            this.resumeButton.Size = new System.Drawing.Size(91, 23);
            this.resumeButton.TabIndex = 4;
            this.resumeButton.Text = "استئناف";
            this.resumeButton.UseVisualStyleBackColor = false;
            this.resumeButton.Click += new System.EventHandler(this.resumeButton_Click);
            // 
            // deCompressButton
            // 
            this.deCompressButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(169)))), ((int)(((byte)(100)))));
            this.deCompressButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.deCompressButton.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.deCompressButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(51)))), ((int)(((byte)(46)))));
            this.deCompressButton.Location = new System.Drawing.Point(663, 271);
            this.deCompressButton.Name = "deCompressButton";
            this.deCompressButton.Size = new System.Drawing.Size(97, 32);
            this.deCompressButton.TabIndex = 5;
            this.deCompressButton.Text = "فك الضغط";
            this.deCompressButton.UseVisualStyleBackColor = false;
            this.deCompressButton.Click += new System.EventHandler(this.deCompressButton_Click);
            // 
            // listBoxObject
            // 
            this.listBoxObject.BackColor = System.Drawing.Color.Khaki;
            this.listBoxObject.FormattingEnabled = true;
            this.listBoxObject.Location = new System.Drawing.Point(12, 45);
            this.listBoxObject.Name = "listBoxObject";
            this.listBoxObject.Size = new System.Drawing.Size(534, 225);
            this.listBoxObject.TabIndex = 6;
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.progressBar1.Location = new System.Drawing.Point(12, 276);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(534, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // passwordBox
            // 
            this.passwordBox.BackColor = System.Drawing.Color.Khaki;
            this.passwordBox.Location = new System.Drawing.Point(555, 143);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.PasswordChar = '●';
            this.passwordBox.Size = new System.Drawing.Size(206, 20);
            this.passwordBox.TabIndex = 8;
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.ForeColor = System.Drawing.Color.Gold;
            this.status.Location = new System.Drawing.Point(484, 313);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(62, 13);
            this.status.TabIndex = 9;
            this.status.Text = "الحالة: جاهز";
            // 
            // deCompressSelectButton
            // 
            this.deCompressSelectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(169)))), ((int)(((byte)(100)))));
            this.deCompressSelectButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.deCompressSelectButton.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.deCompressSelectButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(51)))), ((int)(((byte)(46)))));
            this.deCompressSelectButton.Location = new System.Drawing.Point(566, 271);
            this.deCompressSelectButton.Name = "deCompressSelectButton";
            this.deCompressSelectButton.Size = new System.Drawing.Size(93, 32);
            this.deCompressSelectButton.TabIndex = 10;
            this.deCompressSelectButton.Text = "فك ضغط ملف";
            this.deCompressSelectButton.UseVisualStyleBackColor = false;
            this.deCompressSelectButton.Click += new System.EventHandler(this.deCompressSelectButton_Click);
            // 
            // contextMenuButtonAdd
            // 
            this.contextMenuButtonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(169)))), ((int)(((byte)(100)))));
            this.contextMenuButtonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.contextMenuButtonAdd.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.contextMenuButtonAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(51)))), ((int)(((byte)(46)))));
            this.contextMenuButtonAdd.Location = new System.Drawing.Point(15, 9);
            this.contextMenuButtonAdd.Name = "contextMenuButtonAdd";
            this.contextMenuButtonAdd.Size = new System.Drawing.Size(130, 25);
            this.contextMenuButtonAdd.TabIndex = 11;
            this.contextMenuButtonAdd.Text = "اضافة إلى القائمة اليمنى";
            this.contextMenuButtonAdd.UseVisualStyleBackColor = false;
            this.contextMenuButtonAdd.Click += new System.EventHandler(this.contextMenuButtonAdd_Click);
            // 
            // algorithmSelector
            // 
            this.algorithmSelector.BackColor = System.Drawing.Color.Khaki;
            this.algorithmSelector.FormattingEnabled = true;
            this.algorithmSelector.Location = new System.Drawing.Point(566, 63);
            this.algorithmSelector.Name = "algorithmSelector";
            this.algorithmSelector.Size = new System.Drawing.Size(121, 21);
            this.algorithmSelector.TabIndex = 12;
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(169)))), ((int)(((byte)(100)))));
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cancelButton.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(51)))), ((int)(((byte)(46)))));
            this.cancelButton.Location = new System.Drawing.Point(12, 342);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(90, 26);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "إلغاء الضغط";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // contextMenuButtonRemove
            // 
            this.contextMenuButtonRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(169)))), ((int)(((byte)(100)))));
            this.contextMenuButtonRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.contextMenuButtonRemove.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.contextMenuButtonRemove.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(51)))), ((int)(((byte)(46)))));
            this.contextMenuButtonRemove.Location = new System.Drawing.Point(152, 9);
            this.contextMenuButtonRemove.Name = "contextMenuButtonRemove";
            this.contextMenuButtonRemove.Size = new System.Drawing.Size(122, 26);
            this.contextMenuButtonRemove.TabIndex = 14;
            this.contextMenuButtonRemove.Text = "ازالة من القائمة اليمنى";
            this.contextMenuButtonRemove.UseVisualStyleBackColor = false;
            this.contextMenuButtonRemove.Click += new System.EventHandler(this.contextMenuButtonRemove_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Gold;
            this.label1.Location = new System.Drawing.Point(693, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "اختر الخوارزمية";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Gold;
            this.label2.Location = new System.Drawing.Point(552, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "اختياري";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.ForeColor = System.Drawing.Color.Gold;
            this.checkBox1.Location = new System.Drawing.Point(656, 108);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox1.Size = new System.Drawing.Size(104, 17);
            this.checkBox1.TabIndex = 19;
            this.checkBox1.Text = "ادخل كلمة المرور";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fastRadio);
            this.groupBox1.Controls.Add(this.normalRadio);
            this.groupBox1.ForeColor = System.Drawing.Color.Gold;
            this.groupBox1.Location = new System.Drawing.Point(558, 169);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(200, 49);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "سرعة الضغط";
            // 
            // fastRadio
            // 
            this.fastRadio.AutoSize = true;
            this.fastRadio.ForeColor = System.Drawing.Color.Gold;
            this.fastRadio.Location = new System.Drawing.Point(25, 19);
            this.fastRadio.Name = "fastRadio";
            this.fastRadio.Size = new System.Drawing.Size(50, 17);
            this.fastRadio.TabIndex = 1;
            this.fastRadio.TabStop = true;
            this.fastRadio.Text = "سريع";
            this.fastRadio.UseVisualStyleBackColor = true;
            this.fastRadio.CheckedChanged += new System.EventHandler(this.fastRadio_CheckedChanged);
            // 
            // normalRadio
            // 
            this.normalRadio.AutoSize = true;
            this.normalRadio.ForeColor = System.Drawing.Color.Gold;
            this.normalRadio.Location = new System.Drawing.Point(138, 19);
            this.normalRadio.Name = "normalRadio";
            this.normalRadio.Size = new System.Drawing.Size(50, 17);
            this.normalRadio.TabIndex = 0;
            this.normalRadio.TabStop = true;
            this.normalRadio.Text = "عادي";
            this.normalRadio.UseVisualStyleBackColor = true;
            this.normalRadio.CheckedChanged += new System.EventHandler(this.normalRadio_CheckedChanged);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(51)))), ((int)(((byte)(46)))));
            this.ClientSize = new System.Drawing.Size(770, 380);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.contextMenuButtonRemove);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.algorithmSelector);
            this.Controls.Add(this.contextMenuButtonAdd);
            this.Controls.Add(this.deCompressSelectButton);
            this.Controls.Add(this.status);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.listBoxObject);
            this.Controls.Add(this.deCompressButton);
            this.Controls.Add(this.resumeButton);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.compressButton);
            this.Controls.Add(this.addFolderButton);
            this.Controls.Add(this.addFilesButton);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "Form1";
            this.Text = "برنامج ضغط";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button deCompressSelectButton;
        private System.Windows.Forms.Button contextMenuButtonAdd;
        private System.Windows.Forms.ComboBox algorithmSelector;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button contextMenuButtonRemove;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton fastRadio;
        private System.Windows.Forms.RadioButton normalRadio;
    }
}
