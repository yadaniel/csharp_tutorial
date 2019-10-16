using System;

class App {
    public static void Main(string[] args) {
        Console.WriteLine("Main");
        var app = new App();
        app.test1();
        app.test2();
        app.test3();
        app.test4();
    }

    public void test1() {
        Func<int,int,int> f1 = (x,y) => x + y;
        Func<int,int,int> f2 = (x,y) => {
            return (x + y);
        };
        var r = f1(1,2);
        Console.WriteLine($"{r.GetType().ToString()}");

        Action<int,int> p1 = (x,y) => Console.WriteLine($"{x+y}");
        Action<int,int> p2 = (x,y) => {
            Console.WriteLine($"{x+y}");
        };
    }


    public delegate int F();
    public event F f;    // events are variables of delegate type. Variables can only be called from within the class/struct.
    public int f1() {
        Console.WriteLine("f1");
        return 1;
    }
    public int f2() {
        Console.WriteLine("f2");
        return 2;
    }

    // delegate int F();
    // event F f;    // events are variables of delegate type. Variables can only be called from within the class/struct.
    // int f1() {
    //     Console.WriteLine("f1");
    //     return 1;
    // }
    // int f2() {
    //     Console.WriteLine("f2");
    //     return 2;
    // }

    public void test2() {
        F f = new F(f1);
        Console.WriteLine($"test2 {f()}");

        f = f2;
        Console.WriteLine($"test2 {f()}");
    }

    public void runf() {
        f();
    }

    public void test3() {
        f += f1;
        f += f2;
        f();   // call from within class
    }

    public void test4() {
        // Type t = 1.GetType();
        // Type t = 1.0.GetType();
        // Type t = 1f.GetType();
        // Type t = 1m.GetType();
        Type t = '1'.GetType();
        Console.WriteLine($"{t.ToString()}");
    }
}

class Test {
    public static void Main(string[] args) {
        Console.WriteLine("Test");
        var app = new App();
        app.f += app.f1;   // event can be loaded outside of the class
        // app.f();    // event can not be called outside of the class
        app.runf();
    }
}

