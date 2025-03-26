# WebSharper Intersection Observer API Binding

This repository provides an F# [WebSharper](https://websharper.com/) binding for the [Intersection Observer API](https://developer.mozilla.org/en-US/docs/Web/API/Intersection_Observer_API), enabling WebSharper applications to efficiently observe element visibility within a viewport.

## Repository Structure

The repository consists of two main projects:

1. **Binding Project**:

   - Contains the F# WebSharper binding for the Intersection Observer API.

2. **Sample Project**:
   - Demonstrates how to use the Intersection Observer API with WebSharper syntax.
   - Includes a GitHub Pages demo: [View Demo](https://dotnet-websharper.github.io/IntersectionObserverAPI/).

## Installation

To use this package in your WebSharper project, add the NuGet package:

```bash
   dotnet add package WebSharper.IntersectionObserver
```

## Building

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.

### Steps

1. Clone the repository:

   ```bash
   git clone https://github.com/dotnet-websharper/IntersectionObserver.git
   cd IntersectionObserver
   ```

2. Build the Binding Project:

   ```bash
   dotnet build WebSharper.IntersectionObserver/WebSharper.IntersectionObserver.fsproj
   ```

3. Build and Run the Sample Project:

   ```bash
   cd WebSharper.IntersectionObserver.Sample
   dotnet build
   dotnet run
   ```

4. Open the hosted demo to see the Sample project in action:
   [https://dotnet-websharper.github.io/IntersectionObserverAPI/](https://dotnet-websharper.github.io/IntersectionObserverAPI/)

## Example Usage

Below is an example of how to use the Intersection Observer API in a WebSharper project:

```fsharp
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

    // Function to observe an element's visibility changes
    let observeElement () =
      // Select the element with the class "observer-box"
      let targetBox = JS.Document.QuerySelector(".observer-box")

      // Create a new IntersectionObserver instance with a callback function
      let observer = new IntersectionObserver((fun (entries: IntersectionObserverEntry array) ->
         printfn("for each")
         for entry in entries do
               // Log the observed element and its visibility status
               printfn($"Observed: {entry.Target}, isIntersecting: {entry.IsIntersecting}")
               if (entry.IsIntersecting) then
                  // If the element is visible, add the "visible" class
                  printfn("Add visible")
                  entry.Target.ClassList.Add("visible")
               else
                  // If the element is not visible, remove the "visible" class
                  printfn("Remove visible")
                  entry.Target.ClassList.Remove("visible")
      ), IntersectionObserverOptions(Threshold = 0.5))

      // Start observing the target element
      observer.Observe(targetBox)

    [<SPAEntryPoint>]
    let Main () =
        // Initialize the UI template
        IndexTemplate.Main()
            .PageInit(fun () ->
               observeElement()
            )
            .Doc()
        |> Doc.RunById "main"
```

This example demonstrates how to observe and react to changes in the visibility of an element using the Intersection Observer API in WebSharper.

## Important Considerations

- **Performance Efficiency**: Intersection Observer is optimized for performance, reducing the need for frequent polling.
- **Threshold Values**: The threshold determines the visibility percentage required to trigger a callback.
- **Limited Browser Support**: Some older browsers do not support the Intersection Observer API; check [MDN Intersection Observer API](https://developer.mozilla.org/en-US/docs/Web/API/Intersection_Observer_API) for the latest compatibility details.
