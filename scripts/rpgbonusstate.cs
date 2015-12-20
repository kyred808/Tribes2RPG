//======================================================================
// Bonus States are special bonuses for a certain player that last a
// certain amount of ticks.  A tick is decreased every 2 seconds by
// the zone check.
//======================================================================
//except zone check doesnt work, so we have to find a new system. 
//$maxBonusStates = 10;

//function DecreaseBonusStateTicks(%client, %b)
//{
//	
//}

//While this method of applying bonuses is unique, I find it too limiting on what you can do with bonuses from TRPG 1.
//So I'm going to adapt it so it is similar to how TRPG 1 does it.  Also, I want bonuses to be saved.  -Kyred

$SpecialVarDesc[1] = "";
$SpecialVarDesc[2] = "";
$SpecialVarDesc[3] = "MDEF (Magical)";
$SpecialVarDesc[4] = "HP";
$SpecialVarDesc[5] = "Mana";
$SpecialVarDesc[6] = "ATK";
$SpecialVarDesc[7] = "DEF";
$SpecialVarDesc[8] = "[Internal]";
$SpecialVarDesc[9] = "";
$SpecialVarDesc[10] = "HP regen";
$SpecialVarDesc[11] = "Mana regen";
$SpecialVarDesc[12] = "Movement";


function DecreaseBonusStateTicks(%client, %amt, %name)
{
	if($debugMode) echo("DecreaseBonusStateTicks(" SPC %client SPC "," SPC %amt SPC "," SPC %name SPC ")");
	if(%name !$= "")  //Decrease for a specific bonus.
	{
		if(IsInCommaList(%client.data.BonusList, %name))
		{
			if(%amt !$= "" || %amt > 0)
				%client.data.BonusTicks[%name] -= %amt;
			else			
				%client.data.BonusTicks[%name]--;
			
			if(%client.data.BonusTicks[%name] <= 0)  //Bonus has expired.  Removed it from the list.
			{
				%client.data.BonusList = strreplace(%client.data.BonusList, %name,"");
				%client.data.BonusTicks[%name] = "";
				%client.data.BonusState[%name] = "";
				//Add client notification here.
			}
		}
	}
	else //All bonuses.
	{
		for(%i = 0; (%name = GetWord(%client.data.BonusList, %i)) !$= ""; %i++)
		{
			if(%amt !$= "" || %amt > 0)
				%client.data.BonusTicks[%name] -= %amt;
			else			
				%client.data.BonusTicks[%name]--;
				
			if(%client.data.BonusTicks[%name] <= 0)  //Bonus has expired.  Removed it from the list.
			{
				%client.data.BonusList = strreplace(%client.data.BonusList, %name,"");
				%client.data.BonusTicks[%name] = "";
				%client.data.BonusState[%name] = "";
				//Add client notification here.
			}
		}
	}
}

//This function created a bonus state.
function OldAddBonusState(%client, %bonus, %ticks, %name)
{
	//if($debugger == %client) echo("AddBonusState(" SPC %client SPC %bonus SPC %ticks SPC %name SPC ")");
	//add entry to bonus state system
	//lets not use global vars this time
	//name is the 'name' of the bonus this way we can prevent 'stacking' in addpoints we add this to the players statistics
	CalculateBonusState(%client);//update the bonus state delete as nessisary.

	//add new entry or update old one.
	//if(%client.bonusTicks[%name] == 0)
	//navigate bonus list and see if its there
	if(!IsInCommaList(%client.BonusList, %name))
	%client.BonusList = rtrim(%client.bonusList @ %name @ " ");
	%client.BonusState[%name] = %bonus;
	
	%client.BonusTicks[%name] = %ticks;
	//if($debugger == %client) echo(%client.bonusList[%name] SPC %name SPC %client.bonusticks[%name] SPC %ticks);
}

//Example of bonus: AddBonusState(%clVictim, "12 -40", %time, "Encumber");
//In this case, name can be for a specific bonus, or catagory of bonuses.
function AddBonusState(%client, %bonus, %ticks, %name) //, %name)
{
	if(%name $= "")
		%name = "Generic";
	%bInList = IsInCommaList(%client.data.BonusList, %name);
	//%bonusId = GetWord(%bonus,0);
	//%bonusAmt = GetWord(%bonus,1);
	if(!%bInList) {
		%client.data.BonusList = rtrim(%client.data.BonusList @ %name @ " ");
		%client.data.BonusCount++;
		%client.data.BonusState[%name] = %bonus;
	}		
	%client.data.BonusTicks[%name] = %ticks;
}

