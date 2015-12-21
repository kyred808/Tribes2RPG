
function findEmptySeat(%vehicle, %player, %forceNode)
{
   %node = -1;
   %dataBlock = %vehicle.getDataBlock();
   %dis = 25;
   %playerPos = getWords(%player.getTransform(), 0, 2);
   %message = "";
   %minNode = 0;
 if(%forceNode !$= "")
      %node = %forceNode;
   else
   {
      for(%i = 0; %i < %dataBlock.numMountPoints; %i++)
         if(!%vehicle.getMountNodeObject(%i))
         {
            %seatPos = getWords(%vehicle.getSlotTransform(%i), 0, 2);
            %disTemp = VectorLen(VectorSub(%seatPos, %playerPos));
            if(%disTemp <= %dis && ( %datablock.isProtectedMountPoint[%i] == false || %vehicle.owner == %player.client ))
            {
               %node = %i;
               %dis = %disTemp;
            }
         }
    }
   if(%node != -1 && %node < %minNode)
   {
      if(%message $= "")
      {
         if(%node == 0)
            %message = '\c2No node found.~wfx/misc/misc.error.wav';
         else
            %message = '\c2Only Scout or Assault Armors can use that position.~wfx/misc/misc.error.wav';
      }
      
      if(!%player.noSitMessage)
      {
         %player.noSitMessage = true;
         %player.schedule(2000, "resetSitMessage");
         messageClient(%player.client, 'MsgArmorCantMountVehicle', %message);   
      }
      %node = -1;
   }
   return %node;
}


//**************************************************************
// SOUNDS
//**************************************************************
datablock EffectProfile(ScoutEngineEffect)
{
   effectname = "vehicles/outrider_engine";
   minDistance = 5.0;
   maxDistance = 10.0;
};

datablock EffectProfile(ScoutThrustEffect)
{
   effectname = "vehicles/outrider_boost";
   minDistance = 5.0;
   maxDistance = 10.0;
};

datablock AudioProfile(ScoutSqueelSound)
{
   filename    = "fx/vehicles/outrider_skid.wav";
   description = ClosestLooping3d;
   preload = true;
};

// Scout
datablock AudioProfile(ScoutEngineSound)
{
   filename    = "fx/vehicles/outrider_engine.wav";
   description = AudioDefaultLooping3d;
   preload = true;
   effect = ScoutEngineEffect;
};

datablock AudioProfile(ScoutThrustSound)
{
   filename    = "fx/vehicles/outrider_boost.wav";
   description = AudioDefaultLooping3d;
   preload = true;
   effect = ScoutThrustEffect;
};

//**************************************************************
// LIGHTS
//**************************************************************
datablock RunningLightData(WildcatLight1)
{
   radius = 1.0;
   color = "1.0 1.0 1.0 0.3";
   nodeName = "Headlight_node01";
   direction = "-1.0 1.0 0.0";
   texture = "special/headlight4";
};

datablock RunningLightData(WildcatLight2)
{
   radius = 1.0;
   color = "1.0 1.0 1.0 0.3";
   nodeName = "Headlight_node02";
   direction = "1.0 1.0 0.0";
   texture = "special/headlight4";
};

