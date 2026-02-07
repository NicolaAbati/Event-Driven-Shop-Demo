namespace Shared.Contracts;

/// <summary>
/// Event that is published when a new order is created.
/// </summary>
/// <param name="OrderId">The unique identifier of the order.</param>
/// <param name="CustomerEmail">The email address of the customer who created the order.</param>
public record OrderCreatedEvent(
    Guid OrderId,
    string CustomerEmail
);