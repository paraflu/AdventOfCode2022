module Day5.Tests

open NUnit.Framework
open Day5.Part1

let toString (s:Stack) = List.map (fun (x:Crate) -> x.Badge) s
let simple c = c |> toString |> List.toSeq |> String.concat "" |> (fun s -> s.Trim())


[<SetUp>]
let Setup () = ()

[<Test>]
let Test1 () =
    let initialState = """
    [D]
[N] [C]
[Z] [M] [P]
 1   2   3 """

    let crateHolder = initialState.Split("\r\n") |> parse 

    Assert.AreEqual(3, crateHolder |> List.length)

    //crateHolder |> List.head |> List.map (fun s -> s.Badge) |> printf "%A\n"

    Assert.AreEqual(3, crateHolder |> List.head |> List.length)
    Assert.AreEqual("NZ", crateHolder.[0] |> simple)
    Assert.AreEqual("DCM", crateHolder.[1] |> simple)
    Assert.AreEqual("P", crateHolder.[2] |> simple); 

[<Test>]
let Test2 () =
    let initialState = """
    [D]     [B]
[N] [C]
[Z] [M] [P] [N]
 1   2   3   4"""

    let crateHolder = initialState.Split("\r\n") |> parse 

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

    let cmd = "move 1 from 2 to 1" |> parseCommandList |> Seq.head

    Assert.AreEqual(1, cmd.move)
    Assert.AreEqual(1, cmd.src)
    Assert.AreEqual(0, cmd.dest)

    let crates = initialState.Split("\r\n") |> parse 
    let firstCommand = execCommand crates cmd

    
    Assert.AreEqual("DNZ", firstCommand.[0] |> simple)

    Assert.AreEqual("CM", firstCommand.[1] |> simple)

    Assert.AreEqual("P", firstCommand.[2] |> simple)
    Assert.AreEqual("BN", firstCommand.[3] |> simple)

    Assert.AreEqual("DCPB", firstCommand |> getHeader |> Seq.toArray |> System.String)

   

[<Test>]
let Test5 () =
    let initialState = """
    [D]
[N] [C]
[Z] [M] [P]
 1   2   3 """

    let crates = initialState.Split("\r\n") |> parse 
    let firstCommand = 
        "move 1 from 2 to 1" 
        |> parseCommandList 
        |> Seq.head 
        |> execCommand crates
        |> tap printCrateHolder

    Assert.AreEqual("DNZ", firstCommand.[0] |> simple)
    Assert.AreEqual("CM", firstCommand.[1] |> simple)
    Assert.AreEqual("P", firstCommand.[2] |> simple)

    let secondCommand =  
        "move 3 from 1 to 3" 
        |> parseCommandList 
        |> Seq.head 
        |> execCommand firstCommand
        //|> tap printCrateHolder

    Assert.AreEqual("", secondCommand.[0] |> simple)
    Assert.AreEqual("CM", secondCommand.[1] |> simple)
    Assert.AreEqual("DNZP", secondCommand.[2] |> simple)

    Assert.AreEqual(" CD", secondCommand |> getHeader |> Seq.toArray |> System.String)

//[<Test>]
//let TestNormalize () =
//    let c = [[Crate("[A]"), Crate("[B]")]]

[<Test>]
let TestReduce () =

    let cmd = "move 1 from 2 to 1
move 3 from 1 to 3" |> parseCommandList |> Seq.toList
    let initialState = """
    [D]
[N] [C]
[Z] [M] [P]
 1   2   3 """

    let crates = initialState.Split("\r\n") |> parse

 
        
    let result = reduce cmd crates

    Assert.AreEqual("", result.[0] |> simple)
    Assert.AreEqual("CM", result.[1] |> simple)
    Assert.AreEqual("DNZP", result.[2] |> simple)

    Assert.AreEqual(" CD", result |> getHeader |> Seq.toArray |> System.String)


[<Test>]
let TestFile () =
    let fileContent = """    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2"""

    let (spec, commands) = getSpecFromContent fileContent

    Assert.AreEqual(4, spec |> List.length)    
    Assert.AreEqual(4, spec |> List.length)

    let initial = spec |> List.toArray |> parse
    let cmdList = commands |> List.map parseCommand

    let solve_part1 x = x |> getHeader |> Seq.map string |> String.concat ""

    Assert.AreEqual("NDP", initial |> solve_part1)

    let step1 = (cmdList |> List.take 1, initial) ||> reduce 
    Assert.AreEqual("DCP", step1 |> solve_part1)

    let step2 = (cmdList |> List.take 2, initial) ||> reduce 
    Assert.AreEqual(" CZ", step2 |> solve_part1)

    let step3 = (cmdList |> List.take 3, initial) 
                ||> reduce 
                //|> tap printCrateHolder
    Assert.AreEqual("M Z", step3 |> solve_part1)

    let step4 = (cmdList, initial) 
                ||> reduce 
    Assert.AreEqual("CMZ", step4 |> solve_part1)