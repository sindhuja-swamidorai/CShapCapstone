using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BookStore.Entities;

public class Author
{
    [JsonPropertyName("author_id")]
    public int AuthorId { get; set; }

    [JsonPropertyName("name")]
    public string AuthorName { get; set; } = null!;

    [JsonPropertyName("biography")]
    public string? Biography { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
