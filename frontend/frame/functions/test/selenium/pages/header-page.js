const BasePage = require('./base-page');
const config = require('../helper/selenium-config');

class HeaderPage extends BasePage {
  constructor(driver) {
    super(driver, config.url.frame, 'id_c414bd3632514e83807ad8faf0b651a1');
  }

  async clickOnLogo() {
    return super.clickOn('header > .logo');
  }

  async shopNameText() {
    return super.getText('header > .brand');
  }
}

module.exports = HeaderPage;