datablock RunningLightData(WildcatLight3)
{
   type = 2;
   radius = 100.0;
   color = "1.0 1.0 1.0 1.0";
   offset = "0.0 0.0 0.0";
   direction = "0.0 1.0 0.0";
   texture = "special/projheadlight";
};
datablock HoverVehicleData(RPGBoat) : WildcatDamageProfile
{
   spawnOffset = "0 0 2";

   floatingGravMag = 1.0;

   catagory = "Vehicles";
   shapeFile = "FishingBoat.dts";
   computeCRC = true;

   //debrisShapeName = "vehicle_grav_scout_debris.dts";
   //debris = ShapeDebris;
   renderWhenDestroyed = true;
   cantAbandon = true;
   drag = 0.01;
   density = 0.9;

   mountPose[0] = scoutRoot;
   //mountPose[1] = Root;
   cameraMaxDist = 5.0;
   cameraOffset = 0.7;
   cameraLag = 0.5;
   numMountPoints = 2;
   isProtectedMountPoint[0] = true;
   isProtectedMountPoint[1] = false;
   //explosion = VehicleExplosion;
	explosionDamage = 0.5;
	explosionRadius = 5.0;

   hitpoints = 600;
   maxDamage = 0.60;
   destroyedLevel = 0.60;

   isShielded = false;
   rechargeRate = 0.7;
   energyPerDamagePoint = 75;
   maxEnergy = 150;
   minJetEnergy = 15;
   jetEnergyDrain = 1.3;

   // Rigid Body
   mass = 600;
   bodyFriction = 200.0;
   bodyRestitution = 500.5;  
   softImpactSpeed = 20;       // Play SoftImpact Sound
   hardImpactSpeed = 28;      // Play HardImpact Sound

   // Ground Impact Damage (uses DamageType::Ground)
   minImpactSpeed = 100;
   speedDamageScale = 0.010;

   // Object Impact Damage (uses DamageType::Impact)
   collDamageThresholdVel = 23;
   collDamageMultiplier   = 0.030;

   dragForce            = 25 / 45.0;
   vertFactor           = 50.0;
   floatingThrustFactor = 0.01;// bound by [0, 1]

   mainThrustForce    = 10;
   reverseThrustForce = 6;
   strafeThrustForce  = 0.5;
   turboFactor        = 1.0;

   brakingForce = 25;
   brakingActivationSpeed = 4;

   stabLenMin = 0.80;//minimum height off the ground
   stabLenMax = 0.80;//max height off the ground
   stabSpringConstant  = 30;//spring = up
   stabDampingConstant = 16;//damp = down =/

   gyroDrag = 16;
   normalForce = 30;
   restorativeForce = 50;
   steeringForce = 10;
   rollForce  = 1;//side to side / \
   pitchForce = 1;// up and down

   dustEmitter = VehicleLiftoffDustEmitter;
   triggerDustHeight = 0.0;
   dustHeight = 0.1;
   dustTrailEmitter = TireEmitter;
   dustTrailOffset = "0.0 -1.0 0.5";
   triggerTrailHeight = 3.6;
   dustTrailFreqMod = 15.0;

   //jetSound         = ScoutSqueelSound;
   //engineSound      = ScoutEngineSound;
  // floatSound       = ScoutThrustSound;
   softImpactSound  = GravSoftImpactSound;
   hardImpactSound  = HardImpactSound;
   wheelImpactSound = WheelImpactSound;

   //
   softSplashSoundVelocity = 10.0; 
   mediumSplashSoundVelocity = 20.0;   
   hardSplashSoundVelocity = 30.0;   
   exitSplashSoundVelocity = 10.0;
   
   exitingWater      = VehicleExitWaterSoftSound;
   impactWaterEasy   = VehicleImpactWaterSoftSound;
   impactWaterMedium = VehicleImpactWaterSoftSound;
   impactWaterHard   = VehicleImpactWaterMediumSound;
   waterWakeSound    = VehicleWakeSoftSplashSound; 

   minMountDist = 4;

  // damageEmitter[0] = SmallLightDamageSmoke;
  // damageEmitter[1] = SmallHeavyDamageSmoke;
  // damageEmitter[2] = DamageBubbles;
   damageEmitterOffset[0] = "0.0 -1.5 0.5 ";
   damageLevelTolerance[0] = 0.3;
   damageLevelTolerance[1] = 0.7;
   numDmgEmitterAreas = 1;

   //splashEmitter[0] = VehicleFoamDropletsEmitter;
   //splashEmitter[1] = VehicleFoamEmitter;
   
   //shieldImpact = VehicleShieldImpact;
   
   //forwardJetEmitter = WildcatJetEmitter;

   cmdCategory = Tactical;
   cmdIcon = CMDHoverScoutIcon;
   cmdMiniIconName = "commander/MiniIcons/com_landscout_grey";
   targetNameTag = 'Boat';
   targetTypeTag = '';
   sensorData = VehiclePulseSensor;

   checkRadius = 1.7785;
   observeParameters = "1 10 10";

	DamageScale[$DamageType::Landing] = 1.0;
	DamageScale[$DamageType::Ground] = 1.0;
	DamageScale[$DamageType::Piercing] = 0.5;
	DamageScale[$DamageType::Slashing] = 1.0;
	DamageScale[$DamageType::Bludgeoning] = 2.0;
	DamageScale[$DamageType::Archery] = 0.0;
	DamageScale[$DamageType::Spell] = 1.0;
   //runningLight[0] = WildcatLight1;
   //runningLight[1] = WildcatLight2;
   //runningLight[2] = WildcatLight3;

   shieldEffectScale = "0.9375 1.125 0.6";
};

