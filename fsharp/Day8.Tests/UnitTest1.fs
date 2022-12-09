module Day8.Tests

open NUnit.Framework
open Day8.Part1

[<SetUp>]
let Setup () =
    ()

[<Test>]
let Test1 () =
    let trees = [
        [3;0;3;7;3];
        [2;5;5;1;2];
        [6;5;3;3;2];
        [3;3;5;4;9];
        [3;5;3;9;0];
    ]


    Assert.AreEqual(true, checkVisibleByDirection trees Top (2,2))
    Assert.AreEqual(true, checkVisibleByDirection trees Left (2,2))
    Assert.AreEqual(false, checkVisibleByDirection trees Bottom (2,2))
    Assert.AreEqual(false, checkVisibleByDirection trees Right (2,2))

    Assert.AreEqual([3;0;3;7;3], getRow trees Top (1,2))

    Assert.AreEqual(true, checkVisibleByDirection trees Top (1,1))
    Assert.AreEqual(true, checkVisibleByDirection trees Top (2,1))
    Assert.AreEqual(true, checkVisibleByDirection trees Top (3,1))
    Assert.AreEqual(true, checkVisibleByDirection trees Top (4,1))
    Assert.AreEqual(true, checkVisibleByDirection trees Top (5,1))

    Assert.AreEqual(true, checkVisibleByDirection trees Left (2,2))
    Assert.AreEqual(false, checkVisibleByDirection trees Bottom (2,2))
    Assert.AreEqual(false, checkVisibleByDirection trees Right (2,2))

    Assert.AreEqual(21, visibleTrees trees)