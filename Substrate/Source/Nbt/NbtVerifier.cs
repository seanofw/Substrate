using System.Collections.Generic;

namespace Substrate.Nbt
{
	public class NbtVerifier
	{
		private List<NbtError> _errors = new List<NbtError>();
		private List<NbtError> _warnings = new List<NbtError>();
		private List<string> _path = new List<string>();

 		public static NbtErrors Verify(TagNode root, SchemaNode schema)
		{
			NbtVerifier verifier = new NbtVerifier();

			verifier.VerifyRecursively(root, schema);

			return new NbtErrors(verifier._errors, verifier._warnings);
		}

		private void VerifyRecursively(TagNode tag, SchemaNode schema)
		{
			if (tag == null)
			{
				_errors.Add(new NbtError(_path, NbtErrorKind.Error_MissingTag, "Tag was null, not a valid tag."));
				return;
			}

			if (schema is SchemaNodeScalar scalar)
			{
				VerifyScalar(tag, scalar);
				return;
			}

			if (schema is SchemaNodeString str)
			{
				VerifyString(tag, str);
				return;
			}

			if (schema is SchemaNodeByteArray array)
			{
				VerifyByteArray(tag, array);
				return;
			}

			if (schema is SchemaNodeIntArray intarray)
			{
				VerifyIntArray(tag, intarray);
				return;
			}

			if (schema is SchemaNodeLongArray longarray)
			{
				VerifyLongArray(tag, longarray);
				return;
			}

			if (schema is SchemaNodeShortArray shortarray)
			{
				VerifyShortArray(tag, shortarray);
				return;
			}

			if (schema is SchemaNodeList list)
			{
				VerifyList(tag, list);
				return;
			}

			if (schema is SchemaNodeCompound compound)
			{
				VerifyCompound(tag, compound);
				return;
			}

			_errors.Add(new NbtError(_path, NbtErrorKind.Error_InvalidTagType, "Unknown tag type."));
			return;
		}

		private string StringifyActualValue(TagNode tag)
		{
			if (tag is TagNodeString stag)
			{
				string data = stag.Data;
				if (string.IsNullOrEmpty(data))
					return "string (\"\")";
				else if (data.Length > 40)
					return $"string (\"{data.Substring(0, 40)}...\")";
				else
					return $"string (\"{data}\")";
			}
			if (tag is TagNodeByte btag)
				return $"byte ({btag.Data})";
			if (tag is TagNodeShort shtag)
				return $"short ({shtag.Data})";
			if (tag is TagNodeInt itag)
				return $"int ({itag.Data})";
			if (tag is TagNodeLong ltag)
				return $"long ({ltag.Data})";
			if (tag is TagNodeFloat ftag)
				return $"float ({ftag.Data})";
			if (tag is TagNodeDouble dtag)
				return $"double ({dtag.Data})";
			if (tag is TagNodeByteArray byteArray)
				return $"byte array (size {byteArray.Length})";
			if (tag is TagNodeShortArray shortArray)
				return $"short array (size {shortArray.Length})";
			if (tag is TagNodeIntArray intArray)
				return $"int array (size {intArray.Length})";
			if (tag is TagNodeLongArray longArray)
				return $"long array (size {longArray.Length})";
			if (tag is TagNodeList list)
				return $"list ({list.Count} children)";
			if (tag is TagNodeCompound compound)
				return $"compound ({compound.Count} children)";

			return "(unknown tag)";
		}

		private void VerifyScalar(TagNode tag, SchemaNodeScalar schema)
		{
			if (!tag.IsCastableTo(schema.Type))
			{
				_errors.Add(new NbtError(_path, NbtErrorKind.Error_InvalidTagType, $"Expected a {schema.Type} value; got {StringifyActualValue(tag)}."));
			}
		}

		private void VerifyString(TagNode tag, SchemaNodeString schema)
		{
			TagNodeString stag = tag as TagNodeString;
			if (stag == null)
			{
				_errors.Add(new NbtError(_path, NbtErrorKind.Error_InvalidTagType, "Expected a string value; got " + StringifyActualValue(tag)));
				return;
			}
			if (schema.Length > 0 && stag.Length > schema.Length)
			{
				_errors.Add(new NbtError(_path, NbtErrorKind.Error_InvalidTagValue, $"String is too long ({stag.Length} chars > limit of {schema.Length})"));
				return;
			}
			if (schema.Value != null && stag.Data != schema.Value)
			{
				_errors.Add(new NbtError(_path, NbtErrorKind.Error_InvalidTagValue, $"String is supposed to be \"{schema.Value}\", but was actually \"{stag.Data}\"."));
				return;
			}
		}

		private void VerifyByteArray(TagNode tag, SchemaNodeByteArray schema)
		{
			TagNodeByteArray atag = tag as TagNodeByteArray;
			if (atag == null)
			{
				_errors.Add(new NbtError(_path, NbtErrorKind.Error_InvalidTagType, "Expected a byte array; got " + StringifyActualValue(tag)));
				return;
			}
			if (schema.Length > 0 && atag.Length != schema.Length)
			{
				_errors.Add(new NbtError(_path, NbtErrorKind.Error_InvalidTagValue, $"Byte array is the wrong size ({atag.Length} bytes != expected {schema.Length})"));
				return;
			}
		}

