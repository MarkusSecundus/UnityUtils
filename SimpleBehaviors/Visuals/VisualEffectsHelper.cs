using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MarkusSecundus.Utils;
using MarkusSecundus.Utils.Datastructs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace MarkusSecundus.Utils.Behaviors.Cosmetics
{
    /// <summary>
    /// Component responsible for performing various visual effects that can be started via a <see cref="UnityEvent"/> callback
    /// </summary>
    public class VisualEffectsHelper : MonoBehaviour
    {
        /// <summary>
        /// Renderers to be affected by the effects
        /// </summary>
        [SerializeField] Renderer[] AffectedRenderers;
        /// <summary>
        /// Parameters for the blinking effect
        /// </summary>
        [SerializeField] BlinkingArgs Blinking = BlinkingArgs.Default;
        /// <summary>
        /// Parameters for the blinking effect
        /// </summary>
        [System.Serializable]
        public struct BlinkingArgs
        {
            /// <summary>
            /// How many seconds the effect takes from start to finish
            /// </summary>
            public float TotalDuration;
            /// <summary>
            /// How many seconds from peak to fade. Must be lesser than <see cref="TotalDuration"/>
            /// </summary>
            public float FadeTime;
            /// <summary>
            /// Color with which the objects blink
            /// </summary>
            public Color Color;

            public static BlinkingArgs Default => new BlinkingArgs { TotalDuration = 0.0f, FadeTime = 0.0f, Color = Color.magenta };
        }

        private class InternalBlinkingInfo
        {
            public TweenerCore<Color, Color, ColorOptions> CurrentlyPlaying;
            public Color OriginalColor;
        }
        private DefaultValDict<Renderer, InternalBlinkingInfo> blinkingRendererMetadata = new DefaultValDict<Renderer, InternalBlinkingInfo>(r => new InternalBlinkingInfo());

        /// <summary>
        /// Start the blinking event. Parameters are obtained from <see cref="Blinking"/>
        /// </summary>
        public void Blink()
        {
            foreach (var renderer in AffectedRenderers)
            {
                var data = blinkingRendererMetadata[renderer];
                var mat = renderer.material;
                if (data.CurrentlyPlaying == null)
                {
                    data.OriginalColor = mat.color;
                }
                data.CurrentlyPlaying = mat.DOColor(Blinking.Color, Blinking.TotalDuration - Blinking.FadeTime)
                    .OnComplete(() =>
                {
                    data.CurrentlyPlaying = mat.DOColor(data.OriginalColor, Blinking.FadeTime)
                            .OnComplete(() => data.CurrentlyPlaying = null);
                });
            }
        }

        [System.Serializable]
        public struct BlinkingOnOffArgs
        {
            public GameObject[] AffectedObjects;

            public int BlinksCount;
            public float OnDuration_seconds;
            public float OffDuration_seconds;
            public float BuildupTime_seconds;
            public UnityEvent OnBlinkingFinished;
        }


        [SerializeField] BlinkingOnOffArgs BlinkingOnOff;

        private Dictionary<GameObject, Coroutine> blinkingOnOffRendererMetadata = new ();

        public void BlinkOnOff()
        {
            foreach(var obj in BlinkingOnOff.AffectedObjects)
            {
                if(blinkingOnOffRendererMetadata.TryGetValue(obj, out var coroutine))
                {
                    this.StopCoroutine(coroutine);
                }
                Coroutine blinker = StartCoroutine(impl(obj));
            }

            IEnumerator impl(GameObject obj)
            {
                bool isActive = obj.activeSelf;
                if (BlinkingOnOff.BuildupTime_seconds > 0f) 
                    yield return new WaitForSeconds(BlinkingOnOff.BuildupTime_seconds);
                for(int t=BlinkingOnOff.BlinksCount*2 - 1; t --> 0;)
                {
                    obj.SetActive(isActive = !isActive);
                    yield return new WaitForSeconds(isActive ? BlinkingOnOff.OnDuration_seconds : BlinkingOnOff.OffDuration_seconds);
                }
                obj.SetActive(isActive = !isActive);

                blinkingOnOffRendererMetadata.Remove(obj);
                if (blinkingOnOffRendererMetadata.Count <= 0)
                    BlinkingOnOff.OnBlinkingFinished.Invoke();
            }
        }

        public void EmitParticles(int count)
        {
            var particles = GetComponent<ParticleSystem>();
            particles.Emit(count);
        }
    }
}