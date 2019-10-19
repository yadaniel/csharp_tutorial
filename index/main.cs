using System;
using System.Collections.Generic;

using static System.Math;

class Setting {
    Dictionary<string,int> dict = new Dictionary<string,int>();

    public int this[string key] {

        get {
            return dict[key];
        } set {
            dict[key] = value;
        }

    }
}

class App {
    static void Main(string[] args) {
        var s = new Setting();
        s["foo"] = 1;
        Console.WriteLine($"{s["foo"]}");
        s["bar"] = 10;
        Console.WriteLine($"{s["bar"]}");

        try {
            Console.WriteLine($"{s["foobar"]}");
        } catch (KeyNotFoundException exc) {
            Console.WriteLine("key not found");
        }
    }
}

class Test {
    static void Main(string[] args) {
    }
}

