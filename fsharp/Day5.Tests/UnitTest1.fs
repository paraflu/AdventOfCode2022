module Day5.Tests

open NUnit.Framework

[<SetUp>]
let Setup () = ()

[<Test>]
let Test1 () =
    let initialState = """
    [D]
[N] [C]
[Z] [M] [P]
 1   2   3 """

    let columns = Day5.Part1.parse initialState

    Assert.AreEqual(seq { 'N', 'Z' }, columns |> Seq.head)
    let action = "move 1 from 2 to 1"

    // let state = Day5.Part1.processAction action

    Assert.AreEqual("DCP", "")
