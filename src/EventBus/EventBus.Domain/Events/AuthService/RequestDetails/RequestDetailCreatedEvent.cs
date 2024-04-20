using EventBus.Domain.Events.Basics;

namespace EventBus.Domain.Events.AuthService.RequestDetails;

public record RequestDetailCreatedEvent(string ControllerName,
                                        string Path,
                                        string HttpMethod,
                                        int StatusCode,
                                        string? RequestBody,
                                        string? RequestContentType,
                                        string? ResponseBody,
                                        string? ResponseContentType,
                                        long RequestTimeInMiliseconds,
                                        DateTime SentAt,
                                        int? UserId) : EventBase();