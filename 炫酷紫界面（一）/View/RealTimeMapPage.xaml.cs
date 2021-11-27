using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using 炫酷紫界面_一_.ViewModel;

namespace 炫酷紫界面_一_.View
{
    /// <summary>
    /// RealTimeMapPage.xaml 的交互逻辑
    /// </summary>
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public partial class RealTimeMapPage : Page
    {
        private string longitude = "120.3428";
        private string latitude = "30.31514";

        private bool openRealTimeCheck = true;
        private bool openRouteTrack = false;

        public RealTimeMapPage()
        {
            InitializeComponent();

            //消息标志token：ViewAlert，用于标识只阅读某个或者某些Sender发送的消息，并执行相应的处理，所以Sender那边的token要保持一致
            //执行方法Action：ShowReceiveInfo，用来执行接收到消息后的后续工作，注意这边是支持泛型能力的，所以传递参数很方便。
            Messenger.Default.Register<string>(this, "TransferMessage", ShowReceiveMapInfo);
            this.DataContext = new TransferViewModel();
            //卸载当前(this)对象注册的所有MVVMLight消息
            this.Unloaded += (sender, e) => Messenger.Default.Unregister(this);

            WebBrowserMap.Navigating += WebBrowserMap_Navigating;
        }

        private void ShowReceiveMapInfo(string msg)
        {
            string[] CurrentData = msg.Split(',');

            if (CurrentData.Length == 4 && CurrentData[0].Equals(":GPS"))
            {
                longitude = CurrentData[1];
                latitude = CurrentData[2];
            }
        }

        private void WebBrowserMap_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            SetWebBrowserSilent(sender as WebBrowser, true);
        }

        private void SetWebBrowserSilent(WebBrowser webBrowser, bool silent)
        {
            FieldInfo fi = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fi != null)
            {
                object browser = fi.GetValue(webBrowser);
                if (browser != null)
                {
                    browser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, browser, new object[] { silent });
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(RecordData);
            try
            {
                WebBrowserMap.Source = new Uri(Environment.CurrentDirectory + @"\html\baiDuMap.html");
                WebBrowserMap.ObjectForScripting = this;
            }
            catch (Exception)
            {

            }
        }

        private void RecordData()
        {
            while (true)
            {
                Thread.Sleep(5000);
                this.Dispatcher.Invoke(new Action(() =>
                {
                    openRouteTrack = (bool)OpenRouteTracking.IsChecked; //路线开关
                    openRealTimeCheck = (bool)OpenRealTimeMap.IsChecked; //启动定位开关
                }));


                if (openRealTimeCheck && IsFloat(longitude) && IsFloat(latitude))
                {
                    string lnglat = Change(Convert.ToDouble(latitude), Convert.ToDouble(longitude));
                    string[] str = lnglat.Split(',');

                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        try
                        {
                            if (openRouteTrack && lnglat != null)
                            {
                                WebBrowserMap.InvokeScript("ListLocation", new object[] { Convert.ToDouble(str[1]), Convert.ToDouble(str[0]) });
                            }
                            else
                            {
                                WebBrowserMap.InvokeScript("theLocation", new object[] { Convert.ToDouble(longitude), Convert.ToDouble(latitude) });
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }));
                }
            }
        }

        // 通过GET方式发送数据
        public string SendDataByGET(string Url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        public static bool IsFloat(string str)
        {
            if (str == "")
            {
                return false;
            }
            string regextext = @"^(-?\d+)(\.\d+)?$";
            Regex regex = new Regex(regextext, RegexOptions.None);
            return regex.IsMatch(str.Trim());
        }

        // 把google/GPS坐标转换成百度地图坐标
        private string Change(double y, double x)
        {
            try
            {
                string path = "http://api.map.baidu.com/ag/coord/convert?from=0&to=4&x=" + x + "+&y=" + y + "&callback=BMap.Convertor.cbk_7594";
                string res = SendDataByGET(path);
                if (res.IndexOf("(") > 0 && res.IndexOf(")") > 0)
                {
                    int sint = res.IndexOf("(") + 1;
                    int eint = res.IndexOf(")");
                    int ls = res.Length;
                    string str = res.Substring(sint, eint - sint);
                    int errint = res.IndexOf("error") + 7;
                    int enderr = res.IndexOf("error") + 8;
                    string err = res.Substring(errint, 1);
                    if ("0".Equals(err))
                    {
                        int sx = str.IndexOf(",\"x\":\"") + 6;
                        int sy = str.IndexOf("\",\"y\":\"");
                        int endy = str.IndexOf("\"}");
                        int sl = str.Length;
                        string xp = str.Substring(sx, sy - sx);
                        string yp = str.Substring(sy + 7, endy - sy - 7);
                        byte[] outputb = Convert.FromBase64String(xp);
                        string XStr = Encoding.Default.GetString(outputb);
                        outputb = Convert.FromBase64String(yp);
                        string YStr = Encoding.Default.GetString(outputb);
                        return YStr + "," + XStr;
                    }
                }
            }
            catch (Exception)
            {

            }
            return null;
        }

        private void ClearAllButton_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserMap.InvokeScript("clearAll");
        }

        private void GetDistanceButton_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserMap.InvokeScript("openGetDistance");
        }
    }
}
