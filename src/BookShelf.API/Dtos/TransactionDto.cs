namespace BookShelf.API.Dtos;

public class TransactionDto
{
    public int BookId { get; set; }
    public int UserId { get; set; }
    public DateTime BorrowedDate { get; set; }
}
