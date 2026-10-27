using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace Ingress.Web.Models;

/// <summary>
/// 
/// </summary>
[ExcludeFromCodeCoverage]
public class Author
{
    /// <summary>
    /// 
    /// </summary>
    public int Id {get; init;}

    /// <summary>
    /// 
    /// </summary>
    public string Name {get; set;}

    /// <summary>
    /// 
    /// </summary>
    public string email {get; set;} 
}

/// <summary>
/// 
/// </summary>
public class AuthorValidator: AbstractValidator<Author>
{
    public AuthorValidator()
    {
        RuleFor(author => author.Name)
            .NotEmpty().WithMessage("Author name can not be empty")
            .MaximumLength(50)
            .MinimumLength(3);

        RuleFor(author => author.email)
            .EmailAddress();
    }
}