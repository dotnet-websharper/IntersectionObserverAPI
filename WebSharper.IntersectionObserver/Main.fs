namespace WebSharper.IntersectionObserver

open WebSharper
open WebSharper.JavaScript
open WebSharper.InterfaceGenerator

module Definition =

    let IntersectionObserverEntry =
        Class "IntersectionObserverEntry"
        |+> Instance [
            "boundingClientRect" =? T<DOMRectReadOnly>
            "intersectionRatio" =? T<float>
            "intersectionRect" =? T<DOMRectReadOnly>
            "isIntersecting" =? T<bool>
            "rootBounds" =? T<DOMRectReadOnly>
            "target" =? T<Dom.Element>
            "time" =? T<float>
        ]

    let rootType = T<Dom.Document> + T<Dom.Element>

    let IntersectionObserverOptions =
        Pattern.Config "IntersectionObserverOptions" {
            Required = []
            Optional = [
                "root", rootType
                "rootMargin", T<string>
                "threshold", T<float> + !| T<float>            
            ]
        }

    let IntersectionObserver =
        let IntersectionObserverCallback = (!|IntersectionObserverEntry)?entries * !?TSelf?observer ^-> T<unit>

        Class "IntersectionObserver"
        |+> Static [
            Constructor (IntersectionObserverCallback?callback * !?IntersectionObserverOptions?options)
        ]
        |+> Instance [
            "root" =? rootType
            "rootMargin" =? T<string>
            "thresholds" =? T<float[]>

            "observe" => T<Dom.Element>?target ^-> T<unit>
            "unobserve" => T<Dom.Element>?target ^-> T<unit>
            "disconnect" => T<unit> ^-> T<unit>
            "takeRecords" => T<unit> ^-> !| IntersectionObserverEntry
        ]

    let Assembly =
        Assembly [
            Namespace "WebSharper.IntersectionObserver" [
                IntersectionObserver
                IntersectionObserverOptions
                IntersectionObserverEntry
            ]
        ]

[<Sealed>]
type Extension() =
    interface IExtension with
        member ext.Assembly =
            Definition.Assembly

[<assembly: Extension(typeof<Extension>)>]
do ()
