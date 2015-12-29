RPGSkill::AddSkillCatagory("Basic","Basic Skills");
//Speaking skills
//RPGSkill::AddSkill(%cmd,%name,%cat,%type,%func)
RPGSkill::AddSkill("#say","Say","Basic",$Skill::Speech,"BasicSkillsay");
RPGSkill::AddSkill("#whisper","Whisper","Basic",$Skill::Speech,"BasicSkillsay");
RPGSkill::AddSkill("#tell","Tell","Basic",$Skill::Speech,"BasicSkillsay");
RPGSkill::AddSkill("#zone","Zone Talk","Basic",$Skill::Speech,"BasicSkillsay");
RPGSkill::AddSkill("#party","Party Talk","Basic",$Skill::Speech,"BasicSkillsay");
RPGSkill::AddSkill("#shout","Shout","Basic",$Skill::Speech,"BasicSkillsay");
RPGSkill::AddSkill("#global","Global Talk","Basic",$Skill::Speech,"BasicSkillsay");
RPGSkill::AddSkill("#group","Group Talk","Basic",$Skill::Speech,"BasicSkillsay");
RPGSkill::AddSkill("#guild","Guild Talk","Basic",$Skill::Speech,"BasicSkillsay");
RPGSkill::AddSkill("#r","Tell Respond","Basic",$Skill::Speech,"BasicSkillsay");

RPGSkill::SetDesc("#say","Simple speaking ability for close communication.  Can used to talk to NPCs.");
RPGSkill::SetDesc("#whisper","Simple speaking ability for short range communication.");
RPGSkill::SetDesc("#tell","Talk privately with another player.");
RPGSkill::SetDesc("#zone","Talk to only other players in the same zone. Default for team chat.");
RPGSkill::SetDesc("#party","Talk to only other players in the same party.");
RPGSkill::SetDesc("#shout","Simple speaking ability with a longer range than Say.");
RPGSkill::SetDesc("#global","Talk to all players.  Default for chat.");
RPGSkill::SetDesc("#group","Talk to all players in the same group as you.");
RPGSkill::SetDesc("#guild","Talk to all players in the same guild.");
RPGSkill::SetDesc("#r","Respond to the player who last private messaged you.");

RPGSkill::SetExample("#say","#say <message>");
RPGSkill::SetExample("#whisper","#whisper <message>");
RPGSkill::SetExample("#tell","#tell <PlayerName>, <message>");
RPGSkill::SetExample("#zone","#zone <message>");
RPGSkill::SetExample("#party","#party <message>");
RPGSkill::SetExample("#shout","#shout <message>");
RPGSkill::SetExample("#global","#global <message>");
RPGSkill::SetExample("#group","#group <message>");
RPGSkill::SetExample("#guild","#guild <message>");
RPGSkill::SetExample("#r","#r <message>");

$SkillRestriction["#say"] = $Skill::Speech @ " 0";
$SkillRestriction["#whisper"] = $Skill::Speech @ " 0";
$SkillRestriction["#tell"] = $Skill::Speech @ " 0";
$SkillRestriction["#zone"] = $Skill::Speech @ " 0";
$SkillRestriction["#z"] = $Skill::Speech @ " 0";
$SkillRestriction["#party"] = $Skill::Speech @ " 0";
$SkillRestriction["#p"] = $Skill::Speech @ " 0";
$SkillRestriction["#shout"] = $Skill::Speech @ " 3";
$SkillRestriction["#group"] = $Skill::Speech @ " 5";
$SkillRestriction["#guild"] = $Skill::Speech @ " 10";
$SkillRestriction["#gu"] = $Skill::Speech @ " 10";
$SkillRestriction["#r"] = $Skill::Speech @ " 0";

