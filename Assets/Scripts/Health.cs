using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Make HealthPack appear in random locations when enemy dies
//TODO: Make Colorful gradient for health bar

public class Health : MonoBehaviour
{
    [Range(50, 300)] public int maxHealth = 100;
    [Range(0, 100)] public float chanceToDropHealthPack = 50f;

    [SerializeField] bool isPlayer;
    [SerializeField] int health = 100;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;

    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    public GameObject Hp;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            audioPlayer.PlayDamageClip();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    public int GetHealth()
    {
        return health;
    }
//TODO: Balance health
    public void IncreaseHealth(float percentToRecover)
    {
        health += (int)(100 * percentToRecover);
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    //TODO: Add HealthPack when enemy dies
    void Die()
    {
        //print("It's a : " + gameObject.name);
        if (!isPlayer)
        {
            RollForHealthPack();
            scoreKeeper.ModifyScore(score);
            print("Enemy died");
        }
        else
        {
            levelManager.LoadGameOver();
        }

        Destroy(gameObject);
    }

    void RollForHealthPack()
    {
        int roll = Random.Range(0, 100);
        if (roll <= chanceToDropHealthPack)
        {
            print("Dropped health pack");

            GameObject newHP = Instantiate(Hp, transform.position, Quaternion.identity) as GameObject;
            print("newHP: " + newHP);

        }
    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }
}
//     void OnDestroy()
//     {
//         if (!isPlayer)
//         {
//             print("Enemy destructor running");
//             RollForHealthPack();
//         }
//     }
// }


