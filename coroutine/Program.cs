using System;
using System.Diagnostics;
using System.Collections.Generic;
using Serilog;
using System.Threading;
using static System.Console;

namespace coroutine {
    class Program {
        static void Main(string[] args) {

            Debug.Write("from debug");
            Trace.Write("test");

            foreach(var i in count()) {
                Write($"{i}, ");
                Thread.Sleep(250);
            }
            WriteLine();

            foreach(var i in range(10)) {
                Write($"{i}, ");
            }
            WriteLine();

            var e = range_rev(10);
            while(e.MoveNext()) {
                int i = e.Current;
                Write($"{i}, ");
            }
            WriteLine();

            // ReadLine();
            ReadKey(intercept: true);   // no echo
            WriteLine();
        }

        public static IEnumerable<string> count() {
            yield return "one";
            yield return "two";
            yield return "three";
            yield return "four";
            yield break;
        }

        public static IEnumerable<int> range(int n) {
            for(int i=0; i<n; i++) {
                yield return i;
            }
        }

        public static IEnumerator<int> range_rev(int n) {
            for(int i=n; i>=0; i--) {
                yield return i;
            }
        }

    }
}

