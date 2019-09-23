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

        internal void ClearAndFlush()
        {
            _session.Flush();
            _session.Clear();
        }

        internal EventEntity CreateEvent(string name)
        {
            var eventEntity = new EventEntity(name);
            _session.Save(eventEntity);
            return eventEntity;
        }

        internal EventRaceEntity CreateEventRace(EventEntity eventEntity)
        {
            var eventRace = new EventRaceEntity(eventEntity);
            _session.Save(eventRace);
            return eventRace;
        }

        internal OrganisationEnity CreateOrganisation(string name)
        {
            var organisation = new OrganisationEnity(name, 3, 752);
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

        internal ControlEntity CrateControl(EventRaceEntity eventRace, int id, string typeCode = "WC")
        {
            var control = new ControlEntity(eventRace, id)
            {
                TypeCode = typeCode,
            };
            _session.Save(control);
            return control;
        }

        internal SplitTimeControlEntity CreateSplitTimeControl(EventRaceEntity eventRace, ControlEntity control, string name)
        {
            var splitTimeControl = new SplitTimeControlEntity(eventRace)
            {
                TimingControl = control,
                Name = name,
            };
            _session.Save(splitTimeControl);
            return splitTimeControl;
        }

        internal RaceClassSplitTimeControlEntity CreateRaceClassSplitTimeControl(RaceClassEntity raceClass, SplitTimeControlEntity splitTimeControl)
        {
            var raceClassSplitTimeControl = new RaceClassSplitTimeControlEntity
            {
                Id = new RaceClassSplitTimeControlKey
                {
                    RaceClass = raceClass,
                    SplitTimeControl = splitTimeControl,
                }
            };
            _session.Save(raceClassSplitTimeControl);
            return raceClassSplitTimeControl;
        }

        internal CourseEntity CreateCourse(EventRaceEntity eventRace, string name)
        {
            var course = new CourseEntity(eventRace, name);
            _session.Save(course);
            return course;
        }

        internal RaceClassCourseEntity CreateRaceClassCourse(RaceClassEntity raceClass, CourseEntity course)
        {
            var raceClassCourse = new RaceClassCourseEntity(raceClass, course);
            _session.Save(raceClassCourse);
            return raceClassCourse;
        }

        internal CoursesWaypointControlEntity CreateCourseWaypointControl(CourseEntity course, ControlEntity control, int ordered)
        {
            var coursesWaypointControl = new CoursesWaypointControlEntity(course, control, ordered);
            _session.Save(coursesWaypointControl);
            return coursesWaypointControl;
        }

        internal EntryEntity CreatePersonAndEntry(EventEntity eventEntity, string firstName, string familyName, OrganisationEnity organisation)
        {
            var person = new PersonEntity
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
            return entry;
        }

        internal void CreatePersonAndEntryAndResult(EventEntity eventEntity, RaceClassEntity raceClass, string firstName, string familyName, OrganisationEnity organisation, string status, TimeSpan totalTime)
        {
            var entry = CreatePersonAndEntry(eventEntity, firstName, familyName, organisation);

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
