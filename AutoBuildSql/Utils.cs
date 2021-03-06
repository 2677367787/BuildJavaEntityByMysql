﻿/***************************************************** 
** 命名空间：AutoBuildSql
** 文件名称：Utils
** 内容简述： 
** 版　　本：V1.0 
** 作　　者： 
** 创建日期：2018/3/12 22:44:56
** 修改记录： 
*****************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AutoBuildSql
{
    public class Utils
    {
        /// <summary>
        /// 绑定数据到下拉框
        /// </summary>
        /// <param name="cb">下拉框对象</param>
        /// <param name="list">数据源</param>
        public static void BinderComboBox<T>(ComboBox cb, IList<T> list)
        {
            BindingSource bs = new BindingSource {DataSource = list};
            cb.DataSource = bs; 
        }

        public static bool IsNumeric(string value)
        {
            return Regex.IsMatch(value, @"^-?[1-9]\d*\.?\d*|-?0\.\d*[1-9]\d*$");
        }

        public static string GetRandomString(int length)
        {
            return GetRandomString(length, true, true, false, false, "");
        }

        public static void BinderComboBox(ComboBox cb, IDictionary<object, object> dic)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dic;
            cb.DataSource = bs;
            cb.DisplayMember = "Value";
            cb.ValueMember = "Key";
        }

        #region 5.0 生成随机字符串
        ///<summary>
        ///生成随机字符串 
        ///</summary>
        ///<param name="length">目标字符串的长度</param>
        ///<param name="useNum">是否包含数字，1=包含，默认为包含</param>
        ///<param name="useLow">是否包含小写字母，1=包含，默认为包含</param>
        ///<param name="useUpp">是否包含大写字母，1=包含，默认为包含</param>
        ///<param name="useSpe">是否包含特殊字符，1=包含，默认为不包含</param>
        ///<param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
        ///<returns>指定长度的随机字符串</returns>
        public static string GetRandomString(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;
            if (useNum) { str += "0123456789"; }
            if (useLow) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }
            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }
        #endregion

        private static IList<int> GetStrIndex(string str)
        {
            IList<int> list = new List<int>();
            char[] strChar = str.ToCharArray();
            for (int i = 0; i < strChar.Length; i++)
            {
                if (strChar[i] > 'A' && strChar[i] < 'Z')
                {
                    list.Add(i);
                }
            }
            return list;
        }

        public static string ConvertToSqlFiledRule(string str)
        {
            IList<int> indexs = GetStrIndex(str);
            IList<string> result = new List<string>();
            char[] strChar = str.ToCharArray();
            for (int i = 0; i < strChar.Length; i++)
            {
                if (indexs.Contains(i))
                {
                    result.Add("_" + strChar[i].ToString().ToLower());
                }
                else
                {
                    result.Add(strChar[i].ToString());
                }
            }
            return string.Join("", result);
        }

        public static string GetFieldSource(DataTable dt,string field,string tables)
        {
            string where = string.Format("COLUMN_NAME='{0}' and TABLE_NAME in('{1}'))", field, tables);
            DataRow[] dr = dt.Select(where);
            return dr[0][""].ToString();
        }

        public static string GetFieldValue(DataSet ds, string tabName, string field)
        {
            return ds.Tables[tabName].Rows[0][field].ToString();
        }
    }
}
