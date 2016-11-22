using UnityEngine;
using System.Collections;

public class ButterflyController : MonoBehaviour
{

    public Transform target;

    public bool randomizeColors;
    public Gradient rainbowGradient;

    public Color outlineColor;
    public Color butterflyColor;

    public Renderer outlineRenderer;
    public Renderer butterflyRenderer;
    public Animation butterflyAnimation;

    public LayerMask playerLayer;
    public LayerMask butterflyLayer;

    private Vector3 offset;
    private Vector3 anchor;

    void Start()
    {
        if (randomizeColors)
        {
            outlineColor = rainbowGradient.Evaluate(Random.value);
            butterflyColor = rainbowGradient.Evaluate(Random.value);

            float scale = Random.Range(0.4f, 1.1f);
            transform.localScale = new Vector3(scale, scale, scale);

            butterflyAnimation["Take 001"].speed = Random.Range(0.9f, 1.1f);
            butterflyAnimation["Take 001"].time = Random.value;
        }

        anchor = transform.position;

        outlineRenderer.material.SetColor("_TintColor", outlineColor);
        butterflyRenderer.material.SetColor("_TintColor", butterflyColor);
        StartCoroutine(ChangePosition());
    }

    void FixedUpdate()
    {
        Vector3 position = transform.position;
        Vector3 targetPosition = (target ? target.position : anchor) + offset;

        targetPosition.y = Mathf.Max(targetPosition.y, 1);

        position = Vector3.Lerp(position, targetPosition, 0.02f);
        transform.position = position;

        transform.forward = Vector3.Slerp(transform.forward, targetPosition - position, 0.02f);
    }

    private IEnumerator ChangePosition()
    {
        while (true)
        {
            offset = Vector3.Scale(Random.onUnitSphere, new Vector3(1, 0.2f, 1)) * Random.Range(0.1f, 4);
            yield return new WaitForSeconds(Random.Range(0.5f, 1));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (GameState.IsPlaying)
        {
            if (gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                SphereCollider collider = GetComponent<SphereCollider>();
                float radius = collider.radius;
                radius = radius - 0.5f;
                if (radius < 0)
                {
                    collider.enabled = false;
                }
                else
                {
                    collider.radius = radius;
                }
                return;
            }

            gameObject.layer = LayerMask.NameToLayer("Player");

            SfxManager.PlayButterflySfx();

            target = other.gameObject.transform;

            GameState.FlockSize++;
        }
    }
}
