

using DG.Tweening;
using MarkusSecundus.Utils.Primitives;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    [SerializeField] float duration_seconds = 1f;
    [SerializeField] float alphaLow = 0f;
    [SerializeField] float alphaHigh = 1f;
    [SerializeField] bool fadeOutOnStart = false;
    private void Start()
    {
        if (fadeOutOnStart) FadeOut();
    }
    public void FadeOut() => FadeOut(duration_seconds);
    public void FadeOut(float duration) => FadeOut(null, duration_seconds);
    public void FadeOut(System.Action onFinished, float? duration = null) => RunEffect(alphaHigh, alphaLow, duration??duration_seconds, onFinished);
    public void FadeIn() => FadeIn(duration_seconds);
    public void FadeIn(float duration) => FadeIn(null, duration);
    public void FadeIn(System.Action onFinished, float? duration=null) => RunEffect(alphaLow, alphaHigh, duration??duration_seconds, onFinished);
    void RunEffect(float alphaBegin, float alphaEnd, float duration, System.Action onFinished=null)
    {
        gameObject.SetActive(true);
        Tween last = null;
        foreach(var rend in GetComponentsInChildren<Graphic>(true))
        {
            rend.gameObject.SetActive(true);
            rend.color = rend.color.With(a: alphaBegin);
            var tween = last = rend.DOFade(alphaEnd, duration_seconds);
            if (alphaEnd <= 0f) tween.onComplete += () => rend.gameObject.SetActive(false);
        }
        if (last != null) last.onComplete += () =>
        {
            onFinished?.Invoke();
        };
        else
            onFinished?.Invoke();
    }
}
