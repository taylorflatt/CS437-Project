namespace KNearestNeighbor
{
    partial class Wizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Wizard));
            this.wizardControl1 = new WizardBase.WizardControl();
            this.licenceStep1 = new WizardBase.LicenceStep();
            this.intermediateStep1 = new WizardBase.IntermediateStep();
            this.finishStep1 = new WizardBase.FinishStep();
            this.wizardControl2 = new WizardBase.WizardControl();
            this.descriptionStep1 = new WizardBase.StartStep();
            this.initialDataStep2 = new WizardBase.IntermediateStep();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.kValueTB = new System.Windows.Forms.TextBox();
            this.kValueLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.attribute5TB = new System.Windows.Forms.TextBox();
            this.attribute5Label = new System.Windows.Forms.Label();
            this.attribute4TB = new System.Windows.Forms.TextBox();
            this.attribute4Label = new System.Windows.Forms.Label();
            this.attribute3TB = new System.Windows.Forms.TextBox();
            this.attribute3Label = new System.Windows.Forms.Label();
            this.attribute2TB = new System.Windows.Forms.TextBox();
            this.attribute2Label = new System.Windows.Forms.Label();
            this.attribute1TB = new System.Windows.Forms.TextBox();
            this.attribute1Label = new System.Windows.Forms.Label();
            this.modelTB = new System.Windows.Forms.TextBox();
            this.modelLabel = new System.Windows.Forms.Label();
            this.displayDataStep3 = new WizardBase.IntermediateStep();
            this.finishStep = new WizardBase.FinishStep();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.numClassesTB = new System.Windows.Forms.TextBox();
            this.numClassesLabel = new System.Windows.Forms.Label();
            this.initialDataStep2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.BackButtonEnabled = false;
            this.wizardControl1.BackButtonVisible = true;
            this.wizardControl1.CancelButtonEnabled = true;
            this.wizardControl1.CancelButtonVisible = true;
            this.wizardControl1.EulaButtonEnabled = true;
            this.wizardControl1.EulaButtonText = "eula";
            this.wizardControl1.EulaButtonVisible = true;
            this.wizardControl1.HelpButtonEnabled = true;
            this.wizardControl1.HelpButtonVisible = true;
            this.wizardControl1.Location = new System.Drawing.Point(75, 396);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.NextButtonEnabled = true;
            this.wizardControl1.NextButtonVisible = true;
            this.wizardControl1.Size = new System.Drawing.Size(534, 403);
            this.wizardControl1.WizardSteps.AddRange(new WizardBase.WizardStep[] {
            this.licenceStep1,
            this.intermediateStep1,
            this.finishStep1});
            // 
            // licenceStep1
            // 
            this.licenceStep1.BindingImage = ((System.Drawing.Image)(resources.GetObject("licenceStep1.BindingImage")));
            this.licenceStep1.LicenseFile = "";
            this.licenceStep1.Name = "licenceStep1";
            this.licenceStep1.Title = "License Agreement.";
            this.licenceStep1.Warning = "Please read the following license agreement. You must accept the terms  of this a" +
    "greement before continuing.";
            this.licenceStep1.WarningFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            // 
            // intermediateStep1
            // 
            this.intermediateStep1.BindingImage = ((System.Drawing.Image)(resources.GetObject("intermediateStep1.BindingImage")));
            this.intermediateStep1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.intermediateStep1.Name = "intermediateStep1";
            // 
            // finishStep1
            // 
            this.finishStep1.BindingImage = ((System.Drawing.Image)(resources.GetObject("finishStep1.BindingImage")));
            this.finishStep1.Name = "finishStep1";
            // 
            // wizardControl2
            // 
            this.wizardControl2.BackButtonEnabled = true;
            this.wizardControl2.BackButtonVisible = true;
            this.wizardControl2.CancelButtonEnabled = true;
            this.wizardControl2.CancelButtonVisible = true;
            this.wizardControl2.EulaButtonEnabled = true;
            this.wizardControl2.EulaButtonText = "eula";
            this.wizardControl2.EulaButtonVisible = true;
            this.wizardControl2.HelpButtonEnabled = true;
            this.wizardControl2.HelpButtonVisible = true;
            this.wizardControl2.Location = new System.Drawing.Point(2, 0);
            this.wizardControl2.Name = "wizardControl2";
            this.wizardControl2.NextButtonEnabled = true;
            this.wizardControl2.NextButtonVisible = true;
            this.wizardControl2.Size = new System.Drawing.Size(618, 640);
            this.wizardControl2.WizardSteps.AddRange(new WizardBase.WizardStep[] {
            this.descriptionStep1,
            this.initialDataStep2,
            this.displayDataStep3,
            this.finishStep});
            // 
            // descriptionStep1
            // 
            this.descriptionStep1.BindingImage = ((System.Drawing.Image)(resources.GetObject("descriptionStep1.BindingImage")));
            this.descriptionStep1.Icon = ((System.Drawing.Image)(resources.GetObject("descriptionStep1.Icon")));
            this.descriptionStep1.Name = "descriptionStep1";
            this.descriptionStep1.Subtitle = "KNN is an non parametric lazy learning algorithm.";
            this.descriptionStep1.Title = "Determine your nearest competitor";
            // 
            // initialDataStep2
            // 
            this.initialDataStep2.BindingImage = ((System.Drawing.Image)(resources.GetObject("initialDataStep2.BindingImage")));
            this.initialDataStep2.Controls.Add(this.tableLayoutPanel1);
            this.initialDataStep2.Controls.Add(this.groupBox2);
            this.initialDataStep2.Controls.Add(this.groupBox3);
            this.initialDataStep2.Controls.Add(this.groupBox1);
            this.initialDataStep2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.initialDataStep2.Name = "initialDataStep2";
            this.initialDataStep2.Subtitle = resources.GetString("initialDataStep2.Subtitle");
            this.initialDataStep2.Title = "Data Initialization";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(117, 399);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numClassesTB);
            this.groupBox2.Controls.Add(this.numClassesLabel);
            this.groupBox2.Controls.Add(this.checkBox2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(13, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(414, 76);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Training Data";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(6, 19);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(139, 17);
            this.checkBox2.TabIndex = 2;
            this.checkBox2.Text = "Normalize Training Data";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(333, 47);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.kValueTB);
            this.groupBox3.Controls.Add(this.kValueLabel);
            this.groupBox3.Location = new System.Drawing.Point(13, 150);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(204, 178);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "KNN Options";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 50);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(99, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Normalize Input";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(171, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "N/A";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Current K-Value:";
            // 
            // kValueTB
            // 
            this.kValueTB.Location = new System.Drawing.Point(57, 19);
            this.kValueTB.Name = "kValueTB";
            this.kValueTB.Size = new System.Drawing.Size(100, 20);
            this.kValueTB.TabIndex = 1;
            // 
            // kValueLabel
            // 
            this.kValueLabel.AutoSize = true;
            this.kValueLabel.Location = new System.Drawing.Point(7, 24);
            this.kValueLabel.Name = "kValueLabel";
            this.kValueLabel.Size = new System.Drawing.Size(44, 13);
            this.kValueLabel.TabIndex = 0;
            this.kValueLabel.Text = "K-Value";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.attribute5TB);
            this.groupBox1.Controls.Add(this.attribute5Label);
            this.groupBox1.Controls.Add(this.attribute4TB);
            this.groupBox1.Controls.Add(this.attribute4Label);
            this.groupBox1.Controls.Add(this.attribute3TB);
            this.groupBox1.Controls.Add(this.attribute3Label);
            this.groupBox1.Controls.Add(this.attribute2TB);
            this.groupBox1.Controls.Add(this.attribute2Label);
            this.groupBox1.Controls.Add(this.attribute1TB);
            this.groupBox1.Controls.Add(this.attribute1Label);
            this.groupBox1.Controls.Add(this.modelTB);
            this.groupBox1.Controls.Add(this.modelLabel);
            this.groupBox1.Location = new System.Drawing.Point(223, 150);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(204, 178);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input";
            // 
            // attribute5TB
            // 
            this.attribute5TB.Location = new System.Drawing.Point(83, 152);
            this.attribute5TB.Name = "attribute5TB";
            this.attribute5TB.Size = new System.Drawing.Size(100, 20);
            this.attribute5TB.TabIndex = 11;
            // 
            // attribute5Label
            // 
            this.attribute5Label.AutoSize = true;
            this.attribute5Label.Location = new System.Drawing.Point(22, 155);
            this.attribute5Label.Name = "attribute5Label";
            this.attribute5Label.Size = new System.Drawing.Size(55, 13);
            this.attribute5Label.TabIndex = 10;
            this.attribute5Label.Text = "Attribute5:";
            // 
            // attribute4TB
            // 
            this.attribute4TB.Location = new System.Drawing.Point(83, 125);
            this.attribute4TB.Name = "attribute4TB";
            this.attribute4TB.Size = new System.Drawing.Size(100, 20);
            this.attribute4TB.TabIndex = 9;
            // 
            // attribute4Label
            // 
            this.attribute4Label.AutoSize = true;
            this.attribute4Label.Location = new System.Drawing.Point(22, 128);
            this.attribute4Label.Name = "attribute4Label";
            this.attribute4Label.Size = new System.Drawing.Size(55, 13);
            this.attribute4Label.TabIndex = 8;
            this.attribute4Label.Text = "Attribute4:";
            // 
            // attribute3TB
            // 
            this.attribute3TB.Location = new System.Drawing.Point(83, 99);
            this.attribute3TB.Name = "attribute3TB";
            this.attribute3TB.Size = new System.Drawing.Size(100, 20);
            this.attribute3TB.TabIndex = 7;
            // 
            // attribute3Label
            // 
            this.attribute3Label.AutoSize = true;
            this.attribute3Label.Location = new System.Drawing.Point(22, 102);
            this.attribute3Label.Name = "attribute3Label";
            this.attribute3Label.Size = new System.Drawing.Size(55, 13);
            this.attribute3Label.TabIndex = 6;
            this.attribute3Label.Text = "Attribute3:";
            // 
            // attribute2TB
            // 
            this.attribute2TB.Location = new System.Drawing.Point(83, 73);
            this.attribute2TB.Name = "attribute2TB";
            this.attribute2TB.Size = new System.Drawing.Size(100, 20);
            this.attribute2TB.TabIndex = 5;
            // 
            // attribute2Label
            // 
            this.attribute2Label.AutoSize = true;
            this.attribute2Label.Location = new System.Drawing.Point(22, 76);
            this.attribute2Label.Name = "attribute2Label";
            this.attribute2Label.Size = new System.Drawing.Size(55, 13);
            this.attribute2Label.TabIndex = 4;
            this.attribute2Label.Text = "Attribute2:";
            // 
            // attribute1TB
            // 
            this.attribute1TB.Location = new System.Drawing.Point(83, 47);
            this.attribute1TB.Name = "attribute1TB";
            this.attribute1TB.Size = new System.Drawing.Size(100, 20);
            this.attribute1TB.TabIndex = 3;
            // 
            // attribute1Label
            // 
            this.attribute1Label.AutoSize = true;
            this.attribute1Label.Location = new System.Drawing.Point(22, 50);
            this.attribute1Label.Name = "attribute1Label";
            this.attribute1Label.Size = new System.Drawing.Size(55, 13);
            this.attribute1Label.TabIndex = 2;
            this.attribute1Label.Text = "Attribute1:";
            // 
            // modelTB
            // 
            this.modelTB.Location = new System.Drawing.Point(83, 21);
            this.modelTB.MaxLength = 15;
            this.modelTB.Name = "modelTB";
            this.modelTB.Size = new System.Drawing.Size(100, 20);
            this.modelTB.TabIndex = 1;
            // 
            // modelLabel
            // 
            this.modelLabel.AutoSize = true;
            this.modelLabel.Location = new System.Drawing.Point(22, 24);
            this.modelLabel.Name = "modelLabel";
            this.modelLabel.Size = new System.Drawing.Size(39, 13);
            this.modelLabel.TabIndex = 0;
            this.modelLabel.Text = "Model:";
            // 
            // displayDataStep3
            // 
            this.displayDataStep3.BindingImage = ((System.Drawing.Image)(resources.GetObject("displayDataStep3.BindingImage")));
            this.displayDataStep3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.displayDataStep3.Name = "displayDataStep3";
            // 
            // finishStep
            // 
            this.finishStep.BindingImage = ((System.Drawing.Image)(resources.GetObject("finishStep.BindingImage")));
            this.finishStep.Name = "finishStep";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // numClassesTB
            // 
            this.numClassesTB.Location = new System.Drawing.Point(111, 44);
            this.numClassesTB.Name = "numClassesTB";
            this.numClassesTB.Size = new System.Drawing.Size(100, 20);
            this.numClassesTB.TabIndex = 7;
            this.numClassesTB.TextChanged += new System.EventHandler(this.numClassesTB_TextChanged);
            // 
            // numClassesLabel
            // 
            this.numClassesLabel.AutoSize = true;
            this.numClassesLabel.Location = new System.Drawing.Point(7, 47);
            this.numClassesLabel.Name = "numClassesLabel";
            this.numClassesLabel.Size = new System.Drawing.Size(98, 13);
            this.numClassesLabel.TabIndex = 6;
            this.numClassesLabel.Text = "Number of Classes:";
            // 
            // Wizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 639);
            this.Controls.Add(this.wizardControl2);
            this.Controls.Add(this.wizardControl1);
            this.Name = "Wizard";
            this.Text = "Wizard";
            this.initialDataStep2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private WizardBase.WizardControl wizardControl1;
        private WizardBase.LicenceStep licenceStep1;
        private WizardBase.IntermediateStep intermediateStep1;
        private WizardBase.FinishStep finishStep1;
        private WizardBase.WizardControl wizardControl2;
        private WizardBase.StartStep descriptionStep1;
        private WizardBase.IntermediateStep initialDataStep2;
        private WizardBase.IntermediateStep displayDataStep3;
        private WizardBase.FinishStep finishStep;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBox1;
        protected internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox kValueTB;
        private System.Windows.Forms.Label kValueLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox attribute5TB;
        private System.Windows.Forms.Label attribute5Label;
        private System.Windows.Forms.TextBox attribute4TB;
        private System.Windows.Forms.Label attribute4Label;
        private System.Windows.Forms.TextBox attribute3TB;
        private System.Windows.Forms.Label attribute3Label;
        private System.Windows.Forms.TextBox attribute2TB;
        private System.Windows.Forms.Label attribute2Label;
        private System.Windows.Forms.TextBox attribute1TB;
        private System.Windows.Forms.Label attribute1Label;
        private System.Windows.Forms.TextBox modelTB;
        private System.Windows.Forms.Label modelLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox numClassesTB;
        private System.Windows.Forms.Label numClassesLabel;
    }
}