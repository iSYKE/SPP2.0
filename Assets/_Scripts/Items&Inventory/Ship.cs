using UnityEngine;
using System.Collections;

[System.Serializable]
public class Ship {

	public int shipID;

	public string shipName;
	public string shipDesc;
	public string shipPrefabName;

	public float shipValue;

	public float shipCargoMass;
	public float shipCargoVolume;

	public float shipMass;
	public float shipDensity;

	public float shipSpawnYoffset;
	public float shipSailCoefficient;
	public float shipTurnCoefficient;
	public float shipSailIntoWindModifier; //How Much Into the Wind can you sail... ~ between 0 and  0.5

	public float shipMaxHull;
	public float shipMaxSail;
	public int	 shipMaxCrew;

	public int	shipMaxGunsLeft;
	public int  shipMaxGunsRight;


	public Ship( int id, string sName, string sDesc, string sPrefab, float sValue, float sCmass, float sCvol, float sMass, float sDens,
	             float sSco, float sSailCo, float sTurnCo, float sSIWmod, float sHull, float sSail, int sCrew, int maxGL, int maxGR ){

		shipID 			= id;
		shipName 		= sName;
		shipDesc 		= sDesc;
		shipPrefabName  = sPrefab;

		shipValue 		= sValue;
		shipCargoMass 	= sCmass;
		shipCargoVolume = sCvol;

		shipMass 					= sMass;
		shipDensity 				= sDens;

		shipSpawnYoffset 			= sSco;
		shipSailCoefficient 		= sSailCo;
		shipTurnCoefficient			= sTurnCo;
		shipSailIntoWindModifier 	= sSIWmod;

		shipMaxHull = sHull;
		shipMaxSail = sSail;
		shipMaxCrew = sCrew;


		shipMaxGunsLeft  = maxGL;
		shipMaxGunsRight = maxGR;




	}





}
