using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : MonoBehaviour
{
    public int damage = 20;
    
    public float speed = 3;
    public float explosionRadius = 4;

    //For Screen Shake
    private float shakeAmt = 2.5f;
    private float shakeTime = .5f;

    public Sprite blinkSprite;
    public Sprite offSprite;

    private SpriteRenderer spriteRenderer;
    public GameObject explosionEffect;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        spriteRenderer.sprite = offSprite;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.sprite = blinkSprite;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Blocker")
        {
            Destroy(gameObject);
        }
        if (hitInfo.gameObject.tag == "Enemy")
        {
            AudioManager.Instance.PlaySFX("Explosion");
            Instantiate(explosionEffect, gameObject.transform.position, Quaternion.identity);
            CinemachineShake.Instance.ShakeCamera(shakeAmt, shakeTime);
            Collider2D[] zombies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, 1 << LayerMask.NameToLayer("EnemyLayer"));
            foreach (Collider2D en in zombies)
            {
                ZombieAi zombieAi = en.GetComponent<ZombieAi>();
                if (zombieAi != null && zombieAi.tag == "Enemy")
                {
                    zombieAi.TakeDamage(damage, ZombieAi.DamageType.EXPLOSIVE);

                }
            }
            Destroy(gameObject);
        }
    }
}
