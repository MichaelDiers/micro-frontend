/** eslint-disable func-names */
const BasePage = require('./base-page');
const config = require('../helper/selenium-config');

class LicenseIndexPage extends BasePage {
  constructor(driver) {
    super(driver, config.url.licenseIndex, 'id_c29b58e4b3ab492692b4bd06dc90aeb5');
  }

  async headline() {
    return super.getText('main > h1');
  }
}

module.exports = LicenseIndexPage;
