/** eslint-disable func-names */
const { By } = require('selenium-webdriver');
const BasePage = require('./base-page');
const config = require('../helper/selenium-config');

class LicenseIndexPage extends BasePage {
  constructor(driver) {
    super(driver, config.url.licenseIndex, 'id_c29b58e4b3ab492692b4bd06dc90aeb5');
  }

  async headline() {
    const element = await this.driver.findElement(By.css('main > h1'));
    if (element) {
      return element.getText();
    }

    return null;
  }
}

module.exports = LicenseIndexPage;
