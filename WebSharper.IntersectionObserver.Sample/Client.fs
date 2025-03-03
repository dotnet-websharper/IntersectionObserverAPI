namespace WebSharper.IntersectionObserver.Sample

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Templating
open WebSharper.IntersectionObserver

[<JavaScript>]
module Client =
    // The templates are loaded from the DOM, so you just can edit index.html
    // and refresh your browser, no need to recompile unless you add or remove holes.
    type IndexTemplate = Template<"wwwroot/index.html", ClientLoad.FromDocument>

    let observeElement (targetBox: Dom.Element) = 

        let observer = new IntersectionObserver((fun (entries: IntersectionObserverEntry array) -> 
            printfn("for each")
            for entry in entries do
                printfn($"Observed: {entry.Target}, isIntersecting: {entry.IsIntersecting}")
                if (entry.IsIntersecting) then
                    printfn("Add visible")
                    entry.Target.ClassList.Add("visible")
                else
                    printfn("Remove visible")
                    entry.Target.ClassList.Remove("visible")
        ), IntersectionObserverOptions(Threshold = 0.5))

        observer.Observe(targetBox)

    [<SPAEntryPoint>]
    let Main () =
        let targetBox = JS.Document.QuerySelector(".observer-box")

        if not (isNull targetBox) then
            observeElement(targetBox)

        IndexTemplate.Main()
            .Doc()
        |> Doc.RunById "main"