//Add up the points from a bonus.  But Don't update expiration.
function AddBonusStatePoints(%client, %filter)
{
	if($debugMode) echo("AddBonusStatePoints(" SPC %client SPC "," SPC %filter SPC")");
	if(%filter $= "") return; //error

	%add = 0;
	if(%filter $= 3 || %filter $= 7 || %filter $= 6 || %filter $= 20 || %filter $= 21 || %filter $= 22 || %filter $= 23 || %filter $= 24 || %filter $= 25)
		%add = "0r0";		
	
	//echo("Made it this far...");
	for(%i = 0; GetWord(%client.data.BonusList, %i) !$= ""; %i++)
	{
		%name = GetWord(%client.data.BonusList, %i);
		%state = %client.data.bonusState[%name];
		//exists!
		for(%j = 0; GetWord(%state, %j) !$= ""; %j = %j + 2)
		{
			//echo("Bonus state list loop!");
			%a = GetWord(%state, %j);
			%b = GetWord(%state, %j+1);
			if(%a == %filter)
			{
				//If it is just a number, add it.
				if(strreplace(%b, "r", " ") $= %b && strreplace(%b, "R", " ") $= %b)
				{
					%add += %b;
				}
				else //Otherwise, combine it as a roll.
				{
					%tmp = strreplace(%b, "r", " ");
					if(%tmp $= %b)
						%tmp = strreplace(%b, "R", " ");
					%add = CombineRpgRolls(%add, %b, 0, "inf");
				}					
			}
		}
	}
	return %add;
}

function OldAddBonusStatePoints(%client, %filter)
{
	if(%filter $= "") return; //error
	//this function both checks expiration of bonus states and returns the correct value of the summery of all bonus states.
	%now = mfloor(getSimTime()/1000);
	if(%client.lastbonuscalc == 0) %client.lastbonuscalc = %now;
	%diff =  %now - %client.lastbonuscalc;
	
	if(%filter $= 3 || %filter $= 7 || %filter $= 6 || %filter $= 20 || %filter $= 21 || %filter $= 22 || %filter $= 23 || %filter $= 24 || %filter $= 25)
		%add = "0r0";
	else
		%add = 0;

	
	for(%i = 0; GetWord(%client.BonusList, %i) !$= ""; %i++)
	{
		%name = GetWord(%client.BonusList, %i);
		
		if(%client.bonusTicks[%name] <= %diff)
		{
			//no longer exists
			%client.bonusState[%name] = "";
			%client.bonusTicks[%name] = "";
		}
		else
		{
			%state = %client.bonusState[%name];
			//exists!
			for(%j = 0; GetWord(%state, %j) !$= ""; %j = %j + 2)
			{
				%a = GetWord(%state, %j);
				%b = GetWord(%state, %j+1);
				if(%a == %filter)
				{
					//good add itup!
					if(strreplace(%b, "r", " ") $= %b && strreplace(%b, "R", " ") $= %b)
					{
						//normal
						%add += %b;
					}
					else
					{
						%tmp = strreplace(%b, "r", " ");
						if(%tmp $= %b)
							%tmp = strreplace(%b, "R", " ");
						%add = CombineRpgRolls(%add, %b, 0, "inf");
					}					
				}
			}
			%list = %list @ %name @ " ";
			%client.bonusTicks[%name] = %client.bonusTicks[%name] - %diff;
		}
	
	}
	%client.BonusList = %list;
	%client.lastbonuscalc = %now;
	return %add;
}

function GetBonusTimeLeft(%client, %name)
{
	//CalculateBonusState(%client);
	return %client.data.BonusTicks[%name];
}

function OldGetBonusTimeLeft(%client, %name)
{
	CalculateBonusState(%client);
	return %client.BonusTicks[%name];
}
//function UpdateBonusState()
//{
//	return;//remove errospam?
//}


//No longer used.
function OldCalculateBonusState(%client)
{
	%now = mfloor(getSimTime()/1000);
	if(%client.lastbonuscalc == 0) %client.lastbonuscalc = %now;
	//go through the list of names and recompile as we go.
	
	%diff =  %now - %client.lastbonuscalc;

	if(%diff > 0)
	{
		for(%i = 0; GetWord(%client.bonuslist, %i) !$= ""; %i++)
		{
			%name = getWord(%client.bonuslist, %i);
			if(%client.bonusTicks[%name] <= %diff)
			{
				//remove entry;
				%client.BonusTicks[%name] = "";
				%client.BonusState[%name] = "";
			}
			else
			{
				%client.bonusTicks[%name] = %client.bonusTicks[%name] - %diff;
				%list = %list @ %name @ " ";
			}


		}
	}
	else
		%list = %client.bonuslist;
	%client.bonuslist = %list;//updated list
	%client.lastbonuscalc = %now;
}

function debugBonusState(%client)
{
	echo(%client.rpgname SPC "BONUS LIST");
	for(%i = 0; GetWord(%client.data.bonuslist, %i) !$= ""; %i++)
	{
		%name = getWord(%client.data.bonuslist, %i);

		echo("NAME" SPC %name SPC "TICK" SPC %client.data.BonusTicks[%name] SPC "VALUE" SPC %client.data.BonusState[%name]);
	}
	echo("END LIST");
}

