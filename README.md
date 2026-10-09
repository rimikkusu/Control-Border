# Border Control

An ordinary C# Windows Forms game targeting the project's original .NET Framework
4.7.2. Open Border Control.slnx in Visual Studio and run the project.
There are no external packages or generated/downloaded art.

The supplied project was an empty Form1. This implementation adds the ten-day
campaign, daily rules, accounting and scripted visitors described in the brief.

## Play

Each shift starts automatically at launch and after the previous day's report.
The daily bulletin still opens so you can read the rules. A WinForms Timer lifts
your MetalPanel.png layer. Travelers change only while the shutter fully covers
the window. Decision and document hotspots are locked during transitions.
The square on the wall is decorative; the table is in front of all lobby layers.

Click a document on the table to open its movable, modeless Form.
Clicking again focuses that window; closing it lets you reopen it. All documents
close when the traveler leaves. A blank desk slot means the document was not
provided; use the current rules to decide whether it is required.

Click CHECK on the table, then click the same kind of field in two document windows.
The selected Labels turn yellow. The machine reports MATCH or MISMATCH for those
values only. It refuses unrelated field types and selections within one document.
Click another field to start another pair, or click the machine's x corner.
Closing a selected window clears the selection. Dates and rules remain your job.

Each correct decision pays 6 credits. The first two daily errors are warnings;
later errors cost 10 credits, rising to 15 on day 9. Rent, food and heating cost
35 credits daily. Earnings and fines are settled once in the printed report.
A negative balance is permitted, so the school project always reaches day 10.
Time advances from 08:00 to 18:00 as you process the finite daily queue.
Continue from the report and read the next bulletin while its shift starts automatically.

Scripted visitors: Mira on day 3, a worker on day 5, a courier on day 9,
and an investigator on day 10. Their choices affect the final story.
Most random visitors are valid; approximately 35% receive a document problem.
New checks accumulate across the campaign. The rulebook states each active rule.

## Source files and Toolbox

- Form1.cs / Form1.Designer.cs: standard Form, compact status Labels and header,
  document window ownership, Timer animation and shift transitions.
- LobbyPanel.cs: a double-buffered WinForms Panel that draws your five original
  PNG layers, traveler and desk objects with correctly scaled mouse hotspots.
- PassportForm, EntryPermitForm, WorkPermitForm, HealthCertificateForm:
  ordinary Forms with their own Designer files and clickable Label fields.
- BulletinForm and ShiftReportForm: ordinary Forms with read-only TextBoxes.
- GameData.cs: traveler and document data.
- TravelerGenerator.cs: consistent random documents, problems and special visitors.
- DailyRules.cs: bulletins and validation, called after a decision.
- GameSession.cs: the ten-day campaign, accounting and story outcomes.
- InspectionController.cs: shared field comparison and selection cleanup.
- GameArt.cs and Assets/README.md: optional images and rectangle placeholders.

Forms and their layout controls remain editable through the actual WinForms
Toolbox and Designer. The lobby artwork is composited inside LobbyPanel to keep
PNG transparency correct. Its small hotspot rectangles use the original 450 x
280 coordinates. See ControlBorderArt/Lobby/README.md to replace or add layers.

## Verification

Tests/GameplayChecks.cs is a standalone test runner, not game code. It checks
rule introduction, missing and expired documents, mismatches, warnings/fines,
idempotent settlement, generator consistency, Designer loading, document
lifecycle and real Timer transitions through the ten-day UI campaign.
Tests/Run-Checks.ps1 builds the project and runner using installed Visual Studio
tools. It uses no test framework or NuGet packages.

Debug and Release builds work with Visual Studio 2022 MSBuild on this machine.
The installed Visual Studio 18 also launches the game through F5. Its command-line
compiler encountered a PATH/Path environment collision, so the check script
defaults to the working 2022 tools.

The original Toolbox, booth Designer, shutter and passport window were inspected
in the running desktop interface. The new layered scene has pixel checks for
transparency, depth, shutter masking, nearest-neighbor scaling and resized hits.
The subsequent live desktop check was stopped by the user with physical Escape.
The test runner writes its latest results to Tests/latest-result.txt.
