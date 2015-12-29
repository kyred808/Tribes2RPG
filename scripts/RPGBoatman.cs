function SpawnBoatman(%index,%pathGroup,%pos)
{
	%name = "Boatman" SPC %index;
	%ai = aiConnect(%name, 1);
	%ai.boatmanIndex = %index;
	%ai.initPos = %pos;
	$RPGGame::boatman[%index] = %ai;
	%ai.data = new ScriptObject();
	
	if(isObject(%ai))
	{
		Game.AIHasJoined(%ai);
		storeData(%ai,"LVL",getRPGroll("80r100")); //Hard coded for now.
		storeData(%ai,"CLASS","Fighter");
		storeData(%ai,"RACE","MaleHuman");
		storeData(%ai,"magic",0);
		HardcodeAIskills(%ai);
		%ai.team = 1;
		%armor = "MaleHumanArmor";
		
		%player = new Player()
		{
			dataBlock = %armor;
			position = "0 0 1000";
		};
		%player.setDataBlock(%armor);
		%player.setOwnerClient(%ai);
		%ai.player = %player;
		%ai.setControlObject(%player);
		//Let's spawn his vehicle first.
		
		%pos = VectorAdd(%pos,"0 0 8");
		%veh = new HoverVehicle() 
		{
			dataBlock  = RPGBigBoat;
			position = %pos;
     	};
		%ai.vehicle = %veh;
		%veh.owner = %ai;
		%veh.mountObject(%ai.player,0);
		%ai.setControlObject(%veh);
		%ai.setPilotPitchRange(-0.2, 0.05, 0.05); //Tweak this later.
		%ai.boatGroup = %pathGroup;
		%ai.addTask(AIBoatman);
	}
}

function AIBoatman::init(%task, %client)
{
	%task.boatGroup = %client.boatGroup;
}

function AIBoatman::assume(%task, %client)
{
   %task.setWeightFreq(100);
   %task.setMonitorFreq(10);
	%task.currentNode = 0;
	%task.forward = true;
	%task.state = "MoveToNextNode";
	//%task.bStart = true;
//    //next, start the pilot on his way to mounting the vehicle
//    %client.pilotVehicle = true;
//    %client.stepMove($player.flyer.position, 0.25, $AIModeMountVehicle);
}

function AIBoatman::weight(%task, %client)
{
   %task.setWeight(10000);
}

function AIBoatman::monitor(%task, %client)
{
	//echo("Monitor");
   //messageall(0, " AITraining1Pilot::monitor "@%task.locationIndex);
	if(!%task.locationIndex)
		%task.locationIndex = 0;
	%group = %client.boatGroup;
	

	if(%client.vehicleMounted)
	{
		%targetNode = %group.getObject(%task.locationIndex);
		switch$(%task.state)
		{
			case "MoveToNextNode":
				%targetNode = %group.getObject(%task.locationIndex);
				%client.setPilotDestination(%targetNode.position,0.8);
				//%client.setPilotAim($PilotAim);
				%pos = %client.vehicle.position;
				%pos2D = getWord(%pos, 0) SPC getWord(%pos, 1) SPC "0";
				%dest =  %targetNode.position;
				%dest2D = getWord(%dest, 0) SPC getWord(%dest, 1) SPC "0";
				//echo("Location Index:" SPC %task.locationIndex);
				if (VectorDist(%dest2D, %pos2D) < 20)
				{
					if(%targetNode.dock)
					{
						%task.state = "docked";
						%task.docktime = getSimTime();
						%client.setControlObject(%client.player);
						%task.setMonitorFreq(100);
						if(%targetNode.dockWaitTime !$= "")
							%task.waitTime = %targetNode.dockWaitTime;
						else
							%task.waitTime = 10000; //Default is 10 seconds;
					}
					if(%targetNode.bAlignFirst)
					{
						%task.state = "Align";
					}
					
					//Determine next target.
					if(%group.looptype $= "" || %group.looptype $= "circuit")
					{
						//echo("Loop type circuit");
						if(%group.getCount() > %task.locationIndex + 1)
							%task.locationIndex++;
						else
							%task.locationIndex = 0;
					}
					else if(%group.looptype $= "invert")
					{
						%cnt = %group.getCount();
						//echo("Forward:" SPC %task.forward SPC "Index:" SPC %task.locationIndex @"/"@ %cnt);
						if(%task.forward)
						{
							%task.locationIndex++;
							if(%task.locationIndex >= %cnt - 1)
								%task.forward = false;								
						}
						else
						{
							%task.locationIndex--;
							if(%task.locationIndex <= 0)
								%task.forward = true;
						}
					}
				}
			case "Docked":
				if(getSimTime() - %task.docktime > %task.waitTime)
				{
					%client.setControlObject(%client.vehicle);
					%task.setMonitorFreq(10);
					%task.state = "Align";
					%task.docktime = "";
					%task.waitTime = "";
				}
			case "Align":
				%pos = %client.vehicle.position;
				%pos2D = getWord(%pos, 0) SPC getWord(%pos, 1) SPC "0";
				%dest =  %targetNode.position;
				%dest2D = getWord(%dest, 0) SPC getWord(%dest, 1) SPC "0";
				%targetDir = VectorNormalize(VectorSub(%dest2D,%pos2D));				
				//Dir and Forward Vector need to line up.
				%forward = %client.vehicle.getForwardVector();
				%forward2D = getWord(%forward,0) SPC getWord(%forward,1) SPC "0";
				//echo("DIR:" SPC %targetDir);
				//echo("Forward:" SPC %forward2D);
				if(VectorDot(%forward2D,%targetDir) < 0.98) {
					//echo("setAim");
					//%client.setPilotDestination(%dest,0.1);
					%client.setPilotAim(%targetNode.position);
					//echo(VectorDot(%forward2D,%targetDir));
				} else {
					%task.state = "MoveToNextNode";
				}

				
		}
	}
	else
	{
		if(IsObject(%client.vehicle) && %client.vehicle.getState !$= "Destroyed")
			%client.stepMove(%client.vehicle, 0.25, $AIModeExpress);
		else
		{
			Game.schedule(5000,"SpawnBoatman",%client.boatmanIndex,%group,%client.initPos);
			%client.schedule(2000,"onAIDrop");
			%client.schedule(2500,"delete");
		}
	}

   
}