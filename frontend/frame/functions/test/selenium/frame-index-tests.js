/* eslint-disable func-names, prefer-arrow-callback */

const assert = require('assert');
const BasePage = require('./pages/base-page');
const FrameIndexPage = require('./pages/frame-index-page');

describe('frame/index page', function () {
  let driver = null;
  this.timeout(5000);

  before(async function () {
    driver = await BasePage.driver();
  });

  after(async function () {
    await driver.quit();
  });

  describe('mobile', function () {
    it('check headline', async function () {
      await BasePage.mobilescreen(driver);
      const frameIndexPage = new FrameIndexPage(driver);
      await frameIndexPage.request();
      assert.equal(await frameIndexPage.headline(), 'Frame index');
    });
  });

  describe('fullscreen', function () {
    it('check headline', async function () {
      await BasePage.fullscreen(driver);
      const frameIndexPage = new FrameIndexPage(driver);
      await frameIndexPage.request();
      assert.equal(await frameIndexPage.headline(), 'Frame index');
    });
  });
});
