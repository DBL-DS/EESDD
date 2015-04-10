using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.CSV
{
    class CSVReader
    {
        FileStream stream;
        StreamReader reader;
        HeadersHolder headersHolder;
        string lineStr;
        string[] values;
        int cloumnsCount;
        bool readHeaderDone;            //强制必须先readHeader()

        public CSVReader(String filePath, Encoding encoding){
            stream = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            if (stream != null) {
                reader = new StreamReader(stream, encoding);                
            }
        }

        public CSVReader(String filePath)
        {
            stream = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            if (stream != null) {
                reader = new StreamReader(stream, Encoding.Default);
            }
        }
        /// <summary>
        /// 读取表头，并将表头内容存入一个HeadersHolder对象
        /// HeadersHolder对象中有一个Dictionary对象用来存放“键/值”对
        /// 键为 列名，值为 索引号（即列号）
        /// </summary>
        /// <returns></returns>
        public bool readHeader() {
            readHeaderDone = true;
            if (readRecord()) {
                if (headersHolder == null)
                {
                    headersHolder = new HeadersHolder();
                    headersHolder.headers = values;

                    int index = 0;
                    foreach (string header in headersHolder.headers)
                    {
                        headersHolder.IndexByName.Add(header, index++);
                    }
                    cloumnsCount = index;
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// 读取CSV文件中的一行内容，并将这一行字符串以","为分隔符切割 (循环调用直到返回值为false)
        /// </summary>
        /// <returns>读取一行成功，返回true，否则返回失败</returns>
        public bool readRecord() {
            if (reader != null && readHeaderDone) {
                if ((lineStr = reader.ReadLine()) != null)
                {
                    values = lineStr.Split(',');
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 根据列名，获得该行对应列的值
        /// </summary>
        /// <param name="cloumnName"></param>
        /// <returns></returns>
        public string get(string cloumnName) {
            return get(getIndex(cloumnName));
        }
        /// <summary>
        /// 根据索引，获取该行对应列的值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string get(int index) {
            if (index < cloumnsCount)
            {
                return values[index];                
            }
            return null;
        }
        /// <summary>
        /// 根据列名获取索引号
        /// </summary>
        /// <param name="cloumnName"></param>
        /// <returns></returns>
        public int getIndex(string cloumnName) {
            return headersHolder.IndexByName[cloumnName];
        }
        /// <summary>
        /// HeadersHolder对象中有一个Dictionary对象用来存放“键/值”对
        /// 键为 列名，值为 索引号（即列号）
        /// </summary>
        class HeadersHolder {
            public string[] headers = null;
            public Dictionary<string,int> IndexByName = new Dictionary<string,int>();

            public HeadersHolder() { 
            }
        }
    }
}
