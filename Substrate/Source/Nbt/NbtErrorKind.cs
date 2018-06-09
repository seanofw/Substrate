namespace Substrate.Nbt
{
	public enum NbtErrorKind
	{
		Errors,
		Error_Exception,
		Error_IOError,
		Error_InvalidVersion,
		Error_MissingTag,
		Error_InvalidTagType,
		Error_InvalidTagValue,

		Warnings,
		Warning_UnexpectedTag,
	}
}
