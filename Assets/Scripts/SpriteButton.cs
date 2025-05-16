using UnityEngine;

public class SpriteButton : MonoBehaviour
{
    private Collider2D collider2D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        Debug.Log("Start");
    }

    void OnMouseOver()
    {
        Debug.Log("Mouse entered the button area");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
