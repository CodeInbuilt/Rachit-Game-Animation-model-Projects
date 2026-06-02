using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower States")]
    public float range = 8f;
    public int damage = 25;
    public float fireRate = 1f;
    public int cost = 50;

    [Header("Targeting Mode")]
    public bool first = true;
    public bool last = false;
    public bool strong = false;
    public bool closest = true;

    [Header("Effects")]
    [SerializeField] private GameObject shootEffect;

    [Header("Audio")]
    [SerializeField] private AudioClip shootSound;
    private AudioSource audioSource;

    [System.NonSerialized]
    public GameObject target;
    private float cooldown = 0f;

    void Start()
    {
        // Auto-grab or auto-create AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.volume = 1f;
    }

    void Update()
    {
        if (target)
        {
            if (cooldown >= fireRate)
            {
                transform.right = target.transform.position - transform.position;

                target.GetComponent<Enemy>().TakeDamage(damage);
                cooldown = 0f;

                StartCoroutine(ShootEffect());
            }
            else
            {
                cooldown += 1 * Time.deltaTime;
            }
        }
    }

    IEnumerator ShootEffect()
    {
        shootEffect.SetActive(true);

        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
            Debug.Log("Playing shoot sound: " + shootSound.name);
        }
        else
        {
            Debug.LogWarning("shootSound is NULL — assign an AudioClip in Inspector!");
        }

        yield return new WaitForSeconds(0.5f);
        shootEffect.SetActive(false);
    }
}