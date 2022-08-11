using System;

// note: "as" cannot be used with non-nullable type
// note: "out" parameter cannot have default value

class App {

    public int id = 0;

    public static void Main(string[] args) {

        // var app = new App();
        // var app = new App() {};
        // var app = new App() { id = 1};
        // var app = new App {};
        var app = new App { id = 1 };

        // out type varname = declaration in place

        int zz;                                     // zz not initialized
        app.foo(out int xx, out int yy, out zz);    // zz must be written
        Console.WriteLine($"{xx},{yy},{zz}");       // zz read and must have value

        int y_ = 0;
        app.foo2(out int x_, ref y_);

        app.foo2(out _, ref y_);    // when out not used => explicit discard with _

        Environment.Exit(1);
    }

    public UInt32 foo(out int x, out int y, out int z) {
        x = 1;
        y = 2;
        z = 3;
        return (uint)(x + y);
    }

    public void foo2(out int x, ref int y) {
        x = y + 1;
        // y not required to be written
        y = 2;  // but it can and will keep the value
    }

    // public SByte bar() {
    // public Int16 bar() {
    // public Int32 bar() {
    // public Int64 bar() {
    //
    // public Byte bar() {
    // public UInt16 bar() {
    // public UInt32 bar() {
    public UInt64 bar() {
        return 1;
    }

    // public System.Void f0() {}
    public void f0() {}

    // public System.Double f1() {
    public double f1() {
        return 0.0f;    // float converted to double
    }

    // public System.Single f2() {
    public float f2() {
        //return 0.0;     // double not converted to float
        return 0.0f;
    }

}

