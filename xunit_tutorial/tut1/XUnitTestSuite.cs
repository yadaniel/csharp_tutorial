using System;
using Xunit;

namespace tut1 {
    public class XUnitTest {

        [Fact]
        public void Test1() {
            Assert.Equal(1, 1);
            Assert.Equal(expected: 2, 2);
            Assert.Equal(3, actual: 3);
            Assert.Equal(expected: 4, actual: 4);
        }

        [Fact]
        public void Test2() {
            var app = new App();
            Assert.True(app.isOdd(11));
            Assert.True(app.isEven(10));
            Assert.True(app.isPercent(75));
        }

        [Fact]
        public void Test3() {
            var app = new App();
            Assert.False(app.isOdd(10));
            Assert.False(app.isEven(11));
            Assert.False(app.isPercent(101));
        }

        [Fact]
        public void Test4() {
            var app = new App();
            var (a,b,c,d) = app.apply(70,7);
            Assert.True(a == 77);
            Assert.False(b == 0);
            Assert.Equal(490, c);
            Assert.Equal(10, d);
        }

        [Fact]
        public void Test5() {
            Assert.True(0 == 1);
        }

    }
}

