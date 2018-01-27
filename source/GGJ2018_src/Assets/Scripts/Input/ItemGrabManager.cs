using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrabManager : MonoBehaviour
{
	private GameObject heldItem = null;

	public GameObject HeldItem
	{
		get { return heldItem; }
	}

	#region Unity Messages
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if( heldItem != null )
		{
			//Vector2 mousePos = Input.mousePosition;
			//Vector3 newPosition;
		}
	}
	#endregion Unity Messages

	#region Public Interface
	public void GrabItem( GameObject item )
	{

	}
	#endregion Public Interface

	#region Helpers
	private void ReleaseItem()
	{

	}
	#endregion Helpers
}