function BasicSkillsay(%client,%cmd,%cropped,%adminLvl)
{
	%TrueClientId = %client;
	%TCsenderName = %client.rpgName;
	%botTalk = false;
	switch$(%cmd)
	{
		case "#say":
			if(%TrueClientID.player)// does player exist
			{
				
				%count = ClientGroup.getCount();
				for(%icl = 0; %icl < %count; %icl++)
				{
					%cl = ClientGroup.getObject(%icl);
					if(isobject(%cl.player))
					{
						%talkingPos = %TrueClientId.player.getPosition();
						%receivingPos = %cl.player.getPosition();
						%distVec = VectorDist(%talkingPos, %receivingPos);
						if(%distVec <= $maxSAYdistVec)
						{
							//%newmsg = FadeMsg(%cropped, %distVec, $maxSAYdistVec);
							%newmsg = %cropped;

							if(!%cl.muted[%TrueClientId] && %cl !$= %TrueClientId)
								messageClient(%cl, 'RPGchatCallback', %TCsenderName @ " says, \"" @ %newmsg @ "\"");
						}
					}
				}
				messageClient(%TrueClientId, 'RPGchatCallback', "You say, \"" @ %cropped @ "\"");
				UseSkill(%TrueClientId, $SkillSpeech, true, true);

				%botTalk = true;
			}
		case "#whisper":
				%count = ClientGroup.getCount();
				for(%icl = 0; %icl < %count; %icl++)
				{
					%cl = ClientGroup.getObject(%icl);

					%talkingPos = %TrueClientId.player.getPosition();
					%receivingPos = %cl.player.getPosition();
					%distVec = VectorDist(%talkingPos, %receivingPos);
					if(%distVec <= $maxWHISPERdistVec)
					{
						//%newmsg = FadeMsg(%cropped, %distVec, $maxWHISPERdistVec);
						%newmsg = %cropped;

						if(!%cl.muted[%TrueClientId] && %cl !$= %TrueClientId)
							messageClient(%cl, 'RPGchatCallback', %TCsenderName @ " whispers, \"" @ %newmsg @ "\"");
					}
				}
				messageClient(%TrueClientId, 'RPGchatCallback', "You whisper, \"" @ %cropped @ "\"");
				UseSkill(%TrueClientId, $SkillSpeech, true, true);

				%botTalk = true;			
		case "#shout":
			%count = ClientGroup.getCount();
			for(%icl = 0; %icl < %count; %icl++)
			{
				%cl = ClientGroup.getObject(%icl);

				%talkingPos = %TrueClientId.player.getPosition();
				%receivingPos = %cl.player.getPosition();
				%distVec = VectorDist(%talkingPos, %receivingPos);
				if(%distVec <= $maxSHOUTdistVec)
				{
					//%newmsg = FadeMsg(%cropped, %distVec, $maxSHOUTdistVec);
					%newmsg = %cropped;

					if(!%cl.muted[%TrueClientId] && %cl !$= %TrueClientId)
						messageClient(%cl, 'RPGchatCallback', %TCsenderName @ " shouts, \"" @ %newmsg @ "\"");
				}
			}
			messageClient(%TrueClientId, 'RPGchatCallback', "You shouted, \"" @ %cropped @ "\"");
			UseSkill(%TrueClientId, $SkillSpeech, true, true);

			//%botTalk = true;
		case "#tell":
			if(%cropped $= "")
			{
				messageClient(%TrueClientId, 'RPGchatCallback', "syntax: #tell whoever, message");
			}
			else
			{
				%pos1 = 0;
				%pos2 = strstr(%cropped, ",");
				%name = getsubstr(%cropped, %pos1, %pos2-%pos1);
				%final = getsubstr(%cropped, %pos2 + 2, strlen(%cropped)-%pos2-2);
				%cl = getClientByName(%name);
	
				if(%cl !$= -1)
				{
					%n = %cl.nameBase;	//capitalize the name properly
					if(!%cl.muted[%TrueClientId])
					{
						messageClient(%cl, 'RPGchatCallback', %TCsenderName @ " tells you, \"" @ %final @ "\"");
						if(%cl !$= %TrueClientId)
							messageClient(%TrueClientId, 'RPGchatCallback', "You tell " @ %n @ ", \"" @ %final @ "\"");
						%cl.replyTo = %TCsenderName;

						UseSkill(%TrueClientId, $SkillSpeech, true, true);
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', %n @ " has muted you.");
				}
				else
					messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
			}
	
			//%botTalk = true;
		case "#r":
			if(%cropped $= "")
				messageClient(%TrueClientId, 'RPGchatCallback', "syntax: #r message");
			else
			{
				%name = %TrueClientId.replyTo;
				if(%name !$= "")
				{
					%cl = getClientByName(%name);
		
					if(%cl !$= -1)
					{
						if(!%cl.muted[%TrueClientId])
						{
							messageClient(%cl, 'RPGchatCallback', %TCsenderName @ " replies, \"" @ %cropped @ "\"");
							if(%cl !$= %TrueClientId)
								messageClient(%TrueClientId, 'RPGchatCallback', "You reply to " @ %name @ ", \"" @ %cropped @ "\"");
							%cl.replyTo = %TCsenderName;
	
							//UseSkill(%TrueClientId, $SkillSpeech, true, true);
						}
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
		
					%botTalk = true;
				}
				else
					messageClient(%TrueClientId, 'RPGchatCallback', "You haven't received a #tell to reply to yet.");
			}

		case "#global" or "#g":
			if(!fetchData(%TrueClientId, "ignoreGlobal"))
			{
				%count = ClientGroup.getCount();
				for(%icl = 0; %icl < %count; %icl++)
				{
					%cl = ClientGroup.getObject(%icl);
						  if(!%cl.muted[%TrueClientId] && %cl !$= %TrueClientId && !fetchData(%cl, "ignoreGlobal"))
								messageClient(%cl, 'RPGchatCallback', "[GLBL] " @ %TCsenderName @ " \"" @ %cropped @ "\"");
				}
				messageClient(%TrueClientId, 'RPGchatCallback', "[GLBL] \"" @ %cropped @ "\"");

				//UseSkill(%TrueClientId, $SkillSpeech, true, true);
			}
			else
				messageClient(%TrueClientId, 'RPGchatCallback', "You can't send a Global message when ignoring other Global messages.");
		case "#guild" or "#gu":
			%guildid = IsInWhatGuild(%client);
			%guild = GuildGroup.GetObject(%guildid);
			if(%guildid != -1 || %guild.GetGUIDRank(%TrueClientId.guid) == 0) //*************************GUID
			{
				//ok user is in a guild so we get the guild and obtain the list.
				
				
				
				%count = ClientGroup.getCount();
				for(%icl = 0; %icl < %count; %icl++)
				{
					%cl = ClientGroup.getObject(%icl);
					if(%cl.isAiControlled()) continue; //skip bots
					
					if(%guild.GUIDinGuild(%cl.guid) && %guild.GetGUIDRank(%cl.guid) > 0 && (!%cl.muted[%TrueClientId] && %cl !$= %TrueClientId) )
						messageClient(%cl, 'RPGchatCallback'  ,"[GUILD] " @ %TCsenderName @ " \"" @ %cropped @ "\"");
					
				}
				messageClient(%TrueClientID, 'RPGchatCallback', "[GUILD] \"" @ %cropped @ "\"");
				UseSkill(%TrueClientId, $SkillSpeech, true, true);
			}
			else
				messageClient(%TrueClientID, 'RPGchatcallback', "You are not in a guild.");
			//return;
		case "#zone" or "#z":
			%count = ClientGroup.getCount();
			for(%icl = 0; %icl < %count; %icl++)
			{
				%cl = ClientGroup.getObject(%icl);
				if(!%cl.muted[%TrueClientId] && %cl !$= %TrueClientId && fetchData(%cl, "zone") $= fetchData(%TrueClientId, "zone"))
					messageClient(%cl, 'RPGchatCallback', "[ZONE] " @ %TCsenderName @ " \"" @ %cropped @ "\"");
			}
			messageClient(%TrueClientId, 'RPGchatCallback', "[ZONE] \"" @ %cropped @ "\"");

			UseSkill(%TrueClientId, $SkillSpeech, true, true);
			
			//return;

		case "#group":
			%count = ClientGroup.getCount();
			for(%icl = 0; %icl < %count; %icl++)
			{
				%cl = ClientGroup.getObject(%icl);

				if(!%cl.muted[%TrueClientId] && %cl !$= %TrueClientId && IsInGroupList(%TrueClientId, %cl))
				{
					if(IsInGroupList(%cl, %TrueClientId))
						messageClient(%cl, 'RPGchatCallback', "[GRP] " @ %TCsenderName @ " \"" @ %cropped @ "\"");
					else
						messageClient(%TrueClientId, 'RPGchatCallback', %cl.nameBase @ " does not have you on his/her group-list.");
				}
			}

			messageClient(%TrueClientId, 'RPGchatCallback', "[GRP] \"" @ %cropped @ "\"");
			UseSkill(%TrueClientId, $SkillSpeech, true, true);

			//return;

		case "#party" or "#p":
			%list = GetPartyListIAmIn(%TrueClientId);
			
			for(%p = strstr(%list, ","); (%p = strstr(%list, ",")) !$= -1; %list = getsubstr(%list, %p+1, 99999))
			{
				%cl = getsubstr(%list, 0, %p);
				
				if(!%cl.muted[%TrueClientId] && %cl !$= %TrueClientId)
					messageClient(%cl, 'RPGchatCallback', "[PRTY] " @ %TCsenderName @ " \"" @ %cropped @ "\"");
			}

			messageClient(%TrueClientId, 'RPGchatCallback', "[PRTY] \"" @ %cropped @ "\"");
			UseSkill(%TrueClientId, $SkillSpeech, true, true);

			//return;
	}
	
	if(%botTalk)
	{
		//Most of this is for the admin quest commands.  The call to the BotTalk function at the bottom leads to the actual BotTalk processing. -Kyred
		//check for onHear events
		%count = ClientGroup.getCount();
		for(%icl = 0; %icl < %count; %icl++)
		{
			%oid = ClientGroup.getObject(%icl);

			%time = getTime();
			if(%time - fetchData(%oid, "nextOnHear") > 0.05)
			{
				storeData(%oid, "nextOnHear", %time);

				%oname = %oid.nameBase;

				%index = GetEventCommandIndex(%oid, "onHear");
				if(%index !$= -1)
				{
					for(%i2 = 0; (%index2 = GetWord(%index, %i2)) !$= ""; %i2++)
					{
						%ec = $EventCommand[%oid, %index2];
	
						%hearName = GetWord(%ec, 2);
						%radius = GetWord(%ec, 3);
						if(VectorDist(%oid.player.getPosition(), %TrueClientId.player.getPosition()) <= %radius)
						{
							%targetname = GetWord(%ec, 5);
							if(stricmp(%targetname, "all") !$= 0)
								%targetId = getClientByName(%targetname);
	
							if(stricmp(%targetname, "all") $= 0 || %targetId $= %TrueClientId)
							{
								%sname = GetWord(%ec, 0);
								%type = GetWord(%ec, 1);
								%keep = GetWord(%ec, 4);
								%var = GetWord(%ec, 6);
								if(stricmp(%var, "var") $= 0)
									%var = true;
								else
								{
									%div1 = strstr(%ec, "|");
									%div2 = String::ofindSubStr(%ec, "|", %div1+1);
									%text = getsubstr(%ec, %div1+1, %div2);
									%oec = getsubstr(%ec, %div1+%div2+2, 99999);
								}
	
								if(stricmp(%cropped, %text) $= 0 || %var)
								{
									if((%cl = getClientByName(%sname)) $= -1)
										%cl = 2048;

									%cmd = getsubstr($EventCommand[%oid, %index2], strstr($EventCommand[%oid, %index2], ">")+1, 99999);
									if(%var)
										%cmd = strreplace(%cmd, "^var", %cropped);
	
									%pcmd = ParseBlockData(%cmd, %TrueClientId, "");
									if(!%keep)
										$EventCommand[%oid, %index2] = "";
									RPGchat(%cl, 0, %pcmd); //, %sname);
								}
							}
						}
					}
				}
			}
		}
		BotTalk(%TrueClientId,%cropped);
	}
}
//Sense Heading skills
RPGSkill::AddSkill("#compass","Compass","Basic",$Skill::SenseHeading,"BasicSkillcompass");
RPGSkill::AddSkill("#track","Track","Basic",$Skill::SenseHeading,"BasicSkilltrack");
RPGSkill::AddSkill("#advcompass","Advanced Compass","Basic",$Skill::SenseHeading,"BasicSkilladvcompass");
RPGSkill::AddSkill("#zonelist","Zone List","Basic",$Skill::SenseHeading,"BasicSkillzonelist");
RPGSkill::AddSkill("#trackpack","Track Pack","Basic",$Skill::SenseHeading,"BasicSkilltrackpack");

RPGSkill::SetDesc("#compass","Determines the distance and direction of either town or dungeon.");
RPGSkill::SetDesc("#track","Determines the distance and direction of a player or bot in range.");
RPGSkill::SetDesc("#advcompass","Determines the distance and direction of a specific town or dungeon.");
RPGSkill::SetDesc("#zonelist","List all the players and bots in the same zone as you.");
RPGSkill::SetDesc("#trackpack","Talk the location of a player's last dropped pack.");

RPGSkill::SetExample("#compass","#compass town");
RPGSkill::SetExample("#track","#track <PlayerName/BotName>");
RPGSkill::SetExample("#advcompass","#advcompass Keldrin");
RPGSkill::SetExample("#zonelist","#zonelist <players/enemies/all>");
RPGSkill::SetExample("#trackpack","#tackpack <PlayerName>");

$SkillRestriction["#compass"] = $Skill::SenseHeading @ " 3";
$SkillRestriction["#track"] = $Skill::SenseHeading @ " 15";
$SkillRestriction["#advcompass"] = $Skill::SenseHeading @ " 20";
$SkillRestriction["#zonelist"] = $Skill::SenseHeading @ " 45";
$SkillRestriction["#trackpack"] = $Skill::SenseHeading @ " 85";

function BasicSkillCompass(%TrueClientId,%cmd,%cropped,%adminLvl)
{			
	if(%cropped $= "")
		messageClient(%TrueClientId, 'RPGchatCallback', "Use #compass town or #compass dungeon. (Do not specify which, simply write town or dungeon)");
	else
	{
		%mpos = GetNearestZone(%TrueClientId, %cropped, 4);

		if(%mpos !$= false && ( %cropped $= "town" || %cropped $= "dungeon" ) ) // only allow town and dungeon queries.
		{
			%d = GetNESW(%TrueClientId.player.getPosition(), %mpos);
			UseSkill(%TrueClientId, $SkillSenseHeading, true, true);

			messageClient(%TrueClientId, 'RPGchatCallback', "The nearest " @ %cropped @ " is " @ %d @ " of here.");
		}
		else
			messageClient(%TrueClientId, 1, "Error finding a zone!");
	}
}

function BasicSkillTrack(%TrueClientId,%cmd,%cropped,%adminLvl)
{
	//%cropped = GetWord(%cropped, 0);
	if(%cropped $= "")
		messageClient(%TrueClientId, 'RPGchatCallback', "Please specify a name.");
	else
	{
		%id = getClientByName(%cropped);
		if(%id !$= -1)
		{
			%clientpos = %TrueClientId.player.getPosition();
			%idpos = fetchData(%id,"lastScent");
			//%idpos = %id.player.getPosition();

			%dist = round(VectorDist(%clientpos, %idpos));
			
			if(Cap(%TrueClientId.PlayerSkill[$Skill::SenseHeading] * 7.5, 100, "inf") >= %dist)
			{
				%d = GetNESW(%clientpos, %idpos);
				messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " is " @ %d @ " of here, " @ %dist @ " meters away.");
				UseSkill(%TrueClientId, $Skill::SenseHeading, true, true);
				//if(%dist <= 10)
				//	Client::unhide(%id);//counter counter!
			}
			else
			{
				messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " is too far from you to track with your current sense heading skills.");
				UseSkill(%TrueClientId, $Skill::SenseHeading, false, true);
			}
		}
		else
			messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");

	}
	//return;
}

