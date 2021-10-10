using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float timeToExplode;

    private TextMesh timerText;
    private Collider2D explosionCollider;
    private SpriteRenderer bombRenderer;
    private ParticleSystem bombParticle;
    private float explodeTimer;
    private bool hasExploded = false;

    private void OnEnable()
    {
        explodeTimer = timeToExplode;
        explosionCollider.enabled = false;
        hasExploded = false;
        bombRenderer.enabled = true;
        timerText.GetComponent<MeshRenderer>().enabled = true;
    }

    private void Awake()
    {
        timerText = GetComponentInChildren<TextMesh>();
        explosionCollider = GetComponent<Collider2D>();
        bombRenderer = GetComponentInChildren<SpriteRenderer>();
        bombParticle = GetComponentInChildren<ParticleSystem>();
        explosionCollider.enabled = false;

        explodeTimer = timeToExplode;
    }

    private void Update()
    {
        if(explodeTimer > 0f)
        {
            explodeTimer -= Time.deltaTime;
            timerText.text = Mathf.RoundToInt(explodeTimer).ToString();
        }
        else
        {
            if (hasExploded) return;

            hasExploded = true;
            StartCoroutine(DelayColliderAppearance());
        }
    }

    private IEnumerator DelayColliderAppearance()
    {
        //disable visual & enable collider
        timerText.GetComponent<MeshRenderer>().enabled = false;
        bombRenderer.enabled = false;
        explosionCollider.enabled = true;

        bombParticle.Emit(30);

        //wait for 0.5 sec and disable collider and gameobject
        yield return new WaitForSeconds(0.5f);
        explosionCollider.enabled = false;
        gameObject.SetActive(false);
    }
}
