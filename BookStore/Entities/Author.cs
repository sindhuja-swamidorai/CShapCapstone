﻿using System;
using System.Collections.Generic;

namespace BookStore.Entities;

public class Author
{
    public int AuthorId { get; set; }

    public string AuthorName { get; set; } = null!;

    public string? Biography { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
