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
            this.SuspendLayout();
            // 
            // lblStartPath
            // 
            this.lblStartPath.AutoSize = true;
            this.lblStartPath.Location = new System.Drawing.Point(9, 12);
            this.lblStartPath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStartPath.Name = "lblStartPath";
            this.lblStartPath.Size = new System.Drawing.Size(155, 13);
            this.lblStartPath.TabIndex = 0;
            this.lblStartPath.Text = "Путь к исходной директории:";
            // 
            // txtStartPath
            // 
            this.txtStartPath.Location = new System.Drawing.Point(164, 10);
            this.txtStartPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtStartPath.Name = "txtStartPath";
            this.txtStartPath.Size = new System.Drawing.Size(583, 20);
            this.txtStartPath.TabIndex = 1;
            this.txtStartPath.TextChanged += new System.EventHandler(this.txtStartPath_TextChanged);
            // 
            // btnStartPath
            // 
            this.btnStartPath.Location = new System.Drawing.Point(751, 8);
            this.btnStartPath.Margin = new System.Windows.Forms.Padding(2);
            this.btnStartPath.Name = "btnStartPath";
            this.btnStartPath.Size = new System.Drawing.Size(22, 22);
            this.btnStartPath.TabIndex = 2;
            this.btnStartPath.Text = "+";
            this.btnStartPath.UseVisualStyleBackColor = true;
            this.btnStartPath.Click += new System.EventHandler(this.btnStartPath_Click);
            // 
            // btnEndPath
            // 
            this.btnEndPath.Location = new System.Drawing.Point(751, 30);
            this.btnEndPath.Margin = new System.Windows.Forms.Padding(2);
            this.btnEndPath.Name = "btnEndPath";
            this.btnEndPath.Size = new System.Drawing.Size(22, 22);
            this.btnEndPath.TabIndex = 5;
            this.btnEndPath.Text = "+";
            this.btnEndPath.UseVisualStyleBackColor = true;
            this.btnEndPath.Click += new System.EventHandler(this.btnEndPath_Click);
            // 
            // txtEndPath
            // 
            this.txtEndPath.Location = new System.Drawing.Point(164, 32);
            this.txtEndPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtEndPath.Name = "txtEndPath";
            this.txtEndPath.Size = new System.Drawing.Size(583, 20);
            this.txtEndPath.TabIndex = 4;
            this.txtEndPath.TextChanged += new System.EventHandler(this.txtEndPath_TextChanged);
            // 
            // lblEndPath
            // 
            this.lblEndPath.AutoSize = true;
            this.lblEndPath.Location = new System.Drawing.Point(8, 35);
            this.lblEndPath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEndPath.Name = "lblEndPath";
            this.lblEndPath.Size = new System.Drawing.Size(155, 13);
            this.lblEndPath.TabIndex = 3;
            this.lblEndPath.Text = "Путь к конечной директории:";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(615, 325);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 25);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "Начать";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_ClickAsync);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(698, 325);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // progBar
            // 
            this.progBar.Location = new System.Drawing.Point(12, 325);
            this.progBar.Margin = new System.Windows.Forms.Padding(2);
            this.progBar.MarqueeAnimationSpeed = 10;
            this.progBar.Name = "progBar";
            this.progBar.Size = new System.Drawing.Size(599, 25);
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
            this.checkDirectory.Location = new System.Drawing.Point(164, 56);
            this.checkDirectory.Margin = new System.Windows.Forms.Padding(2);
            this.checkDirectory.Name = "checkDirectory";
            this.checkDirectory.Size = new System.Drawing.Size(233, 17);
            this.checkDirectory.TabIndex = 9;
            this.checkDirectory.Text = "Обработка каталогов по исходному пути";
            this.checkDirectory.UseVisualStyleBackColor = true;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(12, 78);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessage.Size = new System.Drawing.Size(761, 242);
            this.txtMessage.TabIndex = 10;
            // 
            // ConverterToPDF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 361);
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
            this.Margin = new System.Windows.Forms.Padding(2);
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
    }
}

