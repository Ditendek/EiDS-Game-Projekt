using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagramSpriteExchange : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject bossGameObject;
    BossAI bossAi;
    [SerializeField] private Sprite acitvatedPenta;
    [SerializeField] private Sprite deactivatedPenta;
    SpriteRenderer spriteRenderer;
     void Start()
    {
        bossAi = bossGameObject.GetComponent<BossAI>();
         spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bossAi.GetState() == BossAI.State.Spawning)
        {
            spriteRenderer.sprite = acitvatedPenta;
        }
        else
        {
            spriteRenderer.sprite = deactivatedPenta;
        }
    }
}
