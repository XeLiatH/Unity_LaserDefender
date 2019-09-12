using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = 0.2f;

    Material myMaterial;
    Vector2 offset;

    void Start()
    {
        myMaterial = gameObject.GetComponent<Renderer>().material;
        offset = new Vector2(0f, backgroundScrollSpeed);
    }

    void Update()
    {
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
