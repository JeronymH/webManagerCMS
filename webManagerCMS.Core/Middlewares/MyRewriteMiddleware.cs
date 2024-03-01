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
			string? path = context.Request.Path.Value;

			if (path != null)
			{
				RedirectToResizer(context, path);
            }

            await _next(context);
		}

		private void RedirectToResizer(HttpContext context, string path) {
            Match match = Regex.Match(path, @"^/r/files/(.*)$");

            if (!match.Success)
				return;

            context.Response.Redirect("https://webmanager.lan/api/r/filemanager-gallery/" + match.Groups[1].Value);
        }
	}
}
