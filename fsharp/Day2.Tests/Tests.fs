namespace Day2.Tests

open NUnit.Framework

[<TestFixture>]
type TestClass() =

    [<Test>]
    member this.TestData() =
        let test_data =
            """A Y
            B X
            C Z"""

        let result: int = test_data |> Day2.Main.game |> Seq.sum
        Assert.AreEqual(15, result)

    [<Test>]
    member this.TestBase1() =
        Assert.AreEqual(8, "A Y" |> Day2.Main.game |> Seq.sum)


    [<Test>]
    member this.TestBase2() =
        Assert.AreEqual(1, "B X" |> Day2.Main.game |> Seq.sum)

    [<Test>]
    member this.TestBase3() =
        Assert.AreEqual(6, "C Z" |> Day2.Main.game |> Seq.sum)