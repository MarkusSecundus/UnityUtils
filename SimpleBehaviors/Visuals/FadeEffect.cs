

using DG.Tweening;
using MarkusSecundus.Utils.Assets._Scripts.Utils.SimpleBehaviors.Visuals;
using MarkusSecundus.Utils.Datastructs;
using MarkusSecundus.Utils.Primitives;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MarkusSecundus.Utils.Behaviors.Cosmetics
{
    public class FadeEffect : MonoBehaviour, IStoppable
    {
        [SerializeField] string _comment;
        [SerializeField] float duration_seconds = 1f;
        [SerializeField] float alphaLow = 0f;
        [SerializeField] float alphaHigh = 1f;
        [SerializeField] ActionOnStart _onStart;

        [SerializeField] Ease ease = Ease.Unset;

        [SerializeField] UnityEvent onComplete;


        [System.Serializable]
        public enum ActionOnStart
        {
            DoNothing = 0, FadeIn, FadeOut
        }


        private void Start()
        {
            if (_onStart == ActionOnStart.FadeIn) FadeIn(onComplete.Invoke);
            else if (_onStart == ActionOnStart.FadeOut) FadeOut(onComplete.Invoke);
        }
        public void FadeOut() => FadeOut(duration_seconds);
        public void FadeOut(float duration) => FadeOut(null, duration_seconds);
        public void FadeOut(System.Action onFinished, float? duration = null, bool includeDefaultAction = true) => _runEffect(alphaHigh, alphaLow, duration ?? duration_seconds, ease, onFinished, includeDefaultAction);
        public void FadeIn() => FadeIn(duration_seconds);
        public void FadeIn(float duration) => FadeIn(null, duration);
        public void FadeIn(System.Action onFinished, float? duration = null, bool includeDefaultAction = true) => _runEffect(alphaLow, alphaHigh, duration ?? duration_seconds, ease, onFinished, includeDefaultAction);

        void _runEffect(float alphaBegin, float alphaEnd, float duration, Ease ease, System.Action onFinished = null, bool includeDefaultAction = true)
        {
            _tweens.Clear();

            gameObject.SetActive(true);
            Tween last = null;
            foreach (var rend in GetComponentsInChildren<Graphic>(true))
            {
                rend.gameObject.SetActive(true);
                rend.color = rend.color.With(a: alphaBegin);
                var tween = last = rend.DOFade(alphaEnd, duration_seconds).SetEase(ease);
                _tweens.Add(tween);
                if (alphaEnd <= 0f) tween.onComplete += () => rend.gameObject.SetActive(false);
            }
            if (last != null) last.onComplete += () =>
            {
                _tweens.Clear();
                this.onComplete.Invoke();
                onFinished?.Invoke();
            };
            else
            {
                _tweens.Clear();
                this.onComplete.Invoke();
                onFinished?.Invoke();
            }
        }
        List<Tween> _tweens = new();
        public bool IsRunning => (! _tweens.IsNullOrEmpty()) && _tweens.Any(t => t.IsPlaying());
        public void Stop()
        {
            foreach (var t in _tweens) if (t.IsPlaying()) t.Kill();
            _tweens.Clear();
        }
    }
}