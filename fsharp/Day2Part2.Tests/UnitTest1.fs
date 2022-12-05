module Day2Part2.Tests

open NUnit.Framework

[<SetUp>]
let Setup () =
    ()

[<Test>]
let TestBase1 () =
    Assert.AreEqual(4, "A Y" |> Day2.Part2.game |> Seq.sum)


[<Test>]
let TestBase2() =
    Assert.AreEqual(1, "B X" |> Day2.Part2.game |> Seq.sum)

[<Test>]
let TestBase3() =
    Assert.AreEqual(7, "C Z" |> Day2.Part2.game |> Seq.sum)