function RPGBoat::onadd(%this, %obj)
{
	Parent::onadd(%this, %obj);
	%obj.mountable = true;
	%obj.hp = %this.hitpoints;
}
function VehicleData::damageObject(%data, %targetObject, %sourceObject, %position, %amount, %damageType, %momVec, %theClient, %proj)
{

   %clAttacker = %sourceObject.client;
   if(%clAttacker $= "")
   {
   	%clAttacker = %sourceObject.sourceObject.client;
   }
   %value = %amount;
   if(%damagetype == $DamageType::Spell)
   {
	 if(%clAttacker.focus == true)
	{
		%focused = true;
		%clAttacker.focus = false;
	}
	%spell = %sourceobject.spell; //workaround

	if(%spell $= "")
	%spell = %clAttacker.lastspell;
	
	%clAttacker.lastspell = "";
	%weapon = %spell;
	%value = $spelldata[%spell, DamageMod];//booom!
	%element = $spelldata[%spell, Element];
	//For the case of SPELLS, the initial damage has already been determined before calling this function
	%skilltype = $Skill::OffensiveCasting;

	%dmg = %value;
	%value = (%dmg / 100) * GetPlayerSkill(%clAttacker, %skilltype) + %dmg/10;
	if(%clAttacker.isAiControlled())
		%value /= 2;//drop damage by 2 for bots, bots dont have to worry about mana.

	%value /= 10;
   }
   %amount = round(%value) / %data.hitpoints;

   %sourceClient = %clattacker;

 
    if(%sourceObject)
    {
        %targetObject.lastDamagedBy = %sourceObject;
        %targetObject.lastDamageType = %damageType;
    }
    else 
        %targetObject.lastDamagedBy = 0;


   // Scale damage type & include shield calculations...
   if (%data.isShielded)
      %amount = %data.checkShields(%targetObject, %position, %amount, %damageType);
   
   
   %damageScale = %data.damageScale[%damageType];
   if(%damageScale !$= "")
      %amount *= %damageScale;
      
   if(%amount != 0)
      %targetObject.applyDamage(%amount);
      
   if(%targetObject.getDamageState() $= "Destroyed" )
   {
      if( %momVec !$= "")
         %targetObject.setMomentumVector(%momVec);
   }
   if(%value > 0)
   {
   	MessageClient(%sourceClient, 'DamageBoat', $MsgLtBlue @ "You hit the boat for" SPC round(%value) SPC "points of damage!");
   }
   else
   	MessageClient(%sourceClient, 'DamageBoat', $MsgLtBlue @ "You hit the boat for no damage!");
}
//=====================
//Other
//=====================

//**************************************************************
// SOUNDS
//**************************************************************
datablock EffectProfile(AssaultVehicleEngineEffect)
{
   effectname = "vehicles/tank_engine";
   minDistance = 5.0;
   maxDistance = 10.0;
};

datablock EffectProfile(AssaultVehicleThrustEffect)
{
   effectname = "vehicles/tank_boost";
   minDistance = 5.0;
   maxDistance = 10.0;
};

datablock EffectProfile(AssaultTurretActivateEffect)
{
   effectname = "vehicles/tank_activate";
   minDistance = 5.0;
   maxDistance = 10.0;
};

datablock EffectProfile(AssaultMortarDryFireEffect)
{
   effectname = "weapons/mortar_dryfire";
   minDistance = 5.0;
   maxDistance = 10.0;
};

datablock EffectProfile(AssaultMortarFireEffect)
{
   effectname = "vehicles/tank_mortar_fire";
   minDistance = 5.0;
   maxDistance = 10.0;
};

datablock EffectProfile(AssaultMortarReloadEffect)
{
   effectname = "weapons/mortar_reload";
   minDistance = 5.0;
   maxDistance = 10.0;
};

