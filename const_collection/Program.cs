using System;
using System.Collections.Generic;
using System.Collections.Immutable;

// alias to namespace
using Const = System.Collections.Immutable;

// alias to a type
using CList = System.Collections.Immutable.ImmutableList;       // ImmutableList<int>.Emtpy OK, but not CList<int>.Empty
using CArray = System.Collections.Immutable.ImmutableArray;     // using alias types cannot be used with type argumemnts

// open up static object
using static System.Console;

class App {
    public static void Main() {
        Console.WriteLine("main");

        test1();
        test2();
        test3();
        test4();
        test5();
        test6();
    }

    static void test1() {
        var l0 = ImmutableList.Create<int>();
        var l1 = l0.Add(0).Add(1).Add(2).Add(3).Add(4);

        // convert to mutable list
        var l2 = new List<int>(l1);

        // show content
        foreach(var i in l2) {
            Write($"{i}, ");
        }
        WriteLine();
    }

    static void test2() {
        var l0 = CList.Create<int>(0, 1, 2, 3 ,4);
        var l1 = l0.Add(5).Add(6).Add(7).Add(8).Add(9);

        // convert to mutable list
        var l2 = new List<int>(l1);

        // show content
        foreach(var i in l2) {
            Write($"{i}, ");
        }
        WriteLine();
    }

    static void test3() {
        var a0 = CArray.Create<int>(0, 1, 2, 3, 4);
        var a1 = a0.Add(5).Add(6).Add(7).Add(8).Add(9);

        // convert to mutable list
        var a2 = a1.ToArray();

        // // show content
        foreach(var i in a2) {
            Write($"{i}, ");
        }
        WriteLine();

    }

    static void test4() {
        var a0 = ImmutableArray<int>.Empty;     // OK, similar to string.Empty
        // var a0 = CArray<int>.Empty;          // using alias types cannot be used with type arguments
        var a1 = a0.Add(5).Add(6).Add(7).Add(8).Add(9);

        // convert to mutable list
        var a2 = a1.ToArray();

        // // show content
        foreach(var i in a2) {
            Write($"{i}, ");
        }
        WriteLine();

    }

    static void test5() {
        var p1 = new Params();
        var p2 = new Params() {d = 10};         // OK to set from initializer-list {}
        // var p3 = new Params() {a = 10};      // not OK to set from initializer-list {}
        var p4 = p2 with { e = 100 };

        WriteLine($"{p1.a}");
        WriteLine($"{p1}");
    }

    static void test6() {
        string s0;                      // reference type => null
        string s1 = "";                 // string.Empty;
        string? s2 = string.Empty;
        string s3 = new string("");
        string s4 = new string("") {};
        string s5 = new String("") {};
        string s6 = new System.String("") {};

        string[] sx1 = {"1", "2", "3", "4"};
        string[] sx2 = new string[] {"1", "2", "3", "4"};

        int[] ix1 = {1, 2, 3, 4};
        int[] ix2 = new int[] {1, 2, 3, 4};
    }
}

public record class Params {
    public int a { get; }       // implicit 0
    public int b { get; } = default;
    public int b1 { get; } = default!;
    public int b2 { get; init; } = default!;
    public int c { get; } = 1;
    public int d { get; init; }
    public int e { get; init; } = 3;

    public Params() {
        b = 1;                  // OK to set from constructor
        d = 2;
    }
}

