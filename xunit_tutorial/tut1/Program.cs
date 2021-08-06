using System;
using System.IO.Ports;
using Xunit;

namespace tut1 {
    public class App {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
        }

        public bool isOdd(uint n) => n % 2 == 1;
        public bool isEven(uint n) => n % 2 == 0;
        public bool isPercent(double p) => (p >= 0) && (p <= 100);
        public (int,int,int,int) apply(int x, int y) => (x+y,x-y,x*y,x/y);

    }
}

