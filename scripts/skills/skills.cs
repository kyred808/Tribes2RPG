if($RPGSkill::count > 0)
	deletevariables("$RPGSkill::*");
$RPGSkill::numberCatagories = 0;
$RPGSkill::count = 0;
//$RPGSkill::catagory["basic"] = 0;
//$RPGSkill::name[0] = "basic";

function RPGSkill::AddSkillCatagory(%name,%desc)
{
	if($RPGSkill::numberCatagories $= "")
	{
		$RPGSkill::numberCatagories = 0;
	}
	%cnt = $RPGSkill::numberCatagories;
	$RPGSkill::catagory[%name] = %cnt;
	$RPGSkill::catagoryName[%cnt] = %name;
	$RPGSkill::catagoryDesc[%cnt] = %desc;
	$RPGSkill::numberCatagories++;
}

function RPGSkill::catagoryAddUITag(%catName,%tag)
{
	if($RPGSkill::catagoryUITags[%catName] $= "")
		$RPGSkill::catagoryUITags[%catName] = %tag;
	else
		$RPGSkill::catagoryUITags[%catName] = $RPGSkill::catagoryUITags[%catName] SPC %tag;
}

//If %tag is left blank, then line will simply be %info.
//If %tag is not blank, then the resulting line will be %info concat with the skill's UI tag in %tag.
function RPGSkill::catagoryUIInfoLine(%catName,%line,%info,%tag)
{
	if($RPGSkill::catagoryUILineCount[%catName] $= "")
		$RPGSkill::catagoryUILineCount[%catName] = 0;
	
	%line = $RPGSkill::catagoryUILineCount[%catName];
	$RPGSkill::catagoryUILine::dataTag[%catName,%line] = %tag;
	$RPGSkill::catagoryUILine[%catName,%line] = %info;
	$RPGSkill::catagoryUILineCount[%catName]++;
}

// function RPGSkill::catagory::addUIFormat(%catName,%tag,%format)
// {
	// $RPGSkill::catagoryUIFormat[%catName,%tag] = %format;
// }

// function RPGSkill::catagory::addUITitle(%catName,%tag,%title)
// {
	// $RPGSkill::catagoryUIFormat[%catName,%tag,Title] = %format;
// }

// $RPGSkill::skillID["#say"] = 0;
// $RPGSkill::skill[0,Name] = "Say";
// $RPGSkill::skill[0,Command] = "#say";
// $RPGSkill::skill[0,Function] = "BasicSkill::say";
// $RPGSkill::skill[0,Catagory] = "basic";
// $RPGSkill::skill[0,Desc] = "basic";
// OLD $RPGSkill::skill[0,Type] = $Skill::Speech;
// $RPGSkill::skill[0,Type] = 'Skill';
// $RPGSkill::skill[0,Dead] = true;
// $RPGSkill::skill[0,Jail] = true;

// $RPGSkill::skill[0,Example] = "#say message";

// function OldRPGSkill::AddSkill(%cmd,%name,%cat,%type,%func)
// {
	// %cnt = $RPGSkill::count;
	// $RPGSkill::skillID[%cmd] = %cnt;
	// $RPGSkill::skill[%cnt,Name] = %name;
	// $RPGSkill::skill[%cnt,Command] = %cmd;
	// $RPGSkill::skill[%cnt,Function] = %func;
	// $RPGSkill::skill[%cnt,Catagory] = %cat;
	// $RPGSkill::skill[%cnt,Type] = %type;
	// $RPGSkill::skill[%cnt,Dead] = false;
	// $RPGSkill::skill[%cnt,Jail] = false;
	// $RPGSkill::count++;
// }

function RPGSkill::AddSkill(%cmd,%cat,%type,%func)
{
	%cnt = $RPGSkill::count;
	$RPGSkill::skillID[%cmd] = %cnt;
	//$RPGSkill::skill[%cnt,Name] = %name;
	$RPGSkill::skill[%cnt,Command] = %cmd;
	$RPGSkill::skill[%cnt,Function] = %func;
	$RPGSkill::skill[%cnt,Catagory] = %cat;
	$RPGSkill::skill[%cnt,Type] = %type;
	$RPGSkill::skill[%cnt,Dead] = false;
	$RPGSkill::skill[%cnt,Jail] = false;
	$RPGSkill::count++;
}

function RPGSkill::addUIInfo(%cmd,%tag,%info)
{
	%id = $RPGSkill::skillID[%cmd];
	$RPGSkill::skill[%id,UI,%tag] = %info;
}

