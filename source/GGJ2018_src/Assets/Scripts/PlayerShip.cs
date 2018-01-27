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

    List<DelayField> delayFieldsImIn = new List<DelayField>();

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

    public void AddFuel(float amountToAdd)
    {
        float newAmount = fuel + amountToAdd;
        if (newAmount > maxFuel)
        {
            newAmount = maxFuel;
        }

        fuel = newAmount;
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
        if (!canGo)
        {
            // make sure our gravity doesn't move us
            //body.AddForce(Physics.gravity * -1f * body.gravityScale);
            return;
        }
        if (fuel > 0f)
        {
            float maxSpeed = CalcMaxSpeed();
            if (body.velocity.magnitude < maxSpeed)
            {
                body.AddForce(transform.up * forwardSpeed * Time.deltaTime, ForceMode2D.Force);
            }
        }

        ApplyDelayFieldBrakes();
    }

    void ApplyDelayFieldBrakes()
    {
        // while in delay fields we'll hit the brakes if we are going too fast
        float maxSpeed = CalcMaxSpeed();
        if (body.velocity.magnitude > maxSpeed)
        {
            // HIT THE BRAKES!!!!
            body.AddForce(body.velocity * -1f * DelayField.BRAKE_POWER);
        }
    }

    float CalcMaxSpeed()
    {
        float max = float.PositiveInfinity;
        for (int i = 0; i < delayFieldsImIn.Count; i++)
        {
            if (delayFieldsImIn[i])
            {
                if (max > delayFieldsImIn[i].maxSpeed)
                {
                    max = delayFieldsImIn[i].maxSpeed;
                }
            }
        }
        return max;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        DelayField delayField = collision.GetComponent<DelayField>();
        if (delayField)
        {
            delayFieldsImIn.Add(delayField);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        DelayField delayField = other.GetComponent<DelayField>();
        if (delayField)
        {
            delayFieldsImIn.Remove(delayField);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (canGo)
        {
            WinPortal winPortal = collision.collider.GetComponent<WinPortal>();
            if (winPortal)
            {
                // YOU WIN!!!!
                Debug.Log("You win!!!!");

                canGo = false;

                gameObject.SetActive(false);

                return;
            }

            // fuel pickups are handled in fuelpickup.cs.ontriggerenter (cause they have trigger volumes)

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
                if (spacemanBody)
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
