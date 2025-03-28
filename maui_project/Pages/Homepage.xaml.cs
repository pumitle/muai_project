using maui_project.ViewModel;

namespace maui_project.Pages
{
    public partial class Homepage : ContentPage
    {
        public Homepage()
        {
            InitializeComponent();

            var userId = App.CurrentUserId;

            if (!string.IsNullOrEmpty(userId))
            {
                var viewModel = (HomeViewModel)BindingContext;
                viewModel.SetCurrentUser(userId);
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void OnPreviousSemesterSelected(object sender, EventArgs e)
        {
            var viewModel = (HomeViewModel)BindingContext;
            viewModel.OnPreviousSemesterSelected();
        }
    }
}
