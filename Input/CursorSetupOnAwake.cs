using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarkusSecundus.Utils.Input
{
    public class CursorSetupOnAwake : MonoBehaviour
    {
        [SerializeField] bool CursorIsVisible = true;
        [SerializeField] CursorLockMode LockMode = CursorLockMode.None;
        [SerializeField] Texture2D CursorTexture;
        [SerializeField] Vector2 CursorTextureHotspot = Vector2.zero;
        [SerializeField] CursorMode CursorTextureMode;
        private void Awake() => DoSetup();

        public void DoSetup()
        {
            UnityEngine.Cursor.visible = CursorIsVisible;
            UnityEngine.Cursor.lockState = LockMode;
            if (CursorTexture)
                UnityEngine.Cursor.SetCursor(CursorTexture, CursorTextureHotspot, CursorTextureMode);
        }
    }
}
