
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maui_project.Pages;
using maui_project.Model;

namespace maui_project.ViewModel;

public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    string email = "";

    [ObservableProperty]
    string password = "";

    private List<User> users = new();

    public LoginViewModel()
    {
        LoadUsers();
    }

    private async void LoadUsers()
    {
        try
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("users.json");
            using var reader = new StreamReader(stream);
            string json = await reader.ReadToEndAsync();

            users = User.FromJson(json);
            System.Diagnostics.Debug.Print($"Loaded {users.Count} users.");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Print("Error loading users: " + ex.Message);
        }
    }



    [RelayCommand]
    async Task Login()
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            await Shell.Current.DisplayAlert("แจ้งเตือน", "กรุณากรอกอีเมลและรหัสผ่าน", "ตกลง");
            return;
        }

        System.Diagnostics.Debug.Print($"Input Email: {email}");
        System.Diagnostics.Debug.Print($"Input Password: {password}");

        foreach (var u in users)
        {
            System.Diagnostics.Debug.Print($"Checking {u.Email} with {u.Password}");
        }

        var user = users.FirstOrDefault(u => u.Email.Trim() == email.Trim() && u.Password.Trim() == password.Trim());

        if (user == null)
        {
            await Shell.Current.DisplayAlert("แจ้งเตือน", "อีเมลหรือรหัสผ่านไม่ถูกต้อง", "ตกลง");
            return;
        }

        // ✅ กำหนด CurrentUserId หลังจากล็อกอินสำเร็จ
        App.CurrentUserId = user.UserId;
        System.Diagnostics.Debug.Print("กำหนดค่า CurrentUserId: " + App.CurrentUserId);

        // ✅ Navigate to Homepage with user data
        var navParams = new Dictionary<string, object>
    {
        { "CurrentUser", user }
    };

        // ✅ เปลี่ยนหน้าไป Homepage และตรวจสอบค่าที่ส่งไป
        // await Shell.Current.GoToAsync(nameof(Homepage));
        await Shell.Current.GoToAsync(nameof(Homepage), navParams);
    }


}
