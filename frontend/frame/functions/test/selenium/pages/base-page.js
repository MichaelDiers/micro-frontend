const { Builder, By, until } = require('selenium-webdriver');
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

  static async driver() {
    return new Builder().forBrowser('firefox').build();
  }

  static async fullscreen(driver) {
    await driver.manage().window().fullscreen();
  }

  static async mobilescreen(driver) {
    await driver.manage().window().setRect({
      width: 300,
      height: 800,
      x: 0,
      y: 0,
    });
  }

  async request(timeout = config.timeout.checkPageId) {
    await this.driver.get(this.url);
    return this.checkPageId(timeout);
  }
}

module.exports = BasePage;
