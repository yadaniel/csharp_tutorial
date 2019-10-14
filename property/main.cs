using System;

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

/// auto implemented, initialized in constructor
class Property3 {
    public int x {
        get;
        set;
    }
    public Property3() {
        x = 10;
    }
}

/// auto implemented property with and without initializer
/// only auto implemented properties can have initializers
class Property4 {
    public int x {
        get;
        set;
    } = 10;
    public int y {
        get;
        set;
    }
}

/// auto property with get, private set
class Property5 {
    public int x {
        get;
        private set;
    } = 10;
    public int y {
        get;
        private set;
    }
}

/// auto property with get only
/// note: auto property with set only not possible
class Property6 {
    public int x {
        get;
    } = 10;
    public int y {
        get;
    }
}

/// property with back field may have get only and set only
/// property with back field may not have initializers
/// back field can be initialized
class Property7 {
    // set only
    private int x_;
    public int x {
        set {
            x_ = value;
        }
    }
    // get only
    private int y_ = 10;
    public int y {
        get {
            return y_;
        }
    }
}

class Test {
    public static void Main(string[] args) {
        var p1 = new Property1();
        Console.WriteLine($"{p1.x}");

        var p2 = new Property2();
        Console.WriteLine($"{p2.x}");

        var p3 = new Property3();
        Console.WriteLine($"{p3.x}");

        var p4 = new Property4();
        Console.WriteLine($"{p4.x}");

        var p5 = new Property5();
        // p5.x = 11;
        Console.WriteLine($"{p5.x}");
    }
}

