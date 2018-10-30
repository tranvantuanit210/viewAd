using System;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
namespace ViewAD
{
    public partial class frmMain : Form
    {
        public ChromiumWebBrowser chromeBrowser;
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Start the browser after initialize global component
            InitializeChromium();
        }

        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings();
            // Initialize cef with the provided settings
            settings.CefCommandLineArgs.Add("proxy-server", "200.29.191.149:3128");
            settings.UserAgent = "My/Custom/User-Agent-AndStuff";
            Cef.Initialize(settings);
            // Create a browser component
            chromeBrowser = new ChromiumWebBrowser("https://www.google.com");
            // Add it to the form and fill it to the form window.
            this.pContainer.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;
            chromeBrowser.AddressChanged += Chrome_AddressChanged;
        }

        private void Chrome_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            this.Invoke(new MethodInvoker(() => { txtUrl.Text = e.Address; }));
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            chromeBrowser.Load(txtUrl.Text);
        }
    }
}
