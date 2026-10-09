using System;
using System.Collections.Generic;

namespace Border_Control
{
    public class TravelerGenerator
    {
        private readonly Random random;
        private readonly string[] countries = { "Valdora", "Norvia", "Estara", "Ruvia" };
        private readonly string[] firstNames = { "Anton", "Mara", "Pavel", "Elena", "Niko", "Vera", "Tomas", "Lena" };
        private readonly string[] lastNames = { "Varga", "Petrov", "Ionescu", "Novak", "Marin", "Sokol", "Volkov", "Dima" };
        private readonly string[] occupations = { "Machinist", "Nurse", "Carpenter", "Driver" };
        private readonly string[] employers = { "North Rail Works", "City Hospital", "Oak Workshop", "Union Freight" };

        public TravelerGenerator() : this(new Random()) { }
        public TravelerGenerator(Random random) { this.random = random; }

        public Traveler Create(DailyRules rules, int queueNumber)
        {
            Traveler traveler = CreateValid(rules);
            if (rules.Day == 3 && queueNumber == 3)
            {
                traveler.FullName = traveler.Passport.FullName = "Mira Volkov";
                traveler.Sex = traveler.Passport.Sex = "F";
                traveler.Passport.Nationality = traveler.Nationality = "Ruvia";
                traveler.EntryPermit = MakeEntry(traveler, rules);
                traveler.SpecialRole = "REFUGEE";
                traveler.Dialogue = "My family is waiting on the other side. Please let me through, even with the ban.";
                return traveler;
            }
            if (rules.Day == 5 && queueNumber == 3)
            {
                traveler.Purpose = "WORK";
                traveler.EntryPermit = MakeEntry(traveler, rules);
                traveler.WorkPermit = null;
                traveler.SpecialRole = "WORKER";
                traveler.Dialogue = "The factory promised to send my work permit. I only have these papers.";
                return traveler;
            }
            if (rules.Day == 9 && queueNumber == 4)
            {
                traveler.SpecialRole = "COURIER";
                traveler.Dialogue = "These papers are in order. I carry evidence for the city. Please don't turn me away.";
                return traveler;
            }
            if (rules.Day == 10 && queueNumber == 5)
            {
                traveler.SpecialRole = "INVESTIGATOR";
                traveler.Dialogue = "I'm here to hear the witnesses. Your decision today may finally open an inquiry.";
                return traveler;
            }
            // Most visitors are valid. An invalid visitor has one issue, occasionally two.
            if (random.Next(100) < 35)
            {
                AddProblem(traveler, rules);
                if (rules.Day >= 8 && random.Next(100) < 25) AddProblem(traveler, rules);
            }
            return traveler;
        }

        public Traveler CreateValid(DailyRules rules)
        {
            int first = random.Next(firstNames.Length);
            string nationality = countries[random.Next(rules.RestrictNationality ? 3 : 4)];
            string purpose = nationality == DailyRules.HomeCountry ? "RETURN"
                : new[] { "VISIT", "WORK", "TRANSIT" }[random.Next(3)];
            DateTime birth = rules.Date.AddYears(-random.Next(19, 66)).AddDays(-random.Next(365));
            var traveler = new Traveler
            {
                FullName = firstNames[first] + " " + lastNames[random.Next(lastNames.Length)],
                Nationality = nationality, DateOfBirth = birth, Sex = first % 2 == 0 ? "M" : "F",
                Purpose = purpose, PortraitStyle = random.Next(4), SpecialRole = ""
            };
            traveler.Passport = new Passport
            {
                FullName = traveler.FullName, Nationality = nationality, DateOfBirth = birth, Sex = traveler.Sex,
                Number = nationality.Substring(0, 2).ToUpperInvariant() + "-" + random.Next(100000, 1000000),
                Expires = rules.Date.AddDays(random.Next(20, 900))
            };
            if (rules.RequireEntry && nationality != DailyRules.HomeCountry) traveler.EntryPermit = MakeEntry(traveler, rules);
            if (rules.RequireWork && purpose == "WORK") traveler.WorkPermit = MakeWork(traveler, rules);
            if (rules.RequireHealth)
            {
                traveler.HealthCertificate = new HealthCertificate
                {
                    FullName = traveler.FullName, Number = "HC-" + random.Next(100000, 1000000),
                    Examined = rules.Date.AddDays(-random.Next(1, 25)), ValidUntil = rules.Date.AddDays(random.Next(10, 120)),
                    Cleared = true
                };
            }
            switch (purpose)
            {
                case "WORK": traveler.Dialogue = "I'm starting a job in Valdora. Here are my documents."; break;
                case "TRANSIT": traveler.Dialogue = "Just passing through. I won't be staying long."; break;
                case "RETURN": traveler.Dialogue = "I'm coming home. It has been a long trip."; break;
                default: traveler.Dialogue = "I'm here to visit family. Here are my papers."; break;
            }
            return traveler;
        }

