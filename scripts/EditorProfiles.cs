//-----------------------------------------------------------------------------
// Torque Game Engine
// 
// Copyright (c) 2001 GarageGames.Com
// Portions Copyright (c) 2001 by Sierra Online, Inc.
//-----------------------------------------------------------------------------

//--------------------------------------------------------------------------

$Gui::fontCacheDirectory = expandFilename("./cache");
$Gui::clipboardFile      = expandFilename("./cache/clipboard.gui");

// GuiDefaultProfile is a special case, all other profiles are initialized
// to the contents of this profile first then the profile specific
// overrides are assigned.

if(!isObject(GuiDefaultProfile)) new GuiControlProfile (GuiDefaultProfile)
{
   tab = false;
   canKeyFocus = false;
   hasBitmapArray = false;
   mouseOverSelected = false;

   // fill color
   opaque = false;
   fillColor = ($platform $= "macos") ? "211 211 211" : "192 192 192";
   fillColorHL = ($platform $= "macos") ? "244 244 244" : "220 220 220";
   fillColorNA = ($platform $= "macos") ? "244 244 244" : "220 220 220";

   // border color
   border = false;
   borderColor   = "0 0 0";
   borderColorHL = "128 128 128";
   borderColorNA = "64 64 64";

   // font
   fontType = "Arial";
   fontSize = 14;

   fontColor = "0 0 0";
   fontColorHL = "32 100 100";
   fontColorNA = "0 0 0";
   fontColorSEL= "200 200 200";

   // bitmap information
   bitmap = "gui/darkWindow";
   bitmapBase = "";
   textOffset = "0 0";

   // used by guiTextControl
   modal = true;
   justify = "left";
   autoSizeWidth = false;
   autoSizeHeight = false;
   returnTab = false;
   numbersOnly = false;
   cursorColor = "0 0 0 255";

   // sounds
   soundButtonDown = "";
   soundButtonOver = "";
};

if(!isObject(GuiInputCtrlProfile)) new GuiControlProfile( GuiInputCtrlProfile )
{
   tab = true;
	canKeyFocus = true;
};

if(!isObject(GuiDialogProfile)) new GuiControlProfile(GuiDialogProfile);


if(!isObject(GuiSolidDefaultProfile)) new GuiControlProfile (GuiSolidDefaultProfile)
{
   opaque = true;
   border = true;
};

if(!isObject(GuiWindowProfile)) new GuiControlProfile (GuiWindowProfile)
{
   opaque = true;
   border = 2;
   fillColor = ($platform $= "macos") ? "211 211 211" : "192 192 192";
   fillColorHL = ($platform $= "macos") ? "190 255 255" : "64 150 150";
   fillColorNA = ($platform $= "macos") ? "255 255 255" : "150 150 150";
   fontColor = ($platform $= "macos") ? "0 0 0" : "255 255 255";
   fontColorHL = ($platform $= "macos") ? "200 200 200" : "0 0 0";
   text = "GuiWindowCtrl test";
   bitmap = "gui/darkWindow";
   textOffset = ($platform $= "macos") ? "5 5" : "6 6";
   hasBitmapArray = true;
   justify = ($platform $= "macos") ? "center" : "left";
};

if(!isObject(EditorToolButtonProfile)) new GuiControlProfile (EditorToolButtonProfile)
{
   opaque = true;
   border = 2;
};

if(!isObject(GuiContentProfile)) new GuiControlProfile (GuiContentProfile)
{
   opaque = true;
   fillColor = "255 255 255";
};

if(!isObject(GuiModelessDialogProfile)) new GuiControlProfile("GuiModelessDialogProfile")
{
   modal = false;
};

if(!isObject(GuiButtonProfile)) new GuiControlProfile (GuiButtonProfile)
{
   opaque = true;
   border = true;
   fontColor = "0 0 0";
   fontColorHL = "32 100 100";
   fixedExtent = true;
   justify = "center";
	canKeyFocus = false;
};

if(!isObject(GuiBorderButtonProfile)) new GuiControlProfile (GuiBorderButtonProfile)
{
   fontColorHL = "0 0 0";
};

if(!isObject(GuiMenuBarProfile)) new GuiControlProfile (GuiMenuBarProfile)
{
   fontType = "Arial";
   fontSize = 14;
   opaque = true;
   fillColor = "192 192 192";
   fillColorHL = "220 220 220";
   fillColorNA = "220 220 220";
   fillColorHL = "0 0 96";
   border = 4;
   fontColor = "0 0 0";
   fontColorHL = "255 255 255";
   fontColorNA = "128 128 128";
   fixedExtent = true;
   justify = "center";
   canKeyFocus = false;
   mouseOverSelected = true;
   //bitmap = ($platform $= "macos") ? "./osxMenu" : "./torqueMenu";
   //hasBitmapArray = true;
};

