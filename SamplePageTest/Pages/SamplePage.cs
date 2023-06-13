using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;

namespace SamplePageTest.Pages
{
    public class SamplePage
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        private IWebElement _fileInputLocator => _driver.FindElement(By.Id("g2599-name"));
        public IWebElement _nameInputLocator => _driver.FindElement(By.Id("g2599-name"));
        public IWebElement _emailInputLocator => _driver.FindElement(By.Id("g2599-email"));
        public IWebElement _experienceDropdownLocator => _driver.FindElement(By.Id("g2599-experienceinyears"));
        public IWebElement _functionalTestingCheckboxLocator => _driver.FindElement(By.XPath("//*[@id='contact-form-2599']/form/div[5]/label[2]"));
        public IWebElement _educationRadioLocator => _driver.FindElement(By.XPath("//*[@id='contact-form-2599']/form/div[6]/label[3]"));
        public IWebElement _commentTextAreaLocator => _driver.FindElement(By.Id("contact-form-comment-g2599-comment"));
        public IWebElement _submitButtonLocator => _driver.FindElement(By.ClassName("pushbutton-wide"));

        public SamplePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void AttachFile(string filePath)
        {
            _fileInputLocator.Click();

            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            // Очікування відображення вікна вибору файлу
            IWebElement fileUploadDialog = _wait.Until(driver =>
            {
                try
                {
                    IWebElement dialog = driver.FindElement(By.XPath("//input[@type='file']"));
                    if (dialog.Displayed)
                        return dialog;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
                return null;
            });

            // Введення шляху до файлу та очікування закриття вікна
            fileUploadDialog.SendKeys(filePath);
        }

        public void EnterName(string name)
        {
            _nameInputLocator.SendKeys(name);
        }

        public void EnterEmail(string email)
        {
            _emailInputLocator.SendKeys(email);
        }

        public void SelectExperience(string experience)
        {
            _experienceDropdownLocator.SendKeys(experience);
        }

        public void CheckFunctionalTesting()
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)_driver;
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView({ behavior: 'auto', block: 'center', inline: 'center' });", _functionalTestingCheckboxLocator);

            _functionalTestingCheckboxLocator.Click();
        }

        public void SelectPostGraduate()
        {
            //_actions = new Actions(_driver);
            //_actions.MoveToElement(_educationRadioLocator);
            //_actions.Perform();

            //WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            //wait.Until(driver => _educationRadioLocator.Displayed && _educationRadioLocator.Enabled);

            //_educationRadioLocator.Click();

            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView({ behavior: 'auto', block: 'center', inline: 'center' });", _educationRadioLocator);

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => _educationRadioLocator.Displayed && _educationRadioLocator.Enabled);

            _educationRadioLocator.Click();

            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
        }

        public void SetComment(string comment)
        {
            _commentTextAreaLocator.SendKeys(comment);
        }

        public void ClickSubmitButton()
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)_driver;
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView({ behavior: 'auto', block: 'center', inline: 'center' });", _submitButtonLocator);

            _submitButtonLocator.Click();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }
    }
}
