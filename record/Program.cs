using System;
using static System.Console;

record R(int x0, int x1);           // implicit class
record class RC(int x0, int x1);    // explicit class
record struct RS(int x0, int x1);   // explicit struct

record Ra(int x0, int x1);          // no defaults
// record Rb(int x0 = 0, int x1);   // error, default parameters implies that following paramters must be defauls as well
record Rc(int x0, int x1 = 1);      // OK
record Rd(int x0 = 0, int x1 = 1);  // OK

record Re {
    public int x0 { get; init; } 
    public int x1 { get; init; }
}

record Rf {
    public int x0 { get; init; } = 0;
    public int x1 { get; init; } = 1;
}

record Rg {
    public int x0 { get; init; } = 0;
    public int x1 { get; init; } = 1;
    public Rg() {
        x0 = 10;
        x1 = 11;
    }
    public Rg(int x0, int x1) {
        this.x0 = x0;
        this.x1 = x1;
    }
    public void Deconstruct(out int x0, out int x1) {
        x0 = this.x0;
        x1 = this.x1;
    }
    public void Deconstruct(out int x0, out int x1, out int sum) {
        x0 = this.x0;
        x1 = this.x1;
        sum = x0 + x1;
    }
}

class C {
    public int x0 = 0;
    public int x1 = 0;

    // may be private
    public C() {
        x0 = 1;
        x1 = 1;
    }

    // may be private
    public C(int x0, int x1) {
        this.x0 = x0;
        this.x1 = x1;
    }

    public int Deconstruct(out int x0) {
        x0 = this.x0;
        return x1;
    }

    public void Deconstruct(out int x0, out int x1) {
        x0 = this.x0;
        x1 = this.x1;
    }

    public void Deconstruct(out int x0, out int x1, out int sum) {
        x0 = this.x0;
        x1 = this.x1;
        sum = x0 + x1;
    }

    public override string ToString() {
        return $"x0={x0}, x1={x1}";
    }

    public string ToString(string msg = "") {
        return $"{msg}: x0={x0}, x1={x1}";
    }

}

struct S {
    public int x0 = 0;
    public int x1 = 0;

    // may not be private
    public S() {
        x0 = 1;
        x1 = 1;
    }

    // may be private
    public S(int x0, int x1) {
        this.x0 = x0;
        this.x1 = x1;
    }

    public int Deconstruct(out int x0) {
        x0 = this.x0;
        return x1;
    }

    public void Deconstruct(out int x0, out int x1) {
        x0 = this.x0;
        x1 = this.x1;
    }

    public void Deconstruct(out int x0, out int x1, out int sum) {
        x0 = this.x0;
        x1 = this.x1;
        sum = x0 + x1;
    }

    public override string ToString() {
        return $"x0={x0}, x1={x1}";
    }

    public string ToString(string msg = "") {
        return $"{msg}: x0={x0}, x1={x1}";
    }

}

// maybe struct as well
struct App {
    public static int Main(string[] args) {
        WriteLine("record C#10" + Environment.NewLine);

        var app = new App();
        app.test_class();
        app.test_struct();
        app.test_record();

        return 0;
    }

    void test_class() {
        WriteLine(" === class === ");
        var c0 = new C();
        var c1 = new C{};
        var c2 = new C(1,0) {x0 = 0, x1 = 1};
        var c3 = new C(x0:1,x1:0) {x0 = 0, x1 = 1};
        var c4 = new C(x0:1,x1:0) {x0 = 0, x1 = 1};     // same values as c3
        WriteLine((c3 as object).ToString());   // resolves to overriden method string ToString()
        WriteLine(c3.ToString());       // resolves to method string ToString(string)
        WriteLine(c3.ToString("c3"));
        WriteLine($"c3==c4 => {c3 == c4}");
        WriteLine($"c3==c4 => {c3.Equals(c4)}");
        WriteLine($"c3==c4 => {object.ReferenceEquals(c3,c4)}");

        int y0, y1;
        y1 = c4.Deconstruct(out y0);    // explicit resolution possible, may return non-void type
        (y0, y1) = c4;      // method arguments are in () => resolve to Deconstruct(out int, out int)
        (y0, y1, _) = c4;   // method arguments are in () => resolve to Deconstruct(out int, out int, out int)

        (int z0, int z1) = c4;

        int p0;
        (p0, int p1) = c4;

        int q1;
        (int q0, q1) = c4;

        WriteLine();
    }

    void test_struct() {
        WriteLine(" === struct === ");
        var s0 = new S();
        var s1 = new S{};
        var s2 = new S(1,0) {x0 = 0, x1 = 1};
        var s3 = new S(x0:1,x1:0) {x0 = 0, x1 = 1};
        var s4 = new S(x0:1,x1:0) {x0 = 0, x1 = 1};     // same values as s3
        WriteLine((s3 as object).ToString());   // resolves to overriden method string ToString()
        WriteLine(s3.ToString());       // resolves to method string ToString(string)
        WriteLine(s3.ToString("s3"));
        // WriteLine($"s3==s4 => {s3 == s4}");                          // error: no operator == for structs
        WriteLine($"s3==s4 => {s3.Equals(s4)}");
        // WriteLine($"s3==s4 => {object.ReferenceEquals(s3,s4)}");     // warning: do not pass structs to ReferenceEquals => returns always false
        
        int y0, y1;
        // (y0) = s4;   // not possible
        // (y0,) = s4;  // not possible
        y1 = s4.Deconstruct(out y0);    // explicit resolution possible, may return non-void type

        (y0, y1) = s4;
        (y0, y1, _) = s4;

        (int z0, int z1) = s4;

        int p0;
        (p0, int p1) = s4;

        int q1;
        (int q0, q1) = s4;

        WriteLine();
    }

    void test_record() {
        WriteLine(" === record === ");
        var r0 = new R(x0:0, x1:1);
        var r1 = new R(x0:0, x1:1) { x0 = 10, x1 = 11 };
        var r2 = new R(x0:0, x1:1) { x0 = 10, x1 = 11 };
        // var _ = new R{};    // no such syntax
        var r3 = new Rd() { x0 = 10, x1 = 11 };
        
        WriteLine((r0 as object).ToString()); 
        WriteLine(r0.ToString()); 

        WriteLine($"r1==r2 => {r1 == r2}");         // true when all members are equal
        WriteLine($"r1==r2 => {r1.Equals(r2)}");    // same as above
        WriteLine($"r1==r1 => {object.ReferenceEquals(r1,r1)}");    // true when same object
        WriteLine($"r1==r2 => {object.ReferenceEquals(r1,r2)}");    // otherwise false

        // WriteLine($"r2==r3 => {r2 == r3}");      // must be of the same type, not pure structural equality as in F#

        var r4 = new Rg();
        var r5 = new Rg(1,2);
        var r6 = new Rg(1,2) {};
        var r7 = new Rg(x0:1,2) {};
        var r8 = new Rg(x0:1,x1:2) {};
        var r9 = new Rg(1,x1:2) {};
        var r10 = new Rg(x0:1,x1:2) {x0 = 10, x1 = 11};
        
        int y0, y1;
        (y0, y1) = r10;
        (y0, y1, _) = r10;

        (int z0, int z1) = r10;

        int p0;
        (p0, int p1) = r10;

        int q1;
        (int q0, q1) = r10;

        WriteLine();
    }

}

