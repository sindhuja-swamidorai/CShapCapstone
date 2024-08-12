using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookStore.Entities;

public class Genre
{
    [JsonPropertyName("genre_id")]
    public int GenreId { get; set; }

    [JsonPropertyName("genre_name")]
    public string GenreName { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    [NotMapped]
    [JsonPropertyName("count")]
    public int Count { get; set; } 

}
