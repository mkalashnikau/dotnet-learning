namespace HelloWorldWinFormsApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelMessage = new Label();
            textBoxUsername = new TextBox();
            SuspendLayout();
            // 
            // labelMessage
            // 
            labelMessage.AutoSize = true;
            labelMessage.Location = new Point(17, 21);
            labelMessage.Name = "labelMessage";
            labelMessage.Size = new Size(314, 20);
            labelMessage.TabIndex = 0;
            labelMessage.Text = "Please, enter username and click 'Enter' button";
            // 
            // textBoxUsername
            // 
            textBoxUsername.Location = new Point(17, 56);
            textBoxUsername.Name = "textBoxUsername";
            textBoxUsername.Size = new Size(314, 27);
            textBoxUsername.TabIndex = 1;
            textBoxUsername.KeyDown += textBoxUsername_KeyDown;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(427, 163);
            Controls.Add(textBoxUsername);
            Controls.Add(labelMessage);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelMessage;
        private TextBox textBoxUsername;
    }
}
