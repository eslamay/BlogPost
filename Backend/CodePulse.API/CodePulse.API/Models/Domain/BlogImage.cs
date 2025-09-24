namespace CodePulse.API.Models.Domain
{
	public class BlogImage
	{
		public Guid Id { get; set; }
		public string fileName { get; set; }
		public string fileExtension { get; set; }
		public string Title { get; set; }
		public string Url { get; set; }
		public DateTime DateCreated { get; set; }

	}
}
