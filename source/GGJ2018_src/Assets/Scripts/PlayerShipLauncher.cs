using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipLauncher : MonoBehaviour
{
	public void ResetLauncher()
	{
		enabled = true;
	}

	void Update()
	{
		if( Input.GetMouseButtonDown( 0 ) )
		{
			Debug.Log( "I'm trying I swear!" );
			Vector3 origin = new Vector3( Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane );
			Ray ray = Camera.main.ScreenPointToRay( origin );

			RaycastHit2D hitInfo = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
			if( hitInfo )
			{
				Debug.Log( "I hit something! " + hitInfo.collider.name );
				PlayerShip player = hitInfo.transform.gameObject.GetComponent<PlayerShip>();
				if( player != null )
				{
					Debug.Log( "Extreme woot!" );
					player.LaunchShip();
					enabled = false;
				}
			}
		}
	}
}
