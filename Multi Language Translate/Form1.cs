using Multi_Language_Translate.Interfaces;

namespace Multi_Language_Translate
{
    public partial class Form1 : Form
    {
        private readonly ITranslator _translator;

        public Form1(ITranslator translator)
        {
            _translator = translator ?? throw new ArgumentNullException(nameof(translator));
            InitializeComponent();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await InitializeLanguagesAsync();
        }

        private void InitializeComponents()
        {
            if (ButtonTranslate != null)
            {
                ButtonTranslate.Click += ButtonTranslate_Click;
            }
        }

        private async Task InitializeLanguagesAsync()
        {
            try
            {
                UpdateStatus("Diller yükleniyor...", Color.Blue);

                var languages = await _translator.GetSupportedLanguagesAsync();

                if (ComboBoxSourceLanguage != null && ComboBoxTargetLanguage != null)
                {
                    ComboBoxSourceLanguage.DataSource = new BindingSource(languages, null);
                    ComboBoxSourceLanguage.DisplayMember = "Value";
                    ComboBoxSourceLanguage.ValueMember = "Key";

                    ComboBoxTargetLanguage.DataSource = new BindingSource(languages, null);
                    ComboBoxTargetLanguage.DisplayMember = "Value";
                    ComboBoxTargetLanguage.ValueMember = "Key";

                    // Set default languages
                    if (languages.ContainsKey("en"))
                        ComboBoxSourceLanguage.SelectedValue = "en";
                    if (languages.ContainsKey("tr"))
                        ComboBoxTargetLanguage.SelectedValue = "tr";
                }

                UpdateStatus("Hazýr", Color.Green);
                InitializeComponents();
            }
            catch (Exception ex)
            {
                UpdateStatus($"Hata: {ex.Message}", Color.Red);
                MessageBox.Show($"Diller yüklenirken hata oluþtu:\n{ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ButtonTranslate_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxSourceText?.Text))
            {
                MessageBox.Show("Lütfen çevrilecek metni girin.",
                    "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await TranslateTextAsync();
        }

        private async Task TranslateTextAsync()
        {
            try
            {
                if (ComboBoxSourceLanguage?.SelectedValue == null ||
                    ComboBoxTargetLanguage?.SelectedValue == null)
                {
                    MessageBox.Show("Lütfen dilleri seçin.",
                        "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                UpdateStatus("Çeviriliyor...", Color.Blue);

                if (ButtonTranslate != null)
                    ButtonTranslate.Enabled = false;

                string sourceLanguage = ComboBoxSourceLanguage.SelectedValue.ToString() ?? "en";
                string targetLanguage = ComboBoxTargetLanguage.SelectedValue.ToString() ?? "tr";
                string sourceText = TextBoxSourceText?.Text ?? "";

                string translatedText = await _translator.TranslateAsync(
                    sourceText,
                    sourceLanguage,
                    targetLanguage
                );

                if (TextBoxTranslatedText != null)
                {
                    TextBoxTranslatedText.Text = translatedText;
                }

                UpdateStatus("Çeviri tamamlandý", Color.Green);
            }
            catch (Exception ex)
            {
                UpdateStatus($"Hata: {ex.Message}", Color.Red);
                MessageBox.Show($"Çeviri sýrasýnda hata oluþtu:\n{ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (ButtonTranslate != null)
                    ButtonTranslate.Enabled = true;
            }
        }

        private void UpdateStatus(string message, Color color)
        {
            if (LabelStatus != null)
            {
                LabelStatus.Text = message;
                LabelStatus.ForeColor = color;
            }
        }
    }
}
