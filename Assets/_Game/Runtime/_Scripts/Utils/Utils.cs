using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public static class ArrayUtils
{
    public static T[] GetFilled<T>(int length, T value)
    {
        T[] array = new T[length];
        for (int i = 0; i < length; i++)
            array[i] = value;
        return array;
    }

    public static T[] GetFilled<T>(int length) where T : new()
    {
        T[] array = new T[length];
        for (int i = 0; i < length; i++)
            array[i] = new T();
        return array;
    }

    public static T GetRandom<T>(this T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    public static void Shuffle<T>(this T[] array)
    {
        List<T> list = new List<T>(array);
        int i = 0;
        while (list.Count > 0)
        {
            T element = list[Random.Range(0, list.Count)];
            array[i] = element;
            list.Remove(element);
            i++;
        }
    }
}

public static class AudioUtils
{
    public static void SetTimeToRandom(this AudioSource audioSource)
    {
        audioSource.time = Random.value * audioSource.clip.length;
    }
}

public static class BoolUtils
{
    public static float ToFloat(this bool boolVar)
    {
        return boolVar ? 1f : 0f;
    }

    public static int ToInt(this bool boolVar)
    {
        return boolVar ? 1 : 0;
    }

    public static bool GetRandom(float trueChance = .5f)
    {
        return Random.value < trueChance;
    }
}

public static class BoundsUtils
{
    public static Vector2 GetRandomPosition(this Bounds bounds)
    {
        return new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
    }
}

public static class CameraUtils
{
    public static RaycastHit2D[] GetRaycastHits2D(this Camera camera, Vector2 screenPosition, float rayDistance = 10f)
    {
        return Physics2D.GetRayIntersectionAll(camera.ScreenPointToRay(screenPosition), rayDistance);
    }
}

public static class ColliderUtils
{
    public static Vector2 GetRandomPosition(this CircleCollider2D collider)
    {
        if (collider.transform.lossyScale.y > collider.transform.lossyScale.x)
        {
            return (Vector2)collider.transform.position + Random.insideUnitCircle * collider.transform.lossyScale.y *
                collider.radius;
        }
        return (Vector2)collider.transform.position + Random.insideUnitCircle * collider.transform.lossyScale.x *
            collider.radius;
    }
}

public static class ComponentUtils
{
    public static bool HasLayer<T>(this IEnumerable<T> components, int layer) where T : Component
    {
        return components.Any(component => component.gameObject.layer == layer);
    }
}

public static class CursorUtils
{
    public static Vector2 centerPosition => new Vector2(16f, 16f);
}

public static class EnumUtils
{
    public static int Count<T>()
    {
        Array array = Enum.GetValues(typeof(T));
        return array.Length;
    }

    public static T GetRandom<T>()
    {
        Array array = Enum.GetValues(typeof(T));
        T value = (T)array.GetValue(Random.Range(0, array.Length));
        return value;
    }

    public static T[] ToArray<T>()
    {
        Array array = Enum.GetValues(typeof(T));
        return array.OfType<T>().Select(value => value).ToArray();
    }

    public static List<T> ToList<T>()
    {
        Array array = Enum.GetValues(typeof(T));
        return array.OfType<T>().Select(value => value).ToList();
    }
}

public static class EventUtils
{
    public static void AddListener(this EventTrigger eventTrigger, EventTriggerType type, UnityAction action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener((data) => action());
        eventTrigger.triggers.Add(entry);
    }

    public static void RemoveListener(this EventTrigger eventTrigger, EventTriggerType type, UnityAction action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.RemoveListener((data) => action());
        eventTrigger.triggers.Remove(entry);
    }

    public static void RemoveAllListeners(this EventTrigger eventTrigger)
    {
        eventTrigger.triggers.Clear();
    }

    public static bool HasEventObject(Vector2 screenPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = screenPosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        return results.Count > 0;
    }
}

public static class FloatUtils
{
    public static float Clamp(this float num, Vector2 range)
    {
        if (range.y < range.x)
            return Mathf.Clamp(num, range.y, range.x);
        return Mathf.Clamp(num, range.x, range.y);
    }

    public static float GetSign(this float num)
    {
        if (num > 0f)
            return 1f;
        else if (num < 0f)
            return -1f;
        return 0f;
    }

    public static float GetRandomSign(float positiveChance = .5f)
    {
        if (Random.value < positiveChance)
            return 1f;
        return -1f;
    }
}

public static class ListUtils
{
    public static bool AddIfNotAlready<T>(this List<T> list, T newItem)
    {
        if (!list.Contains(newItem))
        {
            list.Add(newItem);
            return true;
        }
        return false;
    }

    public static List<int> GetRange(int endExclusive)
    {
        List<int> list = new List<int>();
        if (endExclusive > 0)
        {
            for (int i = 0; i < endExclusive; i++)
                list.Add(i);
        }
        else
        {
            for (int i = 0; i > endExclusive; i--)
                list.Add(i);
        }
        return list;
    }

    public static T GetRandom<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static void Shuffle<T>(this List<T> list)
    {
        List<T> tempList = new List<T>(list);
        while (tempList.Count > 0)
        {
            T element = tempList[Random.Range(0, tempList.Count)];
            list.Add(element);
            tempList.Remove(element);
        }
    }
}

public static class QuaternionUtils
{
    public static Quaternion X(float x)
    {
        return Quaternion.Euler(x, 0f, 0f);
    }

    public static Quaternion Y(float y)
    {
        return Quaternion.Euler(0f, y, 0f);
    }

    public static Quaternion Z(float z)
    {
        return Quaternion.Euler(0f, 0f, z);
    }

    public static Quaternion SetX(this Quaternion quaternion, float x)
    {
        return Quaternion.Euler(x, quaternion.y, quaternion.z);
    }

    public static Quaternion SetY(this Quaternion quaternion, float y)
    {
        return Quaternion.Euler(quaternion.x, y, quaternion.z);
    }

    public static Quaternion SetZ(this Quaternion quaternion, float z)
    {
        return Quaternion.Euler(quaternion.x, quaternion.y, z);
    }

    public static Quaternion ChangeXBy(this Quaternion quaternion, float xChange)
    {
        return Quaternion.Euler(quaternion.eulerAngles.x + xChange, quaternion.eulerAngles.y,
            quaternion.eulerAngles.z);
    }

    public static Quaternion ChangeYBy(this Quaternion quaternion, float yChange)
    {
        return Quaternion.Euler(quaternion.eulerAngles.x, quaternion.eulerAngles.y + yChange,
            quaternion.eulerAngles.z);
    }

    public static Quaternion ChangeZBy(this Quaternion quaternion, float zChange)
    {
        return Quaternion.Euler(quaternion.eulerAngles.x, quaternion.eulerAngles.y,
            quaternion.eulerAngles.z + zChange);
    }
}

public static class RaycastUtils
{
    public static T GetComponent<T>(this RaycastHit2D[] hits) where T : Component
    {
        foreach (RaycastHit2D hit in hits)
        {
            T component = hit.collider.GetComponent<T>();
            if (component != null)
                return component;
        }
        return null;
    }
}

public static class RectTransformUtils
{
    public static void SetAnchorPosX(this RectTransform rectTrf, float x)
    {
        rectTrf.anchoredPosition = new Vector2(x, rectTrf.anchoredPosition.y);
    }

    public static void SetAnchorPosY(this RectTransform rectTrf, float y)
    {
        rectTrf.anchoredPosition = new Vector2(rectTrf.anchoredPosition.x, y);
    }

    public static void ChangeAnchorPosXBy(this RectTransform rectTrf, float xChange)
    {
        rectTrf.anchoredPosition = new Vector2(rectTrf.anchoredPosition.x + xChange, rectTrf.anchoredPosition.y);
    }

    public static void ChangeAnchorPosYBy(this RectTransform rectTrf, float yChange)
    {
        rectTrf.anchoredPosition = new Vector2(rectTrf.anchoredPosition.x, rectTrf.anchoredPosition.y + yChange);
    }

    public static void SetSizeDeltaX(this RectTransform rectTrf, float x)
    {
        rectTrf.sizeDelta = new Vector2(x, rectTrf.sizeDelta.y);
    }

    public static void SetSizeDeltaY(this RectTransform rectTrf, float y)
    {
        rectTrf.sizeDelta = new Vector2(rectTrf.sizeDelta.x, y);
    }

    public static void ChangeSizeDeltaXBy(this RectTransform rectTrf, float xChange)
    {
        rectTrf.sizeDelta = new Vector2(rectTrf.sizeDelta.x + xChange, rectTrf.sizeDelta.y);
    }

    public static void ChangeSizeDeltaYBy(this RectTransform rectTrf, float yChange)
    {
        rectTrf.sizeDelta = new Vector2(rectTrf.sizeDelta.x, rectTrf.sizeDelta.y + yChange);
    }
}

public static class RenderUtils
{
    public static void SetMaterialColor(this Renderer renderer, Color color)
    {
        renderer.material.color = color;
    }

    public static void SetColor(this LineRenderer line, Color color)
    {
        line.startColor = color;
        line.endColor = color;
    }

    public static void SetAlpha(this SpriteRenderer sprite, float alpha)
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
    }

    public static void SetColorRGBOnly(this SpriteRenderer sprite, Color color)
    {
        sprite.color = new Color(color.r, color.g, color.b, sprite.color.a);
    }

    public static void SetColorRGBOnly(this TextMeshPro text, Color color)
    {
        text.color = new Color(color.r, color.g, color.b, text.color.a);
    }

    public static Color SetAlpha(this Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }
}

