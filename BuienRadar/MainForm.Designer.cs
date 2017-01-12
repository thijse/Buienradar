namespace BuienRadar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonFetchRainfall = new System.Windows.Forms.Button();
            this.listBoxRainFallData = new System.Windows.Forms.ListBox();
            this.textBoxLocation = new System.Windows.Forms.TextBox();
            this.textBoxLattitude = new System.Windows.Forms.TextBox();
            this.textBoxLongitude = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonLookup = new System.Windows.Forms.Button();
            this.buttonFetchWeatherStationData = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // buttonFetchRainfall
            // 
            this.buttonFetchRainfall.Location = new System.Drawing.Point(12, 88);
            this.buttonFetchRainfall.Name = "buttonFetchRainfall";
            this.buttonFetchRainfall.Size = new System.Drawing.Size(113, 36);
            this.buttonFetchRainfall.TabIndex = 0;
            this.buttonFetchRainfall.Text = " Fetch rainfall data";
            this.buttonFetchRainfall.UseVisualStyleBackColor = true;
            this.buttonFetchRainfall.Click += new System.EventHandler(this.ButtonFetchRainfallClick);
            // 
            // listBoxRainFallData
            // 
            this.listBoxRainFallData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxRainFallData.FormattingEnabled = true;
            this.listBoxRainFallData.Location = new System.Drawing.Point(12, 135);
            this.listBoxRainFallData.Name = "listBoxRainFallData";
            this.listBoxRainFallData.Size = new System.Drawing.Size(506, 355);
            this.listBoxRainFallData.TabIndex = 1;
            // 
            // textBoxLocation
            // 
            this.textBoxLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLocation.Location = new System.Drawing.Point(66, 15);
            this.textBoxLocation.Name = "textBoxLocation";
            this.textBoxLocation.Size = new System.Drawing.Size(371, 20);
            this.textBoxLocation.TabIndex = 2;
            this.textBoxLocation.Text = "Eindhoven";
            // 
            // textBoxLattitude
            // 
            this.textBoxLattitude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLattitude.Location = new System.Drawing.Point(66, 53);
            this.textBoxLattitude.Name = "textBoxLattitude";
            this.textBoxLattitude.Size = new System.Drawing.Size(181, 20);
            this.textBoxLattitude.TabIndex = 3;
            // 
            // textBoxLongitude
            // 
            this.textBoxLongitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLongitude.Location = new System.Drawing.Point(364, 53);
            this.textBoxLongitude.Name = "textBoxLongitude";
            this.textBoxLongitude.Size = new System.Drawing.Size(154, 20);
            this.textBoxLongitude.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Location";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Latitude";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(304, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Longitude";
            // 
            // buttonLookup
            // 
            this.buttonLookup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLookup.Location = new System.Drawing.Point(443, 15);
            this.buttonLookup.Name = "buttonLookup";
            this.buttonLookup.Size = new System.Drawing.Size(75, 21);
            this.buttonLookup.TabIndex = 8;
            this.buttonLookup.Text = "Lookup";
            this.buttonLookup.UseVisualStyleBackColor = true;
            this.buttonLookup.Click += new System.EventHandler(this.ButtonLookupClick);
            // 
            // buttonFetchWeatherStationData
            // 
            this.buttonFetchWeatherStationData.Location = new System.Drawing.Point(140, 88);
            this.buttonFetchWeatherStationData.Name = "buttonFetchWeatherStationData";
            this.buttonFetchWeatherStationData.Size = new System.Drawing.Size(113, 36);
            this.buttonFetchWeatherStationData.TabIndex = 9;
            this.buttonFetchWeatherStationData.Text = " Fetch weather station data";
            this.buttonFetchWeatherStationData.UseVisualStyleBackColor = true;
            this.buttonFetchWeatherStationData.Click += new System.EventHandler(this.ButtonFetchWeatherStationDataClick);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 509);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Data courtesy of buienradar";
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(450, 509);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(68, 13);
            this.linkLabel1.TabIndex = 11;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "buienradar.nl";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 531);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonFetchWeatherStationData);
            this.Controls.Add(this.buttonLookup);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxLongitude);
            this.Controls.Add(this.textBoxLattitude);
            this.Controls.Add(this.textBoxLocation);
            this.Controls.Add(this.listBoxRainFallData);
            this.Controls.Add(this.buttonFetchRainfall);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "BuienRadar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonFetchRainfall;
        private System.Windows.Forms.ListBox listBoxRainFallData;
        private System.Windows.Forms.TextBox textBoxLocation;
        private System.Windows.Forms.TextBox textBoxLattitude;
        private System.Windows.Forms.TextBox textBoxLongitude;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonLookup;
        private System.Windows.Forms.Button buttonFetchWeatherStationData;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

