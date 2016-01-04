//Was used for version 1 and 2.
//if(!isObject(SICatagoryTextProfile))
//	new GuiControlProfile (SICatagoryTextProfile)
//	{
		// font = "Arial Bold";
		// fontSize = 20;
		// fontColor = "200 200 200";
		// fontColorNA = "200 200 0";
		// justify = "center";
	// };

if(!isObject(RPGSkillInfo))
	exec("gui/RPGSkillInfo/RPGSkillInfoGUIv3.gui");

function RPGSkillInfo::updateSkillScreen(%this,%id)
{
	//%cmd = $csRPGSkill::skill[%id,Command];
	//%cat = $csRPGSkill::skill[%id,Catagory];
	//echo(%cmd SPC %cat);
	%text = "";
	%cat = %cat = $csRPGSkill::catagoryName[%this.selectedCat];
	for(%i = 0; %i < $csRPGSkill::catagoryUILineCount[%cat]; %i++)
	{
		//Case 1:If %tag is left blank, then line will simply be %info.
		//Case 2:If %tag is not blank, then the resulting line will be %info concat with the skill's UI tag in %tag.
		//Case 3:%tag and %info are both blank: New line.
		//Case 4:%info blank, but %tag is not blank.  Data from %tag is simply displayed.
		%info = $csRPGSkill::catagoryUILine[%cat,%i];
		%tag = $csRPGSkill::catagoryUILine::dataTag[%cat,%i];
		%sid = $csRPGSkill::catagorySkillCmd[%cat,%id];
		if(%info !$= "")
		{
			if(%tag $= "") //Case 1
			{
				if(%text $= "")
					%text = %info NL "";
				else
					%text = %text @ %info NL "";
			}
			else //Case 2
			{
				if(%text $= "")
					%text = %info @ $csRPGSkill::skill[%sid,UI,%tag] NL "";
				else
					%text = %text @ %info @ $csRPGSkill::skill[%sid,UI,%tag] NL "";
			}
		}
		else
		{
			if(%tag $= "") //Case 3
			{
				%text = %text NL ""; //Don't care if %text is "" or not.
			}
			else  //Case 4
			{
				if(%text $= "")
					%text = $csRPGSkill::skill[%sid,UI,%tag] NL "";
				else
					%text = %text @ $csRPGSkill::skill[%sid,UI,%tag] NL "";
			}
		}
	}
	echo(%text);
	SISkillInfoTextBox.setValue(%text);
}

//Old
// function RPGSkillInfo::updateSkillScreen(%this,%id)
// {
	// // %text = "<font:Verdana Bold:16><color:ffcc00><just:center>"@$RPGSkill::skill[%id,Name] NL "";
	// // %text = %text @ "<just:left><font:Arial Bold:14><color:F5F5DC>Description:" NL "<font:Arial:14>"@$csRPGSkill::skill[%id,Desc] NL "" NL "";
	// // %text = %text @ "Command:" SPC $csRPGSkill::skill[%id,Command] NL "";
	// // %text = %text @ "Type:" SPC $RPGSkill[$csRPGSkill::skill[%id,Type]] NL "" NL "";
	// // %text = %text @ "<font:Arial Bold:14>Skill Requirements:" NL "<font:Arial:14>" @ InterpretSkillReqs($csSkillRestriction[$csRPGSkill::skill[%id,Command]]) NL "" NL "";
	// // %text = %text @ "<font:Arial Bold:14>Example:" NL "<font:Arial:14>" @ $csRPGSkill::skill[%id,Example];
	
	// %cmd = $csRPGSkill::skill[%id,Command];
	// %cat = $csRPGSkill::skill[%id,Catagory];
	// echo(%cmd SPC %cat);
	// %text = "";
	// for(%i = 0; %i < $csRPGSkill::catagoryUILineCount[%cat]; %i++)
	// {
		// //Case 1:If %tag is left blank, then line will simply be %info.
		// //Case 2:If %tag is not blank, then the resulting line will be %info concat with the skill's UI tag in %tag.
		// //Case 3:%tag and %info are both blank: New line.
		// //Case 4:%info blank, but %tag is not blank.  Data from %tag is simply displayed.
		// %info = $csRPGSkill::catagoryUILine[%cat,%i];
		// %tag = $csRPGSkill::catagoryUILine::dataTag[%cat,%i];
		
		// if(%info !$= "")
		// {
			// if(%tag $= "") //Case 1
			// {
				// if(%text $= "")
					// %text = %info NL "";
				// else
					// %text = %text @ %info NL "";
			// }
			// else //Case 2
			// {
				// if(%text $= "")
					// %text = %info @ $csRPGSkill::skill[%id,UI,%tag] NL "";
				// else
					// %text = %text @ %info @ $csRPGSkill::skill[%id,UI,%tag] NL "";
			// }
		// }
		// else
		// {
			// if(%tag $= "") //Case 3
			// {
				// %text = %text NL ""; //Don't care if %text is "" or not.
			// }
			// else  //Case 4
			// {
				// if(%text $= "")
					// %text = $csRPGSkill::skill[%id,UI,%tag] NL "";
				// else
					// %text = %text @ $csRPGSkill::skill[%id,UI,%tag] NL "";
			// }
		// }
	// }
	// echo(%text);
	// SISkillInfoTextBox.setValue(%text);
