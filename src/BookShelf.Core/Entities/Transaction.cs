namespace BookShelf.Core.Entities;

public class Transaction
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int UserId { get; set; }
    public DateTime BorrowedDate { get; set; }
    public DateTime? ReturnedDate { get; set; }
    public int LateFee { get; set; }
    public Book Book { get; set; }
    public User User { get; set; }
    public DateTime DueDate => BorrowedDate.AddDays(14);
}

