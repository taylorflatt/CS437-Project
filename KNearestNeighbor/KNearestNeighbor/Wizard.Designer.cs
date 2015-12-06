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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Wizard));
            this.wizardControl2 = new WizardBase.WizardControl();
            this.descriptionStep1 = new WizardBase.StartStep();
            this.initialDataStep2 = new WizardBase.IntermediateStep();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numClassesLabel = new System.Windows.Forms.Label();
            this.normalizeTrainingDataCheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.kValueTB = new System.Windows.Forms.TextBox();
            this.kValueLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.normalizeInputDataCheckBox = new System.Windows.Forms.CheckBox();
            this.displayDataStep3 = new WizardBase.IntermediateStep();
            this.finishStep = new WizardBase.FinishStep();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.predictAttributeNumLabel = new System.Windows.Forms.Label();
            this.chooseTrainingDataLabel = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.fileLocationLabel = new System.Windows.Forms.Label();
            this.errorProviderK = new System.Windows.Forms.ErrorProvider(this.components);
            this.initialDataStep2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderK)).BeginInit();
            this.SuspendLayout();
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
            this.wizardControl2.Size = new System.Drawing.Size(618, 720);
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
            this.initialDataStep2.Controls.Add(this.groupBox4);
            this.initialDataStep2.Controls.Add(this.groupBox2);
            this.initialDataStep2.Controls.Add(this.groupBox3);
            this.initialDataStep2.Controls.Add(this.groupBox1);
            this.initialDataStep2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.initialDataStep2.Name = "initialDataStep2";
            this.initialDataStep2.Subtitle = resources.GetString("initialDataStep2.Subtitle");
            this.initialDataStep2.Title = "Data Initialization";
            this.initialDataStep2.Click += new System.EventHandler(this.initialDataStep2_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.68628F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.31372F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(14, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(204, 260);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.fileLocationLabel);
            this.groupBox2.Controls.Add(this.chooseTrainingDataLabel);
            this.groupBox2.Controls.Add(this.predictAttributeNumLabel);
            this.groupBox2.Controls.Add(this.numClassesLabel);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(13, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(592, 73);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Training Information";
            // 
            // numClassesLabel
            // 
            this.numClassesLabel.AutoSize = true;
            this.numClassesLabel.Location = new System.Drawing.Point(7, 48);
            this.numClassesLabel.Name = "numClassesLabel";
            this.numClassesLabel.Size = new System.Drawing.Size(106, 13);
            this.numClassesLabel.TabIndex = 6;
            this.numClassesLabel.Text = "Number of Attributes:";
            // 
            // normalizeTrainingDataCheckBox
            // 
            this.normalizeTrainingDataCheckBox.AutoSize = true;
            this.normalizeTrainingDataCheckBox.Location = new System.Drawing.Point(6, 68);
            this.normalizeTrainingDataCheckBox.Name = "normalizeTrainingDataCheckBox";
            this.normalizeTrainingDataCheckBox.Size = new System.Drawing.Size(220, 17);
            this.normalizeTrainingDataCheckBox.TabIndex = 2;
            this.normalizeTrainingDataCheckBox.Text = "(Recommended) Normalize Training Data";
            this.normalizeTrainingDataCheckBox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(87, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.normalizeInputDataCheckBox);
            this.groupBox3.Controls.Add(this.kValueTB);
            this.groupBox3.Controls.Add(this.normalizeTrainingDataCheckBox);
            this.groupBox3.Controls.Add(this.kValueLabel);
            this.groupBox3.Location = new System.Drawing.Point(256, 156);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(349, 276);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "KNN Options";
            // 
            // kValueTB
            // 
            this.kValueTB.Location = new System.Drawing.Point(57, 19);
            this.kValueTB.Name = "kValueTB";
            this.kValueTB.Size = new System.Drawing.Size(100, 20);
            this.kValueTB.TabIndex = 1;
            this.kValueTB.TextChanged += new System.EventHandler(this.kValueTB_TextChanged);
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
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(13, 147);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 285);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input";
            // 
            // normalizeInputDataCheckBox
            // 
            this.normalizeInputDataCheckBox.AutoSize = true;
            this.normalizeInputDataCheckBox.Location = new System.Drawing.Point(6, 45);
            this.normalizeInputDataCheckBox.Name = "normalizeInputDataCheckBox";
            this.normalizeInputDataCheckBox.Size = new System.Drawing.Size(99, 17);
            this.normalizeInputDataCheckBox.TabIndex = 5;
            this.normalizeInputDataCheckBox.Text = "Normalize Input";
            this.normalizeInputDataCheckBox.UseVisualStyleBackColor = true;
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
            // predictAttributeNumLabel
            // 
            this.predictAttributeNumLabel.AutoSize = true;
            this.predictAttributeNumLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.predictAttributeNumLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.predictAttributeNumLabel.Location = new System.Drawing.Point(119, 48);
            this.predictAttributeNumLabel.Name = "predictAttributeNumLabel";
            this.predictAttributeNumLabel.Size = new System.Drawing.Size(129, 13);
            this.predictAttributeNumLabel.TabIndex = 8;
            this.predictAttributeNumLabel.Text = "We see no attributes.";
            // 
            // chooseTrainingDataLabel
            // 
            this.chooseTrainingDataLabel.AutoSize = true;
            this.chooseTrainingDataLabel.Location = new System.Drawing.Point(7, 21);
            this.chooseTrainingDataLabel.Name = "chooseTrainingDataLabel";
            this.chooseTrainingDataLabel.Size = new System.Drawing.Size(74, 13);
            this.chooseTrainingDataLabel.TabIndex = 9;
            this.chooseTrainingDataLabel.Text = "Training Data:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 19);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(580, 214);
            this.dataGridView1.TabIndex = 16;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dataGridView1);
            this.groupBox4.Location = new System.Drawing.Point(13, 438);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(592, 239);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "View Training Data";
            // 
            // fileLocationLabel
            // 
            this.fileLocationLabel.AutoSize = true;
            this.fileLocationLabel.Location = new System.Drawing.Point(163, 21);
            this.fileLocationLabel.Name = "fileLocationLabel";
            this.fileLocationLabel.Size = new System.Drawing.Size(27, 13);
            this.fileLocationLabel.TabIndex = 10;
            this.fileLocationLabel.Text = "N/A";
            // 
            // errorProviderK
            // 
            this.errorProviderK.ContainerControl = this;
            // 
            // Wizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 721);
            this.Controls.Add(this.wizardControl2);
            this.Name = "Wizard";
            this.Text = "Wizard";
            this.initialDataStep2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderK)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private WizardBase.WizardControl wizardControl2;
        private WizardBase.StartStep descriptionStep1;
        private WizardBase.IntermediateStep initialDataStep2;
        private WizardBase.IntermediateStep displayDataStep3;
        private WizardBase.FinishStep finishStep;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox normalizeInputDataCheckBox;
        private System.Windows.Forms.TextBox kValueTB;
        private System.Windows.Forms.Label kValueLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox normalizeTrainingDataCheckBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label numClassesLabel;
        private System.Windows.Forms.Label predictAttributeNumLabel;
        private System.Windows.Forms.Label chooseTrainingDataLabel;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label fileLocationLabel;
        private System.Windows.Forms.ErrorProvider errorProviderK;
    }
}