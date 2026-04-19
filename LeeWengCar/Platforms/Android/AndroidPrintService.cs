using Android.Content;
using Android.Print;
using Microsoft.Maui.ApplicationModel;

namespace LeeWengCar; // <--- MAKE SURE THIS MATCHES YOUR PROJECT NAME

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

            var printAdapter = webView.CreatePrintDocumentAdapter("Lee Weng Motor Invoice");
            printManager.Print("Lee Weng Motor Invoice", printAdapter, null);
        });
    }
}