using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Light))]
public class LightFeedback : MonoBehaviour
{
    private UnityAction endVotingListener;

    private Light light;

    [Range(0, 1f)]
    public float opinionThreshold = 0.5f;

    Coroutine lightAnimation;


    [Header("Light Animation Settings")]
    public float flickerFrequency = 20f;
    public float flickerDuration = 1f;
    public Vector2 flickerRange = new Vector2(0.5f, 4f);

    public AnimationCurve blendCurve;
    public float blendAmplitudeMultiplier = 3f;
    public float blendDuration = 3f;


    void Awake()
    {
        light = GetComponent<Light>();
        endVotingListener = new UnityAction(UpdateLight);
    }

    void OnEnable()
    {
        EventManager.StartListening("EndVoting", endVotingListener);

    }

    void OnDisable()
    {
        EventManager.StopListening("EndVoting", endVotingListener);
    }

    void UpdateLight()
    {
        float currentOpinion = Globals.Instance.GetLatestOpinion();

        if (currentOpinion < opinionThreshold)
        {
            // disagreement
            OnDisagree();

        } else
        {
            // agreement
            OnAgree();
        }
    }

#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.Button]
#endif
    void OnAgree()
    {
        if (lightAnimation != null)
        {
            Debug.LogWarning("Animation still running...");
            return;
        }
        lightAnimation = StartCoroutine(BlendCoroutine());
    }

#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.Button]
#endif
    void OnDisagree()
    {
        if (lightAnimation != null)
        {
            Debug.LogWarning("Animation still running...");
            return;
        }
        lightAnimation = StartCoroutine(FlickerCoroutine());
    }

#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.Button]
#endif
    void StopLightAnimation()
    {
        if (lightAnimation == null) return;
        StopCoroutine(lightAnimation);
    }

    IEnumerator BlendCoroutine()
    {
        float baseIntensity = light.intensity;
        for (float t = 0; t < blendDuration; t += Time.deltaTime)
        {
            light.intensity = blendCurve.Evaluate(t / blendDuration) * blendAmplitudeMultiplier;
            yield return null;
        }
        light.intensity = baseIntensity;
        lightAnimation = null;
    }

    IEnumerator FlickerCoroutine()
    {
        float baseIntensity = light.intensity;
        for (float t = 0; t < flickerDuration; t += 1f/flickerFrequency)
        {
            light.intensity = Random.Range(flickerRange.x, flickerRange.y);
            yield return null;
        }
        light.intensity = baseIntensity;
        lightAnimation = null;
    }
}
