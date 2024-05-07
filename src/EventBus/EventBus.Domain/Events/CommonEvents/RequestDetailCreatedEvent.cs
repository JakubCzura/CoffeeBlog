using EventBus.Domain.Events.Basics;

namespace EventBus.Domain.Events.CommonEvents;

public record RequestDetailCreatedEvent(string MicroserviceName,
                                        string ControllerName,
                                        string Path,
                                        string HttpMethod,
                                        int StatusCode,
                                        string? RequestBody,
                                        string? RequestContentType,
                                        string? ResponseBody,
                                        string? ResponseContentType,
                                        long RequestTimeInMiliseconds,
                                        DateTime SentAt,
                                        int? UserId,
                                        string EventPublisherName) : EventBase(EventPublisherName);