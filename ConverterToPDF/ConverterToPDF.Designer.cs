namespace ConverterToPDF
{
    partial class ConverterToPDF
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConverterToPDF));
            this.lblStartPath = new System.Windows.Forms.Label();
            this.txtStartPath = new System.Windows.Forms.TextBox();
            this.btnStartPath = new System.Windows.Forms.Button();
            this.btnEndPath = new System.Windows.Forms.Button();
            this.txtEndPath = new System.Windows.Forms.TextBox();
            this.lblEndPath = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.progBar = new System.Windows.Forms.ProgressBar();
            this.checkDirectory = new System.Windows.Forms.CheckBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.checkNoDirectoryProcessing = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblStartPath
            // 
            this.lblStartPath.AutoSize = true;
            this.lblStartPath.Location = new System.Drawing.Point(12, 15);
            this.lblStartPath.Name = "lblStartPath";
            this.lblStartPath.Size = new System.Drawing.Size(201, 17);
            this.lblStartPath.TabIndex = 0;
            this.lblStartPath.Text = "Путь к исходной директории:";
            // 
            // txtStartPath
            // 
            this.txtStartPath.Location = new System.Drawing.Point(219, 12);
            this.txtStartPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtStartPath.Name = "txtStartPath";
            this.txtStartPath.Size = new System.Drawing.Size(776, 22);
            this.txtStartPath.TabIndex = 1;
            this.txtStartPath.TextChanged += new System.EventHandler(this.txtStartPath_TextChanged);
            // 
            // btnStartPath
            // 
            this.btnStartPath.Location = new System.Drawing.Point(1001, 10);
            this.btnStartPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStartPath.Name = "btnStartPath";
            this.btnStartPath.Size = new System.Drawing.Size(29, 27);
            this.btnStartPath.TabIndex = 2;
            this.btnStartPath.Text = "+";
            this.btnStartPath.UseVisualStyleBackColor = true;
            this.btnStartPath.Click += new System.EventHandler(this.btnStartPath_Click);
            // 
            // btnEndPath
            // 
            this.btnEndPath.Location = new System.Drawing.Point(1001, 37);
            this.btnEndPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEndPath.Name = "btnEndPath";
            this.btnEndPath.Size = new System.Drawing.Size(29, 27);
            this.btnEndPath.TabIndex = 5;
            this.btnEndPath.Text = "+";
            this.btnEndPath.UseVisualStyleBackColor = true;
            this.btnEndPath.Click += new System.EventHandler(this.btnEndPath_Click);
            // 
            // txtEndPath
            // 
            this.txtEndPath.Location = new System.Drawing.Point(219, 39);
            this.txtEndPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtEndPath.Name = "txtEndPath";
            this.txtEndPath.Size = new System.Drawing.Size(776, 22);
            this.txtEndPath.TabIndex = 4;
            this.txtEndPath.TextChanged += new System.EventHandler(this.txtEndPath_TextChanged);
            // 
            // lblEndPath
            // 
            this.lblEndPath.AutoSize = true;
            this.lblEndPath.Location = new System.Drawing.Point(11, 43);
            this.lblEndPath.Name = "lblEndPath";
            this.lblEndPath.Size = new System.Drawing.Size(203, 17);
            this.lblEndPath.TabIndex = 3;
            this.lblEndPath.Text = "Путь к конечной директории:";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(820, 400);
            this.btnStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 31);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "Начать";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_ClickAsync);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(931, 400);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 31);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // progBar
            // 
            this.progBar.Location = new System.Drawing.Point(16, 400);
            this.progBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progBar.MarqueeAnimationSpeed = 10;
            this.progBar.Name = "progBar";
            this.progBar.Size = new System.Drawing.Size(799, 31);
            this.progBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progBar.TabIndex = 8;
            this.progBar.Tag = "";
            this.progBar.Visible = false;
            // 
            // checkDirectory
            // 
            this.checkDirectory.AutoSize = true;
            this.checkDirectory.Checked = true;
            this.checkDirectory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkDirectory.Location = new System.Drawing.Point(219, 69);
            this.checkDirectory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkDirectory.Name = "checkDirectory";
            this.checkDirectory.Size = new System.Drawing.Size(300, 21);
            this.checkDirectory.TabIndex = 9;
            this.checkDirectory.Text = "Обработка каталогов по исходному пути";
            this.checkDirectory.UseVisualStyleBackColor = true;
            this.checkDirectory.CheckedChanged += new System.EventHandler(this.checkDirectory_CheckedChanged);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(16, 122);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessage.Size = new System.Drawing.Size(1013, 271);
            this.txtMessage.TabIndex = 10;
            // 
            // checkNoDirectoryProcessing
            // 
            this.checkNoDirectoryProcessing.AutoSize = true;
            this.checkNoDirectoryProcessing.Location = new System.Drawing.Point(219, 95);
            this.checkNoDirectoryProcessing.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkNoDirectoryProcessing.Name = "checkNoDirectoryProcessing";
            this.checkNoDirectoryProcessing.Size = new System.Drawing.Size(256, 21);
            this.checkNoDirectoryProcessing.TabIndex = 11;
            this.checkNoDirectoryProcessing.Text = "Обработка каталогов отсутствует";
            this.checkNoDirectoryProcessing.UseVisualStyleBackColor = true;
            this.checkNoDirectoryProcessing.CheckedChanged += new System.EventHandler(this.checkNoDirectoryProcessing_CheckedChanged);
            // 
            // ConverterToPDF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 444);
            this.Controls.Add(this.checkNoDirectoryProcessing);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.checkDirectory);
            this.Controls.Add(this.progBar);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnEndPath);
            this.Controls.Add(this.txtEndPath);
            this.Controls.Add(this.lblEndPath);
            this.Controls.Add(this.btnStartPath);
            this.Controls.Add(this.txtStartPath);
            this.Controls.Add(this.lblStartPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "ConverterToPDF";
            this.Text = "ConverterToPDF";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStartPath;
        private System.Windows.Forms.TextBox txtStartPath;
        private System.Windows.Forms.Button btnStartPath;
        private System.Windows.Forms.Button btnEndPath;
        private System.Windows.Forms.TextBox txtEndPath;
        private System.Windows.Forms.Label lblEndPath;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ProgressBar progBar;
        private System.Windows.Forms.CheckBox checkDirectory;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.CheckBox checkNoDirectoryProcessing;
    }
}

