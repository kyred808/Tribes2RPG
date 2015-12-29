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

// $RPGSkill::skillID["#say"] = 0;
// $RPGSkill::skill[0,Name] = "Say";
// $RPGSkill::skill[0,Command] = "#say";
// $RPGSkill::skill[0,Function] = "BasicSkill::say";
// $RPGSkill::skill[0,Catagory] = "basic";
// $RPGSkill::skill[0,Desc] = "basic";
// $RPGSkill::skill[0,Type] = $Skill::Speech;

// $RPGSkill::skill[0,Example] = "#say message";

function RPGSkill::AddSkill(%cmd,%name,%cat,%type,%func)
{
	%cnt = $RPGSkill::count;
	$RPGSkill::skillID[%cmd] = %cnt;
	$RPGSkill::skill[%cnt,Name] = %name;
	$RPGSkill::skill[%cnt,Command] = %cmd;
	$RPGSkill::skill[%cnt,Function] = %func;
	$RPGSkill::skill[%cnt,Catagory] = %cat;
	$RPGSkill::skill[%cnt,Type] = %type;
	$RPGSkill::count++;
}

function RPGSkill::UseSkill(%client,%cmd,%args,%adminLvl)
{
	if(Game.IsJailed(%client) && %adminLvl < 5)
			return;
	// if($RPGSkill::skillID[%cmd] !$= "")
	// {
	if(SkillCanUse(%client, %cmd))
	{
		%sid = $RPGSkill::skillID[%cmd];
		schedule(10,0,$RPGSkill::skill[%sid,Function],%client,%cmd,%args,%adminLvl);
	} else {
		messageClient(%client, 'RPGchatCallback', "You lack the necessary skills to use this command.");
		UseSkill(%client,  $RPGSkill::skill[$RPGSkill::skillID[%cmd],Type], false, true);
	}
	// } 
	// else {
		// messageClient(%client, 'RPGchatCallback', "That is not a valid command.");
	// }
	
}

function RPGSkill::SetDesc(%cmd,%desc)
{
	$RPGSkill::skill[$RPGSkill::skillID[%cmd],Desc] = %desc;
}

function RPGSkill::SetExample(%cmd,%example)
{
	$RPGSkill::skill[$RPGSkill::skillID[%cmd],Example] = %example;
}

function RPGSkill::SetRestrictions(%cmd,%reqs)
{
	$SkillRestriction[%cmd] = %reqs;
}

function ServerCmdSendSkillData(%client)
{
	for(%i = 0; %i < $RPGSkill::numberCatagories; %i++)
		CommandToClient(%client,'RegisterSkillCatagory',$RPGSkill::catagoryName[%i],$RPGSkill::catagoryDesc[%i]);
	for(%i = 0; %i < $RPGSkill::count; %i++)
	{
		CommandToClient(%client,'RegisterSkillCommand',$RPGSkill::skill[%i,Name],$RPGSkill::skill[%i,Command],$RPGSkill::skill[%i,Catagory],$RPGSkill::skill[%i,Type]);
		CommandToClient(%client,'SetSkillCommandInfo',$RPGSkill::skill[%i,Command],$RPGSkill::skill[%i,Example],$RPGSkill::skill[%i,Desc],$SkillRestriction[$RPGSkill::skill[%i,Command]]);
	}
}

exec("scripts/skills/basicskills.cs");