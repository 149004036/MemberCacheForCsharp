using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MemberCacheForCsharp.Common.Utils
{
    public class WebClientUtils
    {
        private readonly static IDictionary<string, PropertyInfo[]> PropertyCached = new Dictionary<string, PropertyInfo[]>();

        /// <summary>
        /// get 请求
        /// </summary>
        /// <typeparam name="T">返回对象</typeparam>
        /// <param name="url">请求url</param>
        /// <param name="param">参数</param>
        /// <param name="headers">header</param>
        /// <returns></returns>
        public static T GetParamJson<T>(string url, IDictionary<string, string> param = null, IDictionary<string, string> headers = null)
        {
            T t = default(T);
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            using (HttpClient client = new HttpClient(handler))
            {
                if (headers != null)
                    foreach (var item in headers)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                if (param != null)
                {
                    int i = 0;
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in param)
                    {
                        sb.AppendFormat("{2}{0}={1}", item.Key, item.Value, (i == 0 ? "?" : "&"));
                    }
                    url += sb;
                    i++;
                }

                HttpResponseMessage responseMessage = client.GetAsync(url).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var str = responseMessage.Content.ReadAsStringAsync().Result;
                    t = JsonConvert.DeserializeObject<T>(str);
                }
            }

            return t;
        }

        /// <summary>
        /// get 请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="o"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static T GetParamJson<T>(string url, object o, IDictionary<string, string> headers = null)
        {
            T t = default(T);
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            using (HttpClient client = new HttpClient(handler))
            {
                if (headers != null)
                    foreach (var item in headers)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                if (o != null)
                {
                    Type type = o.GetType();
                    PropertyInfo[] pro = null;
                    string typeName = type.FullName;
                    if (PropertyCached.ContainsKey(typeName))
                    {
                        pro = PropertyCached[typeName];
                    }
                    else
                    {
                        pro = type.GetProperties();
                        PropertyCached.Add(typeName, pro);
                    }
                    StringBuilder sb = new StringBuilder();
                    if (pro != null)
                    {
                        int i = 0;
                        foreach (var propertyInfo in pro)
                        {
                            Type methodType = propertyInfo.PropertyType;
                            string param = propertyInfo.Name;
                            object v = ConvertParamToString(propertyInfo.GetValue(o));
                            if (v != null)
                            {
                                sb.AppendFormat("{2}{0}={1}", param, ConvertParamToString(v), (i == 0 ? "?" : "&"));
                                i++;
                            }
                        }
                    }
                    url += sb;
                }

                HttpResponseMessage responseMessage = client.GetAsync(url).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var str = responseMessage.Content.ReadAsStringAsync().Result;
                    t = JsonConvert.DeserializeObject<T>(str);
                }
            }

            return t;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private static string ConvertParamToString(object o)
        {
            string str = string.Empty;
            if (o == null)
                return str;
            if (o is DateTime)
            {
                DateTime tem = (DateTime)o;
                str = tem == DateTime.MinValue ? null : tem.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (o is int)
            {
                int tem = (int)o;
                str = tem.ToString();
            }
            else if (o is float)
            {
                float tem = (float)o;
                str = tem == 0 ? null : tem.ToString();
            }
            else if (o is double)
            {
                double tem = (double)o;
                str = tem == 0 ? null : tem.ToString();
            }
            else if (o is char)
            {
                char tem = (char)o;
                str = tem == 0 ? null : tem.ToString();
            }
            else if (o is bool)
            {
                bool tem = (bool)o;
                str = tem.ToString();
            }
            else if (o is string)
            {
                str = o.ToString();
            }
            return str;
        }

        /// <summary>
        /// get 请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static T GetJson<T>(string url, IDictionary<string, string> headers = null)
        {
            return GetParamJson<T>(url, null, headers);
        }

        /// <summary>
        /// post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static T PostJson<T>(string url, IDictionary<string, string> param, IDictionary<string, string> headers = null)
        {
            T t = default(T);
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            using (HttpClient client = new HttpClient(handler))
            {
                if (headers != null)
                    foreach (var item in headers)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                var content = new FormUrlEncodedContent(param);

                var response = client.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string str = response.Content.ReadAsStringAsync().Result;
                    t = JsonConvert.DeserializeObject<T>(str);
                }
            }
            return t;
        }

        public static string ObjectConvertParamToString(object o)
        {
            StringBuilder sb = new StringBuilder();
            if (o != null)
            {
                Type type = o.GetType();
                PropertyInfo[] pro = null;
                string typeName = type.FullName;
                if (PropertyCached.ContainsKey(typeName))
                {
                    pro = PropertyCached[typeName];
                }
                else
                {
                    pro = type.GetProperties();
                    PropertyCached.Add(typeName, pro);
                }
                if (pro != null)
                {
                    foreach (var propertyInfo in pro)
                    {
                        int i = 0;

                        sb.AppendFormat("{2}{0}={1}", propertyInfo.Name, ConvertParamToString(propertyInfo.GetValue(o)), (i == 0 ? "?" : "&"));
                        i++;
                    }
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 值from url
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static T PostUrlJson<T>(string url, object obj, IDictionary<string, string> headers = null)
        {
            T t = default(T);
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            using (HttpClient client = new HttpClient(handler))
            {
                if (headers != null)
                    foreach (var item in headers)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                string param = ObjectConvertParamToString(obj);
                var content = new StringContent("", System.Text.Encoding.UTF8, "application/json");

                var response = client.PostAsync(url + param, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string str = response.Content.ReadAsStringAsync().Result;
                    t = JsonConvert.DeserializeObject<T>(str);
                }
            }
            return t;
        }

        /// <summary>
        /// post body 请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static T PostBodyJson<T>(string url, object obj, IDictionary<string, string> headers = null)
        {
            T t = default(T);
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            using (HttpClient client = new HttpClient(handler))
            {
                if (headers != null)
                    foreach (var item in headers)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                string param = JsonConvert.SerializeObject(obj);
                var content = new StringContent(param, System.Text.Encoding.UTF8, "application/json");

                var response = client.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string str = response.Content.ReadAsStringAsync().Result;
                    t = JsonConvert.DeserializeObject<T>(str);
                }
            }
            return t;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <param name="fileNames"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static T PostFilesJson<T>(string url, object obj, List<string> fileNames, IDictionary<string, string> headers = null)
        {
            T t = default(T);
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            using (HttpClient client = new HttpClient(handler))
            {
                using (var content = new MultipartFormDataContent())//表明是通过multipart/form-data的方式上传数据  
                {
                    if (headers != null)
                        foreach (var item in headers)
                        {
                            client.DefaultRequestHeaders.Add(item.Key, item.Value);
                        }
                    var formDatas = GetFormDataByteArrayContent(obj);//获取键值集合对应的ByteArrayContent集合  GetNameValueCollection(null)
                    var files = GetFileByteArrayContent((fileNames != null ? new HashSet<string>(fileNames) : null));//获取文件集合对应的ByteArrayContent集合   GetHashSet(this.gv_File)
                    Action<List<ByteArrayContent>> act = (dataContents) =>
                    {
                        if (dataContents == null)
                            return;
                        foreach (var byteArrayContent in dataContents)
                        {
                            content.Add(byteArrayContent);
                        }
                    };
                    act(formDatas);
                    act(files);
                    var response = client.PostAsync(url, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string str = response.Content.ReadAsStringAsync().Result;
                        t = JsonConvert.DeserializeObject<T>(str);
                    }
                }
            }
            return t;

        }

        /// <summary>  
        /// 获取文件集合对应的ByteArrayContent集合  
        /// </summary>  
        /// <param name="files"></param>  
        /// <returns></returns>  
        private static List<ByteArrayContent> GetFileByteArrayContent(HashSet<string> files)
        {
            List<ByteArrayContent> list = new List<ByteArrayContent>();
            foreach (var file in files)
            {
                var fileContent = new ByteArrayContent(File.ReadAllBytes(file));
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = Path.GetFileName(file)
                };
                list.Add(fileContent);
            }
            return list;
        }

        /// <summary>  
        /// 获取键值集合对应的ByteArrayContent集合  
        /// </summary>  
        /// <param name="collection"></param>  
        /// <returns></returns>  
        private static List<ByteArrayContent> GetFormDataByteArrayContent(object obj)
        {
            List<ByteArrayContent> list = new List<ByteArrayContent>();
            if (obj == null)
                return list;
            string param = JsonConvert.SerializeObject(obj);
            var content = new StringContent(param, System.Text.Encoding.UTF8, "application/json");

            list.Add(content);
            return list;
        }

        /// <summary>  
        /// 获取键值集合对应的ByteArrayContent集合  
        /// </summary>  
        /// <param name="collection"></param>  
        /// <returns></returns>  
        private List<ByteArrayContent> GetFormDataByteArrayContent(NameValueCollection collection)
        {
            List<ByteArrayContent> list = new List<ByteArrayContent>();
            foreach (var key in collection.AllKeys)
            {
                var dataContent = new ByteArrayContent(Encoding.UTF8.GetBytes(collection[key]));
                dataContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    Name = key
                };
                list.Add(dataContent);
            }
            return list;
        }

        public static string PostXml(string url, string strPost)
        {
            string result = "";

            StreamWriter myWriter = null;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            objRequest.Method = "POST";
            //objRequest.ContentLength = strPost.Length;
            objRequest.ContentType = "text/xml";//提交xml 
                                                //objRequest.ContentType = "application/x-www-form-urlencoded";//提交表单
            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(strPost);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                myWriter.Close();
            }

            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                sr.Close();
            }
            return result;
        }
    }
}
