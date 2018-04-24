namespace PeopleDb

open WebSharper
open WebSharper.Sitelets
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Html

[<JavaScript>]
module Client =

    let Query<'Result> (endpoint: Api.EndPoint) =
        async {
            let! text = Router.Ajax Api.Router endpoint
            return Json.Deserialize<Api.Result<'Result>> text
        }

    let Main (initPeople: (int * Api.PersonData)[]) =
        let people = ListModel.Create fst initPeople

        
        let rvInput = Var.Create ""
        let submit = Submitter.CreateOption rvInput.View
        let vReversed =
            submit.View.MapAsync(function
                | None -> async { return "" }
                | Some input -> Server.DoSomething input
            )
        div [] [
            Doc.Input [] rvInput
            Doc.Button "Send" [] submit.Trigger
            hr [] []
            h4 [attr.``class`` "text-muted"] [text "The server responded:"]
            div [attr.``class`` "jumbotron"] [h1 [] [textView vReversed]]
        ]
