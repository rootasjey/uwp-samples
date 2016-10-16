using System.Text.RegularExpressions;
using Windows.ApplicationModel.DataTransfer;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.UI.Notifications;

namespace uwpsamples.Controllers {
    public class Common {
        private static object _sharedObj;
        private static DataTransferManager _dataTransferManager;

        /// <summary>
        /// Open the Share UI with the quote's data (share on twitter, facebook, sms, ...)
        /// </summary>
        /// <param name="obj">The quote to share</param>
        public static void share(object obj) {
            // If the user clicks the share button, invoke the share flow programatically.
            _sharedObj = obj;
            DataTransferManager.ShowShareUI();
        }

        public static void RegisterForShare() {
            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager,
                DataRequestedEventArgs>(ShareTextHandler);
        }

        public static void ShareTextHandler(DataTransferManager sender, DataRequestedEventArgs e) {
            string text = _sharedObj.ToString();

            DataRequest request = e.Request;
            request.Data.Properties.Title = "My App";
            request.Data.Properties.Description = "Share something";
            request.Data.SetText(text);
        }

        /// <summary>
        /// Copy the quote's content to the clipboard
        /// </summary>
        /// <param name="obj">The quote's content to copy</param>
        public void Copy(object obj) {
            DataPackage dataPackage = new DataPackage();
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            dataPackage.SetText("sample text");
            Clipboard.SetContent(dataPackage);
        }
        
        /// <summary>
        /// Update the application's tile with the most recent quote
        /// </summary>
        /// <param name="obj">The quote's content to update the tile with</param>
        public static void UpdateTile(object obj) {
            XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text09);
            XmlNodeList tileTextAttributes = tileXml.GetElementsByTagName("text");

            tileTextAttributes[0].InnerText = "First line";
            tileTextAttributes[1].InnerText = "Second line";

            // Square tile
            XmlDocument squareTileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text02);
            XmlNodeList squareTileTextAttributes = squareTileXml.GetElementsByTagName("text");
            //squareTileTextAttributes[0].AppendChild(squareTileXml.CreateTextNode("Hello World! My very own tile notification"));
            squareTileTextAttributes[0].InnerText = "First line";
            squareTileTextAttributes[1].InnerText = "Second line";

            // Integration of the two tile templates
            IXmlNode node = tileXml.ImportNode(squareTileXml.GetElementsByTagName("binding").Item(0), true);
            tileXml.GetElementsByTagName("visual").Item(0).AppendChild(node);

            // Tile Notification
            TileNotification tileNotification = new TileNotification(tileXml);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }

        /// <summary>
        /// Normalize the text:
        /// Remove the html tags (<h1></h1>, ...), ans special chars (&amp;)
        /// </summary>
        /// <param name="text">Normalize</param>
        public static string DeleteHTMLTags(string text) {
            if (text == null) {
                return null;
            }

            text = Regex.Replace(text, @"<(.|\n)*?>", string.Empty);

            text = text
                .Replace("&laquo;", "")
                .Replace("&ldquo;", "")
                .Replace("&rdquo;", "")
                .Replace("&nbsp;", "")
                .Replace("&raquo;", "")
                .Replace("&#039;", "'")
                .Replace("&quot;", "'")
                .Replace("&amp;", "&")
                .Replace("[+]", "")
                .Replace("[", "")
                .Replace("]", "")
                .Replace("\n", "")
                .Replace("  ", "");

            return text;
        }
    }
}