public static class TransformUtils
{
    public static List<Transform> GetChildren(this Transform parent)
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in parent)
            children.Add(child);
        return children;
    }

    public static void SetPositionX(this Transform transform, float x)
    {
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    public static void SetPositionY(this Transform transform, float y)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    public static void SetPositionZ(this Transform transform, float z)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }

    public static void ChangePositionXBy(this Transform transform, float xChange)
    {
        transform.position += new Vector3(xChange, 0f, 0f);
    }

    public static void ChangePositionYBy(this Transform transform, float yChange)
    {
        transform.position += new Vector3(0f, yChange, 0f);
    }

    public static void ChangePositionZBy(this Transform transform, float zChange)
    {
        transform.position += new Vector3(0f, 0f, zChange);
    }

    public static void SetLocalPosX(this Transform transform, float x)
    {
        transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
    }

    public static void SetLocalPosY(this Transform transform, float y)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
    }

    public static void SetLocalPosZ(this Transform transform, float z)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
    }

    public static void ChangeLocalPosXBy(this Transform transform, float xChange)
    {
        transform.localPosition += new Vector3(xChange, 0f, 0f);
    }

    public static void ChangeLocalPosYBy(this Transform transform, float yChange)
    {
        transform.localPosition += new Vector3(0f, yChange, 0f);
    }

    public static void ChangeLocalPosZBy(this Transform transform, float zChange)
    {
        transform.localPosition += new Vector3(0f, 0f, zChange);
    }

    public static void SetLocalScale(this Transform transform, float scale)
    {
        transform.localScale = new Vector3(scale, scale, scale);
    }

    public static void MultiplyLocalScaleBy(this Transform transform, float multiplier)
    {
        transform.localScale *= multiplier;
    }

    public static void SetLocalScaleX(this Transform transform, float x)
    {
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    public static void SetLocalScaleY(this Transform transform, float y)
    {
        transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
    }

    public static void SetLocalScaleZ(this Transform transform, float z)
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, z);
    }

    public static void ChangeLocalScaleXBy(this Transform transform, float xChange)
    {
        transform.localScale += new Vector3(xChange, 0f, 0f);
    }

    public static void ChangeLocalScaleYBy(this Transform transform, float yChange)
    {
        transform.localScale += new Vector3(0f, yChange, 0f);
    }

    public static void ChangeLocalScaleZBy(this Transform transform, float zChange)
    {
        transform.localScale += new Vector3(0f, 0f, zChange);
    }

    public static void SetEulerAnglesX(this Transform transform, float x)
    {
        transform.eulerAngles = new Vector3(x, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    public static void SetEulerAnglesY(this Transform transform, float y)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, y, transform.eulerAngles.z);
    }

    public static void SetEulerAnglesZ(this Transform transform, float z)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, z);
    }

    public static void ChangeEulerAnglesXBy(this Transform transform, float xChange)
    {
        transform.eulerAngles += new Vector3(xChange, 0f, 0f);
    }

    public static void ChangeEulerAnglesYBy(this Transform transform, float yChange)
    {
        transform.eulerAngles += new Vector3(0f, yChange, 0f);
    }

    public static void ChangeEulerAnglesZBy(this Transform transform, float zChange)
    {
        transform.eulerAngles += new Vector3(0f, 0f, zChange);
    }

    public static void SetLocalEulerAnglesX(this Transform transform, float x)
    {
        transform.localEulerAngles = new Vector3(x, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    public static void SetLocalEulerAnglesY(this Transform transform, float y)
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, y, transform.localEulerAngles.z);
    }

    public static void SetLocalEulerAnglesZ(this Transform transform, float z)
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, z);
    }

    public static void ChangeLocalEulerAnglesXBy(this Transform transform, float xChange)
    {
        transform.localEulerAngles += new Vector3(xChange, 0f, 0f);
    }

    public static void ChangeLocalEulerAnglesYBy(this Transform transform, float yChange)
    {
        transform.localEulerAngles += new Vector3(0f, yChange, 0f);
    }

    public static void ChangeLocalEulerAnglesZBy(this Transform transform, float zChange)
    {
        transform.localEulerAngles += new Vector3(0f, 0f, zChange);
    }
}