        private EntryPermit MakeEntry(Traveler traveler, DailyRules rules)
        {
            return new EntryPermit
            {
                FullName = traveler.FullName, PassportNumber = traveler.Passport.Number, Purpose = traveler.Purpose,
                StayDays = traveler.Purpose == "TRANSIT" ? 2 : random.Next(7, 61),
                Expires = rules.Date.AddDays(random.Next(5, 100))
            };
        }

        private WorkPermit MakeWork(Traveler traveler, DailyRules rules)
        {
            int job = random.Next(occupations.Length);
            return new WorkPermit
            {
                FullName = traveler.FullName, Occupation = occupations[job], Employer = employers[job],
                Expires = rules.Date.AddDays(random.Next(10, 200)), Purpose = "WORK"
            };
        }

        private void AddProblem(Traveler traveler, DailyRules rules)
        {
            var changes = new List<Action>();
            changes.Add(() => traveler.Passport.Expires = rules.Date.AddDays(-random.Next(1, 80)));
            if (rules.RestrictNationality) changes.Add(() => traveler.Passport.Nationality = traveler.Nationality = "Ruvia");
            if (traveler.EntryPermit != null)
            {
                changes.Add(() => traveler.EntryPermit = null);
                changes.Add(() => { if (traveler.EntryPermit != null) traveler.EntryPermit.Expires = rules.Date.AddDays(-1); });
                if (rules.CheckMatching)
                {
                    changes.Add(() => { if (traveler.EntryPermit != null) traveler.EntryPermit.FullName = "Ivan " + lastNames[random.Next(lastNames.Length)]; });
                    changes.Add(() => { if (traveler.EntryPermit != null) traveler.EntryPermit.PassportNumber = "NO-000001"; });
                }
                if (rules.ExtraChecks) changes.Add(() => { if (traveler.EntryPermit != null) traveler.EntryPermit.Purpose = "UNLISTED"; });
                if (rules.CheckPurposeAndStay) changes.Add(() => { if (traveler.EntryPermit != null) traveler.EntryPermit.StayDays = 120; });
                if (rules.CheckPurposeAndStay) changes.Add(() => { if (traveler.EntryPermit != null) traveler.EntryPermit.Purpose = traveler.Purpose == "WORK" ? "VISIT" : "WORK"; });
            }
            if (traveler.WorkPermit != null)
            {
                changes.Add(() => traveler.WorkPermit = null);
                changes.Add(() => { if (traveler.WorkPermit != null) traveler.WorkPermit.Expires = rules.Date.AddDays(-1); });
                changes.Add(() => { if (traveler.WorkPermit != null) traveler.WorkPermit.FullName = "Ivan Marin"; });
            }
            if (traveler.HealthCertificate != null)
            {
                changes.Add(() => traveler.HealthCertificate = null);
                changes.Add(() => { if (traveler.HealthCertificate != null) traveler.HealthCertificate.Cleared = false; });
                changes.Add(() => { if (traveler.HealthCertificate != null) traveler.HealthCertificate.ValidUntil = rules.Date.AddDays(-1); });
                changes.Add(() => { if (traveler.HealthCertificate != null) traveler.HealthCertificate.FullName = "Ivan Marin"; });
                if (rules.ExtraChecks) changes.Add(() => { if (traveler.HealthCertificate != null) traveler.HealthCertificate.Examined = rules.Date.AddDays(3); });
            }
            if (rules.ExtraChecks)
            {
                changes.Add(() => traveler.Passport.Number = "INVALID");
                changes.Add(() => traveler.Passport.DateOfBirth = rules.Date.AddYears(2));
            }
            changes[random.Next(changes.Count)]();
        }
    }
}
