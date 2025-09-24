namespace CodePulse.API.Models.DTO
{
	public class UpdateBlogPostRequestDto
	{
		public string Name { get; set; }
		public string ShortDescription { get; set; }
		public string Content { get; set; }
		public string FeaturedImageUrl { get; set; }
		public string UrlHandle { get; set; }
		public DateTime PublishedDate { get; set; }
		public string Author { get; set; }
		public bool IsVisible { get; set; }

		public List<Guid> CategoryId { get; set; } = new List<Guid>();
	}
}
