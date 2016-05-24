using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace Common
{
    public class StringTools
    {


        // The secret salt...
        // had trouble moving this to the Web.config due to the characters in the string... 
        // might find a way to use the users password salt vs. this one.  
        private const string SecretSalt = "H3#@*ALMLLlk31q4l1ncL#@RFHF#N3fNM><#WH$O@#!FN#LNl33N#LNFl#J#Y$#IOHhnf;;3qrthl3q";
        //private const string SecretSalt = "H3#@*ALMLLlk31q4l1ncL#@...";
        //private const string SecretSalt = System.Configuration.ConfigurationManager.AppSettings["SecertSalt"].ToString();



        ///// <summary>
        ///// Takes a file name and sends back a URL safe one (Ahhh... Nice...)  
        ///// </summary>
        ///// <param name="sFileName"></param>
        ///// <returns></returns>
        //public string scrubFileName(string sName)
        //{
        //    string NewFileName = sName;
        //    // Replace space with _ remove ()&'
        //    NewFileName = Regex.Replace(NewFileName, " ", "_");
        //    NewFileName = Regex.Replace(NewFileName, "\\)", "");
        //    NewFileName = Regex.Replace(NewFileName, "\\(", "");
        //    NewFileName = Regex.Replace(NewFileName, "&", "");
        //    NewFileName = Regex.Replace(NewFileName, "'", "");
        //    NewFileName = Regex.Replace(NewFileName, "#", "");
        //    NewFileName = Regex.Replace(NewFileName, "$", "");
        //    NewFileName = Regex.Replace(NewFileName, "%", "");
        //    NewFileName = scrubText(NewFileName);
        //    return NewFileName;
        //}

        public string scrubHtml(string stringIn)
        {
            stringIn = Regex.Replace(stringIn, "<", "&lt;");
            stringIn = Regex.Replace(stringIn, ">", "&gt;");
            stringIn = Regex.Replace(stringIn, "script", "scr!pt");
            return stringIn;
        }

        public string unscrubHtml(string stringIn)
        {
            stringIn = Regex.Replace(stringIn, "&lt;", "<");
            stringIn = Regex.Replace(stringIn, "&gt;", ">");
            return stringIn;
        }


        /// <summary>
        /// Takes a file name and sends back a URL safe one (Ahhh... Nice...)  
        /// </summary>
        /// <param name="sFileName"></param>
        /// <returns></returns>
        public string scrubFileName(string sText)
        {
            string CleanText = sText;
            // Replace space with _ remove ()&'
            CleanText = Regex.Replace(CleanText, " ", "_");
            CleanText = Regex.Replace(CleanText, "\\)", "");
            CleanText = Regex.Replace(CleanText, "\\(", "");
            CleanText = Regex.Replace(CleanText, "&", "");
            CleanText = Regex.Replace(CleanText, "'", "");
            CleanText = Regex.Replace(CleanText, "#", "");
            CleanText = Regex.Replace(CleanText, "$", "");
            CleanText = Regex.Replace(CleanText, "%", "");

            return CleanText;
        }


        /// <summary>
        /// QueryString to integer.  Converts a QueryString value to an integer
        /// </summary>
        /// <param name="ValueToCheck">The QueryString to Check</param>
        /// <returns>a number</returns>
        public int QueryStringtoInteger(string ValueToCheck)
        {

            // QueryString Kung Fu
            int OutValue = 0;
            bool isNumber = int.TryParse(ValueToCheck, System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.CurrentCulture, out OutValue);

            if (isNumber)
                OutValue = Convert.ToInt16(ValueToCheck);

            return OutValue;
        }



        //        private string QueryStringChecker(string ValueToCheck)
        //        {

        //            // QueryString Kung Fu
        //            int OutValue = 0;
        //            bool isNumber = int.TryParse(ValueToCheck, System.Globalization.NumberStyles.Any,
        //System.Globalization.CultureInfo.CurrentCulture, out OutValue);

        //            if (isNumber)
        //                OutValue = Convert.ToInt16(ValueToCheck);

        //            return "";
        //        }





        //////////////////////////////////////////////////////////////////////////
        // Encrypt
        /// <summary>
        /// Encrypts a QueryString 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="nonTamperProofParams"></param>
        /// <param name="tamperProofParams"></param>
        /// <returns></returns>
        public string CreateTamperProofURL(string url, string nonTamperProofParams, string tamperProofParams)
        {
            string tplURL = url;
            if (nonTamperProofParams.Length > 0 || tamperProofParams.Length > 0)
            { url += "?"; }

            // Add on the tamper & non-tamper proof parameters, if any
            if (nonTamperProofParams.Length > 0)
            {
                url += nonTamperProofParams;
                if (tamperProofParams.Length > 0)
                { url += "&"; }
            }

            if (tamperProofParams.Length > 0)
            { url += string.Concat("Digest=", CreateDigest(tamperProofParams)); }
            return url;
        }

        private string CreateDigest(string tamperProofParams)
        {

            string Digest = string.Empty;
            string input = string.Concat(SecretSalt, tamperProofParams, SecretSalt);
            // the array of bytes that will contain the encrypted value of input
            byte[] hashedDataBytes;
            // the encoder class used to convert strPlainText to an array of bytes
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            // Create an instance of the MD5CryptoServiceProvider class
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            // call computeHash, passing in the plain-text string as an array of bytes
            // the return value is the encrypted value, as an array of bytes
            hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(input));
            // base-64 encode the result and strip off the ending '==', if it exists
            Digest = Convert.ToBase64String(hashedDataBytes).TrimEnd("=".ToCharArray());

            return Digest;
        }
        //////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////
        // Decrypt and match
        /// <summary>
        /// Makes sure query string was not messed with.  true if altered, false if okay
        /// </summary>
        /// <param name="tamperPoofParams">string</param>
        public bool EnsureURLNotTampered(string tamperPoofParams, string receivedDigest)
        {

            // determine what the digest should be
            string expectedDigest = CheckDigest(tamperPoofParams);

            //  Any + in the digest passed through the querystring would be
            //  convereted into spaces, so 'uncovert' them
            //string receivedDigest = "";// Request.QueryString["Digest"];
            if (receivedDigest == "")
            {
                // Oh my, we didn't get a Digest!
                //Response.Write("YOU MUST PASS IN A DIGEST!");
                //Response.End();
                //BOOM
                return true;
            }
            else
            {
                receivedDigest = receivedDigest.Replace(" ", "+");

                //    Now, see if the received and expected digests match up
                if (expectedDigest.CompareTo(receivedDigest) != 0)
                {
                    // Don't match up, egad
                    //Response.Write("THE URL HAS BEEN TAMPERED WITH.");
                    //Response.End();
                    // BOOM
                    return true;
                }

                //OK
                return false;
            }


        }

        private string CheckDigest(string tamperProofParams)
        {
            string Digest = string.Empty;
            string input = string.Concat(SecretSalt, tamperProofParams, SecretSalt);

            // The array of bytes that will contain the encrypted value of input
            byte[] hashedDataBytes;
            // The encoder class used to convert strPlainText to an array of bytes
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            // Create an instance of the MD5CryptoServiceProvider class
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            // Call ComputeHash, passing in the plain-text string as an array of bytes
            // The return value is the encrypted value, as an array of bytes

            // Call ComputeHash, passing in the plain-text string as an array of bytes
            // The return value is the encrypted value, as an array of bytes
            hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(input));
            // Base-64 Encode the results and strip off ending '==', if it exists
            Digest = Convert.ToBase64String(hashedDataBytes).TrimEnd("=".ToCharArray());

            return Digest;
        }
        //////////////////////////////////////////////////////////////////////////


        // house keeping -- not sure this is the best way to do this.  
        public void Dispose() { }

    }
}
