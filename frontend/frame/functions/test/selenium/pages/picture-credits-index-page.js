const BasePage = require('./base-page');
const config = require('../helper/selenium-config');

class PictureCreditsIndexPage extends BasePage {
  constructor(driver) {
    super(driver, config.url.pictureCreditsIndex, 'id_ca156b87aaaf42fd9ff1ccecda7828c2');
  }

  async headline() {
    return super.getText('main > h1');
  }
}

module.exports = PictureCreditsIndexPage;
