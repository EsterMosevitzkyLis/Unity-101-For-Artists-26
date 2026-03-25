using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace toolsforlinks
{
    [RequireComponent(typeof(TMP_Text))]
    public class LinkHandlerForTMPtext : MonoBehaviour, IPointerClickHandler
    {
        private TMP_Text _tmpText;
        private Camera _camera;

        public delegate void ClickOnLinkEvent(string keyword);
        public static event ClickOnLinkEvent OnClickedOnLinkEvent;

        private void Awake()
        {
            _tmpText = GetComponent<TMP_Text>();

            // Always fallback to main camera (works in PBR / world space)
            _camera = Camera.main;
        }

        private void Update()
        {
            // 🔹 World-space / PBR click support
            if (Input.GetMouseButtonDown(0))
            {
                HandleClick(Input.mousePosition);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // 🔹 UI click support
            HandleClick(eventData.position);
        }

        private void HandleClick(Vector3 position)
        {
            if (_tmpText == null) return;

            int linkIndex = TMP_TextUtilities.FindIntersectingLink(
                _tmpText,
                position,
                _camera
            );

            if (linkIndex == -1) return;

            TMP_LinkInfo linkInfo = _tmpText.textInfo.linkInfo[linkIndex];

            string linkID = linkInfo.GetLinkID();
            string linkText = linkInfo.GetLinkText();

            Debug.Log($"Clicked link: {linkID}");

            // Open URL
            if (System.Uri.IsWellFormedUriString(linkID, System.UriKind.Absolute))
            {
                Application.OpenURL(linkID);
            }
            else
            {
                OnClickedOnLinkEvent?.Invoke(linkText);
            }
        }
    }
}