using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using maui_project.Model;
using maui_project1.Model;
namespace maui_project.ViewModel;
using Newtonsoft.Json;
public class RegViewModel : ObservableObject
{
	private List<Croustreq> _courseDetails;

	// Bind this property to the UI CollectionView
	public List<Croustreq> CourseDetails
	{
		get => _courseDetails;
		set => SetProperty(ref _courseDetails, value); // Notifies UI when CourseDetails changes
	}

	public RegViewModel()
	{
		LoadCoursesAsync(); // Load courses on initialization
	}

	private async Task LoadCoursesAsync()
	{
		var courseDetails = await LoadCourseDetailsAsync();
		// Bind the loaded course details to the CourseDetails property
		CourseDetails = courseDetails;
	}

	private async Task<List<Croustreq>> LoadCourseDetailsAsync()
	{
		try
		{
			// Open the file from the app package
			using var stream = await FileSystem.OpenAppPackageFileAsync("courses.json");
			using var reader = new StreamReader(stream);
			var json = await reader.ReadToEndAsync();

			Debug.WriteLine(json); // Check the loaded JSON data

			// Deserialize JSON into a List<Croustreq> object
			var data = JsonConvert.DeserializeObject<List<Croustreq>>(json);

			// Check if data is valid and print course details
			if (data != null)
			{
				foreach (var course in data)
				{
					Debug.WriteLine($"CourseId: {course.CourseId}");
					Debug.WriteLine($"CourseName: {course.CourseName}");
					Debug.WriteLine($"Description: {course.CourseDescription}");
					Debug.WriteLine($"CreditHours: {course.CreditHours}");
					Debug.WriteLine($"Department: {course.Department}");
					Debug.WriteLine($"Semester: {course.Semester}");
					Debug.WriteLine($"AvailableSeats: {course.AvailableSeats}");
					Debug.WriteLine("------------");
				}
			}
			else
			{
				Debug.WriteLine("No course data found.");
			}

			return data; // Return the deserialized List<Croustreq>
		}
		catch (Exception ex)
		{
			Debug.WriteLine($"Error reading JSON: {ex.Message}");
			return null; // Return null if error occurs
		}
	}
}
