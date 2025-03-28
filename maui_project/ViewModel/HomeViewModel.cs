using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maui_project.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace maui_project.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Course> currentSemesterCourses = new ObservableCollection<Course>();

        [ObservableProperty]
        private ObservableCollection<Course> selectedPreviousSemesterCourses = new ObservableCollection<Course>();

        [ObservableProperty]
        private ObservableCollection<string> previousSemesters = new ObservableCollection<string>();

        [ObservableProperty]
        private string selectedPreviousSemester;

        private List<User> users = new List<User>();

        [ObservableProperty]
        private User? currentUser;

        // โหลดข้อมูลผู้ใช้
        public HomeViewModel()
        {
            LoadUsers();  // โหลดข้อมูลผู้ใช้จากไฟล์
        }

        private async void LoadUsers()
        {
            try
            {
                var stream = await FileSystem.OpenAppPackageFileAsync("users.json");
                using (var reader = new StreamReader(stream))
                {
                    string json = await reader.ReadToEndAsync();
                    users = User.FromJson(json);
                }

                SetCurrentUser(App.CurrentUserId); // ใช้ CurrentUserId ที่ตั้งใน App.xaml.cs เพื่อเรียก SetCurrentUser
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("Error loading users: " + ex.Message);
            }

            PreviousSemesters.Insert(0, "เลือกเทอมที่ต้องการดู");
        }

        public void SetCurrentUser(string userId)
        {
            var matchedUsers = users.Where(u => u.UserId == userId).ToList();
            CurrentUser = matchedUsers.FirstOrDefault();

            if (CurrentUser != null)
            {
                // ✅ แสดงข้อมูลผู้ใช้
                System.Diagnostics.Debug.Print($"ชื่อ: {CurrentUser.Name}");
                System.Diagnostics.Debug.Print($"อีเมล: {CurrentUser.Email}");
                System.Diagnostics.Debug.Print($"คณะ: {CurrentUser.Faculty}");
                System.Diagnostics.Debug.Print($"ปีการศึกษา: {CurrentUser.Year}");

                var currentSemester = CurrentUser.Enrollment
                    .OrderByDescending(e => e.AcademicYear)
                    .ThenByDescending(e => e.Semester)
                    .FirstOrDefault();

                if (currentSemester != null)
                {
                    CurrentSemesterCourses = new ObservableCollection<Course>(currentSemester.Courses);
                }

                var previousSemestersList = CurrentUser.Enrollment
                    .OrderByDescending(e => e.AcademicYear)
                    .ThenByDescending(e => e.Semester)
                    .Skip(1)
                    .ToList();

                if (previousSemestersList.Any())
                {
                    PreviousSemesters.Clear();
                    foreach (var semester in previousSemestersList)
                    {
                        PreviousSemesters.Add($"เทอม {semester.Semester}");
                    }
                }
            }
        }


        [RelayCommand]
        public void OnPreviousSemesterSelected()
        {
            if (selectedPreviousSemester != null)
            {
                var selectedSemester = currentUser?.Enrollment
                    .FirstOrDefault(e => $"เทอม {e.Semester}" == selectedPreviousSemester);

                if (selectedSemester != null)
                {
                    SelectedPreviousSemesterCourses = new ObservableCollection<Course>(selectedSemester.Courses);
                }
            }
        }
    }
}
