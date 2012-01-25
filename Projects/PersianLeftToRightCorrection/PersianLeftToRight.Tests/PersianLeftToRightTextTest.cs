using PersianLeftToRightCorrection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace PersianLeftToRight.Tests
{
    
    
    /// <summary>
    ///This is a test class for PersianLeftToRightTextTest and is intended
    ///to contain all PersianLeftToRightTextTest Unit Tests
    ///</summary>
	[TestClass()]
	public class PersianLeftToRightTextTest
	{


		private TestContext _testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return _testContextInstance;
			}
			set
			{
				_testContextInstance = value;
			}
		}

		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion


 		/// <summary>
		///A test for CorrectPersinForLTR
		///</summary>
		[TestMethod()]
		public void CorrectPersinForLtrTest()
		{
			string text;  
			string expected;


			// -------------------------
			text = "من ";
			expected = " من";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "Hi ";
			expected = " Hi";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));


			// -------------------------
			text = "من.";
			expected = ".من";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "hi.";
			expected = ".hi";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "hi. you";
			expected = "hi. you";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "من و you";
			expected = "you من و";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "a^b";
			expected = "a^b";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));


			// -------------------------
			text = "من و you ";
			expected = " you من و";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = ". ";
			expected = " .";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text) );

			// -------------------------
			text = "";
			expected = "";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "یک//دو";
			expected = "یک//دو";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));


			// -------------------------
			text = "me ... and";
			expected = "me ... and";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "یکی ... و";
			expected = "یکی ... و";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "تست . ";
			expected = " . تست";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = " . . . خوب ";
			expected = " خوب . . . ";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = " . . . خوب";
			expected = "خوب . . . ";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));


			// -------------------------
			text = "یک///";
			expected = "///یک";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "- متن -";
			expected = "- متن -";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "من/تو";
			expected = "من/تو";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "...خوب";
			expected = "خوب...";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "متن ... یک دوست";
			expected = "متن ... یک دوست";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "hi ... hello I'm here.";
			expected = ".hi ... hello I'm here";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "یک..دو";
			expected = "یک..دو";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "Hi. ";
			expected = " .Hi";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "یک1.دو";
			expected = "یک1.دو";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "تست 1+1=2 می شود";
			expected = "تست 1+1=2 می شود";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "plus 1+1=2 equals";
			expected = "plus 1+1=2 equals";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));


			// -------------------------
			text = "plus 1 +1 =2 equals";
			expected = "plus 1 +1 =2 equals";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "yes12";
			expected = "yes12";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "متن1234";
			expected = "متن1234";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "1 نفر    ";
			expected = "    نفر 1";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "1نفر   ";
			expected = "   1نفر";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "1مردthat";
			expected = "that1مرد";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "1 یک 2 دو";
			expected = "یک 2 دو 1";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "1یک 2";
			expected = "1یک 2";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "1man 2";
			expected = "1man 2";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "من+ 1";
			expected = "1 +من";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "من1 +";
			expected = "+ من1";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "متن1 man";
			expected = "man متن1";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// not valid test------------ Numbers behave different in RLT/LTR contexts
			//// -------------------------
			//text = "متنtest1";
			//expected = "testمتن1";
			//Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinForLTR(text));

			// not valid test------------ Numbers behave different in RLT/LTR contexts
			//// -------------------------
			//text = "یکone1";
			//expected = "oneیک1";
			//Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinForLTR(text));

			// not valid test ------------ Numbers behave different in RLT/LTR contexts
			//// -------------------------
			//text = "1 یک 2 دو 3 چهار";
			//expected = "1 یک 2 دو 3 چهار";
			//Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinForLTR(text));

			// -------------------------
			text = "1یک 2 دو 3 چهار";
			expected = "1یک 2 دو 3 چهار";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));

			// -------------------------
			text = "x+3";
			expected = "x+3";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));
			
			// -------------------------
			text = "من x+3";
			expected = "x+3 من";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));
			
			// -------------------------
			text = "من>  ";
			expected = "  <من";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));
			
			// -------------------------
			text = "man> ";
			expected = " <man";
			Assert.AreEqual(expected, PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(text));
		}


	}
}
