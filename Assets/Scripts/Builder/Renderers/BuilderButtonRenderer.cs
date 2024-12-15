using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class BuilderButtonRenderer : MonoBehaviour
{
    public event Action OnOpenBuilderPanel;

    [SerializeField]
    private Button _builderButtonUI;

    public void Start()
    {
        _builderButtonUI.onClick.AddListener(() => OnOpenBuilderPanel.Invoke());
        SubscribeToButtonEvents();
    }

    // (TODO) Think how shared code
    private void SubscribeToButtonEvents()
    {
        // Add EventTrigger component
        EventTrigger trigger = _builderButtonUI.gameObject.AddComponent<EventTrigger>();

        // Create pointer enter event
        EventTrigger.Entry pointerEnterEvent = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        pointerEnterEvent.callback.AddListener((_) => HandlePointerEnter());

        // Create pointer exit event
        EventTrigger.Entry pointerExitEvent = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit
        };
        pointerExitEvent.callback.AddListener((_) => HandlePointerExit());

        // Add events to EventTrigger
        trigger.triggers.Add(pointerEnterEvent);
        trigger.triggers.Add(pointerExitEvent);
    }

    private void HandlePointerEnter()
    {
        _builderButtonUI.transform.DOScale(1.2f, 0.3f);
    }

    private void HandlePointerExit()
    {
        _builderButtonUI.transform.DOScale(1f, 0.3f);
    }
}
