namespace GraduateWork
{
    partial class Form1
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
            this.InFilepath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ProcessData = new MetroFramework.Controls.MetroButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.OutFilepath = new System.Windows.Forms.TextBox();
            this.Divide = new MetroFramework.Controls.MetroButton();
            this.label5 = new System.Windows.Forms.Label();
            this.Accuracy = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Speed = new System.Windows.Forms.TextBox();
            this.Learn = new System.Windows.Forms.Button();
            this.layers = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.metroRadioButton2 = new MetroFramework.Controls.MetroRadioButton();
            this.Default = new MetroFramework.Controls.MetroRadioButton();
            this.Test = new MetroFramework.Controls.MetroButton();
            this.Logs = new System.Windows.Forms.TextBox();
            this.DeleteCheck = new MetroFramework.Controls.MetroButton();
            this.label7 = new System.Windows.Forms.Label();
            this.Percent = new System.Windows.Forms.TextBox();
            this.test_count = new System.Windows.Forms.TextBox();
            this.l_percent = new System.Windows.Forms.TextBox();
            this.l_count = new System.Windows.Forms.TextBox();
            this.test_percent = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InFilepath
            // 
            this.InFilepath.Location = new System.Drawing.Point(22, 89);
            this.InFilepath.Name = "InFilepath";
            this.InFilepath.Size = new System.Drawing.Size(174, 20);
            this.InFilepath.TabIndex = 0;
            this.InFilepath.Text = "..\\..\\Data\\chronic_kidney_disease.txt";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Путь до входного файла:";
            // 
            // ProcessData
            // 
            this.ProcessData.Location = new System.Drawing.Point(22, 119);
            this.ProcessData.Name = "ProcessData";
            this.ProcessData.Size = new System.Drawing.Size(174, 31);
            this.ProcessData.TabIndex = 3;
            this.ProcessData.Text = "Обработать данные";
            this.ProcessData.UseSelectable = true;
            this.ProcessData.Click += new System.EventHandler(this.ProcessData_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(225, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Атрибуты:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Путь до выходного файла:";
            // 
            // OutFilepath
            // 
            this.OutFilepath.Location = new System.Drawing.Point(22, 39);
            this.OutFilepath.Name = "OutFilepath";
            this.OutFilepath.Size = new System.Drawing.Size(174, 20);
            this.OutFilepath.TabIndex = 5;
            this.OutFilepath.Text = "..\\..\\Data\\process_data.txt";
            // 
            // Divide
            // 
            this.Divide.Location = new System.Drawing.Point(22, 217);
            this.Divide.Name = "Divide";
            this.Divide.Size = new System.Drawing.Size(174, 31);
            this.Divide.TabIndex = 7;
            this.Divide.Text = "Разделить выборку";
            this.Divide.UseSelectable = true;
            this.Divide.Click += new System.EventHandler(this.Divide_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(281, 240);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 45;
            this.label5.Text = "Точность :";
            // 
            // Accuracy
            // 
            this.Accuracy.Location = new System.Drawing.Point(414, 233);
            this.Accuracy.Name = "Accuracy";
            this.Accuracy.Size = new System.Drawing.Size(44, 20);
            this.Accuracy.TabIndex = 44;
            this.Accuracy.Text = "6";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(281, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 13);
            this.label4.TabIndex = 43;
            this.label4.Text = "Скорость обучения:";
            // 
            // Speed
            // 
            this.Speed.Location = new System.Drawing.Point(414, 207);
            this.Speed.Name = "Speed";
            this.Speed.Size = new System.Drawing.Size(44, 20);
            this.Speed.TabIndex = 42;
            this.Speed.Text = "0,5";
            // 
            // Learn
            // 
            this.Learn.Location = new System.Drawing.Point(552, 144);
            this.Learn.Name = "Learn";
            this.Learn.Size = new System.Drawing.Size(166, 31);
            this.Learn.TabIndex = 41;
            this.Learn.Text = "Обучить";
            this.Learn.UseVisualStyleBackColor = true;
            this.Learn.Click += new System.EventHandler(this.Learn_Click);
            // 
            // layers
            // 
            this.layers.Enabled = false;
            this.layers.FormattingEnabled = true;
            this.layers.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.layers.Location = new System.Drawing.Point(674, 68);
            this.layers.Name = "layers";
            this.layers.Size = new System.Drawing.Size(44, 21);
            this.layers.TabIndex = 39;
            this.layers.Text = "0";
            this.layers.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(565, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 13);
            this.label6.TabIndex = 40;
            this.label6.Text = "Количество слоев";
            // 
            // metroRadioButton2
            // 
            this.metroRadioButton2.AutoSize = true;
            this.metroRadioButton2.Location = new System.Drawing.Point(549, 44);
            this.metroRadioButton2.Name = "metroRadioButton2";
            this.metroRadioButton2.Size = new System.Drawing.Size(129, 15);
            this.metroRadioButton2.TabIndex = 38;
            this.metroRadioButton2.Text = "Выбор параметров";
            this.metroRadioButton2.UseSelectable = true;
            this.metroRadioButton2.CheckedChanged += new System.EventHandler(this.metroRadioButton2_CheckedChanged);
            // 
            // Default
            // 
            this.Default.AutoSize = true;
            this.Default.Checked = true;
            this.Default.Location = new System.Drawing.Point(549, 23);
            this.Default.Name = "Default";
            this.Default.Size = new System.Drawing.Size(108, 15);
            this.Default.TabIndex = 37;
            this.Default.TabStop = true;
            this.Default.Text = "По умолчанию";
            this.Default.UseSelectable = true;
            this.Default.CheckedChanged += new System.EventHandler(this.Default_CheckedChanged);
            // 
            // Test
            // 
            this.Test.Enabled = false;
            this.Test.Location = new System.Drawing.Point(22, 254);
            this.Test.Name = "Test";
            this.Test.Size = new System.Drawing.Size(174, 30);
            this.Test.TabIndex = 47;
            this.Test.Text = "Тест";
            this.Test.UseSelectable = true;
            this.Test.Click += new System.EventHandler(this.Test_Click);
            // 
            // Logs
            // 
            this.Logs.Location = new System.Drawing.Point(552, 188);
            this.Logs.Multiline = true;
            this.Logs.Name = "Logs";
            this.Logs.Size = new System.Drawing.Size(166, 80);
            this.Logs.TabIndex = 49;
            // 
            // DeleteCheck
            // 
            this.DeleteCheck.Location = new System.Drawing.Point(284, 170);
            this.DeleteCheck.Name = "DeleteCheck";
            this.DeleteCheck.Size = new System.Drawing.Size(174, 31);
            this.DeleteCheck.TabIndex = 50;
            this.DeleteCheck.Text = "Снять выделение";
            this.DeleteCheck.UseSelectable = true;
            this.DeleteCheck.Click += new System.EventHandler(this.DeleteCheck_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(160, 13);
            this.label7.TabIndex = 51;
            this.label7.Text = "Процент обучающей выборки:";
            // 
            // Percent
            // 
            this.Percent.Location = new System.Drawing.Point(23, 184);
            this.Percent.Name = "Percent";
            this.Percent.Size = new System.Drawing.Size(174, 20);
            this.Percent.TabIndex = 52;
            this.Percent.Text = "80";
            // 
            // test_count
            // 
            this.test_count.Location = new System.Drawing.Point(564, 289);
            this.test_count.Name = "test_count";
            this.test_count.Size = new System.Drawing.Size(67, 20);
            this.test_count.TabIndex = 53;
            // 
            // l_percent
            // 
            this.l_percent.Location = new System.Drawing.Point(650, 315);
            this.l_percent.Name = "l_percent";
            this.l_percent.Size = new System.Drawing.Size(67, 20);
            this.l_percent.TabIndex = 54;
            // 
            // l_count
            // 
            this.l_count.Location = new System.Drawing.Point(564, 315);
            this.l_count.Name = "l_count";
            this.l_count.Size = new System.Drawing.Size(67, 20);
            this.l_count.TabIndex = 55;
            // 
            // test_percent
            // 
            this.test_percent.Location = new System.Drawing.Point(650, 289);
            this.test_percent.Name = "test_percent";
            this.test_percent.Size = new System.Drawing.Size(67, 20);
            this.test_percent.TabIndex = 56;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(564, 351);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(153, 20);
            this.textBox1.TabIndex = 57;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(284, 261);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(174, 23);
            this.button1.TabIndex = 58;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 383);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.test_percent);
            this.Controls.Add(this.l_count);
            this.Controls.Add(this.l_percent);
            this.Controls.Add(this.test_count);
            this.Controls.Add(this.Percent);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.DeleteCheck);
            this.Controls.Add(this.Logs);
            this.Controls.Add(this.Test);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Accuracy);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Speed);
            this.Controls.Add(this.Learn);
            this.Controls.Add(this.layers);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.metroRadioButton2);
            this.Controls.Add(this.Default);
            this.Controls.Add(this.Divide);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.OutFilepath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ProcessData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.InFilepath);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox InFilepath;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroButton ProcessData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox OutFilepath;
        private MetroFramework.Controls.MetroButton Divide;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Learn;
        private System.Windows.Forms.Label label6;
        private MetroFramework.Controls.MetroRadioButton metroRadioButton2;
        private MetroFramework.Controls.MetroButton Test;
        public System.Windows.Forms.ComboBox layers;
        public MetroFramework.Controls.MetroRadioButton Default;
        public System.Windows.Forms.TextBox Logs;
        public System.Windows.Forms.TextBox Accuracy;
        public System.Windows.Forms.TextBox Speed;
        private MetroFramework.Controls.MetroButton DeleteCheck;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox Percent;
        public System.Windows.Forms.TextBox test_count;
        public System.Windows.Forms.TextBox l_percent;
        public System.Windows.Forms.TextBox l_count;
        public System.Windows.Forms.TextBox test_percent;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
    }
}

