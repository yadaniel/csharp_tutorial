using System;

interface Ix {
    void foo();
    void bar();
}

interface Iy {
    void foo();
    void foobar();
}

interface Iz {

    // public int a;

    // public int a {
    //     get;
    //     set;
    // }

    public void a() {
        Console.WriteLine("a");
    }

}

class Q : Iz {
}

class X : Ix, Iy {
    public void foo() {
        Console.WriteLine("X.foo");
    }
    public void bar() {
        Console.WriteLine("X.bar");
    }
    public void foobar() {
        Console.WriteLine("X.foobar");
    }
}

class Y : Ix, Iy {
    void Ix.foo() {
        Console.WriteLine("Y.Ix.foo");
    }
    void Iy.foo() {
        Console.WriteLine("Y.Iy.foo");
    }
    public void bar() {
        Console.WriteLine("Y.bar");
    }
    public void foobar() {
        Console.WriteLine("Y.foobar");
    }
}

class Z : Ix, Iy {
    void Ix.foo() {
        Console.WriteLine("Z.Ix.foo");
    }
    void Iy.foo() {
        Console.WriteLine("Z.Iy.foo");
    }
    public void foo() {
        Console.WriteLine("Z.foo");
    }
    public void bar() {
        Console.WriteLine("Z.bar");
    }
    public void foobar() {
        Console.WriteLine("Z.foobar");
    }
}

class App {
    public static void Main(string[] args) {
        Console.WriteLine("Main");
        var app = new App();
        app.test1();
        app.test2();
        app.test3();
        app.test4();
    }

    /// X has one implementation of foo for both interfaces
    public void test1() {
        Console.WriteLine(Environment.NewLine + "test1");
        X x = new X();
        x.foo();
        x.bar();
        x.foobar();

        Ix ix = x;
        ix.foo();
        ix.bar();

        Iy iy = x;
        iy.foo();
        iy.foobar();
    }

    /// Y has two implementations of foo, one for each interface
    public void test2() {
        Console.WriteLine(Environment.NewLine + "test2");
        Y y = new Y();
        // (y as Ix).foo();
        // (y as Iy).foo();
        y.bar();
        y.foobar();

        Ix ix = y;
        ix.foo();
        ix.bar();

        Iy iy = y;
        iy.foo();
        iy.foobar();
    }

    /// Z has three implementations of foo, one for each interface and one for class
    public void test3() {
        Console.WriteLine(Environment.NewLine + "test3");

        Z z = new Z();
        (z as Ix).foo();
        (z as Iy).foo();
        z.foo();
        z.bar();
        z.foobar();

        Ix ix = z;
        ix.foo();
        ix.bar();

        Iy iy = z;
        iy.foo();
        iy.foobar();
    }

    /// default implementations
    public void test4() {
        Console.WriteLine(Environment.NewLine + "test4");

        Q q;
        return;

        // q = new Q;
        q = new Q();   // exception because of default implementation in interface
        q = new Q {};
        q = new Q() {};

        Console.WriteLine($"{q}");
    }

}

class Test {
    public static void Main(string[] args) {
        Console.WriteLine("Test");
    }
}

