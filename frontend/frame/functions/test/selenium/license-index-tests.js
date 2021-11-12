/* eslint-disable func-names, prefer-arrow-callback */

const assert = require('assert');
const BasePage = require('./pages/base-page');
const LicenseIndexPage = require('./pages/license-index-page');
const windowSize = require('./helper/window-size');

describe('license/index page', function () {
  let driver = null;
  this.timeout(5000);

  before(async function () {
    driver = await BasePage.driver();
  });

  after(async function () {
    await driver.quit();
  });

  windowSize.sizes().forEach((setSizeFunction) => {
    describe(setSizeFunction.name, function () {
      it('check headline', async function () {
        await setSizeFunction(driver);
        const page = new LicenseIndexPage(driver);
        await page.request();
        assert.equal(await page.headline(), 'License index');
      });
    });
  });
});
