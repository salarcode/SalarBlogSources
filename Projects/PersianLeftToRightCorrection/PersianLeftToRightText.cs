using System;
using System.Collections.Generic;
using System.Text;

namespace PersianLeftToRightCorrection
{
	/// <summary>
	/// Modifiying the RTL text to display correctly in LTR context.
	/// This is done by reordering groups of different alphabet and symbol types.
	/// </summary>
	/// <author>
	/// Salar khalilzadeh 
	/// salar2k@gmail.com
	/// http://blog.salarcode.com/
	/// 2012-1-25
	/// </author>
	public static class PersianLeftToRightText
	{
		/// <summary>
		/// Not detected as symbol but behave like symbols
		/// </summary>
		private static readonly char[] _neutralChars = {
		                                               	'.', '(', ')', '{', '}', '[', ']', '+', '=', '-'
		                                               	, '_', '*', '&', '^', '%', '$', '#', '@', '!', '~', '`'
		                                               	, ':', ';', '"', '\'', '|', '\\', '>', '<', ',', '/'
		                                               	, '^', '«', '»', '٪', '٬', '٫', '،'
		                                               };

		/// <summary>
		/// Persian/arabic characters
		/// </summary>
		private static readonly char[] _persianChars = { 
														// persian characters
														 'ء', 'آ', 'ا', 'أ', 'ب', 'پ', 'ت', 'ث', 'ج', 'چ'
														, 'ح', 'خ', 'د', 'ذ', 'ر', 'ز', 'ژ', 'س', 'ش', 'ص'
														, 'ض', 'ط', 'ظ', 'ع', 'غ', 'ف', 'ق', 'ک', 'گ', 'ل'
														, 'م', 'ن', 'و', 'ؤ', 'ه', 'ی', 'ئ', 'َ', 'ِ', 'ُ'
														, 'ً', 'ٍ', 'ٌ', 'ّ', 'ْ', 'ٓ', 'ٔ', 'ٕ', 'ٰ',
														
														// arabic characters
														'ۀ', 'ى','ي','ة','ك','ٱ','إ',

														// RTL charcter for both arabic/persian
														'ـ','؟', '؛'
		                                               };

		private static readonly char[] _standardNumbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

		/// <summary>
		/// Modifiying the RTL text to display correctly in LTR context
		/// </summary>
		public static string CorrectPersinRtlToDisplayLtr(string text)
		{
			// grouping the text
			List<TextGroup> textGroups = GroupTheText(text);

			// -----------------
			textGroups = ReorderRtlGroupsForLtr(textGroups);

			// comining the result
			var sb = new StringBuilder();
			foreach (TextGroup g in textGroups)
			{
				sb.Append(g.Text);
			}
			return sb.ToString();
		}

		/// <summary>
		/// Adds Right-To-Left embedding character to the beginning of the text
		/// </summary>
		public static string CorrectPersinRtlToDisplayLtrUnicode(string text)
		{
			if (string.IsNullOrEmpty(text))
				return text;

			const char rleChar = (char)8235;
			if (text[0] != rleChar)
			{
				text = rleChar + text;
			}
			return text;
		}

		/// <summary>
		/// Replacing numbers in text by persian unicode numbers
		/// </summary>
		public static string ConvertToPersianNumbers(string text)
		{
			var sb = new StringBuilder();
			foreach (var c in text)
			{
				if (char.IsNumber(c) && Array.IndexOf(_standardNumbers, c) != -1)
				{
					var num = int.Parse(c.ToString());
					sb.Append((char)((num % 10) + 1776));
				}
				else
				{
					sb.Append(c);
				}
			}
			return sb.ToString();
		}

		/// <summary>
		/// Seperates the text into different groups by different types
		/// </summary>
		private static List<TextGroup> GroupTheText(string text)
		{
			var strColl = new List<TextGroup>();

			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];
				TextGroup group;

				if (strColl.Count > 0)
					group = strColl[strColl.Count - 1];
				else
				{
					group = new TextGroup(TextGroupType.Space);
					strColl.Add(group);
				}

