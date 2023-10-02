
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MyToggle : MonoBehaviour
{
    public Image onImage;
    public Image notOnImage;
    [HideInInspector]
    public Button button;
    private bool isOn;
    public  bool IsOn
    {
        get
        {
            return isOn;
        }
        set
        {
            isOn = value;
            SetImage(value);
        }
    }



    private void Awake()
    {
        button = transform.AddComponent<Button>();
        button.onClick .AddListener(() =>
        {
            IsOn = !IsOn;
        });
    }
    private void SetImage(bool value)
    {
        onImage.gameObject.SetActive(value);
        notOnImage.gameObject.SetActive(!value);
    }
}