function BasicSkillAdvcompass(%TrueClientId,%cmd,%cropped,%adminLvl)
{
	if(%cropped $= "")
		messageClient(%TrueClientId, 'RPGchatCallback', "Use #advcompass zone keyword");
	else
	{
		%obj = GetZoneByKeywords(%TrueClientId, %cropped, 3);

		if(%obj !$= false)
		{
			%mpos = Zone::getMarker(%obj);

			%d = GetNESW(%TrueClientId.player.getPosition(), %mpos);
			UseSkill(%TrueClientId, $SkillSenseHeading, true, true);

			messageClient(%TrueClientId, 'RPGchatCallback', Zone::getDesc(%obj) @ " is " @ %d @ " of here.");
		}
		else
			messageClient(%TrueClientId, 1, "Couldn't fine a zone to match those keywords.");
	}
	//return;
}

function BasicSkillZonelist(%TrueClientId,%cmd,%cropped,%adminLvl)
{
	%c1 = GetWord(%cropped, 0);

	if(%c1 !$= -1)
	{
		if(stricmp(%c1, "all") $= 0)
			%t = 1;
		else if(stricmp(%c1, "players") $= 0)
			%t = 2;
		else if(stricmp(%c1, "enemies") $= 0)
			%t = 3;

		%list = Zone::getPlayerList(fetchData(%TrueClientId, "zone"), %t);

		if(%list !$= "")
		{
			for(%i = 0; (%id = GetWord(%list, %i)) !$= ""; %i++)
				messageClient(%TrueClientId, 'RPGchatCallback', %id.nameBase);
		}
		else
			messageClient(%TrueClientId, 'RPGchatCallback', "[none]");
	}
	else
		messageClient(%TrueClientId, 'RPGchatCallback', "Please specify 'players', 'enemies', or 'all'");
}

function BasicSkillTrackPack(%TrueClientId,%cmd,%cropped,%adminLvl)
{
	//Unimplemented for some reason? -Kyred
	return;
	%cropped = GetWord(%cropped, 0);

	if(%cropped $= "")
		messageClient(%TrueClientId, 'RPGchatCallback', "Please specify a name.");
	else
	{
		%id = getClientByName(%cropped);
		if(%id !$= -1)
		{
			%cropped = %id.nameBase;	//properly capitalize name

			%closest = 5000000;
			%closestId = -1;
			%clientpos = %TrueClientId.player.getPosition();
			%list = fetchData(%id, "lootbaglist");
			for(%i = strstr(%list, ","); strstr(%list, ",") !$= -1; %list = getsubstr(%list, %i+1, 99999))
			{
				%id = getsubstr(%list, 0, %i);
				%idpos = %id.player.getPosition();
				%dist = round(VectorDist(%clientpos, %idpos));
				if(%dist < %closest)
				{
					%closest = %dist;
					%closestId = %id;
				}
			}
			if(%closestId !$= -1)
			{
				%idpos = %closestId.player.getPosition();

				if(Cap(%TrueClientId.PlayerSkill[$SkillSenseHeading] * 15, 100, "inf") >= %closest)
				{
					%d = GetNESW(%clientpos, %idpos);
					messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ "'s nearest backpack is " @ %d @ " of here, " @ %closest @ " meters away.");
					UseSkill(%TrueClientId, $SkillSenseHeading, true, true);
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ "'s nearest backpack is too far from you to track with your current sense heading skills.");
					UseSkill(%TrueClientId, $SkillSenseHeading, false, true);
				}
			}
			else
			{
				messageClient(%TrueClientId, 'RPGchatCallback', %cropped @ " doesn't have any dropped backpacks.");
				UseSkill(%TrueClientId, $SkillSenseHeading, false, true);
			}
		}
		else
			messageClient(%TrueClientId, 'RPGchatCallback', "Invalid player name.");
	}
	//return;
}

//Backstab Skills
RPGSkill::AddSkill("#backstab","Backstab","Basic",$Skill::Backstabbing,"BasicSkillbackstab");
RPGSkill::AddSkill("#surge","Surge","Basic",$Skill::Backstabbing,"BasicSkillsurge");
RPGSkill::AddSkill("#encumber","Encumbering Strike","Basic",$Skill::Backstabbing,"BasicSkillencumber");

RPGSkill::SetDesc("#backstab","Next attack with a piercing weapon will attempt to backstab.  Only an attack from behind will be successful.");
RPGSkill::SetDesc("#surge","???.");
RPGSkill::SetDesc("#encumber","Next attack with a piercing weapon will temporarily increase the weight the target is carrying.  Can induce encumbered status.");

RPGSkill::SetExample("#backstab","#backstab");
RPGSkill::SetExample("#surge","#surge");
RPGSkill::SetExample("#encumber","#encumber");

$SkillRestriction["#backstab"] = $Skill::Backstabbing @ " 15";
$SkillRestriction["#surge"] = $Skill::Backstabbing @ " 50";
$SkillRestriction["#encumber"] = $Skill::Backstabbing @ " 150";

function BasicSkillBackstab(%TrueClientId,%cmd,%cropped,%adminLvl)
{
	if(SkillCanUse(%TrueClientId, "#backstab"))
	{
		if(!(fetchdata(%trueClientId, "NextHitBackStab") || fetchdata(%trueClientId, "BlockBackStab")))
		{
			messageClient(%TrueClientId, 'RPGchatCallback', "You are ready to backstab!");
			storeData(%TrueClientId, "NextHitBackStab", true);
			storeData(%TrueClientId, "blockBackStab", true);
		}
		else
			if(fetchdata(%TrueClientId, "NextHitBackStab"))
				messageClient(%TrueClientId, 'RPGchatCallback', "You are already ready to backstab.");
			else
				messageClient(%TrueClientId, 'RPGchatCallback', "You are too tired to backstab again, wait a bit");
	}
}

function BasicSkillSurge(%TrueClientId,%cmd,%cropped,%adminLvl)
{
	if( !(fetchdata(%TrueClientId, "blockSurge")  ) )
	{
		//storedata(%TrueClientId, "Surge", true);
		AddBonusState(%TrueClientId, "12 10", 5, "Surge");
		storedata(%TrueClientId, "BlockSurge", true);
		weightcall(%TrueClientId, false);
		debugBonusState(%TrueClientId);
		schedule(5000, %TrueClientId, "endsurge", %TrueClientId);
		messageClient(%TrueClientid, 'RPGchatCallback', "You begin to surge!");	
	}
	else
		messageClient(%TrueClientId, 'RPGchatCallback', "You are too tired to boost your speed.");
}

function BasicSkillEncumber(%TrueClientId,%cmd,%cropped,%adminLvl)
{
	if(!(fetchdata(%trueClientId, "NextHitencumber") || fetchdata(%trueClientId, "blockEncumber")))
	{
		messageClient(%TrueClientId, 'RPGchatCallback', "You are ready to Encumber!");
		storeData(%TrueClientId, "NextHitencumber", true);
		storeData(%TrueClientId, "blockencumber", true);
	}
	else
		if(fetchdata(%TrueClientId, "NextHitEncumber"))
			messageClient(%TrueClientId, 'RPGchatCallback', "You are already ready to Encumber.");
		else
			messageClient(%TrueClientId, 'RPGchatCallback', "You are too tired to Encumber again, wait a bit");					
}

