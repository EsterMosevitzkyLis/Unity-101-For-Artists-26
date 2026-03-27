using UnityEngine;
using UnityEngine.Events;
using System.Collections; // Required for Coroutines

[RequireComponent(typeof(Collider))]
public class Toggle3D : MonoBehaviour
{
    public ToggleGroup3D group;
    [SerializeField] private bool m_IsOn;

    [Header("Transitions")]
    public Animator targetAnimator;
    public string normalTrigger = "Normal";
    public string highlightedTrigger = "Highlighted";
    public string pressedTrigger = "Pressed";
    public string selectedTrigger = "Selected";
    public string disabledTrigger = "Disabled";

    [Header("Animation Timing")]
    [Tooltip("How long the 'Pressed' animation lasts before the toggle actually flips.")]
    public float pressedDelay = 0.3f;

    [System.Serializable] public class ToggleEvent : UnityEvent<bool> { }
    public ToggleEvent onValueChanged;

    public bool isOn
    {
        get => m_IsOn;
        set => Set(value);
    }

    private void OnEnable() { if (group != null) group.RegisterToggle(this); }
    private void OnDisable() { if (group != null) group.UnregisterToggle(this); }

    private void Start()
    {
        if (targetAnimator == null) targetAnimator = GetComponent<Animator>();
        RefreshVisuals();
    }

    public void SetIsOnWithoutNotify(bool value)
    {
        m_IsOn = value;
        RefreshVisuals();
    }

    private void Set(bool value)
    {
        if (m_IsOn == value) return;
        m_IsOn = value;

        if (group != null && enabled && gameObject.activeInHierarchy)
        {
            if (m_IsOn || (!group.AnyTogglesOn() && !group.allowSwitchOff))
            {
                m_IsOn = true;
                group.NotifyToggleOn(this);
            }
        }

        onValueChanged.Invoke(m_IsOn);
        RefreshVisuals();
    }

    private void OnMouseEnter()
    {
        if (!enabled || targetAnimator == null) return;
        targetAnimator.SetTrigger(highlightedTrigger);
    }

    private void OnMouseExit() => RefreshVisuals();

    private void OnMouseDown()
    {
        if (!enabled || targetAnimator == null) return;

        // 1. Play the animation immediately
        targetAnimator.SetTrigger(pressedTrigger);

        // 2. Start the delay before changing the logic/events
        StartCoroutine(DelayedToggle());
    }

    private IEnumerator DelayedToggle()
    {
        // Wait for the animation to play
        yield return new WaitForSeconds(pressedDelay);

        // 3. NOW execute the logic and invoke the Events (window_camera, lamp_flip, etc.)
        if (m_IsOn && group != null && !group.allowSwitchOff)
        {
            group.SwitchToNext(this);
        }
        else
        {
            isOn = !isOn;
        }
    }

    private void RefreshVisuals()
    {
        if (targetAnimator != null)
        {
            targetAnimator.SetTrigger(m_IsOn ? selectedTrigger : normalTrigger);
        }
    }
}