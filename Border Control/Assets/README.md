# Replaceable art

The lobby now uses your original layers in ../../ControlBorderArt/Lobby.
Its README explains layer order and replacement. The old Backgrounds/Booth,
Backgrounds/Desk and Shutter/MetalShutter slots below describe the initial
placeholder interface and are no longer the active lobby layers.
Character and document image slots are still active.

The game works with no image or audio files. Place your PNGs here, then rebuild.
Existing PNGs are copied to the output's Assets folder automatically.
PictureBoxes keep the image's aspect ratio and use nearest-neighbor rendering.
Unreadable or missing images fall back to simple drawn rectangles.
Restart the game after changing artwork.

Draw these first:

1. Shutter/MetalShutter.png -- the moving metal shutter (678 x 276 display area).
2. Characters/TravelerPlaceholder.png -- shared portrait, about 114 x 174.
3. Documents/Passport.png, EntryPermit.png, WorkPermit.png, HealthCertificate.png
   -- document objects on the desk (124 x 94 display areas).
4. Backgrounds/Booth.png -- view through the window (678 x 276).
5. Backgrounds/Desk.png -- wood or metal desk (1066 x 200).

Those are display areas, not required image resolutions. Smaller pixel art works.
Use transparent PNG backgrounds for portraits and desk documents if useful.
The detailed document windows are ordinary editable Labels and PictureBoxes;
keep their fields readable rather than flattening the whole document into art.

Reserved for later:

- Documents/IdentityCard.png (optional; identity cards are not a campaign rule).
- UI/ApproveStamp.png, DenyStamp.png, MatchingMachine.png, DailyBulletin.png.
- Audio/Shutter.wav, Stamp.wav, Paper.wav, NextTraveler.wav.

Reserved files currently have no gameplay effect. You can style their actual
Button, Panel and document Form controls in the Visual Studio Designer.
No art is downloaded or generated.
