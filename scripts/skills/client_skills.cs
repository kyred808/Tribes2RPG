function clientCmdRegisterSkill(%skillId,%skillDesc,%skillInfo)
{
	$RPGSkill[%skillId] = %skillDesc;
	$RPGSkillInfo[%skillId] = %skillInfo;
}

function ClientCmdRegisterSkillCommand(%name,%cmd,%cat,%type)
{
	echo("ClientCmdRegisterSkill(" SPC %name SPC "," SPC %cmd SPC "," SPC %cat SPC "," SPC %type SPC ")");
	if($ClientSideRPGSkill::count $= "")
		$ClientSideRPGSkill::count = 0;
	
	%cnt = $ClientSideRPGSkill::count;
	$csRPGSkill::skillID[%cmd] = %cnt;
	$csRPGSkill::skill[%cnt,Name] = %name;
	$csRPGSkill::skill[%cnt,Command] = %cmd;
	$csRPGSkill::skill[%cnt,Catagory] = %cat;
	$csRPGSkill::skill[%cnt,Type] = %type;
	$ClientSideRPGSkill::count++;
}

function ClientCmdSetSkillCommandInfo(%cmd,%example,%desc,%rest)
{
	%id = $csRPGSkill::skillID[%cmd];
	$csRPGSkill::skill[%id,Example] = %example;
	$csRPGSkill::skill[%id,Desc] = %desc;
	$csSkillRestriction[%cmd] = %rest;
}

function TestSkillStorage()
{
	echo("Skill Library:");
	for(%i = 0; $csRPGSkill::skill[%i,Name] !$= ""; %i++)
	{	
		echo("===" SPC $csRPGSkill::skill[%i,Name] SPC "===");
		echo("Command:" SPC $csRPGSkill::skill[%i,Command]);
		echo("Catagory:" SPC $csRPGSkill::skill[%i,Catagory]);
		echo("Type:" SPC $csRPGSkill::skill[%i,Type]);
		echo("Example:" SPC $csRPGSkill::skill[%i,Example]);
		echo("Desc:" SPC $csRPGSkill::skill[%i,Desc]);		
		echo("Restrictions:" SPC $csSkillRestriction[$csRPGSkill::skill[%i,Command]]);
	}
}