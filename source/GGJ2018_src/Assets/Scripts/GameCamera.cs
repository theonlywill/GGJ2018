using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public float bottomMostPos = 39.4f;
    public Vector3 offsetFromPlayer = Vector3.zero;
    public float maxVelocityOffset = 10f;
    public float velocityOffsetMultiplier = 10f;

    public float followLerpStrength = 0.25f;

    CameraShake shake;

	// Use this for initialization
	void Start ()
    {
        shake = GetComponent<CameraShake>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 newPos = transform.position;

		// follow player ship
        if(GameManager.playerShip && GameManager.playerShip.canGo)
        {
            Vector3 playerPosPlusVelocity = GameManager.playerShip.transform.position;
            if(GameManager.playerShip.body && GameManager.playerShip.body.velocity.magnitude > 0f)
            {
                Vector3 velocityMod = GameManager.playerShip.body.velocity * velocityOffsetMultiplier;
                velocityMod = Vector3.ClampMagnitude(velocityMod, maxVelocityOffset);
                playerPosPlusVelocity += velocityMod;
            }
            newPos = Vector3.Lerp(newPos, playerPosPlusVelocity + offsetFromPlayer, followLerpStrength * Time.deltaTime);
        }

        // enforce bottom limit
        if(newPos.y < bottomMostPos)
        {
            newPos.y = bottomMostPos;
        }
        // enforce sides limits
        newPos.x = 0f;

        transform.position = newPos;
	}

    [ContextMenu("Test Shake")]
    public void TestShake()
    {
        Shake(1f, 2f);
    }

    public void Shake(float amount, float duration)
    {
        if(shake)
        {
            shake.shake = duration;
            shake.shakeAmount = amount;
        }
    }
}
