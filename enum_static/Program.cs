using System;
using System.Text;
using System.Text.RegularExpressions;

// make fields accessible with short name
using static State;

enum State {
    S0,     // 0
    S1,     // 1
    S2,     // 2
    S3,     // 3
    S4,     // 4
}

class App {
    public static void Main() {
        Console.WriteLine("in main");

        test();
        test0();
        test1();
        test2();
        test3();
    }

    static void test() {
        State s;                    // value type
        s = S1;

        int i = s switch {
            S0 => 0,
            S1 => 1,
            _ => -1 
        };

        // with switch expression use => OR, AND, WHEN
        // never use |,&,if
        int j = s switch {
            S0 | S2 => 0,       // 0|2 = 2
            S0 => -1,           // 0
            // S0 & S2 => 0,    // 0&2 = 0   error: not reachable
            S1 or S3 => 1, 
            S4 when true => 2, 
            // S4 when false => 3,      // error: not reachable
            // S4 => 4,                 // error: not reachable
            _ => -1 
        };

        Console.WriteLine($"{s} => {i}, {j}");
    }

    static void test0() {
        State s = default;          // default
        switch(s) {
            default:
                break;
        }
    }

    static void test1() {
        State s = default(State);   // default for value type
        switch(s) {
            default:
                break;
        }
    }

    static void test2() {
        State s = new();        // target typed
        switch(s) {
            default:
                break;
        }
    }

    static void test3() {
        var s = new State();    // RHS typed
        switch(s) {
            default:
                break;
        }
    }

}
