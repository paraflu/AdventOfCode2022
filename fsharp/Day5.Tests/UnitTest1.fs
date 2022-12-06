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

    let crateHolder = Day5.Part1.parse initialState

    Assert.AreEqual(3, crateHolder |> List.length)

    //crateHolder |> List.head |> List.map (fun s -> s.Badge) |> printf "%A\n"

    Assert.AreEqual(3, crateHolder |> List.head |> List.length)
    Assert.AreEqual(seq {" "; "N" ; "Z"}, crateHolder |> List.head |> List.map (fun s -> s.Badge))
    Assert.AreEqual(seq {"D"; "C" ; "M"}, crateHolder |> List.tail |> List.head |> List.map (fun s -> s.Badge ))
    Assert.AreEqual(seq {" "; " "; "P"}, crateHolder 
                                                |> List.tail 
                                                |> List.tail 
                                                |> List.head 
                                                |> List.map (fun s -> s.Badge ))

[<Test>]
let Test2 () =
    let initialState = """
    [D]     [B]
[N] [C]
[Z] [M] [P] [N]
 1   2   3   4"""

    let crateHolder = Day5.Part1.parse initialState

    Assert.AreEqual(4, crateHolder |> List.length)

    //crateHolder |> List.head |> List.map (fun s -> s.Badge) |> printf "%A\n"

    Assert.AreEqual(3, crateHolder |> List.head |> List.length)
    Assert.AreEqual(seq {" "; "N" ; "Z"}, crateHolder |> List.head |> List.map (fun s -> s.Badge))
    Assert.AreEqual(seq {"D"; "C"; "M"}, crateHolder |> List.tail |> List.head |> List.map (fun s -> s.Badge ))
    Assert.AreEqual(seq {" "; " ";"P"}, crateHolder 
                                                |> List.tail 
                                                |> List.tail 
                                                |> List.head 
                                                |> List.map (fun s -> s.Badge ))
    Assert.AreEqual(seq {"B"; " ";"N"}, crateHolder 
                                        |> List.rev 
                                        |> List.head
                                        |> List.map (fun s -> s.Badge))

[<Test>]
let Test3 () =
    let initialState = """
    [D]     [B]
[N] [C]
[Z] [M] [P] [N]
 1   2   3   4"""

    let cmd = "move 1 from 2 to 1" |> Part1.parseCommand |> Seq.head

    Assert.AreEqual(0, cmd.move)
    Assert.AreEqual(1, cmd.src)
    Assert.AreEqual(0, cmd.dest)

    let crates = Part1.parse initialState


    let newCrate = Part1.execCommand crates cmd

    printf "Before\n"
    crates |> Part1.printCrateHolder 
    printf "After\n"
    newCrate |> Part1.printCrateHolder

    let x = ["N";"Z"]
    Assert.AreEqual(x, 
        newCrate |> List.head |> List.map (fun s -> s.Badge))