//Ignite Skills
RPGSkill::AddSkill("#ignite","Ignite Arrow","Basic",$Skill::IgniteArrow,"BasicSkillignite");
RPGSkill::SetDesc("#ignite","Ignites the next arrow you shoot.");
RPGSkill::SetExample("#ignite","#ignite");
$SkillRestriction["#ignite"] = $Skill::IgniteArrow @ " 15";

function BasicSkillIgnite(%TrueClientId,%cmd,%cropped,%adminLvl)
{
	if(!(fetchdata(%trueClientId, "NextHitignite") || fetchdata(%trueClientId, "Blockignite")))
	{
		messageClient(%TrueClientId, 'RPGchatCallback', "You light your projectile!");
		storeData(%TrueClientId, "NextHitignite", true);
		storeData(%TrueClientId, "blockignite", true);
	}
	else
		if(fetchdata(%TrueClientId, "NextHitignite"))
			messageClient(%TrueClientId, 'RPGchatCallback', "You are already ready to ignite.");
		else
			messageClient(%TrueClientId, 'RPGchatCallback', "You are too tired to ignite again, wait a bit");
}

//Cleave Skills
RPGSkill::AddSkill("#cleave","Cleave","Basic",$Skill::Cleaving,"BasicSkillcleave");
RPGSkill::AddSkill("#berserk","Berserk","Basic",$Skill::Cleaving,"BasicSkillberserk");
RPGSkill::AddSkill("#targetleg","Target Leg","Basic",$Skill::Cleaving,"BasicSkilltargetleg");

RPGSkill::SetDesc("#cleave","Increasing damage of next slashing attack.");
RPGSkill::SetDesc("#berserk","???");
RPGSkill::SetDesc("#targetleg","???");

RPGSkill::SetExample("#cleave","#cleave");
RPGSkill::SetExample("#berserk","#berserk");
RPGSkill::SetExample("#targetleg","#targetleg");

$SkillRestriction["#cleave"] = $Skill::Cleaving @ " 15";
$SkillRestriction["#berserk"] = $Skill::Cleaving @ " 50";
$SkillRestriction["#targetleg"] = $Skill::Cleaving @ " 150";

function BasicSkillCleave(%TrueClientId,%cmd,%cropped,%adminLvl)
{
	if(!(fetchdata(%trueClientId, "NextHitCleave") || fetchdata(%trueClientId, "blockCleave")))
	{
		messageClient(%TrueClientId, 'RPGchatCallback', "You are ready to Cleave!");
		storeData(%TrueClientId, "NextHitCleave", true);
		storeData(%TrueClientId, "blockCleave", true);
	}
	else
		if(fetchdata(%TrueClientId, "NextHitCleave"))
			messageClient(%TrueClientId, 'RPGchatCallback', "You are already ready to cleave.");
		else
			messageClient(%TrueClientId, 'RPGchatCallback', "You are too tired to cleave again, wait a bit");
}

function BasicSkillBerserk(%TrueClientId,%cmd,%cropped,%adminLvl)
{
	if( !(fetchdata(%TrueClientId, "blockberserk") || fetchdata(%TrueClientId, "Surge") ) )
	{
		storedata(%TrueClientId, "Surge", true);
		weightcall(%TrueClientId, false);
		storedata(%TrueClientId, "BlockBerserk", true);
		schedule(5000, %TrueClientId, "endberserk", %TrueClientId);
		messageClient(%TrueClientId, 'RPGchatCallBack', "You go Berserk!");
	}
}

function BasicSkillTargetLeg(%TrueClientId,%cmd,%cropped,%adminLvl)
{
	if(!(fetchdata(%trueClientId, "NextHitLeg") || fetchdata(%trueClientId, "blockLeg")))
	{
		messageClient(%TrueClientId, 'RPGchatCallback', "You are ready to Target Leg!");
		storeData(%TrueClientId, "NextHitLeg", true);
		storeData(%TrueClientId, "blockLeg", true);
	}
	else
		if(fetchdata(%TrueClientId, "NextHitLeg"))
			messageClient(%TrueClientId, 'RPGchatCallback', "You are already ready to Target Leg.");
		else
			messageClient(%TrueClientId, 'RPGchatCallback', "You are too tired to Target Leg again, wait a bit");					
}

//Stealing Skills
RPGSkill::AddSkill("#steal","Steal","Basic",$Skill::Stealing,"BasicSkillsteal");
RPGSkill::AddSkill("#pickpocket","Pickpocket","Basic",$Skill::Stealing,"BasicSkillpickpocket");
RPGSkill::AddSkill("#mug","Mug","Basic",$Skill::Stealing,"BasicSkillmug");

RPGSkill::SetDesc("#steal","Steal coins from a player or bot. Must be very close and looking at them.");
RPGSkill::SetDesc("#pickpocket","Steal unequipped items from a player or bot. Must be very close and looking at them.");
RPGSkill::SetDesc("#mug","Steal any item from a player or bot. Must be very close and looking at them.");

RPGSkill::SetExample("#steal","#steal");
RPGSkill::SetExample("#pickpocket","#pickpocket");
RPGSkill::SetExample("#mug","#mug");

$SkillRestriction["#steal"] = $Skill::Stealing @ " 15";
$SkillRestriction["#pickpocket"] = $Skill::Stealing @ " 270";
$SkillRestriction["#mug"] = $Skill::Stealing @ " 620";

function BasicSkillSteal(%TrueClientId,%cmd,%cropped,%adminLvl)
{
	%time = getTime();
	if(%time - %TrueClientId.lastStealTime > $stealDelay)
	{
		%TrueClientId.lastStealTime = %time;

		if((%reason = AllowedToSteal(%TrueClientId)) $= "true")
		{
			if(getLOSinfo(%TrueClientId, 1))
			{
				%id = $los::object.client;
				if($los::object.getClassName() $= "Player")
				{
					%victimName = %id.nameBase;
					%stealerName = %TCsenderName;
					%victimCoins = fetchData(%id, "COINS");
					%fail = false;
					if(%victimCoins > 0)
					{
						%r1 = GetRpgRoll("1r" @ (%TrueClientId.PlayerSkill[$SkillStealing] * (4/5)));
						%r2 = GetRpgRoll("1r" @ %id.PlayerSkill[$SkillStealing]);
						%a = %r1 - %r2;
						if(%a > 0)
						{
							%amount = mfloor(%a * getRandom() * 1.2);
							if(%amount > %victimCoins)
								%amount = %victimCoins;

							if(%amount > 0)
							{
								storeData(%TrueClientId, "COINS", %amount, "inc");
								storeData(%id, "COINS", %amount, "dec");
								PerhapsPlayStealSound(%TrueClientId, 0);

								messageClient(%TrueClientId, 'RPGchatCallback', "You successfully stole " @ %amount @ " coins from " @ %victimName @ "!");

								RefreshAll(%TrueClientId);
								RefreshAll(%id);

								UseSkill(%TrueClientId, $SkillStealing, true, true);
								PostSteal(%TrueClientId, true, 0);
							}
							else
								%fail = true;
						}
						else
							%fail = true;

						if(%fail)
						{
							messageClient(%TrueClientId, 'RPGchatCallback', "You failed to steal from " @ %victimName @ "!");
							messageClient(%id, 'RPGchatCallback', %stealerName @ " just failed to steal from you!");

							UseSkill(%TrueClientId, $SkillStealing, false, true);
							PostSteal(%TrueClientId, false, 0);
						}
					}
					else
					{
						messageClient(%TrueClientId, 'RPGchatCallback', %victimName @ " doesn't appear to be carrying any coins...");
					}
				}
			}
		}
		else
			messageClient(%TrueClientId, 'RPGchatCallback', %reason);
	}
	//return;
}

