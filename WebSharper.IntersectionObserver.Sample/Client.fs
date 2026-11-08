namespace WebSharper.IntersectionObserver.Sample

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Notation
open WebSharper.UI.Client
open WebSharper.UI.Templating
open WebSharper.IntersectionObserver

[<JavaScript>]
module Client =
    // The template is loaded from the DOM, so editing index.html and refreshing
    // the browser is enough; a rebuild is only needed when holes change.
    type IndexTemplate = Template<"wwwroot/index.html", ClientLoad.FromDocument>

    let status = Var.Create "Scroll down to see the effect."

    let observeElement () =
        let target = JS.Document.QuerySelector(".observer-box")

        let observer =
            new IntersectionObserver(
                (fun (entries: IntersectionObserverEntry array) ->
                    for entry in entries do
                        if entry.IsIntersecting then
                            entry.Target.ClassList.Add("visible")
                            status := "The observed box is visible."
                        else
                            entry.Target.ClassList.Remove("visible")
                            status := "The observed box is out of view."
                ),
                IntersectionObserverOptions(Threshold = 0.5)
            )

        observer.Observe(target)

    [<SPAEntryPoint>]
    let Main () =
        IndexTemplate()
            .PageInit(fun () -> observeElement ())
            .status(status.V)
            .Bind()