function OlddebugBonusState(%client)
{
	echo(%client.rpgname SPC "BONUS LIST");
	for(%i = 0; GetWord(%client.bonuslist, %i) !$= ""; %i++)
	{
		%name = getWord(%client.bonuslist, %i);

		echo("NAME" SPC %name SPC "TICK" SPC %client.BonusTicks[%name] SPC "VALUE" SPC %client.BonusState[%name]);
	}
	echo("END LIST");
}

//This was just used for spell resistance.  I think it subtracts the bonus value based on the damage done?
//A better description would be nice.  Anyway, I commented out that section and made spell resistance just work as MDEF.
//I might revist this later.  -Kyred
function OldModifyBonusState(%client, %char, %mod)
{
	
	//if(%mod <= 0) return 0;
	//here we go.
	%now = mfloor(getSimTime()/1000);
	if(%client.lastbonuscalc == 0) %client.lastbonuscalc = %now;
	//go through the list of names and recompile as we go.
	
	%diff =  %now - %client.lastbonuscalc;
	for(%i = 0; GetWord(%client.bonuslist, %i) !$= ""; %i++)
	{
		%name = getWord(%client.bonuslist, %i);
		if(%client.bonusTicks[%name] <= %diff)
		{
			//remove entry;
			%client.BonusTicks[%name] = "";
			%client.BonusState[%name] = "";
		}
		else
		{
			%client.bonusTicks[%name] = %client.bonusTicks[%name] - %diff;
			%list = %list @ %name @ " ";
			//earlier casted spells are in the front of the list
			%a = %client.BonusState[%name];
			
			if( strreplace(%mod, "r", " ") $= %mod && strreplace(%mod, "R", " ") $= %mod )
			{
				%ran = false;
				%mod1 = %mod;
			}
			else
			{
				%ran = true;
			
				%tmp = strreplace(%mod, "r", " ");
				if(%tmp == %v)
					%tmp = strreplace(%mod, "R", " ");
				%mod1 = mfloor(getword(%tmp, 0));
				%mod2 = mfloor(getword(%tmp, 1)); 
				
			}
			%final = "";
			for(%j = 0; GetWord(%a, %j) !$= ""; %j+=2)
			{
				%e = GetWord(%a, %j);
				%v = GetWord(%a, %j+1);
				
				%add = true;
				if(%e == %char )
				{
					%add = false;
					if(strreplace(%v, "r", " ") $= %v && strreplace(%v, "R", " ") $= %v)
					{
						//normal
						if(%ran)
						{
							if(%v > %mod1)
							{
								%v -= %mod1;
								%mod1 = 0;
							}
							else
							{
								%mod1 -= %v;
								%v = 0;
							}
							if(%v > %mod2)
							{
								%v -= %mod2;
								%mod2 = 0;
							}
							else
							{
								%mod2 -= %v;
								%v = 0;
							}
						}
						else
						{
							if( %mod1 > %v)
							{
								%mod1 -= %v;
								%v = 0;
							}
							else
							{
								%v -= %mod1;
								%mod1 = 0;
							}
						}
							if(%v == 0)
								%add = false;
							else
								%add = true;
					}
					else
					{
						%tmp = strreplace(%v, "r", " ");
						if(%tmp == %v)
							%tmp = strreplace(%v, "R", " ");
						%g = mfloor(getword(%tmp, 0));
						%f = mfloor(getword(%tmp, 1));
						if(%ran)
						{
							if(%mod2 > %f)
							{
								%mod2 -= %f;
								%f = 0;
								%mod1 -= %g;
								%g = 0;
							}
							else
							{
								%f -= %mod2;
								%g -= %mod1;
								if(%g < 0)
								%g = 0;
								%mod2 = 0;
								%mod1 = 0;
							}
						
						}
						else
						{
							if(%mod1 > %f)
							{
								%mod1 -= %f;
								%f = 0;
								%g = 0;
							}
							else
							{
								%f -= %mod1;
								if(%mod1 > %g)
								%g = 0;
								else
								%g -= %mod1;
								%mod1 = 0;
							}
						}
						if(%g < 1) %g = 1;
						if(%f < 1) %f = 1;
						if(%g > %f)
						%g = %f;
						%v = %g @ "r" @ %f;
						if(%f > 0)
						%add = true;
						else
						%add = false;
						
						

					}
				}
				if(%add)
				%final = %final @ %e SPC %v @ " ";
				

			}
			if(%final $= "")
			{
				%client.bonusTicks[%name] = 0;//delete!
			}
			else
				%client.bonusState[%name] = %final;
			if(%ran)
			{
				%mod = %mod1 @ "r" @ %mod2;
			}
			else
				%mod = %mod1;
			
		}
	
	
	}
	%client.bonuslist = %list;//updated list	
	%client.lastbonuscalc = %now;
	if($debug)
	debugbonusstate(%client);
	return %mod;
}