public static class UIUtils
{
    public static void SetAlpha(this Image img, float alpha)
    {
        img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
    }

    public static void SetColorRGBOnly(this Image img, Color color)
    {
        img.color = new Color(color.r, color.g, color.b, img.color.a);
    }

    public static void SetColorRGBOnly(this TextMeshProUGUI text, Color color)
    {
        text.color = new Color(color.r, color.g, color.b, text.color.a);
    }
}

public static class VectorUtils
{
    public static Vector2 Clamp(this Vector2 vector, Bounds bounds)
    {
        return new Vector2(Mathf.Clamp(vector.x, bounds.min.x, bounds.max.x),
            Mathf.Clamp(vector.y, bounds.min.y, bounds.max.y));
    }

    public static Vector2 Clamp(this Vector3 vector, Bounds bounds)
    {
        return new Vector2(Mathf.Clamp(vector.x, bounds.min.x, bounds.max.x),
            Mathf.Clamp(vector.y, bounds.min.y, bounds.max.y));
    }

    public static bool IsWithin(this Vector2 position, Bounds bounds)
    {
        return bounds.min.x < position.x && position.x < bounds.max.x &&
            bounds.min.y < position.y && position.y < bounds.max.y;
    }

    public static bool IsWithin(this Vector3 position, Bounds bounds)
    {
        return bounds.min.x < position.x && position.x < bounds.max.x &&
            bounds.min.y < position.y && position.y < bounds.max.y;
    }