function BasicSkillPickpocket(%TrueClientId,%cmd,%cropped,%adminLvl)
{
	%time = getTime();//ok pickpocket will only steal small items, user really wont have a choice to what they steal either unlike tribes rpg.
	//I kind of like this idea.  -Kyred
	if(%time - %TrueClientId.lastStealTime > $stealDelay)
	{
		%TrueClientId.lastStealTime = %time;

		if((%reason = AllowedToSteal(%TrueClientId)) $= "true")
		{
			if(getLOSinfo(%TrueClientId, 1))
			{
				%id = $los::object.client;
				%pos = $los::position;
				
				if($los::object.getClassName() $= "Player")
				{
					%damloc = %id.player.getDamageLocation(%pos);
					%tciskill = %TrueClientID.PlayerSkill[$SkillStealing] * 4/5;
					%idskill = %id.playerSkill[$skillStealing];
					
					//echo(%damloc);
					//%TrueClientId.stealType = 1;
					//SetupInvSteal(%TrueClientId, %id);
					//attempt to steal
					%inv = fetchdata(%id, "inventory");
					%num = getwordcount(%inv);
					//randomly select an item
					%itemid = GetWord(%inv, getrandom(0,%num-1));
					
					if(%itemid)
					{
						%item = getItem(%itemid);
						if($itemtype[%item] $= "armor" || $itemType[%item] $= "weapon")
						{
							//error cannot steal
							%fail = true;
						}
						else
						{
							%backfront = getword(%damloc, 1);
							if(%backfront $= "back_right" || %backfront $= "back" || %backfront $= "back_left" || %backfront $= "middle_back")
								%multi = 1;
							else
								%multi = 0.5;//odds of stealing 1/2
							if(!id.isAIControlled())
							%oddcalc = %num - 10;
							else
							%oddcalc = %num;
							if(%oddcalc < 0)
								%oddcalc = 0;
							%ooddcalc = %oddcalc * %tciskill * %multi;
							%doddcalc = 35 * %idskill + 10;
							%success = getRandom(-%doddcalc, %ooddcalc);
							
							if(%success > 0)
							%fail = false;
							else
							%fail = true;
						}
						echo("PICKPOCKET:" SPC -%doddcalc SPC %ooddcalc SPC %success SPC %tciskill SPC %idskill);
						if(!%fail)
						{
							RemoveFromInventory(%id, %itemid);
							AddToInventory(%TrueClientId, %itemid);
							messageClient(%TrueClientId, 'RPGchatCallback', "You sucessfully stole a" SPC GetFullItemName(%itemid));
							MessageClient(%TrueClientID, 'RPGchatcallback', "DEBUG, ODDS TO STEAL:" SPC -%doddcalc SPC " to " SPC %ooddcalc SPC "NUMBER:" SPC %success);
						}
						else
						{
							messageClient(%TrueClientId, 'RPGchatCallback', "You failed to steal from" SPC %id.rpgname @ "!");
							MessageClient(%TrueClientID, 'RPGchatcallback', "DEBUG, ODDS TO STEAL:" SPC -%doddcalc SPC " to " SPC %ooddcalc SPC "NUMBER:" SPC %success);
							messageClient(%id, 'RPGchatCallback', %TrueClientid.rpgname SPC "failed to steal from you!");
							//failed to steal, notify victim? lashback?
						}
						
					}
					else
						messageClient(%TrueClientId, 'RPGchatCallback', "Your target has no items on him");
				}
			}
		}
		else
			messageClient(%TrueClientId, 'RPGchatCallback', %reason);
	}
	//return;
}

//I don't really like how Mug is setup.  I might revise this later.
function BasicSkillMug(%TrueClientId,%cmd,%cropped,%adminLvl)
{
	//can steal ANYTHING unequiped at a higher success rate, however player must be in your target list to attempt!
	%time = getTime();
	if(%time - %TrueClientId.lastStealTime > $stealDelay)
	{
		%TrueClientId.lastStealTime = %time;
		if(fetchdata(%TrueClientId, "NextHitMug") )
		{
			messageClient(%TrueClientId, 'RPGchatCallback', "You are already ready to mug");
		}
		else if( fetchdata(%TrueClientId, "blockHitMug") )
		{
			messageClient(%TrueClientId, 'RPGchatCallback', "You are not ready to mug");
		}
		else
		{
			storedata(%TrueClientId, "blockhitmug", true);
			storedata(%TrueClientId, "NextHitMug", true);
			messageClient(%TrueClientId, 'RPGchatCallback', "You are ready to mug.");
		}		
	}
	//return;
}

function domug(%client, %id,  %damloc)
{
	%fail = true;
	if((%reason = AllowedToSteal(%client)) $= "true")
	{
		if(SkillCanUse(%client, "#mug"))
		{
			
				if(%id.player.getClassName() $= "Player" )
				{
					//%damloc = %id.player.getDamageLocation(%pos);
					%tciskill = %client.PlayerSkill[$SkillStealing];
					%idskill = %id.playerSkill[$skillStealing];

					
					//%TrueClientId.stealType = 1;
					//SetupInvSteal(%TrueClientId, %id);
					//attempt to steal
					%inv = fetchdata(%id, "inventory");
					%num = getwordcount(%inv);
					//randomly select an item
					%itemid = GetWord(%inv, getrandom(0,%num-1));

					if(%itemid)
					{
						if($InvInfo[%itemid, equipped])
							%fail = true;
						else
						{
							%item = getItem(%itemid);

							%backfront = getword(%damloc, 1);
							if(%backfront $= "back_right" || %backfront $= "back" || %backfront $= "back_left" || %backfront $= "middle_back")
								%multi = 1;
							else
								%multi = 0.5;//odds of stealing 1/2

							if(!id.isAIControlled())
							%oddcalc = %num - 10;
							else
							%oddcalc = %num;
							if(%oddcalc < 0)
								%oddcalc = 0;
							%ooddcalc = %oddcalc * %tciskill * %multi;
							%doddcalc = 35 * %idskill + 10;
							%success = getRandom(-%doddcalc, %ooddcalc);

							if(%success > 0)
							%fail = false;
							else
							%fail = true;
						}

						//echo("MUG:" SPC -%doddcalc SPC %ooddcalc SPC %success SPC %tciskill SPC %idskill);
						if(!%fail)
						{
							RemoveFromInventory(%id, %itemid);
							AddToInventory(%client, %itemid);
							messageClient(%client, 'RPGchatCallback', "You sucessfully stole a" SPC GetFullItemName(%itemid));
							//MessageClient(%client, 'RPGchatcallback', "DEBUG, ODDS TO STEAL:" SPC -%doddcalc SPC " to " SPC %ooddcalc SPC "NUMBER:" SPC %success);
						}
						else
						{
							messageClient(%client, 'RPGchatCallback', "You failed to steal from" SPC %id.rpgname @ "!");
							//MessageClient(%client, 'RPGchatcallback', "DEBUG, ODDS TO STEAL:" SPC -%doddcalc SPC " to " SPC %ooddcalc SPC "NUMBER:" SPC %success);
							messageClient(%id, 'RPGchatCallback', %TrueClientid.rpgname SPC "failed to steal from you!");
							//failed to steal, notify victim? lashback?
						}

					}
					else
						messageClient(%client, 'RPGchatCallback', "Your target has no items on him");
				}
		}
		else
		{
			messageClient(%client, 'RPGchatCallback', "You can't pickpocket because you lack the necessary skills.");
			UseSkill(%TrueClientId, $SkillStealing, false, true);
		}
	}
	else
		messageClient(%client, 'RPGchatCallback', %reason);
	return !%fail;
}

//Hide Skills
RPGSkill::AddSkill("#hide","Hide in Shadows","Basic",$Skill::Hiding,"BasicSkillhide");
RPGSkill::SetDesc("#hide","Become invisible.  Must be close to a wall or tall object to work.  Higher levels in hiding allow for faster movement without unhiding.");
RPGSkill::SetExample("#hide","#hide");
$SkillRestriction["#hide"] = $Skill::Hiding @ " 15";

function BasicSkillHide(%TrueClientId,%cmd,%cropped,%adminLvl)
{
	if(!fetchData(%TrueClientId, "invisible") && !fetchData(%TrueClientId, "blockHide") && %Trueclientid.lasthide + 5000 < getSimTime())
	{
		%closeEnoughToWall = Cap(%TrueClientId.PlayerSkill[$SkillHiding] / 125, 3.5, 8);

		%pos = %TrueClientId.player.getPosition();

		%closest = 10000;
		for(%i = 0; %i <= 6.283; %i+= 0.52)
		{
			%dir = mCos(%i) SPC mSin(%i) SPC "0";
			getLOSinfo(%TrueClientId, 25, "", %dir);
			%dist = VectorDist(%pos, $los::position);
			//Debug flares
			//%p = new FlareProjectile() {
			//  dataBlock        	= Flare;
			//  initialDirection 	= %dir;
			//  initialPosition  	= %pos;
			//  sourceObject     	= %TrueClientId.player;
			//  sourceSlot       	= 1;
			//  vehicleObject    	= 0;
			//  spell			= $Skill::NeutralCasting;
			//};
			
			if(%dist < %closest && $los::position !$= "0 0 0" && $los::position !$= "")
				%closest = %dist;
		}

		if(%closest <= %closeEnoughToWall)
		{
			messageClient(%TrueClientId, 'RPGchatCallback', "You are successful at Hide In Shadows.");

			Client::hide(%TrueClientID);
			//GameBase::startFadeOut(%TrueClientId);
			//storeData(%TrueClientId, "invisible", true);

			%grace = Cap(%TrueClientId.PlayerSkill[$SkillHiding] / 20, 5, 100);
			WalkSlowInvisLoop(%TrueClientId, 5, %grace);
			%Trueclientid.lasthide = getSimTime();
			UseSkill(%TrueClientId, $Skill::Hiding, true, true);
		}
		else
		{
			messageClient(%TrueClientId, 'RPGchatCallback', "You were unsuccessful at Hide In Shadows.");
			UseSkill(%TrueClientId, $Skill::Hiding, false, true);
		}
	}
}

