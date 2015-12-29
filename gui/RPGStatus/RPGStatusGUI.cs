<<<<<<< HEAD
=======
//Not used.
>>>>>>> origin/development
new GuiControlProfile("HPDisplayProfile")
{
   fontType = "Univers";
   fontSize = 16;
   fontColor = "200 200 200";
   autoSizeWidth = true;
   autoSizeHeight = true;
   fillColor = "255 255 0";
   opaque = "true";
};
new GuiControlProfile("MPDisplayProfile")
{
   fontType = "Univers";
<<<<<<< HEAD
   fontSize = 12;
=======
   fontSize = 16;
>>>>>>> origin/development
   fontColor = "200 200 0";
   autoSizeWidth = true;
   autoSizeHeight = true;
   fillColor = "255 255 0";
   opaque = "true";
};

//Executed in client.cs PlayGUI::onWake
function RPGS_HP_Text::onWake(%this)
{
	CommandToServer('CheckUpdateClientData');
	%this.lastmanacheck = getTime();
	%this.update();
}

function RPGS_HP_Text::update(%this)
{
	//$RPGS::HPText = "HP:" SPC $RPGClientData["HP"] @"/"@ $RPGClientData["MaxHP"] SPC "MP:" SPC $RPGClientData["MANA"] @"/"@ $RPGClientData["MaxMANA"];
	//$RPGS::MPText = ;
	if(getTime() - %this.lastmanacheck > 5)
	{
		CommandToServer('fetchdata',"MANA");
		%this.lastmanacheck = getTime();
	}
	
	%this.setValue("HP:" SPC $RPGClientData["HP"] @"/"@ $RPGClientData["MaxHP"]);
	RPGS_MP_Text.setValue("MP:" SPC $RPGClientData["MANA"] @"/"@ $RPGClientData["MaxMANA"]);
	$RPGS::UpdateLoop = %this.schedule(2000,"update");
}

function RPGS_HP_Text::onSleep(%this) {
	// echo("Sleep Called.");
	cancel($RPGS::UpdateLoop);
}

//Now apart of playgui
//if(!isObject(RPGStatusGUI))
//   exec("gui/RPGStatus/RPGStatusGui.gui");

//These are our two basic open/close functions
// function RPGSG_OpenGUI() {
   // %pos = hudClusterBack.position;
   // %posx = GetWord(%pos,0);
   // RPGS_Frame.position = %posx SPC GetWord(%pos,1) + 50;
   // Canvas.PushDialog(RPGStatusGUI);
// }

// function RPGSG_CloseGUI() {
   // Canvas.PopDialog(RPGStatusGUI);
// }
// function RPGStatusGUI::onWake(%this)
// {
	// CommandToServer('CheckUpdateClientHPMP');
	// CommandToServer('CheckUpdateClientData');
	// RPGS_HP_Text.text = "HP:";
	// RPGS_MP_Text.text = "MP:";
	
	// CommandToServer('fetchData',"MaxHP");
	// CommandToServer('fetchData',"MaxMANA");
	// %this.lastMAXupdate = getTime();
	// %this.lastupdate = getTime();
	
	
	// $RPGS::UpdateLoop = "";
	// $RPGS::HPText = "";
	// $RPGS::MPText = "";
	// if($RPGServerData["UpdateData"])
		// %this.update();
	// else
		// Canvas.PopDialog(RPGStatusGUI); //Close the gui.
		
// }

// function RPGStatusGUI::update(%this)
// {	
	// $RPGS::HPText = "HP:" SPC $RPGClientData["HP"] @"/"@ $RPGClientData["MaxHP"];
	// $RPGS::MPText = "MP:" SPC $RPGClientData["MANA"] @"/"@ $RPGClientData["MaxMANA"];
	// $RPGS::UpdateLoop = %this.schedule(2000,"update");
// }

// function RPGStatusGUI::onSleep(%this) {
	// echo("Sleep Called.");
	// cancel($RPGS::UpdateLoop);
// }