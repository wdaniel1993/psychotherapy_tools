﻿namespace TherapyTools.Application.Common.Interfaces;

public interface IAggregateIntegrationEvent : IIntegrationEvent
{
    Guid AggregateId { get; }
}