using System;
using System.Collections.Generic;
using System.Linq;

namespace Substrate.Nbt
{
	public class NbtError
	{
		public readonly NbtErrorKind ErrorKind;
		public readonly IList<string> Path;
		public readonly string Message;

		public NbtError(IEnumerable<string> path, NbtErrorKind errorKind, string message)
		{
			ErrorKind = errorKind;
			Path = Array.AsReadOnly(path.ToArray());
			Message = message;
		}

		public override string ToString()
		{
			string errorKind = ErrorKind.ToString().Replace("_", ": ");

			if (Path.Any())
				return errorKind + ": /" + string.Join("/", Path) + ": " + Message;
			else
				return errorKind + ": " + Message;
		}
	}
}
