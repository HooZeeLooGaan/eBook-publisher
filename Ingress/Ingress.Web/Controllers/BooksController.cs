using Microsoft.AspNetCore.Mvc;
using Ingress.Web.Models;
using Ingress.Service.Command;
using Mapster;
using MediatR;

namespace Ingress.Web.Controller;

/// <summary>
/// Enpoints to perform operations over a book draft
/// </summary>
[ApiController]
[Route("api/books")] 
public class BooksController: ControllerBase
{
    public readonly IMediator _mediatr;
    public BooksController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetAllBooks()
    {
        return Ok();
    }
    
    [HttpGet]
    [Route("{id}")]
    public IActionResult GetBookById()
    {
        return Ok();
    }

    /// <summary>
    /// Make a draft book and process it for approval and pubishing
    /// </summary>
    /// <param name="bookRequest">Book draft request</param>
    /// <returns>Book ID</returns>
    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] BookRequest bookRequest)
    {
        var bookCommand = bookRequest.Adapt<CreateBookCommand>();
        var response = await _mediatr.Send(bookCommand);
        return CreatedAtAction(nameof(CreateBook), response);
    }
}