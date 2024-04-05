﻿using Microsoft.AspNetCore.Rewrite;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace webManagerCMS.Core.Middlewares
{
	public class MyRewriteMiddleware
	{
		private readonly RequestDelegate _next;

		public MyRewriteMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			if (context.Request.Path.Value != null)
			{
				RedirectToResizer(context);
                GetMutationAlias(context);
                GetPageAliasLvl(context, 0);
                GetPageAliasLvl(context, 1);
                GetPageAliasLvl(context, 2);
                GetPageAliasLvl(context, 3);
            }

            await _next(context);
		}

		private void RedirectToResizer(HttpContext context) {
            Match match = Regex.Match(context.Request.Path.Value + context.Request.QueryString, @"^/r/files/(.*)$");

            if (!match.Success)
				return;

            context.Response.Redirect("https://webmanager.lan/api/r/filemanager-gallery/" + match.Groups[1].Value);
        }

        private void GetMutationAlias(HttpContext context)
        {
            Match match = Regex.Match(context.Request.Path.Value + context.Request.QueryString, @"^/(cz|en|de|it|sk)(?:(/[^\?]+)|/)?(?:(\?.+)|(\?)?)?$");

            if (!match.Success)
                return;

            // Get captured groups
            var mutationAlias = match.Groups[1].Value;
            var remainingPath = match.Groups[2].Value;
            var queryString = match.Groups[3].Value;

            // Build the new URL
            string newUrl, newQueryString;
            if (!string.IsNullOrEmpty(remainingPath))
                newUrl = remainingPath;
            else
                newUrl = "/";

            if (!string.IsNullOrEmpty(queryString))
            {
                newQueryString = "?" + queryString;
                newQueryString += $"&rMutationAlias={mutationAlias}";
            }
            else
                newQueryString = $"?rMutationAlias={mutationAlias}";

            context.Request.Path = newUrl;
            context.Request.QueryString = new QueryString(newQueryString);
        }

        private void GetPageAliasLvl(HttpContext context, int lvl)
        {
            Match match = Regex.Match(context.Request.Path.Value + context.Request.QueryString, @"^(?:/(?:([^/\.\?]*)(?=-\d+(?:$|\?|\.html|/))-(\d+)|([^/\.\?]*))(?:/|\.html)?([^\?]+(?<!\.asp))?)?(?:$|(\?.+)?)$");

            if (!match.Success)
                return;

            // Get captured groups
            var alias0 = match.Groups[1].Value;
            var page0 = match.Groups[2].Value;
            var alias1 = match.Groups[3].Value;
            var remainingPath = match.Groups[4].Value;
            var queryString = match.Groups[5].Value;

            // Build the new URL
            string newUrl;
            string newQueryString = "";
            string queryAddChar = "?";
            if (!string.IsNullOrEmpty(remainingPath))
                newUrl = "/" + remainingPath;
            else
                newUrl = "/" + remainingPath;

            if (!string.IsNullOrEmpty(queryString))
            {
                newQueryString += queryString;
                queryAddChar = "&";
            }

            if (!string.IsNullOrEmpty(alias0))
                newQueryString += $"{queryAddChar}rAlias{lvl}={alias0}&rPage{lvl}={page0}";
            else
                newQueryString += $"{queryAddChar}rAlias{lvl}={alias1}";

            context.Request.Path = newUrl;
            context.Request.QueryString = new QueryString(newQueryString);
        }
    }
}
