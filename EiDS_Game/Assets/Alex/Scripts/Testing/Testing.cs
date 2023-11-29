using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public int x = 10;
    public int y = 10;

    private Texture2D texture;
    // Start is called before the first frame update
    void Start()
    {
        texture = new(x, y, TextureFormat.ARGB32, false);
        texture.name = "Borders";
        texture.filterMode = FilterMode.Point;

        for(int i = 0; i < x * y; ++i) texture.SetPixel((int) i%x, (int) i/y, new(0, 0, 0, 0));

        Color[] c = new Color[10];
        for(int i = 0; i < c.Length; ++i) c[i] = Color.white;

        /*texture.SetPixels(0, 0, 10, 1, c);
        texture.SetPixels(0, 9, 10, 1, c);*/
        //texture.SetPixel(5, 5, c[0]);

        for(int i = 0; i < 10; ++i) texture.SetPixel(Random.Range(0, x), Random.Range(0, y), c[0]);

        texture.Apply();

        GetComponent<SpriteRenderer>().sprite = Sprite.Create(texture, new Rect(0, 0, x, y), Vector2.zero, 1);

        /*if(GetComponent<PolygonCollider2D>() != null)
        {
            Destroy(GetComponent<PolygonCollider2D>());
        }

        PolygonCollider2D p = this.gameObject.AddComponent<PolygonCollider2D>();
        p.usedByComposite = true;
        p.useDelaunayMesh = true;*/

        GetComponent<CompositeCollider2D>().GenerateGeometry();
    }
}
