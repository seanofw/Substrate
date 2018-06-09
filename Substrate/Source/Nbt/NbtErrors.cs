using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Substrate.Nbt
{
	public class NbtErrors : ICollection<NbtError>
	{
		public bool HasErrors => Errors.Count != 0;
		public bool HasWarnings => Warnings.Count != 0;
		public bool Success => Errors.Count == 0;

		public int Count => Errors.Count + Warnings.Count;
		public bool IsReadOnly => true;

		public readonly IList<NbtError> Errors;
		public readonly IList<NbtError> Warnings;

		public NbtErrors(IEnumerable<NbtError> errors, IEnumerable<NbtError> warnings)
		{
			Errors = Array.AsReadOnly(errors.ToArray());
			Warnings = Array.AsReadOnly(warnings.ToArray());
		}

		public static NbtErrors FromMessage(NbtErrorKind errorKind, string message)
		{
			IEnumerable<NbtError> errorCollection = new[] { new NbtError(new string[0], errorKind, message) };

			return errorKind >= NbtErrorKind.Warnings
				? new NbtErrors(new NbtError[0], errorCollection)
				: new NbtErrors(errorCollection, new NbtError[0]);
		}

		public NbtErrors WithError(NbtErrorKind errorKind, string message)
		{
			List<NbtError> errors = Errors.ToList();
			errors.Add(new NbtError(new string[0], errorKind, message));
			return new NbtErrors(errors, Warnings);
		}

		public NbtErrors WithWarning(NbtErrorKind errorKind, string message)
		{
			List<NbtError> warnings = Warnings.ToList();
			warnings.Add(new NbtError(new string[0], errorKind, message));
			return new NbtErrors(warnings, Errors);
		}

		public static implicit operator bool(NbtErrors results)
		{
			return results.Success;
		}

		public IEnumerator<NbtError> GetEnumerator()
		{
			foreach (NbtError error in Errors)
			{
				yield return error;
			}
			foreach (NbtError warning in Warnings)
			{
				yield return warning;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Add(NbtError item)
		{
			throw new NotSupportedException("This collection is read-only.");
		}

		public void Clear()
		{
			throw new NotSupportedException("This collection is read-only.");
		}

		public bool Contains(NbtError item)
		{
			return Errors.Contains(item) || Warnings.Contains(item);
		}

		public void CopyTo(NbtError[] array, int arrayIndex)
		{
			Errors.CopyTo(array, arrayIndex);
			Warnings.CopyTo(array, arrayIndex + Errors.Count);
		}

		public bool Remove(NbtError item)
		{
			throw new NotSupportedException("This collection is read-only.");
		}

		public override string ToString()
		{
			if (!Errors.Any() && !Warnings.Any())
				return "Success";
			return $"{Errors.Count} Errors, {Warnings.Count} Warnings.";
		}
	}
}
