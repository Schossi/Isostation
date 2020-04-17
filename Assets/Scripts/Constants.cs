using UnityEngine;
using System.Collections;

public static class Constants
{
	public static int PlayerLayer;
	public static int StationLayer;
	public static int InteractionLayer;

	static Constants()
	{
		PlayerLayer = LayerMask.NameToLayer("Player");
		StationLayer = LayerMask.NameToLayer("Station");
		InteractionLayer = LayerMask.NameToLayer("InteractionZone");
	}
}
