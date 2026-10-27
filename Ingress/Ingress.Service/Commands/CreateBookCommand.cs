using MediatR;
using Ingress.Web.Models;
using EBookPlatform.Contracts;

namespace Ingress.Service.Command;

public class CreateBookCommand: IRequest<Guid>
{
    /// <summary>
    /// 
    /// </summary>
    public Guid BookId {get; init;} = Guid.NewGuid();

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

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
{
    private readonly IMessageSession _messageSession;
    public CreateBookCommandHandler(IMessageSession messageSession)
    {
        _messageSession = messageSession;
    }

    public async Task<Guid> Handle(CreateBookCommand command, CancellationToken cancellationToken)
    {
        await _messageSession.Publish<DraftSubmittedEvent>(message =>
        {
            message.SubmissionId = command.BookId;
            message.Title = command.Name;
            message.AuthorEmail = command.Author.email;
            message.PublishYear = command.PublishYear;
            message.Genre = command.Genre.ToString();
        });

        return command.BookId;
    }
}