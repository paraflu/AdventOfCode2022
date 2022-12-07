open System.IO
open AnsiColor

let day1_result =
    File.ReadAllText "../../../day1.realdata.txt"
    |> Day1.Main.caloriesList
    |> Seq.max

setForeground Colors.Green
printf "[*] Day1 : %d\n" day1_result

let day2_result =
    File.ReadAllText "../../../day2.realdata.txt"
    |> Day2.Main.game
    |> Seq.sum

printf "[*] Day2 : %d\n" day2_result


let day2_part2_result =
    File.ReadAllText "../../../day2_part2.realdata.txt"
    |> Day2.Part2.game
    |> Seq.sum

printf "[*] Day2 (part2) : %d\n" day2_part2_result

let day3_result =
    File.ReadAllText "../../../day3.realdata.txt"
    |> Day3.Part1.solve

printf "[*] Day3 : %d\n" day3_result


let day3_part2_result =
    File.ReadAllText "../../../day3_part2.realdata.txt"
    |> Day3.Part2.solve

printf "[*] Day3 (part2) : %d\n" day3_part2_result

let day4_result =
    File.ReadAllText "../../../day4.realdata.txt"
    |> Day4.Part1.solve

printf "[*] Day4 : %d\n" day4_result

let day4_part2_result =
    File.ReadAllText "../../../day4.realdata.txt"
    |> Day4.Part2.solve

printf "[*] Day4 (part2): %d\n" day4_part2_result

let day5_result =
    File.ReadAllText "../../../day5.realdata.txt"
    |> Day5.Part1.solve

day5_result |> Day5.Part1.getHeader |> Seq.map string |> String.concat ""|> printf "[*] Day5 : %s\n" 


let day5_part2_result =
    File.ReadAllText "../../../day5.realdata.txt"
    |> Day5.Part2.solve

day5_part2_result |> Day5.Part1.getHeader |> Seq.map string |> String.concat ""|> printf "[*] Day5 (part2) : %s\n" 

let day6_result =
    File.ReadAllText "../../../day6.realdata.txt"

let (_, m) = Day6.Part1.get_marker day6_result 4 
printf "[*] Day6 : %d\n" m
let (_, m2) = Day6.Part1.get_marker day6_result 14 
printf "[*] Day6 (part2): %d\n" m2


setForeground Colors.Yellow
printf "Premere invio per terminare"
let c = System.Console.ReadLine 

AnsiColor.resetColor()
