using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

    [JsonIgnore]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    [NotMapped]
    [JsonPropertyName("count")]
    public int Count { get; set; }

}
