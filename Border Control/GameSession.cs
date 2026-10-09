using System;
using System.Collections.Generic;

namespace Border_Control
{
    public class GameSession
    {
        public DailyRules Rules { get; private set; }
        public int Balance { get; private set; }
        public int Processed { get; private set; }
        public int Correct { get; private set; }
        public int Incorrect { get; private set; }
        public int Earnings { get; private set; }
        public int Penalties { get; private set; }
        public bool ShiftStarted { get; private set; }
        public bool IsSettled { get; private set; }
        public bool ShiftFinished { get { return Processed >= Rules.TravelerLimit; } }
        public bool RefugeeAdmitted { get; private set; }
        public bool CourierAdmitted { get; private set; }
        public bool InvestigatorAdmitted { get; private set; }
        public int TotalCorrect { get; private set; }
        public int TotalProcessed { get; private set; }
        private DailyReport lastReport;
        private Traveler lastProcessedTraveler;
        private string storyToday = "No special incidents.";

        public GameSession()
        {
            Rules = new DailyRules(1);
            Balance = 50;
        }

        public bool StartShift()
        {
            if (ShiftStarted || IsSettled) return false;
            ShiftStarted = true;
            return true;
        }

        public string RecordDecision(Traveler traveler, bool approve)
        {
            if (!ShiftStarted || IsSettled || ShiftFinished || traveler == null || traveler == lastProcessedTraveler)
                throw new InvalidOperationException("This traveler cannot be processed now.");
            lastProcessedTraveler = traveler;
            List<string> problems = Rules.FindProblems(traveler);
            bool correct = approve == (problems.Count == 0);
            Processed++;
            TotalProcessed++;
            string feedback;
            if (correct)
            {
                Correct++;
                TotalCorrect++;
                Earnings += 6;
                feedback = (approve ? "APPROVED" : "DENIED") + " / Correct decision. +6 credits.";
            }
            else
            {
                Incorrect++;
                int fine = Incorrect <= 2 ? 0 : Rules.Fine;
                Penalties += fine;
                feedback = "CITATION / " + (fine == 0 ? "Warning " + Incorrect + "/2. " : "Fine: " + fine + " credits. ");
                feedback += problems.Count == 0 ? "Documents were valid." : string.Join(" ", problems);
            }
            if (traveler.SpecialRole == "REFUGEE")
            {
                RefugeeAdmitted = approve;
                storyToday = approve ? "You let Mira cross despite the Ruvian ban. Her family reached safety." : "Mira was turned back at the border.";
            }
            if (traveler.SpecialRole == "COURIER")
            {
                CourierAdmitted = approve;
                storyToday = approve ? "The courier delivered evidence of the border ministry's abuses." : "The courier's evidence never reached the city.";
            }
            if (traveler.SpecialRole == "WORKER")
                storyToday = approve ? "The worker crossed without the promised work permit." : "The worker returned to the factory for a work permit.";
            if (traveler.SpecialRole == "INVESTIGATOR")
            {
                InvestigatorAdmitted = approve;
                storyToday = approve ? "The investigator entered Valdora to hear witnesses." : "The investigator was turned away.";
            }
            return feedback;
        }

        // Settlement is idempotent: reopening a report never pays or charges twice.
        public DailyReport SettleDay()
        {
            if (IsSettled) return lastReport;
            if (!ShiftFinished) throw new InvalidOperationException("Finish the queue before settling.");
            Balance += Earnings - Penalties - 35;
            IsSettled = true;
            lastReport = new DailyReport
            {
                Day = Rules.Day, Processed = Processed, Correct = Correct, Incorrect = Incorrect,
                Earnings = Earnings, Penalties = Penalties, Expenses = 35, Balance = Balance,
                Story = Rules.Day == 10 ? EndingText() : storyToday
            };
            return lastReport;
        }

        public bool NextDay()
        {
            if (!IsSettled || Rules.Day == 10) return false;
            Rules = new DailyRules(Rules.Day + 1);
            Processed = Correct = Incorrect = Earnings = Penalties = 0;
            ShiftStarted = IsSettled = false;
            lastProcessedTraveler = null;
            storyToday = "No special incidents.";
            return true;
        }

        public string ClockText()
        {
            int minutes = 8 * 60 + 10 * 60 * Processed / Rules.TravelerLimit;
            return (minutes / 60).ToString("00") + ":" + (minutes % 60).ToString("00");
        }

        public string EndingText()
        {
            string ending = CourierAdmitted && InvestigatorAdmitted
                ? "The evidence reaches an investigator. An inquiry begins."
                : "The ministry carries on. Your booth is quiet again.";
            ending += RefugeeAdmitted ? "\nMira and her family are safe." : "\nYou still remember Mira at the shutter.";
            return ending + "\nRecord: " + TotalCorrect + "/" + TotalProcessed + " correct. Savings: " + Balance + " credits.";
        }
    }
}
