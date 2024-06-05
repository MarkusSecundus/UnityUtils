

using DG.Tweening;
using MarkusSecundus.Utils.Primitives;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    [SerializeField] string _comment;
    [SerializeField] float duration_seconds = 1f;
    [SerializeField] float alphaLow = 0f;
    [SerializeField] float alphaHigh = 1f;
    [SerializeField] ActionOnStart _onStart;

    [SerializeField] Ease ease = Ease.Unset;

    [SerializeField] UnityEvent onComplete;

    [System.Serializable]public enum ActionOnStart
    {
        DoNothing = 0, FadeIn, FadeOut
    }


    private void Start()
    {
        if (_onStart == ActionOnStart.FadeIn)   FadeIn(onComplete.Invoke);
        else if (_onStart == ActionOnStart.FadeOut) FadeOut(onComplete.Invoke);
    }
    public void FadeOut() => FadeOut(duration_seconds);
    public void FadeOut(float duration) => FadeOut(null, duration_seconds);
    public void FadeOut(System.Action onFinished, float? duration = null, bool includeDefaultAction = true) => _runEffect(alphaHigh, alphaLow, duration??duration_seconds,ease, onFinished, includeDefaultAction);
    public void FadeIn() => FadeIn(duration_seconds);
    public void FadeIn(float duration) => FadeIn(null, duration);
    public void FadeIn(System.Action onFinished, float? duration=null, bool includeDefaultAction=true) => _runEffect(alphaLow, alphaHigh, duration??duration_seconds,ease, onFinished, includeDefaultAction);

    void _runEffect(float alphaBegin, float alphaEnd, float duration, Ease ease, System.Action onFinished=null, bool includeDefaultAction=true)
    {
        gameObject.SetActive(true);
        Tween last = null;
        foreach(var rend in GetComponentsInChildren<Graphic>(true))
        {
            rend.gameObject.SetActive(true);
            rend.color = rend.color.With(a: alphaBegin);
            var tween = last = rend.DOFade(alphaEnd, duration_seconds).SetEase(ease);
            if (alphaEnd <= 0f) tween.onComplete += () => rend.gameObject.SetActive(false);
        }
        if (last != null) last.onComplete += () =>
        {
            this.onComplete.Invoke();
            onFinished?.Invoke();
        };
        else
        {
            this.onComplete.Invoke();
            onFinished?.Invoke();
        }
    }
}
