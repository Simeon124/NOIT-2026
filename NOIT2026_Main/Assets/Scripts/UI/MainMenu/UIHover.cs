using UnityEngine;
using UnityEngine.EventSystems;

public class UIHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float sizeMultiplier;
    [SerializeField] private AudioSource audioSource;
    private Vector3 originalSize;
    
    void Start()
    {
        originalSize = this.transform.localScale;
    }
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        transform.localScale = originalSize * sizeMultiplier;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalSize;
    }
}
