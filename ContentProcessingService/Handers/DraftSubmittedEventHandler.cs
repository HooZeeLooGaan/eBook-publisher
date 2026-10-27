using EBookPlatform.Contracts;

namespace ContentProcessingService.Handlers;

class DraftSubmittedEventHandler : IHandleMessages<DraftSubmittedEvent>
{
    private readonly ILogger<DraftSubmittedEvent> _logger;
    public DraftSubmittedEventHandler(ILogger<DraftSubmittedEvent> logger)
    {
        _logger = logger;
    }

    public async Task Handle(DraftSubmittedEvent message, IMessageHandlerContext messageHandlerContext)
    {
        _logger.LogInformation("Processing book draft {Id}: {Title}", message.SubmissionId, message.Title);

        if (message.PublishYear > DateTime.UtcNow.Year)
        {
            _logger.LogWarning("Invalid publish year for draft {Id}", message.SubmissionId);
            return;
        }

        await Task.CompletedTask;
    }
}