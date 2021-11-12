const BasePage = require('./base-page');
const config = require('../helper/selenium-config');

class FooterPage extends BasePage {
  constructor(driver) {
    super(driver, config.url.frame, 'id_c414bd3632514e83807ad8faf0b651a1');
  }

  async clickOnLicenses() {
    return super.clickOn('footer > #licenseLink');
  }

  async clickOnPictureCredits() {
    return super.clickOn('footer > #pictureCreditLink');
  }
}

module.exports = FooterPage;
