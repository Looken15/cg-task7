namespace task7
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.button6 = new System.Windows.Forms.Button();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.save_button = new System.Windows.Forms.Button();
            this.load_button = new System.Windows.Forms.Button();
            this.moveButton = new System.Windows.Forms.RadioButton();
            this.rotateButton = new System.Windows.Forms.RadioButton();
            this.ScaleButton = new System.Windows.Forms.RadioButton();
            this.drawButton = new System.Windows.Forms.RadioButton();
            this.clear_button = new System.Windows.Forms.Button();
            this.countBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mesh_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(694, 425);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(713, 13);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(81, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(712, 249);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(33, 17);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "X";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(712, 272);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(33, 17);
            this.checkBox2.TabIndex = 10;
            this.checkBox2.Text = "Y";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(712, 295);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(33, 17);
            this.checkBox3.TabIndex = 11;
            this.checkBox3.Text = "Z";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(712, 220);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(83, 23);
            this.button6.TabIndex = 12;
            this.button6.Text = "Mirror";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(712, 318);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(82, 17);
            this.checkBox5.TabIndex = 14;
            this.checkBox5.Text = "From centre";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Default mode",
            "Rotation line",
            "Edit rotation line"});
            this.comboBox2.Location = new System.Drawing.Point(712, 341);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(83, 21);
            this.comboBox2.TabIndex = 17;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // save_button
            // 
            this.save_button.Location = new System.Drawing.Point(711, 385);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(83, 23);
            this.save_button.TabIndex = 18;
            this.save_button.Text = "Сохранить";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // load_button
            // 
            this.load_button.Location = new System.Drawing.Point(711, 414);
            this.load_button.Name = "load_button";
            this.load_button.Size = new System.Drawing.Size(83, 23);
            this.load_button.TabIndex = 19;
            this.load_button.Text = "Загрузить";
            this.load_button.UseVisualStyleBackColor = true;
            this.load_button.Click += new System.EventHandler(this.load_button_Click);
            // 
            // moveButton
            // 
            this.moveButton.AutoSize = true;
            this.moveButton.Location = new System.Drawing.Point(713, 40);
            this.moveButton.Name = "moveButton";
            this.moveButton.Size = new System.Drawing.Size(52, 17);
            this.moveButton.TabIndex = 20;
            this.moveButton.TabStop = true;
            this.moveButton.Text = "Move";
            this.moveButton.UseVisualStyleBackColor = true;
            this.moveButton.CheckedChanged += new System.EventHandler(this.moveButton_CheckedChanged);
            // 
            // rotateButton
            // 
            this.rotateButton.AutoSize = true;
            this.rotateButton.Location = new System.Drawing.Point(713, 62);
            this.rotateButton.Name = "rotateButton";
            this.rotateButton.Size = new System.Drawing.Size(57, 17);
            this.rotateButton.TabIndex = 21;
            this.rotateButton.TabStop = true;
            this.rotateButton.Text = "Rotate";
            this.rotateButton.UseVisualStyleBackColor = true;
            this.rotateButton.CheckedChanged += new System.EventHandler(this.rotateButton_CheckedChanged);
            // 
            // ScaleButton
            // 
            this.ScaleButton.AutoSize = true;
            this.ScaleButton.Location = new System.Drawing.Point(714, 85);
            this.ScaleButton.Name = "ScaleButton";
            this.ScaleButton.Size = new System.Drawing.Size(52, 17);
            this.ScaleButton.TabIndex = 22;
            this.ScaleButton.TabStop = true;
            this.ScaleButton.Text = "Scale";
            this.ScaleButton.UseVisualStyleBackColor = true;
            this.ScaleButton.CheckedChanged += new System.EventHandler(this.ScaleButton_CheckedChanged);
            // 
            // drawButton
            // 
            this.drawButton.AutoSize = true;
            this.drawButton.Location = new System.Drawing.Point(713, 108);
            this.drawButton.Name = "drawButton";
            this.drawButton.Size = new System.Drawing.Size(50, 17);
            this.drawButton.TabIndex = 23;
            this.drawButton.TabStop = true;
            this.drawButton.Text = "Draw";
            this.drawButton.UseVisualStyleBackColor = true;
            this.drawButton.CheckedChanged += new System.EventHandler(this.drawButton_CheckedChanged);
            // 
            // clear_button
            // 
            this.clear_button.Location = new System.Drawing.Point(713, 194);
            this.clear_button.Name = "clear_button";
            this.clear_button.Size = new System.Drawing.Size(83, 23);
            this.clear_button.TabIndex = 24;
            this.clear_button.Text = "Clear";
            this.clear_button.UseVisualStyleBackColor = true;
            this.clear_button.Click += new System.EventHandler(this.clear_button_Click);
            // 
            // countBox
            // 
            this.countBox.Location = new System.Drawing.Point(767, 132);
            this.countBox.Name = "countBox";
            this.countBox.Size = new System.Drawing.Size(21, 20);
            this.countBox.TabIndex = 25;
            this.countBox.Text = "6";
            this.countBox.TextChanged += new System.EventHandler(this.countBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(717, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Counter";
            // 
            // mesh_button
            // 
            this.mesh_button.Location = new System.Drawing.Point(714, 158);
            this.mesh_button.Name = "mesh_button";
            this.mesh_button.Size = new System.Drawing.Size(83, 23);
            this.mesh_button.TabIndex = 27;
            this.mesh_button.Text = "Make mesh";
            this.mesh_button.UseVisualStyleBackColor = true;
            this.mesh_button.Click += new System.EventHandler(this.mesh_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mesh_button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.countBox);
            this.Controls.Add(this.clear_button);
            this.Controls.Add(this.drawButton);
            this.Controls.Add(this.ScaleButton);
            this.Controls.Add(this.rotateButton);
            this.Controls.Add(this.moveButton);
            this.Controls.Add(this.load_button);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.checkBox5);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.Button load_button;
        private System.Windows.Forms.RadioButton moveButton;
        private System.Windows.Forms.RadioButton rotateButton;
        private System.Windows.Forms.RadioButton ScaleButton;
        private System.Windows.Forms.RadioButton drawButton;
        private System.Windows.Forms.Button clear_button;
        private System.Windows.Forms.TextBox countBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button mesh_button;
    }
}

