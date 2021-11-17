/* eslint-disable func-names, prefer-arrow-callback */

const assert = require('assert');
const BasePage = require('./pages/base-page');
const FrameIndexPage = require('./pages/frame-index-page');
const language = require('../../app/language/language');
const windowSize = require('./helper/window-size');

describe('frame/index page', function () {
  let driver = null;
  this.timeout(5000);

  before(async function () {
    driver = await BasePage.driver();
  });

  after(async function () {
    await driver.quit();
  });

  language.supported.forEach((lang) => {
    windowSize.sizes().forEach((setSizeFunction) => {
      describe(setSizeFunction.name, function () {
        it('check headline', async function () {
          await setSizeFunction(driver);
          const frameIndexPage = new FrameIndexPage(driver);
          await frameIndexPage.request(lang);
          assert.equal(await frameIndexPage.headline(), language.translations(lang).frame.headline);
        });
      });
    });
  });
});
