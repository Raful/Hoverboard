using UnityEngine;
using System.Collections;

public class RailCounter {
	private static int tal;
	private static bool rail;
	private static bool allowRail;
	private static bool tunnelBool;
	public static float tunnelAngle;

	public static int getNum()
	{
		return tal;
	}

	public static void decNum()
	{
		tal--;
	}
	public static void incNum()
	{
		tal++;
	}
	public static void nullNum()
	{
		tal = 0;
	}
	public static void railFalse()
	{
		rail = false;
	}
	public static void railTrue()
	{
		rail = true;
	}
	public static void railSwap()
	{
		rail = !rail;
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
}
