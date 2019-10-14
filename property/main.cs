#define DEBUG

// [System.Diagnostics.Conditional("DEBUG")]
// public static void WriteLine (object value);

using System;
using System.Diagnostics;

/// style
class Property1 {
    private int x_;
    public int x {
        get {
            return x_;
        } set {
            x_ = value;
        }
    }
    public Property1() {
        x_ = 10;
    }
}

//
class Property2 {
    private int x_;
    public int x {
        get => x_;
        set => x_ = value;
    }
    public Property2() {
        x = 10;
    }
}

/// auto implemented
class Property3 {
    public int x {
        get;
        set;
    }
    public Property3() {
        x = 10;
    }
}

/// auto implemented, initialized
/// only auto implemented properties can have initializers
class Property4 {
    public int x {
        get;
        set;
    } = 10;
}

class Property5 {
    public int x {
        get;
        private set;
    } = 10;
}

class Property6 {
    public int x {
        get;
    } = 10;
}

class Property7 {
    private int x_;
    public int x {
        set {
            x_ = value;
        }
    }
}

class Test {
    public static void Main(string[] args) {
        var p1 = new Property1();
        Console.WriteLine($"{p1.x}");
        Debug.WriteLine($"{p1.x}");

        var p2 = new Property2();
        Console.WriteLine($"{p2.x}");
        Debug.WriteLine($"{p2.x}");

        var p3 = new Property3();
        Console.WriteLine($"{p3.x}");
        Debug.WriteLine($"{p3.x}");

        var p4 = new Property4();
        Console.WriteLine($"{p4.x}");
        Debug.WriteLine($"{p4.x}");

        var p5 = new Property5();
        // p5.x = 11;
        Console.WriteLine($"{p5.x}");
        Debug.WriteLine($"{p5.x}");
    }
}

