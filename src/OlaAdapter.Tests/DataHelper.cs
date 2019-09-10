using System;
using NHibernate;
using OlaDatabase.Entities;

namespace OlaAdapter.Tests
{
    public class DataHelper
    {
        private ISession _session;

        public DataHelper(ISession session)
        {
            _session = session;
        }

        public OrganisationEnity CreateOrganisation(string name)
        {
            var organisation = new OrganisationEnity
            {
                Name = name,
            };
            _session.Save(organisation);

            return organisation;
        }

        internal void SaveResult(RaceClassEntity raceClass, string firstName, string familyName, OrganisationEnity organisation, string status, TimeSpan totalTime)
        {
            PersonEntity person = new PersonEntity
            {
                FirstName = firstName,
                FamilyName = familyName,
                Organisation = organisation,
            };
            _session.Save(person);

            var entry = new EntryEntity
            {
                Person = person,
            };
            _session.Save(entry);

            var result = new ResultEntity
            {
                RunnerStatus = status,
                RaceClass = raceClass,
                Entry = entry,
                TotalTime = (int)totalTime.TotalSeconds * 100,
            };
            _session.Save(result);
        }
    }
}
