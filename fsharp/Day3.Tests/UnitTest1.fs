module Day3.Tests

open NUnit.Framework
open Day3

[<SetUp>]
let Setup () =
    ()

[<Test>]
let TestCase1 () =
    let content = "vJrwpWtwJgWrhcsFMMfFFhFp"
    let zaino = Part1.Zaino(content)
    Assert.AreEqual("vJrwpWtwJgWr", zaino.First)
    Assert.AreEqual("hcsFMMfFFhFp", zaino.Second)
    let check = Part1.checkBackpack content 
    Assert.IsTrue(check|> Seq.exists (fun (er, i) -> er))

    Assert.AreEqual(1, check 
            |> Seq.filter (fun (er, i) -> er) 
            |> Seq.length)

    Assert.AreEqual('p', check 
            |> Seq.find (fun (err, i) -> err) |> (fun (_, i) -> i))


[<Test>]
let TestCase2 () = 
    let content = "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL"
    let zaino = Part1.Zaino(content)
    Assert.AreEqual("jqHRNqRjqzjGDLGL", zaino.First)
    Assert.AreEqual("rsFMfFZSrLrFZsSL", zaino.Second)
    let check = Part1.checkBackpack content

    Assert.IsTrue(check|> Seq.exists (fun (er, i) -> er))

    Assert.AreEqual(2, check 
            |> Seq.filter (fun (er, i) -> er) 
            |> Seq.length)

    Assert.AreEqual('L', check 
            |> Seq.find (fun (err, i) -> err) |> (fun (_, i) -> i))

[<Test>]
let TestCase3 () = 
    let content = "PmmdzqPrVvPwwTWBwg"
    let zaino = Part1.Zaino(content)
    Assert.AreEqual("PmmdzqPrV", zaino.First)
    Assert.AreEqual("vPwwTWBwg", zaino.Second)
    let check = Part1.checkBackpack content

    Assert.IsTrue(check|> Seq.exists (fun (er, i) -> er))

    Assert.AreEqual(2, check 
            |> Seq.filter (fun (er, i) -> er) 
            |> Seq.length)

    Assert.AreEqual('P', check 
            |> Seq.find (fun (err, i) -> err) |> (fun (_, i) -> i))

[<Test>]
let TestCase4 () = 
    let content = "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn"
    let check = Part1.checkBackpack content

    Assert.IsTrue(check|> Seq.exists (fun (er, i) -> er))

    Assert.AreEqual('v', check 
            |> Seq.find (fun (err, i) -> err) |> (fun (_, i) -> i))


[<Test>]
let TestCase5 () = 
    let content = "ttgJtRGJQctTZtZT"
    let check = Part1.checkBackpack content

    Assert.IsTrue(check|> Seq.exists (fun (er, i) -> er))

    Assert.AreEqual('t', check 
            |> Seq.find (fun (err, i) -> err) |> (fun (_, i) -> i))


[<Test>]
let TestCase6 () = 
    let content = "CrZsJsPPZsGzwwsLwLmpwMDw"
    let check = Part1.checkBackpack content

    Assert.IsTrue(check|> Seq.exists (fun (er, i) -> er))

    Assert.AreEqual('s', check 
            |> Seq.find (fun (err, i) -> err) |> (fun (_, i) -> i))

[<Test>]
let TestPriority () = 
    Assert.AreEqual(16, Part1.getPriority('p'))    
    Assert.AreEqual(38, Part1.getPriority('L'))    
    Assert.AreEqual(42, Part1.getPriority('P'))    
    Assert.AreEqual(22, Part1.getPriority('v'))    
    Assert.AreEqual(20, Part1.getPriority('t'))
    Assert.AreEqual(19, Part1.getPriority('s'))

[<Test>]
let TestGame () =
    let content = """vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw"""
    Assert.AreEqual(157, Part1.solve(content))



[<Test>]
let TestCase31 () =
    let content = """vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg"""
    let badge = content.Split("\r\n") 
                    |> Day3.Part2.parseContent  
                    |> Day3.Part2.getBadge     
    Assert.AreEqual('r', badge)

[<Test>]
let TestCase32 () =
    let content = """wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw"""
    let badge = content.Split("\r\n")
                    |> Day3.Part2.parseContent 
                    |> Day3.Part2.getBadge 
    Assert.AreEqual('Z', badge)