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

        let result = test_data |> Day2.Main.game |> Seq.sum
        Assert.AreEqual(15, result)
