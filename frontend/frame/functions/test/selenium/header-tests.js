/* eslint-disable func-names, prefer-arrow-callback */

const assert = require('assert');
const BasePage = require('./pages/base-page');
const FrameIndexPage = require('./pages/frame-index-page');
const HeaderPage = require('./pages/header-page');
const windowSize = require('./helper/window-size');
const collections = require('./helper/collections');

describe('header/tests', function () {
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
        it(`check header for ${page.constructor.name}`, async function () {
          page.setDriver(driver);
          await setSizeFunction(driver);

          const frameIndexPage = new FrameIndexPage(driver);
          const headerPage = new HeaderPage(driver);

          assert.equal(await page.request(), true);
          assert.equal(await headerPage.shopNameText(), 'The Micro Frontend Shop');
          await headerPage.clickOnLogo();

          assert.equal(await frameIndexPage.checkPageId(), true);
        });
      });
    });
  });
});
