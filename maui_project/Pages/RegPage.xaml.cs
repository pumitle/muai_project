using System.Diagnostics;
using maui_project.ViewModel;
using maui_project1.Model;
using Newtonsoft.Json;
namespace maui_project.Pages;

public partial class RegPage : ContentPage
{
	public Croustreq CourseDetails { get; set; }
	public RegPage()
	{
		InitializeComponent();
		BindingContext = new RegViewModel();
	}
	// ฟังก์ชันเพื่อโหลดข้อมูลคอร์สจากไฟล์ JSON  // ฟังก์ชันเพื่อโหลดข้อมูลคอร์สจากไฟล์ JSON
	// public async Task<List<Croustreq>> LoadCoursesFromJsonAsync()
	// {
	// 	using var stream = await FileSystem.OpenAppPackageFileAsync("courses.json");
	// 	using var reader = new StreamReader(stream);
	// 	var json = await reader.ReadToEndAsync();

	// 	var data = JsonConvert.DeserializeObject<Croustreq>(json);

	// 	if (data?.Courses != null && data.Courses.Any())
	// 	{
	// 		AllCourses = new List<Course>(data.Courses); // Store all courses
	// 		FilteredCourses.Clear();  // Clear existing filtered courses
	// 		foreach (var course in AllCourses)
	// 		{
	// 			FilteredCourses.Add(course); // Add courses to FilteredCourses
	// 		}
	// 	}

	// 	return AllCourses;
	// }
	private async void OnAddCourseClicked(object sender, EventArgs e)
	{
		bool isConfirmed = await DisplayAlert(
		"ยืนยันการถเพิ่มวิชา", // Title of the alert
		"คุณต้องการเพิ่มวิชานี้จริงๆ หรือไม่?", // Message in the alert
		"ยืนยัน", // Confirm button text
		"ยกเลิก"  // Cancel button text
	);

		if (isConfirmed)
		{
			// If confirmed, proceed with the withdrawal logic
			Debug.WriteLine("Withdrawal confirmed!");

			// You can trigger the withdrawal functionality here (e.g., calling ViewModel's method)
			// Example: await WithdrawalFunction();

		}
		else
		{
			// Handle the cancel action
			Debug.WriteLine("Withdrawal canceled.");
		}
	}
	private async void OnSearchTextChanged(object sender, EventArgs e)
	{
		// await Shell.Current.GoToAsync(nameof(RegPage));
	}


}