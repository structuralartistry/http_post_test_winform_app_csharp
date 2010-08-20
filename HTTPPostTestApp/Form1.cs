using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Net;

namespace HTTPPostTestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string RequestHttpPost(string URI, string PostData)
        {
            try
            {
                string strResponse = string.Empty;
                WebRequest webRequest = null;
                WebResponse webResponse = null;
                StreamReader streamReader = null;

                try
                {
                    webRequest = WebRequest.Create(URI);

                    //req.Proxy = WebProxy.GetDefaultProxy(); // Enable if using proxy

                    webRequest.Method = "POST";        // Post method

                    webRequest.ContentType = "text/xml";     // content type

                    // Wrap the request stream with a text-based writer
                    StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());

                    // Write the post data into the stream
                    streamWriter.WriteLine(PostData);
                    streamWriter.Close();
                    // Send the data to the webserver
                    webResponse = webRequest.GetResponse();

                    //output the response to string
                    streamReader = new StreamReader(webResponse.GetResponseStream());
                    strResponse = streamReader.ReadToEnd();
                    return strResponse;
                }
                catch (WebException WebAppErr)
                {
                    return WebAppErr.ToString();
                }
                catch (Exception AppErr)
                {
                    return AppErr.ToString();
                }
                finally
                {
                    if (webRequest != null) webRequest.GetRequestStream().Close();
                    if (webResponse != null) webResponse.GetResponseStream().Close();
                    if (streamReader != null) streamReader.Close();
                }
            }
            catch (Exception AppErr)
            {
                return AppErr.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.txtResponse.Text = RequestHttpPost(this.txtURL.Text, this.txtPost.Text);
        }
    }
}
