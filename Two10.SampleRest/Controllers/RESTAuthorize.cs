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

using System.Web.Mvc;
using Two10.Swt;

namespace Two10.SampleRest.Controllers
{
    class RESTAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            string signingKey = "8YMtduGa+9B8MpSEIESXI0wuzvyspxJ1TGhSDlDvjSY=";
            SimpleWebToken swt = SimpleWebToken.FromHttpContext(httpContext);

            if (swt.CheckSignature(signingKey))
            {
                return true;
                //return base.AuthorizeCore(httpContext);
            }
            else
            {
                return false;
            }
        }

        
    }
}
