using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace webManagerCMS.Core.Middlewares
{
	public class RewriteMiddleware
	{
		private readonly RequestDelegate _next;

		public RewriteMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			string? path = context.Request.Path.Value;
			string newPath = "";

			if (path != null && path != "/")
			{
				newPath = path;
				GetMutationAlias(context, ref newPath);
				GetPageAlias(context, ref newPath, 0);
				GetPageAlias(context, ref newPath, 1);
			}

			// Match any URL with query parameters
			var match = Regex.Match(path, @"^(.+)\?");

			if (match.Success)
			{
				// Extract query string and remove leading "?"
				var queryString = match.Groups[2].Value.TrimStart('?');

				// Redirect to new URL with extracted parameters
				newPath = queryString.Length > 0 ? $"?{queryString}" : "";
				context.Response.Redirect(newPath, true);
			}

			//if (path != newPath)
			//	context.Response.Redirect(newPath, true);

			if (path != "/")
				context.Response.Redirect("/");
            await _next(context);
		}

		private void GetMutationAlias(HttpContext context, ref string path) {
			Match match = Regex.Match(path, @"^/(cz|en|de|it|sk)(?:/([^?]+))?", RegexOptions.IgnoreCase);

			if (!match.Success)
				return;

			string languageCode = match.Groups[1].Value;

			if (languageCode == context.Request.Query["rMutationAlias"].ToString())
				return;

			string capturedPath = match.Groups[2].Value;

			// Combine query string
			string queryString = context.Request.QueryString.HasValue
				? context.Request.QueryString.Value.Substring(1) + "&"
				: "";


			// Construct new URL
			path = $"/{languageCode}/{capturedPath}{(queryString.Length > 0 ? "?" : "")}{queryString}{(queryString.Length > 0 ? "&" : "?")}rMutationAlias={languageCode}";
		}

		//zeptat se Davida co s tím
		private void GetPageAlias(HttpContext context, ref string path, int lvl) {
			Match match = Regex.Match(path, @"^(?:/(?:([^/\.\?]*)(?=-\d+(?:$|\?|\.html|/))-(\d+)|([^/\.\?]*))(?:/|\.html)?([^\?]+(?<!\.asp))?)?(?:$|(\?.+)?)$", RegexOptions.IgnoreCase);

			if (!match.Success)
				return;

			// Extract captured groups
			string alias1 = match.Groups[1].Value;
			string id = match.Groups[2].Value;
			string alias2 = match.Groups[3].Value;
			string pathComponent = match.Groups[4].Value;
			string queryString = match.Groups[5].Value;

			//if (alias1 == context.Request.Query[$"rPageAlias{lvl}"].ToString())
			//	return;

			// Construct new URL
			path = $"/{pathComponent}{queryString}{(queryString.Length > 0 ? "&" : "?")}rPageAlias{lvl}={alias1}&rPageAlias{lvl}={alias2}";
		}
	}
}
