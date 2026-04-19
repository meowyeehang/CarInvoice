using Android.Content;
using Android.Print;
using Microsoft.Maui.ApplicationModel;

namespace RongCar; // <--- MAKE SURE THIS MATCHES YOUR PROJECT NAME

public class AndroidPrintService
{
    public void PrintHtml(string html)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            var context = Platform.CurrentActivity;
            var printManager = (PrintManager)context.GetSystemService(Context.PrintService);
            
            var webView = new Android.Webkit.WebView(context);
            webView.SetWebViewClient(new Android.Webkit.WebViewClient());
            
            webView.LoadDataWithBaseURL(null, html, "text/html", "UTF-8", null);

            var printAdapter = webView.CreatePrintDocumentAdapter("Rong Motor Invoice");
            printManager.Print("Rong Motor Invoice", printAdapter, null);
        });
    }
}