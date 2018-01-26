using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShip : MonoBehaviour
{
    [Range(0f, 100f)]
    public float fuel = 100f;
    public float maxFuel = 100f;
    public float forwardSpeed = 3f;

    public float fuelConsumptionRate = 5f;

    Rigidbody2D body;

    public UnityEvent onFuelEmpty;

    public bool canGo = false;

    public GameObject explosionPrefab;
    public GameObject deadSpacemanPrefab;

    public GameObject model;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.simulated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canGo)
        {
            UpdateFuel();
        }
    }

    [ContextMenu("Reset Ship")]
    public void ResetShip()
    {
        // TODO: Place me on the launchpad

        model.SetActive(true);
        ResetFuel();
        body.simulated = false;
    }

    [ContextMenu("LAUNCH")]
    public void LaunchShip()
    {
        canGo = true;
        body.simulated = true;
        // todo: play some launch sfx
    }

    public void ResetFuel()
    {
        fuel = maxFuel;
    }

    void UpdateFuel()
    {
        float newFuel = fuel - fuelConsumptionRate * Time.deltaTime;
        if (newFuel < 0f)
        {
            newFuel = 0f;
        }

        if (newFuel <= 0f && fuel > 0f)
        {
            OnFuelEmpty();
        }

        fuel = newFuel;
    }

    void OnFuelEmpty()
    {
        onFuelEmpty.Invoke();
    }

    public void FixedUpdate()
    {
        if(!canGo)
        {
            // make sure our gravity doesn't move us
            //body.AddForce(Physics.gravity * -1f * body.gravityScale);
            return;
        }
        if (fuel > 0f)
        {
            body.AddForce(transform.up * forwardSpeed * Time.deltaTime, ForceMode2D.Force);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (canGo)
        {
            WinPortal winPortal = collision.otherCollider.GetComponent<WinPortal>();
            if(winPortal)
            {
                // YOU WIN!!!!
                Debug.Log("You win!!!!");


                gameObject.SetActive(false);
                return;
            }
            // TODO: YOU LOOSE!!

            // create our explosion and our dead space man
            if (explosionPrefab)
            {
                GameObject goExplosion = GameObject.Instantiate<GameObject>(explosionPrefab, transform.position, Quaternion.identity, null);
            }

            if (deadSpacemanPrefab)
            {
                GameObject goSpaceman = GameObject.Instantiate<GameObject>(deadSpacemanPrefab, transform.position, Quaternion.identity, null);
                // add a tiiiny amount of force to him, so he'll float
                Rigidbody2D spacemanBody = goSpaceman.GetComponent<Rigidbody2D>();
                if(spacemanBody)
                {
                    float forcePower = Random.Range(.1f, 1f);
                    spacemanBody.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)), ForceMode2D.Impulse);
                }
            }

            // hide our model
            model.SetActive(false);
            canGo = false;
            body.simulated = false;
        }
    }
}
