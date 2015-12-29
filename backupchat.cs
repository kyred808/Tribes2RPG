switch$(%w1)
		{
			case "#say":
				if(SkillCanUse(%TrueClientId, "#say"))
				{
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
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You lack the necessary skills to use this command.");
					UseSkill(%TrueClientId, $SkillSpeech, false, true);
				}
	
			case "#shout":
				if(SkillCanUse(%TrueClientId, "#shout"))
				{
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
	
					%botTalk = true;
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You lack the necessary skills to use this command.");
					UseSkill(%TrueClientId, $SkillSpeech, false, true);
				}

			case "#whisper":
				if(SkillCanUse(%TrueClientId, "#whisper"))
				{
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
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You lack the necessary skills to use this command.");
					UseSkill(%TrueClientId, $SkillSpeech, false, true);
				}
		}

		if(Game.IsJailed(%TrueClientId) && %clienttoServeradminlevel < 5)
			return;

		switch$(%w1)
		{
			case "#tell":
				if(SkillCanUse(%TrueClientId, "#tell"))
				{
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
			
					%botTalk = true;
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You lack the necessary skills to use this command.");
					UseSkill(%TrueClientId, $SkillSpeech, false, true);
				}

			case "#r":
				if(SkillCanUse(%TrueClientId, "#tell"))
				{
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
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You lack the necessary skills to use this command.");
					UseSkill(%TrueClientId, $SkillSpeech, false, true);
				}
				return;

			case "#global" or "#g":
				if(SkillCanUse(%TrueClientId, "#global"))
				{
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
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You lack the necessary skills to use this command.");
					UseSkill(%TrueClientId, $SkillSpeech, false, true);
				}
				return;
			case "#guild" or "#gu":
				if(SkillCanUse(%TrueClientId, "#guild"))
				{
					%guildid = IsInWhatGuild(%client);
					%guild = GuildGroup.GetObject(%guildid);
					if(%guildid != -1 || %guild.GetGUIDRank(%TrueClientId.guid) == 0)
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
				
				
				}
				else
					messageClient(%TrueClientID, 'RPGchatCallback', "You lack the necessary skills to use this command.");
			
				return;
			case "#zone" or "#z":
				if(SkillCanUse(%TrueClientId, "#zone"))
				{
					%count = ClientGroup.getCount();
					for(%icl = 0; %icl < %count; %icl++)
					{
						%cl = ClientGroup.getObject(%icl);
						if(!%cl.muted[%TrueClientId] && %cl !$= %TrueClientId && fetchData(%cl, "zone") $= fetchData(%TrueClientId, "zone"))
				      		messageClient(%cl, 'RPGchatCallback', "[ZONE] " @ %TCsenderName @ " \"" @ %cropped @ "\"");
					}
					messageClient(%TrueClientId, 'RPGchatCallback', "[ZONE] \"" @ %cropped @ "\"");
	
					UseSkill(%TrueClientId, $SkillSpeech, true, true);
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You lack the necessary skills to use this command.");
					UseSkill(%TrueClientId, $SkillSpeech, false, true);
				}
				return;

			case "#group":
				if(SkillCanUse(%TrueClientId, "#group"))
				{
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
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You lack the necessary skills to use this command.");
					UseSkill(%TrueClientId, $SkillSpeech, false, true);
				}
				return;

			case "#party" or "#p":
				if(SkillCanUse(%TrueClientId, "#party"))
				{
					%list = GetPartyListIAmIn(%TrueClientId);
					
					for(%p = strstr(%list, ","); (%p = strstr(%list, ",")) !$= -1; %list = getsubstr(%list, %p+1, 99999))
					{
						%cl = getsubstr(%list, 0, %p);
						
						if(!%cl.muted[%TrueClientId] && %cl !$= %TrueClientId)
							messageClient(%cl, 'RPGchatCallback', "[PRTY] " @ %TCsenderName @ " \"" @ %cropped @ "\"");
					}
	
					messageClient(%TrueClientId, 'RPGchatCallback', "[PRTY] \"" @ %cropped @ "\"");
					UseSkill(%TrueClientId, $SkillSpeech, true, true);
				}
				else
				{
					messageClient(%TrueClientId, 'RPGchatCallback', "You lack the necessary skills to use this command.");
					UseSkill(%TrueClientId, $SkillSpeech, false, true);
				}
				return;
			case "#leavearena":
				if(inArenaroster(%TrueClientId))
				{
					leaveArena(%TrueClientId);
				
				}
				else
					messageClient(%TrueClientId, 'RPGchatCallback', "You must be in the arena wait room in order to leave it");
		}