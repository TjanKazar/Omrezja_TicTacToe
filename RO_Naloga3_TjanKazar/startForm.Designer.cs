namespace RO_Naloga3_TjanKazar
{
    partial class startForm
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
            btnServer = new Button();
            label1 = new Label();
            btnClient = new Button();
            SuspendLayout();
            // 
            // btnServer
            // 
            btnServer.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnServer.Location = new Point(326, 371);
            btnServer.Name = "btnServer";
            btnServer.Size = new Size(145, 48);
            btnServer.TabIndex = 1;
            btnServer.Text = "host game";
            btnServer.UseVisualStyleBackColor = true;
            btnServer.Click += btnServerClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.FlatStyle = FlatStyle.Popup;
            label1.Font = new Font("Comic Sans MS", 50F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.MenuHighlight;
            label1.Location = new Point(58, 50);
            label1.Name = "label1";
            label1.Size = new Size(433, 95);
            label1.TabIndex = 2;
            label1.Text = "Tic-tac-toe !";
            label1.Click += label1_Click;
            // 
            // btnClient
            // 
            btnClient.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnClient.Location = new Point(58, 371);
            btnClient.Name = "btnClient";
            btnClient.Size = new Size(145, 48);
            btnClient.TabIndex = 3;
            btnClient.Text = "join game";
            btnClient.UseVisualStyleBackColor = true;
            btnClient.Click += btnClientClick;
            // 
            // startForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(557, 495);
            Controls.Add(btnClient);
            Controls.Add(label1);
            Controls.Add(btnServer);
            Name = "startForm";
            Text = "Tic-Tac-toe!";
            Load += startForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnServer;
        private Label label1;
        private Button btnClient;
    }
}