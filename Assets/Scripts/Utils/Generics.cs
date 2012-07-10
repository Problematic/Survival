using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class Generics {
    public static T Duplicate<T>(this T prefab, Vector3 position, Quaternion rotation) where T : Object {
        if (prefab == null) {
            Debug.LogError("prefab was null");
            return null;
        }
        return (T)Object.Instantiate(prefab, position, rotation);
    }

    public static T Duplicate<T>(this T prefab, Vector3 position) where T : Object {
        if (prefab == null) {
            Debug.LogError("prefab was null");
            return null;
        }
        return (T)Object.Instantiate(prefab, position, Quaternion.identity);
    }

    public static T Duplicate<T>(this T prefab) where T : Object {
        if (prefab == null) {
            Debug.LogError("prefab was null");
            return null;
        }
        return prefab.Duplicate(Vector3.zero);
    }

    public static T Duplicate<T>(this T prefab, Transform transform) where T : Object {
        if (prefab == null) {
            Debug.LogError("prefab was null");
            return null;
        }
        return prefab.Duplicate(transform.position, transform.rotation);
    }

    public static void Evaluate<T>(this IEnumerable<T> xs) {
#pragma warning disable 0168
        foreach (var x in xs) ;
#pragma warning restore 0168
    }

    public static void Shuffle<T>(this T[] self) {
        for (int i = 0; i < self.Length - 1; ++i) {
            var j = Random.Range(i, self.Length-1);
            var temp = self[i];
            self[i] = self[j];
            self[j] = temp;
        }
    }
    public static T ArgMin<T>(this IEnumerable<T> e, System.Func<T, float> selector) {
        float min = float.PositiveInfinity;
        T argmin = default(T);
        foreach (var x in e) {
            float val = selector(x);
            if (val < min) {
                min = val;
                argmin = x;
            }
        }
        return argmin;
    }

    public static int IntMax<T>(this IEnumerable<T> self, System.Func<T, int> selector) {
        int max = int.MinValue;
        foreach (var x in self) {
            max = Mathf.Max(max, selector(x));
        }
        return max;
    }

    public static T ArgMax<T>(this IEnumerable<T> e, System.Func<T, float> selector) {
        float max = float.NegativeInfinity;
        T argmax = default(T);
        foreach (var x in e) {
            float val = selector(x);
            if (val > max) {
                max = val;
                argmax = x;
            }
        }
        return argmax;
    }

    public static void SetTransform<T>(this T component, Transform transform) where T : Component {
        component.transform.position = transform.position;
        component.transform.rotation = transform.rotation;
    }

    public static void SetTransform(this GameObject go, Transform transform) {
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;
    }

    public static void Add<K1, K2, V>(this Dictionary<K1, Dictionary<K2, V>> self, K1 k1, K2 k2, V v) {
        if (!self.ContainsKey(k1)) {
            self[k1] = new Dictionary<K2, V>();
        }
        var inner = self[k1];
        inner.Add(k2, v);
    }

    public static V Get<K1, K2, V>(this Dictionary<K1, Dictionary<K2, V>> self, K1 k1, K2 k2) {
        if (!self.ContainsKey(k1)) return default(V);
        if (!self[k1].ContainsKey(k2)) return default(V);
        return self[k1][k2];
    }
}
