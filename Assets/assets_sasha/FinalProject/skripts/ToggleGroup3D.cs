using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("3D/Toggle Group 3D")]
public class ToggleGroup3D : MonoBehaviour
{
    public bool allowSwitchOff = false;
    public List<Toggle3D> m_Toggles = new List<Toggle3D>();

    public void RegisterToggle(Toggle3D toggle)
    {
        if (!m_Toggles.Contains(toggle)) m_Toggles.Add(toggle);
    }

    public void UnregisterToggle(Toggle3D toggle)
    {
        if (m_Toggles.Contains(toggle)) m_Toggles.Remove(toggle);
    }

    public void NotifyToggleOn(Toggle3D activeToggle)
    {
        foreach (var t in m_Toggles)
        {
            if (t == activeToggle) continue;
            t.SetIsOnWithoutNotify(false);
        }
    }

    public void SwitchToNext(Toggle3D current)
    {
        foreach (var t in m_Toggles)
        {
            if (t == current) continue;
            t.isOn = true;
            break;
        }
    }

    public bool AnyTogglesOn() => m_Toggles.Exists(t => t.isOn);
}