using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PseudoSpriteAnimation : MonoBehaviour
{
    public Sprite[] blastSprites;
    public float playSpeed = 3;
    public bool loop;

    SpriteRenderer overlay;

    void Start()
    {
        if (!overlay) overlay = GetComponent<SpriteRenderer>();
        if (!overlay) overlay = gameObject.AddComponent<SpriteRenderer>();
        StartCoroutine(PlaySpriteAnim());
    }

    IEnumerator PlaySpriteAnim()
    {
        var spritesLength = blastSprites.Length;
        var index = 0;

        while (index < spritesLength)
        {
            overlay.sprite = blastSprites[index++];
            yield return new WaitForSeconds(Time.deltaTime * playSpeed);
            if (loop) index %= spritesLength;
        }
        Destroy(gameObject);
    }
}