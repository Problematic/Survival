using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public static class CoroutineExts {


    public static IEnumerator RunParallel(this IEnumerable<IEnumerator> self) {
        var enumerators = self.ToArray();
        bool[] done = new bool[enumerators.Length];
        bool running = true;
        while(true) {
            running = false;
            for (int i = 0; i < enumerators.Length; ++i) {
                if (!done[i]){
                    if (!enumerators[i].MoveNext()) done[i] = true;
                    running = true;
                }
            }
            if (!running) yield break;
            yield return null;
        }
    }

    public static Coroutine StartCoroutine(this IEnumerator self, MonoBehaviour runner) {
        return runner.StartCoroutine(self);
    }

    public static IEnumerator While(this IEnumerator self, Func<bool> condition) {
        while (condition() && self.MoveNext()) yield return self.Current;
    }

    public static IEnumerator EndEffect(this IEnumerator self, params Action[] effects) {
		while (self.MoveNext()) yield return self.Current;
        foreach (var effect in effects) effect();
    }

    public static IEnumerator StartEffect(this IEnumerator self, params Action[] effects) {
        foreach (var effect in effects) effect();
        while (self.MoveNext()) yield return self.Current;
    }
}
