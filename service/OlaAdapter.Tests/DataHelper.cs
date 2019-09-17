using System;
using NHibernate;
using OlaDatabase.Entities;

namespace OlaAdapter.Tests
{
    internal class DataHelper
    {
        private readonly ISession _session;

        internal DataHelper(ISession session)
        {
            _session = session;
        }

        internal EventEntity CreateEvent(string name)
        {
            var eventEntity = new EventEntity
            {
                Name = name,
                EventForm = "IndSingleDay",
            };

            _session.Save(eventEntity);
            return eventEntity;
        }

        internal EventRaceEntity CreateEventRace(EventEntity eventEntity)
        {
            var eventRace = new EventRaceEntity
            {
                EventRaceId = 1,
                RaceStatus = "notActivated",
                RaceLightCondition = "Day",
                RaceDistance = "Middle",
                Event = eventEntity,
            };
            _session.Save(eventRace);
            return eventRace;
        }

        internal OrganisationEnity CreateOrganisation(string name)
        {
            var organisation = new OrganisationEnity
            {
                Name = name,
            };
            _session.Save(organisation);

            return organisation;
        }

        internal RaceClassEntity CreateEventClassAndRaceClass(EventEntity eventEntity, EventRaceEntity eventRace, string Name)
        {
            var eventClass = new EventClassEntity
            {
                ClassStatus = "enterable",
                Name = Name,
                ShortName = Name,
                Sex = "M",
                Event = eventEntity
            };
            _session.Save(eventClass);
            var raceClass = new RaceClassEntity
            {
                RaceClassName = Name,
                EventClass = eventClass,
                EventRace = eventRace,
                RaceClassStatus = "started",
                AllocationMethod = "sNormalDraw",
                ViewWhichOrganisation = "club",
                StartMethod = "allocatedStart",
            };
            _session.Save(raceClass);
            return raceClass;
        }

        internal void CreatePersonAndEntryAndResult(EventEntity eventEntity, RaceClassEntity raceClass, string firstName, string familyName, OrganisationEnity organisation, string status, TimeSpan totalTime)
        {
            PersonEntity person = new PersonEntity
            {
                FirstName = firstName,
                FamilyName = familyName,
                DefaultOrganisation = organisation,
            };
            _session.Save(person);

            var entry = new EntryEntity
            {
                Competitor = person,
                Type = "single",
                Event = eventEntity,
            };
            _session.Save(entry);

            var result = new ResultEntity
            {
                RunnerStatus = status,
                RaceClass = raceClass,
                Entry = entry,
                TotalTime = (int)totalTime.TotalSeconds * 100,
            };
            raceClass.Results.Add(result);
            _session.Save(result);
        }
    }
}
