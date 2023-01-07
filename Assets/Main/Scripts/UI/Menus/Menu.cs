using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    public bool IsVisible { get; private set; }

    public void SetMenuVisibility(bool isVisible)
    {
        IsVisible = isVisible;
        gameObject.SetActive(IsVisible);
    }
}
