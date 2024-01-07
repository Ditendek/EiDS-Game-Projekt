using System;
using System.Collections;

using UnityEngine;


public class AimWithTree : AttackMode
{
    private Vector3 playerPosition;
    public Transform enemyGFX;

    private bool isScaling = false;
 //   private bool doneScaling = false;
    private float scalingTimeCounter = 0;
 
    // Update is called once per fram

      public override void Attack()
    {
        playerPosition = PlayerMovement.PlayerPosition;
        StartCoroutine(ScaleOverTime(enemyGFX, new Vector3((enemyGFX.localScale.x / enemyGFX.localScale.x) * 1.7f, 1.3f, 1f), 0.2f, true));


        OnStab?.Invoke(this, new OnStabEventArgs
        {
            stabTargetPosition = playerPosition + new Vector3(0,-0.125f, 0)
        }) ;
    }
    IEnumerator ScaleOverTime(Transform objectToScale, Vector3 toScale, float duration, bool upScale)
    {
        if (isScaling)
        {
            yield break;
        }

        isScaling = true;

        scalingTimeCounter = 0;
        Vector3 startScaleSize = objectToScale.localScale;
        while (scalingTimeCounter < duration)
        {
      
            scalingTimeCounter += Time.deltaTime;
            if (playerPosition.x < transform.position.x && toScale.x > 0 ||
                playerPosition.x > transform.position.x && toScale.x < 0 )
            {
                 toScale.x=-toScale.x;
            }


            objectToScale.localScale = Vector3.Lerp(startScaleSize, toScale, scalingTimeCounter / duration);
          /*  if (scalingTimeCounter / duration >= 1)
            { 
                    doneScaling = true;
            }*/
            yield return null;
        }
        isScaling = false;
        if (upScale)
            {
                StartCoroutine(ScaleOverTime(enemyGFX, new Vector3((enemyGFX.localScale.x / enemyGFX.localScale.x) * 1f,1f, 1f), 0.1f, false));
            }
    }
    public event EventHandler<OnStabEventArgs> OnStab;
    public class OnStabEventArgs : EventArgs
    {
        public Vector3 stabTargetPosition;
    };
}
