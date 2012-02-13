using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Salar.ResumableDownload
{
	/// <summary>
	/// Managing user downloads speed limit
	/// </summary>
	public static class UserSpeedLimitManager
	{
		private class DownloadInfo
		{
			public string UserIP;
			public int SpeedLimit;
			public DownloadDataInfo DataInfo;
		}

		private static volatile List<DownloadInfo> _userDownloadInfo = new List<DownloadInfo>();

		/// <summary>
		/// Adding a new download to the list!
		/// </summary>
		public static void StartNewDownload(DownloadDataInfo dataInfo, string userIP, int userSpeedLimit = 0)
		{
			if (userIP == null || dataInfo == null)
				throw new ArgumentNullException();

			dataInfo.UserId = userIP;
			dataInfo.Finished += DataInfoFinished;
			lock (_userDownloadInfo)
			{
				_userDownloadInfo.Add(new DownloadInfo { UserIP = userIP, DataInfo = dataInfo, SpeedLimit = userSpeedLimit });
			}

			// changing all the downloads speed beglogs to this user
			ApplySpeedLimit(userIP, userSpeedLimit);
		}

		static void DataInfoFinished(DownloadDataInfo dataInfo)
		{
			if (dataInfo != null)
			{
				var userIP = dataInfo.UserId;
				var etag = dataInfo.EntityTag;
				RemoveDownloadInfoByEtag(etag);
				RefreshUserLimitState(userIP);
			}
		}

		/// <summary>
		/// Limiting all downloads which belogs to a user
		/// </summary>
		public static void ApplySpeedLimit(string userIP, int bytesPerSecond)
		{
			if (bytesPerSecond > 0)
			{
				var liveDownsCount = _userDownloadInfo.Count(x => x.UserIP == userIP);
				if (liveDownsCount == 0)
					return;

				var speadedBytes = (bytesPerSecond + 1) / liveDownsCount;

				// millisecods, this should help spreading speed equally through time
				var spreadedSleep = 1000 / (liveDownsCount * 2);

				foreach (var x in _userDownloadInfo.Where(x => x.UserIP == userIP))
				{
					Thread.Sleep(spreadedSleep);
					x.SpeedLimit = bytesPerSecond;
					x.DataInfo.LimitTransferSpeed(speadedBytes);
				}
			}
			else
			{
				foreach (var x in _userDownloadInfo.Where(x => x.UserIP == userIP))
				{
					x.SpeedLimit = 0;
					x.DataInfo.LimitTransferSpeed(0);
				}
			}
		}

		/// <summary>
		/// Recalculate user speed limit for all active downloads
		/// </summary>
		public static void RefreshUserLimitState(string userIP)
		{
			if (userIP == null)
				return;

			var downInfo = _userDownloadInfo.FirstOrDefault(x => x.UserIP == userIP);
			if (downInfo != null)
			{
				var bytesPerSecond = downInfo.SpeedLimit;

				// reapply the speed to all
				ApplySpeedLimit(userIP, bytesPerSecond);
			}
		}

		private static void RemoveDownloadInfoByEtag(string etag)
		{
			if (etag == null)
				return;
			lock (_userDownloadInfo)
			{
				for (int i = _userDownloadInfo.Count - 1; i >= 0; i--)
				{
					var t = _userDownloadInfo[i];
					if (t.DataInfo.EntityTag == etag)
					{
						_userDownloadInfo.RemoveAt(i);
					}
				}
			}
		}

	}
}
