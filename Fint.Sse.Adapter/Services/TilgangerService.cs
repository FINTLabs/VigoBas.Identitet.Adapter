using System.Collections.Generic;
using System.Linq;
using Fint.Event.Model;
using FINT.Model.Ressurser.Tilganger;
using FINT.Model.Felles.Kompleksedatatyper;
using Fint.Relation.Model;

namespace Fint.Sse.Adapter.Services
{
    public class TilgangerService : ITilgangerService
    {
        private IEnumerable<Identitet> _identiteter;
        private IEnumerable<Rettighet> _rettigheter;

        public TilgangerService()
        {
            SetupExampleData();
        }

        public void GetAllIdentiteter(Event<object> serverSideEvent)
        {
            var relationRettighet1 = new RelationBuilder()
                .With(Identitet.Relasjonsnavn.RETTIGHET)
                .ForType("no.FINT.Model.Ressurser.Tilganger.Rettighet") //TODO
                .Value("BAT-001")
                .Build();

            var relationRettighet2 = new RelationBuilder()
                .With(Identitet.Relasjonsnavn.RETTIGHET)
                .ForType("no.FINT.Model.Ressurser.Tilganger.Rettighet") //TODO
                .Value("BAT-002")
                .Build();

            var identiteter = _identiteter.ToList();

            serverSideEvent.Data.Add(FintResource<Identitet>.With(identiteter[0]).AddRelations(relationRettighet1));
            serverSideEvent.Data.Add(FintResource<Identitet>.With(identiteter[1]).AddRelations(relationRettighet2));
        }

        public void GetAllRettigheter(Event<object> serverSideEvent)
        {
            var relationIdentitet1 = new RelationBuilder()
                .With(Rettighet.Relasjonsnavn.IDENTITET)
                .ForType("no.FINT.Model.Ressurser.Tilganger.Identitet") //TODO
                .Value("BATMAN")
                .Build();

            var relationIdentitet2 = new RelationBuilder()
                .With(Rettighet.Relasjonsnavn.IDENTITET)
                .ForType("no.FINT.Model.Ressurser.Tilganger.Identitet") //TODO
                .Value("ROBIN")
                .Build();

            var rettigheter = _rettigheter.ToList();

            serverSideEvent.Data.Add(FintResource<Rettighet>.With(rettigheter[0]).AddRelations(relationIdentitet1));
            serverSideEvent.Data.Add(FintResource<Rettighet>.With(rettigheter[1]).AddRelations(relationIdentitet2));
        }

        private void SetupExampleData()
        {
            _identiteter = new List<Identitet>
            {
                new Identitet
                {
                    SystemId = new Identifikator { Identifikatorverdi = "BATMAN" },
                },
                new Identitet
                {
                    SystemId = new Identifikator { Identifikatorverdi = "ROBIN" },
                }
            };

            _rettigheter = new List<Rettighet>
            {
                new Rettighet {
                    SystemId = new Identifikator { Identifikatorverdi = "BAT-001" },
                    Beskrivelse = "Grants access to the secret cave",
                    Navn = "BATCAVE"
                },
                new Rettighet {
                    SystemId = new Identifikator { Identifikatorverdi = "BAT-002" },
                    Beskrivelse = "Grants access to driving the ultimate vehicle",
                    Navn = "BATMOBILE"
                }
            };
        }
    }
}