    public static Vector2 SetX(this Vector2 vector, float x)
    {
        return new Vector2(x, vector.y);
    }

    public static Vector2 SetY(this Vector2 vector, float y)
    {
        return new Vector2(vector.x, y);
    }

    public static Vector2 ChangeXBy(this Vector2 vector, float xChange)
    {
        return new Vector2(vector.x + xChange, vector.y);
    }

    public static Vector2 ChangeYBy(this Vector2 vector, float yChange)
    {
        return new Vector2(vector.x, vector.y + yChange);
    }

    public static Vector2 MultiplyXBy(this Vector2 vector, float multiplier)
    {
        return new Vector2(vector.x * multiplier, vector.y);
    }

    public static Vector2 MultiplyYBy(this Vector2 vector, float multiplier)
    {
        return new Vector2(vector.x, vector.y * multiplier);
    }

    public static Vector3 SetX(this Vector3 vector, float newX)
    {
        return new Vector3(newX, vector.y, vector.z);
    }

    public static Vector3 SetY(this Vector3 vector, float newY)
    {
        return new Vector3(vector.x, newY, vector.z);
    }

    public static Vector3 SetZ(this Vector3 vector, float newZ)
    {
        return new Vector3(vector.x, vector.y, newZ);
    }

    public static Vector3 ChangeXBy(this Vector3 vector, float xChange)
    {
        return new Vector3(vector.x + xChange, vector.y, vector.z);
    }

