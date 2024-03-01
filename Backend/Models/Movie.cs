namespace Backend.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        //List of Actor
        public ICollection<Person> Actors { get; set; }
        public string Language { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CoverImage { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
