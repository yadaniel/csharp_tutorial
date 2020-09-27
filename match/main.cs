using System;
using static System.Math;
using static System.Console;

class App {
    private int foo;
    public int bar1 = 1, bar2 = 2;

    // tuples
    (int x, int y, int z) swap(int a, int b) {
        return (b, a, a + b);
    }

    static void Main( string[] args ) {
        var app = new App() {
            foo = 1, bar1 = 2, bar2 = 3
        };
        WriteLine($"{app.foo}");

        // using extension function
        app.process();

        // discard
        (var _, var _, var sum) = app.swap(1, 2);
        var (_, _, sum1) = app.swap(1, 2);

        // switch pattern
        var n = int.Parse(Console.ReadLine());
        switch(n) {
        case int x when ((x > 0) && (x < 100)):
            WriteLine("between 0 and 100");
            break;
        case int x when ((x >= 100) && (x < 200)):
            WriteLine("between 100 and 200");
            break;
        default:
            WriteLine("unknown");
            break;
        }

        // switch expression
        var i = int.Parse(Console.ReadLine());
        var j = i switch {
        0 => 0,
        1 => 10,
        _ => 100
    };
    WriteLine($"j={j}");

    }
}

class User {
    public void test() {
        var a0 = new App();
        var a1 = new App() {
            // foo = 1
        };
        var a2 = new App() {
            // foo = 0,
            bar1 = 1,
            bar2 = 2,
        };
    }
}

static class Util {
    public static void process(this App app) {
        WriteLine($"{app.bar1}, {app.bar2}");
        // WriteLine($"{app.foo}");
    }
}

// global namespace
class X {}
class Y {}
class Z {}

namespace Q {
class X {}
class Y {}
class Z {
    private double q0 = 1.0;
    protected double q1 = 1.0;
    public double q2 = 1.0;
    private void f0() {}
    protected void f1() {}
    public void f2() {}

    private Func<int, double> a0 = (int x) => {
        return x * 1.0;
    };
    protected Func<int, int> a1 = (int x) => x;
    public Func<int, int> a2 = (int x) => (x + x);

    private Action<int, int> b0;
    protected Action<int, int> b1 = null;
    // public Action<int, int> b2 = (x, y) => x + y;
    public Action<int, int> b2 = (x, y) => {
        var z = x + y;
    };

    // public Action<int, int> b3 = (x, y) => if(true) {};  // not allowed

    public Action<int, int> b3 = (x, y) => {
        if(true) {
        }
    };

}
}

namespace R {
class X {}
class Y {}
class Z {}
}

