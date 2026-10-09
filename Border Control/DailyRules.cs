using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Border_Control
{
    public class DailyRules
    {
        public const string HomeCountry = "Valdora";
        public int Day { get; private set; }
        public DateTime Date { get { return new DateTime(1986, 11, 1).AddDays(Day - 1); } }
        public bool RequireEntry { get { return Day >= 2; } }
        public bool RestrictNationality { get { return Day >= 3; } }
        public bool CheckMatching { get { return Day >= 4; } }
        public bool RequireWork { get { return Day >= 5; } }
        public bool RequireHealth { get { return Day >= 6; } }
        public bool ExtraChecks { get { return Day >= 7; } }
        public bool CheckPurposeAndStay { get { return Day >= 8; } }
        public int TravelerLimit { get { return 6 + (Day - 1) / 2; } }
        public int Fine { get { return Day >= 9 ? 15 : 10; } }

        public DailyRules(int day)
        {
            if (day < 1 || day > 10) throw new ArgumentOutOfRangeException("day");
            Day = day;
        }

        public string NewRule
        {
            get
            {
                switch (Day)
                {
                    case 1: return "Every traveler needs an unexpired passport.";
                    case 2: return "Foreign travelers now need an unexpired entry permit.";
                    case 3: return "Entry from Ruvia is suspended. Deny Ruvian passports.";
                    case 4: return "Names and passport numbers must match between required documents.";
                    case 5: return "All workers now need an unexpired work permit.";
                    case 6: return "All travelers need a valid health certificate with medical clearance.";
                    case 7: return "Check document numbers, birth dates, exam dates and permit purposes.";
                    case 8: return "Entry purpose must match the traveler. Maximum stay: 90 days. Work permits are for workers only.";
                    case 9: return "Longer queue. Fines rise to 15 credits after two daily warnings.";
                    default: return "Final inspection day. All previous rules remain in force.";
                }
            }
        }

        public string BulletinText()
        {
            var text = new StringBuilder();
            text.AppendLine("MINISTRY OF BORDER AFFAIRS / VALDORA");
            text.AppendLine("DAY " + Day + "    " + Date.ToString("yyyy-MM-dd"));
            text.AppendLine("\nNEW TODAY\n" + NewRule + "\n");
            text.AppendLine("CURRENT RULES");
            text.AppendLine("- Passport required. Expiry today is still valid.");
            if (RequireEntry) text.AppendLine("- Foreigners: entry permit required and unexpired.");
            if (RestrictNationality) text.AppendLine("- Ruvian nationality: entry prohibited.");
            if (CheckMatching) text.AppendLine("- Required documents: names match the passport; entry passport number matches.");
            if (RequireWork) text.AppendLine("- WORK: work permit required and unexpired (including citizens).");
            if (RequireHealth) text.AppendLine("- Everyone: unexpired health certificate; status must be CLEARED.");
            if (ExtraChecks)
            {
                text.AppendLine("- Passport number: two capital letters, dash, six digits (VA-123456).");
                text.AppendLine("- Passport birth date must be in the past; sex must be F or M.");
                text.AppendLine("- Health number: HC- and six digits. Exam cannot be in the future or after expiry.");
                text.AppendLine("- Permit purpose: VISIT, TRANSIT, WORK or RETURN.");
            }
            if (CheckPurposeAndStay)
            {
                text.AppendLine("- Entry purpose matches the stated purpose; stay is 1 to 90 days.");
                text.AppendLine("- A supplied work permit must state WORK; its holder must be a worker.");
            }
            text.AppendLine("\nPAY AND SHIFT");
            text.AppendLine("6 credits per correct decision. First two errors: warnings.");
            text.AppendLine("Further errors: " + Fine + " credits each. Daily expenses: 35 credits.");
            text.AppendLine("Queue: " + TravelerLimit + " travelers. Clock advances with each decision.");
            text.AppendLine("\nINSPECTION MACHINE");
            text.AppendLine("Open documents, enable inspection, select the same field type on two documents.");
            text.AppendLine("MATCH compares selected values only. You must still check the rules and dates.");
            return text.ToString();
        }

        // Only the completed decision receives a list of problems.
        public List<string> FindProblems(Traveler traveler)
        {
            var problems = new List<string>();
            Passport passport = traveler.Passport;
            if (passport == null)
            {
                problems.Add("Missing passport.");
                return problems;
            }
            if (passport.Expires.Date < Date) problems.Add("Passport expired.");
            if (RestrictNationality && passport.Nationality == "Ruvia") problems.Add("Ruvian entry is prohibited.");
            bool foreign = passport.Nationality != HomeCountry;
            EntryPermit entry = traveler.EntryPermit;
            WorkPermit work = traveler.WorkPermit;
            HealthCertificate health = traveler.HealthCertificate;
            if (RequireEntry && foreign)
            {
                if (entry == null) problems.Add("Missing entry permit.");
                else
                {
                    if (entry.Expires.Date < Date) problems.Add("Entry permit expired.");
                    if (CheckMatching && !Same(passport.FullName, entry.FullName)) problems.Add("Entry permit name mismatch.");
                    if (CheckMatching && !Same(passport.Number, entry.PassportNumber)) problems.Add("Entry passport number mismatch.");
                    if (ExtraChecks && !ValidPurpose(entry.Purpose)) problems.Add("Invalid entry purpose.");
                    if (CheckPurposeAndStay && !Same(traveler.Purpose, entry.Purpose)) problems.Add("Entry purpose does not match the stated purpose.");
                    if (CheckPurposeAndStay && (entry.StayDays < 1 || entry.StayDays > 90)) problems.Add("Stay must be 1 to 90 days.");
                }
            }
            if (RequireWork && traveler.Purpose == "WORK")
            {
                if (work == null) problems.Add("Missing work permit.");
                else
                {
                    if (work.Expires.Date < Date) problems.Add("Work permit expired.");
                    if (CheckMatching && !Same(passport.FullName, work.FullName)) problems.Add("Work permit name mismatch.");
                }
            }
            if (CheckPurposeAndStay && work != null && (traveler.Purpose != "WORK" || work.Purpose != "WORK"))
                problems.Add("Work permit purpose is inconsistent.");
            if (RequireHealth)
            {
                if (health == null) problems.Add("Missing health certificate.");
                else
                {
                    if (health.ValidUntil.Date < Date) problems.Add("Health certificate expired.");
                    if (!health.Cleared) problems.Add("Medical clearance refused.");
                    if (CheckMatching && !Same(passport.FullName, health.FullName)) problems.Add("Health certificate name mismatch.");
                    if (ExtraChecks && !Regex.IsMatch(health.Number ?? "", @"^HC-\d{6}$")) problems.Add("Invalid health certificate number.");
                    if (ExtraChecks && (health.Examined.Date > Date || health.Examined.Date > health.ValidUntil.Date)) problems.Add("Invalid medical examination date.");
                }
            }
            if (ExtraChecks)
            {
                if (!Regex.IsMatch(passport.Number ?? "", @"^[A-Z]{2}-\d{6}$")) problems.Add("Invalid passport number.");
                if (passport.DateOfBirth.Date >= Date || passport.DateOfBirth.Year < Date.Year - 100) problems.Add("Invalid birth date.");
                if (passport.Sex != "F" && passport.Sex != "M") problems.Add("Invalid sex field.");
                if (string.IsNullOrWhiteSpace(passport.FullName) || string.IsNullOrWhiteSpace(passport.Nationality)) problems.Add("Incomplete passport.");
            }
            return problems;
        }

        public static bool Same(string first, string second)
        {
            return string.Equals((first ?? "").Trim(), (second ?? "").Trim(), StringComparison.OrdinalIgnoreCase);
        }

        private static bool ValidPurpose(string purpose)
        {
            return purpose == "VISIT" || purpose == "WORK" || purpose == "TRANSIT" || purpose == "RETURN";
        }
    }
}
