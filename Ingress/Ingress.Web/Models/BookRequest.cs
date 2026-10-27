using System.Diagnostics.CodeAnalysis;
using FluentValidation;

using Ingress.Service.Command;
using Mapster;

namespace Ingress.Web.Models;

/// <summary>
/// 
/// </summary>
[ExcludeFromCodeCoverage]
public class BookRequest
{
    /// <summary>
    /// 
    /// </summary>
    public Guid BookId {get; init;}

    /// <summary>
    /// 
    /// </summary>
    public string Name {get; set;}

    /// <summary>
    /// 
    /// </summary>
    public Author Author {get; set;}
    
    /// <summary>
    /// 
    /// </summary>
    public int PublishYear {get; set;}

    /// <summary>
    /// 
    /// </summary>
    public int pages {get; set;}

    /// <summary>
    /// 
    /// </summary>
    public Genre Genre {get; set;}
}

/// <summary>
/// 
/// </summary>
public class BookValidator: AbstractValidator<BookRequest>
{
    public BookValidator()
    {
        RuleFor(book => book.Name)
            .NotEmpty().WithMessage("Book entry cannot have a missing name")
            .MaximumLength(100).WithMessage("Book name cannot be more than 100 characters");

        RuleFor(book => book.PublishYear)
            .NotEmpty().WithMessage("Book entry missing publish year")
            .LessThan(DateTime.Today.Year).WithMessage("Book publish year invalid");

        RuleFor(book => book.pages)
            .LessThan(5000);
    }
}