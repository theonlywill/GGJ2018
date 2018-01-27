using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Singleton Access
	private static GameManager activeInstance = null;
	public static GameManager Instance
	{
		get { return activeInstance; }
	}
	#endregion Singleton Access

	private ItemGrabManager itemGrabManager = null;
	public ItemGrabManager ItemGrabManager
	{
		get { return itemGrabManager; }
	}

    public static PlayerShip playerShip;

	private void Awake()
	{
		if(activeInstance != null)
		{
			Destroy( gameObject );
			return;
		}

		activeInstance = this;
		DontDestroyOnLoad( gameObject );

		itemGrabManager = gameObject.AddComponent<ItemGrabManager>();
	}
}