		private void VerifyIntArray(TagNode tag, SchemaNodeIntArray schema)
		{
			TagNodeIntArray atag = tag as TagNodeIntArray;
			if (atag == null)
			{
				_errors.Add(new NbtError(_path, NbtErrorKind.Error_InvalidTagType, "Expected an int array; got " + StringifyActualValue(tag)));
				return;
			}
			if (schema.Length > 0 && atag.Length != schema.Length)
			{
				_errors.Add(new NbtError(_path, NbtErrorKind.Error_InvalidTagValue, $"Int array is the wrong size ({atag.Length} values != expected {schema.Length})"));
				return;
			}
		}

		private void VerifyLongArray(TagNode tag, SchemaNodeLongArray schema)
		{
			TagNodeLongArray atag = tag as TagNodeLongArray;
			if (atag == null)
			{
				_errors.Add(new NbtError(_path, NbtErrorKind.Error_InvalidTagType, "Expected a long array; got " + StringifyActualValue(tag)));
				return;
			}
			if (schema.Length > 0 && atag.Length != schema.Length)
			{
				_errors.Add(new NbtError(_path, NbtErrorKind.Error_InvalidTagValue, $"Long array is the wrong size ({atag.Length} values != expected {schema.Length})"));
				return;
			}
		}

		private void VerifyShortArray(TagNode tag, SchemaNodeShortArray schema)
		{
			TagNodeShortArray atag = tag as TagNodeShortArray;
			if (atag == null)
			{
				_errors.Add(new NbtError(_path, NbtErrorKind.Error_InvalidTagType, "Expected a short array; got " + StringifyActualValue(tag)));
				return;
			}
			if (schema.Length > 0 && atag.Length != schema.Length)
			{
				_errors.Add(new NbtError(_path, NbtErrorKind.Error_InvalidTagValue, $"Short array is the wrong size ({atag.Length} values != expected {schema.Length})"));
				return;
			}
		}

		private void VerifyList(TagNode tag, SchemaNodeList schema)
		{
			TagNodeList ltag = tag as TagNodeList;
			if (ltag == null)
			{
				_errors.Add(new NbtError(_path, NbtErrorKind.Error_InvalidTagType, "Expected a list; got " + StringifyActualValue(tag)));
				return;
			}
			if (ltag.Count > 0 && ltag.ValueType != schema.ItemType)
			{
				_errors.Add(new NbtError(_path, NbtErrorKind.Error_InvalidTagValue, $"List items are the wrong type ({ltag.ValueType} != expected {schema.ItemType})"));
				return;
			}
			if (schema.Length > 0 && ltag.Count != schema.Length)
			{
				_errors.Add(new NbtError(_path, NbtErrorKind.Error_InvalidTagValue, $"List is the wrong size ({ltag.Count} items != expected {schema.Length})"));
				return;
			}

			if (schema.ItemSchema != null)
			{
				int index = 0;
				int pathLength = _path.Count;
				_path.Add(string.Empty);

				try
				{
					foreach (TagNode child in ltag)
					{
						_path[pathLength] = index.ToString();
						VerifyRecursively(child, schema.ItemSchema);
						index++;
					}
				}
				finally
				{
					_path.RemoveAt(pathLength);
				}
			}
		}

		/// <remarks>
		/// Note that in addition to checking this compound tag against the schema, it also MODIFIES
		/// the tag to include a default NBT subtree if it is marked as 'CREATE_ON_MISSING' in the
		/// schema.
		/// </remarks>
		private void VerifyCompound(TagNode tag, SchemaNodeCompound schema)
		{
			TagNodeCompound ctag = tag as TagNodeCompound;
			if (ctag == null)
			{
				_errors.Add(new NbtError(_path, NbtErrorKind.Error_InvalidTagType, "Expected a compound; got " + StringifyActualValue(tag)));
				return;
			}

			Dictionary<string, TagNode> scratch = new Dictionary<string, TagNode>();
			HashSet<string> foundNames = new HashSet<string>();

			int pathLength = _path.Count;
			_path.Add(string.Empty);

			try
			{
				foreach (SchemaNode node in schema)
				{
					TagNode value;
					if (ctag.TryGetValue(node.Name, out value))
					{
						foundNames.Add(node.Name.ToLower());
					}

					if (value == null)
					{
						if ((node.Options & SchemaOptions.CREATE_ON_MISSING) == SchemaOptions.CREATE_ON_MISSING)
						{
							scratch[node.Name] = node.BuildDefaultTree();
							continue;
						}

						if ((node.Options & SchemaOptions.OPTIONAL) == SchemaOptions.OPTIONAL)
							continue;
					}

					_path[pathLength] = node.Name;
					VerifyRecursively(value, node);
				}
			}
			finally
			{
				_path.RemoveAt(pathLength);
			}

			foreach (string tagName in ctag.Keys)
			{
				if (!foundNames.Contains(tagName.ToLower()))
				{
					_warnings.Add(new NbtError(_path, NbtErrorKind.Warning_UnexpectedTag, $"Compound contains an unknown tag \"{tagName}\"."));
				}
			}

			foreach (KeyValuePair<string, TagNode> item in scratch)
			{
				ctag[item.Key] = item.Value;
			}

			scratch.Clear();
		}
	}
}
