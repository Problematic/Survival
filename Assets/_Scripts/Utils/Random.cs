using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public static class RandomExt {
    public static T RandomElement<T>(this IEnumerable<T> items) {
		if (items== null){
			throw new System.Exception("Asking for a random value from a null list");
		}

		if (items.Count()==0){
            return default(T);
		}
        return items.ElementAt(UnityEngine.Random.Range(0, items.Count()));
    }

    public static RandomBag<T> RandomBag<T>(this IEnumerable<T> items) {
        return new RandomBag<T>(items.ToArray());
    }

    public static T RandomElement<T>(this IEnumerable<T> items, Func<T, float> probability) {
        float r = UnityEngine.Random.value;


        float max = 0;
        foreach (var item in items) max += probability(item);
        r *= max;

        foreach (var item in items) {
            float p = probability(item);
            if (r < p) return item;
            r -= p;
        }

        return default(T);
    }
}

public class RandomBag<T> : IEnumerable<T>{
    T[] items;
    int index = 0;
    public RandomBag(params T[] items) {
        this.items = items.ToArray();
        Shuffle();
    }

    public IEnumerator<T> GetEnumerator() {
        while (true) yield return Value;
    }

    IEnumerator IEnumerable.GetEnumerator() {
        while (true) yield return Value;
    }

    public T Value {
        get {
            if (index >= items.Length) Shuffle();
            return items[index++];
        }
    }
    void Shuffle() {
        for (int i = 0; i < items.Length-1; ++i) {
            int j = UnityEngine.Random.Range(i, items.Length);
            var temp = items[i];
            items[i] = items[j];
            items[j] = temp;
        }
        index = 0;
    }
}
