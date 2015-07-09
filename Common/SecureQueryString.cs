using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolBox
{
    public class SecureQueryString
    {


        private string _inputparams;
        private string _outputparams;
        private bool _paramsmatch;




        //The secret salt...
        private const string SecretSalt = "H3#@*ALMLLlk31q4l1ncL#@...";

        //public string InputParams
        //{
        //    get
        //    {
        //        return _inputparams;
        //        //throw new System.NotImplementedException();
        //    }
        //    set
        //    {
        //        _inputparams = value;
        //    }
        //}

        //public string OutputParams
        //{
        //    get
        //    {
        //        return _outputparams;
        //        //throw new System.NotImplementedException();
        //    }
        //    set
        //    {
        //        _outputparams = value;
        //    }
        //}

        public bool ParamsMatch
        {
            get
            {
                return _paramsmatch;
                //throw new System.NotImplementedException();
            }
            set
            {
                _paramsmatch = value;
            }
        }

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    HyperLink1.NavigateUrl = CreateTamperProofURL("SecureQScheck.aspx",
        //                     "NonTamperProof=1", "TP1=bruce&TP2=the&TP3=hamster");
        //}

        //////////////////////////////////////////////////////////////////////////
        // Encrypt
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
        /// Makes sure query string was not messed with.  
        /// </summary>
        /// <param name="tamperPoofParams">string</param>
        private void EnsureURLNotTampered(string tamperPoofParams)
        {

            // determine what the digest should be
            string expectedDigest = CheckDigest(tamperPoofParams);

            //  Any + in the digest passed through the querystring would be
            //  convereted into spaces, so 'uncovert' them
            string receivedDigest = "";// Request.QueryString["Digest"];
            if (receivedDigest == "")
            {
                // Oh my, we didn't get a Digest!
                //Response.Write("YOU MUST PASS IN A DIGEST!");
                //Response.End();
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
                }



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

    }

}