//Focus Skills
RPGSkill::AddSkill("#focus","Focus","Basic",$Skill::Focus,"BasicSkillfocus");
RPGSkill::SetDesc("#focus","Increase the potentency of your next spell.  Increases the cooldown for the spell.");
RPGSkill::SetExample("#focus","#focus");
$SkillRestriction["#focus"] = $Skill::Focus @ " 15";

function BasicSkillFocus(%TrueClientId,%cmd,%cropped,%adminLvl)
{
	if(!(fetchdata(%trueClientId, "NextHitFocus") || fetchdata(%trueClientId, "blockFocus")))
	{
		messageClient(%TrueClientId, 'RPGchatCallback', "You focus your energy!");
		storeData(%TrueClientId, "NextHitFocus", true);
		storeData(%TrueClientId, "blockFocus", true);
	}
	else
		if(fetchdata(%TrueClientId, "NextHitFocus"))
		messageClient(%TrueClientId, 'RPGchatCallback', "You are already focused.");
		else
		messageClient(%TrueClientId, 'RPGchatCallback', "You are too strained to focus, wait a bit.");
}

//Bashing Skills
RPGSkill::AddSkill("#shove","Shove","Basic",$Skill::Bashing,"BasicSkillshove");
RPGSkill::AddSkill("#bash","Bash","Basic",$Skill::Bashing,"BasicSkillbash");
RPGSkill::AddSkill("#disrupt","Disrupt","Basic",$Skill::Bashing,"BasicSkilldisrupt");
RPGSkill::AddSkill("#stun","Stun","Basic",$Skill::Bashing,"BasicSkillstun");

RPGSkill::SetDesc("#shove","Push the target back. Force scales with Bashing level.  Useful for moving a player out of the way.");
RPGSkill::SetDesc("#bash","Next attack with a bludgeoning weapon will bash the target, increasing damage and pushing the target back.  Higher damage and Bashing level increases the force of the bash.");
RPGSkill::SetDesc("#disrupt","???");
RPGSkill::SetDesc("#stun","???");

RPGSkill::SetExample("#shove","#shove");
RPGSkill::SetExample("#bash","#bash");
RPGSkill::SetExample("#disrupt","#disrupt");
RPGSkill::SetExample("#stun","#stun");

$SkillRestriction["#shove"] = $Skill::Bashing @ " 5";
$SkillRestriction["#bash"] = $Skill::Bashing @ " 15";
$SkillRestriction["#disrupt"] = $Skill::Bashing @ " 50";
$SkillRestriction["#stun"] = $Skill::Bashing @ " 170";

function BasicSkillShove(%TrueClientId,%cmd,%cropped,%adminLvl)
{
	%time = getTime();
	if(%time - %TrueClientId.lastShoveTime > 1.5)
	{
		%TrueClientId.lastShoveTime = %time;
		%player = %trueClientId.player;
		if(getLOSinfo(%TrueClientId, 5))
		{
			%pl = $los::object;
			%id = %pl.client;
			if(%TrueClientId.adminLevel > %id.adminLevel || %id.adminLevel < 1 && %id != 0)
			{
				%b = %TrueClientId.player.rotation;
				%c1 = Cap(20 + fetchData(%TrueClientId, "LVL"), 0, 250);
				%c2 = %c1 / 4;
				%muzzlevec = %TrueClientId.player.getMuzzleVector(1);
				%vel = %id.player.getVelocity();
				%mom = GetWord(%muzzlevec, 0)*20+GetWord(%vel, 0) SPC GetWord(%muzzlevec, 1)*20+GetWord(%vel, 1) SPC GetWord(%muzzlevec, 2)*20+GetWord(%vel, 2);	
				%id.player.setVelocity(%mom);				
				
			}
		}
	}
}

function BasicSkillBash(%TrueClientId,%cmd,%cropped,%adminLvl)
{
	if(!(fetchdata(%trueClientId, "NextHitBash") || fetchdata(%trueClientId, "blockBash")))
	{
		messageClient(%TrueClientId, 'RPGchatCallback', "You are ready to bash!");
		storeData(%TrueClientId, "NextHitBash", true);
		storeData(%TrueClientId, "blockBash", true);
	}
	else
		if(fetchdata(%TrueClientId, "NextHitBash"))
			messageClient(%TrueClientId, 'RPGchatCallback', "You are already ready to bash");
		else
			messageClient(%TrueClientId, 'RPGchatCallback', "You are too tired to bash again, wait a bit");

	//return;
}

function BasicSkillDisrupt(%TrueClientId,%cmd,%cropped,%adminLvl)
{
	if(!(fetchdata(%TrueClientId, "NextHitDisrupt") || fetchdata(%TrueClientId, "BlockDisrupt")))
	{
		storedata(%TrueClientId, "NextHitDisrupt", true);
		storedata(%TrueClientId, "blockdisrupt", true);
		messageClient(%TrueClientID, 'RPGchatCallback', "You are ready to disrupt!");
	}
	else
		if(fetchdata(%TrueClientId, "NextHitDisrupt"))
			messageClient(%TrueClientId, 'RPGchatCallback', "You are already ready to disrupt.");
		else
			messageClient(%TrueClientId, 'RPGchatCallback', "You are too tired to Disrupt again, wait a bit");
		
}

function BasicSkillStun(%TrueClientId,%cmd,%cropped,%adminLvl)
{
	if(!(fetchdata(%trueClientId, "NextHitStun") || fetchdata(%trueClientId, "blockStun")))
	{
		messageClient(%TrueClientId, 'RPGchatCallback', "You are ready to Stun!");
		storeData(%TrueClientId, "NextHitStun", true);
		storeData(%TrueClientId, "blockStun", true);
	}
	else
	if(fetchdata(%TrueClientId, "NextHitStun"))
		messageClient(%TrueClientId, 'RPGchatCallback', "You are already ready to Stun.");
	else
		messageClient(%TrueClientId, 'RPGchatCallback', "You are too tired to Stun again, wait a bit");

}


//Helper functions copied over from old locations.
function endsurge(%client)
{
	//CalculateBonusState(%client);
	//debugBonusState(%client);//2
	//storedata(%client, "surge", false);
	schedule(15000, %client, "resetblocksurge", %client);
	weightcall(%client, false);
	MessageClient(%client, 'SurgeOff', "You are no longer surging.");
}
function resetblocksurge(%client)
{
	storedata(%client, "blocksurge", false);
}
function endberserk(%client)
{
	storedata(%client, "surge", false);
	schedule(15000, %client, "resetblockberserk", %client);
	weightcall(%client, false);
	MessageClient(%client, 'BerserkOff', "You are no longer berserk.");
}
function resetblockberserk(%client)
{
	storedata(%client, "blockberserk", false);
}