if(!isObject(GuiButtonSmProfile)) new GuiControlProfile (GuiButtonSmProfile)
{
   fontSize = 14;
};

if(!isObject(GuiRadioProfile)) new GuiControlProfile (GuiRadioProfile)
{
   fontSize = 14;
   fillColor = "232 232 232";
   fontColorHL = "32 100 100";
   fixedExtent = true;
   bitmap = "gui/darkWindow";
   hasBitmapArray = true;
};

if(!isObject(GuiScrollProfile)) new GuiControlProfile (GuiScrollProfile)
{
   opaque = true;
   fillColor = "255 255 255";
   border = 3;
   borderThickness = 2;
   borderColor = "0 0 0";
   bitmap = "gui/darkScroll";
   hasBitmapArray = true;
};

if(!isObject(GuiSliderProfile)) new GuiControlProfile (GuiSliderProfile);


if(!isObject(PainterTextProfile)) new GuiControlProfile (PainterTextProfile)
{
   fontType = "Univers Condensed";
   fontSize = 16;
   fontColor = "0 0 0";
   fontColorLink = "255 96 96";
   fontColorLinkHL = "0 0 255";
   autoSizeWidth = true;
   autoSizeHeight = true;
};

if(!isObject(GuiTextProfile)) new GuiControlProfile (GuiTextProfile)
{
   fontColor = "0 0 0";
   fontColorLink = "255 96 96";
   fontColorLinkHL = "0 0 255";
   autoSizeWidth = true;
   autoSizeHeight = true;
};

if(!isObject(EditorTextProfile)) new GuiControlProfile (EditorTextProfile)
{
   fontType = "Arial Bold";
   fontColor = "0 0 0";
   autoSizeWidth = true;
   autoSizeHeight = true;
};

if(!isObject(EditorTextProfileWhite)) new GuiControlProfile (EditorTextProfileWhite)
{
   fontType = "Arial Bold";
   fontColor = "255 255 255";
   autoSizeWidth = true;
   autoSizeHeight = true;
};

if(!isObject(GuiMediumTextProfile)) new GuiControlProfile (GuiMediumTextProfile)
{
   fontType = "Arial Bold";
   fontColor = "0 0 0";
   autoSizeWidth = true;
   autoSizeHeight = true;
   fontSize = 24;
};

if(!isObject(GuiBigTextProfile)) new GuiControlProfile (GuiBigTextProfile)
{
   fontType = "Arial Bold";
   fontColor = "0 0 0";
   autoSizeWidth = true;
   autoSizeHeight = true;
   fontSize = 36;
};

if(!isObject(GuiCenterTextProfile)) new GuiControlProfile (GuiCenterTextProfile)
{
   fontType = "Arial Bold";
   fontColor = "0 0 0";
   autoSizeWidth = true;
   autoSizeHeight = true;
   justify = "center";
};

if(!isObject(MissionEditorProfile)) new GuiControlProfile (MissionEditorProfile)
{
   canKeyFocus = true;
};

if(!isObject(EditorScrollProfile)) new GuiControlProfile (EditorScrollProfile)
{
   opaque = true;
   fillColor = "192 192 192 192";
   border = 3;
   borderThickness = 2;
   borderColor = "0 0 0";
   bitmap = "gui/darkScroll";
   hasBitmapArray = true;
};

if(!isObject(GuiTextEditProfile)) new GuiControlProfile (GuiTextEditProfile)
{
   opaque = true;
   fillColor = "255 255 255";
   fillColorHL = "128 128 128";
   border = 3;
   borderThickness = 2;
   borderColor = "0 0 0";
   fontColor = "0 0 0";
   fontColorHL = "255 255 255";
   fontColorNA = "128 128 128";
   textOffset = "0 2";
   autoSizeWidth = false;
   autoSizeHeight = true;
   tab = true;
   canKeyFocus = true;
};

if(!isObject(GuiControlListPopupProfile)) new GuiControlProfile (GuiControlListPopupProfile)
{
   opaque = true;
   fillColor = "255 255 255";
   fillColorHL = "128 128 128";
   border = true;
   borderColor = "0 0 0";
   fontColor = "0 0 0";
   fontColorHL = "255 255 255";
   fontColorNA = "128 128 128";
   textOffset = "0 2";
   autoSizeWidth = false;
   autoSizeHeight = true;
   tab = true;
   canKeyFocus = true;
   bitmap = "gui/darkScroll";
   hasBitmapArray = true;
};

if(!isObject(GuiTextArrayProfile)) new GuiControlProfile (GuiTextArrayProfile)
{
   fontType = "Arial Bold";
   fontColor = "0 0 0";
   autoSizeWidth = true;
   autoSizeHeight = true;
   fontColorHL = "32 100 100";
   fillColorHL = "200 200 200";
};

