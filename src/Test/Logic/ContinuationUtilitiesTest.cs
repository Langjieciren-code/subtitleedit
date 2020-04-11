﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nikse.SubtitleEdit.Core;
using System;
using Nikse.SubtitleEdit.Core.Enums;

namespace Test.Logic
{
    [TestClass]
    public class ContinuationUtilitiesTest
    {
        [TestMethod]
        public void SanitizeString1()
        {
            string line1 = "{\an8}<i>'This is a test.'</i>\n \n _";
            string line1Actual = ContinuationUtilities.SanitizeString(line1);
            string line1Expected = "This is a test.";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void SanitizeString2()
        {
            string line1 = "<font color=\"#000000\"><i>Just testin'</i></font>";
            string line1Actual = ContinuationUtilities.SanitizeString(line1);
            string line1Expected = "Just testin'";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void SanitizeString2B()
        {
            string line1 = "<font color=\"#000000\"><i>But this is an ending quote.'</i></font>";
            string line1Actual = ContinuationUtilities.SanitizeString(line1);
            string line1Expected = "But this is an ending quote.";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void SanitizeString3()
        {
            string line1 = "'s Avonds gaat de zon onder.'";
            string line1Actual = ContinuationUtilities.SanitizeString(line1);
            string line1Expected = "'s Avonds gaat de zon onder.";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void SanitizeString4()
        {
            string line1 = "MAN IN BACKGROUND: this is a test: like this.";
            string line1Actual = ContinuationUtilities.SanitizeString(line1);
            string line1Expected = "this is a test: like this.";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void SanitizeString4B()
        {
            string line1 = "Unit tests: used to test code.";
            string line1Actual = ContinuationUtilities.SanitizeString(line1);
            string line1Expected = "Unit tests: used to test code.";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void SanitizeString5()
        {
            string line1 = ">> ...this is a test.";
            string line1Actual = ContinuationUtilities.SanitizeString(line1);
            string line1Expected = "...this is a test.";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void SanitizeString6()
        {
            string line1 = "for example: ...this is a test.";
            string line1Actual = ContinuationUtilities.SanitizeString(line1);
            string line1Expected = "for example: ...this is a test.";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void SanitizeString7()
        {
            string line1 = "- ...this is a test.\n<i>- Another test...</i>";
            string line1Actual = ContinuationUtilities.SanitizeString(line1);
            string line1Expected = "...this is a test.\n- Another test...";
            Assert.AreEqual(line1Expected, line1Actual);
        }
        
        [TestMethod]
        public void SanitizeString8()
        {
            string line1 = "- this is a test.\n<i>- Another test -</i>";
            string line1Actual = ContinuationUtilities.SanitizeString(line1, false);
            string line1Expected = "- this is a test.\n- Another test -";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void ExtractParagraphOnly1()
        {
            string line1 = "{\an8}<i>'This is a test.'</i>\n \n _";
            string line1Actual = ContinuationUtilities.ExtractParagraphOnly(line1);
            string line1Expected = "<i>'This is a test.'</i>";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void ReplaceFirstOccurrence1()
        {
            string line1 = "Mark and Fred. Mark is the strongest.";
            string line1Actual = ContinuationUtilities.ReplaceFirstOccurrence(line1, "Mark", "...Mark");
            string line1Expected = "...Mark and Fred. Mark is the strongest.";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void ReplaceLastOccurrence1()
        {
            string line1 = "You ask who's the strongest. Mark is the strongest";
            string line1Actual = ContinuationUtilities.ReplaceLastOccurrence(line1, "strongest", "strongest...");
            string line1Expected = "You ask who's the strongest. Mark is the strongest...";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void ShouldAddSuffix1()
        {
            string line1 = "This is a test.";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            bool line1Actual = ContinuationUtilities.ShouldAddSuffix(line1, profile);
            Assert.IsFalse(line1Actual);
        }

        [TestMethod]
        public void ShouldAddSuffix2()
        {
            string line1 = "This is a test:";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            bool line1Actual = ContinuationUtilities.ShouldAddSuffix(line1, profile);
            Assert.IsFalse(line1Actual);
        }

        [TestMethod]
        public void ShouldAddSuffix3()
        {
            string line1 = "<i>This is a test,</i>\n \n_";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            bool line1Actual = ContinuationUtilities.ShouldAddSuffix(line1, profile);
            Assert.IsTrue(line1Actual);
        }

        [TestMethod]
        public void AddSuffixIfNeeded1()
        {
            string line1 = "This is a test,";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            string line1Actual = ContinuationUtilities.AddSuffixIfNeeded(line1, profile, false);
            string line1Expected = "This is a test...";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void AddSuffixIfNeeded2()
        {
            string line1 = "This is a test,";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.NoneLeadingTrailingDots);
            string line1Actual = ContinuationUtilities.AddSuffixIfNeeded(line1, profile, false);
            string line1Expected = "This is a test,";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void AddSuffixIfNeeded3()
        {
            string line1 = "This is a test,";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.NoneLeadingTrailingDots);
            string line1Actual = ContinuationUtilities.AddSuffixIfNeeded(line1, profile, true);
            string line1Expected = "This is a test...";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void AddSuffixIfNeeded4()
        {
            string line1 = "{\an8}<i>This is a test,</i>\n \n_";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            string line1Actual = ContinuationUtilities.AddSuffixIfNeeded(line1, profile, false);
            string line1Expected = "{\an8}<i>This is a test...</i>\n \n_";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void AddSuffixIfNeeded5()
        {
            string line1 = "<i>- What is this?</i>\n<i>- This is a test,</i>";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            string line1Actual = ContinuationUtilities.AddSuffixIfNeeded(line1, profile, false);
            string line1Expected = "<i>- What is this?</i>\n<i>- This is a test...</i>";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void AddSuffixIfNeeded6()
        {
            string line1 = "<i>- What is this?\n- This is a test,</i>";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            string line1Actual = ContinuationUtilities.AddSuffixIfNeeded(line1, profile, false);
            string line1Expected = "<i>- What is this?\n- This is a test...</i>";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void AddSuffixIfNeeded7()
        {
            string line1 = "<i>- What are you doing?\n- I'm just testin'</i>";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            string line1Actual = ContinuationUtilities.AddSuffixIfNeeded(line1, profile, false);
            string line1Expected = "<i>- What are you doing?\n- I'm just testin'...</i>";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void AddSuffixIfNeeded8()
        {
            string line1 = "- What is this?\n- This is a <b>test</b>";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            string line1Actual = ContinuationUtilities.AddSuffixIfNeeded(line1, profile, false);
            string line1Expected = "- What is this?\n- This is a <b>test</b>...";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void AddSuffixIfNeeded9()
        {
            string line1 = "<i>- What are you doing?\n- This is a <b>test</b> -</i>";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            string line1Actual = ContinuationUtilities.AddSuffixIfNeeded(line1, profile, false);
            string line1Expected = "<i>- What are you doing?\n- This is a <b>test</b>...</i>";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void AddSuffixIfNeeded10()
        {
            string line1 = "<i>- What are you doing?\n- This is a <b>test</b> -</i>";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDash);
            string line1Actual = ContinuationUtilities.AddSuffixIfNeeded(line1, profile, false);
            string line1Expected = "<i>- What are you doing?\n- This is a <b>test</b> -</i>";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void AddSuffixIfNeeded11()
        {
            string line1 = "What are you <i>do</i>ing,";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            string line1Actual = ContinuationUtilities.AddSuffixIfNeeded(line1, profile, false);
            string line1Expected = "What are you <i>do</i>ing...";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void AddPrefixIfNeeded1()
        {
            string line1 = "this is a test.";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            string line1Actual = ContinuationUtilities.AddPrefixIfNeeded(line1, profile, false);
            string line1Expected = "...this is a test.";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void AddPrefixIfNeeded2()
        {
            string line1 = "this is a test.";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.NoneLeadingTrailingDots);
            string line1Actual = ContinuationUtilities.AddPrefixIfNeeded(line1, profile, false);
            string line1Expected = "this is a test.";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void AddPrefixIfNeeded3()
        {
            string line1 = "this is a test.";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.NoneLeadingTrailingDots);
            string line1Actual = ContinuationUtilities.AddPrefixIfNeeded(line1, profile, true);
            string line1Expected = "...this is a test.";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void AddPrefixIfNeeded4()
        {
            string line1 = "{\an8}<i>this is a test.</i>\n \n_";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            string line1Actual = ContinuationUtilities.AddPrefixIfNeeded(line1, profile, true);
            string line1Expected = "{\an8}<i>...this is a test.</i>\n \n_";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void AddPrefixIfNeeded5()
        {
            string line1 = "<i>- this is a test.\n-A what do you say?";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            string line1Actual = ContinuationUtilities.AddPrefixIfNeeded(line1, profile, true);
            string line1Expected = "<i>- ...this is a test.\n-A what do you say?";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void AddPrefixIfNeeded6()
        {
            string line1 = "- <b>this</b> is a test.\n-A what do you say?";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            string line1Actual = ContinuationUtilities.AddPrefixIfNeeded(line1, profile, true);
            string line1Expected = "- ...<b>this</b> is a test.\n-A what do you say?";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void AddPrefixIfNeeded7()
        {
            string line1 = "- <b>this</b> is a test.\n-A what do you say?";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDash);
            string line1Actual = ContinuationUtilities.AddPrefixIfNeeded(line1, profile, true);
            string line1Expected = "- <b>this</b> is a test.\n-A what do you say?";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void AddPrefixIfNeeded8()
        {
            string line1 = "<i>do</i>ing is what I meant.";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            string line1Actual = ContinuationUtilities.AddPrefixIfNeeded(line1, profile, true);
            string line1Expected = "...<i>do</i>ing is what I meant.";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void AddPrefixIfNeeded9()
        {
            string line1 = "MAN: <i>do</i>ing is what I meant.";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            string line1Actual = ContinuationUtilities.AddPrefixIfNeeded(line1, profile, true);
            string line1Expected = "MAN: ...<i>do</i>ing is what I meant.";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void RemoveSuffix1()
        {
            string line1 = "<i>This is a test,</i>\n \n_";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            string line1Actual = ContinuationUtilities.RemoveSuffix(line1, profile, false);
            string line1Expected = "<i>This is a test</i>\n \n_";
            Assert.AreEqual(line1Expected, line1Actual);
        }
        
        [TestMethod]
        public void RemoveSuffix2()
        {
            string line1 = "<i>This is a test...</i>\n \n_";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            string line1Actual = ContinuationUtilities.RemoveSuffix(line1, profile, false);
            string line1Expected = "<i>This is a test</i>\n \n_";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void RemoveSuffix3()
        {
            string line1 = "<i>This is a <b>test...</b></i>\n \n_";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            string line1Actual = ContinuationUtilities.RemoveSuffix(line1, profile, false);
            string line1Expected = "<i>This is a <b>test</b></i>\n \n_";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void RemoveSuffix4()
        {
            string line1 = "<i>This is a <b>test</b>...</i>\n \n_";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            string line1Actual = ContinuationUtilities.RemoveSuffix(line1, profile, false);
            string line1Expected = "<i>This is a <b>test</b></i>\n \n_";
            Assert.AreEqual(line1Expected, line1Actual);
        }
        
        [TestMethod]
        public void RemovePrefix1()
        {
            string line1 = "<i>...this is a test.</i>\n \n_";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            string line1Actual = ContinuationUtilities.RemovePrefix(line1, profile);
            string line1Expected = "<i>this is a test.</i>\n \n_";
            Assert.AreEqual(line1Expected, line1Actual);
        }
        
        [TestMethod]
        public void RemovePrefix2()
        {
            string line1 = "<i>...<b>this</b> is a test.</i>\n \n_";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            string line1Actual = ContinuationUtilities.RemovePrefix(line1, profile);
            string line1Expected = "<i><b>this</b> is a test.</i>\n \n_";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void RemovePrefix3()
        {
            string line1 = "<i><b>...this</b> is a test.</i>\n \n_";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            string line1Actual = ContinuationUtilities.RemovePrefix(line1, profile);
            string line1Expected = "<i><b>this</b> is a test.</i>\n \n_";
            Assert.AreEqual(line1Expected, line1Actual);
        }
        
        [TestMethod]
        public void RemovePrefix4()
        {
            string line1 = "- and this is the end.";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            string line1Actual = ContinuationUtilities.RemovePrefix(line1, profile);
            string line1Expected = "and this is the end.";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void RemovePrefix5()
        {
            string line1 = "- ...and this is the end.\n- Really?";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            string line1Actual = ContinuationUtilities.RemovePrefix(line1, profile);
            string line1Expected = "- and this is the end.\n- Really?";
            Assert.AreEqual(line1Expected, line1Actual);
        }

        [TestMethod]
        public void IsNewSentence1()
        {
            string line1 = "This is a new sentence.";
            bool line1Actual = ContinuationUtilities.IsNewSentence(line1);
            Assert.IsTrue(line1Actual);
        }

        [TestMethod]
        public void IsNewSentence2()
        {
            string line1 = "but this is not a new sentence.";
            bool line1Actual = ContinuationUtilities.IsNewSentence(line1);
            Assert.IsFalse(line1Actual);
        }

        [TestMethod]
        public void IsNewSentence3()
        {
            string line1 = "iPhone is the market leader.";
            bool line1Actual = ContinuationUtilities.IsNewSentence(line1);
            Assert.IsTrue(line1Actual);
        }
        
        [TestMethod]
        public void IsNewSentence4()
        {
            string line1 = "'s Avonds gaat de zon onder.";
            bool line1Actual = ContinuationUtilities.IsNewSentence(line1);
            Assert.IsTrue(line1Actual);
        }

        [TestMethod]
        public void IsNewSentence5()
        {
            string line1 = "'s avonds gaat de zon onder.";
            bool line1Actual = ContinuationUtilities.IsNewSentence(line1);
            Assert.IsFalse(line1Actual);
        }

        [TestMethod]
        public void IsNewSentence6()
        {
            string line1 = "¿Habla Español?";
            bool line1Actual = ContinuationUtilities.IsNewSentence(line1);
            Assert.IsTrue(line1Actual);
        }

        [TestMethod]
        public void IsNewSentence7()
        {
            string line1 = "¿habla Español?";
            bool line1Actual = ContinuationUtilities.IsNewSentence(line1);
            Assert.IsFalse(line1Actual);
        }

        [TestMethod]
        public void IsEndOfSentence1()
        {
            string line1 = "This is the end of a sentence.";
            bool line1Actual = ContinuationUtilities.IsEndOfSentence(line1);
            Assert.IsTrue(line1Actual);
        }

        [TestMethod]
        public void IsEndOfSentence2()
        {
            string line1 = "this is the end of a sentence.";
            bool line1Actual = ContinuationUtilities.IsEndOfSentence(line1);
            Assert.IsTrue(line1Actual);
        }
        
        [TestMethod]
        public void IsEndOfSentence3()
        {
            string line1 = "is the end?";
            bool line1Actual = ContinuationUtilities.IsEndOfSentence(line1);
            Assert.IsTrue(line1Actual);
        }

        [TestMethod]
        public void IsEndOfSentence4()
        {
            string line1 = "This is the end!";
            bool line1Actual = ContinuationUtilities.IsEndOfSentence(line1);
            Assert.IsTrue(line1Actual);
        }
        
        [TestMethod]
        public void IsEndOfSentence5()
        {
            string line1 = "This is not the end:";
            bool line1Actual = ContinuationUtilities.IsEndOfSentence(line1);
            Assert.IsFalse(line1Actual);
        }

        [TestMethod]
        public void IsEndOfSentence6()
        {
            string line1 = "This is not the end...";
            bool line1Actual = ContinuationUtilities.IsEndOfSentence(line1);
            Assert.IsFalse(line1Actual);
        }

        [TestMethod]
        public void IsEndOfSentence7()
        {
            string line1 = "This is not the end,";
            bool line1Actual = ContinuationUtilities.IsEndOfSentence(line1);
            Assert.IsFalse(line1Actual);
        }
        
        [TestMethod]
        public void IsEndOfSentence8()
        {
            string line1 = "This is--";
            bool line1Actual = ContinuationUtilities.IsEndOfSentence(line1);
            Assert.IsTrue(line1Actual);
        }

        [TestMethod]
        public void IsEndOfSentence9()
        {
            string line1 = "This is not the end;";
            bool line1Actual = ContinuationUtilities.IsEndOfSentence(line1);
            Assert.IsFalse(line1Actual);
        }

        [TestMethod]
        public void IsEndOfSentence10()
        {
            string line1 = "Ψάξατε πραγματικά αυτό;";
            bool line1Actual = ContinuationUtilities.IsEndOfSentence(line1);
            Assert.IsTrue(line1Actual);
        }

        [TestMethod]
        public void IsAllCaps1()
        {
            string line1 = "THIS IS ALL CAPS.";
            bool line1Actual = ContinuationUtilities.IsAllCaps(line1);
            Assert.IsTrue(line1Actual);
        }

        [TestMethod]
        public void IsAllCaps2()
        {
            string line1 = "This is not. NO, it's NOT! SHUT UP!";
            bool line1Actual = ContinuationUtilities.IsAllCaps(line1);
            Assert.IsFalse(line1Actual);
        }

        [TestMethod]
        public void IsAllCaps3()
        {
            string line1 = "THE NEW iPHONE";
            bool line1Actual = ContinuationUtilities.IsAllCaps(line1);
            Assert.IsTrue(line1Actual);
        }

        [TestMethod]
        public void IsAllCaps4()
        {
            string line1 = "ИГРА В ПРЯТКИ";
            bool line1Actual = ContinuationUtilities.IsAllCaps(line1);
            Assert.IsTrue(line1Actual);
        }

        [TestMethod]
        public void IsItalic1()
        {
            string line1 = "<i>This is italic.</i>";
            bool line1Actual = ContinuationUtilities.IsItalic(line1);
            Assert.IsTrue(line1Actual);
        }

        [TestMethod]
        public void IsItalic2()
        {
            string line1 = "<i>This</i> is italic.";
            bool line1Actual = ContinuationUtilities.IsItalic(line1);
            Assert.IsFalse(line1Actual);
        }

        [TestMethod]
        public void IsItalic3()
        {
            string line1 = "<i>This is italic.\n- Really?</i> Just stop it.";
            bool line1Actual = ContinuationUtilities.IsItalic(line1);
            Assert.IsFalse(line1Actual);
        }

        [TestMethod]
        public void IsItalic4()
        {
            string line1 = "<i>This is italic.\nThis is, too.</i>";
            bool line1Actual = ContinuationUtilities.IsItalic(line1);
            Assert.IsTrue(line1Actual);
        }

        [TestMethod]
        public void IsItalic5()
        {
            string line1 = "<i>This is italic.</i>\n<i>This is, too.</i>";
            bool line1Actual = ContinuationUtilities.IsItalic(line1);
            Assert.IsTrue(line1Actual);
        }

        [TestMethod]
        public void IsItalic6()
        {
            string line1 = "<i>- This is italic.</i>\n<i>- This is, too.</i>";
            bool line1Actual = ContinuationUtilities.IsItalic(line1);
            Assert.IsTrue(line1Actual);
        }

        [TestMethod]
        public void IsItalic7()
        {
            string line1 = "<i>This is italic.</i> <i>This too.</i>";
            bool line1Actual = ContinuationUtilities.IsItalic(line1);
            Assert.IsFalse(line1Actual);
        }

        [TestMethod]
        public void HasPrefix1()
        {
            string line1 = "...this is a prefix.";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            bool line1Actual = ContinuationUtilities.HasPrefix(line1, profile);
            Assert.IsTrue(line1Actual);
        }

        [TestMethod]
        public void HasPrefix2()
        {
            string line1 = "Not here.";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            bool line1Actual = ContinuationUtilities.HasPrefix(line1, profile);
            Assert.IsFalse(line1Actual);
        }

        [TestMethod]
        public void HasPrefix3()
        {
            string line1 = "~~ this is my own prefix.";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            bool line1Actual = ContinuationUtilities.HasPrefix(line1, profile);
            Assert.IsFalse(line1Actual);
        }

        [TestMethod]
        public void HasPrefix4()
        {
            string line1 = "~~ this is my own prefix.";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            profile.Prefix = "~~";
            bool line1Actual = ContinuationUtilities.HasPrefix(line1, profile);
            Assert.IsTrue(line1Actual);
        }

        [TestMethod]
        public void HasSuffix1()
        {
            string line1 = "This is a suffix...";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            bool line1Actual = ContinuationUtilities.HasSuffix(line1, profile);
            Assert.IsTrue(line1Actual);
        }

        [TestMethod]
        public void HasSuffix2()
        {
            string line1 = "Not here.";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            bool line1Actual = ContinuationUtilities.HasSuffix(line1, profile);
            Assert.IsFalse(line1Actual);
        }

        [TestMethod]
        public void HasSuffix3()
        {
            string line1 = "This is my own suffix ~~";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            bool line1Actual = ContinuationUtilities.HasSuffix(line1, profile);
            Assert.IsFalse(line1Actual);
        }

        [TestMethod]
        public void HasSuffix4()
        {
            string line1 = "This is my own suffix ~~";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            profile.Suffix = "~~";
            bool line1Actual = ContinuationUtilities.HasSuffix(line1, profile);
            Assert.IsTrue(line1Actual);
        }

        [TestMethod]
        public void StartsWithConjunction1()
        {
            string line1 = "but is this reality?";
            bool line1Actual = ContinuationUtilities.StartsWithConjunction(line1, "en");
            Assert.IsTrue(line1Actual);
        }

        [TestMethod]
        public void StartsWithConjunction2()
        {
            string line1 = "is a walk in the park.";
            bool line1Actual = ContinuationUtilities.StartsWithConjunction(line1, "en");
            Assert.IsFalse(line1Actual);
        }

        [TestMethod]
        public void StartsWithConjunction3()
        {
            string line1 = "maar dat is ook de waarheid.";
            bool line1Actual = ContinuationUtilities.StartsWithConjunction(line1, "nl");
            Assert.IsTrue(line1Actual);
        }

        [TestMethod]
        public void StartsWithConjunction4()
        {
            string line1 = "is een fluitje van een cent.";
            bool line1Actual = ContinuationUtilities.StartsWithConjunction(line1, "nl");
            Assert.IsFalse(line1Actual);
        }

        [TestMethod]
        public void MergeHelper1()
        {
            string line1 = "This needs to be merged...";
            string line2 = "as smoothly as possible.";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            var result = ContinuationUtilities.MergeHelper(line1, line2, profile, "en");
            string line1Actual = result.Item1;
            string line2Actual = result.Item2;
            string line1Expected = "This needs to be merged";
            string line2Expected = "as smoothly as possible.";
            Assert.AreEqual(line1Expected, line1Actual);
            Assert.AreEqual(line2Expected, line1Actual);
        }
        
        [TestMethod]
        public void MergeHelper2()
        {
            string line1 = "This needs to be merged...";
            string line2 = "...but keeping in mind the conjunctions.";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            var result = ContinuationUtilities.MergeHelper(line1, line2, profile, "en");
            string line1Actual = result.Item1;
            string line2Actual = result.Item2;
            string line1Expected = "This needs to be merged,";
            string line2Expected = "but keeping in mind the conjunctions.";
            Assert.AreEqual(line1Expected, line1Actual);
            Assert.AreEqual(line2Expected, line1Actual);
        }

        [TestMethod]
        public void MergeHelper3()
        {
            string line1 = "This needs to be merged...";
            string line2 = "But this is a new sentence.";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            var result = ContinuationUtilities.MergeHelper(line1, line2, profile, "en");
            string line1Actual = result.Item1;
            string line2Actual = result.Item2;
            string line1Expected = "This needs to be merged...";
            string line2Expected = "But this is a new sentence.";
            Assert.AreEqual(line1Expected, line1Actual);
            Assert.AreEqual(line2Expected, line1Actual);
        }

        [TestMethod]
        public void MergeHelper4()
        {
            string line1 = "The winner is...";
            string line2 = "...Mark.";
            var profile = ContinuationUtilities.GetContinuationProfile(ContinuationStyle.LeadingTrailingDots);
            var result = ContinuationUtilities.MergeHelper(line1, line2, profile, "en");
            string line1Actual = result.Item1;
            string line2Actual = result.Item2;
            string line1Expected = "The winner is";
            string line2Expected = "Mark.";
            Assert.AreEqual(line1Expected, line1Actual);
            Assert.AreEqual(line2Expected, line1Actual);
        }
    }
}
