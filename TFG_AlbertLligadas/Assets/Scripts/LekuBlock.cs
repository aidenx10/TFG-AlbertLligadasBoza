using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class LekuBlock : MonoBehaviour
{
    public GameObject item;
    public GameObject spawnPoint;
    public int maxHits = 1;
    public Sprite emptyBlock;

    private bool animating;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!animating && maxHits != 0 && other.tag == "Player")
        {
            Hit();
        }
    }

    private void Hit()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        maxHits--;

        if (maxHits == 0)
        {
            sr.sprite = emptyBlock;
        }

        if (item != null)
        {
            Instantiate(item, spawnPoint.transform.position, Quaternion.identity);
        }

        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        animating = true;

        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 0.5f;

        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);
        animating = false;

    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.125f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = to;
    }
}
