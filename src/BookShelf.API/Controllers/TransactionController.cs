using BookShelf.API.Dtos;
using BookShelf.Core.Entities;
using BookShelf.Core.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace BookShelf.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ListTransactions _listTransactions;
    private readonly GetTransactionDetails _getTransactionDetails;
    private readonly BorrowBook _borrowBook;
    private readonly ReturnBook _returnBook;

    public TransactionController(ListTransactions listTransactions, 
        GetTransactionDetails getTransactionDetails,
        BorrowBook borrowBook,
        ReturnBook returnBook)
    {
        _listTransactions = listTransactions;
        _getTransactionDetails = getTransactionDetails;
        _borrowBook = borrowBook;
        _returnBook = returnBook;
    }

    [HttpGet]
    public async Task<IActionResult> GetTransactions([FromQuery] int? userId, [FromQuery] int? bookId, [FromQuery] bool? isActive)
    {
        var transactions = await _listTransactions.Execute(userId, bookId, isActive);

        if (transactions == null || !transactions.Any())
            return NotFound("No transactions found matching the criteria.");

        return Ok(transactions);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransaction(int id)
    {
        var transaction = await _getTransactionDetails.Execute(id);
        if (transaction == null)
        {
            return NotFound();
        }
        return Ok(transaction);
    }

    [HttpPost("BorrowBook")]
    public async Task<IActionResult> BorrowBook(TransactionDto transaction)
    {
        await _borrowBook.Execute(transaction.BookId, transaction.UserId);
        return CreatedAtAction(nameof(GetTransaction), transaction);
    }

    [HttpPut("ReturnBook/{id}")]
    public async Task<IActionResult> ReturnBook(int id)
    {
        await _returnBook.Execute(id);

        return Ok();
    }
}
