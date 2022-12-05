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

setForeground Colors.Yellow
printf "Premere invio per terminare"
let c = System.Console.ReadLine 

AnsiColor.resetColor()
