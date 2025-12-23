namespace Multi_Language_Translate
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            
            // Label for source language
            Label lblSourceLanguage = new Label
            {
                Text = "Kaynak Dil:",
                Location = new Point(20, 20),
                AutoSize = true
            };

            // ComboBox for source language selection
            ComboBoxSourceLanguage = new ComboBox
            {
                Name = "ComboBoxSourceLanguage",
                Location = new Point(20, 50),
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // Label for target language
            Label lblTargetLanguage = new Label
            {
                Text = "Hedef Dil:",
                Location = new Point(200, 20),
                AutoSize = true
            };

            // ComboBox for target language selection
            ComboBoxTargetLanguage = new ComboBox
            {
                Name = "ComboBoxTargetLanguage",
                Location = new Point(200, 50),
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // Label for source text
            Label lblSourceText = new Label
            {
                Text = "Çevrilecek Metin:",
                Location = new Point(20, 100),
                AutoSize = true
            };

            // TextBox for source text
            TextBoxSourceText = new TextBox
            {
                Name = "TextBoxSourceText",
                Location = new Point(20, 130),
                Width = 750,
                Height = 100,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            // Label for translated text
            Label lblTranslatedText = new Label
            {
                Text = "Çevrilen Metin:",
                Location = new Point(20, 250),
                AutoSize = true
            };

            // TextBox for translated text
            TextBoxTranslatedText = new TextBox
            {
                Name = "TextBoxTranslatedText",
                Location = new Point(20, 280),
                Width = 750,
                Height = 100,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true
            };

            // Translate button
            ButtonTranslate = new Button
            {
                Text = "Çevir",
                Location = new Point(20, 400),
                Width = 100,
                Height = 40,
                Font = new Font(Font.FontFamily, 10, FontStyle.Bold)
            };

            // Status label
            LabelStatus = new Label
            {
                Name = "LabelStatus",
                Text = "Hazýr",
                Location = new Point(150, 410),
                AutoSize = true,
                ForeColor = Color.Green
            };

            // Form properties
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 470);
            Text = "Çoklu Dil Çevirisi";
            StartPosition = FormStartPosition.CenterScreen;

            // Add controls to form
            Controls.Add(lblSourceLanguage);
            Controls.Add(ComboBoxSourceLanguage);
            Controls.Add(lblTargetLanguage);
            Controls.Add(ComboBoxTargetLanguage);
            Controls.Add(lblSourceText);
            Controls.Add(TextBoxSourceText);
            Controls.Add(lblTranslatedText);
            Controls.Add(TextBoxTranslatedText);
            Controls.Add(ButtonTranslate);
            Controls.Add(LabelStatus);
        }

        #endregion

        public ComboBox? ComboBoxSourceLanguage { get; set; }
        public ComboBox? ComboBoxTargetLanguage { get; set; }
        public TextBox? TextBoxSourceText { get; set; }
        public TextBox? TextBoxTranslatedText { get; set; }
        public Button? ButtonTranslate { get; set; }
        public Label? LabelStatus { get; set; }
    }
}