if(!isObject(GuiTextListProfile)) new GuiControlProfile (GuiTextListProfile)
{
   fontType = "Arial Bold";
   fontColor = "0 0 0";
   autoSizeWidth = true;
   autoSizeHeight = true;

} ;

if(!isObject(GuiTreeViewProfile)) new GuiControlProfile (GuiTreeViewProfile)
{
   fontSize = 13;  // dhc - trying a better fit...
   fontColor = "0 0 0";
   fontColorHL = "64 150 150";
};

if(!isObject(GuiCheckBoxProfile)) new GuiControlProfile (GuiCheckBoxProfile)
{
   opaque = false;
   fillColor = "232 232 232";
   border = false;
   borderColor = "0 0 0";
   fontSize = 14;
   fontColor = "0 0 0";
   fontColorHL = "32 100 100";
   fixedExtent = true;
   justify = "left";
   bitmap = "gui/darkScroll";
   hasBitmapArray = true;

};

if(!isObject(GuiPopUpMenuProfile)) new GuiControlProfile (GuiPopUpMenuProfile)
{
   opaque = true;
   mouseOverSelected = true;

   border = 4;
   borderThickness = 2;
   borderColor = "0 0 0";
   fontSize = 14;
   fontColor = "0 0 0";
   fontColorHL = "32 100 100";
   fontColorSEL = "32 100 100";
   fixedExtent = true;
   justify = "center";
   bitmap = "gui/darkScroll";
   hasBitmapArray = true;
};

if(!isObject(GuiEditorClassProfile)) new GuiControlProfile (GuiEditorClassProfile)
{
   opaque = true;
   fillColor = "232 232 232";
   border = true;
   borderColor   = "0 0 0";
   borderColorHL = "127 127 127";
   fontColor = "0 0 0";
   fontColorHL = "32 100 100";
   fixedExtent = true;
   justify = "center";
   bitmap = "gui/darkScroll";
   hasBitmapArray = true;
};


if(!isObject(LoadTextProfile)) new GuiControlProfile ("LoadTextProfile")
{
   fontColor = "66 219 234";
   autoSizeWidth = true;
   autoSizeHeight = true;
};


if(!isObject(GuiMLTextProfile)) new GuiControlProfile ("GuiMLTextProfile")
{
   fontColorLink = "255 96 96";
   fontColorLinkHL = "0 0 255";
};

if(!isObject(GuiMLTextEditProfile)) new GuiControlProfile (GuiMLTextEditProfile) 
{ 
   fontColorLink = "255 96 96"; 
   fontColorLinkHL = "0 0 255"; 

   fillColor = "255 255 255"; 
   fillColorHL = "128 128 128"; 

   fontColor = "0 0 0"; 
   fontColorHL = "255 255 255"; 
   fontColorNA = "128 128 128"; 

   autoSizeWidth = true; 
   autoSizeHeight = true; 
   tab = true; 
   canKeyFocus = true; 
}; 

//--------------------------------------------------------------------------
// Console Window

if(!isObject(GuiConsoleProfile)) new GuiControlProfile ("GuiConsoleProfile")
{
   fontType = ($platform $= "macos") ? "Courier New" : "Lucida Console";
   fontSize = 12;
   fontColor = "0 0 0";
   fontColorHL = "130 130 130";
   fontColorNA = "255 0 0";
   fontColors[6] = "50 50 50";
   fontColors[7] = "50 50 0";  
   fontColors[8] = "0 0 50"; 
   fontColors[9] = "0 50 0";   
};


if(!isObject(GuiProgressProfile)) new GuiControlProfile ("GuiProgressProfile")
{
   opaque = false;
   fillColor = "44 152 162 100";
   border = true;
   borderColor   = "78 88 120";
};

if(!isObject(GuiProgressTextProfile)) new GuiControlProfile ("GuiProgressTextProfile")
{
   fontColor = "0 0 0";
   justify = "center";
};



//--------------------------------------------------------------------------
// Gui Inspector

if(!isObject(GuiInspectorTextEditProfile)) new GuiControlProfile ("GuiInspectorTextEditProfile")
{
   opaque = true;
   fillColor = "255 255 255";
   fillColorHL = "128 128 128";
   border = true;
   borderColor = "0 0 0";
   fontColor = "0 0 0";
   fontColorHL = "255 255 255";
   autoSizeWidth = false;
   autoSizeHeight = true;
   tab = false;
   canKeyFocus = true;
};

//-------------------------------------- Cursors
//
if(!isObject(DefaultCursor)) new GuiCursor(DefaultCursor)
{
   hotSpot = "1 1";
   bitmapName = "gui/CUR_3darrow";
};

