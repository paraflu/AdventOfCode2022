module Day6.Tests

open NUnit.Framework
open Day6.Part1

[<SetUp>]
let Setup () =
    ()

[<Test>]
let Test1 () =
    let stream = "mjqjpqmgbljsphdztnvjfqwrcgsmlb"

    let (marker, position) = get_marker stream 4 
    Assert.AreEqual("jpqm", marker)
    Assert.AreEqual(7, position)


[<Test>]
let Test2 () =
    let stream = "bvwbjplbgvbhsrlpgdmjqwftvncz"
    
    let (marker, position) = get_marker stream 4 
    Assert.AreEqual(5, position)

[<Test>]
let Test3 () =
    let stream = "nppdvjthqldpwncqszvftbrmjlhg"
    
    let (marker, position) = get_marker stream 4
    Assert.AreEqual(6, position)

[<Test>]
let Test4 () =
    let stream = "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg"
    
    let (marker, position) = get_marker stream 4
    Assert.AreEqual(10, position)

[<Test>]
let Test5 () =
    let stream = "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw"
    
    let (marker, position) = get_marker stream 4
    Assert.AreEqual(11, position)