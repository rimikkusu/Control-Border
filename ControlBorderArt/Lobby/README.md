# Original lobby layers

All five source files are PNGs with RGBA channels and the same 450 x 280 canvas.
Keep their transparent margins and align them at (0, 0). The originals are used
directly; they are never flattened, recolored or overwritten by the game.

Rendering order in LobbyPanel.OnPaint:

1. BackGround.png -- fully opaque environment.
2. Existing traveler placeholder -- behind the glass, shutter and counter.
3. GlassPanel.png -- its original alpha is 51/255 (20%) with a clear round opening.
4. MetalPanel.png -- independently animated shutter; only its Y position changes.
5. Walls.png -- left/right frame, including the decorative square.
6. Table.png -- foremost lobby layer, covering the lower walls and traveler.
7. Small temporary document, matcher, stamp and rulebook objects on the table.

The square on the wall is decorative and has no click action or tooltip.
Shifts start automatically at launch and after the previous day's report.
The shutter closes and reopens automatically between travelers, then stays
closed after the final traveler. A NEW CAMPAIGN button appears after day 10.
The rulebook is the small RULES book on the right side of the table.
CHECK toggles field inspection; its x corner cancels the current selection.

Replace an image by keeping its filename and the full 450 x 280 canvas. Rebuild
and restart. The project links this folder and copies its PNGs to both Debug and
Release output; you do not need to move your source files into bin.

To add a layer, add its exported transparent PNG, add its filename in
LobbyPanel.LoadArtwork, and call DrawLayer in OnPaint at the appropriate depth.
The optional traveler and document images still use Assets/Characters and
Assets/Documents. Their aspect ratios are retained.

Hotspot rectangles and the traveler position are in ORIGINAL canvas pixels in
LobbyPanel.cs. Drawing and mouse clicks share the same scale and centered origin.
The entire scene uses integer scaling whenever it fits, and nearest-neighbor
scaling when the window is too small for 1x. Transparent layers are composited
in one double-buffered WinForms Panel.

The source images are loaded once and cloned, releasing file locks. All cached
images, the tooltip and the small display font are disposed with the Panel.
