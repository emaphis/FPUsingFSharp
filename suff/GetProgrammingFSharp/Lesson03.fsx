/// 3.3.1  Creating scripts in F#

let text = "Hello, world"
text.Length

/// 3.3.3 Woring with functions in scripts

let greetPerson name age =
    sprintf "Hello, %s. Your are %d years old" name age

let greeting = greetPerson "Fred" 25

/// Do this

// count the words in a string
let countWords(text : string) =
    let count = (text.Split ([|' '|])).Length
    count

let cnt = countWords("ab cd ef")
