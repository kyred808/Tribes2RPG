if(!isObject(RPGSkillInfo))
	exec("gui/RPGSkillInfo/RPGSkillInfoGUI.gui");

//if(!isObject(SICatagoryTextProfile))
	new GuiControlProfile (SICatagoryTextProfile)
	{
		font = "Arial Bold";
		fontSize = 20;
		fontColor = "200 200 200";
		fontColorNA = "200 200 0";
		justify = "center";
	};

function RPGSkillInfo::updateSkillScreen(%this,%id)
{
	%text = "<font:Verdana Bold:16><color:ffcc00><just:center>"@$RPGSkill::skill[%id,Name] NL "";
	%text = %text @ "<just:left><font:Arial Bold:14><color:F5F5DC>Description:" NL "<font:Arial:14>"@$csRPGSkill::skill[%id,Desc] NL "" NL "";
	%text = %text @ "Command:" SPC $csRPGSkill::skill[%id,Command] NL "";
	%text = %text @ "Type:" SPC $RPGSkill[$csRPGSkill::skill[%id,Type]] NL "" NL "";
	%text = %text @ "<font:Arial Bold:14>Skill Requirements:" NL "<font:Arial:14>" @ InterpretSkillReqs($csSkillRestriction[$csRPGSkill::skill[%id,Command]]) NL "" NL "";
	%text = %text @ "<font:Arial Bold:14>Example:" NL "<font:Arial:14>" @ $csRPGSkill::skill[%id,Example];
	
	
	SISkillInfoTextBox.setValue(%text);
}

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

function RPGSkillInfo::populateSkillList(%this)
{
	SISkillList.clear();
	%cat = $csRPGSkill::catagoryName[%this.selectedCat];
	for(%i = 0; $csRPGSkill::catagorySkillCmd[%cat,%i] !$= ""; %i++)
	{
		%id = $csRPGSkill::catagorySkillCmd[%cat,%i];
		SISkillList.addRow(%i,$csRPGSkill::skill[%id,Name]);
	}
}

function RPGSkillInfo::onWake(%this)
{
	%this.selectedCat = 0; //Start with basic.
	%catTxt = $csRPGSkill::catagoryDesc[%this.selectedCat]; //$csRPGSkill::catagoryDesc[%this.selectedCat]; //"<font:Verdana Bold:16><color:ffcc00>" @ $csRPGSkill::catagoryDesc[%this.selectedCat];
	SISkillCatagoryText.setText(%catTxt);
	%this.populateSkillList();
}

function RPGSkillInfo::onSleep(%this)
{
	
}

function SISkillList::onSelect(%this, %itemId, %text)
{
	SISkillList.SelectedText = %text;
	RPGSkillInfo.updateSkillScreen(%itemId);
}

//For when the select button is pushed.
function RPGSkillInfo::onSelectCatagory(%this)
{
	
}