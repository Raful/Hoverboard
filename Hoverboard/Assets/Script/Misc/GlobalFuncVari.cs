﻿using UnityEngine;
using System.Collections;

/*
 * This script is used to access global variabels through functions.
 * The script was first created to keep track on how many grind the player was on,
 * this was done so the player could do a continus grind. Later it got changed to a 
 * script/class for keeping track of multiple things, either by changing there value or
 * fetching the current value.
 * 
 * Most of the script has been made co-op by Erik and Niklas.
 * 
 * Created by: Erik Åsén
 * Edited by: Niklas, Creator
 *
 */

public class GlobalFuncVari {
	private static int numberOfRailOn;		// The name explains it
	private static bool rail, allowRail;	// Grind bools
	private static bool tunnelBool;			
	public static float tunnelAngle;
	private static bool camFollow;
	private static float jumpPower;

//  ------ Grind specific variabels

	public static int getNum()
	{
		return numberOfRailOn;
	}

	public static void decNum()
	{
		numberOfRailOn--;
	}
	public static void incNum()
	{
		numberOfRailOn++;
	}

	public static void railFalse()
	{
		rail = false;
	}
	public static void railTrue()
	{
		rail = true;
	}
	public static bool getRailbool()
	{
		return rail;
	}

	public static void allowRailFalse()
	{
		allowRail = false;
	}

	public static void allowRailTrue()
	{
		allowRail = true;
	}
	public static bool getallowRail()
	{
		return allowRail;
	}

//  ------ End of grind variabels modifications

//  ------ Tunnel trigger variabels
	public static bool getTunnelBool()
	{
		return tunnelBool;
	}

	public static void tunnelBoolFalse()
	{
		tunnelBool = false;
	}
	public static void tunnelBoolTrue()
	{
		tunnelBool = true;
	}
//  ------ End of tunnel trigger variabels modifications

//  ------ Camera variabels
	public static void stopCam()
	{
		camFollow = false;
	}

	public static void followCam()
	{
		camFollow = true;
	}

	public static bool getCamfollowBool()
	{
		return camFollow;
	}
//  ------ End of camera variabels modifications

//  ------ Wallride variabels
	public static void setJumpPower(float power)
	{
		jumpPower = power;
	}

	public static float getJumpPower()
	{
		return jumpPower;
	}
//  ------ End of wallride varaibels modifications
}
