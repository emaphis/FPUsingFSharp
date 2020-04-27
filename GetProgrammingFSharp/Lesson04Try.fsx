// Lesson 4 "You Try"  - 55. 56

// Listing 4.8 - Create a form to display a web page
open System
open System.Net
open System.Windows.Forms

let openURI uri =
    let browser =
        let webClient = new WebClient()
        let website = webClient.DownloadString(Uri uri)
        new WebBrowser(ScriptErrorsSuppressed = true,
                       Dock = DockStyle.Fill,
                       DocumentText = website)
    
    let form = new Form(Text = "Hello from F#")
    form.Controls.Add browser
    form.Show()

openURI "https://fsharp.org"
