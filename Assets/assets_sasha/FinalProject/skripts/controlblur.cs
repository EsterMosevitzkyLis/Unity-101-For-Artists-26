using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class controlblur : MonoBehaviour
{
    public Volume globalVolume;
    private DepthOfField dof;

    private bool toggleDof;
    public float focusDistance;

    private void ToggleBackgroundUI()
    {
        toggleDof = !toggleDof;
        if(globalVolume.profile.TryGet(out dof))
        {
            dof.active = toggleDof;
            dof.focusDistance.value = focusDistance;
        }

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ToggleBackgroundUI();
        }
    }
}
