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

using System;
using Two10.Swt;

namespace Two10.Sample1
{
    class Program
    {
        static void Main(string[] args)
        {

            string ACSSigningKey = "YOUR_ACS_SIGNING_KEY";

            string wrapEndpoint = "https://YOURPROJECT.accesscontrol.windows.net/WRAPv0.9";
            string wrapName = "YOUR_USER_NAME";
            string wrapPassword = "YOUR_PASSWORD";
            string wrapScope = "http://YOURCOMPANY.com/";

            SimpleWebToken s = SimpleWebToken.GetToken(wrapEndpoint,
                wrapName,
                wrapPassword,
                wrapScope);

            Console.WriteLine("Audience={0}",s.Audience);
            Console.WriteLine("ExpiresOn={0}",s.ExpiresOn);
            Console.WriteLine("Issuer={0}",s.Issuer);
            Console.WriteLine("HmacSha256={0}", s.HmacSha256);
        

            if (s.CheckSignature(ACSSigningKey))
                Console.WriteLine("Valid signature");
            else
                Console.WriteLine("Invalid signature");
        
            }
    }
}
