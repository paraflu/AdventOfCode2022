module Day7.Tests

open System
open System.IO
open NUnit.Framework
open Microsoft.FSharpLu
open Day7.Part1

[<SetUp>]
let Setup () =
    ()

[<Test>]
let Test1 () =

    let sizes = 
        File.ReadAllLines "../../../day7.testdata.txt"
        |> Array.toList
        |> calculateSizes 
    
    //let root = File.ReadAllLines "../../../day7.testdata.txt" 
    //            |> Array.toList 
    //            |> List.tail  // salto la prima riga con il cd /
    //            |> makeRoot "/"


    //let eSize = "/a/e".Split("/") 
    //            |> Array.toList 
    //            |> getSize root

    Assert.AreEqual(584, sizes[["e";"a";"/"]])


    //let aSize = "/a".Split("/") 
    //            |> Array.toList 
    //            |> getSize root
    //Assert.AreEqual(94853, aSize);

    //let dSize = "/d".Split("/") 
    //            |> Array.toList 
    //            |> getSize root
    //Assert.AreEqual(24933642, dSize);

    //let rootSize = "/".Split("/") 
    //            |> Array.toList 
    //            |> getSize root
    //Assert.AreEqual(48381165, rootSize);

