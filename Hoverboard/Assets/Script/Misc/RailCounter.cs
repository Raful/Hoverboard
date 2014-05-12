using UnityEngine;
using System.Collections;

public class RailCounter {
	private static int tal;
	private static bool rail;

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
	public static bool getRailbool()
	{
		return rail;
	}

}
