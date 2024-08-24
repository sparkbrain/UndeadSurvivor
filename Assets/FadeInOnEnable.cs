using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        Image background = GetComponent<Image>();

        background.DOFade(0, 0);
        background.DOFade(1, 2);
    }
}
