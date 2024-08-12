using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace BookStore.Entities;

public class Book
{
    [JsonPropertyName("book_id")]
    public int BookId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;

    [JsonPropertyName("price")]
    public double? Price { get; set; }

    [JsonPropertyName("publication_date")]
    public DateTime? PublicationDate { get; set; }

    [JsonPropertyName("author_id")]
    public int? AuthorId { get; set; }

    [JsonPropertyName("genre_id")]
    public int? GenreId { get; set; }

    [NotMapped]
    [JsonPropertyName("author_name")]
    public string? AuthorName {  get; set; }

    [NotMapped]
    [JsonPropertyName("genre_name")]
    public string? GenreName { get; set; }

    public virtual Author? Author { get; set; }

    public virtual Genre? Genre { get; set; }
}
