/** eslint-disable func-names */
const { By } = require('selenium-webdriver');
const BasePage = require('./base-page');
const config = require('../helper/selenium-config');

class FrameIndexPage extends BasePage {
  constructor(driver) {
    super(driver, config.url.frameIndex, 'id_c414bd3632514e83807ad8faf0b651a1');
  }

  async headline() {
    const element = await this.driver.findElement(By.css('main > h1'));
    if (element) {
      return element.getText();
    }

    return null;
  }
}

module.exports = FrameIndexPage;
