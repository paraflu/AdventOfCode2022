module Day4.Tests

open NUnit.Framework

[<SetUp>]
let Setup () =
    ()

[<Test>]
let TestCase1 () =
    let (a,b)= Day4.Part1.makeRange "2-4,6-8"

    Assert.AreEqual(seq {2..4}, a)
    Assert.AreEqual(seq {6..8}, b)

    Assert.IsFalse(Day4.Part1.overlap a b)
    Assert.IsFalse(Day4.Part1.fullyContain a b)

[<Test>]
let TestCase2 () =
    let (a,b)= Day4.Part1.makeRange "2-3,4-5"

    Assert.AreEqual(seq {2..3}, a)
    Assert.AreEqual(seq {4..5}, b)

    Assert.IsFalse(Day4.Part1.overlap a b)
    Assert.IsFalse(Day4.Part1.fullyContain a b)

[<Test>]
let TestCase3 () =
    let (a,b)= Day4.Part1.makeRange "5-7,7-9"

    Assert.AreEqual(seq {5..7}, a)
    Assert.AreEqual(seq {7..9}, b)

    Assert.IsTrue(Day4.Part1.overlap a b)
    Assert.IsFalse(Day4.Part1.fullyContain a b)

[<Test>]
let TestCase4 () =
    let (a,b)= Day4.Part1.makeRange "2-8,3-7"

    Assert.AreEqual(seq {2..8}, a)
    Assert.AreEqual(seq {3..7}, b)

    Assert.IsTrue(Day4.Part1.overlap a b)

    Assert.IsTrue(Day4.Part1.fullyContain a b)

[<Test>]
let TestCase5 () =
    let (a,b)= Day4.Part1.makeRange "6-6,4-6"

    Assert.AreEqual(seq {6..6}, a)
    Assert.AreEqual(seq {4..6}, b)

    Assert.IsTrue(Day4.Part1.overlap a b)
    Assert.IsTrue(Day4.Part1.fullyContain a b)


[<Test>]
let TestCase6 () =
    let (a,b)= Day4.Part1.makeRange "2-6,4-8"

    Assert.AreEqual(seq {2..6}, a)
    Assert.AreEqual(seq {4..8}, b)

    Assert.IsTrue(Day4.Part1.overlap a b)
    Assert.IsFalse(Day4.Part1.fullyContain a b)

[<Test>]
let TestCase7 () = 
    let content = """2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8""" 

    Assert.AreEqual(2, Day4.Part1.solve content);

[<Test>]
let TestCase8 () = 
    let content = """2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8""" 

    Assert.AreEqual(4, Day4.Part2.solve content);