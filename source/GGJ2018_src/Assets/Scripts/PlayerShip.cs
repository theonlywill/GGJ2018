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

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFuel();
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
        if (fuel > 0f)
        {
            body.AddForce(transform.up * forwardSpeed * Time.deltaTime, ForceMode2D.Force);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO: YOU LOOSE!!


    }
}
