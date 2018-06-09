namespace Substrate.Nbt
{
	public enum NbtErrorKind
	{
		Errors,
		Error_MissingTag,
		Error_InvalidTagType,
		Error_InvalidTagValue,
		Error_IOError,

		Warnings,
		Warning_UnexpectedTag,
	}
}
