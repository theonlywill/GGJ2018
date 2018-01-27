using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrabManager : MonoBehaviour
{
	private GameObject heldItem = null;
	private Rigidbody2D heldBody = null;

	public GameObject HeldItem
	{
		get { return heldItem; }
	}

	#region Unity Messages
	// Update is called once per frame
	void Update()
	{
		if( heldItem != null )
		{
			if( Input.GetMouseButton( 0 ) )
			{
				UpdateHeldItemPosition();
			}
			else
			{
				ReleaseItem();
			}
		}
	}
	#endregion Unity Messages

	#region Public Interface
	public void GrabItem( GameObject item )
	{
		heldItem = item;
		heldBody = heldItem.GetComponent<Rigidbody2D>();
		heldBody.isKinematic = true;
		UpdateHeldItemPosition();
	}
	#endregion Public Interface

	#region Helpers
	private void ReleaseItem()
	{
		heldBody.isKinematic = false;
		heldBody = null;
		heldItem = null;
	}

	private void UpdateHeldItemPosition()
	{
		Vector3 newPos = WorldPositionFromMousePosition();
		heldItem.transform.position = new Vector3( newPos.x, newPos.y, heldItem.transform.position.z );
	}

	private Vector3 WorldPositionFromMousePosition()
	{
		Vector3 mouse = Input.mousePosition;
		mouse.z = heldItem.transform.position.z;
		Vector3 newPos = Camera.main.ScreenToWorldPoint( mouse );

		return newPos;
	}
	#endregion Helpers
}
