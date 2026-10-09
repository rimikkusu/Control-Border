using System;

namespace Border_Control
{
    // Documents keep their data even when their inspection window is closed.
    public class Passport
    {
        public string FullName { get; set; }
        public string Nationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string Number { get; set; }
        public DateTime Expires { get; set; }
    }

    public class EntryPermit
    {
        public string FullName { get; set; }
        public string PassportNumber { get; set; }
        public string Purpose { get; set; }
        public int StayDays { get; set; }
        public DateTime Expires { get; set; }
    }

    public class WorkPermit
    {
        public string FullName { get; set; }
        public string Occupation { get; set; }
        public string Employer { get; set; }
        public DateTime Expires { get; set; }
        public string Purpose { get; set; }
    }

    public class HealthCertificate
    {
        public string FullName { get; set; }
        public string Number { get; set; }
        public DateTime Examined { get; set; }
        public DateTime ValidUntil { get; set; }
        public bool Cleared { get; set; }
    }

    public class Traveler
    {
        public string FullName { get; set; }
        public string Nationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string Purpose { get; set; }
        public string Dialogue { get; set; }
        public int PortraitStyle { get; set; }
        public string SpecialRole { get; set; }
        public Passport Passport { get; set; }
        public EntryPermit EntryPermit { get; set; }
        public WorkPermit WorkPermit { get; set; }
        public HealthCertificate HealthCertificate { get; set; }

        public int AgeOn(DateTime date)
        {
            int age = date.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > date.AddYears(-age).Date) age--;
            return age;
        }
    }

    public class DailyReport
    {
        public int Day { get; set; }
        public int Processed { get; set; }
        public int Correct { get; set; }
        public int Incorrect { get; set; }
        public int Earnings { get; set; }
        public int Penalties { get; set; }
        public int Expenses { get; set; }
        public int Balance { get; set; }
        public string Story { get; set; }
    }
}
