namespace CodePulse.API.Models.DTO
{
	public class BlogImageDto
	{
		public Guid Id { get; set; }
		public string fileName { get; set; }
		public string fileExtension { get; set; }
		public string Title { get; set; }
		public string Url { get; set; }
		public DateTime DateCreated { get; set; }
	}
}
