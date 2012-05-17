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
using System.Web.Mvc;
using Two10.Swt;

namespace Two10.Swt
{
    public enum TokenType
    {
        Swt,
        Saml
    }

    public class TokenAuthorizeAttribute : AuthorizeAttribute
    {
        public TokenType TokenType { get; set; }
        public TokenAuthorizeAttribute(TokenType tokenType)
        {
            this.TokenType = tokenType;
        }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            switch (this.TokenType)
            {
                case TokenType.Swt:
                    return AuthorizeSwt(httpContext);

                case TokenType.Saml:
                    return false;

                default:
                    throw new InvalidOperationException("Invalid TokenType");
            }
        }

        private bool AuthorizeSwt(System.Web.HttpContextBase httpContext)
        {
            string signingKey = "8YMtduGa+9B8MpSEIESXI0wuzvyspxJ1TGhSDlDvjSY=";
            string trustedIssuer = "http://localhost:50865/";
            string expectedAudience = "http://www.robblackwell.org.uk/";

            try
            {
                SimpleWebToken swt = SimpleWebToken.FromHttpContext(httpContext);

                if (SimpleWebToken.Validate(swt, signingKey, trustedIssuer, expectedAudience) == SimpleWebTokenValidationResult.Valid)
                {
                    // No Roles/Claims specified
                    if (string.IsNullOrWhiteSpace(this.Roles))
                    {
                        return true;
                    }
                    else
                    {
                        var principle = swt.ToPrinciple();
                        string[] roles = this.Roles.Trim().Split(',');

                        foreach (string role in roles)
                        {
                            if (principle.IsInRole(role))
                                return true;
                        }

                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }            
        }        
    }
}