datablock EffectProfile(AssaultChaingunFireEffect)
{
   effectname = "weapons/chaingun_fire";
   minDistance = 5.0;
   maxDistance = 10.0;
};

datablock AudioProfile(AssaultVehicleSkid)
{
   filename    = "fx/vehicles/tank_skid.wav";
   description = ClosestLooping3d;
   preload = true;
};

datablock AudioProfile(AssaultVehicleEngineSound)
{
   filename    = "fx/vehicles/tank_engine.wav";
   description = AudioDefaultLooping3d;
   preload = true;
   effect = AssaultVehicleEngineEffect;
};

datablock AudioProfile(AssaultVehicleThrustSound)
{
   filename    = "fx/vehicles/tank_boost.wav";
   description = AudioDefaultLooping3d;
   preload = true;
   effect = AssaultVehicleThrustEffect;
};

datablock AudioProfile(AssaultChaingunFireSound)
{
   filename    = "fx/vehicles/tank_chaingun.wav";
   description = AudioDefaultLooping3d;
   preload = true;
   effect = AssaultChaingunFireEffect;
};

datablock AudioProfile(AssaultChaingunReloadSound)
{
   filename    = "fx/weapons/chaingun_dryfire.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(TankChaingunProjectile)
{
   filename    = "fx/weapons/chaingun_projectile.wav";
   description = ProjectileLooping3d;
   preload = true;
};

datablock AudioProfile(AssaultTurretActivateSound)
{
    filename    = "fx/vehicles/tank_activate.wav";
   description = AudioClose3d;
   preload = true;
   effect = AssaultTurretActivateEffect;
};

datablock AudioProfile(AssaultChaingunDryFireSound)
{
   filename    = "fx/weapons/chaingun_dryfire.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(AssaultChaingunIdleSound)
{
   filename    = "fx/misc/diagnostic_on.wav";
   description = ClosestLooping3d;
   preload = true;
};

datablock AudioProfile(AssaultMortarDryFireSound)
{
   filename    = "fx/weapons/mortar_dryfire.wav";
   description = AudioClose3d;
   preload = true;
   effect = AssaultMortarDryFireEffect;
};

datablock AudioProfile(AssaultMortarFireSound)
{
   filename    = "fx/vehicles/tank_mortar_fire.wav";
   description = AudioClose3d;
   preload = true;
   effect = AssaultMortarFireEffect;
};

datablock AudioProfile(AssaultMortarReloadSound)
{
   filename    = "fx/weapons/mortar_reload.wav";
   description = AudioClose3d;
   preload = true;
   effect = AssaultMortarReloadEffect;
};

datablock AudioProfile(AssaultMortarIdleSound)
{
   filename    = "fx/misc/diagnostic_on.wav";
   description = ClosestLooping3d;
   preload = true;
};

//**************************************************************
// LIGHTS
//**************************************************************
datablock RunningLightData(TankLight1)
{
   radius = 1.5;
   color = "1.0 1.0 1.0 0.2";
   nodeName = "Headlight_node01";
   direction = "0.0 1.0 0.0";
   texture = "special/headlight4";
};

datablock RunningLightData(TankLight2)
{
   radius = 1.5;
   color = "1.0 1.0 1.0 0.2";
   nodeName = "Headlight_node02";
   direction = "0.0 1.0 0.0";
   texture = "special/headlight4";
};

datablock RunningLightData(TankLight3)
{
   radius = 1.5;
   color = "1.0 1.0 1.0 0.2";
   nodeName = "Headlight_node03";
   direction = "0.0 1.0 0.0";
   texture = "special/headlight4";
};

datablock RunningLightData(TankLight4)
{
   radius = 1.5;
   color = "1.0 1.0 1.0 0.2";
   nodeName = "Headlight_node04";
   direction = "0.0 1.0 0.0";
   texture = "special/headlight4";
};

datablock ParticleEmitterData(TankDustEmitter)
{
   ejectionPeriodMS = 12;
   periodVarianceMS = 0;
   ejectionVelocity = 20.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 90;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   useEmitterColors = true;
   particles = "TankDust";
};

datablock HoverVehicleData(RPGTransport) : TankDamageProfile
{
   spawnOffset = "0 0 4";

   floatingGravMag = 3.5;

   catagory = "Vehicles";
   shapeFile = "vehicle_air_hapc.dts";
   multipassenger = true;
   computeCRC = true;
   renderWhenDestroyed = false;
                                                 

   debrisShapeName = "vehicle_air_hapc_debris.dts";
   debris = ShapeDebris;

   drag = 0.0;
   density = 0.9;

   mountPose[0] = sitting;
//   mountPose[1] = sitting;
   numMountPoints = 6;
   isProtectedMountPoint[0] = true;
   isProtectedMountPoint[1] = true;
   isProtectedMountPoint[2] = true;
   isProtectedMountPoint[3] = true;
   isProtectedMountPoint[4] = true;
   isProtectedMountPoint[5] = true;

   cameraMaxDist = 17;
   cameraOffset = 2;
   cameraLag = 8.5;
   explosion = LargeGroundVehicleExplosion;
   explosionDamage = 0.5;
   explosionRadius = 5.0;

   maxSteeringAngle = 0.5;  // 20 deg.

   hitpoints = 1600;
   maxDamage = 3.15;
   destroyedLevel = 3.15;

   isShielded = true;
   rechargeRate = 1.0;
   energyPerDamagePoint = 300;
   maxEnergy = 400;
   minJetEnergy = 15;
   jetEnergyDrain = 2.0;

   // Rigid Body
   mass = 400; //1500
   bodyFriction = 0.1; //0.8
   bodyRestitution = 0.5;
   minRollSpeed = 3;
   //gyroForce = 400; //400
   //gyroDamping = 0.3;
   stabilizerForce = 20;
   minDrag = 10;
   softImpactSpeed = 15;       // Play SoftImpact Sound
   hardImpactSpeed = 18;      // Play HardImpact Sound

   // Ground Impact Damage (uses DamageType::Ground)
   minImpactSpeed = 17;
   speedDamageScale = 0.060;

   // Object Impact Damage (uses DamageType::Impact)
   collDamageThresholdVel = 18;
   collDamageMultiplier   = 0.045;

   dragForce            = 40 / 20;
   vertFactor           = 0.0;
   floatingThrustFactor = 0.15;

   mainThrustForce    = 150;
   reverseThrustForce = 40;
   strafeThrustForce  = 40;
   turboFactor        = 1.7;

   brakingForce = 25;
   brakingActivationSpeed = 4;

   stabLenMin = 3.25;
   stabLenMax = 4;
   stabSpringConstant  = 50;
   stabDampingConstant = 20;

   gyroDrag = 16; //20
   normalForce = 30; //20
   restorativeForce = 20; //10
   steeringForce = 30; //15
   rollForce  = 15; //5
   pitchForce = 7; //3

   dustEmitter = TankDustEmitter;
   triggerDustHeight = 4;
   dustHeight = 1.0;
   dustTrailEmitter = TireEmitter;
   dustTrailOffset = "0.0 -1.0 0.5";
   triggerTrailHeight = 3.6;
   dustTrailFreqMod = 15.0;

   jetSound         = AssaultVehicleThrustSound;
   engineSound      = AssaultVehicleEngineSound;
   floatSound       = AssaultVehicleSkid;
   softImpactSound  = GravSoftImpactSound;
   hardImpactSound  = HardImpactSound;
   wheelImpactSound = WheelImpactSound;

   forwardJetEmitter = TankJetEmitter;
   
   //
   softSplashSoundVelocity = 5.0; 
   mediumSplashSoundVelocity = 10.0;   
   hardSplashSoundVelocity = 15.0;   
   exitSplashSoundVelocity = 10.0;
   
   exitingWater      = VehicleExitWaterMediumSound;
   impactWaterEasy   = VehicleImpactWaterSoftSound;
   impactWaterMedium = VehicleImpactWaterMediumSound;
   impactWaterHard   = VehicleImpactWaterMediumSound;
   waterWakeSound    = VehicleWakeMediumSplashSound; 

   minMountDist = 4;

   damageEmitter[0] = SmallLightDamageSmoke;
   damageEmitter[1] = SmallHeavyDamageSmoke;
   damageEmitter[2] = DamageBubbles;
   damageEmitterOffset[0] = "0.0 -1.5 3.5 ";
   damageLevelTolerance[0] = 0.3;
   damageLevelTolerance[1] = 0.7;
   numDmgEmitterAreas = 1;

   splashEmitter[0] = VehicleFoamDropletsEmitter;
   splashEmitter[1] = VehicleFoamEmitter;

   shieldImpact = VehicleShieldImpact;

   cmdCategory = "Tactical";
   cmdIcon = CMDGroundTankIcon;
   cmdMiniIconName = "commander/MiniIcons/com_tank_grey";
   targetNameTag = 'Hover Carriage';
   targetTypeTag = 'Assault Tank';
   sensorData = VehiclePulseSensor;
   mountable = true;
   checkRadius = 5.0;
   observeParameters = "1 10 10";
   runningLight[0] = TankLight1;
   runningLight[1] = TankLight2;
   runningLight[2] = TankLight3;
   runningLight[3] = TankLight4;
   shieldEffectScale = "0.9 1.0 0.6";
   showPilotInfo = 1;
};
function VehicleData::isMountable(%data, %obj, %val)
{
   %obj.mountable = %val;
}
function RPGTransport::onAdd(%this, %obj)
{
	Parent::onadd(%this, %obj);
	%obj.mountable = true;
	%obj.hp = %this.hitpoints;
	%obj.schedule(2000, "playThread", $ActivateThread, "activate");
}

function spawnTransport(%client)
{
	%player = %client.player;
	
	if(getLOSinfo(%client, 200))
	{
		%pos = VectorAdd($los::position,"0 0 8");
		%veh = new HoverVehicle() 
		{
			dataBlock  = RPGTransport;
			position = %pos;
     	};
		%veh.owner = %client;
		MissionCleanup.add(%veh);
	}
}
//===========
//=HoverBoat=
//===========
datablock HoverVehicleData(RPGBigBoat) : WildcatDamageProfile
{
   spawnOffset = "0 0 2";

   floatingGravMag = 1.0;

   catagory = "Vehicles";
   shapeFile = "vehicle_air_hapc.dts";
   computeCRC = true;
   
   

   debrisShapeName = "vehicle_air_hapc_debris.dts";
   debris = ShapeDebris;
   renderWhenDestroyed = false;
   cantAbandon = true;
   drag = 0.01;
   density = 0.9;

   mountPose[0] = sitting;
//   mountPose[1] = sitting;
   numMountPoints = 6;
   isProtectedMountPoint[0] = true;
   isProtectedMountPoint[1] = false;
   isProtectedMountPoint[2] = false;
   isProtectedMountPoint[3] = false;
   isProtectedMountPoint[4] = false;
   isProtectedMountPoint[5] = false;
   
   cameraMaxDist = 17;
   cameraOffset = 2;
   cameraLag = 8.5;
   explosion = LargeGroundVehicleExplosion;
   explosionDamage = 0.5;
   explosionRadius = 5.0;


   hitpoints = 600;
   maxDamage = 0.60;
   destroyedLevel = 0.60;

   isShielded = false;
   rechargeRate = 0.7;
   energyPerDamagePoint = 75;
   maxEnergy = 150;
   minJetEnergy = 15;
   jetEnergyDrain = 1.3;

   // Rigid Body
   mass = 600;
   bodyFriction = 200.0;
   bodyRestitution = 500.5;  
   softImpactSpeed = 20;       // Play SoftImpact Sound
   hardImpactSpeed = 28;      // Play HardImpact Sound

   // Ground Impact Damage (uses DamageType::Ground)
   minImpactSpeed = 100;
   speedDamageScale = 0.010;

   // Object Impact Damage (uses DamageType::Impact)
   collDamageThresholdVel = 23;
   collDamageMultiplier   = 0.030;

   dragForce            = 25 / 45.0;
   vertFactor           = 50.0;
   floatingThrustFactor = 0.01;// bound by [0, 1]

   mainThrustForce    = 10;
   reverseThrustForce = 6;
   strafeThrustForce  = 0.5;
   turboFactor        = 2.0;

   brakingForce = 25;
   brakingActivationSpeed = 4;

   stabLenMin = 0.80;//minimum height off the ground
   stabLenMax = 0.80;//max height off the ground
   stabSpringConstant  = 30;//spring = up
   stabDampingConstant = 16;//damp = down =/

   gyroDrag = 16;
   normalForce = 30;
   restorativeForce = 50;
   steeringForce = 3; //Smaller steering force means less wobble while ai is steers.
   rollForce  = 1;//side to side /\
   pitchForce = 1;// up and down

   dustEmitter = VehicleLiftoffDustEmitter;
   triggerDustHeight = 0.0;
   dustHeight = 0.1;
   dustTrailEmitter = TireEmitter;
   dustTrailOffset = "0.0 -1.0 0.5";
   triggerTrailHeight = 3.6;
   dustTrailFreqMod = 15.0;

   jetSound         = AssaultVehicleThrustSound;
   engineSound      = AssaultVehicleEngineSound;
   floatSound       = AssaultVehicleSkid;
   softImpactSound  = GravSoftImpactSound;
   hardImpactSound  = HardImpactSound;
   wheelImpactSound = WheelImpactSound;

   //
   softSplashSoundVelocity = 10.0; 
   mediumSplashSoundVelocity = 20.0;   
   hardSplashSoundVelocity = 30.0;   
   exitSplashSoundVelocity = 10.0;
   
   exitingWater      = VehicleExitWaterSoftSound;
   impactWaterEasy   = VehicleImpactWaterSoftSound;
   impactWaterMedium = VehicleImpactWaterSoftSound;
   impactWaterHard   = VehicleImpactWaterMediumSound;
   waterWakeSound    = VehicleWakeSoftSplashSound; 

   minMountDist = 4;

   damageEmitter[0] = LightDamageSmoke;
   damageEmitter[1] = HeavyDamageSmoke;
   damageEmitter[2] = DamageBubbles;
   damageEmitterOffset[0] = "3.0 -3.0 0.0 ";
   damageEmitterOffset[1] = "-3.0 -3.0 0.0 ";
   damageLevelTolerance[0] = 0.3;
   damageLevelTolerance[1] = 0.7;
   numDmgEmitterAreas = 2;

   //splashEmitter[0] = VehicleFoamDropletsEmitter;
   //splashEmitter[1] = VehicleFoamEmitter;
   
   //shieldImpact = VehicleShieldImpact;
   forwardJetEmitter = FlyerJetEmitter;  //TankJetEmitter
   //forwardJetEmitter = WildcatJetEmitter;

   cmdCategory = Tactical;
   cmdIcon = CMDHoverScoutIcon;
   cmdMiniIconName = "commander/MiniIcons/com_landscout_grey";
   targetNameTag = 'Boat';
   targetTypeTag = '';
   sensorData = VehiclePulseSensor;

   ccheckRadius = 7.8115; //1.7785
   observeParameters = "1 15 15"; //observeParameters = "1 10 10";

	DamageScale[$DamageType::Landing] = 1.0;
	DamageScale[$DamageType::Ground] = 1.0;
	DamageScale[$DamageType::Piercing] = 0.5;
	DamageScale[$DamageType::Slashing] = 1.0;
	DamageScale[$DamageType::Bludgeoning] = 2.0;
	DamageScale[$DamageType::Archery] = 0.0;
	DamageScale[$DamageType::Spell] = 1.0;
   runningLight[0] = TankLight1;
   runningLight[1] = TankLight2;
   runningLight[2] = TankLight3;
   runningLight[3] = TankLight4;

   shieldEffectScale = "0.9375 1.125 0.6";
};
function RPGBigBoat::onAdd(%this, %obj)
{
	Parent::onadd(%this, %obj);
	%obj.mountable = true;
	%obj.hp = %this.hitpoints;
	%obj.schedule(2000, "playThread", $ActivateThread, "activate");
}
function spawnBigBoat(%client)
{
	%player = %client.player;
	
	if(getLOSinfo(%client, 200))
	{
		%pos = VectorAdd($los::position,"0 0 8");
		%veh = new HoverVehicle() 
		{
			dataBlock  = RPGBigBoat;
			position = %pos;
     	};
		%veh.owner = %client;
		MissionCleanup.add(%veh);
	}
}