    public static Vector3 ChangeYBy(this Vector3 vector, float yChange)
    {
        return new Vector3(vector.x, vector.y + yChange, vector.z);
    }

    public static Vector3 ChangeZBy(this Vector3 vector, float zChange)
    {
        return new Vector3(vector.x, vector.y, vector.z + zChange);
    }

    public static Vector3 MultiplyXBy(this Vector3 vector, float multiplier)
    {
        return new Vector3(vector.x * multiplier, vector.y, vector.z);
    }

    public static Vector3 MultiplyYBy(this Vector3 vector, float multiplier)
    {
        return new Vector3(vector.x, vector.y * multiplier, vector.z);
    }

    public static Vector3 MultiplyZBy(this Vector3 vector, float multiplier)
    {
        return new Vector3(vector.x, vector.y, vector.z * multiplier);
    }

    public static Quaternion ToQuaternion(this Vector3 vector)
    {
        return Quaternion.Euler(vector.x, vector.y, vector.z);
    }

    public static float GetRandom(this Vector2 range)
    {
        return Random.Range(range.x, range.y);
    }

    public static float GetRandom(this Vector2Int rangeInclusive)
    {
        return Random.Range(rangeInclusive.x, rangeInclusive.y + 1);
    }

    public static Vector2 GetHeading2DTo(this Vector2 vectorFrom, Vector2 vectorTo)
    {
        return vectorTo - vectorFrom;
    }

    public static Vector2 GetHeading2DTo(this Vector3 vectorFrom, Vector2 vectorTo)
    {
        return vectorTo - (Vector2)vectorFrom;
    }

    public static Vector3 GetHeadingTo(this Vector2 vectorFrom, Vector3 vectorTo)
    {
        return vectorTo - (Vector3)vectorFrom;
    }

    public static Vector3 GetHeadingTo(this Vector3 vectorFrom, Vector3 vectorTo)
    {
        return vectorTo - vectorFrom;
    }

    public static Vector2 GetDirection2DTo(this Vector3 vectorFrom, Vector2 vectorTo)
    {
        return (vectorTo - (Vector2)vectorFrom).normalized;
    }

    public static Vector3 GetDirectionTo(this Vector2 vectorFrom, Vector3 vectorTo)
    {
        return (vectorTo - (Vector3)vectorFrom).normalized;
    }

    public static Vector3 GetDirectionTo(this Vector3 vectorFrom, Vector3 vectorTo)
    {
        return (vectorTo - vectorFrom).normalized;
    }

    public static float GetRotationZ(this Vector2 direction, bool rotateOpposite = false)
    {
        float rotation = -(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        while (rotation < 0f)
            rotation += 360f;
        while (rotation > 360f)
            rotation -= 360f;
        if (rotateOpposite)
            return -rotation;
        return rotation;
    }

    public static Vector2 GetDirection2D(float rotationZ, float rotationZOffset = 0f)
    {
        return new Vector2(Mathf.Cos((rotationZ + rotationZOffset) * Mathf.Deg2Rad),
            Mathf.Sin((rotationZ + rotationZOffset) * Mathf.Deg2Rad));
    }
}