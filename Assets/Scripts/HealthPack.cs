using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealthPack : MonoBehaviour
{
    public Sprite[] healthPackType;

    [Range(1, 100)] public int percentChanceBlue, percentChanceRed;
    [Range(0.01f, 1f)] public float greenRecoveryPercentage, blueRecoveryPercentage, redRecoveryPercentage;
    private float _recoveryPercentage;


    // Start is called before the first frame update
    void Start()
    {
        RandomizeHealthPackType();
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.GetComponent<Player>())
        {
            print("Health pack collided with player");
            var health = trigger.gameObject.GetComponent<Health>();
            health.IncreaseHealth(30);
            Destroy(gameObject);
        }
    }

    void RandomizeHealthPackType()
    {
        int randomNumber = UnityEngine.Random.Range(1, 101);

        if (randomNumber <= percentChanceRed)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = healthPackType[1];
            _recoveryPercentage = redRecoveryPercentage;
        }

        else if (randomNumber <= percentChanceBlue)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = healthPackType[0];
            _recoveryPercentage = blueRecoveryPercentage;
        }

        else
        {
            _recoveryPercentage = greenRecoveryPercentage;
        }
    }
}