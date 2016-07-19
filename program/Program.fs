open StarWars.API
open System

[<EntryPoint>]
let main argv = 
    let numberOfStarships = getAllStarships().Length

    printfn "All Starships:"
    let allStarships = getAllStarships()
     
    let starshipType x =
        match x with
        | "unknown" -> "unknown"
        | "1" | "2" | "3" -> "transport"
        | _ -> "battle station"

    allStarships
    |> Seq.groupBy(fun s -> s.Manufacturer)
    |> Seq.sortBy(fun (manufacturer, starships) -> (starships |> Seq.length) * -1)
    |> Seq.iter (fun (manufacturer, starships) -> 
                     printfn "%s - %i"  manufacturer (starships |> Seq.length)
                  )

    printfn ""

    let allPeople = getAllPeople()

    let filmsByUrl = 
        getAllFilms() 
        |> Array.map (fun film -> (film.Url,film)) 
        |> Map.ofArray

//    let firstFilm (person:Person.Root) = 
//        person.films
//        |>Array.map (fun filmUrl -> filmsByUrl.[filmUrl])
//        |>Array.min(fun film -> film.EpisodeId)

    allPeople
        |> Array.iter (fun person -> 
            let firstFilm = 
                (person.Films
                |>Array.map (fun filmUrl -> filmsByUrl.[filmUrl])
                |>Array.sortBy(fun film -> film.EpisodeId)).[0]
            printfn "%s first appears in: %s" person.Name firstFilm.Title)


    // allPeople.ForEach(p => Console.Writeline(p.Name));

    // public void PrintName(Person p) {....}
    // allPeople.ForEach(PrintName);



    Console.ReadKey()|> ignore
    0 // return an integer exit code