// }

function InterpretSkillReqs(%reqs)
{
	%txt = "";
	for(%i = 0; getWord(%reqs,%i) !$= ""; %i+=2)
	{
		%skill = $RPGSkill[getWord(%reqs,%i)];
		echo(%skill);
		%amt = getWord(%reqs,%i+1);
		if(%txt $= "")
			%txt = %skill SPC %amt;
		else
			%txt = %txt @ "," SPC %skill SPC %amt;
	}
	return %txt;
}

function RPGSkillInfo::populateSkillList(%this,%catId)
{
	SISkillList.clear();
	%cat = $csRPGSkill::catagoryName[%catId];
	//echo("Populating skill list...");
	for(%i = 0; $csRPGSkill::catagorySkillCmd[%cat,%i] !$= ""; %i++)
	{
		%id = $csRPGSkill::catagorySkillCmd[%cat,%i];
		//echo("Adding skill:" SPC $csRPGSkill::skill[%id,Name]);
		SISkillList.addRow(%i,$csRPGSkill::skill[%id,UI,Name]);
	}
	//echo("Done Populating skill list.");
}

function RPGSkillInfo::onWake(%this)
{
	if(%this.selectedCat $= "")
		%this.selectedCat = 0; //Start with the first skill catagory.
	
	//%catTxt = $csRPGSkill::catagoryDesc[%this.selectedCat]; //$csRPGSkill::catagoryDesc[%this.selectedCat]; //"<font:Verdana Bold:16><color:ffcc00>" @ $csRPGSkill::catagoryDesc[%this.selectedCat];
	//SISkillCatagoryText.setText(%catTxt);
	
	if(SICatagoryDropDown.size() == 0)
	{
		for(%i = 0; $csRPGSkill::catagoryDesc[%i] !$= ""; %i++)
		{
			SICatagoryDropDown.add($csRPGSkill::catagoryDesc[%i],%i);
		}
	}
	SICatagoryDropDown.setSelected(%this.selectedCat);
	%this.populateSkillList(%this.selectedCat);
}

function RPGSkillInfo::onSleep(%this)
{
	
}

function SISkillList::onSelect(%this, %itemId, %text)
{
	SISkillList.SelectedText = %text;
	RPGSkillInfo.updateSkillScreen(%itemId);
}

function SICatagoryDropDown::onSelect(%this, %id, %text)
{
	RPGSkillInfo.selectedCat = %id;
	RPGSkillInfo.populateSkillList(%id);
}