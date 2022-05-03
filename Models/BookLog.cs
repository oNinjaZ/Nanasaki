using System;

namespace Nanasaki.Models;

public class BookLog
{
    public Guid Id { get; set; }
    public string User { get; set; }
    public int PagesRead { get; set; }
    public DateTime LogDate { get; set; }
}