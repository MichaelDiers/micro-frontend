/* eslint-disable func-names, prefer-arrow-callback */

const assert = require('assert');
const BasePage = require('./pages/base-page');
const FooterPage = require('./pages/footer-page');
const windowSize = require('./helper/window-size');
const collections = require('./helper/collections');
const LicenseIndexPage = require('./pages/license-index-page');
const PictureCreditsIndexPage = require('./pages/picture-credits-index-page');

describe('footer tests', function () {
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
      collections.pages().forEach((page) => {
        it(`check footer for ${page.constructor.name}`, async function () {
          page.setDriver(driver);
          await setSizeFunction(driver);

          const footerPage = new FooterPage(driver);
          const licenseIndexPage = new LicenseIndexPage(driver);
          const pictureCreditsIndexPage = new PictureCreditsIndexPage(driver);

          assert.equal(await page.request(), true);
          await footerPage.clickOnLicenses();
          assert.equal(await licenseIndexPage.checkPageId(), true);

          assert.equal(await page.request(), true);
          await footerPage.clickOnPictureCredits();
          assert.equal(await pictureCreditsIndexPage.checkPageId(), true);
        });
      });
    });
  });
});
