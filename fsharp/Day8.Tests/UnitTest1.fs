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

    Assert.AreEqual([3;2;6;3;3], getRow trees Top (1,1))
    Assert.AreEqual([0;5;5;3;5], getRow trees Top (2,1))
    Assert.AreEqual([5;3;5;5;0], getRow trees Bottom (2,1))
    Assert.AreEqual([2;5;5;1;2], getRow trees Left (2,2))
    Assert.AreEqual([0;9;3;5;3], getRow trees Right (1,5))

    Assert.AreEqual(true, checkVisibleByDirection trees Top (2,2))
    Assert.AreEqual(true, checkVisibleByDirection trees Left (2,2))
    Assert.AreEqual(false, checkVisibleByDirection trees Bottom (2,2))
    Assert.AreEqual(false, checkVisibleByDirection trees Right (2,2))

    Assert.AreEqual(true, checkVisibleByDirection trees Top (1,1))
    Assert.AreEqual(true, checkVisibleByDirection trees Top (2,1))
    Assert.AreEqual(true, checkVisibleByDirection trees Top (3,1))
    Assert.AreEqual(true, checkVisibleByDirection trees Top (4,1))
    Assert.AreEqual(true, checkVisibleByDirection trees Top (5,1))

    Assert.IsTrue(checkVisibleByDirection trees Left (1,1))
    Assert.IsTrue(checkVisibleByDirection trees Left (1,2))
    Assert.IsTrue(checkVisibleByDirection trees Left (1,3))
    Assert.IsTrue(checkVisibleByDirection trees Left (1,4))
    Assert.IsTrue(checkVisibleByDirection trees Left (1,5))

    Assert.AreEqual(true, checkVisibleByDirection trees Left (2,2))
    Assert.AreEqual(false, checkVisibleByDirection trees Bottom (2,2))
    Assert.AreEqual(false, checkVisibleByDirection trees Right (2,2))

    Assert.AreEqual(21, visibleTrees trees)


[<Test>]
let Test2 () =
    let maze = [
        [3;0;3;7;3];
        [2;5;5;1;2];
        [6;5;3;3;2];
        [3;3;5;4;9];
        [3;5;3;9;0];
    ]

    Assert.AreEqual(0, getScore maze Top (1,1))
    Assert.AreEqual(0, getScore maze Top (2,1))
    Assert.AreEqual(0, getScore maze Top (3,1))
    Assert.AreEqual(0, getScore maze Top (4,1))
    Assert.AreEqual(0, getScore maze Top (5,1))
    Assert.AreEqual(0, getScore maze Top (1,5))
    Assert.AreEqual(0, getScore maze Top (2,5))
    Assert.AreEqual(0, getScore maze Top (3,5))
    Assert.AreEqual(0, getScore maze Top (4,5))
    Assert.AreEqual(0, getScore maze Top (5,5))

    Assert.AreEqual(1, getScore maze Top (3,2))
    Assert.AreEqual(2, getScore maze Bottom (3,2))
    Assert.AreEqual(1, getScore maze Left (3,2))
    Assert.AreEqual(2, getScore maze Right (3,2))

    Assert.AreEqual(4, getScoreTree maze (3,2) )

    Assert.AreEqual(8, getMaxScenicScore maze)