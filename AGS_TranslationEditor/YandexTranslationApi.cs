/*
    Copyright 2015 Bernd Keilmann

    This file is part of the AGS Translation Editor.

    AGS Translation Editor is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    AGS Translation Editor is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with AGS Translation Editor.  If not, see<http://www.gnu.org/licenses/>.

    Diese Datei ist Teil von AGS Translation Editor.

    AGS Translation Editor ist Freie Software: Sie können es unter den Bedingungen
    der GNU General Public License, wie von der Free Software Foundation,
    Version 3 der Lizenz oder (nach Ihrer Wahl) jeder späteren
    veröffentlichten Version, weiterverbreiten und/oder modifizieren.

    Fubar wird in der Hoffnung, dass es nützlich sein wird, aber
    OHNE JEDE GEWÄHRLEISTUNG, bereitgestellt; sogar ohne die implizite
    Gewährleistung der MARKTFÄHIGKEIT oder EIGNUNG FÜR EINEN BESTIMMTEN ZWECK.
    Siehe die GNU General Public License für weitere Details.

    Sie sollten eine Kopie der GNU General Public License zusammen mit diesem
    Programm erhalten haben.Wenn nicht, siehe <http://www.gnu.org/licenses/>.
*/

using System;
using System.IO;
using System.Net;
using System.Xml;

namespace AGS_TranslationEditor
{
    class YandexTranslationApi
    {
        const string apikey = "trnsl.1.1.20150903T113901Z.32e3dd5afcacce0b.8e25a0d10146112dab5142b5c06aaa80e85dac98";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">Your Yandex ApiKey</param>
        /// <param name="ui">If set, the service's response will contain a list of supported language codes and the corresponding language names (langs)</param>
        void getLangs(string key, string ui)
        {
            string req = string.Format("https://translate.yandex.net/api/v1.5/tr/getLangs?key={0}&ui={1}", apikey, ui);


        }

        /// <summary>
        /// Detects the language of the specified text.
        /// </summary>
        /// <param name="key">Your Yandex ApiKey</param>
        /// <param name="text">The text to detect the language for.</param>
        /// <param name="format">Text format. plaintext, HTML</param>
        void detect(string key, string text, string format)
        {
            string req = string.Format("https://translate.yandex.net/api/v1.5/tr/detect?key={0}&text={1}", apikey, text);

        }


        /// <summary>
        /// Translates the text.
        /// </summary>
        /// <param name="key">Your Yandex ApiKey</param>
        /// <param name="lang">Translation direction (for example, "en-ru" or "ru")</param>
        /// <param name="text">The text to be translated.</param>
        /// <param name="format">Text format. plaintext, HTML</param>
        /// <param name="options">Translation options.</param>
        public static string translate(string key, string lang, string text, string format, string options)
        {
            string translation = text;
            string req = string.Format("https://translate.yandex.net/api/v1.5/tr/translate?key={0}&lang={1}&text={2}", apikey, lang, text);
            WebRequest request = WebRequest.Create(req);
            WebResponse response = request.GetResponse();

            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();

            // Create an XmlReader
            using (XmlReader xreader = XmlReader.Create(new StringReader(responseFromServer)))
            {
                xreader.ReadToFollowing("Translation");
                xreader.MoveToFirstAttribute();
                string genre = xreader.Value;

                xreader.ReadToFollowing("text");
                translation = xreader.ReadElementContentAsString();
            }

            // Clean up the streams and the response.
            reader.Close();
            response.Close();

            return translation;
        }
    }
}