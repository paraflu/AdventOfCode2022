namespace Day1.Tests

open NUnit.Framework

[<TestFixture>]
type TestClass() =

    [<Test>]
    member this.TestData() =
        let test =
            """
        1000
        2000
        3000

        4000

        5000
        6000

        7000
        8000
        9000

        10000"""

        Assert.AreEqual(24000, test |> Day1.Main.caloriesList |> Seq.max)
