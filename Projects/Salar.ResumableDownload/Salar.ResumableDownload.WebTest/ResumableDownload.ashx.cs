using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Salar.ResumableDownload.WebTest
{
	/// <summary>
	/// Summary description for ResumableDownload
	/// </summary>
	public class ResumableDownload : IHttpHandler
	{
		/// <summary>
		/// 10 KB limit
		/// </summary>
		const int DownloadLimit = 500 * 1024;

		public void ProcessRequest(HttpContext context)
		{
			// Accepting user request

			// reading the query
			var fileNameQuery = context.Request.QueryString["file"];

			// validating the request
			if (string.IsNullOrEmpty(fileNameQuery))
			{
				InvalidRequest(context, "Invalid request! Specify file name in url e.g.: ResumableDownload.ashx?file=sample.zip");
				return;
			}

			// the physical file address path
			var fileName = context.Server.MapPath(fileNameQuery);
			if (!File.Exists(fileName))
			{
				InvalidRequest(context, "File does not exists!");
				return;
			}

			// reading file info
			var fileInfo = new FileInfo(fileName);
			var fileLength = fileInfo.Length;

			// Download information class
			var downloadInfo = new DownloadDataInfo(fileName);

			// Reading request download range
			var requestedRanges = HeadersParser.ParseHttpRequestHeaderMultipleRange(context.Request, fileLength);

			// apply the ranges to the download info
			downloadInfo.InitializeRanges(requestedRanges);

			string etagMatched;
			int outcomeStausCode = 200;

			// validating the ranges specified
			if (!HeadersParser.ValidatePartialRequest(context.Request, downloadInfo, out etagMatched, ref outcomeStausCode))
			{
				// the request is invalid, this is the invalid code
				context.Response.StatusCode = outcomeStausCode;

				// show to the client what is the real ETag
				if (!string.IsNullOrEmpty(etagMatched))
					context.Response.AppendHeader("ETag", etagMatched);

				// stop the preoccess
				// but don't hassle with error messages
				return;
			}

			// user ID, or IP or anything you use to identify the user
			var userIP = context.Request.UserHostAddress;

			// limiting the download speed manager and the speed limit
			UserSpeedLimitManager.StartNewDownload(downloadInfo, userIP, DownloadLimit);

			// It is very important to destory the DownloadProcess object
			// Here the using block does it for us.
			using (var process = new DownloadProcess(downloadInfo))
			{
				// start the download
				var state = process.ProcessDownload(context.Response);

				// checking the state of the download
				if (state == DownloadProcess.DownloadProcessState.PartFinished)
				{
					// all parts of download are finish, do something here!
				}
			}
		}

		public void InvalidRequest(HttpContext context, string message = "Invalid Request!")
		{
			context.Response.Write(message);
			context.Response.StatusCode = 500;
		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}