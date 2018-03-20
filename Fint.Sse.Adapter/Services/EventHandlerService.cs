using System;
using Fint.Event.Model;
using Fint.Event.Model.Health;
using FINT.Model.Ressurser.Tilganger;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Fint.Sse.Adapter.Services
{
    public class EventHandlerService : IEventHandlerService
    {
        private readonly IEventStatusService _statusService;
        private readonly IHttpService _httpService;
        private readonly ITilgangerService _tilgangerService;
        private readonly ILogger<EventHandlerService> _logger;
        private readonly AppSettings _appSettings;

        public EventHandlerService(
            IEventStatusService statusService,
            IHttpService httpService,
            ITilgangerService tilgangerService,
            IOptions<AppSettings> appSettings,
            ILogger<EventHandlerService> logger)
        {
            _statusService = statusService;
            _httpService = httpService;
            _tilgangerService = tilgangerService;
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        public void HandleEvent(Event<object> serverSideEvent)
        {
            if (serverSideEvent.IsHealthCheck())
            {
                PostHealthCheckResponse(serverSideEvent);
            }
            else
            {
                if (_statusService.VerifyEvent(serverSideEvent).Status == Status.ADAPTER_ACCEPTED)
                {
                    var action =
                        (TilgangerActions) Enum.Parse(typeof(TilgangerActions), serverSideEvent.Action, ignoreCase: true);
                    var responseEvent = serverSideEvent;

                    switch (action)
                    {
                        case TilgangerActions.GET_ALL_IDENTITET:
                            _tilgangerService.GetAllIdentiteter(serverSideEvent);
                            break;
                        case TilgangerActions.GET_ALL_RETTIGHET:
                            _tilgangerService.GetAllRettigheter(serverSideEvent);
                            break;
                        default:
                            var message = $"Unhandled action: {action}";
                            _logger.LogError(message);
                            throw new Exception(message);
                    }

                    responseEvent.Status = Status.ADAPTER_RESPONSE;
                    _logger.LogInformation("POST EventResponse");
                    _httpService.Post(_appSettings.ResponseEndpoint, responseEvent);
                }
            }
        }

        private void PostHealthCheckResponse(Event<object> serverSideEvent)
        {
            var healthCheckEvent = serverSideEvent;
            healthCheckEvent.Status = Status.TEMP_UPSTREAM_QUEUE;

            if (IsHealthy())
            {
                healthCheckEvent.Data.Add(new Health("adapter", HealthStatus.APPLICATION_HEALTHY));
            }
            else
            {
                healthCheckEvent.Data.Add(new Health("adapter", HealthStatus.APPLICATION_UNHEALTHY));
                healthCheckEvent.Message = "The adapter is unable to communicate with the application.";
            }

            _httpService.Post(_appSettings.ResponseEndpoint, healthCheckEvent);
        }

        private bool IsHealthy()
        {
            //Check application connectivity etc.
            return true;
        }
    }
}