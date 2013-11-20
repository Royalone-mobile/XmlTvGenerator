﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml.Linq;
using XmlTvGenerator.Core.Translator.Cache;

namespace XmlTvGenerator.Core.Translator
{
    public class GoogleTranslator : TranslatorBase
    {
        Dictionary<Language, string> _languageDict;

        public GoogleTranslator(CacheManagerBase cache) : base(cache)
        {
            _languageDict = GetGoogleLanguageDict();
        }

        public override string Translate(Language from, Language to, string text)
        {
            var cacheText = Cache.Get(from, to, text);
            if (cacheText != null)
                return cacheText;
            Uri address = new Uri("http://translate.google.com/translate_t");
            var web = new WebClient();
            web.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

            var postData = string.Format("hl=en&ie=UTF8&oe=UTF8submit=Translate&langpair={0}|{1}&text={2}", _languageDict[from], _languageDict[to], HttpUtility.UrlEncode(text));

            var res = web.UploadString(address, postData);

            var doc = new HtmlDocument();
            doc.LoadHtml(res);            
            var span = doc.DocumentNode.Descendants("span").Where(x => x.Attributes["id"] != null && x.Attributes["id"].Value == "result_box").FirstOrDefault();
            var val = span != null ? span.InnerText : null;
            if (val != null)
                Cache.Add(from, to, text, val);
            return val;
        }

        Dictionary<Language, string> GetGoogleLanguageDict()
        {
            var dict = new Dictionary<Language, string>();
            dict[Language.Esperanto] = "eo";
            dict[Language.English] = "en";
            dict[Language.Dutch] = "nl";
            dict[Language.Danish] = "da";
            dict[Language.Czech] = "cs";
            dict[Language.Croatian] = "hr";
            dict[Language.Chinese] = "zh-CN";
            dict[Language.Catalan] = "ca";
            dict[Language.Bulgarian] = "bg";
            dict[Language.Bengali] = "bn";
            dict[Language.Belarusian] = "be";
            dict[Language.Basque] = "eu";
            dict[Language.Azerbaijani] = "az";
            dict[Language.Armenian] = "hy";
            dict[Language.Arabic] = "ar";
            dict[Language.Albanian] = "sq";
            dict[Language.Indonesian] = "id";
            dict[Language.Icelandic] = "is";
            dict[Language.Hungarian] = "hu";
            dict[Language.Hindi] = "hi";
            dict[Language.Hebrew] = "iw";
            dict[Language.Haitian_Creole] = "ht";
            dict[Language.Greek] = "el";
            dict[Language.Georgian] = "ka";
            dict[Language.German] = "de";
            dict[Language.Galician] = "gl";
            dict[Language.French] = "fr";
            dict[Language.Finnish] = "fi";
            dict[Language.Filipino] = "tl";
            dict[Language.Estonian] = "et";
            dict[Language.Macedonian] = "mk";
            dict[Language.Lithuanian] = "lt";
            dict[Language.Latvian] = "lv";
            dict[Language.Latin] = "la";
            dict[Language.Lao] = "lo";
            dict[Language.Korean] = "ko";
            dict[Language.Japanese] = "ja";
            dict[Language.Italian] = "it";
            dict[Language.Irish] = "ga";
            dict[Language.Slovenian] = "sl";
            dict[Language.Slovak] = "sk";
            dict[Language.Serbian] = "sr";
            dict[Language.Russian] = "ru";
            dict[Language.Romanian] = "ro";
            dict[Language.Portuguese] = "pt";
            dict[Language.Polish] = "pl";
            dict[Language.Persian] = "fa";
            dict[Language.Norwegian] = "no";
            dict[Language.Maltese] = "mt";
            dict[Language.Malay] = "ms";
            dict[Language.Spanish] = "es";
            dict[Language.Swahili] = "sw";
            dict[Language.Swedish] = "sv";
            dict[Language.Tamil] = "ta";
            dict[Language.Telugu] = "te";
            dict[Language.Thai] = "th";
            dict[Language.Turkish] = "tr";
            dict[Language.Ukrainian] = "uk";
            dict[Language.Urdu] = "ur";
            dict[Language.Vietnamese] = "vi";
            dict[Language.Welsh] = "cy";
            dict[Language.Yiddish] = "yi";
            return dict;
        }
    }
}