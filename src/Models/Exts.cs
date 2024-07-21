using Octokit;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiary.Models {
  public static class Ext {

    /// <summary>
    /// async read file from file system into string
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static async Task<string> ReadAllTextAsync(this string filePath) {
      using (var streamReader = new StreamReader(filePath)) {
        return await streamReader.ReadToEndAsync();
      }
    }

    /// <summary>
    /// async write content to fileName location on file system. 
    /// </summary>
    /// <param name="Content"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static async Task<int> WriteAllTextAsync(this string Content, string fileName) {
      using (var streamWriter = new StreamWriter(fileName)) {
        await streamWriter.WriteAsync(Content);
      }
      return 1;
    }
    /// <summary>
    /// Remove all instances of CToRemove from content
    /// </summary>
    /// <param name="content"></param>
    /// <param name="CToRemove"></param>
    /// <returns></returns>    
    public static string RemoveChar(this string content, char CToRemove) {
      string text = content;
      while (text.Contains(CToRemove)) {
        text = text.Remove(text.IndexOf(CToRemove), 1);
      }

      return text;
    }
    public static string MakeIndentSpace(int depth, string space) {
      string ret = "";
      if (depth > 0) {
        int CountDown = depth;
        while (CountDown > 0) {
          ret += space;
          CountDown--;
        }
      }
      return ret;
    }
    /// <summary>
    ///   Splits content on each character in delims string returns string[]
    /// </summary>
    /// <param name="content"></param>
    /// <param name="delims"></param>
    /// <returns></returns>
    public static string[] Parse(this string content, string delims) {
      return content.Split(delims.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
    }

    /// <summary>
    /// Splits contents by delims and takes first string
    /// </summary>
    /// <param name="content"></param>
    /// <param name="delims"></param>
    /// <returns></returns>
    public static string ParseFirst(this string content, string delims) {
      string[] sr = content.Parse(delims);
      if (sr.Length > 0) {
        return sr[0];
      }
      return "";
    }

    /// <summary>
    /// Splits contents by delims and takes last string
    /// </summary>
    /// <param name="content"></param>
    /// <param name="delims"></param>
    /// <returns></returns>
    public static string ParseLast(this string content, string delims) {
      string[] sr = content.Parse(delims);
      if (sr.Length > 0) {
        return sr[sr.Length - 1];
      }
      return "";
    }

     
    /// <summary>
    /// byte[] to utf8 string; use AsString() to reverse
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static byte[] AsBytes(this string text) {
      return Encoding.UTF8.GetBytes(text);
    }

    /// <summary>
    /// Adds AsString support for byte[] 
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string AsString(this byte[] bytes) {
      return Encoding.UTF8.GetString(bytes);
    }

    /// <summary>
    ///     Base 64 encodes string variant uses ? as fillers instead of = for inifiles.
    /// </summary>
    /// <param name="Text"></param>
    /// <returns></returns>
    public static string AsBase64Encoded(this string Text) {
      return Convert.ToBase64String(Encoding.UTF8.GetBytes(Text)).Replace('=', '?');
    }

    /// <summary>
    /// Base 64 decodes string variant uses converts ? back to = as fillers for inifiles.
    /// </summary>
    /// <param name="Text"></param>
    /// <returns></returns>    
    public static string AsBase64Decoded(this string Text) {
      if (string.IsNullOrEmpty(Text)) return "";
      byte[] bytes = Convert.FromBase64String(Text.Replace('?', '='));
      return Encoding.UTF8.GetString(bytes);
    }
    /// <summary>
    /// lower case first letter of content concat with remainder.
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>    
    public static string AsLowerCaseFirstLetter(this string content) {
      if (string.IsNullOrEmpty(content)) return "";
      return content.Substring(0, 1).ToLower() + content.Substring(1);
    }
    /// <summary>
    /// Uppercase first letter of content concat with rest of content.
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public static string AsUpperCaseFirstLetter(this string content) {
      if (string.IsNullOrEmpty(content)) return "";
      return content.Substring(0, 1).ToUpper() + content.Substring(1);
    }
    public static string AsStr1P(this decimal x) {
      return String.Format(CultureInfo.InvariantCulture, "{0:0.0}", x);
    }

  }

  public static class GitExt {
    public static string AsString(this RateLimit rate, string Name) {
      return $"{Name}:{rate.Remaining} of {rate.Limit} per hour";
    }
  }


}