function BotTalk(%TrueClientId,%cropped)
{
	echo("Bottalk Go!");
	//========== BOT TALK ======================================================================================
	//process TownBot talk

	%initTalk = "";
	for(%i = 0; (%w = GetWord("hail hello hi greetings yo hey sup salutations g'day howdy", %i)) !$= ""; %i++)
		if(stricmp(%cropped, %w) $= 0)
			%initTalk = true;

	%clientPos = %TrueClientId.player.getTransform();
	%closest = 5000000;

	for(%i = 0; (%id = GetWord($TownBotList, %i)) !$= ""; %i++)
	{
		
		%botPos = %id.getTransform();
		%dist = VectorDist(%clientPos, %botPos);

		if(%dist < %closest)
		{
			%closest = %dist;
			%closestId = %id;
			%closestPos = %botPos;
		}
	}
	

	%aiName = %closestId.name;//hrm
	%aiType = %closestID.mtype;
	%displayName = %aiName;

	if(%closest <= ($maxAIdistVec + (%TrueClientId.PlayerSkill[$SkillSpeech] / 50)) && %TrueClientId.team $= %closestId.team)
	{
		
		if(%aitype $= "merchant")
		{
			echo("Merchant Talk:" SPC $state[%closestId, %TrueClientId]);
			//process merchant code
			%trigger[2] = "buy";
			%trigger[3] = "yes";
			if($state[%closestId, %TrueClientId] $= "")
			{
				if(%initTalk)
				{
					AI::sayLater(%TrueClientId, %closestId, "Did you come to see what items you can BUY?", true);
					$state[%closestId, %TrueClientId] = 1;
				}
			}
			else if($state[%closestId, %TrueClientId] $= 1)
			{
				//if(strstr(%message, %trigger[2]) !$= -1)  Why are we using %message here?  %cropped should work. -kyred
				if(strstr(%cropped, %trigger[2]) !$= -1)
				{
					$state[%closestId, %TrueClientId] = "";
					SetupShop(%TrueClientId, %closestId);

					AI::sayLater(%TrueClientId, %closestId, "Take a look at what I have.", true);
				}
				if(strstr(%cropped, %trigger[3]) !$= -1)
				{
					$state[%closestId, %TrueClientId] = "";
					SetupShop(%TrueClientId, %closestId);

					AI::sayLater(%TrueClientId, %closestId, "Take a look at what I have.", true);
				}
			}
			return;
		}

		else
		if(%aitype $= "bank")
		{
			//process banker code
			%trigger[2] = "yes";
			%trigger[3] = "no";
			if($state[%closestId, %TrueClientId] $= "")
			{
				if(%initTalk)
				{
					AI::sayLater(%TrueClientId, %closestId, "Would you like to access your bank account? (Yes or No)", true);
					$state[%closestId, %TrueClientId] = 1;
				}
			}
			if($state[%closestId, %TrueClientId] $= 1)
			{
				if(strstr(%cropped, %trigger[2]) !$= -1)
				{
					//deposit question
					AI::sayLater(%TrueClientId, %closestId, "Ok, here is what you have stored here.", true);
					setupbank(%TrueClientId, %closestId);
					$state[%closestId, %TrueClientId] = "";
				}
				if(strstr(%cropped, %trigger[3]) !$= -1)
				{
					//withdraw question
					AI::sayLater(%TrueClientId, %closestId, "Good day, sir.", true);
					$state[%closestId, %TrueClientId] = "";
				}
			}
		}
		
		if(%aitype $= "assassin")
		{
			//process assassin code
			%trigger[2] = "yes";
			%trigger[3] = "no";
			%trigger[4] = "buy";
			if($state[%closestId, %TrueClientId] $= "")
			{
				if(%initTalk)
				{
					%highest = -1;
					%list = GetPlayerIdList();
					for(%i = 0; (%id = GetWord(%list, %i)) !$= ""; %i++)
					{
						if(fetchData(%id, "bounty") $= "")
							storeData(%id, "bounty", 0);
						if(fetchData(%id, "bounty") > %highest)
						{
							%h = %id;
							%highest = fetchData(%id, "bounty");
						}
					}
					%n = %h.nameBase;
					%c = fetchData(%h, "bounty");

					AI::sayLater(%TrueClientId, %closestId, "The highest bounty is currently on " @ %n @ " for $" @ %c @ ". Give me someone's name and I'll tell you their bounty, unless you want to BUY something." , true);

					$state[%closestId, %TrueClientId] = 1;
				}
			}
			else if($state[%closestId, %TrueClientId] $= 1)
			{
				if(strstr(%cropped, %trigger[4]) !$= -1)
				{
					%cost = GetLCKcost(%TrueClientId);

					AI::sayLater(%TrueClientId, %closestId, "I will sell you one LCK point for $" @ %cost @ ". (YES/NO)", true);
					$state[%closestId, %TrueClientId] = 2;
				}
				else
				{
					%lowest = 99999;
					%h = "";
					%list = GetPlayerIdList();
					for(%i = 0; (%id = GetWord(%list, %i)) !$= ""; %i++)
					{
						%comp = stricmp(%cropped, %id.nameBase);
						if(%comp < 0) %comp = -%comp;

						if(%comp < %lowest)
						{
							%h = %id;
							%lowest = %comp;
						}
					}
					if(%h !$= "")
					{
						%l = fetchData(%h, "LVL");
						%c = getFinalCLASS(%h);
						AI::sayLater(%TrueClientId, %closestId, "Are you talking about " @ %h.nameBase @ " the Level " @ %l @ " " @ %c @ "?", true);
						storeData(%TrueClientId, "tmpdata", %h);
						$state[%closestId, %TrueClientId] = 3;
					}
					else
					{
						AI::sayLater(%TrueClientId, %closestId, "I have no idea who you are talking about. Goodbye.", true);
						$state[%closestId, %TrueClientId] = "";
					}
				}
			}
			else if($state[%closestId, %TrueClientId] $= 2)
			{
				if(strstr(%cropped, %trigger[2]) !$= -1)
				{
					%cost = GetLCKcost(%TrueClientId);

					if(fetchData(%TrueClientId, "COINS") >= %cost)
					{
						AI::sayLater(%TrueClientId, %closestId, "Here's your LCK point, thanks for your business.", true);
						GiveThisStuff(%TrueClientId, "LCK 1", true);
						storeData(%TrueClientId, "COINS", %cost, "dec");
						RefreshAll(%TrueClientId);
					}
					else
						AI::sayLater(%TrueClientId, %closestId, "You can't afford this.", true);

					$state[%closestId, %TrueClientId] = "";
				}
				else if(strstr(%cropped, %trigger[3]) !$= -1)
				{
					AI::sayLater(%TrueClientId, %closestId, "See ya.", true);
					$state[%closestId, %TrueClientId] = "";
				}
			}
			else if($state[%closestId, %TrueClientId] $= 3)
			{
				if(strstr(%cropped, %trigger[2]) !$= -1)
				{
					%id = fetchData(%TrueClientId, "tmpdata");
					if(%id !$= %TrueClientId)
					{
						%n = %id.nameBase;
						if(IsInCommaList(fetchData(%TrueClientId, "TempKillList"), %n))
						{
							storeData(%TrueClientId, "TempKillList", RemoveFromCommaList(fetchData(%TrueClientId, "TempKillList"), %n));
							AI::sayLater(%TrueClientId, %closestId, "I see you've killed " @ %n @ ". Here's your reward... " @ fetchData(%id, "bounty") @ " coins. Goodbye.", true);
							storeData(%TrueClientId, "COINS", fetchData(%id, "bounty"), "inc");
							storeData(%id, "bounty", 0);

							%TrueClientId.player.playAudio(0, SoundMoney1);
							RefreshAll(%TrueClientId);
						}
						else
							AI::sayLater(%TrueClientId, %closestId, %n @ "'s bounty is currently at " @ fetchData(%id, "bounty") @ " coins. Goodbye.", true);
					}
					else
						AI::sayLater(%TrueClientId, %closestId, "You can't get a reward for killing yourself... idiot.", true);

					$state[%closestId, %TrueClientId] = "";
				}
				else if(strstr(%cropped, %trigger[3]) !$= -1)
				{
					AI::sayLater(%TrueClientId, %closestId, "Well then, I have no idea who you are talking about. Goodbye.", true);
					storeData(%TrueClientId, "tmpdata", "");
					$state[%closestId, %TrueClientId] = "";
				}
			}
		}
		else if(%aitype $= "quest")
		{
			Schedule(0, game, "QUEST" @ %closestid.cFunction, %closestid.qobj,  %closestid, %TrueClientId, %cropped);
		}
		else if(%aitype $= "Porter")
		{
			//process manager code
			%trigger[2] = "fight";
			%trigger[3] = "refuse";
			if($state[%closestId, %TrueClientId] $= "")
			{
				if(%initTalk)
				{
					
					//AI::sayLater(%TrueClientId, %closestId, "Hail. Welcome to the arena, where the strongest warriors in the land compete for dominance! You may chose to FIGHT or you may REFUSE.", true);
					AI::sayLater(%TrueClientId, %closestId, "Sorry, arena is currently closed. Come back later!", true);
					$state[%closestId, %TrueClientId] = 0;
				}
			}
			else if($state[%closestId, %TrueClientId] $= 1)
			{
				if(strstr(%cropped, %trigger[2]) !$= -1)
				{
					//FIGHT
					%x = AddToRoster(%TrueClientId);
					if(%x !$= 0)
					{
						//TeleportToMarker(%TrueClientId, "TheArena/WaitingRoomMarkers", 0, 1);

						$state[%closestId, %TrueClientId] = "";
						MessageClient(%TrueClientId, 'RPGchatcallback', "You have entered the waiting room. To leave the wait room type #leavearena");
					}
					else
					{
						//arena is full
						AI::sayLater(%TrueClientId, %closestId, "Sorry, the arena roster is full right now.", true);
						$state[%closestId, %TrueClientId] = "";
					}
				}
				else if(strstr(%cropped, %trigger[3]) !$= -1)
				{
				

						AI::sayLater(%TrueClientId, %closestId, "That is too bad. However, we plan on some major improvements in the future, so I hope you choose to come fight later.", true);
				
				}
			}
		}
		else if(%aitype $= "botmaker")
		{
			//process botmaker code
			%trigger[2] = "yes";
			%trigger[3] = "no";
			if($state[%closestId, %TrueClientId] $= "")
			{
				if(%initTalk)
				{
					if(CountObjInCommaList($PetList) >= $maxPets)
					{
						AI::sayLater(%TrueClientId, %closestId, "I'm sorry but all my helpers are already on duty.", true);
						$state[%closestId, %TrueClientId] = "";
					}
					else if(CountObjInCommaList(fetchData(%TrueClientId, "PersonalPetList")) >= $maxPetsPerPlayer)
					{
						AI::sayLater(%TrueClientId, %closestId, "I'm sorry but you have too many helpers currently at your disposal.", true);
						$state[%closestId, %TrueClientId] = "";
					}
					else
					{
						AI::sayLater(%TrueClientId, %closestId, "I have all sorts of helpers at my disposal. Tell me which class you are interested in.", true);
						$state[%closestId, %TrueClientId] = 1;
					}
				}
			}
			else if($state[%closestId, %TrueClientId] $= 1)
			{
				%class = GetWord(%cropped, 0);
				%gender = GetWord(%cropped, 1);
				%defaults = $BotInfo[%aiName, DEFAULTS, %class];
				if(%gender $= -1)
					%gender = "Male";

				if(stricmp(%gender, "male") $= 0)
				{
					%gender = "Male";
					%gflag = true;
				}
				else if(stricmp(%gender, "female") $= 0)
				{
					%gender = "Female";
					%gflag = true;
				}

				if(stricmp(%class, "mage") $= 0)
					%class = "Mage";
				else if(stricmp(%class, "fighter") $= 0)
					%class = "Fighter";
				else if(stricmp(%class, "paladin") $= 0)
					%class = "Paladin";
				else if(stricmp(%class, "thief") $= 0)
					%class = "Thief";
				else if(stricmp(%class, "bard") $= 0)
					%class = "Bard";
				else if(stricmp(%class, "ranger") $= 0)
					%class = "Ranger";
				else if(stricmp(%class, "cleric") $= 0)
					%class = "Cleric";
				else if(stricmp(%class, "druid") $= 0)
					%class = "Druid";

				if(%defaults !$= "")
				{
					if(%gflag)
					{
						%lvl = GetStuffStringCount(%defaults, "LVL");
						%nc = mpow(%lvl, 2) * 3;
						$tmpdata[%TrueClientId, 1] = %class;
						$tmpdata[%TrueClientId, 2] = %gender;
						$tmpdata[%TrueClientId, 3] = %nc;	//just so the equation is only in one place.

						AI::sayLater(%TrueClientId, %closestId, "My " @ %class @ "s are Level " @ %lvl @ ", and will cost you " @ %nc @ " coins. [yes/no]", true);
						$state[%closestId, %TrueClientId] = 2;
					}
					else
					{
						AI::sayLater(%TrueClientId, %closestId, "Invalid gender. Use 'male' or 'female'.", true);
						$state[%closestId, %TrueClientId] = "";
					}
				}
				else
				{
					AI::sayLater(%TrueClientId, %closestId, "Invalid class. Use any of the following: mage fighter paladin ranger thief bard cleric druid.", true);
					$state[%closestId, %TrueClientId] = "";
				}
			}
			else if($state[%closestId, %TrueClientId] $= 2)
			{
				if(strstr(%cropped, %trigger[2]) !$= -1)
				{
					%nc = $tmpdata[%TrueClientId, 3];

					if(%nc <= 0)
					{
						AI::sayLater(%TrueClientId, %closestId, "Invalid request.  Your transaction has been cancelled.~wError_Message.wav", true);
						$state[%closestId, %TrueClientId] = "";
					}
					else if(%nc <= fetchData(%TrueClientId, "COINS"))
					{
						%class = $tmpdata[%TrueClientId, 1];
						%gender = $tmpdata[%TrueClientId, 2];
						%defaults = $BotInfo[%aiName, DEFAULTS, %class];
						%lvl = GetStuffStringCount(%defaults, "LVL");

						storeData(%TrueClientId, "COINS", %nc, "dec");
						%closestId.player.playAudio(0, SoundMoney1);
						RefreshAll(%TrueClientId);

						%n = "";
						for(%i = 0; (%a = GetWord($BotInfo[%aiName, NAMES], %i)) !$= ""; %i++)
						{
							if(getClientByName(%a) $= -1)
							{
								%n = %a;
								break;
							}
						}
						if(%n $= "")
							%n = "generic";

						$BotEquipment[generic] = "CLASS " @ %class @ " " @ %defaults;
						%an = AI::helper("generic", %n, "TempSpawn " @ $BotInfo[%aiName, DESTSPAWN].player.getPosition() @ " " @ GameBase::getTeam(%TrueClientId));
						%id = AI::getId(%an);
						%id.sex = %gender;
						ChangeRace(%id, "Human");
						storeData(%id, "tmpbotdata", %TrueClientId);
						storeData(%id, "botAttackMode", 2);

						schedule(55*60*1000, "Pet::BeforeTurnEvil(" @ %id @ ");");
						schedule(60*60*1000, "Pet::TurnEvil(" @ %id @ ");");

						$PetList = AddToCommaList($PetList, %id);
						storeData(%TrueClientId, "PersonalPetList", AddToCommaList(fetchData(%TrueClientId, "PersonalPetList"), %id));
						storeData(%id, "petowner", %TrueClientId);
					
						AI::sayLater(%TrueClientId, %closestId, "This is " @ %n @ ", a Level " @ %lvl @ " " @ %class @ "! He is at your disposal. He will follow you around and fight for you for the next hour.", true);
						$state[%closestId, %TrueClientId] = "";
					}
					else
					{
						AI::sayLater(%TrueClientId, %closestId, "You don't have enough coins. Goodbye.", true);
						$state[%closestId, %TrueClientId] = "";
					}

				}
				else if(strstr(%cropped, %trigger[3]) !$= -1)
				{
					AI::sayLater(%TrueClientId, %closestId, "As you wish. Goodbye.", true);
					$state[%closestId, %TrueClientId] = "";
				}
			}
		}
		else if(%aitype$= "Boat")
		{
			if($state[%closestId, %TrueClientId] $= "")
			{
				if(%inittalk)
				{
					AI::sayLater(%TrueClientId, %closestId, "Hello traveller! Welcome to my boat shop, would you like to RENT a boat for 100 coins?");
				}
				else if(strstr(%cropped, "rent") !$= -1)
				{
					if(fetchdata(%TrueClientId, "COINS") >= 100)
					{
						%obj = Boat @ %closestid.shop;
						if(isobject(%obj))
						{
	
							%flag = false;
							for(%i = 0; %i < %obj.getCount(); %i++)
							{
								
								%zobj = %obj.getObject(%i);
								InitContainerRadiusSearch(VectorAdd(%zobj.getWorldBoxCenter(), "-2 -2 -2"), "4 4 4", $TypeMasks::VehicleObjectType );
								%ret = containerSearchNext();
								if(%ret == 0)
								{
									%flag = true;
									break;
								}
							}
							if(%flag)
							{
								if(%TrueClientId.boat)
								{
									%TrueClientId.Boat.delete();
								}
								AI::sayLater(%TrueClientId, %closestId, "Ok here you go!");
								%spawnpos = %zobj.getWorldBoxCenter();
								
								%boat = new HoverVehicle() 
								{
										 dataBlock  = RPGBoat;
				
									};
									%waterheight = getword(VectorAdd(GlobalWater.position, GlobalWater.scale) , 2);
									%spawnpos = GetWord(%spawnpos, 0) SPC Getword(%spawnpos, 1) SPC %waterheight+3;
									%ztran = %zobj.getTransform();
									//%zrot = getword(%ztran, 3) SPC getword(%ztran, 4) SPC getword(%ztran, 5) SPC getword(%ztran, 6);
				
									%zrot = getWords(%ztran, 3, 5);
									%angle =  1;
								
								%boat.setTransform(%spawnpos SPC %zrot SPC %angle);
								MissionCleanup.add(%boat);
								
								%boat.owner = %trueclientid;
								
								%zobj.full = true;
								%trueclientid.boat = %boat;
								StoreData(%TrueClientId, "COINS", 100, "dec");
							}
							else
								AI::sayLater(%TrueClientId, %closestId, "Sorry I do not have any spots availible, come back later.");
						}
						else
							AI::sayLater(%TrueClientId, %closestId, "Sorry I do not have any spots availible, come back later. [ERROR]");
					}
					else 
						AI::sayLater(%TrueClientId, %closestId, "Come back when you have enough money!");
				}
			
			}
		}
		else if(%aiType $= "Guild")
		{
			
			//process guildmaster code
			if($state[%closestId, %TrueClientId] $= "")
			{
				if(%initTalk)
				{
					if(fetchData(%TrueClientId, "LVL") >= 25)
					{
							AI::sayLater(%TrueClientId, %closestId, "Hello adventurer. Here is a list of the guilds currently registered in this land.", true);
							$state[%closestId, %TrueClientId] = "";
							CommandToClient(%TrueClientId, 'OpenGuildGUI');
					}
					else
					{
						AI::sayLater(%TrueClientId, %closestId, "Come back when you are at least level 25. Goodbye.", true);
						$state[%closestId, %TrueClientId] = "";
					}
				}
			}
			
		}
	}
	else
	{
		//This condition occurs when you are talking from too far of any TownBot.  All states are cleared here.
		//This means that potentially, you could initiate a conversation with the banker, travel for an hour
		//WITHOUT saying a word, come back and continue the conversation.  As soon as you speak in a way that
		//townbots hear you (#say, #shout, #tell) and are too far from them, all conversations are reset.

		for(%i = 0; (%id = GetWord($TownBotList, %i)) !$= ""; %i++)
			$state[%id, %TrueClientId] = "";
	}
	
}