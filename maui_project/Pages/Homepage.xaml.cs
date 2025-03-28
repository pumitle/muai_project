using System.Diagnostics;
using maui_project.Model;
using maui_project.ViewModel;

namespace maui_project.Pages
{
    [QueryProperty(nameof(CurrentUser), "CurrentUser")]
    public partial class Homepage : ContentPage
    {
        public User CurrentUser { get; set; }
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
        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);

            if (CurrentUser != null)
            {
                var viewModel = (HomeViewModel)BindingContext;
                viewModel.SetCurrentUser(CurrentUser.UserId);
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
        private async void OnButton1Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(RegPage));
        }
        private async void OnButton2Clicked(object sender, EventArgs e)
        {
            // await Shell.Current.GoToAsync(nameof(RegPage));
        }
        private async void OnButton3Clicked(object sender, EventArgs e)
        {
            bool isConfirmed = await DisplayAlert(
            "ยืนยันการออกจากระบบ", // Title of the alert
            "คุณต้องการออกจากระบบหรือไม่?", // Message in the alert
            "ออกจากระบบ", // Confirm button text
            "ยกเลิก"  // Cancel button text
        );

            if (isConfirmed)
            {
                // Perform logout actions, e.g., clearing session or navigating to login page
                Debug.WriteLine("Logged out successfully!");

                // Navigate to login page (example)
                await Shell.Current.GoToAsync(nameof(LoginPage));
            }
            else
            {
                Debug.WriteLine("Logout canceled.");
            }
        }

    }
}
