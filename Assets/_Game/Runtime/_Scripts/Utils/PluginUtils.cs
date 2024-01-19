using DG.Tweening;
using UnityEngine;

public static class TweenUtils
{
    public static Tween SetEaseOvershootOrAmp(this Tween tween, float value)
    {
        tween.easeOvershootOrAmplitude = value;
        return tween;
    }

    public static Tween SetFullPosition(this Tween tween, float value)
    {
        tween.fullPosition = value;
        return tween;
    }
}