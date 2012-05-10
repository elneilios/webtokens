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
using System.Web.Mvc;
using Two10.Swt;

namespace Two10.SampleRest.Controllers
{
    public class MyController : Controller
    {
        // Web Resource Access Protocol v0.9 compatible endpoint for issuing SWT tokens
        public ActionResult Wrap()
        {
            string name = Request.Form["wrap_name"];
            string password = Request.Form["wrap_password"];
            string scope = Request.Form["wrap_scope"];

            string signingKey = "8YMtduGa+9B8MpSEIESXI0wuzvyspxJ1TGhSDlDvjSY=";

            if ((name == "robblackwell") && (password == "MyPassword") && (scope == "http://www.robblackwell.org.uk/"))
            {
                NameValueCollection claims = new NameValueCollection();

                claims.Add("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "robblackwell");
                claims.Add("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "http://localhost:50865/");

                SimpleWebToken swt = new SimpleWebToken("http://localhost:50865/",
                    "http://www.robblackwell.org.uk/", 1331740071, claims);

                swt.Sign(signingKey);
                return Content( "wrap_access_token=" + swt.ToUrlEncodedString() + "&wrap_access_token_expires_in=600", "application/xml");
            }
            else
            {
                Response.StatusCode = 401; // Unauthorized
                return null;
            }
        }

        // Note that a custom authorization attribute is used to secure this endpoint, and consumers
        // must present a valid simple web token ussued from the above WRAP endpoint.

        [RESTAuthorize]
        public ActionResult Test()
        {
            string foo = "Hello World";

            Response.ContentType = "application/json";
            return Json(foo, JsonRequestBehavior.AllowGet);

        }

    }
}