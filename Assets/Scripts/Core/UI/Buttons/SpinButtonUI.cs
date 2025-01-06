using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpinButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Tween _animation;

    public void OnDisable()
    {
        OnPointerExit(new PointerEventData(EventSystem.current));
    }

    public void OnDestroy()
    {
        _animation?.Kill();
    }

    public void OnPointerEnter(PointerEventData data)
    {
        _animation = gameObject.transform.DORotate(new Vector3(0, 0, 270), .2f);
    }

    public void OnPointerExit(PointerEventData data)
    {
        _animation = gameObject.transform.DORotate(new Vector3(0, 0, 0), .2f);
    }
}
