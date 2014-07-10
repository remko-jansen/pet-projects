namespace Stitch
{
    partial class Main
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxImage1 = new System.Windows.Forms.TextBox();
            this.textBoxImage2 = new System.Windows.Forms.TextBox();
            this.buttonBrowseImage1 = new System.Windows.Forms.Button();
            this.buttonBrowseImage2 = new System.Windows.Forms.Button();
            this.buttonStitch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxOutPutImage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Image 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Image 2";
            // 
            // textBoxImage1
            // 
            this.textBoxImage1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxImage1.Location = new System.Drawing.Point(65, 13);
            this.textBoxImage1.Name = "textBoxImage1";
            this.textBoxImage1.Size = new System.Drawing.Size(534, 20);
            this.textBoxImage1.TabIndex = 2;
            this.textBoxImage1.TextChanged += new System.EventHandler(this.textBoxImage1_TextChanged);
            // 
            // textBoxImage2
            // 
            this.textBoxImage2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxImage2.Location = new System.Drawing.Point(65, 40);
            this.textBoxImage2.Name = "textBoxImage2";
            this.textBoxImage2.Size = new System.Drawing.Size(534, 20);
            this.textBoxImage2.TabIndex = 3;
            this.textBoxImage2.TextChanged += new System.EventHandler(this.textBoxImage2_TextChanged);
            // 
            // buttonBrowseImage1
            // 
            this.buttonBrowseImage1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseImage1.Location = new System.Drawing.Point(605, 11);
            this.buttonBrowseImage1.Name = "buttonBrowseImage1";
            this.buttonBrowseImage1.Size = new System.Drawing.Size(25, 23);
            this.buttonBrowseImage1.TabIndex = 4;
            this.buttonBrowseImage1.Text = "...";
            this.buttonBrowseImage1.UseVisualStyleBackColor = true;
            this.buttonBrowseImage1.Click += new System.EventHandler(this.buttonBrowseImage1_Click);
            // 
            // buttonBrowseImage2
            // 
            this.buttonBrowseImage2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseImage2.Location = new System.Drawing.Point(605, 38);
            this.buttonBrowseImage2.Name = "buttonBrowseImage2";
            this.buttonBrowseImage2.Size = new System.Drawing.Size(25, 23);
            this.buttonBrowseImage2.TabIndex = 5;
            this.buttonBrowseImage2.Text = "...";
            this.buttonBrowseImage2.UseVisualStyleBackColor = true;
            this.buttonBrowseImage2.Click += new System.EventHandler(this.buttonBrowseImage2_Click);
            // 
            // buttonStitch
            // 
            this.buttonStitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStitch.Location = new System.Drawing.Point(555, 97);
            this.buttonStitch.Name = "buttonStitch";
            this.buttonStitch.Size = new System.Drawing.Size(75, 23);
            this.buttonStitch.TabIndex = 6;
            this.buttonStitch.Text = "Stitch";
            this.buttonStitch.UseVisualStyleBackColor = true;
            this.buttonStitch.Click += new System.EventHandler(this.buttonStitch_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Output";
            // 
            // textBoxOutPutImage
            // 
            this.textBoxOutPutImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutPutImage.Location = new System.Drawing.Point(65, 66);
            this.textBoxOutPutImage.Name = "textBoxOutPutImage";
            this.textBoxOutPutImage.Size = new System.Drawing.Size(534, 20);
            this.textBoxOutPutImage.TabIndex = 8;
            // 
            // Main
            // 
            this.AcceptButton = this.buttonStitch;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 132);
            this.Controls.Add(this.textBoxOutPutImage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonStitch);
            this.Controls.Add(this.buttonBrowseImage2);
            this.Controls.Add(this.buttonBrowseImage1);
            this.Controls.Add(this.textBoxImage2);
            this.Controls.Add(this.textBoxImage1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(658, 170);
            this.Name = "Main";
            this.Text = "Image Stitcher";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Main_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Main_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxImage1;
        private System.Windows.Forms.TextBox textBoxImage2;
        private System.Windows.Forms.Button buttonBrowseImage1;
        private System.Windows.Forms.Button buttonBrowseImage2;
        private System.Windows.Forms.Button buttonStitch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxOutPutImage;
    }
}

