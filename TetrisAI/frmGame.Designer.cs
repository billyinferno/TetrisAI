namespace TetrisAI
{
    partial class frmGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGame));
            this.imgTetris = new System.Windows.Forms.ImageList(this.components);
            this.btnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // imgTetris
            // 
            this.imgTetris.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgTetris.ImageStream")));
            this.imgTetris.TransparentColor = System.Drawing.Color.Transparent;
            this.imgTetris.Images.SetKeyName(0, "bg");
            this.imgTetris.Images.SetKeyName(1, "1");
            this.imgTetris.Images.SetKeyName(2, "2");
            this.imgTetris.Images.SetKeyName(3, "3");
            this.imgTetris.Images.SetKeyName(4, "4");
            this.imgTetris.Images.SetKeyName(5, "5");
            this.imgTetris.Images.SetKeyName(6, "6");
            this.imgTetris.Images.SetKeyName(7, "7");
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(226, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.TabStop = false;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // frmGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 658);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmGame";
            this.Text = " ";
            this.Load += new System.EventHandler(this.frmGame_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmGame_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imgTetris;
        private System.Windows.Forms.Button btnStart;
    }
}

