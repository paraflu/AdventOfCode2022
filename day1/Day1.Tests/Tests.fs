namespace Day1.Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Day1

[<TestClass>]
type TestClass() =

    [<TestMethod>]
    member this.TestMethodPassing() = Assert.IsTrue(true)

    [<TestMethod>]
    member this.TestDAta() =
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

        Assert.Equals(24000, test |> Day1.Main.caloriesList)
