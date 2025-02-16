using System;
using UnityEngine;

namespace MarkusSecundus.Utils.Debugging
{
    public static class GizmoHelpers
    {
        public ref struct GizmoColorScope
        {
            readonly Color _oldColor;
            public GizmoColorScope(Color newColor)
            {
                _oldColor = Gizmos.color;
                Gizmos.color = newColor;
            }
            public void Dispose()
            {
                Gizmos.color = _oldColor;
            }
        }

        public static GizmoColorScope ColorScope(Color newColor) => new GizmoColorScope(newColor);
    }
}
