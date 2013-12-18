namespace FeatureBee
{
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Web;

    public static class Tray
    {
        private static string LoadResource(string resourceName)
        {
            var content = new StringBuilder();
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static HtmlString RenderIcon()
        {
            // TOOD: Check if it is allowed to render (based on web.config)
            var full = new StringBuilder();
            full.AppendFormat("<style>{0}</style>", LoadResource("FeatureBee.TrayIcon.featureBeeTrayIcon.css"));
            full.Append(LoadResource("FeatureBee.TrayIcon.featureBeeTrayIcon.html"));
            full.AppendFormat(
                "<script type\"text/javascript\">{0}</script>",
                LoadResource("FeatureBee.TrayIcon.featureBeeTrayIcon.js"));

            return new HtmlString(full.ToString());
        }
    }
}