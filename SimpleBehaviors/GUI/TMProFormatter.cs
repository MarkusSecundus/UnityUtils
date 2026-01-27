using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MarkusSecundus.Utils.Behaviors.GUI
{
    public class TMProFormatter : MonoBehaviour
    {
        [SerializeField] string Format;

        TMP_Text _text;
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void SetTextWithIntArgument(int arg) => _text.text = string.Format(Format, arg);
        public void SetTextWithStringArgument(string arg) => _text.text = string.Format(Format, arg);

    }
}