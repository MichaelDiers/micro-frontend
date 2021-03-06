/** eslint-disable func-names */
const BasePage = require('./base-page');
const config = require('../helper/selenium-config');

class FrameIndexPage extends BasePage {
  constructor(driver) {
    super(driver, config.url.frameIndex, 'id_c414bd3632514e83807ad8faf0b651a1');
  }

  async headline() {
    return super.getText('main > h1');
  }
}

module.exports = FrameIndexPage;
