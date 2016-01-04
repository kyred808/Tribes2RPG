RPGSkill::AddSkillCatagory("command","RPG Commands");

RPGSkill::AddSkill("#savecharacter","Save Character","command","cmd","RPGCommandSaveCharacter");
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

RPGSkill::AddSkill("#dropcoins","Drop Coins","command","cmd","RPGCommandDropCoins");
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

