using EventBus.Domain.Events.Basics;

namespace EventBus.Domain.Events.StatisticsCollector.UserDiagnostics;

/// <summary>
/// Request that is used to request user diagnostic data.
/// </summary>
/// <param name="DataCollectedAt"> Date that represents when the data was collected.
/// Preferred way to collect data is to use quartz next day to collect data for the previous day. </param>
/// <param name="EventPublisherName">Name of event publisher.</param>
public record GetUsersDiagnosticDataRequest(DateTime DataCollectedAt,
                                            string EventPublisherName) : StatisticsCollectorEventBase(EventPublisherName);