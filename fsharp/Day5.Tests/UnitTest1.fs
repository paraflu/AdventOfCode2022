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
    [D]     
[N] [C]     [B]
[Z] [M] [P] [N]
 1   2   3   4"""

    let cmd = "move 1 from 2 to 1" |> Part1.parseCommand |> Seq.head

    Assert.AreEqual(1, cmd.move)
    Assert.AreEqual(1, cmd.src)
    Assert.AreEqual(0, cmd.dest)

    let crates = Part1.parse initialState


    let newCrate = Part1.execCommand crates cmd

    printf "Before\n"
    crates |> Part1.printCrateHolder 
    printf "After\n"
    newCrate |> Part1.printCrateHolder

    let toString (s:Part1.Stack) = List.map (fun (x:Part1.Crate) -> x.Badge) s
    
    let simple c = c |> toString |> List.toSeq |> String.concat ""
    Assert.AreEqual("DNZ", newCrate.[0] |> simple)

    Assert.AreEqual(" CM", newCrate.[1] |> simple)

    Assert.AreEqual("  P", newCrate.[2] |> simple)
    Assert.AreEqual(" BN", newCrate.[3] |> simple)

    Assert.AreEqual("DCPB", newCrate |> Part1.getHeader 
        |> Seq.toArray |> System.String)

[<Test>]
let Test5 () =
    let initialState = """
    [D]
[N] [C]
[Z] [M] [P]
 1   2   3 """

    let crateHolder = Day5.Part1.parse initialState

    Assert.AreEqual(3, crateHolder |> List.length)