function RPGSkill::UseCommand(%client,%cmd,%args,%adminLvl)
{
	%id = $RPGSkill::skillID[%cmd];
	
	//Check if it can be used in jail or not.
	if(Game.IsJailed(%client) && !$RPGSkill::skill[%id,Jail] && %adminLvl < 5)
		return;
	
	//Can we use this while dead?
	if(IsDead(%client) && !$RPGSkill::skill[%id,Dead]) //&& %client !$= 2048) //Not sure if 2048 is a safe client id anymore.
		return;
		
	if($RPGSkill::skill[%cnt,Type] !$= "cmd")
	{
		if(SkillCanUse(%client, %cmd))
			schedule(10,0,$RPGSkill::skill[%id,Function],%client,%cmd,%args,%adminLvl);
		else {
			messageClient(%client, 'RPGchatCallback', "You lack the necessary skills to use this command.");
			UseSkill(%client,  $RPGSkill::skill[$RPGSkill::skillID[%cmd],Type], false, true);
		}
	}
	else
		schedule(10,0,$RPGSkill::skill[%id,Function],%client,%cmd,%args,%adminLvl);
}

// function RPGSkill::UseSkill(%client,%cmd,%args,%adminLvl)
// {
	// if(Game.IsJailed(%client) && %adminLvl < 5)
		// return;
	// // if($RPGSkill::skillID[%cmd] !$= "")
	// // {
	// if(SkillCanUse(%client, %cmd))
	// {
		// %sid = $RPGSkill::skillID[%cmd];
		// schedule(10,0,$RPGSkill::skill[%sid,Function],%client,%cmd,%args,%adminLvl);
	// } else {
		// messageClient(%client, 'RPGchatCallback', "You lack the necessary skills to use this command.");
		// UseSkill(%client,  $RPGSkill::skill[$RPGSkill::skillID[%cmd],Type], false, true);
	// }
	// // } 
	// // else {
		// // messageClient(%client, 'RPGchatCallback', "That is not a valid command.");
	// // }
	
// }

//Some helper functions for common tags.
function RPGSkill::SetName(%cmd,%name)
{
	RPGSkill::AddUIInfo(%cmd,"Name",%name);
}
function RPGSkill::SetDesc(%cmd,%desc)
{
	RPGSkill::AddUIInfo(%cmd,"Desc",%desc);
	//$RPGSkill::skill[$RPGSkill::skillID[%cmd],Desc] = %desc;
}

function RPGSkill::SetExample(%cmd,%example)
{
	RPGSkill::AddUIInfo(%cmd,"Example",%example);
	//$RPGSkill::skill[$RPGSkill::skillID[%cmd],Example] = %example;
}

function RPGSkill::setDeadJail(%cmd,%dead,%jail)
{
	$RPGSkill::skill[%cnt,Dead] = %dead;
	$RPGSkill::skill[%cnt,Jail] = %jail;
}


// function RPGSkill::SetRestrictions(%cmd,%reqs)
// {
	// $SkillRestriction[%cmd] = %reqs;
// }

function ServerCmdSendSkillData(%client)
{
	CommandToClient(%client,'ResetSkillInfo');
	for(%i = 0; %i < $RPGSkill::numberCatagories; %i++) //Catagory stuff first.
	{
		%name = $RPGSkill::catagoryName[%i];
		CommandToClient(%client,'RegisterSkillCatagory',%name,$RPGSkill::catagoryDesc[%i],$RPGSkill::catagoryUITags[%name]);
		for(%k = 0; %k < $RPGSkill::catagoryUILineCount[%name]; %k++)
		{
			CommandToClient(%client,'RegisterSkillCatagoryUILine',%name,%k,$RPGSkill::catagoryUILine[%name,%k],$RPGSkill::catagoryUILine::dataTag[%name,%k]);
		}
	}
	for(%i = 0; %i < $RPGSkill::count; %i++)
	{
		%skill = $RPGSkill::skill[%i,Command];
		%cat = $RPGSkill::skill[%i,Catagory];
		CommandToClient(%client,'RegisterSkillCommand',%skill,%cat,$RPGSkill::skill[%i,Type]);
		for(%k = 0; getWord($RPGSkill::catagoryUITags[%cat],%k) !$= ""; %k++)
		{
			%tag = getWord($RPGSkill::catagoryUITags[%cat],%k);
			CommandToClient(%client,'AddSkillCommandInfo',%skill,%tag,$RPGSkill::skill[%i,UI,%tag]);
		}
		//CommandToClient(%client,'RegisterSkillCommand',$RPGSkill::skill[%i,Name],$RPGSkill::skill[%i,Command],$RPGSkill::skill[%i,Catagory],$RPGSkill::skill[%i,Type]);
		//CommandToClient(%client,'SetSkillCommandInfo',$RPGSkill::skill[%i,Command],$RPGSkill::skill[%i,Example],$RPGSkill::skill[%i,Desc],$SkillRestriction[$RPGSkill::skill[%i,Command]]);
		
	}
}

exec("scripts/skills/basicskills.cs");
exec("scripts/skills/rpgcommands.cs");