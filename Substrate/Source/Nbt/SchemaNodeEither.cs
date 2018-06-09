using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Substrate.Nbt
{
	public class SchemaNodeEither : SchemaNode, ICollection<SchemaNode>
	{
		private IList<SchemaNode> _choices;

		public SchemaNodeEither(string name, params SchemaNode[] choices)
			: base(name, 0)
		{
			_choices = Array.AsReadOnly(choices);
		}

		public SchemaNodeEither(string name, IEnumerable<SchemaNode> choices)
			: base(name, 0)
		{
			_choices = Array.AsReadOnly(choices.ToArray());
		}

		public override TagNode BuildDefaultTree()
		{
			return _choices[0].BuildDefaultTree();
		}

		public int Count => _choices.Count;

		public bool IsReadOnly => true;

		public void Add(SchemaNode item)
		{
			throw new NotSupportedException();
		}

		public void Clear()
		{
			throw new NotSupportedException();
		}

		public bool Contains(SchemaNode item)
		{
			return _choices.Contains(item);
		}

		public void CopyTo(SchemaNode[] array, int arrayIndex)
		{
			_choices.CopyTo(array, arrayIndex);
		}

		public IEnumerator<SchemaNode> GetEnumerator()
		{
			return _choices.GetEnumerator();
		}

		public bool Remove(SchemaNode item)
		{
			throw new NotSupportedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _choices.GetEnumerator();
		}
	}
}
