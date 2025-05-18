using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Panel1 : MonoBehaviour
{
    [SerializeField] private Button scramButton;

    void Start()
    {
        scramButton.onClick.AddListener(ScramButtonClick);
    }

    void Update()
    {

    }

    private void ScramButtonClick() {
        
    }
}
