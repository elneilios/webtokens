#region Copyright (c) 2012 Two10 degrees
//
// (C) Copyright 2012 Two10 degrees
//      All rights reserved.
//
// This software is provided "as is" without warranty of any kind,
// express or implied, including but not limited to warranties as to
// quality and fitness for a particular purpose. Two10 degrees
// does not support the Software, nor does it warrant that the Software
// will meet your requirements or that the operation of the Software will
// be uninterrupted or error free or that any defects will be
// corrected. Nothing in this statement is intended to limit or exclude
// any liability for personal injury or death caused by the negligence of
// Two10 degrees, its employees, contractors or agents.
//
// Two10 degrees is a trading style of Active Web Solutions Ltd.
//
#endregion

using System.Collections.Specialized;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Two10.Swt.Tests
{
    
    [TestClass()]
    public class SimpleWebTokenTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        private static string ACSSigningKey = "8YMtduGa+9B8MpSEIESXI0wuzvyspxJ1TGhSDlDvjSY=";

     
        [TestMethod()]
        public void Test1()
        {
            string wrapEndpoint = "http://localhost:50865/my/wrap";
            string wrapName = "robblackwell";
            string wrapPassword = "MyPassword";
            string wrapScope = "http://www.robblackwell.org.uk/";

            SimpleWebToken swt = SimpleWebToken.GetToken(wrapEndpoint,
                wrapName,
                wrapPassword,
                wrapScope);

            WebClient client = new WebClient();

            client.Headers.Add("Authorization", "WRAP access_token=\"" + swt.ToUrlEncodedString() + "\"");

            byte[] responseBytes = client.DownloadData("http://localhost:50865/my/test");

            string response = Encoding.UTF8.GetString(responseBytes);

            Assert.IsTrue(response.Contains("Hello"));
        }

        [TestMethod()]
        public void Test2()
        {
            string testKey = "8YMtduGa+9B8MpSEIESXI0wuzvyspxJ1TGhSDlDvjSY=";

            string signedToken = "wrap_access_token=http%253a%252f%252fschemas.xmlsoap.org%252fws%252f2005%252f05%252fidentity%252fclaims%252fnameidentifier%3drobblackwell%26http%253a%252f%252fschemas.microsoft.com%252faccesscontrolservice%252f2010%252f07%252fclaims%252fidentityprovider%3dhttps%253a%252f%252fclazure.accesscontrol.windows.net%252f%26Audience%3dhttp%253a%252f%252fwww.robblackwell.org.uk%252f%26ExpiresOn%3d1331740071%26Issuer%3dhttps%253a%252f%252fclazure.accesscontrol.windows.net%252f%26HMACSHA256%3d2QnptKank3k4MwKrwhLIA4qyG1%252fF1cCmHrr%252f2W6xLxw%253d&wrap_access_token_expires_in=600";

            SimpleWebToken swt = SimpleWebToken.Parse(signedToken);

            Assert.IsTrue(swt.CheckSignature(testKey));
        }

        [TestMethod()]
        public void Test3()
        {
            string testKey = "ZncEZCBioztYEE3iC6dSnv+lJC4NmFX7Ns5pDgPKCwU=";

            NameValueCollection claims = new NameValueCollection();

            claims.Add("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "robblackwell");
            claims.Add("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "https://clazure.accesscontrol.windows.net/");

            SimpleWebToken swt = new SimpleWebToken("https://clazure.accesscontrol.windows.net/",
                    "http://www.robblackwell.org.uk/", 1331740071, claims);

            swt.Sign(testKey);

            Assert.IsTrue(swt.CheckSignature(testKey));
        }

       

    }
}
