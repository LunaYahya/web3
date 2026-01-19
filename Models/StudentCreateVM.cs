using System.Collections.Generic;

namespace E_learninig.Models
{
	public class StudentCreateVM
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;

		public List<int> SelectedCourseIds { get; set; } = new List<int>();
	}
}
