using static System.Console;
using System;

//////////////////////////////////////////

internal struct Val {
    internal int cnt;
}

internal struct Wrapped {
    public Val val;
    // public Val val = default(Val);
}

//////////////////////////////////////////

internal ref struct RefVal {
    internal int cnt;
}

// must be ref struct, error when struct
internal ref struct RefWrapped {
    public RefVal refval;
}

//////////////////////////////////////////

namespace ref_struct {
    class Program {
        static void Main(string[] args) {
            WriteLine("Hello World!");

            Wrapped w1 = new Wrapped { val = new Val { cnt = 100 } };
            var w2 = new Wrapped { val = new Val { cnt = 100 } };
            var w3 = new Wrapped { val = default(Val) };
            var w4 = default(Wrapped); 
            Wrapped w5;

            Span<int> s;
        }
    }
}

