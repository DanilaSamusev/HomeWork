namespace Hello.WinFormsApp
{
    partial class Form1
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
            this.UserNameInput = new System.Windows.Forms.TextBox();
            this.RequestLabel = new System.Windows.Forms.Label();
            this.UserIntroductionLabel = new System.Windows.Forms.Label();
            this.EnterUserNameButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UserNameInput
            // 
            this.UserNameInput.Location = new System.Drawing.Point(333, 167);
            this.UserNameInput.Name = "UserNameInput";
            this.UserNameInput.Size = new System.Drawing.Size(100, 20);
            this.UserNameInput.TabIndex = 0;
            // 
            // RequestLabel
            // 
            this.RequestLabel.AutoSize = true;
            this.RequestLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RequestLabel.Location = new System.Drawing.Point(314, 142);
            this.RequestLabel.Name = "RequestLabel";
            this.RequestLabel.Size = new System.Drawing.Size(142, 22);
            this.RequestLabel.TabIndex = 1;
            this.RequestLabel.Text = "Enter user name";
            // 
            // UserIntroductionLabel
            // 
            this.UserIntroductionLabel.AutoSize = true;
            this.UserIntroductionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UserIntroductionLabel.Location = new System.Drawing.Point(252, 227);
            this.UserIntroductionLabel.Name = "UserIntroductionLabel";
            this.UserIntroductionLabel.Size = new System.Drawing.Size(0, 20);
            this.UserIntroductionLabel.TabIndex = 2;
            // 
            // EnterUserNameButton
            // 
            this.EnterUserNameButton.Location = new System.Drawing.Point(346, 193);
            this.EnterUserNameButton.Name = "EnterUserNameButton";
            this.EnterUserNameButton.Size = new System.Drawing.Size(75, 23);
            this.EnterUserNameButton.TabIndex = 3;
            this.EnterUserNameButton.Text = "Enter";
            this.EnterUserNameButton.UseVisualStyleBackColor = true;
            this.EnterUserNameButton.Click += new System.EventHandler(this.EnterUserNameButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 464);
            this.Controls.Add(this.EnterUserNameButton);
            this.Controls.Add(this.UserIntroductionLabel);
            this.Controls.Add(this.RequestLabel);
            this.Controls.Add(this.UserNameInput);
            this.Name = "Form1";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox UserNameInput;
        private System.Windows.Forms.Label RequestLabel;
        private System.Windows.Forms.Label UserIntroductionLabel;
        private System.Windows.Forms.Button EnterUserNameButton;
    }
}

