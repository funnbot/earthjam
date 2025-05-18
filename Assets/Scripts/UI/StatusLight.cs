using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StatusLight : MonoBehaviour
{
    private Image image;
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;

    public class StatusLightEvent : UnityEvent<bool> { }
    public StatusLightEvent onStatusLight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
