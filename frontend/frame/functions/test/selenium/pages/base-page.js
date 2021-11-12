const { Builder, By, until } = require('selenium-webdriver');
const firefox = require('selenium-webdriver/firefox');
const config = require('../helper/selenium-config');

class BasePage {
  constructor(driver, url, id) {
    this.driver = driver;
    this.id = id;
    this.url = url;
  }

  async checkText(id, text, timeout) {
    const element = await this.driver.findElement(By.id(id));
    if (element) {
      await this.driver.wait(until.elementTextIs(element, text), timeout || config.timeout.ajax);
      return await element.getText() === text;
    }

    return false;
  }

  async checkPageId(timeout) {
    const element = await this.driver.wait(
      until.elementLocated(By.id(this.id)),
      timeout || config.timeout.checkPageId,
    );

    return element !== null && await element.getAttribute('id') === this.id;
  }

  async clickOn(cssSelector) {
    const element = await this.driver.findElement(By.css(cssSelector));
    return element.click();
  }

  async getText(cssSelector) {
    const element = await this.driver.findElement(By.css(cssSelector));
    if (element) {
      return element.getText();
    }

    return null;
  }

  static async driver() {
    return new Builder().forBrowser('firefox').setFirefoxOptions(new firefox.Options().headless()).build();
  }

  async request(timeout = config.timeout.checkPageId) {
    await this.driver.get(this.url);
    return this.checkPageId(timeout);
  }

  setDriver(driver) {
    this.driver = driver;
  }
}

module.exports = BasePage;
