namespace Frontend
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        #region //MENU
        // File Commands
        private void OpenCommand(object sender, EventArgs e) { }
        private void ExitCommand(object sender, EventArgs e) { }
        private void SaveCommand(object sender, EventArgs e) { }
        private void EditCommand(object sender, EventArgs e) { }
        // Appearance Editors
        private void DarkMode(object sender, EventArgs e) { }
        private void LightMode(object sender, EventArgs e) { }  
        // Debug Controls
        private void RunCommand(object sender, EventArgs e) { } 
        private void DebugCommand(object sender, EventArgs e) { }
        #endregion

        #region //Text
        //Text Fillers
        private void OnEditorTextChanged(object sender, EventArgs e) { }
        private void OnEditorCompleted(object sender, EventArgs e) { }
        #endregion

        #region //Action

        #endregion

    }
}