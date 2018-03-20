using Fint.Event.Model;

namespace Fint.Sse.Adapter.Services
{
    public interface ITilgangerService
    {
        void GetAllIdentiteter(Event<object> serverSideEvent);
        void GetAllRettigheter(Event<object> serverSideEvent);
    }
}