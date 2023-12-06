[<AutoOpen>]
module aoc.common.Reads

open System.Text.RegularExpressions

let readTokens (line: string) splitpattern =
    Regex
        .Replace(line, splitpattern, " ")
        .Trim()
        .Split(' ')

let readNumbers (line: string) =
    readTokens line @"[^\-\d]+" |> Array.map int64

let readNonNumbers (line: string) = readTokens line @"[\d]+"

let readNumber (line: string) =
    ((readTokens line @"[^\-\d]+") |> Array.map int64)[0]

let readMatrixOfNumbers (lines: string []) = lines |> Array.map readNumbers
