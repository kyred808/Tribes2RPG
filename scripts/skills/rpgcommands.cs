RPGSkill::AddSkillCatagory("command","RPG Commands");
RPGSkill::catagoryAddUITag("command","Name");
RPGSkill::catagoryAddUITag("command","Desc");
RPGSkill::catagoryAddUITag("command","Command");
RPGSkill::catagoryAddUITag("command","Example");

RPGSkill::catagoryUIInfoLine("command",0,"<font:Verdana Bold:16><color:ffcc00><just:center>","Name");
RPGSkill::catagoryUIInfoLine("command",1,"<just:left><font:Arial Bold:14><color:F5F5DC>Description:","");
RPGSkill::catagoryUIInfoLine("command",2,"<font:Arial:14>","Desc");
RPGSkill::catagoryUIInfoLine("command",3,"","");
RPGSkill::catagoryUIInfoLine("command",4,"<font:Arial:14>Command: ","Command");
RPGSkill::catagoryUIInfoLine("command",5,"","");
RPGSkill::catagoryUIInfoLine("command",6,"","");
RPGSkill::catagoryUIInfoLine("command",7,"<font:Arial Bold:14>Example:","");
RPGSkill::catagoryUIInfoLine("command",8,"<font:Arial:14>","Example");


RPGSkill::AddSkill("#savecharacter","command","cmd","RPGCommandSaveCharacter");
//RPGSkill::AddSkill("#savecharacter","Save Character","command","cmd","RPGCommandSaveCharacter");
RPGSkill::addUIInfo("#savecharacter","Command","#savecharacter");
RPGSkill::SetName("#savecharacter","Save Character");
RPGSkill::SetDesc("#savecharacter","Manually save your character on the server. This server does this peroidically and also automatically when you disconnect. Some admins can also use this to save another players character.");
RPGSkill::SetExample("#savecharacter","#savecharacter");
$SkillRestriction["#savecharacter"] = "none 0";

function RPGCommandSaveCharacter(%TrueClientId,%cmd,%cropped,%adminLvl)
{
	if(%adminLvl >= 4)
	{
		if(%cropped $= "")
		{
			%r = SaveCharacter(%TrueClientId);
			messageClient(%TrueClientId, 'RPGchatCallback', "Saving self (" @ %TrueClientId @ "): success = " @ %r);
		}
		else
		{
			%id = getClientByName(%cropped);
			if(%id)
			{
				%r = SaveCharacter(%id);
				messageClient(%TrueClientId, 'RPGchatCallback', "Saving " @ %id.nameBase @ " (" @ %id @ "): success = " @ %r);
			}
			else
				messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
		}
	}
	else
	{
		%time = getTime();
		if(%time - %TrueClientId.lastSaveCharTime > 10)
		{
			%TrueClientId.lastSaveCharTime = %time;

			%r = SaveCharacter(%TrueClientId);
			messageClient(%TrueClientId, 'RPGchatCallback', "Saving self (" @ %TrueClientId @ "): success = " @ %r);
		}
	}
	//return;
}

RPGSkill::AddSkill("#dropcoins","command","cmd","RPGCommandDropCoins");
//RPGSkill::AddSkill("#dropcoins","Drop Coins","command","cmd","RPGCommandDropCoins");
RPGSkill::addUIInfo("#dropcoins","Command","#dropcoins");
RPGSkill::SetName("#dropcoins","Drop Coins");
RPGSkill::SetDesc("#dropcoins","Drop the specified amount of coins on the ground.");
RPGSkill::SetExample("#dropcoins","#dropcoins 234");
$SkillRestriction["#dropcoins"] = "none 0";

function RPGCommandDropCoins(%TrueClientId,%cmd,%cropped,%adminLvl)
{
	%cropped = GetWord(%cropped, 0);

	if(%cropped $= "all")
		%cropped = fetchData(%TrueClientId, "COINS");
	else
		%cropped = mfloor(%cropped);

	if(fetchData(%TrueClientId, "COINS") >= %cropped || %clientToServerAdminLevel >= 4)
	{
		if(%cropped > 0)
		{
			if( !(%clientToServerAdminLevel >= 4) )
				storeData(%TrueClientId, "COINS", %cropped, "dec");

			%toss = GetTypicalTossStrength(%TrueClientId);

			TossLootbag(%TrueClientId, "", %cropped, 0);
			RefreshAll(%TrueClientId);

			messageClient(%TrueClientId, 'RPGchatCallback', "You dropped " @ %cropped @ " coins.");
			%TrueClientId.player.playAudio(0, SoundMoney1);
		}
	}
	else
	{
		messageClient(%TrueClientId, 'RPGchatCallback', "You don't even have that many coins!");
	}
	//return;
}

