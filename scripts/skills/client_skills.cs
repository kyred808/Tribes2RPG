function ClientCmdRegisterSkillCatagory(%name,%desc,%tags)
{
	if($ClientSideRPGSkillCatagory::count $= "")
		$ClientSideRPGSkillCatagory::count = 0;
	
	%cnt = $ClientSideRPGSkillCatagory::count;
	$csRPGSkill::catagoryID[%name] = %cnt;
	$csRPGSkill::catagoryName[%cnt] = %name;
	$csRPGSkill::catagoryDesc[%cnt] = %desc;
	$csRPGSkill::catagoryUITags[%cnt] = %tags;
	$csRPGSkill::catagorySkillCmdCount[%name] = 0;
	$csRPGSkill::catagorySkillCmd[%name,0] = "";
	$ClientSideRPGSkillCatagory::count++;
}
//CommandToClient(%client,'RegisterSkillCatagoryUILine',%name,%k,$RPGSkill::catagoryUILine[%name,%k],$RPGSkill::catagoryUILine::dataTag[%name,%k]);
function ClientCmdRegisterSkillCatagoryUILine(%name,%line,%info,%tag)
{
	if($csRPGSkill::catagoryUILineCount[%name] $= "")
		$csRPGSkill::catagoryUILineCount[%name] = 0;
	
	$csRPGSkill::catagoryUILine[%name,%line] = %info;
	$csRPGSkill::catagoryUILine::dataTag[%name,%line] = %tag;
	$csRPGSkill::catagoryUILineCount[%name]++;
}

//Need to rename this, as this actually involves skill point skills.
function clientCmdRegisterSkill(%skillId,%skillDesc,%skillInfo)
{
	$RPGSkill[%skillId] = %skillDesc;
	$RPGSkillInfo[%skillId] = %skillInfo;
}

function ClientCmdRegisterSkillCommand(%cmd,%cat,%type)
{
	//echo("ClientCmdRegisterSkill(" SPC %name SPC "," SPC %cmd SPC "," SPC %cat SPC "," SPC %type SPC ")");
	if($ClientSideRPGSkill::count $= "")
		$ClientSideRPGSkill::count = 0;
	
	%cnt = $ClientSideRPGSkill::count;
	$csRPGSkill::skillID[%cmd] = %cnt;
	//$csRPGSkill::skill[%cnt,Name] = %name;
	$csRPGSkill::skill[%cnt,Command] = %cmd;
	$csRPGSkill::skill[%cnt,Catagory] = %cat;
	$csRPGSkill::skill[%cnt,Type] = %type;
	$ClientSideRPGSkill::count++;
	
	%catCnt = $csRPGSkill::catagorySkillCmdCount[%cat];
	$csRPGSkill::catagorySkillCmd[%cat,%catCnt] = %cnt;
	$csRPGSkill::catagorySkillCmdCount[%cat]++;
}

//Old method.
function ClientCmdSetSkillCommandInfo(%cmd,%example,%desc,%rest)
{
	%id = $csRPGSkill::skillID[%cmd];
	$csRPGSkill::skill[%id,Example] = %example;
	$csRPGSkill::skill[%id,Desc] = %desc;
	$csSkillRestriction[%cmd] = %rest;
}

function ClientCmdAddSkillCommandInfo(%cmd,%tag,%info)
{
	%id = $csRPGSkill::skillID[%cmd];
	$csRPGSkill::skill[%id,UI,%tag] = %info;
}

function TestSkillStorageV2()
{
	for(%i = 0; $csRPGSkill::skill[%i,UI,Name] !$= ""; %i++)
	{	
		echo("===" SPC $csRPGSkill::skill[%i,UI,"Name"] SPC "===");
		echo("Command:" SPC $csRPGSkill::skill[%i,Command]);
		echo("Catagory:" SPC $csRPGSkill::skill[%i,Catagory]);
		echo("Type:" SPC $csRPGSkill::skill[%i,Type]);
		echo("Example:" SPC $csRPGSkill::skill[%i,UI,"Example"]);
		echo("Desc:" SPC $csRPGSkill::skill[%i,UI,"Desc"]);		
		//echo("Restrictions:" SPC $csSkillRestriction[$csRPGSkill::skill[%i,Command]]);
	}
}

function CleanSkillInfo()
{
	deletevariables("$csRPGSkill*");
	deletevariables("$csRPGSkill::*")
	deletevariables("$ClientSideRPGSkill*");
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