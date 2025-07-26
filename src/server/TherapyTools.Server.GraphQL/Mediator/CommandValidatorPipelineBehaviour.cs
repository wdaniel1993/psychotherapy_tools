using FluentValidation;
using Mediator;

namespace TherapyTools.Server.GraphQL.Mediator;

public sealed class CommandValidatorPipelineBehaviour<TMessage, TResponse>(IValidator<TMessage>? validator = null) : MessagePreProcessor<TMessage, TResponse>
    where TMessage : ICommand
{
    protected override async ValueTask Handle(TMessage message, CancellationToken cancellationToken)
    {
        if (validator is null)
            return;
                        
        var validationResult = await validator.ValidateAsync(message, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
    }
}