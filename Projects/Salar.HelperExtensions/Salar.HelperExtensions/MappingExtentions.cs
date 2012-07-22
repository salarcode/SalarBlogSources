using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace System
{
	public static class MappingExtentions
	{
		/// <summary>
		/// Copy properties
		/// </summary>
		public static T CloneObject<T>(this T sourceModel)
		{
			var dest = Activator.CreateInstance<T>();
			CopyObject(sourceModel, dest);
			return dest;
		}

		/// <summary>
		/// Copy properties
		/// </summary>
		public static void CopyObject(this object source, object dest)
		{
			if (source == null || dest == null)
				return;

			Type sourceType = source.GetType();
			Type destType = dest.GetType();

			PropertyInfo[] srcProps;
			PropertyInfo[] destProps;
			try
			{
				srcProps = sourceType.GetProperties(BindingFlags.SetProperty | BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);
				destProps = destType.GetProperties(BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.Public);
			}
			catch
			{
				return;
			}

			foreach (PropertyInfo srcProp in srcProps)
			{
				try
				{
					PropertyInfo prop = srcProp;
					var destProp = destProps.FirstOrDefault(x => x.Name == prop.Name);
					if (destProp == null) continue;

					destProp.SetValue(dest, srcProp.GetValue(source, null), null);
				}
				catch { }
			}
		}

		public static T CopyObjectByDataMember<T>(this T source)
		{
			var dest = Activator.CreateInstance<T>();
			CopyObjectByDataMember(source, dest);
			return dest;
		}

		/// <summary>
		/// Force initialization of a proxy or persistent collection.
		/// </summary>
		public static void CopyObjectByDataMember(this object source, object dest)
		{
			if (source == null || dest == null)
				return;

			Type sourceType = source.GetType();
			Type destType = dest.GetType();

			PropertyInfo[] srcProps;
			PropertyInfo[] destProps;
			try
			{
				srcProps = sourceType.GetProperties(BindingFlags.SetProperty | BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);
				destProps = destType.GetProperties(BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.Public);
			}
			catch
			{
				return;
			}

			var dataMemType = typeof(DataMemberAttribute);

			foreach (PropertyInfo srcProp in srcProps)
			{
				try
				{
					// don't copy not data members
					if (!srcProp.IsDefined(dataMemType, true))
						continue;

					PropertyInfo prop = srcProp;
					var destProp = destProps.FirstOrDefault(x => x.Name == prop.Name);
					if (destProp == null) continue;

					destProp.SetValue(dest, srcProp.GetValue(source, null), null);
				}
				catch { }
			}
		}

		/// <summary>
		/// Copy properties
		/// </summary>
		public static IList<TDest> CloneListByDataMember<TSrc, TDest>(this IEnumerable<TSrc> source)
			where TSrc : class
			where TDest : class
		{
			if (source == null)
				return null;

			Type sourceType = typeof(TSrc);
			Type destType = typeof(TDest);


			PropertyInfo[] srcPropsAll;
			var srcProps = new List<PropertyInfo>();
			PropertyInfo[] destProps;
			try
			{
				srcPropsAll = sourceType.GetProperties(BindingFlags.SetProperty | BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);
				destProps = destType.GetProperties(BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.Public);
			}
			catch
			{
				return null;
			}
			var dataMemType = typeof(DataMemberAttribute);

			foreach (var propertyInfo in srcPropsAll)
			{
				// don't copy not data members
				if (!propertyInfo.IsDefined(dataMemType, true))
					continue;
				srcProps.Add(propertyInfo);
			}
			var result = Activator.CreateInstance<List<TDest>>();

			foreach (var src in source)
			{
				var destModel = Activator.CreateInstance<TDest>();
				foreach (PropertyInfo srcProp in srcProps)
				{
					try
					{
						PropertyInfo prop = srcProp;
						var destProp = destProps.FirstOrDefault(x => x.Name == prop.Name);
						if (destProp == null) continue;

						destProp.SetValue(destModel, srcProp.GetValue(src, null), null);
					}
					catch { }
				}
				result.Add(destModel);
			}
			return result;
		}
	}
}
