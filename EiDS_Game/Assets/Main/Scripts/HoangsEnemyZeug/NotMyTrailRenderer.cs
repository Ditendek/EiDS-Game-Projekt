using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NotMyTrailRenderer : MonoBehaviour
{
    public int ClonesPerSecond = 10;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Transform tf;
    private List<SpriteRenderer> clones;
    public Vector3 scalePerSecond = new Vector3(1f, 1f, 1f);
    public Color colorPerSecond = new Color(255, 255, 255, 1f);
    private Coroutine trailCoroutine;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        tf = GetComponentInParent<Transform>();
        sr = transform.Find("Trail").GetComponent<SpriteRenderer>();
        clones = new List<SpriteRenderer>();
       // EnableTrail();
    }

    public void EnableTrail()
    {
        if(trailCoroutine!=null)
        StopCoroutine(trailCoroutine);

        trailCoroutine = StartCoroutine(trail());

     //   Debug.Log("Starting CoRoutine");

    }

    void Update()
    {
        for (int i = 0; i < clones.Count; i++)
        {
            clones[i].color -= colorPerSecond * Time.deltaTime;
            clones[i].transform.localScale -= scalePerSecond * Time.deltaTime;
            if (clones[i].color.a <= 0f || clones[i].transform.localScale == Vector3.zero)
            {
                Destroy(clones[i].gameObject);
                clones.RemoveAt(i);
                i--;
            }
        }
    }
    public List<SpriteRenderer> GetCloneList()
    {
        return clones;
    }

    IEnumerator trail()
    {      //  Debug.Log("1.Schritt");

        for (; ; ) //while(true)
        {
           // Debug.Log("whiletrue: " + clones.Count);
          //  if (rb.velocity != Vector2.zero)
            {
                 var clone = new GameObject("trailClone");
                clone.transform.position = tf.position;
                clone.transform.localScale = tf.localScale;
                var cloneRend = clone.AddComponent<SpriteRenderer>();
                cloneRend.sprite = sr.sprite;
                cloneRend.sortingOrder = sr.sortingOrder - 1;
                clones.Add(cloneRend);
             }
            yield return new WaitForSeconds(1f / ClonesPerSecond);
        }
    }
}