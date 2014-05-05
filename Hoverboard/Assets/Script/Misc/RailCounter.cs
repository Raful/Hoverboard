using UnityEngine;
using System.Collections;

public class RailCounter {
	private static int tal;

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
}
