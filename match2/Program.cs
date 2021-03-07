using System;
using System.Linq;
using static System.Math;

// struct object is value type and cannot have constructor explicit constructor without parameters
// struct cannot be declared as static
// struct cannot have default initializer

struct S0 { }
struct S1 {
    S1(uint x) { this = default; }
}
struct S2 {
    S2(uint _) { this = default; }
}


struct S3 {
    S0 s0;
    uint x;
}

struct DataStruct {
    public int x;
    public int y;
    public int z;
}
class DataClass {
    public int x = 1;
    public int y = 2;
    public int z = 3;
}

// class or struct
class DataDeconstruct {
    public int x;
    public int y;
    public int z;
    public void Deconstruct(out int a, out int b) {
        a = x;
        b = y + z;
    }
}

// class or struct
struct Outer {
    public class Inner {
        public int inner_data;
    }

    public Inner inner;
    public int outer_data;
}

interface Irwx {

}

class X<T> {
    public uint x = 1;      // initializer

    public X() { x = 2; }   // overrides initializer
    public X(uint k) { x = k; }   // overrides initializer
}

class C { }

public delegate int F(int arg);
class Match {

    public bool f(uint x) {
        int i = x switch {
            1 => 1,
            2 => 2,
            3 => 3,
            4 => 4,
            5 | 6 | 7 | 8 => 10,
            _ => 0,
        };
        return true;
    }

    public Func<int, int> p0 = (int x) => 0;
    public Func<int, int> p1 = (int x) => x switch {
        _ => 0
    };

    public int p2(int x) => 0;
    public int p3(int x) => x switch { _ => 0 };
    public static int p4(int x) => x switch { _ => 0 };     // p4 can be assigned to F, FF, FFF

    public static F y0 = p4;
    public static FF y1 = p4;
    public static FFF y3 = p4;

    public F f1 = (x) => 0;
    public static F f2 = (x) => 0;
    //public static FF q0 = f2;
    //public static FFF q1 = f2;

    public static int foo(int x) { return x; }  // can be assigned to FF and FFF
    public delegate int FF(int x);
    public delegate int FFF(int x);
    public static FF ff0 = foo;
    public static FFF ff1 = foo;
    //public static FFF ff3 = ff0;  // FF and FFF are different types

}

class Program {
    Func<int, int, string> f0 = (a, b) => (a + b).ToString("X04");
    Func<uint, uint, string> f1 = (uint a, uint b) => { return (a + b).ToString("X04"); };

    static void Main(string[] args) {
        Console.WriteLine("Hello World!");
        S1 s1 = default;
        S2 s2 = default(S2);
        Console.WriteLine(s1.ToString());
        Console.WriteLine(s2.ToString());

        // initializer list works for struct and class
        var dataStruct = new DataStruct() { x = 1, y = 2, z = 3 };
        var dataClass = new DataClass() { x = 1, y = 2, z = 3 };
        Console.WriteLine(dataStruct.ToString());
        Console.WriteLine(dataClass.ToString());

        // initializer runs last
        //var x = new X<int> { };
        //var x = new X<int>() {};
        //var x = new X<int>() {x = 3};     // overrides constructor
        var x = new X<int>(10) { x = 3 };     // overrides constructor
        Console.WriteLine(x.ToString());
        Console.WriteLine(x.x.ToString());

        // var c0 = new C;  // () {} [] expected
        var c1 = new C();
        var c3 = new C() { };
        var c4 = new C { };

        bool found = true;
        if (found) {
            int j = 1 switch { _ => 0 };
            Console.WriteLine($"found {1 + 1}");
        }

        // tuple
        var vals0 = (1, 2, 3, 4);
        int _ = vals0 switch {
            (1, 1, 1, 1) => 1,
            (2, 2, 2, 2) => 2,
            (3, 3, 3, 3) => 3,
            (4, 4, 4, 4) => 4,
            (5, 5, _, _) => 5,
            (var a, var b, var c, var d) when a + b == c + d => a + b + c + d,
            _ => 0,
        };
        Console.WriteLine($"{_}");

        // don't know if possible
        var vals1 = new int[] { 1, 2, 3, 4 };

        // anonymous object
        var vals2 = new {x=1, y=2, z=3};
        _ = vals2 switch {
            {x:1, y:2} => 1,
            _ => 0,
        };

        // struct Data
        var vals3 = new DataStruct {x=1, y=2, z=3};
        _ = vals3 switch {
            {x:1, y:2} => 1,        // implicit ignore
            {x:3, y:1, z:_} => 1,   // explicit ignore
            _ => 0,
        };

        // class Data
        var vals4 = new DataClass {x=1, y=2, z=3};
        _ = vals4 switch {
            {x:1, y:2} => 1,
            _ => 0,
        };

        // class or struct with Deconstruct method
        var vals5 = new DataDeconstruct {x=1, y=2, z=3};
        bool b5 = vals5 switch {
            DataDeconstruct(1,5) => true,
            _ => false,
        };
        if(b5) {
            Console.WriteLine("true");
        }

        // class or struct with nested member
        var vals6 = new Outer { inner = new Outer.Inner{inner_data = 1}, outer_data = 1};
        _ = vals6 switch {
            {inner:{inner_data:1}} => 1,
            {outer_data:1, inner:{inner_data:2}} => 2,
            {outer_data: var v0, inner:{inner_data: var v1}} when v0 > 10 => 3,
            _ => 0,
        };

    }
}