				if (char.IsWhiteSpace(c))
				{
					if (group.Type == TextGroupType.Space)
					{
						group.Text.Append(c);
					}
					else
					{
						group = new TextGroup(TextGroupType.Space);
						group.Text.Append(c);
						strColl.Add(group);
					}
				}
				else if (IsSymbolChar(c))
				{
					// NOTE: every symbol is a group itself
					group = new TextGroup(TextGroupType.Symbol);
					group.Text.Append(c);
					strColl.Add(group);
				}
				else if (char.IsNumber(c))
				{
					if (group.Type == TextGroupType.Number)
					{
						group.Text.Append(c);
					}
					else
					{
						group = new TextGroup(TextGroupType.Number);
						group.Text.Append(c);
						strColl.Add(group);
					}
				}
				else if (IsPersianChar(c))
				{
					if (group.Type == TextGroupType.Persian)
					{
						group.Text.Append(c);
					}
					else
					{
						group = new TextGroup(TextGroupType.Persian);
						group.Text.Append(c);
						strColl.Add(group);
					}
				}
				else
				{
					if (group.Type == TextGroupType.English)
					{
						group.Text.Append(c);
					}
					else
					{
						group = new TextGroup(TextGroupType.English);
						group.Text.Append(c);
						strColl.Add(group);
					}
				}
			}
			return strColl;
		}

		/// <summary>
		/// Reordering RTL groups to display corrently in LTR context
		/// </summary>
		private static List<TextGroup> ReorderRtlGroupsForLtr(List<TextGroup> strGroups)
		{
			var lstFinal = new List<TextGroup>();
			TextGroupType lastGroupType = TextGroupType.Start;
			var lstTemp = new List<TextGroup>();

			// previous command required to skip the upcoming group
			bool skipTheNextGroup = false;

			Func<TextGroupAction?> commandToDo = null;

			for (int i = 0; i < strGroups.Count; i++)
			{
				TextGroupAction textGroupAction = TextGroupAction.FlushLastAndJustAdd;

				TextGroup group = strGroups[i];
				TextGroupType thisGroupType = group.Type;
				TextGroupType? changeLastGroupType = null;

				// -----------------------------
				// Initializations
				int nextItemIndex = i + 1;
				int futureItemIndex = i + 2;
				int previousItemIndex = i - 1;

				TextGroup nextItem = null, futureItem = null;
				TextGroup previousItem = null;
				if (nextItemIndex < strGroups.Count)
				{
					nextItem = strGroups[nextItemIndex];
				}
				if (futureItemIndex < strGroups.Count)
				{
					futureItem = strGroups[futureItemIndex];
				}
				if (previousItemIndex >= 0 && previousItemIndex < strGroups.Count)
				{
					previousItem = strGroups[previousItemIndex];
				}

				// -------------------------
				// conditions
				// -------------------------

				// the previous action requires this group to be skipped
				if (skipTheNextGroup)
				{
					textGroupAction = TextGroupAction.SkipThis;
					skipTheNextGroup = false;
				}
				else if (thisGroupType == TextGroupType.Space)
				{
					// Status = 1
					if (lastGroupType == TextGroupType.Start)
					{
						textGroupAction = TextGroupAction.FlushLastAndJustAdd;
					}
					else
					{
						textGroupAction = TextGroupAction.FlushLastAndJustAdd;
					}
				}
				else if (thisGroupType == TextGroupType.English)
				{
					if (nextItem != null)
					{
						if (nextItem.Type == TextGroupType.Persian)
						{
							// status 5
							textGroupAction = TextGroupAction.JoinCheckedThenFlush;
						}
						else if (nextItem.Type == TextGroupType.English)
						{
							// impossoble
							textGroupAction = TextGroupAction.JoinThisCheckLast;
						}
						else if (nextItem.Type == TextGroupType.Number)
						{
							// staus  = 16
							textGroupAction = TextGroupAction.JoinThisCheckLast;
						}
						else if (nextItem.Type == TextGroupType.Symbol)
						{
							// status 15
							textGroupAction = TextGroupAction.JoinThisCheckLast;
						}
						else if (nextItem.Type == TextGroupType.Space)
						{
							if (futureItem == null)
							{
								// status 8
								textGroupAction = TextGroupAction.JoinThisCheckLast;
							}
							else if (futureItem.Type == TextGroupType.Persian)
							{
								// status = ??
								textGroupAction = TextGroupAction.JoinCheckedThenFlush;
							}
							else if (futureItem.Type == TextGroupType.Symbol)
							{
								TextGroupType? foundType;
								int foundIndex;
								bool has = HasInFuture(strGroups, i, new[] { TextGroupType.English, },
													   new[] { TextGroupType.Symbol, TextGroupType.Number, TextGroupType.Space, },
													   out foundType, out foundIndex);
								if (has)
								{
									// status ??
									textGroupAction = TextGroupAction.JoinThisAndNextCheckLast;
									skipTheNextGroup = true;
								}
								else
								{
									// status = ??
									textGroupAction = TextGroupAction.JoinThisCheckLast;
								}
							}
							else
							{
								// status 14
								textGroupAction = TextGroupAction.JoinThisAndNextCheckLast;
								skipTheNextGroup = true;
							}
						}
						else
						{
							throw new Exception("Unknown condition!");
							textGroupAction = TextGroupAction.FlushLastAndJustAdd;
						}
					}
					else
					{
						// statsu =??
						textGroupAction = TextGroupAction.JoinCheckedThenFlush;
					}
				}
				else if (thisGroupType == TextGroupType.Persian)
				{
					if (nextItem != null)
					{
						if (nextItem.Type == TextGroupType.Persian)
						{
							// statusu =self
							textGroupAction = TextGroupAction.JoinThisCheckLast;
						}
						else if (nextItem.Type == TextGroupType.English)
						{
							// staus 4
							textGroupAction = TextGroupAction.JoinCheckedThenFlush;
						}
						else if (nextItem.Type == TextGroupType.Number)
						{
							// staus  = 7
							textGroupAction = TextGroupAction.JoinThisCheckLast;
						}
						else if (nextItem.Type == TextGroupType.Symbol)
						{
							// status 9
							textGroupAction = TextGroupAction.JoinThisCheckLast;
						}
						else if (nextItem.Type == TextGroupType.Space)
						{
							if (futureItem == null)
							{
								// status 8
								textGroupAction = TextGroupAction.JoinThisCheckLast;
							}
							else if (futureItem.Type == TextGroupType.English)
							{
								// status = ??
								textGroupAction = TextGroupAction.JoinCheckedThenFlush;
							}
							else if (futureItem.Type == TextGroupType.Symbol)
							{
								TextGroupType? foundType;
								int foundIndex;
								bool has = HasInFuture(strGroups, i, new[] { TextGroupType.Persian, },
													   new[] { TextGroupType.Symbol, TextGroupType.Number, TextGroupType.Space, },
													   out foundType, out foundIndex);
								if (has)
								{
									// status ??
									textGroupAction = TextGroupAction.JoinThisAndNextCheckLast;
									skipTheNextGroup = true;
								}
								else
								{
									// status = ??
									textGroupAction = TextGroupAction.JoinThisCheckLast;
								}
							}
							else
							{
								// status 10
								textGroupAction = TextGroupAction.JoinThisAndNextCheckLast;
								skipTheNextGroup = true;
							}
						}
						else
						{
							throw new Exception("Unknown condition!");
							textGroupAction = TextGroupAction.FlushLastAndJustAdd;
						}
					}
					else
					{
						// statsu =??
						textGroupAction = TextGroupAction.JoinCheckedThenFlush;
					}
				}
				else if (thisGroupType == TextGroupType.Symbol)
				{
					int foundIndex;
					TextGroupType? prevAlphabet;

					// is there any alphabet in previous secions?
					bool hasAlphabetInPrevious = HasInPrevious(strGroups, i,
															   new[] { TextGroupType.Persian, TextGroupType.English, },
															   new[] { TextGroupType.Symbol, TextGroupType.Space, TextGroupType.Number, },
															   out prevAlphabet,
															   out foundIndex);

					if (hasAlphabetInPrevious)
					{
						TextGroupType? nextAlphabet;
						// is there any same alphabet in coming groups?
						bool hasAlphabetInNext = HasInFuture(strGroups, i,
															 new[] { prevAlphabet.Value, },
															 new[] { TextGroupType.Symbol, TextGroupType.Space, TextGroupType.Number, },
															 out nextAlphabet,
															 out foundIndex);

						if (hasAlphabetInNext)
						{
							// status=??
							textGroupAction = TextGroupAction.DoCommand;
							commandToDo =
								() => // do TextGroupAction
								{
									// let the loop continute on the alphabet, intead of anything else
									foundIndex--;


									for (int index = i; index <= foundIndex; index++)
									{
										// adding all the groups in the range/ they behave same
										lstTemp.Add(strGroups[index]);
									}

									TextGroup combined = JoinGroups(lstTemp);
									lstTemp.Clear();
									lstTemp.Add(combined);


									// changing index of current group/ skiping added groups
									i = foundIndex;

									// changing the last group type
									group = combined;

									// don't do anything
									return TextGroupAction.SkipThis;
								};
						}
						else
						{
							// no alphabet in upcoming group so ,bracket kind characters should be switched
							group.Text = ReverseBrackedKindChar(group.Text);

							if (prevAlphabet != null && prevAlphabet.Value == TextGroupType.English)
							{
								// in english, if there is number after symbol, orders changes

								TextGroupType? hasNumber;
								// is there any same alphabet in coming groups?
								bool hasNumberInNext = HasInFuture(strGroups, i,
																   new[] { TextGroupType.Number, },
																   new[] { TextGroupType.Symbol, TextGroupType.Space, TextGroupType.Number, },
																   out nextAlphabet,
																   out foundIndex);
								if (hasNumberInNext)
								{
									textGroupAction = TextGroupAction.JoinThisDontCheck;
									changeLastGroupType = prevAlphabet.Value;

									//// next group checks the type, needed
									group.Type = prevAlphabet.Value;
								}
								else
								{
									// status = 17, 18, 19, 20, 2
									textGroupAction = TextGroupAction.FlushLastAndJustAdd;
								}
							}
							else
							{
								// status = 17, 18, 19, 20, 2
								textGroupAction = TextGroupAction.FlushLastAndJustAdd;
							}
						}
					}
					else
					{
						// status = 17, 18, 19, 20, 2
						textGroupAction = TextGroupAction.FlushLastAndJustAdd;
					}
				}
				else if (thisGroupType == TextGroupType.Number)
				{
					int foundIndex;
					TextGroupType? prevAlphabet;

					// is there any alphabet in previous secions?
					bool hasAlphabetInPrevious = HasInPrevious(strGroups, i,
															   new[] { TextGroupType.Persian, TextGroupType.English, },
															   new[] { TextGroupType.Symbol, TextGroupType.Space, TextGroupType.Number, },
															   out prevAlphabet,
															   out foundIndex);

					if (hasAlphabetInPrevious)
					{
						TextGroupType? nextAlphabet;
						// is there any same alphabet in coming groups?
						bool hasAlphabetInNext = HasInFuture(strGroups, i,
															 new[] { prevAlphabet.Value, },
															 new[] { TextGroupType.Symbol, TextGroupType.Space, TextGroupType.Number, },
															 out nextAlphabet,
															 out foundIndex);
						if (hasAlphabetInNext)
						{
							textGroupAction = TextGroupAction.DoCommand;
							commandToDo =
								() => // do TextGroupAction
								{
									// let the loop continute on the alphabet, intead of anything else
									foundIndex--;


									for (int index = i; index <= foundIndex; index++)
									{
										// adding all the groups in the range/ they behave same
										lstTemp.Add(strGroups[index]);
									}

									TextGroup combined = JoinGroups(lstTemp);
									lstTemp.Clear();
									lstTemp.Add(combined);


									// changing index of current group/ skiping added groups
									i = foundIndex;

									// chaning the last group type
									group = combined;

									// don't do anything
									return TextGroupAction.SkipThis;
								};
						}
						else
						{
							// is previous item alphabet?
							if (previousItem != null &&
								(previousItem.Type == TextGroupType.Persian || previousItem.Type == TextGroupType.English))
							{
								// status =??
								textGroupAction = TextGroupAction.JoinThisDontCheck;
								changeLastGroupType = previousItem.Type;

								// next group checks the type, needed
								group.Type = previousItem.Type;
							}
							else
							{
								// is next item alphabet?
								if (nextItem != null && (nextItem.Type == TextGroupType.Persian || nextItem.Type == TextGroupType.English))
								{
									// status =??
									textGroupAction = TextGroupAction.JoinThisDontCheck;
									changeLastGroupType = nextItem.Type;

									// next group checks the type, needed
									group.Type = nextItem.Type;
								}
								else
								{
									// status = ??
									textGroupAction = TextGroupAction.JoinThisDontCheck;
								}
							}
						}
					}
					else
					{
						if (nextItem != null && (nextItem.Type == TextGroupType.Persian || nextItem.Type == TextGroupType.English))
						{
							// status =??
							textGroupAction = TextGroupAction.JoinThisDontCheck;
							changeLastGroupType = nextItem.Type;

							// next group checks the type, needed
							group.Type = nextItem.Type;
						}
						else
						{
							// status = ??
							textGroupAction = TextGroupAction.FlushLastAndJustAdd;
						}
					}
				}
				else
				{
					// status = ??
					textGroupAction = TextGroupAction.FlushLastAndJustAdd;
				}


				// -------------------------
				// performing the specified actions
				// -------------------------

				// if any specific command requred!
				if (textGroupAction == TextGroupAction.DoCommand)
				{
					if (commandToDo != null)
					{
						// setting the result
						textGroupAction = commandToDo() ?? TextGroupAction.SkipThis;
					}
				}

				// the actions
				switch (textGroupAction)
				{
					// ------------------------
					case TextGroupAction.FlushLastAndJustAdd:

						TextGroup combinedFlush = JoinGroups(lstTemp);
						if (combinedFlush != null)
							lstFinal.Add(combinedFlush);
						lstTemp.Clear();

						// just add
						lstFinal.Add(group);
						break;

					// ------------------------
					case TextGroupAction.JoinCheckedThenFlush:

						if (lstTemp.Count > 0 && lstTemp[0].Type != thisGroupType)
						{
							TextGroup combine1 = JoinGroups(lstTemp);
							if (combine1 != null)
								lstFinal.Add(combine1);
							lstTemp.Clear();
						}

						// add to grouop first
						lstTemp.Add(group);
						TextGroup combine2 = JoinGroups(lstTemp);
						if (combine2 != null)
							lstFinal.Add(combine2);
						lstTemp.Clear();
						break;

					// ------------------------
					case TextGroupAction.JoinThisCheckLast:

						// check the previous type, if it differs then flush it!
						if (lstTemp.Count > 0 && lstTemp[0].Type != thisGroupType)
						{
							TextGroup combine = JoinGroups(lstTemp);
							if (combine != null)
								lstFinal.Add(combine);
							lstTemp.Clear();
						}
						lstTemp.Add(group);

						break;

					// ------------------------
					case TextGroupAction.JoinThisAndNextCheckLast:
						// check the previous type, if it differs then flush it!
						if (lstTemp.Count > 0 && lstTemp[0].Type != thisGroupType)
						{
							TextGroup combine = JoinGroups(lstTemp);
							if (combine != null)
								lstFinal.Add(combine);
							lstTemp.Clear();
						}
						lstTemp.Add(group);
						if (nextItem != null)
							lstTemp.Add(nextItem);

						break;

					// ------------------------
					case TextGroupAction.JoinThisDontCheck:

						lstTemp.Add(group);
						break;

					// ------------------------
					case TextGroupAction.DoCommand:
						// already done!
						break;

					// ------------------------
					case TextGroupAction.SkipThis:
						// do nothing!
						break;
				}

				// saving last group type
				// check if custom type is requested
				if (changeLastGroupType != null)
				{
					lastGroupType = changeLastGroupType.Value;
				}
				else
				{
					lastGroupType = group.Type;
				}
			}

			TextGroup combineFinal = JoinGroups(lstTemp);
			if (combineFinal != null)
				lstFinal.Add(combineFinal);

			lstFinal.Reverse();
			return lstFinal;
		}

		/// <summary>
		/// Checks previous groups if there is at least one of specified types
		/// </summary>
		private static bool HasInPrevious(IList<TextGroup> groups,
										  int currentIndex,
										  TextGroupType[] hasThese,
										  TextGroupType[] ignoreThese,
										  out TextGroupType? foundType,
										  out int foundIndex)
		{
			foundIndex = currentIndex;
			foundType = null;
			for (int i = currentIndex - 1; i >= 0; i--)
			{
				TextGroup gt = groups[i];
				// has one of these?
				if (Array.IndexOf(hasThese, gt.Type) != -1)
				{
					foundType = gt.Type;
					foundIndex = i;
					return true;
				}

				// if not, then it should be one of these
				if (Array.IndexOf(ignoreThese, gt.Type) != -1)
				{
					continue;
				}
				else
				{
					// or otherwise we exit
					return false;
				}
			}
			return false;
		}

		/// <summary>
		/// Checks upcoming groups if there is at least one of specified types
		/// </summary>
		private static bool HasInFuture(IList<TextGroup> groups,
										int currentIndex,
										TextGroupType[] hasThese,
										TextGroupType[] ignoreThese,
										out TextGroupType? foundType,
										out int foundIndex)
		{
			foundIndex = currentIndex;
			foundType = null;
			for (int i = currentIndex + 1; i < groups.Count; i++)
			{
				TextGroup gt = groups[i];
				// has one of these?
				if (Array.IndexOf(hasThese, gt.Type) != -1)
				{
					foundType = gt.Type;
					foundIndex = i;
					return true;
				}

				// if not, then it should be one of these
				if (Array.IndexOf(ignoreThese, gt.Type) != -1)
				{
					continue;
				}
				else
				{
					// or otherwise we exit
					return false;
				}
			}
			return false;
		}

		/// <summary>
		/// Joining groups text, uses first group type as result group type
		/// </summary>
		private static TextGroup JoinGroups(IList<TextGroup> groups)
		{
			if (groups.Count == 0)
				return null;

			// the new group type
			TextGroupType type = groups[0].Type;

			var sb = new StringBuilder();
			foreach (TextGroup g in groups)
			{
				sb.Append(g.Text);
			}
			return new TextGroup(type, sb);
		}


		/// <summary>
		/// Reversing bracket character
		/// </summary>
		private static char ReverseBrackedKindChar(char c)
		{
			switch (c)
			{
				case '(':
					return ')';
				case ')':
					return '(';
				case '[':
					return ']';
				case ']':
					return '[';
				case '{':
					return '}';
				case '}':
					return '{';
				case '>':
					return '<';
				case '<':
					return '>';
				case '«':
					return '»';
				case '»':
					return '«';
				default:
					return c;
			}
		}

		/// <summary>
		/// Reversing bracket character in the string of the builder
		/// </summary>
		private static StringBuilder ReverseBrackedKindChar(StringBuilder strBuilder)
		{
			var sb = new StringBuilder();
			foreach (char c in strBuilder.ToString())
			{
				sb.Append(ReverseBrackedKindChar(c));
			}
			return sb;
		}

		/// <summary>
		/// Is character symbol
		/// </summary>
		private static bool IsSymbolChar(char c)
		{
			return char.IsSymbol(c) || Array.IndexOf(_neutralChars, c) != -1;
		}

		/// <summary>
		/// Is character a persian character
		/// </summary>
		private static bool IsPersianChar(char c)
		{
			return Array.IndexOf(_persianChars, c) != -1;
		}

		#region Nested type: TextGroup

		/// <summary>
		/// The group of same type characters
		/// </summary>
		private class TextGroup
		{
			public StringBuilder Text;
			public TextGroupType Type;

			public TextGroup(TextGroupType type)
			{
				Type = type;
				Text = new StringBuilder();
			}

			public TextGroup(TextGroupType type, StringBuilder text)
			{
				Type = type;
				Text = text;
			}

			public override string ToString()
			{
				return Type.ToString() + "=" + Text;
			}
		}

		#endregion

		#region Nested type: TextGroupAction

		/// <summary>
		/// What action is going to perform
		/// </summary>
		private enum TextGroupAction
		{
			/// <summary>
			/// Flush combined groups then just add this to finals
			/// </summary>
			FlushLastAndJustAdd,

			/// <summary>
			/// Join this group to the same previous groups and flush to final
			/// </summary>
			JoinCheckedThenFlush,

			/// <summary>
			/// Join this group to the same previous groups
			/// </summary>
			JoinThisCheckLast,

			/// <summary>
			/// Join this and next group to the same previous groups
			/// </summary>
			JoinThisAndNextCheckLast,

			/// <summary>
			/// Join this to the previous group. (Unchecked)
			/// </summary>
			JoinThisDontCheck,

			/// <summary>
			/// Use this, if the inexer requires to be modified
			/// </summary>
			DoCommand,

			/// <summary>
			/// Skip this group
			/// </summary>
			SkipThis,
		}

		#endregion

		#region Nested type: TextGroupType

		private enum TextGroupType
		{
			/// <summary>
			/// We are in the begining of the text
			/// </summary>
			Start,
			Persian,
			Symbol,
			Space,
			Number,
			English
		}

		#endregion
	}
}