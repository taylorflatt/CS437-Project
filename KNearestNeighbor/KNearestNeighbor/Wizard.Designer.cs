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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.baseControl = new WizardBase.WizardControl();
            this.descriptionStep1 = new WizardBase.StartStep();
            this.programDescriptionTB = new System.Windows.Forms.RichTextBox();
            this.initialDataStep2 = new WizardBase.IntermediateStep();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dataInitializationInstructionsTB = new System.Windows.Forms.RichTextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.fileLocationLabel = new System.Windows.Forms.Label();
            this.chooseTrainingDataLabel = new System.Windows.Forms.Label();
            this.predictAttributeNumLabel = new System.Windows.Forms.Label();
            this.numClassesLabel = new System.Windows.Forms.Label();
            this.browseButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dontNormalizeInputDataCheckBox = new System.Windows.Forms.CheckBox();
            this.kValueTB = new System.Windows.Forms.TextBox();
            this.dontNormalizeTrainingDataCheckBox = new System.Windows.Forms.CheckBox();
            this.kValueLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.displayDataStep3 = new WizardBase.IntermediateStep();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.closestCompetitorDistanceLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.closestCompetitorNameLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.closestCompetitorClassLabel = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.closestCompetitor = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.showAllDistancesRadioButton = new System.Windows.Forms.RadioButton();
            this.showKDistancesRadioButton = new System.Windows.Forms.RadioButton();
            this.plotButton = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.plotYComboBox = new System.Windows.Forms.ComboBox();
            this.plotXComboBox = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.errorProviderK = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderAttributes = new System.Windows.Forms.ErrorProvider(this.components);
            this.dataSet1 = new System.Data.DataSet();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.errorProviderPlot = new System.Windows.Forms.ErrorProvider(this.components);
            this.wizardBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.descriptionStep1.SuspendLayout();
            this.initialDataStep2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.displayDataStep3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderAttributes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPlot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wizardBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // baseControl
            // 
            this.baseControl.BackButtonEnabled = true;
            this.baseControl.BackButtonVisible = true;
            this.baseControl.CancelButtonEnabled = false;
            this.baseControl.CancelButtonText = "New";
            this.baseControl.CancelButtonVisible = false;
            this.baseControl.CausesValidation = false;
            this.baseControl.EulaButtonEnabled = false;
            this.baseControl.EulaButtonText = "eula";
            this.baseControl.EulaButtonVisible = false;
            this.baseControl.HelpButtonEnabled = true;
            this.baseControl.HelpButtonVisible = true;
            this.baseControl.Location = new System.Drawing.Point(2, 0);
            this.baseControl.Name = "baseControl";
            this.baseControl.NextButtonEnabled = true;
            this.baseControl.NextButtonVisible = true;
            this.baseControl.Size = new System.Drawing.Size(618, 720);
            this.baseControl.WizardSteps.AddRange(new WizardBase.WizardStep[] {
            this.descriptionStep1,
            this.initialDataStep2,
            this.displayDataStep3});
            this.baseControl.BackButtonClick += new System.ComponentModel.CancelEventHandler(this.baseControl_BackButtonClick);
            this.baseControl.CancelButtonClick += new System.EventHandler(this.baseControl_CancelButtonClick);
            this.baseControl.CurrentStepIndexChanged += new System.EventHandler(this.wizardControl2_CurrentStepIndexChanged);
            this.baseControl.HelpButtonClick += new System.EventHandler(this.Wizard_HelpButtonClicked);
            this.baseControl.NextButtonClick += new WizardBase.GenericCancelEventHandler<WizardBase.WizardControl>(this.baseControl_NextButtonClick);
            // 
            // descriptionStep1
            // 
            this.descriptionStep1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.descriptionStep1.BindingImage = ((System.Drawing.Image)(resources.GetObject("descriptionStep1.BindingImage")));
            this.descriptionStep1.CausesValidation = false;
            this.descriptionStep1.Controls.Add(this.programDescriptionTB);
            this.descriptionStep1.Icon = null;
            this.errorProviderAttributes.SetIconAlignment(this.descriptionStep1, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.descriptionStep1.Name = "descriptionStep1";
            this.descriptionStep1.Subtitle = "The non parametric lazy learning algorithm.";
            this.descriptionStep1.SubtitleAppearence = ((WizardBase.TextAppearence)(resources.GetObject("descriptionStep1.SubtitleAppearence")));
            this.descriptionStep1.Title = "Welcome to the KNN Calculator";
            // 
            // programDescriptionTB
            // 
            this.programDescriptionTB.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.programDescriptionTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.programDescriptionTB.BulletIndent = 30;
            this.programDescriptionTB.CausesValidation = false;
            this.programDescriptionTB.Cursor = System.Windows.Forms.Cursors.Default;
            this.programDescriptionTB.Location = new System.Drawing.Point(171, 59);
            this.programDescriptionTB.Name = "programDescriptionTB";
            this.programDescriptionTB.ReadOnly = true;
            this.programDescriptionTB.Size = new System.Drawing.Size(447, 621);
            this.programDescriptionTB.TabIndex = 0;
            this.programDescriptionTB.Text = "";
            this.programDescriptionTB.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.programDescriptionTB_LinkClicked);
            this.programDescriptionTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.programDescriptionTB_KeyDown);
            this.programDescriptionTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.programDescriptionTB_KeyPress);
            this.programDescriptionTB.KeyUp += new System.Windows.Forms.KeyEventHandler(this.programDescriptionTB_KeyUp);
            // 
            // initialDataStep2
            // 
            this.initialDataStep2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.initialDataStep2.BindingImage = global::KNearestNeighbor.Properties.Resources.orange_light_top;
            this.initialDataStep2.Controls.Add(this.groupBox5);
            this.initialDataStep2.Controls.Add(this.groupBox4);
            this.initialDataStep2.Controls.Add(this.groupBox2);
            this.initialDataStep2.Controls.Add(this.groupBox3);
            this.initialDataStep2.Controls.Add(this.groupBox1);
            this.initialDataStep2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.initialDataStep2.Name = "initialDataStep2";
            this.initialDataStep2.Subtitle = resources.GetString("initialDataStep2.Subtitle");
            this.initialDataStep2.Title = "Data Initialization";
            this.initialDataStep2.Validating += new System.ComponentModel.CancelEventHandler(this.initialDataStep2_Validating);
            this.initialDataStep2.Validated += new System.EventHandler(this.initialDataStep2_Validated);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dataInitializationInstructionsTB);
            this.groupBox5.Location = new System.Drawing.Point(257, 257);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(348, 194);
            this.groupBox5.TabIndex = 18;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Instructions";
            // 
            // dataInitializationInstructionsTB
            // 
            this.dataInitializationInstructionsTB.BackColor = System.Drawing.SystemColors.Control;
            this.dataInitializationInstructionsTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataInitializationInstructionsTB.BulletIndent = 30;
            this.dataInitializationInstructionsTB.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataInitializationInstructionsTB.Location = new System.Drawing.Point(9, 19);
            this.dataInitializationInstructionsTB.Name = "dataInitializationInstructionsTB";
            this.dataInitializationInstructionsTB.ReadOnly = true;
            this.dataInitializationInstructionsTB.Size = new System.Drawing.Size(333, 169);
            this.dataInitializationInstructionsTB.TabIndex = 2;
            this.dataInitializationInstructionsTB.TabStop = false;
            this.dataInitializationInstructionsTB.Text = "";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dataGridView1);
            this.groupBox4.Location = new System.Drawing.Point(13, 457);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(592, 220);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "View Training Data";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 19);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(580, 195);
            this.dataGridView1.TabIndex = 16;
            this.dataGridView1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.fileLocationLabel);
            this.groupBox2.Controls.Add(this.chooseTrainingDataLabel);
            this.groupBox2.Controls.Add(this.predictAttributeNumLabel);
            this.groupBox2.Controls.Add(this.numClassesLabel);
            this.groupBox2.Controls.Add(this.browseButton);
            this.groupBox2.Location = new System.Drawing.Point(13, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(592, 73);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Training Information";
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
            // chooseTrainingDataLabel
            // 
            this.chooseTrainingDataLabel.AutoSize = true;
            this.chooseTrainingDataLabel.Location = new System.Drawing.Point(7, 21);
            this.chooseTrainingDataLabel.Name = "chooseTrainingDataLabel";
            this.chooseTrainingDataLabel.Size = new System.Drawing.Size(74, 13);
            this.chooseTrainingDataLabel.TabIndex = 9;
            this.chooseTrainingDataLabel.Text = "Training Data:";
            // 
            // predictAttributeNumLabel
            // 
            this.predictAttributeNumLabel.AutoSize = true;
            this.predictAttributeNumLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.predictAttributeNumLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.predictAttributeNumLabel.Location = new System.Drawing.Point(119, 48);
            this.predictAttributeNumLabel.Name = "predictAttributeNumLabel";
            this.predictAttributeNumLabel.Size = new System.Drawing.Size(167, 13);
            this.predictAttributeNumLabel.TabIndex = 8;
            this.predictAttributeNumLabel.Text = "We don\'t see any attributes.";
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
            // browseButton
            // 
            this.helpProvider1.SetHelpKeyword(this.browseButton, "TestKeyWord");
            this.helpProvider1.SetHelpString(this.browseButton, "TestString");
            this.browseButton.Location = new System.Drawing.Point(87, 16);
            this.browseButton.Name = "browseButton";
            this.helpProvider1.SetShowHelp(this.browseButton, true);
            this.browseButton.Size = new System.Drawing.Size(70, 23);
            this.browseButton.TabIndex = 0;
            this.browseButton.TabStop = false;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dontNormalizeInputDataCheckBox);
            this.groupBox3.Controls.Add(this.kValueTB);
            this.groupBox3.Controls.Add(this.dontNormalizeTrainingDataCheckBox);
            this.groupBox3.Controls.Add(this.kValueLabel);
            this.groupBox3.Location = new System.Drawing.Point(256, 156);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(349, 95);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "KNN Options";
            // 
            // dontNormalizeInputDataCheckBox
            // 
            this.dontNormalizeInputDataCheckBox.AutoSize = true;
            this.dontNormalizeInputDataCheckBox.Location = new System.Drawing.Point(6, 72);
            this.dontNormalizeInputDataCheckBox.Name = "dontNormalizeInputDataCheckBox";
            this.dontNormalizeInputDataCheckBox.Size = new System.Drawing.Size(127, 17);
            this.dontNormalizeInputDataCheckBox.TabIndex = 5;
            this.dontNormalizeInputDataCheckBox.Text = "Don\'t Normalize Input";
            this.dontNormalizeInputDataCheckBox.UseVisualStyleBackColor = true;
            // 
            // kValueTB
            // 
            this.kValueTB.Location = new System.Drawing.Point(57, 19);
            this.kValueTB.Name = "kValueTB";
            this.kValueTB.Size = new System.Drawing.Size(100, 20);
            this.kValueTB.TabIndex = 1;
            this.kValueTB.TextChanged += new System.EventHandler(this.kValueTB_TextChanged);
            // 
            // dontNormalizeTrainingDataCheckBox
            // 
            this.dontNormalizeTrainingDataCheckBox.AutoSize = true;
            this.dontNormalizeTrainingDataCheckBox.Location = new System.Drawing.Point(6, 49);
            this.dontNormalizeTrainingDataCheckBox.Name = "dontNormalizeTrainingDataCheckBox";
            this.dontNormalizeTrainingDataCheckBox.Size = new System.Drawing.Size(167, 17);
            this.dontNormalizeTrainingDataCheckBox.TabIndex = 2;
            this.dontNormalizeTrainingDataCheckBox.Text = "Don\'t Normalize Training Data";
            this.dontNormalizeTrainingDataCheckBox.UseVisualStyleBackColor = true;
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
            this.groupBox1.Size = new System.Drawing.Size(237, 304);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input";
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(217, 279);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // displayDataStep3
            // 
            this.displayDataStep3.BindingImage = global::KNearestNeighbor.Properties.Resources.orange_light_top;
            this.displayDataStep3.Controls.Add(this.groupBox7);
            this.displayDataStep3.Controls.Add(this.groupBox6);
            this.displayDataStep3.Controls.Add(this.label14);
            this.displayDataStep3.Controls.Add(this.label13);
            this.displayDataStep3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.displayDataStep3.Name = "displayDataStep3";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.groupBox9);
            this.groupBox7.Controls.Add(this.groupBox8);
            this.groupBox7.Location = new System.Drawing.Point(0, 72);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(615, 127);
            this.groupBox7.TabIndex = 18;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "View Data Output";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label8);
            this.groupBox9.Controls.Add(this.closestCompetitorDistanceLabel);
            this.groupBox9.Controls.Add(this.label6);
            this.groupBox9.Controls.Add(this.label2);
            this.groupBox9.Controls.Add(this.closestCompetitorNameLabel);
            this.groupBox9.Controls.Add(this.label4);
            this.groupBox9.Controls.Add(this.closestCompetitorClassLabel);
            this.groupBox9.Location = new System.Drawing.Point(216, 19);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(393, 100);
            this.groupBox9.TabIndex = 5;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Plot\'s Closest Competitor";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(369, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Choose an X and Y coords to view the closest competitors given that criteria.";
            // 
            // closestCompetitorDistanceLabel
            // 
            this.closestCompetitorDistanceLabel.AutoSize = true;
            this.closestCompetitorDistanceLabel.Location = new System.Drawing.Point(154, 82);
            this.closestCompetitorDistanceLabel.Name = "closestCompetitorDistanceLabel";
            this.closestCompetitorDistanceLabel.Size = new System.Drawing.Size(27, 13);
            this.closestCompetitorDistanceLabel.TabIndex = 5;
            this.closestCompetitorDistanceLabel.Text = "N/A";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(142, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Closest Competitor Distance:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Closest Competitor Name:";
            // 
            // closestCompetitorNameLabel
            // 
            this.closestCompetitorNameLabel.AutoSize = true;
            this.closestCompetitorNameLabel.Location = new System.Drawing.Point(154, 45);
            this.closestCompetitorNameLabel.Name = "closestCompetitorNameLabel";
            this.closestCompetitorNameLabel.Size = new System.Drawing.Size(27, 13);
            this.closestCompetitorNameLabel.TabIndex = 3;
            this.closestCompetitorNameLabel.Text = "N/A";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Closest Competitor Class: ";
            // 
            // closestCompetitorClassLabel
            // 
            this.closestCompetitorClassLabel.AutoSize = true;
            this.closestCompetitorClassLabel.Location = new System.Drawing.Point(154, 63);
            this.closestCompetitorClassLabel.Name = "closestCompetitorClassLabel";
            this.closestCompetitorClassLabel.Size = new System.Drawing.Size(27, 13);
            this.closestCompetitorClassLabel.TabIndex = 2;
            this.closestCompetitorClassLabel.Text = "N/A";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label19);
            this.groupBox8.Controls.Add(this.closestCompetitor);
            this.groupBox8.Location = new System.Drawing.Point(10, 19);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(200, 100);
            this.groupBox8.TabIndex = 4;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "K-NN Closest Competitor";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(5, 20);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(100, 13);
            this.label19.TabIndex = 0;
            this.label19.Text = "Closest Competitor: ";
            // 
            // closestCompetitor
            // 
            this.closestCompetitor.AutoSize = true;
            this.closestCompetitor.Location = new System.Drawing.Point(118, 20);
            this.closestCompetitor.Name = "closestCompetitor";
            this.closestCompetitor.Size = new System.Drawing.Size(27, 13);
            this.closestCompetitor.TabIndex = 2;
            this.closestCompetitor.Text = "N/A";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.showAllDistancesRadioButton);
            this.groupBox6.Controls.Add(this.showKDistancesRadioButton);
            this.groupBox6.Controls.Add(this.plotButton);
            this.groupBox6.Controls.Add(this.label15);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Controls.Add(this.chart1);
            this.groupBox6.Controls.Add(this.plotYComboBox);
            this.groupBox6.Controls.Add(this.plotXComboBox);
            this.groupBox6.Controls.Add(this.label17);
            this.groupBox6.Controls.Add(this.label18);
            this.groupBox6.Location = new System.Drawing.Point(0, 205);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(615, 472);
            this.groupBox6.TabIndex = 14;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Graph";
            // 
            // showAllDistancesRadioButton
            // 
            this.showAllDistancesRadioButton.AutoSize = true;
            this.showAllDistancesRadioButton.Location = new System.Drawing.Point(363, 37);
            this.showAllDistancesRadioButton.Name = "showAllDistancesRadioButton";
            this.showAllDistancesRadioButton.Size = new System.Drawing.Size(116, 17);
            this.showAllDistancesRadioButton.TabIndex = 16;
            this.showAllDistancesRadioButton.TabStop = true;
            this.showAllDistancesRadioButton.Text = "Show All Distances";
            this.showAllDistancesRadioButton.UseVisualStyleBackColor = true;
            // 
            // showKDistancesRadioButton
            // 
            this.showKDistancesRadioButton.AutoSize = true;
            this.showKDistancesRadioButton.Location = new System.Drawing.Point(363, 14);
            this.showKDistancesRadioButton.Name = "showKDistancesRadioButton";
            this.showKDistancesRadioButton.Size = new System.Drawing.Size(152, 17);
            this.showKDistancesRadioButton.TabIndex = 15;
            this.showKDistancesRadioButton.TabStop = true;
            this.showKDistancesRadioButton.Text = "Show K-Nearest Distances";
            this.showKDistancesRadioButton.UseVisualStyleBackColor = true;
            // 
            // plotButton
            // 
            this.plotButton.Location = new System.Drawing.Point(530, 18);
            this.plotButton.Name = "plotButton";
            this.plotButton.Size = new System.Drawing.Size(75, 23);
            this.plotButton.TabIndex = 14;
            this.plotButton.Text = "Plot";
            this.plotButton.UseVisualStyleBackColor = true;
            this.plotButton.Click += new System.EventHandler(this.plotButton_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(843, 23);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(27, 13);
            this.label15.TabIndex = 6;
            this.label15.Text = "N/A";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(733, 23);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(103, 13);
            this.label16.TabIndex = 5;
            this.label16.Text = "Nearest Competitor: ";
            // 
            // chart1
            // 
            chartArea1.AxisX.Interval = 0.05D;
            chartArea1.AxisX.IsStartedFromZero = false;
            chartArea1.AxisX.Maximum = 1D;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisY.Interval = 0.05D;
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.AxisY.Maximum = 1D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            legend1.Title = "Legend";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(6, 53);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(603, 399);
            this.chart1.TabIndex = 4;
            this.chart1.Text = "chart1";
            // 
            // plotYComboBox
            // 
            this.plotYComboBox.FormattingEnabled = true;
            this.plotYComboBox.Location = new System.Drawing.Point(235, 21);
            this.plotYComboBox.Name = "plotYComboBox";
            this.plotYComboBox.Size = new System.Drawing.Size(121, 21);
            this.plotYComboBox.TabIndex = 3;
            // 
            // plotXComboBox
            // 
            this.plotXComboBox.FormattingEnabled = true;
            this.plotXComboBox.Location = new System.Drawing.Point(52, 20);
            this.plotXComboBox.Name = "plotXComboBox";
            this.plotXComboBox.Size = new System.Drawing.Size(121, 21);
            this.plotXComboBox.TabIndex = 2;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(191, 23);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(38, 13);
            this.label17.TabIndex = 1;
            this.label17.Text = "Plot Y:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(7, 22);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(38, 13);
            this.label18.TabIndex = 0;
            this.label18.Text = "Plot X:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(289, 219);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(0, 13);
            this.label14.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(242, 219);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Class:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // errorProviderK
            // 
            this.errorProviderK.ContainerControl = this;
            // 
            // errorProviderAttributes
            // 
            this.errorProviderAttributes.ContainerControl = this;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // errorProviderPlot
            // 
            this.errorProviderPlot.ContainerControl = this;
            // 
            // wizardBindingSource
            // 
            this.wizardBindingSource.DataSource = typeof(KNearestNeighbor.Wizard);
            // 
            // Wizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(619, 721);
            this.Controls.Add(this.baseControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(635, 759);
            this.MinimumSize = new System.Drawing.Size(635, 759);
            this.Name = "Wizard";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "K-NN Algorithm Calculator";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Wizard_HelpButtonClicked);
            this.descriptionStep1.ResumeLayout(false);
            this.initialDataStep2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.displayDataStep3.ResumeLayout(false);
            this.displayDataStep3.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderAttributes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPlot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wizardBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private WizardBase.WizardControl baseControl;
        private WizardBase.IntermediateStep initialDataStep2;
        private WizardBase.IntermediateStep displayDataStep3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox dontNormalizeInputDataCheckBox;
        private System.Windows.Forms.TextBox kValueTB;
        private System.Windows.Forms.Label kValueLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox dontNormalizeTrainingDataCheckBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label numClassesLabel;
        private System.Windows.Forms.Label predictAttributeNumLabel;
        private System.Windows.Forms.Label chooseTrainingDataLabel;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label fileLocationLabel;
        private System.Windows.Forms.ErrorProvider errorProviderK;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ErrorProvider errorProviderAttributes;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Data.DataSet dataSet1;
        private System.Windows.Forms.BindingSource wizardBindingSource;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button plotButton;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ComboBox plotYComboBox;
        private System.Windows.Forms.ComboBox plotXComboBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.RichTextBox programDescriptionTB;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label closestCompetitor;
        protected internal WizardBase.StartStep descriptionStep1;
        private System.Windows.Forms.RichTextBox dataInitializationInstructionsTB;
        private System.Windows.Forms.ErrorProvider errorProviderPlot;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label closestCompetitorNameLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label closestCompetitorClassLabel;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label closestCompetitorDistanceLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton showAllDistancesRadioButton;
        private System.Windows.Forms.RadioButton showKDistancesRadioButton;
    }
}