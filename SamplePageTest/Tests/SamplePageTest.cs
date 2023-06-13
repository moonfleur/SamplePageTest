using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using NUnit.Framework;
using SamplePageTest.Pages;

namespace SamplePageTest
{
    [TestFixture]
    public class SamplePageTest
    {
        private IWebDriver _driver;
        private SamplePage _samplePage;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _samplePage = new SamplePage(_driver);
            _driver.Navigate().GoToUrl("https://www.globalsqa.com/samplepagetest/");
        }

        [Test]
        public void VerifyDisplayedInformationIsCorrect()
        {
            _samplePage.AttachFile("C:\\Users\\Kateryna.Kuzmenko\\Desktop\\radio.png");
            _samplePage.EnterName("Kate Kuzmenko");
            _samplePage.EnterEmail("kkuzmenko@example.com");
            _samplePage.SelectExperience("3-5");
            _samplePage.CheckFunctionalTesting();
            _samplePage.SelectPostGraduate();
            _samplePage.SetComment("This is a test comment.");
            _samplePage.ClickSubmitButton();

            IWebElement confirmationMessage = _driver.FindElement(By.ClassName("contact-form-submission"));
            string messageText = confirmationMessage.Text;

            string expectedName = "Kate Kuzmenko";
            string expectedEmail = "kkuzmenko@example.com";
            string expectedExperience = "3-5";
            string expectedTypeOfTesting = "Functional Testing";
            string expectedEducationLevel = "Post Graduate";
            string expectedComment = "This is a test comment.";

            bool assertFailed = false;

            if (!messageText.Contains(expectedName))
            {
                assertFailed = true;
                Assert.Fail($"Current name is {GetSubstring(messageText, "Name:", "Email:")}, expected name is {expectedName}");
            }

            if (!messageText.Contains(expectedEmail))
            {
                assertFailed = true;
                Assert.Fail($"Current email is {GetSubstring(messageText, "Email:", "Website:")}, expected email is {expectedEmail}");
            }

            if (!messageText.Contains(expectedExperience))
            {
                assertFailed = true;
                Assert.Fail($"Selected experience is {GetSubstring(messageText, "Experience (In Years):", "Expertise ::")}, expected experience is {expectedExperience}");
            }

            if (!messageText.Contains(expectedTypeOfTesting))
            {
                assertFailed = true;
                Assert.Fail($"Selected type of testing is {GetSubstring(messageText, "Expertise ::", "Education:")}, expected type of testing is {expectedTypeOfTesting}");
            }

            if (!messageText.Contains(expectedEducationLevel))
            {
                assertFailed = true;
                Assert.Fail($"Selected level of education is {GetSubstring(messageText, "Education:", "Comment:")}, expected level of education is {expectedEducationLevel}");
            }

            if (messageText.Contains(expectedComment))
            {
                assertFailed = true;
                Assert.Fail($"Entered comment is {GetSubstring(messageText, "Comment:", "")}, expected comment is {expectedComment}");
            }

            if (!assertFailed)
            {
                Assert.Pass();
            }

        //Assert.IsTrue(messageText.Contains("Kate Kuzmenko"), $"Current name is {messageText}, expected name is {expectedName}");
        //Assert.IsTrue(messageText.Contains("kkuzmenko@example.com"), $"Current email is {messageText}, expected email is {expectedEmail}");
        //Assert.IsTrue(messageText.Contains("3-5"), $"Selected experience is {messageText}, expected experience is {expectedExperience}");
        //Assert.IsTrue(messageText.Contains("Functional Testing"), $"Selected type of testing is {messageText}, expected type of testing is {expectedTypeOfTesting}");
        //Assert.IsTrue(messageText.Contains("Post Graduate"), $"Selected level of education is {messageText}, expected level of education is {expectedEducationLevel}");
        //Assert.IsTrue(messageText.Contains("This is a test comment."), $"Entered comment is {messageText}, expected comment is {expectedComment}");
        
        }
        private string GetSubstring(string source, string start, string end)
        {
            int startIndex = source.IndexOf(start) + start.Length;
            int endIndex = end != "" ? source.IndexOf(end) : source.Length;
            return source.Substring(startIndex, endIndex - startIndex).Trim();
        }

        [TearDown] 
        public void Teardown() 
        {
            _driver.Quit();
        }
    }
}
